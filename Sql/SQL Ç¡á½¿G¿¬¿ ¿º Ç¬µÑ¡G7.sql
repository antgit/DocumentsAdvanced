/*
INSERT INTO Analitic.Analitics(Guid, DatabaseId, Flags, StateId, [Name], KindId)
SELECT m.MSC_GUID, 1, 0, 1, m.MSC_NAME, 262148 FROM  Донецк7.dbo.MISC m 
left JOIN Analitic.Analitics a ON a.Guid = m.MSC_GUID  
WHERE  (a.KindId=262148 OR a.KindId IS NULL) AND m.MSC_TYPE=1 and m.MSC_NO=16 AND a.Guid IS NULL
*/
/*
INSERT INTO Analitic.Analitics(Guid, DatabaseId, Flags, StateId, [Name], KindId)
SELECT g2.Grp_GUID, 1, 0, 1, g.GoodGroupName, 262146 FROM  
(SELECT MIN(Grp_ID) AS Grp_ID, GoodGroupName
  FROM  [Донецк7].[dbo].[uv_GoodsGroups] 
WHERE GoodGroupName IS NOT NULL
GROUP BY GoodGroupName) g INNER JOIN [Донецк7].[dbo].[uv_GoodsGroups] g2 ON g.Grp_ID = g2.Grp_ID
left JOIN Analitic.Analitics a ON a.Guid = g2.Grp_GUID  
WHERE  (a.KindId=262146 OR a.KindId IS NULL) AND a.Guid IS NULL
*/
/*
INSERT INTO Analitic.Analitics(Guid, DatabaseId, Flags, StateId, [Name], Code, KindId)
SELECT m.MSC_GUID, 1, 0, 1, m.MSC_NAME, m.MSC_TAG, 262145
  FROM  Донецк7.dbo.MISC m 
left JOIN Analitic.Analitics a ON a.Guid = m.MSC_GUID  
WHERE  (a.KindId=262145 OR a.KindId IS NULL) AND m.MSC_TYPE=1 and m.MSC_NO=23 AND a.Guid IS NULL
*/
/*
INSERT INTO Analitic.Analitics(Guid, DatabaseId, Flags, StateId, [Name], Code, KindId)
SELECT m.MSC_GUID, 1, 0, 1, m.MSC_NAME, m.MSC_TAG, 262145
  FROM  Донецк7.dbo.MISC m 
left JOIN Analitic.Analitics a ON a.Guid = m.MSC_GUID  
WHERE  (a.KindId=262145 OR a.KindId IS NULL) AND m.MSC_TYPE=1 and m.MSC_NO=24 AND a.Guid IS NULL
*/
/*
INSERT INTO Analitic.Analitics(Guid, DatabaseId, Flags, StateId, [Name], Code, KindId)
SELECT m.MSC_GUID, 1, 0, 1, m.MSC_NAME, m.MSC_TAG, 262145
  FROM  Донецк7.dbo.MISC m 
left JOIN Analitic.Analitics a ON a.Guid = m.MSC_GUID  
WHERE  (a.KindId=262145 OR a.KindId IS NULL) AND m.MSC_TYPE=1 and m.MSC_NO=40 AND a.Guid IS NULL
*/
/*
INSERT INTO Analitic.Analitics(Guid, DatabaseId, Flags, StateId, [Name], KindId)
SELECT m.MSC_GUID, 1, 0, 1, m.MSC_NAME, 262145 FROM  
(SELECT MSC_ID, MSC_GUID, MSC_NAME
  FROM  [srv-devel].[Донецк7].[dbo].MISC WHERE msc_no=13 AND MSC_TYPE<>-1) m
left JOIN Analitic.Analitics a ON a.Guid = m.MSC_GUID  
WHERE  (a.KindId=262145 OR a.KindId IS NULL) AND a.Guid IS NULL
*/
/*
INSERT INTO Analitic.Analitics(Guid, DatabaseId, Flags, StateId, [Name], KindId)
SELECT m.MSC_GUID, 1, 0, 1, m.MSC_NAME, 262145 FROM  
(SELECT MSC_ID, MSC_GUID, MSC_NAME
  FROM  [srv-devel].[Донецк7].[dbo].MISC WHERE msc_no=18 AND MSC_TYPE<>-1) m
left JOIN Analitic.Analitics a ON a.Guid = m.MSC_GUID  
WHERE  (a.KindId=262145 OR a.KindId IS NULL) AND a.Guid IS NULL
*/