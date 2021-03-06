--DELETE FROM [Document].Documents WHERE Date>='20100101' AND Kind=131074
DECLARE @Kind INT=131074 -- ��� ��������
DECLARE @FormId INT=74   -- �� ����� ���������
DECLARE @FolderId INT=54   -- �� �����
DECLARE @RootCompanyId INT = 5
DECLARE @MyCompanyId INT = 56

DECLARE @MyCompanyGuid UNIQUEIDENTIFIER
SELECT @MyCompanyGuid = Guid FROM Contractor.Agents a WHERE id=@MyCompanyId

DECLARE @tblTmlId TABLE (Id INT)
INSERT @tblTmlId(Id)
SELECT TML_ID FROM ������7.dbo.TEMPLATES t WHERE 
t.TML_NAME IN('11.04.01. ������ ������� �� ������')


DECLARE @tblDocs TABLE (SourceId INT, Guid UNIQUEIDENTIFIER, Date date, NAME NVARCHAR(255), Number NVARCHAR(50), KindId INT, FormId INT, FolderId INT,
                        CurrencyId INT,AgentFromGuid UNIQUEIDENTIFIER,AgentToGuid UNIQUEIDENTIFIER, Summa money
                       )

INSERT @tblDocs(SourceId, Guid, Date, [NAME], Number, KindId, FormId, FolderId, CurrencyId,
       AgentFromGuid, AgentToGuid, Summa)
SELECT d.DOC_ID AS Id, d.DOC_GUID AS Guid, d.DOC_DATE, d.DOC_NAME AS Name, d.DOC_NO, @Kind AS Kind,@FormId AS FormId, @FolderId AS FolderId,
  515 AS CurrencyId,
  a.AG_GUID AS AgentFromGuid,
  a2.AG_GUID AS AgentToGuid, isnull(d.DOC_SUM,0)
  FROM ������7.dbo.DOCUMENTS d INNER JOIN @tblTmlId t ON d.TML_ID = t.Id
  INNER JOIN ������7.dbo.JOURNAL j ON d.DOC_ID = j.DOC_ID
  LEFT JOIN ������7.dbo.AGENTS a ON j.J_AG1 = a.AG_ID
  LEFT JOIN ������7.dbo.AGENTS a2 ON j.J_AG2 = a2.AG_ID
WHERE d.DOC_DONE = 2 AND j.J_TR_NO = 0 AND j.J_LN_NO = 0 AND d.DOC_DATE>='20100101'

INSERT INTO [Document].Documents(Guid,DatabaseId,DbSourceId,Flags,StateId,
	[Name],	Kind,Date,FormId,FolderId,AgentFromId,AgentDepartmentFromId, AgentToId,AgentDepartmentToId,
	AgentFromName,AgentToName,CurrencyId,Summa,Number)
SELECT d.Guid,3,d.SourceId,0,1,d.NAME,d.KindId,d.Date, d.FormId, d.FolderId,
       a2.Id,           -- ����������� ���
       null,           -- ������������� ���
       @RootCompanyId, -- ����������� ����
       @MyCompanyId,   -- ������������� ����
       --
       a2.[Name],'�� "���������� �������"',515,d.Summa,d.Number
  FROM 
@tblDocs d LEFT JOIN Contractor.Agents a ON d.AgentFromGuid = a.Guid
           LEFT JOIN Contractor.Agents a2 ON d.AgentToGuid = a2.Guid
LEFT JOIN [Document].Documents d2 ON d.Guid = d2.Guid
WHERE d2.Guid IS NULL
-- �������������� �����
INSERT Sales.Documents(Id,Guid,DatabaseId,DbSourceId,Flags,StateId,Date,Kind, StoreId,SupervisorId,ManagerId,DeliveryId,PrcContentId,TaxDocId)
SELECT d.Id,d.Guid,3,d.DbSourceId,0,1,d.Date,d.Kind, a.Id,NULL, NULL, NULL, NULL, NULL 
FROM [Document].Documents d INNER JOIN @tblDocs d3 ON d.Guid=d3.Guid
LEFT JOIN Sales.Documents d2 ON d.Id = d2.Id 
LEFT JOIN Contractor.Agents a ON a.Guid=d3.AgentFromGuid
WHERE d2.Id IS NULL
and d.Kind = @Kind

-------------------------------------------------------------------------------

INSERT Sales.DocumentDetails(Guid,DatabaseId, DbSourceId, Flags, StateId, Date, OwnId,
       Kind, ProductId, UnitId, Price, Qty, Summa)
SELECT j.J_GUID,3, j.J_ID,0,1,d2.Date,d2.Id,d2.Kind,p.Id, NULL, isnull(j.J_PRICE,0), isnull(j.J_QTY,0), isnull(j.J_SUM,0)
  FROM ������7.dbo.DOCUMENTS d INNER JOIN [Document].Documents d2 ON d.DOC_ID = d2.DbSourceId
       INNER JOIN @tblDocs d3 ON d2.Guid = d3.Guid
INNER JOIN ������7.dbo.JOURNAL j ON d.DOC_ID = j.DOC_ID
INNER JOIN ������7.dbo.ENTITIES e ON j.J_ENT=e.ENT_ID
inner join Product.Products p on e.ENT_GUID = p.Guid
LEFT JOIN Sales.DocumentDetails j2 ON j2.Guid=j.J_Guid
WHERE j.J_TR_NO = 0 AND d2.Kind = @Kind AND j2.Guid IS null