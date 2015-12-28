
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 201 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15579 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - код окпо
FROM [SRV-SALES-SQL01].Донецк7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL01].Донецк7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL01].Донецк7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents) --9843

-- Обновление полного наименования клиента
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL01].Донецк7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 201
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- Обновление кода ОКПО
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL01].Донецк7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 201
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16 




------------
-- Винница
------------

INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 221 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15575 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - код окпо
FROM [SRV-SALES-SQL10].Винница7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL10].Винница7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL10].Винница7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents) --9843

-- Обновление полного наименования клиента
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL10].Винница7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 221
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- Обновление кода ОКПО
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL10].Винница7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 221
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16 

----
--Симферопольский филиал ЧП "Украинский продукт" 15554
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 231 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15554 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - код окпо
FROM [SRV-SALES-SQL10].Симферополь7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL10].Симферополь7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL10].Симферополь7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- Обновление полного наименования клиента
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL10].Симферополь7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 231
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- Обновление кода ОКПО
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL10].Симферополь7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 231
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16 

----
--Лисичанский филиал ЧП "Украинский продукт" 15555
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 233 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15555 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - код окпо
FROM [SRV-SALES-SQL11].Лисичанск7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL11].Лисичанск7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL11].Лисичанск7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- Обновление полного наименования клиента
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL11].Лисичанск7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 233
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- Обновление кода ОКПО
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL11].Лисичанск7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 233
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16

----
--Юзовский филиал ЧП "Украинский продукт" 15556
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 234 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15556 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - код окпо
FROM [SRV-SALES-SQL11].Юзовка7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL11].Юзовка7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL11].Юзовка7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- Обновление полного наименования клиента
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL11].Юзовка7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 234
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- Обновление кода ОКПО
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL11].Юзовка7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 234
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16


----
--Мариупольский филиал ЧП "Украинский продукт" 15557
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 208 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15557 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - код окпо
FROM [SRV-SALES-SQL09].Мариуполь7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL09].Мариуполь7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL09].Мариуполь7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- Обновление полного наименования клиента
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL09].Мариуполь7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 208
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- Обновление кода ОКПО
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL09].Мариуполь7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 208
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16

----
--Краматорский филиал ЧП "Украинский продукт" 15558
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 204 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15558 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - код окпо
FROM [SRV-SALES-SQL06].Краматорск7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL06].Краматорск7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL06].Краматорск7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- Обновление полного наименования клиента
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL06].Краматорск7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 204
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- Обновление кода ОКПО
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL06].Краматорск7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 204
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16

----
--Киевский филиал ЧП "Украинский продукт" 15559
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 202 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15559 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - код окпо
FROM [SRV-SALES-SQL04].Киев7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL04].Киев7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL04].Киев7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- Обновление полного наименования клиента
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL04].Киев7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 202
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- Обновление кода ОКПО
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL04].Киев7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 202
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16


----
--Ялтинский филиал ЧП"Украинский продукт" 15560
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 212 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15560 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - код окпо
FROM [SRV-SALES-SQL06].Ялта7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL06].Ялта7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL06].Ялта7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- Обновление полного наименования клиента
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL06].Ялта7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 212
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- Обновление кода ОКПО
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL06].Ялта7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 212
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16

----
--Полтавский филиал ЧП "Украинский продукт" 15561
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 218 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15561 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - код окпо
FROM [SRV-SALES-SQL10].Полтава7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL10].Полтава7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL10].Полтава7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- Обновление полного наименования клиента
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL10].Полтава7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 218
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- Обновление кода ОКПО
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL10].Полтава7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 218
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16


----
--Сумской филиал ЧП  "Украинский продукт" 15562
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 224 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15562 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - код окпо
FROM [SRV-SALES-SQL10].Сумы7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL10].Сумы7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL10].Сумы7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- Обновление полного наименования клиента
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL10].Сумы7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 224
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- Обновление кода ОКПО
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL10].Сумы7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 224
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16

----
--Черкасский филиал ЧП "Украинский продукт" 15563
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 225 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15563 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - код окпо
FROM [SRV-SALES-SQL11].Черкассы7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL11].Черкассы7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL11].Черкассы7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- Обновление полного наименования клиента
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL11].Черкассы7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 225
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- Обновление кода ОКПО
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL11].Черкассы7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 225
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16

----
--Днепропетровский филиал ЧП "Украинский продукт" 15564
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 207 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15564 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - код окпо
FROM [SRV-SALES-SQL07].Днепропетровск7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL07].Днепропетровск7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL07].Днепропетровск7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- Обновление полного наименования клиента
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL07].Днепропетровск7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 207
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- Обновление кода ОКПО
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL07].Днепропетровск7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 207
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16

