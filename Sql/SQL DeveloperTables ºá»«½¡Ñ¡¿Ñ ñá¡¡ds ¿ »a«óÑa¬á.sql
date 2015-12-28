INSERT INTO Developer.Tables(DatabaseId,Flags,StateId,TableSchema,TableName,Memo)
SELECT 1,0,1,a.TABLE_SCHEMA,a.TABLE_NAME,'' FROM INFORMATION_SCHEMA.TABLES a
LEFT JOIN Developer.Tables b
ON a.TABLE_SCHEMA = b.TableSchema AND a.TABLE_NAME=b.TableName
WHERE b.Id IS NULL
      AND a.TABLE_TYPE = 'BASE TABLE' AND a.TABLE_NAME<>'sysdiagrams'
GO
SELECT * FROM Developer.Tables b
LEFT JOIN INFORMATION_SCHEMA.TABLES a ON a.TABLE_SCHEMA = b.TableSchema AND a.TABLE_NAME=b.TableName
WHERE a.TABLE_NAME IS NULL -- AND a.TABLE_NAME<>'sysdiagrams'

SELECT * FROM INFORMATION_SCHEMA.TABLES a
LEFT JOIN Developer.Tables b ON a.TABLE_SCHEMA = b.TableSchema AND a.TABLE_NAME=b.TableName
WHERE b.TableName IS NULL 

SELECT * FROM 
(SELECT t.TableSchema, t.TableName, c.ColumnName, c.OrderNo
   FROM Developer.Tables t INNER JOIN Developer.TableColumns c ON t.Id=c.OwnId) a  
LEFT JOIN INFORMATION_SCHEMA.[COLUMNS] dc ON a.TableSchema=dc.TABLE_SCHEMA AND a.TableName = dc.TABLE_NAME AND a.ColumnName = dc.COLUMN_NAME 
WHERE a.ColumnName IS NULL OR dc.COLUMN_NAME IS null 


SELECT * FROM 
INFORMATION_SCHEMA.[COLUMNS] dc
LEFT JOIN 
(SELECT t.TableSchema, t.TableName, c.ColumnName, c.OrderNo
   FROM Developer.Tables t INNER JOIN Developer.TableColumns c ON t.Id=c.OwnId) a 
ON a.TableSchema=dc.TABLE_SCHEMA AND a.TableName = dc.TABLE_NAME AND a.ColumnName = dc.COLUMN_NAME
WHERE a.ColumnName IS NULL OR dc.COLUMN_NAME IS null 

-- 
INSERT INTO Developer.TableColumns(OwnId,DatabaseId,Flags,StateId,GroupNo,OrderNo,ColumnName,Memo,TypeNameSql)
SELECT 1428,1,8,1,0, dc.ORDINAL_POSITION,dc.COLUMN_NAME,'',dc.DATA_TYPE FROM 
INFORMATION_SCHEMA.[COLUMNS] dc
LEFT JOIN 
(SELECT t.TableSchema, t.TableName, c.ColumnName, c.OrderNo
   FROM Developer.Tables t INNER JOIN Developer.TableColumns c ON t.Id=c.OwnId) a 
ON a.TableSchema=dc.TABLE_SCHEMA AND a.TableName = dc.TABLE_NAME AND a.ColumnName = dc.COLUMN_NAME
WHERE a.ColumnName IS NULL 
AND dc.TABLE_NAME = 'AnaliticDocumentDetails'