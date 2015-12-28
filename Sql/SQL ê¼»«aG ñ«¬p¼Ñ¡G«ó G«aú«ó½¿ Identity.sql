--DELETE FROM [Document].Documents WHERE Date>='20100101' AND Kind=131074
SET IDENTITY_INSERT [Document].Documents ON 
INSERT [Document].Documents(Id,Guid, DatabaseId, DbSourceId, UserName, DateModified,
       Flags, StateId, [Name], Kind, Code, Memo, FlagString, TemplateId, Date,
       FormId, FolderId, AgentFromId, AgentToId, AgentDepartmentFromId,
       AgentDepartmentToId, AgentFromName, AgentToName, AgentDepartmentFromName,
       AgentDepartmentToName, CurrencyId, CurrencyIdTrans, CurrencyIdCountry, Summa,
       SummaBase, SummaCurrency, SummaTax, Number, [Time])
SELECT s.Id,s.Guid, s.DatabaseId, s.DbSourceId, s.UserName, s.DateModified,
       s.Flags, s.StateId, s.[Name], s.Kind, s.Code, s.Memo, s.FlagString, s.TemplateId, s.Date,
       s.FormId, s.FolderId, s.AgentFromId, s.AgentToId, s.AgentDepartmentFromId,
       s.AgentDepartmentToId, s.AgentFromName, s.AgentToName, s.AgentDepartmentFromName,
       s.AgentDepartmentToName, s.CurrencyId, s.CurrencyIdTrans, s.CurrencyIdCountry, s.Summa,
       s.SummaBase, s.SummaCurrency, s.SummaTax, s.Number, s.[Time]
 FROM [srv-devel].[Documents2009].[Document].Documents s LEFT JOIN
[Document].Documents d ON d.Guid = s.Guid
WHERE d.Guid IS NULL;
SET IDENTITY_INSERT [Document].Documents OFF

INSERT Sales.Documents(Id,Guid, DatabaseId, DbSourceId, UserName, DateModified, Flags,
       StateId, Date, Kind, SupervisorId, ManagerId, DeliveryId, PrcContentId,
       TaxDocId, StoreId)
SELECT s.Id,s.Guid, s.DatabaseId, s.DbSourceId, s.UserName, s.DateModified, s.Flags,
       s.StateId, s.Date, s.Kind, s.SupervisorId, s.ManagerId, s.DeliveryId, s.PrcContentId,
       s.TaxDocId, s.StoreId
 FROM [srv-devel].[Documents2009].Sales.Documents s LEFT JOIN
Sales.Documents d ON d.Guid = s.Guid
WHERE d.Guid IS NULL;

INSERT INTO Sales.DocumentDetails(Guid, DatabaseId, DbSourceId, UserName,
            DateModified, Flags, StateId, Date, OwnId, Kind, ProductId, UnitId,
            Price, Qty, Summa, Memo, FUnitId, FQty, TaxAnaliticId)
select s.Guid, s.DatabaseId, s.DbSourceId, s.UserName,
            s.DateModified, s.Flags, s.StateId, s.Date, s.OwnId, s.Kind, s.ProductId, s.UnitId,
            s.Price, s.Qty, s.Summa, s.Memo, s.FUnitId, s.FQty, s.TaxAnaliticId
 FROM [srv-devel].[Documents2009].Sales.DocumentDetails s LEFT JOIN
Sales.DocumentDetails d ON d.Guid = s.Guid
WHERE d.Guid IS NULL;