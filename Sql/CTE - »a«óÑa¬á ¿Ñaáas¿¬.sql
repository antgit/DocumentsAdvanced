
;WITH e AS
    (
      SELECT Id, ParentId, -1 AS Lv, cast((RIGHT(REPLICATE('0', 10) + CONVERT(VARCHAR, Id),10) + '.') AS NVARCHAR(max)) AS P FROM Hierarchy.Hierarchies WHERE ParentId IS null
      UNION ALL
      SELECT et.Id, et.ParentId, e.Lv + 1 AS Lv, e.P + cast((RIGHT(REPLICATE('0', 10) + CONVERT(VARCHAR, et.Id),10) + '.') AS NVARCHAR(max)) AS P FROM Hierarchy.Hierarchies et inner JOIN e ON et.ParentId= e.Id
    )
    SELECT e.p, h.[Path], e.lv, *
      FROM e INNER JOIN Hierarchy.Hierarchies h ON e.Id=h.Id WHERE e.P<>h.[Path]

;WITH e AS
    (
      SELECT Id, ParentId, -1 AS Lv, cast((RIGHT(REPLICATE('0', 10) + CONVERT(VARCHAR, Id),10) + '.') AS NVARCHAR(max)) AS P FROM Hierarchy.Hierarchies WHERE ParentId IS null
      UNION ALL
      SELECT et.Id, et.ParentId, e.Lv + 1 AS Lv, e.P + cast((RIGHT(REPLICATE('0', 10) + CONVERT(VARCHAR, et.Id),10) + '.') AS NVARCHAR(max)) AS P FROM Hierarchy.Hierarchies et inner JOIN e ON et.ParentId= e.Id
    )
    UPDATE Hierarchy.Hierarchies SET [Path] = e.P
    FROM Hierarchy.Hierarchies AS h
         JOIN e ON e.Id = H.Id AND e.P<>h.Path
--------------------------------------------------------   
SELECT h.Id, h.HasContents, 
isnull(a.HasContent,0),
CASE WHEN isnull(a.HasContent,0)>0 AND h.HasContents=1 THEN 1
     WHEN isnull(a.HasContent,0)=0 AND h.HasContents=0 THEN 0
     ELSE -1 end
  FROM Hierarchy.Hierarchies h LEFT JOIN
(SELECT distinct HierarchyId, COUNT(*) AS HasContent  FROM Hierarchy.HierarchyContents GROUP BY HierarchyId) a
ON h.Id=a.HierarchyId
WHERE 
CASE WHEN isnull(a.HasContent,0)>0 AND h.HasContents=1 THEN 1
     WHEN isnull(a.HasContent,0)=0 AND h.HasContents=0 THEN 0
     ELSE -1 END =-1

update Hierarchy.Hierarchies SET HasContents =
case when isnull(a.HasContent,0)>0 THEN 1
     when isnull(a.HasContent,0)=0 THEN 0 end
  FROM Hierarchy.Hierarchies h LEFT JOIN
(SELECT distinct HierarchyId, COUNT(*) AS HasContent  FROM Hierarchy.HierarchyContents GROUP BY HierarchyId) a
ON h.Id=a.HierarchyId
WHERE 
CASE WHEN isnull(a.HasContent,0)>0 AND h.HasContents=1 THEN 1
     WHEN isnull(a.HasContent,0)=0 AND h.HasContents=0 THEN 0
     ELSE -1 END =-1

-------------------------------

;WITH e AS
    (
      SELECT ENT_ID, P0, -1 AS Lv  FROM dbo.ENTITIES m INNER JOIN ENT_tree t on m.ENT_ID=t.ID WHERE P0=0
      UNION ALL
      SELECT m1.ENT_ID, t1.P0, e.Lv + 1 AS Lv FROM dbo.ENTITIES m1 INNER JOIN ENT_tree t1
      on m1.ENT_ID=t1.ID
      inner JOIN e ON t1.P0= e.ENT_ID
    )
    SELECT  e.lv, m.ENT_NAME
      FROM e INNER JOIN ENTITIES m ON e.ENT_ID=m.ENT_ID ORDER BY lv
----------------------------

;WITH e AS
    (
      SELECT MSC_ID, P0, -1 AS Lv  FROM dbo.MISC m INNER JOIN MISC_tree t on m.MSC_ID=t.ID WHERE P0=0
      UNION ALL
      SELECT m1.MSC_ID, t1.P0, e.Lv + 1 AS Lv FROM dbo.MISC m1 INNER JOIN MISC_tree t1
      on m1.MSC_ID=t1.ID
      inner JOIN e ON t1.P0= e.MSC_ID
    )
    SELECT  e.lv, m.MSC_NAME
      FROM e INNER JOIN MISC m ON e.MSC_ID=m.MSC_ID ORDER BY lv