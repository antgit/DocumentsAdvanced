DECLARE @ds DATE = '20100101'
DECLARE @de DATE = '20110101'

DECLARE @DestinationTmlId INT = 98345 -- Идентификатор шаблона в базе "Документы"
DECLARE @DestinationDatabaseId INT = 3 -- Идентификатор базы источника (Донецк=3) в базе "Документы"
DECLARE @DestinationKindId INT = 131073 -- Тип операции в базе "Документы"
DECLARE @DestinationFormId INT = 75 -- Идентификатор формы в базе "Документы"
DECLARE @DestinationFolderId INT = 35 -- Идентификатор папки в базе "Документы"

DECLARE @DestinationMainAgId INT = 5

DECLARE @StepId INT = 3

-- Идентификаторы шаблонов тип операции "Реализация"
DECLARE @tblTmlParams TABLE (Id INT)
INSERT @tblTmlParams(Id)
SELECT v.TML_ID from TML_PARAM_NAMES n
INNER JOIN TML_PARAMS v ON n.PRM_ID=v.PRM_ID
WHERE n.NODE_TYPE = 1 AND n.PRM_NAME = 'Тип операции' AND v.PRM_LONG = 1

-- Таблица идентификаторов документов
CREATE TABLE #tmpDocId(Id int)
INSERT #tmpDocId(Id)
SELECT top 1000 d.DOC_ID from DOCUMENTS d INNER JOIN @tblTmlParams t ON d.TML_ID = t.Id
          WHERE d.DOC_DONE = 2
                AND d.DOC_DATE >=@ds AND d.DOC_DATE<=@de

-- Таблица идентификаторов корреспондентов
CREATE TABLE #tmpAgentId(Id INT, Guid UNIQUEIDENTIFIER, [Name] NVARCHAR(255))
-- Таблица идентификаторов товара
CREATE TABLE #tmpProductId(Id INT,Guid UNIQUEIDENTIFIER, Nom NVARCHAR(50), [Name] NVARCHAR(255))

IF(@StepId>=1)
BEGIN
INSERT #tmpAgentId(Id)
SELECT distinct j.J_AG1 FROM #tmpDocId d INNER JOIN JOURNAL j ON d.Id=j.DOC_ID AND j.J_TR_NO = 0 AND j.J_LN_NO = 0 WHERE j.J_AG1 IS NOT NULL
UNION 
SELECT distinct j.J_AG2 FROM #tmpDocId d INNER JOIN JOURNAL j ON d.Id=j.DOC_ID AND j.J_TR_NO = 0 AND j.J_LN_NO = 0 WHERE j.J_AG2 IS NOT NULL

UPDATE #tmpAgentId
SET #tmpAgentId.Guid = a2.AG_GUID, #tmpAgentId.[Name] = a2.AG_NAME
FROM #tmpAgentId a INNER JOIN dbo.AGENTS a2 ON a.Id=a2.AG_ID
--SELECT * FROM #tmpAgentId
END
IF(@StepId>=2)
BEGIN

INSERT #tmpProductId(Id)
SELECT distinct j.J_ENT
  FROM #tmpDocId d INNER JOIN JOURNAL j ON d.Id=j.DOC_ID AND j.J_TR_NO = 0 WHERE j.J_ENT IS NOT NULL

UPDATE #tmpProductId
SET #tmpProductId.Guid = e.ENT_GUID, #tmpProductId.Nom = e.ENT_NOM, #tmpProductId.[Name] = e.ENT_NAME
FROM #tmpProductId p INNER JOIN dbo.ENTITIES e ON p.Id=e.ENT_ID 

--SELECT * FROM #tmpProductId
END
IF(@StepId<3) RETURN
CREATE TABLE #tmpDocSumSales(Id INT, SummSales MONEY)
INSERT #tmpDocSumSales(Id, SummSales)
SELECT d.Id, SUM(j.J_SUM) FROM #tmpDocId d INNER JOIN JOURNAL j ON d.Id=j.DOC_ID
WHERE j.J_TR_NO = 0
GROUP BY d.Id

CREATE TABLE #tmpDocSumTax(Id INT, SummTax MONEY)
INSERT #tmpDocSumTax(Id, SummTax)
SELECT d.Id, SUM(j.J_SUM) FROM #tmpDocId d INNER JOIN JOURNAL j ON d.Id=j.DOC_ID
WHERE j.J_TR_NO = 1
GROUP BY d.Id