----
--Хмельницкий филиал ЧП "Украинский продукт" 15565
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 222 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15565 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - код окпо
FROM [SRV-SALES-SQL10].Хмельницкий7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL10].Хмельницкий7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL10].Хмельницкий7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- Обновление полного наименования клиента
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL10].Хмельницкий7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 222
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- Обновление кода ОКПО
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL10].Хмельницкий7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 222
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16


----
--Херсонский филиал ЧП "Украинский продукт" 15566
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 215 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15566 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - код окпо
FROM [SRV-SALES-SQL08].Херсон7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL08].Херсон7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL08].Херсон7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- Обновление полного наименования клиента
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL08].Херсон7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 215
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- Обновление кода ОКПО
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL08].Херсон7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 215
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16

----
--Кировоградский филиал ЧП "Украинский продукт" 15567
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 220 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15567 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - код окпо
FROM [SRV-SALES-SQL10].Кировоград7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL10].Кировоград7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL10].Кировоград7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- Обновление полного наименования клиента
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL10].Кировоград7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 220
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- Обновление кода ОКПО
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL10].Кировоград7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 220
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16


----
--Мелитопольский филиал ЧП "Украинский продукт" 15568
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 216 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15568 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - код окпо
FROM [SRV-SALES-SQL08].Мелитополь7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL08].Мелитополь7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL08].Мелитополь7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- Обновление полного наименования клиента
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL08].Мелитополь7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 216
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- Обновление кода ОКПО
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL08].Мелитополь7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 216
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16


----
--Красноармейский филиал ЧП "Украинский продукт" 15569
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 205 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15569 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - код окпо
FROM [SRV-SALES-SQL05].Красноармейск7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL05].Красноармейск7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL05].Красноармейск7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- Обновление полного наименования клиента
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL05].Красноармейск7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 205
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- Обновление кода ОКПО
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL05].Красноармейск7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 205
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16

----
--Харьковский филиал ЧП "Украинский продукт" 15570
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 203 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15570 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - код окпо
FROM [SRV-SALES-SQL05].Харьков7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL05].Харьков7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL05].Харьков7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- Обновление полного наименования клиента
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL05].Харьков7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 203
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- Обновление кода ОКПО
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL05].Харьков7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 203
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16

----
--Запорожский филиал ЧП "Украинский продукт" 15571
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 219 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15571 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - код окпо
FROM [SRV-SALES-SQL10].Запорожье7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL10].Запорожье7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL10].Запорожье7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- Обновление полного наименования клиента
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL10].Запорожье7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 219
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- Обновление кода ОКПО
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL10].Запорожье7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 219
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16

----
--Горловский филиал ЧП "Украинский продукт" 15572
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 209 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15572 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - код окпо
FROM [SRV-SALES-SQL02].Горловка7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL02].Горловка7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL02].Горловка7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- Обновление полного наименования клиента
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL02].Горловка7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 209
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- Обновление кода ОКПО
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL02].Горловка7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 209
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16


----
--Одесский филиал ЧП "Украинский продукт" 15573
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 213 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15573 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - код окпо
FROM [SRV-SALES-SQL09].Одесса7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL09].Одесса7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL09].Одесса7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- Обновление полного наименования клиента
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL09].Одесса7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 213
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- Обновление кода ОКПО
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL09].Одесса7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 213
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16


----
--Львовский филиал ЧП "Украинский продукт" 15574
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 223 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15574 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - код окпо
FROM [SRV-SALES-SQL10].Львов7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL10].Львов7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL10].Львов7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- Обновление полного наименования клиента
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL10].Львов7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 223
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- Обновление кода ОКПО
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL10].Львов7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 223
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16

----
--Луганский филиал ЧП "Украинский продукт" 15576
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 211 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15576 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - код окпо
FROM [SRV-SALES-SQL04].Луганск7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL04].Луганск7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL04].Луганск7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- Обновление полного наименования клиента
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL04].Луганск7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 211
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- Обновление кода ОКПО
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL04].Луганск7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 211
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16

----
--Криворожский филиал ЧП "Украинский продукт" 15577
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 214 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15577 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - код окпо
FROM [SRV-SALES-SQL07].КривойРог7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL07].КривойРог7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL07].КривойРог7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- Обновление полного наименования клиента
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM [SRV-SALES-SQL07].КривойРог7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 214
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- Обновление кода ОКПО
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM [SRV-SALES-SQL07].КривойРог7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 214
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16


----
--Николаевский филиал ЧП "Украинский продукт" 15578
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 232 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15578 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - код окпо
FROM [SRV-SALES-SQL11].Николаев7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL11].Николаев7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL11].Николаев7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)


-- Обновление полного наименования клиента
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM [SRV-SALES-SQL11].Николаев7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 232
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- Обновление кода ОКПО
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM [SRV-SALES-SQL11].Николаев7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 232
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16




INSERT INTO Contractor.Companies(Id)
SELECT a.Id FROM Contractor.Agents a LEFT JOIN Contractor.Companies c ON a.Id=c.Id
WHERE c.Id IS NULL
AND a.KindValue = 1