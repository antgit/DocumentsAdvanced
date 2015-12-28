
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 201 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15579 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - ��� ����
FROM [SRV-SALES-SQL01].������7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL01].������7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL01].������7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents) --9843

-- ���������� ������� ������������ �������
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL01].������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 201
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- ���������� ���� ����
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL01].������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 201
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16 




------------
-- �������
------------

INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 221 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15575 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - ��� ����
FROM [SRV-SALES-SQL10].�������7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL10].�������7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL10].�������7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents) --9843

-- ���������� ������� ������������ �������
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL10].�������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 221
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- ���������� ���� ����
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL10].�������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 221
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16 

----
--��������������� ������ �� "���������� �������" 15554
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 231 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15554 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - ��� ����
FROM [SRV-SALES-SQL10].�����������7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL10].�����������7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL10].�����������7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- ���������� ������� ������������ �������
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL10].�����������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 231
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- ���������� ���� ����
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL10].�����������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 231
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16 

----
--����������� ������ �� "���������� �������" 15555
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 233 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15555 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - ��� ����
FROM [SRV-SALES-SQL11].���������7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL11].���������7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL11].���������7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- ���������� ������� ������������ �������
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL11].���������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 233
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- ���������� ���� ����
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL11].���������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 233
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16

----
--�������� ������ �� "���������� �������" 15556
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 234 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15556 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - ��� ����
FROM [SRV-SALES-SQL11].������7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL11].������7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL11].������7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- ���������� ������� ������������ �������
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL11].������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 234
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- ���������� ���� ����
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL11].������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 234
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16


----
--������������� ������ �� "���������� �������" 15557
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 208 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15557 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - ��� ����
FROM [SRV-SALES-SQL09].���������7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL09].���������7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL09].���������7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- ���������� ������� ������������ �������
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL09].���������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 208
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- ���������� ���� ����
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL09].���������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 208
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16

----
--������������ ������ �� "���������� �������" 15558
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 204 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15558 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - ��� ����
FROM [SRV-SALES-SQL06].����������7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL06].����������7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL06].����������7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- ���������� ������� ������������ �������
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL06].����������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 204
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- ���������� ���� ����
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL06].����������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 204
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16

----
--�������� ������ �� "���������� �������" 15559
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 202 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15559 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - ��� ����
FROM [SRV-SALES-SQL04].����7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL04].����7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL04].����7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- ���������� ������� ������������ �������
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL04].����7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 202
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- ���������� ���� ����
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL04].����7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 202
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16


----
--��������� ������ ��"���������� �������" 15560
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 212 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15560 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - ��� ����
FROM [SRV-SALES-SQL06].����7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL06].����7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL06].����7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- ���������� ������� ������������ �������
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL06].����7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 212
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- ���������� ���� ����
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL06].����7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 212
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16

----
--���������� ������ �� "���������� �������" 15561
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 218 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15561 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - ��� ����
FROM [SRV-SALES-SQL10].�������7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL10].�������7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL10].�������7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- ���������� ������� ������������ �������
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL10].�������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 218
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- ���������� ���� ����
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL10].�������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 218
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16


----
--������� ������ ��  "���������� �������" 15562
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 224 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15562 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - ��� ����
FROM [SRV-SALES-SQL10].����7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL10].����7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL10].����7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- ���������� ������� ������������ �������
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL10].����7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 224
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- ���������� ���� ����
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL10].����7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 224
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16

----
--���������� ������ �� "���������� �������" 15563
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 225 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15563 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - ��� ����
FROM [SRV-SALES-SQL11].��������7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL11].��������7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL11].��������7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- ���������� ������� ������������ �������
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL11].��������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 225
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- ���������� ���� ����
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL11].��������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 225
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16

----
--���������������� ������ �� "���������� �������" 15564
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 207 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15564 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - ��� ����
FROM [SRV-SALES-SQL07].��������������7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL07].��������������7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL07].��������������7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- ���������� ������� ������������ �������
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL07].��������������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 207
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- ���������� ���� ����
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL07].��������������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 207
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16

----
--����������� ������ �� "���������� �������" 15565
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 222 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15565 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - ��� ����
FROM [SRV-SALES-SQL10].�����������7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL10].�����������7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL10].�����������7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- ���������� ������� ������������ �������
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL10].�����������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 222
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- ���������� ���� ����
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL10].�����������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 222
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16


----
--���������� ������ �� "���������� �������" 15566
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 215 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15566 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - ��� ����
FROM [SRV-SALES-SQL08].������7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL08].������7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL08].������7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- ���������� ������� ������������ �������
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL08].������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 215
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- ���������� ���� ����
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL08].������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 215
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16

----
--�������������� ������ �� "���������� �������" 15567
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 220 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15567 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - ��� ����
FROM [SRV-SALES-SQL10].����������7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL10].����������7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL10].����������7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- ���������� ������� ������������ �������
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL10].����������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 220
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- ���������� ���� ����
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL10].����������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 220
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16


