EXEC ('SELECT TOP 10 * FROM ������7.dbo.BIND_DOCS') AT [SRV-SALES-SQL01];

EXEC ('SELECT TOP 10 * FROM ������7.dbo.BIND_DOCS;
SELECT TOP 100 * FROM ������7.dbo.BIND_DOCS;') AT [SRV-SALES-SQL01];

EXEC ('SELECT TOP 10 * FROM ������7.dbo.Documents 
WHERE DOC_DONE = ? AND DOC_DATE < ?', 2, '20100202') AT [SRV-SALES-SQL01];