-- Временная таблица "шапки документов"
CREATE TABLE #tmpDocuments(
	[Guid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[DatabaseId] [int] NOT NULL,
	[DbSourceId] [int] NULL,
	[StateId] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Kind] [int] NOT NULL,
	[Code] [nvarchar](100) NULL,
	[Memo] [nvarchar](max) SPARSE  NULL,
	[TemplateId] [int] NULL,
	[Date] [date] NOT NULL,
	[FormId] [int] NULL,
	[FolderId] [int] NOT NULL,
	[AgentFromId] [int] NULL,
	[AgentToId] [int] NULL,
	[AgentDepartmentFromId] [int] NULL,
	[AgentDepartmentToId] [int] NULL,
	[AgentFromName] [nvarchar](255) NULL,
	[AgentToName] [nvarchar](255) NULL,
	[AgentDepatmentFromName] [nvarchar](255) NULL,
	[AgentDepatmentToName] [nvarchar](255) NULL,
	DeliveryId INT,
	[CurrencyId] [int] NULL,	
	[Summa] [money] NOT NULL,
	[SummaTax] [money] NOT NULL,
	[Number] [nvarchar](50) NULL
	)
INSERT #tmpDocuments(Guid, DatabaseId,DbSourceId,StateId, [Name], Kind, Code, Memo,
       TemplateId, Date, FormId, FolderId, AgentFromId, AgentToId,
       AgentDepartmentFromId, AgentDepartmentToId, AgentFromName, AgentToName,
       AgentDepartmentFromName, AgentDepartmentToName, DeliveryId, CurrencyId, Summa, SummaTax,
       Number)
SELECT d.DOC_GUID AS Guid, @DestinationDatabaseId AS DatabaseId, d.DOC_ID AS DbSourceId, 1 AS StateId, d.DOC_NAME as Name, 
       @DestinationKindId AS KindId, d.DOC_TAG AS Code, d.DOC_MEMO AS Memo, @DestinationTmlId AS TemplateId,
       d.DOC_DATE AS [Date], @DestinationFormId AS FormId, @DestinationFolderId AS FolderId,
       @DestinationMainAgId AS AgentFromId, 
       j.J_AG1 as AgentToId, -- 'Идентификатор клиента' 
       0 as AgentDepatmentFromId, --'Идентификатор филиала'
       j.J_AG1 as AgentDepartmentToId, --'Идентификатор клиента' 
       0 as AgentFromName, --'Имя главного корреспондета'
       a.[Name] as AgentToName, -- 'Имя клиента'
       '' as AgentDepartmentFromName, -- 'Имя филиала' 
       a.[Name] as AgentDepartmentToName, --'Имя клиента'
       a2.Id AS DeliveryId,
       515 AS CurrencyId,
       ds.SummSales AS Summa, -- правильно расчитать сумму = сумме строк первой проводки
       dt.SummTax as SummaTax, -- = сумме строк второй проводки
       d.DOC_NO as Number
  FROM DOCUMENTS d INNER JOIN #tmpDocId t ON d.DOC_ID = t.Id
       INNER JOIN JOURNAL j ON d.DOC_ID=j.DOC_ID AND j.J_TR_NO = 0 AND j.J_LN_NO = 0
       LEFT JOIN #tmpAgentId a ON j.J_AG1 = a.Id
       LEFT JOIN #tmpAgentId a2 ON j.J_AG2 = a2.Id 
       INNER JOIN #tmpDocSumSales ds ON ds.Id=t.Id
       INNER JOIN #tmpDocSumTax dt ON dt.Id=t.Id
WHERE d.DOC_DONE = 2
      AND d.DOC_DATE >=@ds AND d.DOC_DATE<=@de

SELECT * FROM #tmpDocuments ORDER BY DbSourceId

CREATE TABLE #tmpJrn(Id INT, Guid UNIQUEIDENTIFIER, OwnId INT, RowNo INT, Summa MONEY, Price MONEY, Qty MONEY, ProductId INT, SumTax MONEY)
INSERT #tmpJrn(Id, Guid, OwnId, RowNo, Summa, Price, Qty, ProductId)
SELECT j.J_ID,j.J_GUID, d.Id, j.J_LN_NO, j.J_SUM, j.J_PRICE, j.J_QTY, j.J_ENT
  FROM #tmpDocId d inner JOIN JOURNAL j ON d.Id=j.DOC_ID
WHERE j.J_TR_NO = 0 

UPDATE #tmpJrn
SET #tmpJrn.SumTax = j.J_SUM
FROM #tmpJrn d inner JOIN JOURNAL j ON d.OwnId=j.DOC_ID AND d.RowNo=j.J_LN_NO
WHERE j.J_TR_NO = 1

SELECT * FROM #tmpJrn ORDER BY OwnId

DROP TABLE #tmpDocuments
DROP TABLE #tmpAgentId
DROP TABLE #tmpProductId
DROP TABLE #tmpDocId
DROP TABLE #tmpDocSumSales
DROP TABLE #tmpDocSumTax
DROP TABLE #tmpJrn
