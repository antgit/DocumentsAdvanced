USE MASTER;
GO
IF NOT  EXISTS (SELECT srv.name FROM sys.servers srv WHERE srv.server_id != 0 AND srv.name = N'<Server_Name, sysname, Server_Name>')
BEGIN
EXEC master.dbo.sp_addlinkedserver @server = N'<Server_Name, sysname, Server_Name>', @srvproduct=N'SQL Server'
EXEC master.dbo.sp_addlinkedsrvlogin @rmtsrvname = N'<Server_Name, sysname, Server_Name>', @locallogin = NULL , @useself = N'True'
end
GO

USE [<SOURCEDB_NAME, sysname, SOURCEDB_NAME>] 
GO
SET NOCOUNT ON
GO
INSERT INTO Core.Entities(Id, Guid, DatabaseId, DbSourceId, Flags, StateId, Name,Code, MaxKind, Memo)
SELECT a.Id, a.Guid, a.DatabaseId, a.DbSourceId, a.Flags, a.StateId, a.Name, a.Code,a.MaxKind, a.Memo
from [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Entities a LEFT JOIN Core.Entities s ON a.Id=s.Id WHERE s.Id IS NULL;

UPDATE Core.Entities
SET Core.Entities.Id = s.Id,
Core.Entities.Guid = s.Guid,
Core.Entities.DatabaseId = s.DatabaseId,
Core.Entities.DbSourceId = s.DbSourceId,
Core.Entities.Flags = s.Flags,
Core.Entities.StateId =s.StateId,
Core.Entities.Name = s.Name,
Core.Entities.Code = s.Code,
Core.Entities.MaxKind = s.MaxKind,
Core.Entities.Memo = s.Memo
FROM Core.Entities a INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Entities s ON a.Id=s.Id;
PRINT 'Выполнено обновление таблицы Core.Entities...'
GO
INSERT INTO Core.EntityKinds(Id, Guid, DatabaseId, DbSourceId, Flags, StateId,Code, Memo, Name, EntityId, SubKind)
SELECT a.Id, a.Guid, a.DatabaseId, a.DbSourceId, a.Flags, a.StateId, a.Code,a.Memo, a.Name, a.EntityId, a.SubKind
  FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.EntityKinds a LEFT JOIN Core.EntityKinds s ON a.Id=s.Id WHERE s.Id IS NULL;

UPDATE Core.EntityKinds
SET Core.EntityKinds.Id = s.Id,
Core.EntityKinds.Guid = s.Guid,
Core.EntityKinds.DatabaseId = s.DatabaseId,
Core.EntityKinds.DbSourceId = s.DbSourceId,
Core.EntityKinds.Flags = s.Flags,
Core.EntityKinds.StateId = s.StateId,
Core.EntityKinds.Code = s.Code,
Core.EntityKinds.Memo = s.Memo,
Core.EntityKinds.Name = s.Name,
Core.EntityKinds.EntityId = s.EntityId,
Core.EntityKinds.SubKind = s.SubKind
FROM Core.EntityKinds a INNER JOIN  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.EntityKinds s ON a.Id=s.Id;
PRINT 'Выполнено обновление таблицы Core.EntityKinds...'
GO
INSERT INTO Core.EntityMaps(Guid, DatabaseId, DbSourceId, Flags, StateId,EntityId, KindId, Name, Method, [Schema], ProcedureName)
SELECT a.Guid, a.DatabaseId, a.DbSourceId, a.Flags, a.StateId, a.EntityId,a.KindId, a.Name, a.Method, a.[Schema], a.ProcedureName
  FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.EntityMaps a LEFT JOIN Core.EntityMaps s ON a.Guid=s.Guid WHERE s.Guid IS NULL;

UPDATE Core.EntityMaps
SET Core.EntityMaps.Guid = s.Guid,
Core.EntityMaps.DatabaseId = s.DatabaseId,
Core.EntityMaps.DbSourceId = s.DbSourceId,
Core.EntityMaps.Flags = s.Flags,
Core.EntityMaps.StateId = s.StateId,
Core.EntityMaps.EntityId = s.EntityId,
Core.EntityMaps.KindId = s.KindId,
Core.EntityMaps.Name = s.Name,
Core.EntityMaps.Method = s.Method,
Core.EntityMaps.[Schema] = s.[Schema],
Core.EntityMaps.ProcedureName = s.ProcedureName
FROM Core.EntityMaps a INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.EntityMaps s ON a.Guid=s.Guid;
PRINT 'Выполнено обновление таблицы Core.EntityMaps...'
GO


INSERT INTO Core.DocumentTypes(Id, Guid, DatabaseId, DbSourceId, Flags, StateId, Name,Code,  Memo)
SELECT a.Id, a.Guid, a.DatabaseId, a.DbSourceId, a.Flags, a.StateId, a.Name, a.Code,a.Memo
from [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.DocumentTypes a LEFT JOIN Core.DocumentTypes s ON a.Id=s.Id WHERE s.Id IS NULL;
GO
UPDATE Core.DocumentTypes
SET Core.DocumentTypes.Id = s.Id,
Core.DocumentTypes.Guid = s.Guid,
Core.DocumentTypes.DatabaseId = s.DatabaseId,
Core.DocumentTypes.DbSourceId = s.DbSourceId,
Core.DocumentTypes.Flags = s.Flags,
Core.DocumentTypes.StateId =s.StateId,
Core.DocumentTypes.Name = s.Name,
Core.DocumentTypes.Code = s.Code,
Core.DocumentTypes.Memo = s.Memo
FROM Core.DocumentTypes a INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.DocumentTypes s ON a.Id=s.Id;
PRINT 'Выполнено обновление таблицы Core.DocumentTypes...'
GO

INSERT INTO Pl.DocumentKinds(Id, Guid, DatabaseId, DbSourceId, Flags, StateId,Code, Memo, Name, EntityId, KindId,CorrespondenceId,
            UseCustomFilter, AgentFirstFilterId, AgentSecondFilterId,AgentThirdFilterId, AgentFourthFilterId)
SELECT a.Id, a.Guid, a.DatabaseId, a.DbSourceId, a.Flags, a.StateId, a.Code,a.Memo, a.Name, a.EntityId, a.KindId,
a.CorrespondenceId, a.UseCustomFilter, a.AgentFirstFilterId, a.AgentSecondFilterId,a.AgentThirdFilterId, a.AgentFourthFilterId
  FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Pl.DocumentKinds a LEFT JOIN Pl.DocumentKinds s ON a.Id=s.Id WHERE s.Id IS NULL;
GO
UPDATE Pl.DocumentKinds
SET Pl.DocumentKinds.Id = s.Id,
Pl.DocumentKinds.Guid = s.Guid,
Pl.DocumentKinds.DatabaseId = s.DatabaseId,
Pl.DocumentKinds.DbSourceId = s.DbSourceId,
Pl.DocumentKinds.Flags = s.Flags,
Pl.DocumentKinds.StateId = s.StateId,
Pl.DocumentKinds.Code = s.Code,
Pl.DocumentKinds.Memo = s.Memo,
Pl.DocumentKinds.Name = s.Name,
Pl.DocumentKinds.EntityId = s.EntityId,
Pl.DocumentKinds.KindId = s.KindId,
Pl.DocumentKinds.CorrespondenceId = S.CorrespondenceId,
Pl.DocumentKinds.UseCustomFilter = S.UseCustomFilter, 
Pl.DocumentKinds.AgentFirstFilterId=s.AgentFirstFilterId, 
Pl.DocumentKinds.AgentSecondFilterId=s.AgentSecondFilterId,
Pl.DocumentKinds.AgentThirdFilterId=s.AgentThirdFilterId, 
Pl.DocumentKinds.AgentFourthFilterId=s.AgentFourthFilterId
FROM Pl.DocumentKinds a INNER JOIN  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Pl.DocumentKinds s ON a.Id=s.Id;
PRINT 'Выполнено обновление таблицы Pl.EntityKinds...'
GO

INSERT INTO Mktg.DocumentKinds(Id, Guid, DatabaseId, DbSourceId, Flags, StateId,Code, Memo, Name, EntityId, KindId,CorrespondenceId,
            UseCustomFilter, AgentFirstFilterId, AgentSecondFilterId,AgentThirdFilterId, AgentFourthFilterId)
SELECT a.Id, a.Guid, a.DatabaseId, a.DbSourceId, a.Flags, a.StateId, a.Code,a.Memo, a.Name, a.EntityId, a.KindId,
a.CorrespondenceId, a.UseCustomFilter, a.AgentFirstFilterId, a.AgentSecondFilterId,a.AgentThirdFilterId, a.AgentFourthFilterId
  FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Mktg.DocumentKinds a LEFT JOIN Mktg.DocumentKinds s ON a.Id=s.Id WHERE s.Id IS NULL;

 GO
UPDATE Mktg.DocumentKinds
SET Mktg.DocumentKinds.Id = s.Id,
Mktg.DocumentKinds.Guid = s.Guid,
Mktg.DocumentKinds.DatabaseId = s.DatabaseId,
Mktg.DocumentKinds.DbSourceId = s.DbSourceId,
Mktg.DocumentKinds.Flags = s.Flags,
Mktg.DocumentKinds.StateId = s.StateId,
Mktg.DocumentKinds.Code = s.Code,
Mktg.DocumentKinds.Memo = s.Memo,
Mktg.DocumentKinds.Name = s.Name,
Mktg.DocumentKinds.EntityId = s.EntityId,
Mktg.DocumentKinds.KindId = s.KindId,
Mktg.DocumentKinds.CorrespondenceId = S.CorrespondenceId,
Mktg.DocumentKinds.UseCustomFilter = S.UseCustomFilter, 
Mktg.DocumentKinds.AgentFirstFilterId=s.AgentFirstFilterId, 
Mktg.DocumentKinds.AgentSecondFilterId=s.AgentSecondFilterId,
Mktg.DocumentKinds.AgentThirdFilterId=s.AgentThirdFilterId, 
Mktg.DocumentKinds.AgentFourthFilterId=s.AgentFourthFilterId
FROM Mktg.DocumentKinds a INNER JOIN  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Mktg.DocumentKinds s ON a.Id=s.Id;
PRINT 'Выполнено обновление таблицы Mktg.EntityKinds...'
GO

INSERT INTO [Route].DocumentKinds(Id, Guid, DatabaseId, DbSourceId, Flags, StateId,Code, Memo, Name, EntityId, KindId,CorrespondenceId,
            UseCustomFilter, AgentFirstFilterId, AgentSecondFilterId,AgentThirdFilterId, AgentFourthFilterId)
SELECT a.Id, a.Guid, a.DatabaseId, a.DbSourceId, a.Flags, a.StateId, a.Code,a.Memo, a.Name, a.EntityId, a.KindId,
a.CorrespondenceId, a.UseCustomFilter, a.AgentFirstFilterId, a.AgentSecondFilterId,a.AgentThirdFilterId, a.AgentFourthFilterId
  FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Route].DocumentKinds a LEFT JOIN [Route].DocumentKinds s ON a.Id=s.Id WHERE s.Id IS NULL;
GO
UPDATE [Route].DocumentKinds
SET Id = s.Id,
Guid = s.Guid,
DatabaseId = s.DatabaseId,
DbSourceId = s.DbSourceId,
Flags = s.Flags,
StateId = s.StateId,
Code = s.Code,
Memo = s.Memo,
Name = s.Name,
EntityId = s.EntityId,
KindId = s.KindId,
CorrespondenceId = S.CorrespondenceId,
UseCustomFilter = S.UseCustomFilter, 
AgentFirstFilterId=s.AgentFirstFilterId, 
AgentSecondFilterId=s.AgentSecondFilterId,
AgentThirdFilterId=s.AgentThirdFilterId, 
AgentFourthFilterId=s.AgentFourthFilterId
FROM [Route].DocumentKinds a INNER JOIN  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Route].DocumentKinds s ON a.Id=s.Id;
PRINT 'Выполнено обновление таблицы Route.EntityKinds...'
GO

INSERT INTO [Contracts].DocumentKinds(Id, Guid, DatabaseId, DbSourceId, Flags, StateId,Code, Memo, Name, EntityId, KindId,CorrespondenceId,
            UseCustomFilter, AgentFirstFilterId, AgentSecondFilterId,AgentThirdFilterId, AgentFourthFilterId)
SELECT a.Id, a.Guid, a.DatabaseId, a.DbSourceId, a.Flags, a.StateId, a.Code,a.Memo, a.Name, a.EntityId, a.KindId,
a.CorrespondenceId, a.UseCustomFilter, a.AgentFirstFilterId, a.AgentSecondFilterId,a.AgentThirdFilterId, a.AgentFourthFilterId
FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Contracts].DocumentKinds a LEFT JOIN [Contracts].DocumentKinds s ON a.Id=s.Id WHERE s.Id IS NULL;

UPDATE [Contracts].DocumentKinds
SET Id = s.Id,
Guid = s.Guid,
DatabaseId = s.DatabaseId,
DbSourceId = s.DbSourceId,
Flags = s.Flags,
StateId = s.StateId,
Code = s.Code,
Memo = s.Memo,
Name = s.Name,
EntityId = s.EntityId,
KindId = s.KindId,
CorrespondenceId = S.CorrespondenceId,
UseCustomFilter = S.UseCustomFilter, 
AgentFirstFilterId=s.AgentFirstFilterId, 
AgentSecondFilterId=s.AgentSecondFilterId,
AgentThirdFilterId=s.AgentThirdFilterId, 
AgentFourthFilterId=s.AgentFourthFilterId
FROM [Contracts].DocumentKinds a INNER JOIN  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Contracts].DocumentKinds s ON a.Id=s.Id;
PRINT 'Выполнено обновление таблицы Contracts.EntityKinds...'
GO

INSERT INTO Ourp.DocumentKinds(Id, Guid, DatabaseId, DbSourceId, Flags, StateId,Code, Memo, Name, EntityId, KindId,CorrespondenceId,
            UseCustomFilter, AgentFirstFilterId, AgentSecondFilterId,AgentThirdFilterId, AgentFourthFilterId)
SELECT a.Id, a.Guid, a.DatabaseId, a.DbSourceId, a.Flags, a.StateId, a.Code,a.Memo, a.Name, a.EntityId, a.KindId,
a.CorrespondenceId, a.UseCustomFilter, a.AgentFirstFilterId, a.AgentSecondFilterId,a.AgentThirdFilterId, a.AgentFourthFilterId
  FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Ourp.DocumentKinds a LEFT JOIN Ourp.DocumentKinds s ON a.Id=s.Id WHERE s.Id IS NULL;
GO
UPDATE Ourp.DocumentKinds
SET Ourp.DocumentKinds.Id = s.Id,
Ourp.DocumentKinds.Guid = s.Guid,
Ourp.DocumentKinds.DatabaseId = s.DatabaseId,
Ourp.DocumentKinds.DbSourceId = s.DbSourceId,
Ourp.DocumentKinds.Flags = s.Flags,
Ourp.DocumentKinds.StateId = s.StateId,
Ourp.DocumentKinds.Code = s.Code,
Ourp.DocumentKinds.Memo = s.Memo,
Ourp.DocumentKinds.Name = s.Name,
Ourp.DocumentKinds.EntityId = s.EntityId,
Ourp.DocumentKinds.KindId = s.KindId,
Ourp.DocumentKinds.CorrespondenceId = S.CorrespondenceId,
Ourp.DocumentKinds.UseCustomFilter = S.UseCustomFilter, 
Ourp.DocumentKinds.AgentFirstFilterId=s.AgentFirstFilterId, 
Ourp.DocumentKinds.AgentSecondFilterId=s.AgentSecondFilterId,
Ourp.DocumentKinds.AgentThirdFilterId=s.AgentThirdFilterId, 
Ourp.DocumentKinds.AgentFourthFilterId=s.AgentFourthFilterId
FROM Ourp.DocumentKinds a INNER JOIN  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Ourp.DocumentKinds s ON a.Id=s.Id;
PRINT 'Выполнено обновление таблицы Ourp.EntityKinds...'
GO

INSERT INTO XmlData.DocumentKinds(Id, Guid, DatabaseId, DbSourceId, Flags, StateId,Code, Memo, Name, EntityId, KindId,CorrespondenceId,
            UseCustomFilter, AgentFirstFilterId, AgentSecondFilterId,AgentThirdFilterId, AgentFourthFilterId)
