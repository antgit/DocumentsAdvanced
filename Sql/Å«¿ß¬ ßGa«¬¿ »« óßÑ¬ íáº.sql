/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.6.10
 * Time: 11.11.2009 1:57:57
 ************************************************************/

/*
* CATEGORY: Script
* AUTHOR: Luiz Barros
* OBJECTIVE: Find and Replace a string in all string fields (char, varchar, etc) of all tables in the database 
*
* PARAMETERS:
* @SearchChar is the string to be found. Use wildcard % 
* @ReplaceChar is the string to replace occurrences of @SearchChar
* @Replace=0 => search for @SearchChar; @Replace=1 => Find and replace occurrences
*/


SET NOCOUNT ON
DECLARE @SearchChar   VARCHAR(8000),
        @ReplaceChar  VARCHAR(8000),
        @SearchChar1  VARCHAR(8000),
        @Replace      BIT

SET @Replace = 0 -- 0 => only find; 1 => replace
SET @SearchChar = '%колонок%' -- Like 'A%', '%A' or '%A%'
SET @ReplaceChar = 'REPLACE BY THIS STRING' -- don't use wildcards here

IF @Replace = 1
   AND (@SearchChar IS NULL OR @ReplaceChar IS NULL)
BEGIN
    PRINT 'Invalid Parameters' RETURN
END

SET @SearchChar1 = REPLACE(@SearchChar, '%', '')

DECLARE  @sql  VARCHAR(8000),
         @ColumnName VARCHAR(100),
         @TableName VARCHAR(100) 

CREATE TABLE #T
(
	 TableName  VARCHAR(100),
	 FieldName  VARCHAR(100),
	 VALUE  VARCHAR(MAX)
)
 

declare db cursor for 
SELECT '[' + s.NAME + '].[' + b.Name + ']' as TableName,
c.Name as ColumnName
FROM sys.objects b, syscolumns c, sys.schemas s
WHERE C.id = b.OBJECT_ID --b.id 
and b.type='u' 
AND c.xType IN (35, 99, 167, 175, 231, 239) -- string types
AND s.SCHEMA_ID = b.schema_id
order BY b.name 

OPEN db
FETCH NEXT FROM db INTO @TableName, @ColumnName                       
WHILE @@FETCH_STATUS = 0
BEGIN
     
    IF @Replace = 0 
         
        SET @sql = 'INSERT #T SELECT ''' + @TableName + ''', ''' + @ColumnName + 
            ''', [' + @ColumnName + '] FROM ' + @TableName + ' WHERE [' + @ColumnName
            + '] LIKE ''' + @SearchChar + ''''
             
    ELSE
          SET @sql = 'UPDATE ' + @TableName + ' SET [' + @ColumnName + 
                    '] = REPLACE(convert(varchar(max),[' + @ColumnName + ']),'''
                    + @SearchChar1 + ''',''' + @ReplaceChar + ''') WHERE [' + @ColumnName
                    + '] LIKE ''' + @SearchChar + ''''
                     
    
    EXEC (@sql)
          
    
    PRINT @TableName + ' - ' + @ColumnName
     FETCH NEXT FROM db INTO @TableName, @ColumnName
END

IF @Replace = 0
    SELECT *
    FROM   #T
    ORDER BY
           TableName

DROP TABLE #T
CLOSE db
DEALLOCATE db





