DECLARE @tblAgents TABLE (SourceId int,Guid UNIQUEIDENTIFIER, Name NVARCHAR(255), KindId INT)
INSERT @tblAgents(SourceId,Guid, [Name], KindId)
SELECT a.AG_ID, a.AG_GUID AS Guid, a.AG_NAME AS Name, 
CASE WHEN a.AG_TYPE=5 THEN 196612
     WHEN a.AG_TYPE=3 THEN 196610
	 WHEN a.AG_TYPE = 1 OR a.AG_TYPE=2 OR a.AG_TYPE=4 THEN 196609 END AS KindId
FROM Донецк7.dbo.AGENTS a WHERE a.AG_TYPE<>0

SELECT * FROM @tblAgents WHERE KindId IS null

INSERT INTO Contractor.Agents(Guid, DatabaseId, DbSourceId,Flags, StateId, [Name],KindId)
SELECT a.Guid,1,a.SourceId,0,1,a.[Name], a.KindId FROM @tblAgents a
LEFT JOIN Contractor.Agents b ON a.Guid = b.Guid
WHERE b.Guid IS NULL