SELECT a.Id, a.Guid, a.DatabaseId, a.DbSourceId, a.Flags, a.StateId, a.Code,a.Memo, a.Name, a.EntityId, a.KindId,
a.CorrespondenceId, a.UseCustomFilter, a.AgentFirstFilterId, a.AgentSecondFilterId,a.AgentThirdFilterId, a.AgentFourthFilterId
  FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].XmlData.DocumentKinds a LEFT JOIN XmlData.DocumentKinds s ON a.Id=s.Id WHERE s.Id IS NULL;
GO
UPDATE XmlData.DocumentKinds
SET XmlData.DocumentKinds.Id = s.Id,
XmlData.DocumentKinds.Guid = s.Guid,
XmlData.DocumentKinds.DatabaseId = s.DatabaseId,
XmlData.DocumentKinds.DbSourceId = s.DbSourceId,
XmlData.DocumentKinds.Flags = s.Flags,
XmlData.DocumentKinds.StateId = s.StateId,
XmlData.DocumentKinds.Code = s.Code,
XmlData.DocumentKinds.Memo = s.Memo,
XmlData.DocumentKinds.Name = s.Name,
XmlData.DocumentKinds.EntityId = s.EntityId,
XmlData.DocumentKinds.KindId = s.KindId,
XmlData.DocumentKinds.CorrespondenceId = S.CorrespondenceId,
XmlData.DocumentKinds.UseCustomFilter = S.UseCustomFilter, 
XmlData.DocumentKinds.AgentFirstFilterId=s.AgentFirstFilterId, 
XmlData.DocumentKinds.AgentSecondFilterId=s.AgentSecondFilterId,
XmlData.DocumentKinds.AgentThirdFilterId=s.AgentThirdFilterId, 
XmlData.DocumentKinds.AgentFourthFilterId=s.AgentFourthFilterId
FROM XmlData.DocumentKinds a INNER JOIN  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].XmlData.DocumentKinds s ON a.Id=s.Id;
PRINT 'Выполнено обновление таблицы Ourp.EntityKinds...'
GO

INSERT INTO Price.DocumentKinds(Id, Guid, DatabaseId, DbSourceId, Flags, StateId,Code, Memo, Name, EntityId, KindId,CorrespondenceId,
            UseCustomFilter, AgentFirstFilterId, AgentSecondFilterId,AgentThirdFilterId, AgentFourthFilterId)
SELECT a.Id, a.Guid, a.DatabaseId, a.DbSourceId, a.Flags, a.StateId, a.Code,a.Memo, a.Name, a.EntityId, a.KindId,
a.CorrespondenceId, a.UseCustomFilter, a.AgentFirstFilterId, a.AgentSecondFilterId,a.AgentThirdFilterId, a.AgentFourthFilterId
  FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Price.DocumentKinds a LEFT JOIN Price.DocumentKinds s ON a.Id=s.Id WHERE s.Id IS NULL;
GO
UPDATE Price.DocumentKinds
SET Price.DocumentKinds.Id = s.Id,
Price.DocumentKinds.Guid = s.Guid,
Price.DocumentKinds.DatabaseId = s.DatabaseId,
Price.DocumentKinds.DbSourceId = s.DbSourceId,
Price.DocumentKinds.Flags = s.Flags,
Price.DocumentKinds.StateId = s.StateId,
Price.DocumentKinds.Code = s.Code,
Price.DocumentKinds.Memo = s.Memo,
Price.DocumentKinds.Name = s.Name,
Price.DocumentKinds.EntityId = s.EntityId,
Price.DocumentKinds.KindId = s.KindId,
Price.DocumentKinds.CorrespondenceId = S.CorrespondenceId,
Price.DocumentKinds.UseCustomFilter = S.UseCustomFilter, 
Price.DocumentKinds.AgentFirstFilterId=s.AgentFirstFilterId, 
Price.DocumentKinds.AgentSecondFilterId=s.AgentSecondFilterId,
Price.DocumentKinds.AgentThirdFilterId=s.AgentThirdFilterId, 
Price.DocumentKinds.AgentFourthFilterId=s.AgentFourthFilterId
FROM Price.DocumentKinds a INNER JOIN  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Price.DocumentKinds s ON a.Id=s.Id;
PRINT 'Выполнено обновление таблицы Price.DocumentKinds...'
GO

INSERT INTO Sales.DocumentKinds(Id, Guid, DatabaseId, DbSourceId, Flags, StateId,Code, Memo, Name, EntityId, KindId,CorrespondenceId,
            UseCustomFilter, AgentFirstFilterId, AgentSecondFilterId,AgentThirdFilterId, AgentFourthFilterId)
SELECT a.Id, a.Guid, a.DatabaseId, a.DbSourceId, a.Flags, a.StateId, a.Code,a.Memo, a.Name, a.EntityId, a.KindId,
a.CorrespondenceId, a.UseCustomFilter, a.AgentFirstFilterId, a.AgentSecondFilterId,a.AgentThirdFilterId, a.AgentFourthFilterId
  FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Sales.DocumentKinds a LEFT JOIN Sales.DocumentKinds s ON a.Id=s.Id WHERE s.Id IS NULL;
GO
UPDATE Sales.DocumentKinds
SET Sales.DocumentKinds.Id = s.Id,
Sales.DocumentKinds.Guid = s.Guid,
Sales.DocumentKinds.DatabaseId = s.DatabaseId,
Sales.DocumentKinds.DbSourceId = s.DbSourceId,
Sales.DocumentKinds.Flags = s.Flags,
Sales.DocumentKinds.StateId = s.StateId,
Sales.DocumentKinds.Code = s.Code,
Sales.DocumentKinds.Memo = s.Memo,
Sales.DocumentKinds.Name = s.Name,
Sales.DocumentKinds.EntityId = s.EntityId,
Sales.DocumentKinds.KindId = s.KindId,
Sales.DocumentKinds.CorrespondenceId = S.CorrespondenceId,
Sales.DocumentKinds.UseCustomFilter = S.UseCustomFilter, 
Sales.DocumentKinds.AgentFirstFilterId=s.AgentFirstFilterId, 
Sales.DocumentKinds.AgentSecondFilterId=s.AgentSecondFilterId,
Sales.DocumentKinds.AgentThirdFilterId=s.AgentThirdFilterId, 
Sales.DocumentKinds.AgentFourthFilterId=s.AgentFourthFilterId
FROM Sales.DocumentKinds a INNER JOIN  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Sales.DocumentKinds s ON a.Id=s.Id;
PRINT 'Выполнено обновление таблицы Sales.DocumentKinds...'
GO

INSERT INTO Services.DocumentKinds(Id, Guid, DatabaseId, DbSourceId, Flags, StateId,Code, Memo, Name, EntityId, KindId,CorrespondenceId,
            UseCustomFilter, AgentFirstFilterId, AgentSecondFilterId,AgentThirdFilterId, AgentFourthFilterId)
SELECT a.Id, a.Guid, a.DatabaseId, a.DbSourceId, a.Flags, a.StateId, a.Code,a.Memo, a.Name, a.EntityId, a.KindId,
a.CorrespondenceId, a.UseCustomFilter, a.AgentFirstFilterId, a.AgentSecondFilterId,a.AgentThirdFilterId, a.AgentFourthFilterId
  FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Services.DocumentKinds a LEFT JOIN Services.DocumentKinds s ON a.Id=s.Id WHERE s.Id IS NULL;
GO
UPDATE Services.DocumentKinds
SET Services.DocumentKinds.Id = s.Id,
Services.DocumentKinds.Guid = s.Guid,
Services.DocumentKinds.DatabaseId = s.DatabaseId,
Services.DocumentKinds.DbSourceId = s.DbSourceId,
Services.DocumentKinds.Flags = s.Flags,
Services.DocumentKinds.StateId = s.StateId,
Services.DocumentKinds.Code = s.Code,
Services.DocumentKinds.Memo = s.Memo,
Services.DocumentKinds.Name = s.Name,
Services.DocumentKinds.EntityId = s.EntityId,
Services.DocumentKinds.KindId = s.KindId,
Services.DocumentKinds.CorrespondenceId = S.CorrespondenceId,
Services.DocumentKinds.UseCustomFilter = S.UseCustomFilter, 
Services.DocumentKinds.AgentFirstFilterId=s.AgentFirstFilterId, 
Services.DocumentKinds.AgentSecondFilterId=s.AgentSecondFilterId,
Services.DocumentKinds.AgentThirdFilterId=s.AgentThirdFilterId, 
Services.DocumentKinds.AgentFourthFilterId=s.AgentFourthFilterId
FROM Services.DocumentKinds a INNER JOIN  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Services.DocumentKinds s ON a.Id=s.Id;
PRINT 'Выполнено обновление таблицы Services.DocumentKinds...'
GO

INSERT INTO Taxes.DocumentKinds(Id, Guid, DatabaseId, DbSourceId, Flags, StateId,Code, Memo, Name, EntityId, KindId,CorrespondenceId,
            UseCustomFilter, AgentFirstFilterId, AgentSecondFilterId,AgentThirdFilterId, AgentFourthFilterId)
SELECT a.Id, a.Guid, a.DatabaseId, a.DbSourceId, a.Flags, a.StateId, a.Code,a.Memo, a.Name, a.EntityId, a.KindId,
a.CorrespondenceId, a.UseCustomFilter, a.AgentFirstFilterId, a.AgentSecondFilterId,a.AgentThirdFilterId, a.AgentFourthFilterId
  FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Taxes.DocumentKinds a LEFT JOIN Taxes.DocumentKinds s ON a.Id=s.Id WHERE s.Id IS NULL;
GO
UPDATE Taxes.DocumentKinds
SET Taxes.DocumentKinds.Id = s.Id,
Taxes.DocumentKinds.Guid = s.Guid,
Taxes.DocumentKinds.DatabaseId = s.DatabaseId,
Taxes.DocumentKinds.DbSourceId = s.DbSourceId,
Taxes.DocumentKinds.Flags = s.Flags,
Taxes.DocumentKinds.StateId = s.StateId,
Taxes.DocumentKinds.Code = s.Code,
Taxes.DocumentKinds.Memo = s.Memo,
Taxes.DocumentKinds.Name = s.Name,
Taxes.DocumentKinds.EntityId = s.EntityId,
Taxes.DocumentKinds.KindId = s.KindId,
Taxes.DocumentKinds.CorrespondenceId = S.CorrespondenceId,
Taxes.DocumentKinds.UseCustomFilter = S.UseCustomFilter, 
Taxes.DocumentKinds.AgentFirstFilterId=s.AgentFirstFilterId, 
Taxes.DocumentKinds.AgentSecondFilterId=s.AgentSecondFilterId,
Taxes.DocumentKinds.AgentThirdFilterId=s.AgentThirdFilterId, 
Taxes.DocumentKinds.AgentFourthFilterId=s.AgentFourthFilterId
FROM Taxes.DocumentKinds a INNER JOIN  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Taxes.DocumentKinds s ON a.Id=s.Id;
PRINT 'Выполнено обновление таблицы Taxes.DocumentKinds...'
GO

INSERT INTO Core.DocumentTypeMaps(Guid, DatabaseId, DbSourceId, Flags, StateId,EntityId, KindId, Name, Method, [Schema], ProcedureName)
SELECT a.Guid, a.DatabaseId, a.DbSourceId, a.Flags, a.StateId, a.EntityId,a.KindId, a.Name, a.Method, a.[Schema], a.ProcedureName
  FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.DocumentTypeMaps a LEFT JOIN Core.DocumentTypeMaps s ON a.Guid=s.Guid WHERE s.Guid IS NULL;
GO

INSERT INTO Finance.DocumentKinds(Id, Guid, DatabaseId, DbSourceId, Flags, StateId,Code, Memo, Name, EntityId, KindId,CorrespondenceId,
            UseCustomFilter, AgentFirstFilterId, AgentSecondFilterId,AgentThirdFilterId, AgentFourthFilterId)
SELECT a.Id, a.Guid, a.DatabaseId, a.DbSourceId, a.Flags, a.StateId, a.Code,a.Memo, a.Name, a.EntityId, a.KindId,
a.CorrespondenceId, a.UseCustomFilter, a.AgentFirstFilterId, a.AgentSecondFilterId,a.AgentThirdFilterId, a.AgentFourthFilterId
  FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Finance.DocumentKinds a LEFT JOIN Finance.DocumentKinds s ON a.Id=s.Id WHERE s.Id IS NULL;
GO
UPDATE Finance.DocumentKinds
SET Finance.DocumentKinds.Id = s.Id,
Finance.DocumentKinds.Guid = s.Guid,
Finance.DocumentKinds.DatabaseId = s.DatabaseId,
Finance.DocumentKinds.DbSourceId = s.DbSourceId,
Finance.DocumentKinds.Flags = s.Flags,
Finance.DocumentKinds.StateId = s.StateId,
Finance.DocumentKinds.Code = s.Code,
Finance.DocumentKinds.Memo = s.Memo,
Finance.DocumentKinds.Name = s.Name,
Finance.DocumentKinds.EntityId = s.EntityId,
Finance.DocumentKinds.KindId = s.KindId,
Finance.DocumentKinds.CorrespondenceId = S.CorrespondenceId,
Finance.DocumentKinds.UseCustomFilter = S.UseCustomFilter, 
Finance.DocumentKinds.AgentFirstFilterId=s.AgentFirstFilterId, 
Finance.DocumentKinds.AgentSecondFilterId=s.AgentSecondFilterId,
Finance.DocumentKinds.AgentThirdFilterId=s.AgentThirdFilterId, 
Finance.DocumentKinds.AgentFourthFilterId=s.AgentFourthFilterId
FROM Finance.DocumentKinds a INNER JOIN  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Finance.DocumentKinds s ON a.Id=s.Id;
PRINT 'Выполнено обновление таблицы Finance.DocumentKinds...'
GO

UPDATE Core.DocumentTypeMaps
SET Core.DocumentTypeMaps.Guid = s.Guid,
Core.DocumentTypeMaps.DatabaseId = s.DatabaseId,
Core.DocumentTypeMaps.DbSourceId = s.DbSourceId,
Core.DocumentTypeMaps.Flags = s.Flags,
Core.DocumentTypeMaps.StateId = s.StateId,
Core.DocumentTypeMaps.EntityId = s.EntityId,
Core.DocumentTypeMaps.KindId = s.KindId,
Core.DocumentTypeMaps.Name = s.Name,
Core.DocumentTypeMaps.Method = s.Method,
Core.DocumentTypeMaps.[Schema] = s.[Schema],
Core.DocumentTypeMaps.ProcedureName = s.ProcedureName
FROM Core.DocumentTypeMaps a INNER JOIN  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.DocumentTypeMaps s ON a.Guid=s.Guid;
PRINT 'Выполнено обновление таблицы Core.DocumentTypeMaps...'
GO

MERGE Core.FlagValues AS T USING [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.FlagValues AS S ON s.Guid=t.Guid
WHEN NOT MATCHED BY TARGET
     THEN INSERT (Guid, DatabaseId, DbSourceId, Flags, StateId,ToEntityId,Code,[Name], Memo, FlagValue)
     VALUES(S.Guid, S.DatabaseId, S.DbSourceId, S.Flags, S.StateId, S.ToEntityId,S.Code,S.[Name], S.Memo, S.FlagValue)
WHEN MATCHED 
     THEN UPDATE SET 
     DatabaseId=S.DatabaseId, DbSourceId=s.DbSourceId, Flags=s.Flags, StateId=s.StateId, ToEntityId=S.ToEntityId,Code=S.Code,[Name]=S.[Name], Memo=S.Memo, FlagValue=S.FlagValue;
PRINT 'Выполнено обновление допустимых флагов...'
GO

INSERT INTO FileData.Files(Guid, DatabaseId,Flags,StateId,Name,NameFull,KindId,Code,Memo,FlagString,CodeFind, FileExtention,AllowVersion,StreamData)
SELECT a.Guid, a.DatabaseId,a.Flags,a.StateId,a.Name,a.NameFull,a.KindId,a.Code,a.Memo,a.FlagString,a.CodeFind, a.FileExtention, a.AllowVersion, a.StreamData
  from [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].FileData.Files a LEFT JOIN FileData.Files s ON a.Guid=s.Guid WHERE s.Guid IS NULL AND a.IsSystem>0 AND isnull(s.IsExcNoUpdate, 0)=0
GO

INSERT INTO FileData.Files(Guid, DatabaseId,Flags,StateId,Name,NameFull,KindId,Code,Memo,FlagString,CodeFind, FileExtention,AllowVersion,StreamData)
SELECT a.Guid, a.DatabaseId,a.Flags,a.StateId,a.Name,a.NameFull,a.KindId,a.Code,a.Memo,a.FlagString,a.CodeFind, a.FileExtention, a.AllowVersion, a.StreamData
  FROM  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].FileData.Files a LEFT JOIN FileData.Files s ON a.Guid=s.Guid
  inner JOIN  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Developer.Knowledges k ON k.FileId=a.Id 
