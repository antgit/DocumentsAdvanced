DECLARE @RootCompanyId INT = 5
DECLARE @MyCompanyId INT = 56
-------------------------------------------------------------------------------
DECLARE @tblTmlId TABLE (Id INT)
INSERT @tblTmlId(Id)
SELECT TML_ID FROM Донецк7.dbo.TEMPLATES t WHERE (t.FRM_ID = 275 OR t.FRM_ID = 16)
AND t.TML_NAME IN
('4.2.02. Реализация (молочка)',
'4.2.01. Реализация',
'4.2.05. Оптовая отгрузка',
'4.2.09. Реализация ( данон )'
)

DECLARE @tblDocs TABLE (SourceId INT, Guid UNIQUEIDENTIFIER, Date date, NAME NVARCHAR(255), Number NVARCHAR(50), KindId INT, FormId INT, FolderId INT,
                        CurrencyId INT,AgentFromGuid UNIQUEIDENTIFIER,AgentToGuid UNIQUEIDENTIFIER, Summa money
                       )

INSERT @tblDocs(SourceId, Guid, Date, [NAME], Number, KindId, FormId, FolderId, CurrencyId,
       AgentFromGuid, AgentToGuid, Summa)
SELECT d.DOC_ID AS Id, d.DOC_GUID AS Guid, d.DOC_DATE, d.DOC_NAME AS Name, d.DOC_NO, 131073 AS Kind,75 AS FormId, 35 AS FolderId,
  515 AS CurrencyId,
  a.AG_GUID AS AgentFromGuid,
  a2.AG_GUID AS AgentToGuid, isnull(d.DOC_SUM,0)
  FROM Донецк7.dbo.DOCUMENTS d INNER JOIN @tblTmlId t ON d.TML_ID = t.Id
  INNER JOIN Донецк7.dbo.JOURNAL j ON d.DOC_ID = j.DOC_ID
  LEFT JOIN Донецк7.dbo.AGENTS a ON j.J_AG1 = a.AG_ID
  LEFT JOIN Донецк7.dbo.AGENTS a2 ON j.J_AG1 = a2.AG_ID
WHERE d.DOC_DONE = 2 AND j.J_TR_NO = 0 AND j.J_LN_NO = 0

INSERT INTO [Document].Documents(Guid,DatabaseId,DbSourceId,Flags,StateId,
	[Name],	Kind,Date,FormId,FolderId,AgentFromId,AgentDepatmentFromId, AgentToId,
	CurrencyId,Summa,Number)
SELECT d.Guid,3,d.SourceId,0,1,d.NAME,d.KindId,d.Date, d.FormId, d.FolderId,
       @RootCompanyId,@MyCompanyId, a2.Id,515,d.Summa,d.Number
  FROM 
@tblDocs d LEFT JOIN Contractor.Agents a ON d.AgentFromGuid = a.Guid
           LEFT JOIN Contractor.Agents a2 ON d.AgentFromGuid = a2.Guid
LEFT JOIN [Document].Documents d2 ON d.Guid = d2.Guid
WHERE d2.Guid IS NULL
-- Дополнительная шапка
INSERT Sales.Documents(Id,Guid,DatabaseId,DbSourceId,Flags,StateId,Date,Kind,SupervisorId,ManagerId,DeliveryId,PrcContentId,TaxDocId)
SELECT d.Id,d.Guid,3,d.DbSourceId,0,1,d.Date,d.Kind, NULL, NULL, NULL, NULL, NULL 
FROM [Document].Documents d 
LEFT JOIN Sales.Documents d2 ON d.Id = d2.Id WHERE d2.Id IS NULL
and d.Kind = 131073

DECLARE @tblDocAgentDelivery TABLE (Id UNIQUEIDENTIFIER, AgId int)
INSERT @tblDocAgentDelivery(Id, AgId)
SELECT d3.DOC_GUID, j.J_AG2 FROM 
Донецк7.dbo.DOCUMENTS d3 INNER JOIN Донецк7.dbo.JOURNAL j ON d3.DOC_ID=j.DOC_ID
WHERE j.J_TR_NO = 0 AND j.J_LN_NO = 0 AND j.J_AG2 IS NOT NULL

UPDATE Sales.Documents SET DeliveryId = a.Id 
FROM [Document].Documents d INNER JOIN Sales.Documents d2 ON d.Id=d2.Id
INNER JOIN @tblDocAgentDelivery d3 ON d.Guid = d3.Id
INNER JOIN Contractor.Agents a ON d3.AgId=a.DbSourceId
WHERE DeliveryId IS NULL OR DeliveryId<>a.Id
-------------------------------------------------------------------------------

INSERT Sales.DocumentDetails(Guid, DatabaseId, DbSourceId, Flags, StateId, Date, OwnId,
       Kind, ProductId, UnitId, Price, Qty, Summa)
SELECT j.J_GUID,3, j.J_ID,0,1,d2.Date,d2.Id,d2.Kind,p.Id, NULL, isnull(j.J_PRICE,0), isnull(j.J_QTY,0), isnull(j.J_SUM,0)
  FROM Донецк7.dbo.DOCUMENTS d INNER JOIN [Document].Documents d2 ON d.DOC_ID = d2.DbSourceId
INNER JOIN Донецк7.dbo.JOURNAL j ON d.DOC_ID = j.DOC_ID
INNER JOIN Донецк7.dbo.ENTITIES e ON j.J_ENT=e.ENT_ID
inner join Product.Products p on e.ENT_GUID = p.Guid
LEFT JOIN Sales.DocumentDetails mj ON mj.Guid = j.J_GUID 
WHERE j.J_TR_NO = 0 AND d2.Kind = 131073 AND mj.Guid IS NULL







