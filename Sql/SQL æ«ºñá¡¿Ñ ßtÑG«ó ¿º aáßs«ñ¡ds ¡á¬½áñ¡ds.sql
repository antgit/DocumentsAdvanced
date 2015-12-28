SET NOCOUNT ON
BEGIN TRAN
DECLARE @SourceId TABLE(Id INT)

INSERT @SourceId(Id)
SELECT TOP 2000 Id FROM [Document].Documents d WHERE d.Kind = 131073 AND d.IsTemplate=0

DECLARE @IdList TABLE (Id INT, SourceId INT, Dt date)
INSERT INTO [Document].Documents(DatabaseId, Flags, Name, StateId, Kind, Date, FormId,
            FolderId, AgentFromId, AgentToId, AgentDepartmentFromId,
            AgentDepartmentToId, AgentFromName, AgentToName, AgentDepartmentFromName,
            AgentDepartmentToName, CurrencyId, CurrencyIdTrans, CurrencyIdCountry,
            Summa, SummaBase, SummaCurrency, SummaTax, Number,DbSourceId)
OUTPUT INSERTED.Id, INSERTED.DbSourceId, INSERTED.Date INTO @IdList
SELECT TOP 2000 
DatabaseId, Flags, '—чет исход€щий' AS NAME, StateId, 131079 as Kind, DATEADD(d,-1, Date) AS Date, 81 as FormId,
            34 as FolderId, AgentFromId, AgentToId, AgentDepartmentFromId,
            AgentDepartmentToId, AgentFromName, AgentToName, AgentDepartmentFromName,
            AgentDepartmentToName, CurrencyId, CurrencyIdTrans, CurrencyIdCountry,
            Summa, SummaBase, SummaCurrency, SummaTax, Number,d.Id
 FROM [Document].Documents d INNER JOIN @SourceId s ON d.Id=s.Id


INSERT INTO Sales.Documents(Id,DatabaseId, Flags, StateId, Date, Kind, SupervisorId,
            ManagerId, DeliveryId, PrcContentId, TaxDocId, StoreId)
SELECT n.Id, DatabaseId, Flags, StateId, n.Dt as Date, 131079 as Kind, SupervisorId,
            ManagerId, DeliveryId, PrcContentId, TaxDocId, StoreId FROM Sales.Documents d INNER JOIN @IdList n ON d.Id=n.SourceId

SELECT @@ROWCOUNT            

INSERT INTO Sales.DocumentDetails(DatabaseId, Flags, StateId, Date, OwnId, Kind,
            ProductId, UnitId, Price, Qty, Summa, Memo, FUnitId, FQty,
            TaxAnaliticId)
SELECT DatabaseId, Flags, StateId, Date, n.Id, 131079 as Kind,
            ProductId, UnitId, Price, Qty, Summa, Memo, FUnitId, FQty,
            TaxAnaliticId FROM Sales.DocumentDetails d INNER JOIN @IdList n ON d.OwnId = n.SourceId 

SELECT @@ROWCOUNT

SELECT * FROM @IdList il
INSERT INTO [Document].DocumentChains(DatabaseId,Flags, StateId, LeftId, RightId, Kind,OrderNo)
SELECT 1, 0,1,d.Id,d.SourceId,20,0 FROM @IdList d


COMMIT TRAN