----
--�������������� ������ �� "���������� �������" 15568
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 216 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15568 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - ��� ����
FROM [SRV-SALES-SQL08].����������7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL08].����������7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL08].����������7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- ���������� ������� ������������ �������
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL08].����������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 216
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- ���������� ���� ����
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL08].����������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 216
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16


----
--��������������� ������ �� "���������� �������" 15569
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 205 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15569 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - ��� ����
FROM [SRV-SALES-SQL05].�������������7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL05].�������������7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL05].�������������7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- ���������� ������� ������������ �������
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL05].�������������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 205
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- ���������� ���� ����
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL05].�������������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 205
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16

----
--����������� ������ �� "���������� �������" 15570
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 203 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15570 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - ��� ����
FROM [SRV-SALES-SQL05].�������7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL05].�������7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL05].�������7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- ���������� ������� ������������ �������
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL05].�������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 203
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- ���������� ���� ����
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL05].�������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 203
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16

----
--����������� ������ �� "���������� �������" 15571
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 219 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15571 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - ��� ����
FROM [SRV-SALES-SQL10].���������7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL10].���������7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL10].���������7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- ���������� ������� ������������ �������
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL10].���������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 219
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- ���������� ���� ����
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL10].���������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 219
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16

----
--���������� ������ �� "���������� �������" 15572
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 209 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15572 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - ��� ����
FROM [SRV-SALES-SQL02].��������7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL02].��������7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL02].��������7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- ���������� ������� ������������ �������
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL02].��������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 209
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- ���������� ���� ����
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL02].��������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 209
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16


----
--�������� ������ �� "���������� �������" 15573
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 213 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15573 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - ��� ����
FROM [SRV-SALES-SQL09].������7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL09].������7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL09].������7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- ���������� ������� ������������ �������
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL09].������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 213
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- ���������� ���� ����
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL09].������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 213
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16


----
--��������� ������ �� "���������� �������" 15574
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 223 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15574 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - ��� ����
FROM [SRV-SALES-SQL10].�����7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL10].�����7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL10].�����7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- ���������� ������� ������������ �������
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL10].�����7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 223
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- ���������� ���� ����
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL10].�����7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 223
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16

----
--��������� ������ �� "���������� �������" 15576
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 211 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15576 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - ��� ����
FROM [SRV-SALES-SQL04].�������7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL04].�������7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL04].�������7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- ���������� ������� ������������ �������
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM  [SRV-SALES-SQL04].�������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 211
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- ���������� ���� ����
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM  [SRV-SALES-SQL04].�������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 211
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16

----
--������������ ������ �� "���������� �������" 15577
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 214 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15577 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - ��� ����
FROM [SRV-SALES-SQL07].���������7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL07].���������7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL07].���������7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)

-- ���������� ������� ������������ �������
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM [SRV-SALES-SQL07].���������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 214
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- ���������� ���� ����
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM [SRV-SALES-SQL07].���������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
INNER JOIN Contractor.Companies c ON d.Id=c.Id
WHERE d.DatabaseId = 214
AND d.StateId<>1
AND s.AG_CODE IS not null	
AND s.AG_CODE<> isnull(c.Okpo,'')
and len(s.AG_CODE)<=16


----
--������������ ������ �� "���������� �������" 15578
---
INSERT Contractor.Agents(Guid, DatabaseId, DbSourceId, StateId, KindId, [Name], MyCompanyId, AddressPhysical, AddressLegal, Phone, CodeTax)
SELECT 
AG_GUID AS Guid, 232 AS DatabaseId, AG_ID AS DbSourceId, 2 AS StateId, 196609 AS KindId, ag_Name AS [Name], 15578 AS MyCompanyId,AG_WWW AS AddressPhysical, AG_ADDRESS AS AddressLegal,
AG_PHONE AS Phone, AG_VATNO AS CodeTax
--AG_CODE - ��� ����
FROM [SRV-SALES-SQL11].��������7.dbo.Agents
WHERE AG_ID IN
(
	SELECT J_AG1 AS JAG FROM [SRV-SALES-SQL11].��������7.dbo.JOURNAL
	UNION ALL
	SELECT J_AG2 AS JAG FROM [SRV-SALES-SQL11].��������7.dbo.JOURNAL
)
AND AG_TYPE=1
AND AG_Guid NOT IN (SELECT Guid FROM Contractor.Agents)


-- ���������� ������� ������������ �������
UPDATE Contractor.Agents
SET NameFull = AG_CONTACT
FROM [SRV-SALES-SQL11].��������7.dbo.Agents s
INNER JOIN Contractor.Agents d ON s.AG_ID=d.DbSourceId
WHERE d.DatabaseId = 232
AND d.StateId<>1
AND s.AG_CONTACT IS not null	
AND s.AG_CONTACT<> isnull(d.NameFull,'')

-- ���������� ���� ����
UPDATE Contractor.Companies
SET Okpo = AG_CODE
FROM [SRV-SALES-SQL11].��������7.dbo.Agents s
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