WHERE s.Guid IS NULL AND k.IsSystem>0 AND k.FileId IS NOT null
GO

UPDATE FileData.Files
SET FileExtention = s.FileExtention,
NameFull = s.NameFull,
Name = s.Name,
Code = s.Code,
StreamData = s.StreamData,
AllowVersion = s.AllowVersion  
FROM FileData.Files a INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].FileData.Files s ON a.Guid=s.Guid
WHERE s.IsSystem>0 AND isnull(a.IsExcNoUpdate, 0)=0
GO
PRINT 'Выполнено обновление файлов...'
GO

INSERT INTO Product.Products(Guid, DatabaseId,Flags,StateId,Name,NameFull,KindId,Code,Memo,FlagString,CodeFind)
SELECT a.Guid, a.DatabaseId,a.Flags,a.StateId,a.Name,a.NameFull,a.KindId,a.Code,a.Memo,a.FlagString,a.CodeFind
  from [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Product.Products a LEFT JOIN Product.Products s ON a.Guid=s.Guid WHERE s.Guid IS NULL AND a.IsSystem>0 AND isnull(s.IsExcNoUpdate, 0)=0
GO

UPDATE Product.Products
SET DatabaseId = s.DatabaseId,
Flags = s.Flags,
StateId = s.StateId,
Name = s.Name,
NameFull = s.NameFull,
KindId = s.KindId,
Code = s.Code,
Memo = s.Memo,
FlagString = s.FlagString,
CodeFind = s.CodeFind,
Nomenclature = s.Nomenclature,
Articul = s.Articul,
Cataloque = s.Cataloque,
Barcode = s.Barcode,
[Weight] = s.[Weight],
Height = s.Height,
Width = s.Width,
Depth = s.Depth,
[Size] = s.[Size],
StoragePeriod = s.StoragePeriod
FROM Product.Products a INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Product.Products s ON a.Guid=s.Guid
WHERE s.IsSystem>0 AND isnull(a.IsExcNoUpdate, 0)=0
PRINT 'Выполнено обновление товаров...'
GO
UPDATE Contractor.Agents
SET DatabaseId = s.DatabaseId,
Flags = s.Flags,
StateId = s.StateId,
Name = s.Name,
NameFull = s.NameFull,
KindId = s.KindId,
Code = s.Code,
Memo = s.Memo,
FlagString = s.FlagString,
CodeFind = s.CodeFind,
CodeTax = s.CodeTax,
AddressLegal = s.AddressLegal,
AddressPhysical = s.AddressPhysical,
AmmountTrust = s.AmmountTrust,
TimeDelay = s.TimeDelay,
Phone = s.Phone,
MyCompanyId=S.MyCompanyId
FROM Contractor.Agents a INNER JOIN  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Contractor.Agents s ON a.Guid=s.Guid
WHERE s.IsSystem>0 AND isnull(a.IsExcNoUpdate, 0)=0
PRINT 'Выполнено обновление корреспондентов...'
GO

INSERT INTO Contractor.Agents(Guid, DatabaseId,Flags,StateId,Name,NameFull,KindId,Code,Memo,FlagString,CodeFind,CodeTax,AddressLegal,AddressPhysical,AmmountTrust,TimeDelay,Phone)
SELECT a.Guid, a.DatabaseId,a.Flags,a.StateId,a.Name,a.NameFull,a.KindId,a.Code,a.Memo,a.FlagString,a.CodeFind,a.CodeTax,a.AddressLegal,a.AddressPhysical,a.AmmountTrust,a.TimeDelay,a.Phone
  from [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Contractor.Agents a LEFT JOIN Contractor.Agents s ON a.Guid=s.Guid WHERE s.Guid IS NULL AND a.IsSystem>0 AND isnull(s.IsExcNoUpdate, 0)=0
GO

UPDATE Contractor.Companies
SET Flags = S.Flags,
 Guid=S.Guid,
 StateId = S.StateId,
 InternationalName = S.InternationalName,
 Tax = S.Tax,
 NdsPayer = S.NdsPayer,
 Okpo = S.Okpo,
 RegDate = S.RegDate,
 ActivityEconomic = S.ActivityEconomic,
 RegNumber = S.RegNumber,
 RegPensionFund = S.RegPensionFund,
 RegEmploymentService = S.RegEmploymentService,
 RegSocialInsuranceDisability = S.RegSocialInsuranceDisability,
 RegSocialInsuranceNesch = S.RegSocialInsuranceNesch,
 RegPfu = S.RegPfu,
 RegOpfg = S.RegOpfg,
 RegKoatu = S.RegKoatu,
 RegKfv = S.RegKfv,
 RegZkgng = S.RegZkgng,
 RegKved = S.RegKved
FROM Contractor.Companies a INNER JOIN  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Contractor.Companies s ON a.Guid=s.Guid
WHERE s.IsSystem>0 AND isnull(a.IsExcNoUpdate, 0)=0
PRINT 'Выполнено обновление компаний...'
GO

INSERT INTO Contractor.Companies(Id, Guid, DatabaseId, Flags, StateId,
            InternationalName, Tax, NdsPayer, Okpo, RegDate, ActivityEconomic,
            RegNumber, RegPensionFund, RegEmploymentService,
            RegSocialInsuranceDisability, RegSocialInsuranceNesch, RegPfu, RegOpfg,
            RegKoatu, RegKfv, RegZkgng, RegKved)
SELECT a2.Id, a.Guid, a.DatabaseId, a.Flags, a.StateId,
            a.InternationalName, a.Tax, a.NdsPayer, a.Okpo, a.RegDate, a.ActivityEconomic,
            a.RegNumber, a.RegPensionFund, a.RegEmploymentService,
            a.RegSocialInsuranceDisability, a.RegSocialInsuranceNesch, a.RegPfu, a.RegOpfg,
            a.RegKoatu, a.RegKfv, a.RegZkgng, a.RegKved
from [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Contractor.Companies a
     INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Contractor.Agents b ON a.Id=b.Id 
 inner JOIN Contractor.Agents a2 ON b.Guid=a2.Guid
 LEFT JOIN Contractor.Companies s ON a2.Id=s.Id 
 
WHERE s.Guid IS NULL AND a.IsSystem>0 AND isnull(a.IsExcNoUpdate, 0)=0
PRINT 'Выполнено создание данных о копманиях...'
GO

UPDATE Contractor.Banks
SET Guid = s.Guid,
 Flags = s.Flags,
 StateId = s.StateId,
 Mfo = s.Mfo
FROM Contractor.Banks a 
INNER JOIN  Contractor.Agents ag on a.Id=ag.Id
INNER JOIN  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Contractor.Agents ag2 on ag.Guid=ag2.Guid
INNER JOIN  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Contractor.Banks s ON s.Id=ag2.Id
WHERE s.IsSystem>0 AND isnull(a.IsExcNoUpdate, 0)=0
PRINT 'Выполнено обновление данных банков...'
GO

INSERT INTO Contractor.Banks(Id, Guid, DatabaseId, Flags, StateId, Mfo)
SELECT a2.Id, a.Guid, a.DatabaseId, a.Flags, a.StateId, a.Mfo
from [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Contractor.Banks a 
INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Contractor.Agents b ON a.Id=b.Id 
left JOIN Contractor.Agents a2 ON b.Guid=a2.Guid
LEFT JOIN Contractor.Banks s ON a.Guid=s.Guid 
WHERE s.Guid IS NULL and a.IsSystem>0 AND isnull(s.IsExcNoUpdate, 0)=0
PRINT 'Выполнено создание данных о банках...'
GO
  

INSERT Core.Currencies(Guid, DatabaseId, DbSourceId, Flags, StateId, Name,NameFull, KindId, Code, Memo, FlagString, TemplateId, CodeFind, IntCode)
SELECT a.Guid, a.DatabaseId, a.DbSourceId, a.Flags, a.StateId, a.Name, a.NameFull, a.KindId, a.Code, a.Memo, a.FlagString, a.TemplateId, a.CodeFind, a.IntCode
  from [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Currencies a LEFT JOIN Core.Currencies s ON a.Guid=s.Guid WHERE s.Guid IS NULL AND a.IsSystem>0 AND isnull(s.IsExcNoUpdate, 0)=0
  
UPDATE Core.Currencies
SET Core.Currencies.Guid = s.Guid,
Core.Currencies.DatabaseId = s.DatabaseId,
Core.Currencies.DbSourceId = s.DbSourceId,
Core.Currencies.Flags = s.Flags,
Core.Currencies.StateId = s.StateId,
Core.Currencies.Name = s.Name,
Core.Currencies.NameFull = s.NameFull,
Core.Currencies.KindId = s.KindId,
Core.Currencies.Code = s.Code,
Core.Currencies.Memo = s.Memo,
Core.Currencies.FlagString = s.FlagString,
Core.Currencies.TemplateId = s.TemplateId,
Core.Currencies.CodeFind = s.CodeFind,
Core.Currencies.IntCode = s.IntCode
FROM Core.Currencies a INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Currencies s ON a.Guid=s.Guid
WHERE s.IsSystem>0 AND a.IsExcNoUpdate=0; 
PRINT 'Выполнено обновление валют...'
GO

INSERT Core.Units(Guid, DatabaseId, DbSourceId, Flags, StateId, Name, NameFull,
       KindId, Code, Memo, FlagString, TemplateId, CodeFind, CodeInternational)
SELECT a.Guid, a.DatabaseId, a.DbSourceId, a.Flags, a.StateId, a.Name, a.NameFull,
       a.KindId, a.Code, a.Memo, a.FlagString, a.TemplateId, a.CodeFind,a.CodeInternational
  FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Units a LEFT JOIN Core.Units s ON a.Guid=s.Guid WHERE s.Guid IS NULL AND a.IsSystem>0 AND isnull(s.IsExcNoUpdate, 0)=0;

UPDATE Core.Units
SET Core.Units.DatabaseId = s.DatabaseId,
Core.Units.DbSourceId = s.DbSourceId,
Core.Units.Flags = s.Flags,
Core.Units.StateId = s.StateId,
Core.Units.Name = s.Name,
Core.Units.NameFull = s.NameFull,
Core.Units.KindId = s.KindId,
Core.Units.Code = s.Code,
Core.Units.Memo = s.Memo,
Core.Units.FlagString = s.FlagString,
Core.Units.TemplateId = s.TemplateId,
Core.Units.CodeFind = s.CodeFind,
Core.Units.CodeInternational = s.CodeInternational
FROM Core.Units a INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Units s ON a.Guid=s.Guid
WHERE s.IsSystem>0 AND isnull(a.IsExcNoUpdate, 0)=0;
PRINT 'Выполнено обновление единиц измерения...'
GO
INSERT Core.ResourceImages(Guid, DatabaseId, DbSourceId, Flags, StateId, KindId,Code, Memo, [Value])
SELECT a.Guid, a.DatabaseId, a.DbSourceId, a.Flags, a.StateId, a.KindId, a.Code, a.Memo, a.[Value]
FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.ResourceImages a LEFT JOIN Core.ResourceImages s ON a.Guid=s.Guid WHERE s.Guid IS NULL AND a.IsSystem>0 AND isnull(s.IsExcNoUpdate, 0)=0;

UPDATE Core.ResourceImages
SET Core.ResourceImages.Guid = s.Guid,
Core.ResourceImages.DatabaseId = s.DatabaseId,
Core.ResourceImages.DbSourceId = s.DbSourceId,
Core.ResourceImages.Flags = s.Flags,
Core.ResourceImages.StateId = s.StateId,
Core.ResourceImages.KindId = s.KindId,
Core.ResourceImages.Code = s.Code,
Core.ResourceImages.Memo = s.Memo,
Core.ResourceImages.[Value] = s.[Value]
FROM Core.ResourceImages a INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.ResourceImages s ON a.Guid=s.Guid WHERE s.IsSystem>0 AND a.IsExcNoUpdate=0; 
PRINT 'Выполнено обновление системных изображений...'
GO

INSERT Core.ResourceStrings(Guid, DatabaseId, DbSourceId, Flags, StateId, KindId,Code, Memo, [Value], CultureId)
SELECT a.Guid, a.DatabaseId, a.DbSourceId, a.Flags, a.StateId, a.KindId, a.Code, a.Memo, a.[Value], a.CultureId
  FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.ResourceStrings a LEFT JOIN Core.ResourceStrings s ON a.Guid=s.Guid WHERE s.Guid IS NULL AND a.IsSystem>0 AND isnull(s.IsExcNoUpdate, 0)=0; 

UPDATE Core.ResourceStrings
SET Core.ResourceStrings.Guid = s.Guid,
Core.ResourceStrings.DatabaseId = s.DatabaseId,
Core.ResourceStrings.DbSourceId = s.DbSourceId,
Core.ResourceStrings.Flags = s.Flags,
Core.ResourceStrings.StateId = s.StateId,
Core.ResourceStrings.KindId = s.KindId,
Core.ResourceStrings.Code = s.Code,
Core.ResourceStrings.Memo = s.Memo,
Core.ResourceStrings.[Value] = s.[Value],
Core.ResourceStrings.CultureId = s.CultureId
FROM Core.ResourceStrings a INNER JOIN  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.ResourceStrings s ON a.Guid=s.Guid WHERE a.IsSystem>0 AND isnull(s.IsExcNoUpdate, 0)=0;
PRINT 'Выполнено обновление строковых ресурсов...'
GO

MERGE Analitic.Analitics AS T USING [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Analitic.Analitics AS S ON s.Guid=t.Guid
WHEN NOT MATCHED BY TARGET AND S.IsSystem>0
     THEN INSERT (Guid, DatabaseId, DbSourceId, Flags, StateId, Name, NameFull, KindId, Code,Memo, FlagString, CodeFind, MyCompanyId)
     VALUES(S.Guid, S.DatabaseId, S.DbSourceId, S.Flags, S.StateId, S.Name, S.NameFull, S.KindId, S.Code,S.Memo, S.FlagString, S.CodeFind, S.MyCompanyId)
WHEN MATCHED AND S.IsSystem>0 AND isnull(T.IsExcNoUpdate, 0)=0
     THEN UPDATE SET 
     DatabaseId=S.DatabaseId, DbSourceId=s.DbSourceId, Flags=s.Flags, StateId=s.StateId, NAME=s.Name, NameFull=s.NameFull, KindId=s.KindId, Code=s.Code,Memo=s.Memo, FlagString=S.FlagString, CodeFind=S.CodeFind, MyCompanyId=S.MyCompanyId;
PRINT 'Выполнено обновление аналитики...'
GO

MERGE Core.Folders AS T USING [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Folders AS S ON s.Guid=t.Guid
WHEN NOT MATCHED BY TARGET AND S.IsSystem>0
     THEN INSERT (Guid, DatabaseId, DbSourceId, Flags, StateId, Name, NameFull, KindId, Code,Memo, FlagString, CodeFind)
     VALUES(S.Guid, S.DatabaseId, S.DbSourceId, S.Flags, S.StateId, S.Name, S.NameFull, S.KindId, S.Code,S.Memo, S.FlagString, S.CodeFind)
WHEN MATCHED AND S.IsSystem>0 AND isnull(T.IsExcNoUpdate, 0)=0
     THEN UPDATE SET 
     DatabaseId=S.DatabaseId, DbSourceId=s.DbSourceId, Flags=s.Flags, StateId=s.StateId, NAME=s.Name, NameFull=s.NameFull, KindId=s.KindId, Code=s.Code,Memo=s.Memo, FlagString=S.FlagString, CodeFind=S.CodeFind;
PRINT 'Выполнено обновление папок...'
GO


MERGE Core.WebServices AS T USING [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.WebServices AS S ON s.Guid=t.Guid
WHEN NOT MATCHED BY TARGET AND S.IsSystem>0
     THEN INSERT (Guid, DatabaseId, DbSourceId, Flags, StateId, Name, NameFull, KindId, Code,Memo, FlagString, CodeFind, UrlAddress, [Password], [Uid], [AuthKind] )
     VALUES(S.Guid, S.DatabaseId, S.DbSourceId, S.Flags, S.StateId, S.Name, S.NameFull, S.KindId, S.Code,S.Memo, S.FlagString, S.CodeFind, S.UrlAddress, S.[Password], S.[Uid], S.[AuthKind])
WHEN MATCHED AND S.IsSystem>0 AND isnull(T.IsExcNoUpdate, 0)=0
     THEN UPDATE SET 
     DatabaseId=S.DatabaseId, DbSourceId=s.DbSourceId, Flags=s.Flags, StateId=s.StateId, NAME=s.Name, NameFull=s.NameFull, KindId=s.KindId, Code=s.Code,Memo=s.Memo, FlagString=S.FlagString, CodeFind=S.CodeFind, UrlAddress=S.UrlAddress, [Password]=S.[Password], [Uid]=S.[Uid], [AuthKind]=S.[AuthKind];
PRINT 'Выполнено обновление веб служб...'
GO

MERGE BookKeep.Accounts AS T USING [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].BookKeep.Accounts AS S ON s.Guid=t.Guid
WHEN NOT MATCHED BY TARGET AND S.IsSystem>0
     THEN INSERT (Guid, DatabaseId, DbSourceId, Flags, StateId, Name, NameFull, KindId, Code,Memo, FlagString, CodeFind, Turn, TurnKind )
     VALUES(S.Guid, S.DatabaseId, S.DbSourceId, S.Flags, S.StateId, S.Name, S.NameFull, S.KindId, S.Code,S.Memo, S.FlagString, S.CodeFind, S.Turn, S.TurnKind)
WHEN MATCHED AND S.IsSystem>0 AND isnull(T.IsExcNoUpdate, 0)=0
     THEN UPDATE SET 
     DatabaseId=S.DatabaseId, DbSourceId=s.DbSourceId, Flags=s.Flags, StateId=s.StateId, NAME=s.Name, NameFull=s.NameFull, KindId=s.KindId, Code=s.Code,Memo=s.Memo, FlagString=S.FlagString, CodeFind=S.CodeFind, Turn=S.Turn, TurnKind=S.TurnKind;
PRINT 'Выполнено обновление бухгалтерских счетов...'
GO

MERGE Hierarchy.Hierarchies AS T USING [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Hierarchy.Hierarchies AS S ON s.Guid=t.Guid AND S.ParentId IS NULL
WHEN NOT MATCHED BY TARGET AND S.ParentId IS NULL
     THEN INSERT (Guid, DatabaseId, DbSourceId, Flags, StateId, Name, NameFull, KindId, Code,Memo, FlagString, CodeFind,DbEntityId)
     VALUES(S.Guid, S.DatabaseId, S.DbSourceId, S.Flags, S.StateId, S.Name, S.NameFull, S.KindId, S.Code,S.Memo, S.FlagString, S.CodeFind, S.DbEntityId)
WHEN MATCHED AND S.IsSystem>0 AND S.ParentId IS NULL and isnull(T.IsExcNoUpdate, 0)=0
     THEN UPDATE SET 
     DatabaseId=S.DatabaseId, DbSourceId=s.DbSourceId, Flags=s.Flags, StateId=s.StateId, NAME=s.Name, NameFull=s.NameFull, KindId=s.KindId, Code=s.Code,Memo=s.Memo, FlagString=S.FlagString, CodeFind=S.CodeFind, DbEntityId=S.DbEntityId;
PRINT 'Выполнено обновление корневых иерархии...'
GO

SET NOCOUNT ON
;WITH e AS
    (
      SELECT Id, ParentId, -1 AS Lv FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Hierarchy.Hierarchies WHERE ParentId IS NULL AND IsSystem>0
      UNION ALL
      SELECT et.Id, et.ParentId, e.Lv + 1 AS Lv FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Hierarchy.Hierarchies et inner JOIN e ON et.ParentId= e.Id AND et.IsSystem>0
    )
    SELECT e.lv, h.*, h2.Guid AS ParentGuid, h3.Guid AS TemplateGuid, h4.Guid AS RootGuid INTO #tmpHierarchy
      FROM e INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Hierarchy.Hierarchies h ON e.Id=h.Id 
             LEFT JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Hierarchy.Hierarchies h2 ON h.ParentId=h2.Id
             LEFT JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Hierarchy.Hierarchies h3 ON h.TemplateId=h3.Id
             LEFT JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Hierarchy.Hierarchies h4 ON h.RootId=h4.Id
    ORDER BY e.lv
    
DECLARE @guid UNIQUEIDENTIFIER;
DECLARE @ParentGuid UNIQUEIDENTIFIER;
DECLARE @TemplateGuid UNIQUEIDENTIFIER;
DECLARE @RootGuid UNIQUEIDENTIFIER;
DECLARE @rc INT;
DECLARE @parentId INT;
DECLARE @TemplateId INT;
DECLARE @RootId INT;

DECLARE hierarchyCursor CURSOR FOR SELECT Guid, ParentGuid, TemplateGuid, RootGuid FROM #tmpHierarchy ORDER BY lv;

OPEN hierarchyCursor;

FETCH NEXT FROM hierarchyCursor INTO @guid, @ParentGuid, @TemplateGuid, @RootGuid;

WHILE @@FETCH_STATUS = 0
BEGIN
    SELECT @rc = [Hierarchy].[FindIdByGuid](@guid)
    SELECT @parentId = [Hierarchy].[FindIdByGuid](@ParentGuid)
    SELECT @TemplateId = [Hierarchy].[FindIdByGuid](@TemplateGuid)
    SELECT @RootId = [Hierarchy].[FindIdByGuid](@RootGuid)
    IF(@rc is NOT NULL)
    BEGIN
    	UPDATE Hierarchy.Hierarchies
    	SET DbSourceId = b.Id, Code = b.Code, Name = b.Name, NameFull = b.NameFull, CodeFind = b.CodeFind, OrderNo = b.OrderNo, Flags = b.Flags 
    	FROM Hierarchy.Hierarchies h INNER JOIN #tmpHierarchy b ON h.Guid=b.Guid where h.Guid=@guid AND isnull(h.IsExcNoUpdate, 0)=0
    END
    ELSE
    BEGIN
		INSERT INTO Hierarchy.Hierarchies([Guid], [DatabaseId],[DbSourceId],[UserName],[DateModified],[Flags],[StateId],[Name], NameFull,[KindId],[Code], CodeFind,[Memo],[FlagString],[TemplateId],[DbEntityId],[ParentId],[RootId],[ContentFlags],[HasContents],OrderNo)
		SELECT [Guid], [DatabaseId], Id AS [DbSourceId],suser_sname(),GETDATE(),[Flags],[StateId],[Name], NameFull,[KindId],[Code], CodeFind,[Memo],[FlagString],@TemplateId,[DbEntityId],@parentId,@RootId,[ContentFlags],[HasContents],OrderNo FROM #tmpHierarchy WHERE Guid=@guid
    END
    	

    FETCH NEXT FROM hierarchyCursor INTO @guid, @ParentGuid, @TemplateGuid, @RootGuid;
END
CLOSE hierarchyCursor;
DEALLOCATE hierarchyCursor;

DROP TABLE #tmpHierarchy

GO

MERGE Core.ChainKinds AS T USING [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.ChainKinds AS S ON s.Id=t.Id
WHEN NOT MATCHED BY TARGET
     THEN INSERT (Id,Guid, DatabaseId, DbSourceId, Flags, StateId,FromEntityId,ToEntityId,Code,Memo,[Name],NameRight,EntityContent)
     VALUES(S.Id,S.Guid, S.DatabaseId, S.DbSourceId, S.Flags, S.StateId, S.FromEntityId,S.ToEntityId,S.Code,S.Memo,S.[Name],S.NameRight,S.EntityContent)
WHEN MATCHED AND S.IsSystem>0 AND isnull(T.IsExcNoUpdate, 0)=0
     THEN UPDATE SET 
     DatabaseId=S.DatabaseId, DbSourceId=s.DbSourceId, Flags=s.Flags, StateId=s.StateId, FromEntityId=S.FromEntityId,ToEntityId=S.ToEntityId,Code=S.Code,Memo=S.Memo,[Name]=S.[Name],NameRight=S.NameRight,EntityContent=S.EntityContent;
PRINT 'Выполнено обновление видов связи...'
GO

MERGE Core.ChainKindContentTypes AS T USING [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.ChainKindContentTypes AS S ON s.Guid=t.Guid
WHEN NOT MATCHED BY TARGET
     THEN INSERT (Guid, DatabaseId, DbSourceId, Flags, StateId,ElementId,EntityKindId, EntityKindIdFrom)
     VALUES(S.Guid, S.DatabaseId, S.DbSourceId, S.Flags, S.StateId, S.ElementId,S.EntityKindId, EntityKindIdFrom)
WHEN MATCHED 
     THEN UPDATE SET 
     DatabaseId=S.DatabaseId, DbSourceId=s.DbSourceId, Flags=S.Flags, StateId=S.StateId, ElementId=S.ElementId,EntityKindId=S.EntityKindId, EntityKindIdFrom=S.EntityKindIdFrom;
PRINT 'Выполнено обновление типов связи...'
GO

MERGE Core.Branches AS T USING [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Branches AS S ON s.Guid=t.Guid
WHEN NOT MATCHED BY TARGET AND S.IsSystem>0
     THEN INSERT (Guid, DatabaseId, DbSourceId, Flags, StateId, Name, NameFull, KindId, Code,Memo, FlagString, CodeFind, OrderNo, ServerName, DbName, DbCode, Ip,Pws,[Uid],Domain,[Authentication],DateStart,DateEnd)
     VALUES(S.Guid, S.DatabaseId, S.Id, S.Flags, S.StateId, S.Name, S.NameFull, S.KindId, S.Code,S.Memo, S.FlagString, S.CodeFind, S.OrderNo, S.ServerName, S.DbName, S.DbCode, S.Ip,S.Pws,S.[Uid],S.Domain,S.[Authentication],S.DateStart,S.DateEnd)
WHEN MATCHED AND S.IsSystem>0 AND isnull(T.IsExcNoUpdate, 0)=0
     THEN UPDATE SET 
     DatabaseId=S.DatabaseId, DbSourceId=s.Id, Flags=s.Flags, StateId=s.StateId, NAME=s.Name, NameFull=s.NameFull, KindId=s.KindId, Code=s.Code,Memo=s.Memo, FlagString=S.FlagString, CodeFind=S.CodeFind, OrderNo=S.OrderNo, ServerName=S.ServerName, DbName=S.DbName, DbCode=S.DbCode, Ip=S.Ip,Pws=S.Pws,[Uid]=S.[Uid],Domain=S.Domain,[Authentication]=S.[Authentication],DateStart=S.DateStart,DateEnd=S.DateEnd;
PRINT 'Выполнено обновление владельцев...'
GO

MERGE Core.CodeNames AS T USING [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.CodeNames AS S ON s.Guid=t.Guid
WHEN NOT MATCHED BY TARGET AND S.IsSystem>0
     THEN INSERT (Guid, DatabaseId, DbSourceId, Flags, StateId, Name, NameFull, KindId, Code,Memo, FlagString, CodeFind, App, ToEntityId, DocTypeId)
     VALUES(S.Guid, S.DatabaseId, S.Id, S.Flags, S.StateId, S.Name, S.NameFull, S.KindId, S.Code,S.Memo, S.FlagString, S.CodeFind, S.App, S.ToEntityId, S.DocTypeId)
WHEN MATCHED AND S.IsSystem>0 AND isnull(T.IsExcNoUpdate, 0)=0
     THEN UPDATE SET 
     DatabaseId=S.DatabaseId, DbSourceId=s.Id, Flags=s.Flags, StateId=s.StateId, [Name]=s.[Name], NameFull=s.NameFull, KindId=s.KindId, Code=s.Code,Memo=s.Memo, FlagString=S.FlagString, CodeFind=S.CodeFind, App=S.App, ToEntityId=S.ToEntityId, DocTypeId=S.DocTypeId;
PRINT 'Выполнено обновление наименований кодов...'
GO

MERGE Core.CodeNameEntityKinds AS T USING 
(
SELECT b.Id, b.Guid, b.DatabaseId, b.DbSourceId, b.Flags, b.StateId, c.Id AS ElementId,b.EntityKindId 
       FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.CodeNames a
INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.CodeNameEntityKinds b ON a.Id=b.ElementId
left JOIN Core.CodeNames c ON a.Guid=c.Guid
LEFT JOIN Core.CodeNameEntityKinds d ON d.Guid=b.Guid
) AS S ON (s.Guid=t.Guid) OR (s.ElementId=t.ElementId AND s.EntityKindId=t.EntityKindId)
WHEN NOT MATCHED BY TARGET
     THEN INSERT (Guid, DatabaseId, DbSourceId, Flags, StateId,ElementId,EntityKindId)
     VALUES(S.Guid, S.DatabaseId, S.DbSourceId, S.Flags, S.StateId, S.ElementId,S.EntityKindId)
WHEN MATCHED 
     THEN UPDATE SET 
     Guid = s.Guid, DatabaseId=S.DatabaseId, DbSourceId=s.DbSourceId, Flags=S.Flags, StateId=S.StateId, ElementId=S.ElementId,EntityKindId=S.EntityKindId;
PRINT 'Выполнено обновление типов наименований кодов...'
GO


MERGE Core.Libraries AS T USING [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Libraries AS S ON s.Guid=t.Guid
WHEN NOT MATCHED BY TARGET
     THEN INSERT (Guid, DatabaseId, DbSourceId, Flags, StateId, Name, NameFull, KindId, Code,Memo, FlagString, CodeFind, [Url], HelpUrl)
     VALUES(S.Guid, S.DatabaseId, S.Id, S.Flags, S.StateId, S.Name, S.NameFull, S.KindId, S.Code,S.Memo, S.FlagString, S.CodeFind, S.[Url], S.HelpUrl)
WHEN MATCHED AND S.IsSystem>0 AND isnull(T.IsExcNoUpdate, 0)=0
     THEN UPDATE SET 
     DatabaseId=S.DatabaseId, DbSourceId=s.Id, Flags=s.Flags, StateId=s.StateId, NAME=s.Name, NameFull=s.NameFull, KindId=s.KindId, Code=s.Code,Memo=s.Memo, FlagString=S.FlagString, CodeFind=S.CodeFind, [Url]=S.[Url], T.HelpUrl=S.HelpUrl, T.MyCompanyId = S.MyCompanyId;
PRINT 'Выполнено обновление библиотек первый этап...'
GO

MERGE Core.LibraryContents AS T USING
(
	SELECT a.Id, a.Guid, a.DatabaseId, a.DbSourceId, a.Flags, a.StateId, a.TypeName, b.Id AS LibraryId, a.KindCode, a.IsGeneric, a.FullTypeName, b.IsSystem, b.IsExcNoUpdate 
	FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.LibraryContents a
	INNER JOIN Core.Libraries b ON a.LibraryId = b.DbSourceId WHERE b.IsSystem>0
) AS S ON s.Guid=t.Guid
WHEN NOT MATCHED BY TARGET
     THEN INSERT (Guid, DatabaseId, DbSourceId, Flags, StateId, TypeName, LibraryId, KindCode, IsGeneric, FullTypeName)
     VALUES(S.Guid, S.DatabaseId, S.Id, S.Flags, S.StateId, S.TypeName, S.LibraryId, S.KindCode, S.IsGeneric, S.FullTypeName)
WHEN MATCHED AND S.IsSystem>0 AND isnull(S.IsExcNoUpdate, 0)=0
     THEN UPDATE SET 
     DatabaseId=S.DatabaseId, DbSourceId=s.Id, Flags=s.Flags, StateId=s.StateId, TypeName=S.TypeName, LibraryId=S.LibraryId, KindCode=S.KindCode, IsGeneric=S.IsGeneric, FullTypeName=S.FullTypeName;
PRINT 'Выполнено обновление состава библиотек...'
GO
update Core.Libraries
set LibraryTypeId=c2.Id
from 
[<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Libraries a 
inner join [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.LibraryContents c on c.Id=a.LibraryTypeId
inner join Core.Libraries b on a.Guid=b.Guid
inner join Core.LibraryContents c2 on c.Guid=c2.Guid
where a.LibraryTypeId is not null and b.IsSystem>0 
and b.KindValue=18 

PRINT 'Выполнено обновление библиотек второй этап...'
GO

UPDATE Core.Libraries
SET 
AssemblyId = fs.Id,
AssemblySourceId = fs2.Id
FROM  
Core.Libraries b INNER join
[<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Libraries a ON a.Guid=b.Guid
INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].FileData.Files f ON a.AssemblyId = f.Id
INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].FileData.Files f2 ON a.AssemblySourceId = f2.Id
INNER JOIN FileData.Files fs ON fs.Guid = f.Guid
INNER JOIN FileData.Files fs2 ON fs2.Guid = f2.Guid
WHERE b.IsSystem>0 AND isnull(b.IsExcNoUpdate, 0)=0
AND isnull(b.AssemblyId, 0)<> fs.Id
AND isnull(b.AssemblySourceId, 0)<> fs2.Id

PRINT 'Выполнено обновление библиотек третий этап...'
GO

MERGE Core.LibraryChains AS T USING
(
	SELECT sc.Guid, sc.DatabaseId, sc.Flags, sc.StateId, dl.Id AS LeftId, dl2.Id AS RightId, sc.Kind, sc.OrderNo, sc.Code, sc.Memo FROM 
		[<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.LibraryChains sc
		INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Libraries sl ON sc.LeftId=sl.Id
		INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Libraries sl2 ON sc.RightId=sl2.Id
		INNER JOIN Core.Libraries dl ON sl.Guid=dl.Guid
		INNER JOIN Core.Libraries dl2 ON sl2.Guid=dl2.Guid 
		WHERE sc.Kind=8
) AS S ON s.Guid=t.Guid OR (s.LeftId=t.LeftId AND s.RightId=t.RightId AND s.Kind=t.Kind)
WHEN NOT MATCHED BY TARGET
     THEN INSERT (Guid, DatabaseId, Flags, StateId, LeftId, RightId, Kind, OrderNo, Code, Memo)
     VALUES(S.Guid, S.DatabaseId, S.Flags, S.StateId, S.LeftId, S.RightId, S.Kind, S.OrderNo, S.Code, S.Memo)
WHEN MATCHED 
     THEN UPDATE SET 
     DatabaseId=S.DatabaseId, Flags=s.Flags, StateId=s.StateId, LeftId =S.LeftId, RightId=S.RightId, Kind=S.Kind, OrderNo=S.OrderNo, Code=S.Code, Memo=S.Memo;

MERGE Core.RuleSets AS T USING [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.RuleSets AS S ON s.Guid=t.Guid
WHEN NOT MATCHED BY TARGET AND S.IsSystem>0
     THEN INSERT (Guid, DatabaseId, DbSourceId, Flags, StateId, Name, NameFull, KindId, Code,Memo, FlagString, CodeFind, Value)
     VALUES(S.Guid, S.DatabaseId, S.Id, S.Flags, S.StateId, S.Name, S.NameFull, S.KindId, S.Code,S.Memo, S.FlagString, S.CodeFind, S.Value)
WHEN MATCHED AND S.IsSystem>0 AND isnull(T.IsExcNoUpdate, 0)=0
     THEN UPDATE SET 
     DatabaseId=S.DatabaseId, DbSourceId=s.Id, Flags=s.Flags, StateId=s.StateId, NAME=s.Name, NameFull=s.NameFull, KindId=s.KindId, Code=s.Code,Memo=s.Memo, FlagString=S.FlagString, CodeFind=S.CodeFind, VALUE=S.Value;
PRINT 'Выполнено обновление правил первый этап...'
GO


MERGE Hierarchy.HierarchyContents AS T USING
(
SELECT c.Id, c.Guid, c.DatabaseId, c.Flags, c.StateId, h2.Id as HierarchyId, d.Id AS ElementId, 15 as ToDbEntityId, c.SortOrder  
  FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Hierarchy.Hierarchies h
INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Hierarchy.HierarchyContents c ON h.Id=c.HierarchyId
INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Libraries l ON c.ElementId = l.Id
INNER JOIN Hierarchy.Hierarchies h2 ON h.Guid=h2.Guid
INNER JOIN Core.Libraries d ON d.Guid=l.Guid
WHERE h.DbEntityId=15 AND h.IsSystem>0 AND c.ToDbEntityId=15 and l.IsSystem>0	
) AS S ON s.Guid=t.Guid
WHEN NOT MATCHED BY TARGET
     THEN INSERT (Guid, DatabaseId, DbSourceId, Flags, StateId, HierarchyId, ElementId, ToDbEntityId, SortOrder)
     VALUES(S.Guid, S.DatabaseId, S.Id, S.Flags, S.StateId, S.HierarchyId, S.ElementId, S.ToDbEntityId, S.SortOrder)
WHEN MATCHED --AND S.IsSystem>0 AND isnull(S.IsExcNoUpdate, 0)=0
     THEN UPDATE SET 
     DatabaseId=S.DatabaseId, DbSourceId=s.Id, Flags=s.Flags, StateId=s.StateId, HierarchyId=S.HierarchyId, ElementId=S.ElementId, ToDbEntityId=S.ToDbEntityId, SortOrder=S.SortOrder;
PRINT 'Выполнено обновление состава библиотек...'
GO

MERGE Core.CustomViewLists AS T USING [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.CustomViewLists AS S ON s.Guid=t.Guid
WHEN NOT MATCHED BY TARGET
     THEN INSERT (Guid, DatabaseId, DbSourceId, Flags, StateId, Name, NameFull, KindId, Code,Memo, FlagString, CodeFind,EntityId, IsCollectionBased, SystemName, SystemNameRefresh, GroupPanelVisible, Options)
     VALUES(S.Guid, S.DatabaseId, S.DbSourceId, S.Flags, S.StateId, S.Name, S.NameFull, S.KindId, S.Code,S.Memo, S.FlagString, S.CodeFind,S.EntityId, S.IsCollectionBased, S.SystemName, S.SystemNameRefresh, S.GroupPanelVisible, S.Options)
WHEN MATCHED AND S.IsSystem>0 AND isnull(T.IsExcNoUpdate, 0)=0
     THEN UPDATE SET 
     DatabaseId=S.DatabaseId, DbSourceId=s.DbSourceId, Flags=s.Flags, StateId=s.StateId, NAME=s.Name, NameFull=s.NameFull, KindId=s.KindId, Code=s.Code,Memo=s.Memo, FlagString=S.FlagString, CodeFind=S.CodeFind,EntityId=S.EntityId, IsCollectionBased=S.IsCollectionBased, SystemName=S.SystemName, SystemNameRefresh=S.SystemNameRefresh, GroupPanelVisible=S.GroupPanelVisible, [Options]=S.Options;
PRINT 'Выполнено обновление списков...'
GO


MERGE Core.CustomViewColumns AS T USING 
(
SELECT b.Id, b.Guid, b.DatabaseId, b.Flags, b.StateId, b.Name, b.NameFull,
       b.KindId, b.Code, b.Memo, b.FlagString, b.CodeFind, c.Id AS CustomViewListId,
       b.DataProperty, b.DataType, b.[With], b.Visible, b.[Format], b.OrderNo,
       b.Frozen, b.AutoSizeMode, b.DisplayHeader, b.Alignment, b.GroupIndex,
       b.Editable, b.Formula
       FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.CustomViewLists a
INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.CustomViewColumns b ON a.Id=b.CustomViewListId
left JOIN Core.CustomViewLists c ON a.Guid=c.Guid
LEFT JOIN Core.CustomViewColumns d ON d.Guid=b.Guid
WHERE a.IsSystem>0 AND b.IsSystem>0
) AS S ON s.Guid=t.Guid
WHEN NOT MATCHED BY TARGET
     THEN INSERT (Guid, DatabaseId, DbSourceId, Flags, StateId, Name, NameFull, KindId, Code,
       Memo, FlagString, CodeFind, CustomViewListId, DataProperty, DataType,
       [With], Visible, [Format], OrderNo, Frozen, AutoSizeMode, DisplayHeader,
       Alignment, GroupIndex, Editable, Formula)
     VALUES(S.Guid, S.DatabaseId, S.Id, S.Flags, S.StateId, S.Name, S.NameFull, S.KindId, S.Code,
       S.Memo, S.FlagString, S.CodeFind, S.CustomViewListId, S.DataProperty, S.DataType,
       S.[With], S.Visible, S.[Format], S.OrderNo, S.Frozen, S.AutoSizeMode, S.DisplayHeader,
       S.Alignment, S.GroupIndex, S.Editable, S.Formula)
WHEN MATCHED AND isnull(T.IsExcNoUpdate, 0)=0
     THEN UPDATE SET 
     Guid=S.Guid, DatabaseId=S.DatabaseId, DbSourceId=S.Id, Flags=S.Flags, StateId=S.StateId, NAME=S.Name, NameFull=S.NameFull, KindId=S.KindId, Code=S.Code,
       Memo=S.Memo, FlagString=S.FlagString, CodeFind=S.CodeFind, CustomViewListId=S.CustomViewListId, DataProperty=S.DataProperty, DataType=S.DataType,
       [With]=S.[With], Visible=S.Visible, [Format]=S.[Format], OrderNo=S.OrderNo, Frozen=S.Frozen, AutoSizeMode=S.AutoSizeMode, DisplayHeader=S.DisplayHeader,
       Alignment=S.Alignment, GroupIndex=S.GroupIndex, Editable = S.Editable, Formula=S.Formula;
PRINT 'Выполнено обновление колонок...'
GO

MERGE Territory.Countries AS T USING [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Territory.Countries AS S ON s.Guid=t.Guid
WHEN NOT MATCHED BY TARGET
     THEN INSERT (Guid, DatabaseId, DbSourceId, Flags, StateId, Name, NameFull, KindId, Code,Memo, FlagString, CodeFind, X, Y, Iso, Iso3, IsoNum, Fips, Stanag, Iana, Continent)
     VALUES(S.Guid, S.DatabaseId, S.DbSourceId, S.Flags, S.StateId, S.Name, S.NameFull, S.KindId, S.Code,S.Memo, S.FlagString, S.CodeFind,S.X, S.Y, S.Iso, S.Iso3, S.IsoNum, S.Fips, S.Stanag, S.Iana, S.Continent)
WHEN MATCHED AND S.IsSystem>0 AND isnull(T.IsExcNoUpdate, 0)=0
     THEN UPDATE SET 
     DatabaseId=S.DatabaseId, DbSourceId=s.DbSourceId, Flags=s.Flags, StateId=s.StateId, NAME=s.Name, NameFull=s.NameFull, KindId=s.KindId, Code=s.Code,Memo=s.Memo, FlagString=S.FlagString, CodeFind=S.CodeFind, X=S.X, Y=S.Y, Iso=S.Iso, Iso3=S.Iso3, IsoNum=S.IsoNum, Fips=S.Fips, Stanag=S.Stanag, Iana=S.Iana, Continent=S.Continent;
PRINT 'Выполнено обновление стран...'
GO

MERGE Contractor.TimePeriods AS T USING [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Contractor.TimePeriods AS S ON s.Guid=t.Guid
WHEN NOT MATCHED BY TARGET AND S.IsSystem>0
     THEN INSERT (Guid, DatabaseId, DbSourceId, Flags, StateId, Name, NameFull, KindId, Code,Memo, FlagString, CodeFind, MondayW,
       TuesdayW, WednesdayW, ThursdayW, FridayW, SaturdayW, SundayW,
       MondaySH, MondaySM, MondayEH, MondayEM, TuesdaySH, TuesdaySM,
       TuesdayEH, TuesdayEM, WednesdaySH, WednesdaySM, WednesdayEM,
       WednesdayEH, ThursdaySH, ThursdaySM, ThursdayEM, ThursdayEH,
       FridaySH, FridaySM, FridayEH, FridayEM, SaturdaySH, SaturdaySM,
       SaturdayEH, SaturdayEM, SundaySH, SundayEH, SundaySM, SundayEM)
     VALUES(S.Guid, S.DatabaseId, S.DbSourceId, S.Flags, S.StateId, S.Name, S.NameFull, S.KindId, S.Code,S.Memo, S.FlagString, S.CodeFind, S.MondayW,
       S.TuesdayW, S.WednesdayW, S.ThursdayW, S.FridayW, S.SaturdayW, S.SundayW,
       S.MondaySH, S.MondaySM, S.MondayEH, S.MondayEM, S.TuesdaySH, S.TuesdaySM,
       S.TuesdayEH, S.TuesdayEM, S.WednesdaySH, S.WednesdaySM, S.WednesdayEM,
       S.WednesdayEH, S.ThursdaySH, S.ThursdaySM, S.ThursdayEM, S.ThursdayEH,
       S.FridaySH, S.FridaySM, S.FridayEH, S.FridayEM, S.SaturdaySH, S.SaturdaySM,
       S.SaturdayEH, S.SaturdayEM, S.SundaySH, S.SundayEH, S.SundaySM, S.SundayEM)
WHEN MATCHED AND S.IsSystem>0 AND isnull(T.IsExcNoUpdate, 0)=0
     THEN UPDATE SET 
     DatabaseId=S.DatabaseId, DbSourceId=s.DbSourceId, Flags=s.Flags, StateId=s.StateId, NAME=s.Name, NameFull=s.NameFull, KindId=s.KindId, Code=s.Code,Memo=s.Memo, FlagString=S.FlagString, CodeFind=S.CodeFind, 
     MondayW = S.MondayW,
     TuesdayW = S.TuesdayW, WednesdayW = S.WednesdayW, ThursdayW = S.ThursdayW, FridayW = S.FridayW, SaturdayW = S.SaturdayW, SundayW = S.SundayW,
       MondaySH = S.MondaySH, MondaySM = S.MondaySM, MondayEH = S.MondayEH, MondayEM = S.MondayEM, TuesdaySH = S.TuesdaySH, TuesdaySM = S.TuesdaySM,
       TuesdayEH = S.TuesdayEH, TuesdayEM = S.TuesdayEM, WednesdaySH = S.WednesdaySH, WednesdaySM = S.WednesdaySM, WednesdayEM = S.WednesdayEM,
       WednesdayEH = S.WednesdayEH, ThursdaySH = S.ThursdaySH, ThursdaySM = S.ThursdaySM, ThursdayEM = S.ThursdayEM, ThursdayEH = S.ThursdayEH,
       FridaySH = S.FridaySH, FridaySM = S.FridaySM, FridayEH = S.FridayEH, FridayEM = S.FridayEM, SaturdaySH = S.SaturdaySH, SaturdaySM = S.SaturdaySM,
       SaturdayEH = S.SaturdayEH, SaturdayEM = S.SaturdayEM, SundaySH = S.SundaySH, SundayEH = S.SundayEH, SundaySM = S.SundaySM, SundayEM = S.SundayEM;
PRINT 'Выполнено обновление графиков работы...'
GO

MERGE Developer.DbObjects AS T USING [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Developer.DbObjects AS S ON s.Guid=t.Guid
WHEN NOT MATCHED BY TARGET
     THEN INSERT (Guid, DatabaseId, DbSourceId, Flags, StateId, Name, NameFull, KindId, Code,Memo, FlagString, CodeFind,[Schema],[Description],ProcedureImport,ProcedureExport)
     VALUES(S.Guid, S.DatabaseId, S.DbSourceId, S.Flags, S.StateId, S.Name, S.NameFull, S.KindId, S.Code,S.Memo, S.FlagString, S.CodeFind,S.[Schema],S.[Description],S.ProcedureImport,S.ProcedureExport)
WHEN MATCHED AND S.IsSystem>0 AND isnull(T.IsExcNoUpdate, 0)=0
     THEN UPDATE SET 
     DatabaseId=S.DatabaseId, DbSourceId=s.DbSourceId, Flags=s.Flags, StateId=s.StateId, NAME=s.Name, NameFull=s.NameFull, KindId=s.KindId, Code=s.Code,Memo=s.Memo, FlagString=S.FlagString, CodeFind=S.CodeFind,[Schema]=S.[Schema],[Description]=S.[Description],ProcedureImport=S.ProcedureImport,ProcedureExport=S.ProcedureExport;
PRINT 'Выполнено обновление DbObjects...'
GO


MERGE Developer.DbObjectChilds AS T USING 
(
SELECT b.Id, b.Guid, b.DatabaseId, b.Flags, b.StateId, b.Name, b.NameFull,
       b.KindId, b.Code, b.Memo, b.FlagString, b.CodeFind, c.Id AS OwnId,
       b.GroupNo, b.OrderNo, b.Description, b.IsFormula, b.TypeNameSql, b.TypeNameNet, b.TypeLen, b.AllowNull
       
       FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Developer.DbObjects a
INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Developer.DbObjectChilds b ON a.Id=b.OwnId
left JOIN Developer.DbObjects c ON a.Guid=c.Guid
LEFT JOIN Developer.DbObjectChilds d ON d.Guid=b.Guid
WHERE a.IsSystem>0 AND b.IsSystem>0
) AS S ON s.Guid=t.Guid
WHEN NOT MATCHED BY TARGET
     THEN INSERT (Guid, DatabaseId, DbSourceId, Flags, StateId, Name, NameFull, KindId, Code,
       Memo, FlagString, CodeFind, OwnId,
       GroupNo, OrderNo, Description, IsFormula, TypeNameSql, TypeNameNet, TypeLen, AllowNull)
     VALUES(S.Guid, S.DatabaseId, S.Id, S.Flags, S.StateId, S.Name, S.NameFull, S.KindId, S.Code,
       S.Memo, S.FlagString, S.CodeFind, S.OwnId,
       S.GroupNo, S.OrderNo, S.Description, S.IsFormula, S.TypeNameSql, S.TypeNameNet, S.TypeLen, S.AllowNull)
WHEN MATCHED AND isnull(T.IsExcNoUpdate, 0)=0
     THEN UPDATE SET 
     Guid=S.Guid, DatabaseId=S.DatabaseId, DbSourceId=S.Id, Flags=S.Flags, StateId=S.StateId, NAME=S.Name, NameFull=S.NameFull, KindId=S.KindId, Code=S.Code,
       Memo=S.Memo, FlagString=S.FlagString, CodeFind=S.CodeFind, OwnId=S.OwnId,
       GroupNo=S.GroupNo, OrderNo=S.OrderNo, [Description]=S.[Description], IsFormula=S.IsFormula, TypeNameSql=S.TypeNameSql, TypeNameNet=S.TypeNameNet, TypeLen=S.TypeLen, AllowNull=S.AllowNull;
PRINT 'Выполнено обновление DbObjectChilds...'
GO


MERGE Core.SystemParameters AS T USING [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.SystemParameters AS S ON s.Guid=t.Guid
WHEN NOT MATCHED BY TARGET AND S.IsSystem>0
     THEN INSERT (Guid, DatabaseId, DbSourceId, Flags, StateId, Name, NameFull, KindId, Code,Memo, FlagString, CodeFind, ValueInt, ValueString, ValueGuid, ValueMoney,ValueFloat, EntityReferenceId)
     VALUES(S.Guid, S.DatabaseId, S.DbSourceId, S.Flags, S.StateId, S.Name, S.NameFull, S.KindId, S.Code,S.Memo, S.FlagString, S.CodeFind, S.ValueInt, S.ValueString, S.ValueGuid, S.ValueMoney,S.ValueFloat, S.EntityReferenceId)
WHEN MATCHED AND S.IsSystem>0 AND isnull(T.IsExcNoUpdate, 0)=0
     THEN UPDATE SET 
     DatabaseId=S.DatabaseId, DbSourceId=s.DbSourceId, Flags=s.Flags, StateId=s.StateId, NAME=s.Name, NameFull=s.NameFull, KindId=s.KindId, Code=s.Code,Memo=s.Memo, FlagString=S.FlagString, CodeFind=S.CodeFind, ValueInt=S.ValueInt, ValueString=S.ValueString, ValueGuid=S.ValueGuid, ValueMoney=S.ValueMoney,ValueFloat=S.ValueFloat, EntityReferenceId=S.EntityReferenceId;
PRINT 'Выполнено обновление системных параметров...'
GO

MERGE Secure.Users AS T USING [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Secure.Users AS S ON s.Guid=t.Guid
WHEN NOT MATCHED BY TARGET AND S.IsSystem>0
     THEN INSERT (Guid, DatabaseId, DbSourceId, Flags, StateId, Name, NameFull, KindId, Code,Memo, FlagString, CodeFind, AuthenticateKind)
     VALUES(S.Guid, S.DatabaseId, S.DbSourceId, S.Flags, S.StateId, S.Name, S.NameFull, S.KindId, S.Code,S.Memo, S.FlagString, S.CodeFind, S.AuthenticateKind)
WHEN MATCHED AND S.IsSystem>0 AND isnull(T.IsExcNoUpdate, 0)=0
     THEN UPDATE SET 
     DatabaseId=S.DatabaseId, DbSourceId=s.DbSourceId, Flags=s.Flags, StateId=s.StateId, NAME=s.Name, NameFull=s.NameFull, KindId=s.KindId, Code=s.Code,Memo=s.Memo, FlagString=S.FlagString, CodeFind=S.CodeFind, AuthenticateKind=S.AuthenticateKind;
PRINT 'Выполнено обновление системных пользователей и групп...'
GO

MERGE Secure.Rights AS T USING [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Secure.Rights AS S ON s.Guid=t.Guid
WHEN NOT MATCHED BY TARGET AND S.IsSystem>0
     THEN INSERT (Guid, DatabaseId, DbSourceId, Flags, StateId, Name, NameFull, KindId, Code,Memo, FlagString, CodeFind)
     VALUES(S.Guid, S.DatabaseId, S.DbSourceId, S.Flags, S.StateId, S.Name, S.NameFull, S.KindId, S.Code,S.Memo, S.FlagString, S.CodeFind)
WHEN MATCHED AND S.IsSystem>0 AND isnull(T.IsExcNoUpdate, 0)=0
     THEN UPDATE SET 
     DatabaseId=S.DatabaseId, DbSourceId=s.DbSourceId, Flags=s.Flags, StateId=s.StateId, NAME=s.Name, NameFull=s.NameFull, KindId=s.KindId, Code=s.Code,Memo=s.Memo, FlagString=S.FlagString, CodeFind=S.CodeFind;
PRINT 'Выполнено обновление системных разрешений...'
GO

MERGE Core.Notes AS T USING [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Notes AS S ON s.Guid=t.Guid
WHEN NOT MATCHED BY TARGET AND S.IsSystem>0
     THEN INSERT (Guid, DatabaseId, DbSourceId, Flags, StateId, Name, NameFull, KindId, Code,Memo, FlagString, CodeFind, UserOwnerId, MyCompanyId)
     VALUES(S.Guid, S.DatabaseId, S.DbSourceId, S.Flags, S.StateId, S.Name, S.NameFull, S.KindId, S.Code,S.Memo, S.FlagString, S.CodeFind, S.UserOwnerId, S.MyCompanyId)
WHEN MATCHED AND S.IsSystem>0 AND isnull(T.IsExcNoUpdate, 0)=0
     THEN UPDATE SET 
     DatabaseId=S.DatabaseId, DbSourceId=s.DbSourceId, Flags=s.Flags, StateId=s.StateId, NAME=s.Name, NameFull=s.NameFull, KindId=s.KindId, Code=s.Code,Memo=s.Memo, FlagString=S.FlagString, CodeFind=S.CodeFind, UserOwnerId=S.UserOwnerId, MyCompanyId=S.MyCompanyId;
PRINT 'Выполнено обновление примечаний'
GO

MERGE Contractor.Depatments AS T USING [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Contractor.Depatments AS S ON s.Guid=t.Guid
WHEN NOT MATCHED BY TARGET AND S.IsSystem>0
     THEN INSERT (Guid, DatabaseId, DbSourceId, Flags, StateId, Name, NameFull, KindId, Code,Memo, FlagString, CodeFind, MyCompanyId)
     VALUES(S.Guid, S.DatabaseId, S.DbSourceId, S.Flags, S.StateId, S.Name, S.NameFull, S.KindId, S.Code,S.Memo, S.FlagString, S.CodeFind, S.MyCompanyId)
WHEN MATCHED AND S.IsSystem>0 AND isnull(T.IsExcNoUpdate, 0)=0
     THEN UPDATE SET 
     DatabaseId=S.DatabaseId, DbSourceId=s.DbSourceId, Flags=s.Flags, StateId=s.StateId, NAME=s.Name, NameFull=s.NameFull, KindId=s.KindId, Code=s.Code,Memo=s.Memo, FlagString=S.FlagString, CodeFind=S.CodeFind, MyCompanyId=S.MyCompanyId;
PRINT 'Выполнено обновление подразделений'
GO


-- Шаблоны документов Склад
INSERT INTO [Document].Documents(Guid, DatabaseId, Flags, StateId, Name, NameFull,
            Kind, Code, CodeFind, Memo, Date, AgentFromId, AgentToId,
            AgentDepartmentFromId, AgentDepartmentToId, CurrencyId,
            CurrencyIdTrans, CurrencyIdCountry, Summa, SummaBase, SummaCurrency,
            SummaTax, Number, Accounting, MyCompanyId, FolderId, FormId)
SELECT d.Guid, d.DatabaseId, d.Flags, d.StateId, d.Name, d.NameFull,
            d.Kind, d.Code, d.CodeFind, d.Memo, d.Date, d.AgentFromId, d.AgentToId,
            d.AgentDepartmentFromId, d.AgentDepartmentToId, d.CurrencyId,
            d.CurrencyIdTrans, d.CurrencyIdCountry, d.Summa, d.SummaBase, d.SummaCurrency,
            d.SummaTax, d.Number, d.Accounting, d.MyCompanyId, fs.Id, ls.Id
FROM  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Document].Documents d
            INNER JOIN  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Folders f ON d.FolderId=f.Id
            INNER JOIN  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Libraries l ON d.FormId=l.Id
            INNER JOIN Core.Folders fs ON fs.Guid=f.Guid
            INNER JOIN Core.Libraries ls ON ls.Guid=l.Guid
WHERE d.IsTemplate>0 AND d.TypeValue=1
            AND d.Guid IN (
			SELECT Guid from [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Document].Documents WHERE TypeValue = 1 AND IsSystem>0
			)
AND d.Guid NOT IN (SELECT Guid FROM [Document].Documents d2)
GO

-- Шаблоны документов Продаж
INSERT INTO [Document].Documents(Guid, DatabaseId, Flags, StateId, Name, NameFull,
            Kind, Code, CodeFind, Memo, Date, AgentFromId, AgentToId,
            AgentDepartmentFromId, AgentDepartmentToId, CurrencyId,
            CurrencyIdTrans, CurrencyIdCountry, Summa, SummaBase, SummaCurrency,
            SummaTax, Number, Accounting, MyCompanyId, FolderId, FormId)
SELECT d.Guid, d.DatabaseId, d.Flags, d.StateId, d.Name, d.NameFull,
            d.Kind, d.Code, d.CodeFind, d.Memo, d.Date, d.AgentFromId, d.AgentToId,
            d.AgentDepartmentFromId, d.AgentDepartmentToId, d.CurrencyId,
            d.CurrencyIdTrans, d.CurrencyIdCountry, d.Summa, d.SummaBase, d.SummaCurrency,
            d.SummaTax, d.Number, d.Accounting, d.MyCompanyId, fs.Id, ls.Id
FROM  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Document].Documents d
            INNER JOIN  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Folders f ON d.FolderId=f.Id
            INNER JOIN  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Libraries l ON d.FormId=l.Id
            INNER JOIN Core.Folders fs ON fs.Guid=f.Guid
            INNER JOIN Core.Libraries ls ON ls.Guid=l.Guid
WHERE d.IsTemplate>0 AND d.TypeValue=2
            AND d.Guid IN (
			SELECT Guid from [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Document].Documents WHERE TypeValue = 2 AND IsSystem>0
			)
AND d.Guid NOT IN (SELECT Guid FROM [Document].Documents d2)
GO

-- Шаблоны документов Бухгалтерия
INSERT INTO [Document].Documents(Guid, DatabaseId, Flags, StateId, Name, NameFull,
            Kind, Code, CodeFind, Memo, Date, AgentFromId, AgentToId,
            AgentDepartmentFromId, AgentDepartmentToId, CurrencyId,
            CurrencyIdTrans, CurrencyIdCountry, Summa, SummaBase, SummaCurrency,
            SummaTax, Number, Accounting, MyCompanyId, FolderId, FormId)
SELECT d.Guid, d.DatabaseId, d.Flags, d.StateId, d.Name, d.NameFull,
            d.Kind, d.Code, d.CodeFind, d.Memo, d.Date, d.AgentFromId, d.AgentToId,
            d.AgentDepartmentFromId, d.AgentDepartmentToId, d.CurrencyId,
            d.CurrencyIdTrans, d.CurrencyIdCountry, d.Summa, d.SummaBase, d.SummaCurrency,
            d.SummaTax, d.Number, d.Accounting, d.MyCompanyId, fs.Id, ls.Id
FROM  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Document].Documents d
            INNER JOIN  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Folders f ON d.FolderId=f.Id
            INNER JOIN  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Libraries l ON d.FormId=l.Id
            INNER JOIN Core.Folders fs ON fs.Guid=f.Guid
            INNER JOIN Core.Libraries ls ON ls.Guid=l.Guid
WHERE d.IsTemplate>0 AND d.TypeValue=3
            AND d.Guid IN (
			SELECT Guid from [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Document].Documents WHERE TypeValue = 3 AND IsSystem>0
			)
AND d.Guid NOT IN (SELECT Guid FROM [Document].Documents d2)
GO

-- Шаблоны документов Учет компьютеров, Учет принтеров,
INSERT INTO [Document].Documents(Guid, DatabaseId, Flags, StateId, Name, NameFull,
            Kind, Code, CodeFind, Memo, Date, AgentFromId, AgentToId,
            AgentDepartmentFromId, AgentDepartmentToId, CurrencyId,
            CurrencyIdTrans, CurrencyIdCountry, Summa, SummaBase, SummaCurrency,
            SummaTax, Number, Accounting, MyCompanyId, FolderId, FormId)
SELECT d.Guid, d.DatabaseId, d.Flags, d.StateId, d.Name, d.NameFull,
            d.Kind, d.Code, d.CodeFind, d.Memo, d.Date, d.AgentFromId, d.AgentToId,
            d.AgentDepartmentFromId, d.AgentDepartmentToId, d.CurrencyId,
            d.CurrencyIdTrans, d.CurrencyIdCountry, d.Summa, d.SummaBase, d.SummaCurrency,
            d.SummaTax, d.Number, d.Accounting, d.MyCompanyId, fs.Id, ls.Id
FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Document].Documents d
            INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Folders f ON d.FolderId=f.Id
            INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Libraries l ON d.FormId=l.Id
            INNER JOIN Core.Folders fs ON fs.Guid=f.Guid
            INNER JOIN Core.Libraries ls ON ls.Guid=l.Guid
WHERE d.IsTemplate>0 AND d.TypeValue=4
            AND d.Guid IN (
			SELECT Guid from [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Document].Documents WHERE TypeValue = 4 AND IsSystem>0
            )
AND d.Guid NOT IN (SELECT Guid FROM [Document].Documents d2)
GO

-- Шаблоны документов Ценовой политики
INSERT INTO [Document].Documents(Guid, DatabaseId, Flags, StateId, Name, NameFull,
            Kind, Code, CodeFind, Memo, Date, AgentFromId, AgentToId,
            AgentDepartmentFromId, AgentDepartmentToId, CurrencyId,
            CurrencyIdTrans, CurrencyIdCountry, Summa, SummaBase, SummaCurrency,
            SummaTax, Number, Accounting, MyCompanyId, FolderId, FormId)
SELECT d.Guid, d.DatabaseId, d.Flags, d.StateId, d.Name, d.NameFull,
            d.Kind, d.Code, d.CodeFind, d.Memo, d.Date, d.AgentFromId, d.AgentToId,
            d.AgentDepartmentFromId, d.AgentDepartmentToId, d.CurrencyId,
            d.CurrencyIdTrans, d.CurrencyIdCountry, d.Summa, d.SummaBase, d.SummaCurrency,
            d.SummaTax, d.Number, d.Accounting, d.MyCompanyId, fs.Id, ls.Id
FROM  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Document].Documents d
            INNER JOIN  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Folders f ON d.FolderId=f.Id
            INNER JOIN  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Libraries l ON d.FormId=l.Id
            INNER JOIN Core.Folders fs ON fs.Guid=f.Guid
            INNER JOIN Core.Libraries ls ON ls.Guid=l.Guid
WHERE d.IsTemplate>0 AND d.TypeValue=5
            AND d.Guid IN (
              SELECT Guid from [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Document].Documents WHERE TypeValue = 5 AND IsSystem>0 	
            )
AND d.Guid NOT IN (SELECT Guid FROM [Document].Documents d2)
GO
INSERT [Price].[Documents](Id, Guid, DatabaseId, Flags, StateId, Date, Kind,PrcNameId, [ExpireDate], PrcNameId2, DateStart)
SELECT ds.Id, d.Guid, d.DatabaseId, d.Flags, d.StateId, 
            d.Date, d.Kind, ps.Id AS PrcNameId, d.[ExpireDate], ps2.Id as [PrcNameId2], d.[DateStart]
FROM  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Price].[Documents] d
      INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Document].[Documents] dm ON d.Id=dm.Id
      INNER JOIN [Document].[Documents] ds ON ds.Guid=dm.Guid  
            INNER JOIN  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Price].[PriceNames] p ON d.PrcNameId=p.Id
            left JOIN  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Price].[PriceNames] p2 ON d.PrcNameId2=p2.Id
            INNER JOIN  [Price].[PriceNames] ps ON ps.Guid=p.Guid
            left JOIN  [Price].[PriceNames] ps2 ON ps2.Guid=p2.Guid
WHERE d.IsTemplate>0 AND dm.TypeValue=5
            AND d.Guid IN (
              SELECT b.Guid from [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Document].Documents a
              INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Price].[Documents] b ON a.Id=b.Id
               WHERE TypeValue = 5 AND a.IsSystem>0 	
            )
AND d.Guid NOT IN (SELECT Guid FROM [Price].[Documents] d2)
GO

-- Шаблоны финансов
INSERT INTO [Document].Documents(Guid, DatabaseId, Flags, StateId, Name, NameFull,
            Kind, Code, CodeFind, Memo, Date, AgentFromId, AgentToId,
            AgentDepartmentFromId, AgentDepartmentToId, CurrencyId,
            CurrencyIdTrans, CurrencyIdCountry, Summa, SummaBase, SummaCurrency,
            SummaTax, Number, Accounting, MyCompanyId, FolderId, FormId)
SELECT d.Guid, d.DatabaseId, d.Flags, d.StateId, d.Name, d.NameFull,
            d.Kind, d.Code, d.CodeFind, d.Memo, d.Date, d.AgentFromId, d.AgentToId,
            d.AgentDepartmentFromId, d.AgentDepartmentToId, d.CurrencyId,
            d.CurrencyIdTrans, d.CurrencyIdCountry, d.Summa, d.SummaBase, d.SummaCurrency,
            d.SummaTax, d.Number, d.Accounting, d.MyCompanyId, fs.Id, ls.Id
FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Document].Documents d
            INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Folders f ON d.FolderId=f.Id
            INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Libraries l ON d.FormId=l.Id
            INNER JOIN Core.Folders fs ON fs.Guid=f.Guid
            INNER JOIN Core.Libraries ls ON ls.Guid=l.Guid
WHERE d.IsTemplate>0 AND d.TypeValue=6
            AND d.Guid IN (
			SELECT Guid from [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Document].Documents WHERE TypeValue = 6 AND IsSystem>0
            )
AND d.Guid NOT IN (SELECT Guid FROM [Document].Documents d2)
GO

-- Шаблоны налоговых
INSERT INTO [Document].Documents(Guid, DatabaseId, Flags, StateId, Name, NameFull,
            Kind, Code, CodeFind, Memo, Date, AgentFromId, AgentToId,
            AgentDepartmentFromId, AgentDepartmentToId, CurrencyId,
            CurrencyIdTrans, CurrencyIdCountry, Summa, SummaBase, SummaCurrency,
            SummaTax, Number, Accounting, MyCompanyId, FolderId, FormId)
SELECT d.Guid, d.DatabaseId, d.Flags, d.StateId, d.Name, d.NameFull,
            d.Kind, d.Code, d.CodeFind, d.Memo, d.Date, d.AgentFromId, d.AgentToId,
            d.AgentDepartmentFromId, d.AgentDepartmentToId, d.CurrencyId,
            d.CurrencyIdTrans, d.CurrencyIdCountry, d.Summa, d.SummaBase, d.SummaCurrency,
            d.SummaTax, d.Number, d.Accounting, d.MyCompanyId, fs.Id, ls.Id
FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Document].Documents d
            INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Folders f ON d.FolderId=f.Id
            INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Libraries l ON d.FormId=l.Id
            INNER JOIN Core.Folders fs ON fs.Guid=f.Guid
            INNER JOIN Core.Libraries ls ON ls.Guid=l.Guid
WHERE d.IsTemplate>0 AND d.TypeValue=7
            AND d.Guid IN (
			SELECT Guid from [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Document].Documents WHERE TypeValue = 7 AND IsSystem>0
            )
AND d.Guid NOT IN (SELECT Guid FROM [Document].Documents d2)
GO

-- Шаблоны Управление скидками на товар
INSERT INTO [Document].Documents(Guid, DatabaseId, Flags, StateId, Name, NameFull,
            Kind, Code, CodeFind, Memo, Date, AgentFromId, AgentToId,
            AgentDepartmentFromId, AgentDepartmentToId, CurrencyId,
            CurrencyIdTrans, CurrencyIdCountry, Summa, SummaBase, SummaCurrency,
            SummaTax, Number, Accounting, MyCompanyId, FolderId, FormId)
SELECT d.Guid, d.DatabaseId, d.Flags, d.StateId, d.Name, d.NameFull,
            d.Kind, d.Code, d.CodeFind, d.Memo, d.Date, d.AgentFromId, d.AgentToId,
            d.AgentDepartmentFromId, d.AgentDepartmentToId, d.CurrencyId,
            d.CurrencyIdTrans, d.CurrencyIdCountry, d.Summa, d.SummaBase, d.SummaCurrency,
            d.SummaTax, d.Number, d.Accounting, d.MyCompanyId, fs.Id, ls.Id
FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Document].Documents d
            INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Folders f ON d.FolderId=f.Id
            INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Libraries l ON d.FormId=l.Id
            INNER JOIN Core.Folders fs ON fs.Guid=f.Guid
            INNER JOIN Core.Libraries ls ON ls.Guid=l.Guid
WHERE d.IsTemplate>0 AND d.TypeValue=8
            AND d.Guid IN (
			SELECT Guid from [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Document].Documents WHERE TypeValue = 8 AND IsSystem>0
            )
AND d.Guid NOT IN (SELECT Guid FROM [Document].Documents d2)
GO

-- Шаблоны Управление услугами
INSERT INTO [Document].Documents(Guid, DatabaseId, Flags, StateId, Name, NameFull,
            Kind, Code, CodeFind, Memo, Date, AgentFromId, AgentToId,
            AgentDepartmentFromId, AgentDepartmentToId, CurrencyId,
            CurrencyIdTrans, CurrencyIdCountry, Summa, SummaBase, SummaCurrency,
            SummaTax, Number, Accounting, MyCompanyId, FolderId, FormId)
SELECT d.Guid, d.DatabaseId, d.Flags, d.StateId, d.Name, d.NameFull,
            d.Kind, d.Code, d.CodeFind, d.Memo, d.Date, d.AgentFromId, d.AgentToId,
            d.AgentDepartmentFromId, d.AgentDepartmentToId, d.CurrencyId,
            d.CurrencyIdTrans, d.CurrencyIdCountry, d.Summa, d.SummaBase, d.SummaCurrency,
            d.SummaTax, d.Number, d.Accounting, d.MyCompanyId, fs.Id, ls.Id
FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Document].Documents d
            INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Folders f ON d.FolderId=f.Id
            INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Libraries l ON d.FormId=l.Id
            INNER JOIN Core.Folders fs ON fs.Guid=f.Guid
            INNER JOIN Core.Libraries ls ON ls.Guid=l.Guid
WHERE d.IsTemplate>0 AND d.TypeValue=9
            AND d.Guid IN (
			SELECT Guid from [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Document].Documents WHERE TypeValue = 9 AND IsSystem>0
            )
AND d.Guid NOT IN (SELECT Guid FROM [Document].Documents d2)
GO

-- Шаблоны Производство
INSERT INTO [Document].Documents(Guid, DatabaseId, Flags, StateId, Name, NameFull,
            Kind, Code, CodeFind, Memo, Date, AgentFromId, AgentToId,
            AgentDepartmentFromId, AgentDepartmentToId, CurrencyId,
            CurrencyIdTrans, CurrencyIdCountry, Summa, SummaBase, SummaCurrency,
            SummaTax, Number, Accounting, MyCompanyId, FolderId, FormId)
SELECT d.Guid, d.DatabaseId, d.Flags, d.StateId, d.Name, d.NameFull,
            d.Kind, d.Code, d.CodeFind, d.Memo, d.Date, d.AgentFromId, d.AgentToId,
            d.AgentDepartmentFromId, d.AgentDepartmentToId, d.CurrencyId,
            d.CurrencyIdTrans, d.CurrencyIdCountry, d.Summa, d.SummaBase, d.SummaCurrency,
            d.SummaTax, d.Number, d.Accounting, d.MyCompanyId, fs.Id, ls.Id
FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Document].Documents d
            INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Folders f ON d.FolderId=f.Id
            INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Libraries l ON d.FormId=l.Id
            INNER JOIN Core.Folders fs ON fs.Guid=f.Guid
            INNER JOIN Core.Libraries ls ON ls.Guid=l.Guid
WHERE d.IsTemplate>0 AND d.TypeValue=10
            AND d.Guid IN (
			SELECT Guid from [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Document].Documents WHERE TypeValue = 10 AND IsSystem>0
            )
AND d.Guid NOT IN (SELECT Guid FROM [Document].Documents d2)
GO

-- Шаблоны Основные средства
INSERT INTO [Document].Documents(Guid, DatabaseId, Flags, StateId, Name, NameFull,
            Kind, Code, CodeFind, Memo, Date, AgentFromId, AgentToId,
            AgentDepartmentFromId, AgentDepartmentToId, CurrencyId,
            CurrencyIdTrans, CurrencyIdCountry, Summa, SummaBase, SummaCurrency,
            SummaTax, Number, Accounting, MyCompanyId, FolderId, FormId)
SELECT d.Guid, d.DatabaseId, d.Flags, d.StateId, d.Name, d.NameFull,
            d.Kind, d.Code, d.CodeFind, d.Memo, d.Date, d.AgentFromId, d.AgentToId,
            d.AgentDepartmentFromId, d.AgentDepartmentToId, d.CurrencyId,
            d.CurrencyIdTrans, d.CurrencyIdCountry, d.Summa, d.SummaBase, d.SummaCurrency,
            d.SummaTax, d.Number, d.Accounting, d.MyCompanyId, fs.Id, ls.Id
FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Document].Documents d
            INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Folders f ON d.FolderId=f.Id
            INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Libraries l ON d.FormId=l.Id
            INNER JOIN Core.Folders fs ON fs.Guid=f.Guid
            INNER JOIN Core.Libraries ls ON ls.Guid=l.Guid
WHERE d.IsTemplate>0 AND d.TypeValue=11
            AND d.Guid IN (
			SELECT Guid from [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Document].Documents WHERE TypeValue = 11 AND IsSystem>0
            )
AND d.Guid NOT IN (SELECT Guid FROM [Document].Documents d2)
GO

-- Шаблоны Планирование финансовое
INSERT INTO [Document].Documents(Guid, DatabaseId, Flags, StateId, Name, NameFull,
            Kind, Code, CodeFind, Memo, Date, AgentFromId, AgentToId,
            AgentDepartmentFromId, AgentDepartmentToId, CurrencyId,
            CurrencyIdTrans, CurrencyIdCountry, Summa, SummaBase, SummaCurrency,
            SummaTax, Number, Accounting, MyCompanyId, FolderId, FormId)
SELECT d.Guid, d.DatabaseId, d.Flags, d.StateId, d.Name, d.NameFull,
            d.Kind, d.Code, d.CodeFind, d.Memo, d.Date, d.AgentFromId, d.AgentToId,
            d.AgentDepartmentFromId, d.AgentDepartmentToId, d.CurrencyId,
            d.CurrencyIdTrans, d.CurrencyIdCountry, d.Summa, d.SummaBase, d.SummaCurrency,
            d.SummaTax, d.Number, d.Accounting, d.MyCompanyId, fs.Id, ls.Id
FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Document].Documents d
            INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Folders f ON d.FolderId=f.Id
            INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Libraries l ON d.FormId=l.Id
            INNER JOIN Core.Folders fs ON fs.Guid=f.Guid
            INNER JOIN Core.Libraries ls ON ls.Guid=l.Guid
WHERE d.IsTemplate>0 AND d.TypeValue=12
            AND d.Guid IN (
			SELECT Guid from [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Document].Documents WHERE TypeValue = 12 AND IsSystem>0
            )
AND d.Guid NOT IN (SELECT Guid FROM [Document].Documents d2)
GO

-- Шаблоны документов Person
INSERT INTO [Document].Documents(Guid, DatabaseId, Flags, StateId, Name, NameFull,
            Kind, Code, CodeFind, Memo, Date, AgentFromId, AgentToId,
            AgentDepartmentFromId, AgentDepartmentToId, CurrencyId,
            CurrencyIdTrans, CurrencyIdCountry, Summa, SummaBase, SummaCurrency,
            SummaTax, Number, Accounting, MyCompanyId, FolderId, FormId)
SELECT d.Guid, d.DatabaseId, d.Flags, d.StateId, d.Name, d.NameFull,
            d.Kind, d.Code, d.CodeFind, d.Memo, d.Date, d.AgentFromId, d.AgentToId,
            d.AgentDepartmentFromId, d.AgentDepartmentToId, d.CurrencyId,
            d.CurrencyIdTrans, d.CurrencyIdCountry, d.Summa, d.SummaBase, d.SummaCurrency,
            d.SummaTax, d.Number, d.Accounting, d.MyCompanyId, fs.Id, ls.Id
FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Document].Documents d
            INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Folders f ON d.FolderId=f.Id
            INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Libraries l ON d.FormId=l.Id
            INNER JOIN Core.Folders fs ON fs.Guid=f.Guid
            INNER JOIN Core.Libraries ls ON ls.Guid=l.Guid
WHERE d.IsTemplate>0 AND d.TypeValue=13
            AND d.Guid IN (
			SELECT Guid from [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Document].Documents WHERE TypeValue = 13 AND IsSystem>0
			)
AND d.Guid NOT IN (SELECT Guid FROM [Document].Documents d2)
GO

-- Шаблоны документов Маркетинг
INSERT INTO [Document].Documents(Guid, DatabaseId, Flags, StateId, Name, NameFull,
            Kind, Code, CodeFind, Memo, Date, AgentFromId, AgentToId,
            AgentDepartmentFromId, AgentDepartmentToId, CurrencyId,
            CurrencyIdTrans, CurrencyIdCountry, Summa, SummaBase, SummaCurrency,
            SummaTax, Number, Accounting, MyCompanyId, FolderId, FormId)
SELECT d.Guid, d.DatabaseId, d.Flags, d.StateId, d.Name, d.NameFull,
            d.Kind, d.Code, d.CodeFind, d.Memo, d.Date, ags.Id as AgentFromId, ags2.Id as AgentToId,
            ags3.Id as AgentDepartmentFromId, ags4.Id as AgentDepartmentToId, d.CurrencyId,
            d.CurrencyIdTrans, d.CurrencyIdCountry, d.Summa, d.SummaBase, d.SummaCurrency,
            d.SummaTax, d.Number, d.Accounting, d.MyCompanyId, fs.Id, ls.Id
FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Document].Documents d
            INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Folders f ON d.FolderId=f.Id
            INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Libraries l ON d.FormId=l.Id
            INNER JOIN Core.Folders fs ON fs.Guid=f.Guid
            INNER JOIN Core.Libraries ls ON ls.Guid=l.Guid
            LEFT JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Contractor.Agents a ON a.Id=d.AgentFromId
            LEFT JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Contractor.Agents a2 ON a2.Id=d.AgentToId
            LEFT JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Contractor.Agents a3 ON a3.Id=d.AgentDepartmentFromId
            LEFT JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Contractor.Agents a4 ON a4.Id=d.AgentDepartmentToId
            LEFT JOIN Contractor.Agents ags ON ags.Guid=a.Guid
            LEFT JOIN Contractor.Agents ags2 ON ags2.Guid=a2.Guid
            LEFT JOIN Contractor.Agents ags3 ON ags3.Guid=a3.Guid
            LEFT JOIN Contractor.Agents ags4 ON ags4.Guid=a4.Guid
WHERE d.IsTemplate>0 AND d.TypeValue=14
            AND d.Guid IN (
			SELECT Guid from [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Document].Documents WHERE TypeValue = 14 AND IsSystem>0
			)
AND d.Guid NOT IN (SELECT Guid FROM [Document].Documents d2)
GO

-- Шаблоны документов Маршруты
INSERT INTO [Document].Documents(Guid, DatabaseId, Flags, StateId, Name, NameFull,
            Kind, Code, CodeFind, Memo, Date, AgentFromId, AgentToId,
            AgentDepartmentFromId, AgentDepartmentToId, CurrencyId,
            CurrencyIdTrans, CurrencyIdCountry, Summa, SummaBase, SummaCurrency,
            SummaTax, Number, Accounting, MyCompanyId, FolderId, FormId)
SELECT d.Guid, d.DatabaseId, d.Flags, d.StateId, d.Name, d.NameFull,
            d.Kind, d.Code, d.CodeFind, d.Memo, d.Date, d.AgentFromId, d.AgentToId,
            d.AgentDepartmentFromId, d.AgentDepartmentToId, d.CurrencyId,
            d.CurrencyIdTrans, d.CurrencyIdCountry, d.Summa, d.SummaBase, d.SummaCurrency,
            d.SummaTax, d.Number, d.Accounting, d.MyCompanyId, fs.Id, ls.Id
FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Document].Documents d
            INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Folders f ON d.FolderId=f.Id
            INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Libraries l ON d.FormId=l.Id
            INNER JOIN Core.Folders fs ON fs.Guid=f.Guid
            INNER JOIN Core.Libraries ls ON ls.Guid=l.Guid
WHERE d.IsTemplate>0 AND d.TypeValue=15
            AND d.Guid IN (
			SELECT Guid from [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Document].Documents WHERE TypeValue = 15 AND IsSystem>0
			)
AND d.Guid NOT IN (SELECT Guid FROM [Document].Documents d2)
GO

-- Шаблоны документов Управление ремонтами
INSERT INTO [Document].Documents(Guid, DatabaseId, Flags, StateId, Name, NameFull,
            Kind, Code, CodeFind, Memo, Date, AgentFromId, AgentToId,
            AgentDepartmentFromId, AgentDepartmentToId, CurrencyId,
            CurrencyIdTrans, CurrencyIdCountry, Summa, SummaBase, SummaCurrency,
            SummaTax, Number, Accounting, MyCompanyId, FolderId, FormId)
SELECT d.Guid, d.DatabaseId, d.Flags, d.StateId, d.Name, d.NameFull,
            d.Kind, d.Code, d.CodeFind, d.Memo, d.Date, d.AgentFromId, d.AgentToId,
            d.AgentDepartmentFromId, d.AgentDepartmentToId, d.CurrencyId,
            d.CurrencyIdTrans, d.CurrencyIdCountry, d.Summa, d.SummaBase, d.SummaCurrency,
            d.SummaTax, d.Number, d.Accounting, d.MyCompanyId, fs.Id, ls.Id
FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Document].Documents d
            INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Folders f ON d.FolderId=f.Id
            INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Core.Libraries l ON d.FormId=l.Id
            INNER JOIN Core.Folders fs ON fs.Guid=f.Guid
            INNER JOIN Core.Libraries ls ON ls.Guid=l.Guid
WHERE d.IsTemplate>0 AND d.TypeValue=16
            AND d.Guid IN (
			SELECT Guid from [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Document].Documents WHERE TypeValue = 16 AND IsSystem>0
			)
AND d.Guid NOT IN (SELECT Guid FROM [Document].Documents d2)
GO

MERGE Developer.Knowledges AS T USING [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Developer.Knowledges AS S ON s.Guid=t.Guid
WHEN NOT MATCHED BY TARGET AND (S.IsSystem>0 or S.MyCompanyId=-100)
     THEN INSERT (Guid, DatabaseId, DbSourceId, Flags, StateId, Name, NameFull, KindId, Code,Memo, FlagString, CodeFind, MyCompanyId)
     VALUES(S.Guid, S.DatabaseId, S.DbSourceId, S.Flags, S.StateId, S.Name, S.NameFull, S.KindId, S.Code,S.Memo, S.FlagString, S.CodeFind, S.MyCompanyId)
WHEN MATCHED AND (S.IsSystem>0 or S.MyCompanyId=-100) AND isnull(T.IsExcNoUpdate, 0)=0
     THEN UPDATE SET 
     DatabaseId=S.DatabaseId, DbSourceId=s.DbSourceId, Flags=s.Flags, StateId=s.StateId, NAME=s.Name, NameFull=s.NameFull, KindId=s.KindId, Code=s.Code,Memo=s.Memo, FlagString=S.FlagString, CodeFind=S.CodeFind, MyCompanyId=S.MyCompanyId;
PRINT 'Выполнено обновление базы знаний'
GO

update Developer.Knowledges
set FileId=fs.Id
from  
Developer.Knowledges t inner join 
[<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Developer.Knowledges AS S ON s.Guid=t.Guid
inner join [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].FileData.Files f on s.[FileId] = f.Id
inner join FileData.Files fs on f.Guid=fs.Guid
where t.IsSystem>0 and s.IsSystem>0
and isnull(T.IsExcNoUpdate, 0)=0
and isnull(t.FileId, 0)<>fs.Id
GO

MERGE Hierarchy.HierarchyContents AS T USING
(
SELECT c.Id, c.Guid, c.DatabaseId, c.Flags, c.StateId, h2.Id as HierarchyId, d.Id AS ElementId, 75 as ToDbEntityId, c.SortOrder  
  FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Hierarchy.Hierarchies h
INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Hierarchy.HierarchyContents c ON h.Id=c.HierarchyId
INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Developer.Knowledges l ON c.ElementId = l.Id
INNER JOIN Hierarchy.Hierarchies h2 ON h.Guid=h2.Guid
INNER JOIN Developer.Knowledges d ON d.Guid=l.Guid
WHERE h.DbEntityId=75 AND h.IsSystem>0 AND c.ToDbEntityId=75 and l.IsSystem>0	
) AS S ON s.Guid=t.Guid
WHEN NOT MATCHED BY TARGET
     THEN INSERT (Guid, DatabaseId, DbSourceId, Flags, StateId, HierarchyId, ElementId, ToDbEntityId, SortOrder)
     VALUES(S.Guid, S.DatabaseId, S.Id, S.Flags, S.StateId, S.HierarchyId, S.ElementId, S.ToDbEntityId, S.SortOrder)
WHEN MATCHED --AND S.IsSystem>0 AND isnull(S.IsExcNoUpdate, 0)=0
     THEN UPDATE SET 
     DatabaseId=S.DatabaseId, DbSourceId=s.Id, Flags=s.Flags, StateId=s.StateId, HierarchyId=S.HierarchyId, ElementId=S.ElementId, ToDbEntityId=S.ToDbEntityId, SortOrder=S.SortOrder;
PRINT 'Выполнено обновление структуры базы знаний...'
GO
UPDATE Document.Documents
SET [Name] = s.[Name]
FROM Document.Documents a INNER JOIN  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Document.Documents s ON a.Guid=s.Guid
WHERE a.IsTemplate>0 AND isnull(a.IsExcNoUpdate, 0)=0 AND a.Name<>s.Name
PRINT 'Выполнено обновление заголовков шаблона докуметов...'
GO
UPDATE Document.Documents
SET [Code] = s.[Code]
FROM Document.Documents a INNER JOIN  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Document.Documents s ON a.Guid=s.Guid
WHERE a.IsTemplate>0 AND isnull(a.IsExcNoUpdate, 0)=0 AND a.Name<>s.Name
PRINT 'Выполнено обновление кодов шаблона докуметов...'
GO

UPDATE Document.Documents
SET MyCompanyId=-1
FROM Document.Documents a INNER JOIN  [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Document.Documents s ON a.Guid=s.Guid
WHERE a.IsTemplate>0 AND isnull(a.IsExcNoUpdate, 0)=0 AND isnull(a.MyCompanyId,0)<>isnull(s.MyCompanyId,0)
AND s.MyCompanyId = -1
PRINT 'Выполнено обновление владельцев шаблона докуметов...'
GO

MERGE [Ourp].[Equipments] AS T USING [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Ourp].[Equipments] AS S ON s.Guid=t.Guid
WHEN NOT MATCHED BY TARGET AND S.IsSystem>0
     THEN INSERT (Guid, DatabaseId, DbSourceId, Flags, StateId, Name, NameFull, KindId, Code,Memo, FlagString, CodeFind, MyCompanyId)
     VALUES(S.Guid, S.DatabaseId, S.DbSourceId, S.Flags, S.StateId, S.Name, S.NameFull, S.KindId, S.Code,S.Memo, S.FlagString, S.CodeFind, S.MyCompanyId)
WHEN MATCHED AND S.IsSystem>0 AND isnull(T.IsExcNoUpdate, 0)=0
     THEN UPDATE SET 
     DatabaseId=S.DatabaseId, DbSourceId=s.DbSourceId, Flags=s.Flags, StateId=s.StateId, NAME=s.Name, NameFull=s.NameFull, KindId=s.KindId, Code=s.Code,Memo=s.Memo, FlagString=S.FlagString, CodeFind=S.CodeFind, MyCompanyId=S.MyCompanyId;
PRINT 'Выполнено обновление оборудования...'
GO
MERGE [Ourp].EquipmentNodes AS T USING [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Ourp].[EquipmentNodes] AS S ON s.Guid=t.Guid
WHEN NOT MATCHED BY TARGET AND S.IsSystem>0
     THEN INSERT (Guid, DatabaseId, DbSourceId, Flags, StateId, Name, NameFull, KindId, Code,Memo, FlagString, CodeFind, MyCompanyId)
     VALUES(S.Guid, S.DatabaseId, S.DbSourceId, S.Flags, S.StateId, S.Name, S.NameFull, S.KindId, S.Code,S.Memo, S.FlagString, S.CodeFind, S.MyCompanyId)
WHEN MATCHED AND S.IsSystem>0 AND isnull(T.IsExcNoUpdate, 0)=0
     THEN UPDATE SET 
     DatabaseId=S.DatabaseId, DbSourceId=s.DbSourceId, Flags=s.Flags, StateId=s.StateId, NAME=s.Name, NameFull=s.NameFull, KindId=s.KindId, Code=s.Code,Memo=s.Memo, FlagString=S.FlagString, CodeFind=S.CodeFind, MyCompanyId=S.MyCompanyId;
PRINT 'Выполнено обновление оборудования...'
GO

MERGE [Ourp].EquipmentDetails AS T USING [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].[Ourp].[EquipmentDetails] AS S ON s.Guid=t.Guid
WHEN NOT MATCHED BY TARGET AND S.IsSystem>0
     THEN INSERT (Guid, DatabaseId, DbSourceId, Flags, StateId, Name, NameFull, KindId, Code,Memo, FlagString, CodeFind, MyCompanyId)
     VALUES(S.Guid, S.DatabaseId, S.DbSourceId, S.Flags, S.StateId, S.Name, S.NameFull, S.KindId, S.Code,S.Memo, S.FlagString, S.CodeFind, S.MyCompanyId)
WHEN MATCHED AND S.IsSystem>0 AND isnull(T.IsExcNoUpdate, 0)=0
     THEN UPDATE SET 
     DatabaseId=S.DatabaseId, DbSourceId=s.DbSourceId, Flags=s.Flags, StateId=s.StateId, NAME=s.Name, NameFull=s.NameFull, KindId=s.KindId, Code=s.Code,Memo=s.Memo, FlagString=S.FlagString, CodeFind=S.CodeFind, MyCompanyId=S.MyCompanyId;
PRINT 'Выполнено обновление оборудования...'
GO


-- Сихронизация иерархии банков...
DECLARE @rootBankHierarchySource INT
SELECT @rootBankHierarchySource = Id FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Hierarchy.Hierarchies h WHERE h.Code='SYSTEM_AGENT_BANKS' 

DECLARE @rootBankHierarchyDestination INT
SELECT @rootBankHierarchyDestination = Id FROM Hierarchy.Hierarchies h WHERE h.Code='SYSTEM_AGENT_BANKS'

declare @tblBankHierarchyContents table(HierarchyId int, ElementGuid uniqueidentifier, ToDbEntityId int)

insert @tblBankHierarchyContents (HierarchyId , ElementGuid , ToDbEntityId )
SELECT @rootBankHierarchyDestination AS HierarchyId, a.Guid AS ElementGuid, c.ToDbEntityId
  FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Contractor.HierarchyContents c 
INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Contractor.Agents a ON c.ElementId = a.Id 
WHERE c.HierarchyId = @rootBankHierarchySource AND a.MycompanyId=-1

insert Contractor.HierarchyContents([DatabaseId], [StateId], [HierarchyId], [ElementId], [ToDbEntityId])
select a.[DatabaseId], 1 as StateId, @rootBankHierarchyDestination as HierarchyId, a.Id as ElementId, 3 as ToDbEntityId from @tblBankHierarchyContents c inner join Contractor.Agents a on a.Guid = c.ElementGuid
left join Contractor.HierarchyContents c2 ON c2.ElementId = a.Id and c2.HierarchyId = @rootBankHierarchyDestination
where c2.Id is null

GO

-- Сихронизация иерархии отделений банков...
DECLARE @rootBankHierarchySource INT
SELECT @rootBankHierarchySource = Id FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Hierarchy.Hierarchies h WHERE h.Code='SYSTEM_AGENT_BANKFILIALS' 

DECLARE @rootBankHierarchyDestination INT
SELECT @rootBankHierarchyDestination = Id FROM Hierarchy.Hierarchies h WHERE h.Code='SYSTEM_AGENT_BANKFILIALS'

declare @tblBankHierarchyContents table(HierarchyId int, ElementGuid uniqueidentifier, ToDbEntityId int)

insert @tblBankHierarchyContents (HierarchyId , ElementGuid , ToDbEntityId )
SELECT @rootBankHierarchyDestination AS HierarchyId, a.Guid AS ElementGuid, c.ToDbEntityId
  FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Contractor.HierarchyContents c 
INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Contractor.Agents a ON c.ElementId = a.Id 
WHERE c.HierarchyId = @rootBankHierarchySource AND a.MycompanyId=-1

insert Contractor.HierarchyContents([DatabaseId], [StateId], [HierarchyId], [ElementId], [ToDbEntityId])
select a.[DatabaseId], 1 as StateId, @rootBankHierarchyDestination as HierarchyId, a.Id as ElementId, 3 as ToDbEntityId from @tblBankHierarchyContents c inner join Contractor.Agents a on a.Guid = c.ElementGuid
left join Contractor.HierarchyContents c2 ON c2.ElementId = a.Id and c2.HierarchyId = @rootBankHierarchyDestination
where c2.Id is null

GO

-- Сихронизация иерархии отделений банков...
DECLARE @rootBankHierarchySource INT
SELECT @rootBankHierarchySource = Id FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Hierarchy.Hierarchies h WHERE h.Code='SYSTEM_AGENT_BANKASSOCIATION' 

DECLARE @rootBankHierarchyDestination INT
SELECT @rootBankHierarchyDestination = Id FROM Hierarchy.Hierarchies h WHERE h.Code='SYSTEM_AGENT_BANKASSOCIATION'

declare @tblBankHierarchyContents table(HierarchyId int, ElementGuid uniqueidentifier, ToDbEntityId int)

insert @tblBankHierarchyContents (HierarchyId , ElementGuid , ToDbEntityId )
SELECT @rootBankHierarchyDestination AS HierarchyId, a.Guid AS ElementGuid, c.ToDbEntityId
  FROM [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Contractor.HierarchyContents c 
INNER JOIN [<Server_Name, sysname, Server_Name>].[<DB_NAME, sysname, DB_NAME>].Contractor.Agents a ON c.ElementId = a.Id 
WHERE c.HierarchyId = @rootBankHierarchySource AND a.MycompanyId=-1

insert Contractor.HierarchyContents([DatabaseId], [StateId], [HierarchyId], [ElementId], [ToDbEntityId])
select a.[DatabaseId], 1 as StateId, @rootBankHierarchyDestination as HierarchyId, a.Id as ElementId, 3 as ToDbEntityId from @tblBankHierarchyContents c inner join Contractor.Agents a on a.Guid = c.ElementGuid
left join Contractor.HierarchyContents c2 ON c2.ElementId = a.Id and c2.HierarchyId = @rootBankHierarchyDestination
where c2.Id is null
GO


PRINT 'Завершено...'
GO
