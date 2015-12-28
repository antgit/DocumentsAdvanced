-- включить расширенный режим
EXEC sp_configure 'show advanced options', 1
GO
RECONFIGURE
GO
-- разрешить xp_cmdshell
EXEC sp_configure 'xp_cmdshell', 1
GO
RECONFIGURE
GO
-- разрешить clr
EXEC sp_configure 'clr enabled', 1
GO
RECONFIGURE
GO
-- Активация FILESTREAM
EXEC sp_configure filestream_access_level, 2;
GO
RECONFIGURE;
GO
SELECT * FROM sys.configurations ORDER BY name ;