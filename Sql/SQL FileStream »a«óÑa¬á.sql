--
-- Проверка правильности работы базы с FileStream
--

SELECT * FROM   sys.columns
WHERE  OBJECT_ID = OBJECT_ID('FileData.Files', 'table')
       AND system_type_id = TYPE_ID('varbinary')
       AND max_length = -1
       AND is_filestream = 1
       
       
SELECT * FROM   sys.columns
WHERE  OBJECT_ID = OBJECT_ID('FileData.FileVersions', 'table')
       AND system_type_id = TYPE_ID('varbinary')
       AND max_length = -1
       AND is_filestream = 1


select * from sys.columns where 
       (object_id = object_id('FileData.Files', 'table') OR object_id = object_id('FileData.FileVersions', 'table')) 
        and system_type_id = type_id('varbinary') and max_length = -1 and is_filestream = 1

select serverproperty ('FilestreamShareName') 
       ,serverproperty ('FilestreamConfiguredLevel') 
       ,serverproperty ('FilestreamEffectiveLevel')
--EXEC sp_filestream_force_garbage_collection @dbname = N'Documents2011DMPZ';

-- Ознакомится и запомнить
-- http://blogs.msdn.com/b/alexejs/archive/2009/06/03/filestream-2.aspx
-- http://msdn.microsoft.com/en-us/library/cc949109(SQL.100).aspx
-- http://www.mssqltips.com/tip.asp?tip=1854