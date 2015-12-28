print '�������'
if not exists (select * from [Core].[DbEntities] where [Guid]='E2421837-7489-DD11-A479-000C6E4EC13D')
INSERT INTO [Core].[DbEntities]([Id],[Guid],[Name],[Code])
VALUES(28,'E2421837-7489-DD11-A479-000C6E4EC13D',N'��������', 
N'BusinessObjects.Hierarchy, BusinessObjects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null')
GO
delete from Core.DbEntityMethods where DbEntityId = 28;
insert [Core].[DbEntityMethods]([Guid],[DbEntityId],[Name],[Method], [Schema], [ProcedureName])
select a.[Guid],a.[DbEntityId],a.[Name],a.[Method], a.[Schema], a.[ProcedureName] from
(select 'E3421837-7489-DD11-A479-000C6E4EC13D' as [Guid], 28 as [DbEntityId],'��������  �� ��������������' as [Name],'Load' as [Method], 'Hierarchy' as [Schema],  'HierarchyLoad' as [ProcedureName]
union 
select 'E4421837-7489-DD11-A479-000C6E4EC13D', 28, '��� ������','LoadAll','Hierarchy','HierarchyLoadAll'
union 
select 'E5421837-7489-DD11-A479-000C6E4EC13D', 28, '��������','Delete','Hierarchy','HierarchyDelete'
union 
select 'E6421837-7489-DD11-A479-000C6E4EC13D', 28, '��������','Create','Hierarchy','HierarchyInsert'
union 
select 'E7421837-7489-DD11-A479-000C6E4EC13D', 28, '����������','Update','Hierarchy','HierarchyUpdate'
union 
select 'E8421837-7489-DD11-A479-000C6E4EC13D', 28, '�������� �� ��������������','ExistsId','Hierarchy','HierarchyExistsId'
union 
select 'E9421837-7489-DD11-A479-000C6E4EC13D', 28, '�������� �� ����������� ��������������','ExistsGuid','Hierarchy','HierarchyExistsGuid'
union 
select 'EA421837-7489-DD11-A479-000C6E4EC13D', 28, '�������� �� ����������� ��������������','FindByName','Hierarchy','HierarchiesFindByName'
union 
select 'EB421837-7489-DD11-A479-000C6E4EC13D', 28, '�������','LoadTemplates','Hierarchy','AgentsLoadTemplates'
union 
select 'EC421837-7489-DD11-A479-000C6E4EC13D', 28, '��������� ���������','ChangeState','Hierarchy','HierarchyChangeState'
union 
select 'ED421837-7489-DD11-A479-000C6E4EC13D', 28, '�����','LinkLoadSource','Hierarchy','AgentChainsLoadBySource'
union 
select 'EE421837-7489-DD11-A479-000C6E4EC13D', 28, '�����','LinkDelete','Hierarchy','AgentChainDelete'
union 
select 'EF421837-7489-DD11-A479-000C6E4EC13D', 28, '�����','LinkInsert','Hierarchy','AgentChainInsert'
union 
select 'F0421837-7489-DD11-A479-000C6E4EC13D', 28, '�����','LinkUpdate','Hierarchy','AgentChainUpdate'
) a
left join [Core].[DbEntityMethods] b on a.Guid=b.Guid where b.Guid is null
GO
--insert Core.LinkKinds(Id,Guid,StateId,DbEntityId,Code,Name,BranchId)
--select b.Id,b.Guid,b.StateId,b.DbEntityId,b.Code,b.Name,b.BranchId
--from 
--(
--select 0 as Id, '32421837-7489-DD11-A479-000C6E4EC13D' as Guid, 1 as StateId, 3 as DbEntityId, 'Tree' as Code, '�������� �����������' as Name, 1 as BranchId
--union select 1, '33421837-7489-DD11-A479-000C6E4EC13D',1,3,'Workers','���� ����������',1
--) b
--left join Core.LinkKinds a
--on a.Guid = b.Guid where a.Guid is null
--GO
--insert Agent.Agents(Guid, Name, StateId, DbEntityKindId,Flags,BranchId)
--select a.Guid, a.Name, a.StateId, a.DbEntityKindId,a.Flags,a.BranchId from
--(select '5CCADC5E-4800-DD11-8029-000C6E4EC13D' as [Guid], '����� ���������' as Name , 1 as StateId, 196610 as DbEntityKindId, 1 as Flags, 1 as BranchId 
--union select '5DCADC5E-4800-DD11-8029-000C6E4EC13D', '����� �����������', 1, 196609,1, 1
--union select '5ECADC5E-4800-DD11-8029-000C6E4EC13D', '��� �����������', 1, 196612,1, 1
--union select 'F1FF5B09-C379-DD11-80D9-8000600FE800', '����� ����', 1, 196616,1, 1
--union select 'E19AEE33-1971-426B-A037-2DA00631CEDD', '�����', 1, 196624,1, 1
--) a
--left join [Agent].[Agents] b on a.Guid=b.Guid where b.Guid is null
--GO

print '������ �������'
if not exists (select * from [Core].[DbEntities] where [Guid]='F3421837-7489-DD11-A479-000C6E4EC13D')
INSERT INTO [Core].[DbEntities]([Id],[Guid],[Name],[Code])
VALUES(29,'F3421837-7489-DD11-A479-000C6E4EC13D',N'������ �������', 
N'BusinessObjects.HierarchyContent, BusinessObjects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null')
GO
delete from Core.DbEntityMethods where DbEntityId = 29;
insert [Core].[DbEntityMethods]([Guid],[DbEntityId],[Name],[Method], [Schema], [ProcedureName])
select a.[Guid],a.[DbEntityId],a.[Name],a.[Method], a.[Schema], a.[ProcedureName] from
(select 'F4421837-7489-DD11-A479-000C6E4EC13D' as [Guid], 29 as [DbEntityId],'��������  �� ��������������' as [Name],'Load' as [Method], 'Hierarchy' as [Schema],  'HierarchyContentLoad' as [ProcedureName]
union 
select 'F5421837-7489-DD11-A479-000C6E4EC13D', 29, '��� ������','LoadAll','Hierarchy','HierarchyContentLoadAll'
union 
select 'F6421837-7489-DD11-A479-000C6E4EC13D', 29, '��������','Delete','Hierarchy','HierarchyContentDelete'
union 
select 'F7421837-7489-DD11-A479-000C6E4EC13D', 29, '��������','Create','Hierarchy','HierarchyContentInsert'
union 
select 'F8421837-7489-DD11-A479-000C6E4EC13D', 29, '����������','Update','Hierarchy','HierarchyContentUpdate'
union 
select 'F9421837-7489-DD11-A479-000C6E4EC13D', 29, '�������� �� ��������������','ExistsId','Hierarchy','HierarchyContentExistsId'
union 
select 'FA421837-7489-DD11-A479-000C6E4EC13D', 29, '�������� �� ����������� ��������������','ExistsGuid','Hierarchy','HierarchyContentExistsGuid'
union 
select 'FD421837-7489-DD11-A479-000C6E4EC13D', 29, '��������� ���������','ChangeState','Hierarchy','HierarchyContentChangeState'
) a
left join [Core].[DbEntityMethods] b on a.Guid=b.Guid where b.Guid is null
GO

print '��������������'
if not exists (select * from [Core].[DbEntities] where [Guid]='2E69E8C3-D8E9-4851-9B4F-AD8C96D5FA1E')
INSERT INTO [Core].[DbEntities]([Id],[Guid],[Name],[Code])
VALUES(3,'2E69E8C3-D8E9-4851-9B4F-AD8C96D5FA1E',N'�������������', 
N'BusinessObjects.Agent, BusinessObjects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null')
GO
insert [Core].[DbEntityKinds]([Id],[Guid],[Name],[DbEntityId],[SubKind])
select a.[Id],a.[Guid],a.[Name],a.[DbEntityId],a.[SubKind] from
(select 196609 as Id,'3B7E72F0-F46E-DD11-9DD1-000C6E4EC13D' as Guid,'�����������' as Name,	3 as DbEntityId, 1 as SubKind
union 
select 196610,'3C7E72F0-F46E-DD11-9DD1-000C6E4EC13D','��� ����',3,2
union 
select 196612,'3D7E72F0-F46E-DD11-9DD1-000C6E4EC13D','��� �����������',3,4
union 
select 196616,'1EC5F42B-9F79-DD11-80D9-0018F30641D3','����',3,8
union 
select 196624,'0C8DAA1D-D38C-DD11-80D9-000C6E4EC13D','�����',3,16
union 
select 196640,'5ABB7152-D38C-DD11-80D9-000C6E4EC13D','�����������-������������� ����',3,32
) a
left join [Core].[DbEntityKinds] b on a.Id=b.Id where b.Id is null
GO
delete from Core.DbEntityMethods where DbEntityId = 3;
insert [Core].[DbEntityMethods]([Guid],[DbEntityId],[Name],[Method], [Schema], [ProcedureName])
select a.[Guid],a.[DbEntityId],a.[Name],a.[Method], a.[Schema], a.[ProcedureName] from
(select 'C45A1DF4-5F86-DD11-A0B1-000C6E4EC13D' as [Guid], 3 as [DbEntityId],'��������  �� ��������������' as [Name],'Load' as [Method], 'Contractor' as [Schema],  'AgentLoad' as [ProcedureName]
union 
select 'C55A1DF4-5F86-DD11-A0B1-000C6E4EC13D', 3, '��� ������','LoadAll','Contractor','AgentsLoadAll'
union 
select 'C65A1DF4-5F86-DD11-A0B1-000C6E4EC13D', 3, '��������','Delete','Contractor','AgentDelete'
union 
select 'C75A1DF4-5F86-DD11-A0B1-000C6E4EC13D', 3, '��������','Create','Contractor','AgentInsert'
union 
select 'C85A1DF4-5F86-DD11-A0B1-000C6E4EC13D', 3, '����������','Update','Contractor','AgentUpdate'
union 
select 'C95A1DF4-5F86-DD11-A0B1-000C6E4EC13D', 3, '�������� �� ��������������','ExistsId','Contractor','AgentExistsId'
union 
select 'CA5A1DF4-5F86-DD11-A0B1-000C6E4EC13D', 3, '�������� �� ����������� ��������������','ExistsGuid','Contractor','AgentExistsGuid'
union 
select 'CB5A1DF4-5F86-DD11-A0B1-000C6E4EC13D', 3, '�������� �� ����������� ��������������','FindByName','Contractor','AgentsFindByName'
union 
select 'CC5A1DF4-5F86-DD11-A0B1-000C6E4EC13D', 3, '�������','LoadTemplates','Contractor','AgentsLoadTemplates'
union 
select 'CD5A1DF4-5F86-DD11-A0B1-000C6E4EC13D', 3, '��������� ���������','ChangeState','Contractor','AgentChangeState'
union 
select 'E80EFE72-9A47-454E-B437-D71848AAF9C1', 3, '�����','LinkLoadSource','Contractor','AgentChainsLoadBySource'
union 
select '6C30D287-0078-41AB-8D94-CC707B7327D6', 3, '�����','LinkDelete','Contractor','AgentChainDelete'
union 
select '74B2027C-EA56-487C-A41F-F9EA826EB37D', 3, '�����','LinkInsert','Contractor','AgentChainInsert'
union 
select 'CE6D28DE-4F74-40A7-9421-B7B8A90F96C1', 3, '�����','LinkUpdate','Contractor','AgentChainUpdate'
union 
select 'FDAD0C1F-C68D-4EBE-BAA1-B99C44221456', 3, '������ ��������','FirstHierarchy','Hierarchy','AgentFirstHierarchy'
union 
select '41421837-7489-DD11-A479-000C6E4EC13D', 3, '�������� �������� � ��������','LoadByHierarchies','Hierarchy','AgentsLoadByHierarchies'

) a
left join [Core].[DbEntityMethods] b on a.Guid=b.Guid where b.Guid is null
GO
insert Core.LinkKinds(Id,Guid,StateId,DbEntityId,Code,Name,BranchId)
select b.Id,b.Guid,b.StateId,b.DbEntityId,b.Code,b.Name,b.BranchId
from 
(
select 0 as Id, '32421837-7489-DD11-A479-000C6E4EC13D' as Guid, 1 as StateId, 3 as DbEntityId, 'Tree' as Code, '�������� �����������' as Name, 1 as BranchId
union select 1, '33421837-7489-DD11-A479-000C6E4EC13D',1,3,'Workers','���� ����������',1
) b
left join Core.LinkKinds a
on a.Guid = b.Guid where a.Guid is null
GO
insert Agent.Agents(Guid, Name, StateId, DbEntityKindId,Flags,BranchId)
select a.Guid, a.Name, a.StateId, a.DbEntityKindId,a.Flags,a.BranchId from
(select '5CCADC5E-4800-DD11-8029-000C6E4EC13D' as [Guid], '����� ���������' as Name , 1 as StateId, 196610 as DbEntityKindId, 1 as Flags, 1 as BranchId 
union select '5DCADC5E-4800-DD11-8029-000C6E4EC13D', '����� �����������', 1, 196609,1, 1
union select '5ECADC5E-4800-DD11-8029-000C6E4EC13D', '��� �����������', 1, 196612,1, 1
union select 'F1FF5B09-C379-DD11-80D9-8000600FE800', '����� ����', 1, 196616,1, 1
union select 'E19AEE33-1971-426B-A037-2DA00631CEDD', '�����', 1, 196624,1, 1
) a
left join [Agent].[Agents] b on a.Guid=b.Guid where b.Guid is null
GO
print'������������� �����'
if not exists (select * from [Core].[DbEntities] where [Guid]='CB54F170-F39F-4BFA-B114-BA8F44209A78')
INSERT INTO [Core].[DbEntities]([Id],[Guid],[Name],[Code])
VALUES(2,'CB54F170-F39F-4BFA-B114-BA8F44209A78',N'������������ ����', 
N'BusinessObjects.Account, BusinessObjects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null')
GO
insert [Core].[DbEntityKinds]([Id],[Guid],[Name],[DbEntityId],[SubKind])
select a.[Id],a.[Guid],a.[Name],a.[DbEntityId],a.[SubKind] from
(select 131073 as Id,'377E72F0-F46E-DD11-9DD1-000C6E4EC13D' as Guid,'���� ��������' as Name,	2 as DbEntityId, 1 as SubKind
union 
select 131074,'387E72F0-F46E-DD11-9DD1-000C6E4EC13D','���� ���������',2,2
union 
select 131075,'397E72F0-F46E-DD11-9DD1-000C6E4EC13D','���� ��������-��������',2,3
union 
select 131076,'3A7E72F0-F46E-DD11-9DD1-000C6E4EC13D','���� ������������',2,4) a
left join [Core].[DbEntityKinds] b on a.Id=b.Id where b.Id is null
GO
delete from Core.DbEntityMethods where DbEntityId = 2;
insert [Core].[DbEntityMethods]([Guid],[DbEntityId],[Name],[Method], [Schema], [ProcedureName])
select a.[Guid],a.[DbEntityId],a.[Name],a.[Method], a.[Schema], a.[ProcedureName] from
(select 'B0E8CB86-1892-4F36-AB0A-C1896EBE7DB6' as [Guid], 2 as [DbEntityId],'��������  �� ��������������' as [Name],'Load' as [Method], 'BookKeep' as [Schema],  'AccountLoad' as [ProcedureName]
union 
select '2627A774-2A30-4D10-863C-FBDF198D8F31', 2, '��� ������','LoadAll','BookKeep','AccountsLoadAll'
union 
select '3C6C8B11-E5DD-46BC-B571-9FF627C9A410', 2, '��������','Delete','BookKeep','AccountDelete'
union 
select 'CFADBDD4-FBE5-4738-9BB3-986D0EB3B6C9', 2, '��������','Create','BookKeep','AccountInsert'
union 
select 'D38601A6-AA49-461C-A3DD-CB03FCC60AE6', 2, '����������','Update','BookKeep','AccountUpdate'
union 
select '1DEFA724-BF03-494D-94AF-70AFC8C8318C', 2, '�������� �� ��������������','ExistsId','BookKeep','AccountExistsId'
union 
select '53926AFE-E443-4298-9223-4A095F7164EE', 2, '�������� �� ����������� ��������������','ExistsGuid','BookKeep','AccountExistsGuid'
union 
select 'DBF36068-8B02-4635-A54A-3B44F6E5332A', 2, '�������� �� ����������� ��������������','FindByName','BookKeep','AccountsFindByName'
union 
select '94B3132D-5D02-4621-89A6-37A602DB55F5', 2, '�������','LoadTemplates','BookKeep','AccountsLoadTemplates'
union 
select 'B974016F-0D95-43CC-AD66-A453C93C5AD4', 2, '��������� ���������','ChangeState','BookKeep','AccountChangeState'
union 
select '13D77AFF-605D-47B6-8451-318547431ED1', 2, '������ ��������','FirstHierarchy','Hierarchy','AccountFirstHierarchy'
union 
select '98421837-7489-DD11-A479-000C6E4EC13D', 2, '�����','LinkLoadSource','BookKeep','AccountChainsLoadBySource'
union 
select '99421837-7489-DD11-A479-000C6E4EC13D', 2, '�����','LinkDelete','BookKeep','AccountChainDelete'
union 
select '9A421837-7489-DD11-A479-000C6E4EC13D', 2, '�����','LinkInsert','BookKeep','AccountChainInsert'
union 
select '9B421837-7489-DD11-A479-000C6E4EC13D', 2, '�����','LinkUpdate','BookKeep','AccountChainUpdate'
union 
select '42421837-7489-DD11-A479-000C6E4EC13D', 2, '�������� �������� � ��������','LoadByHierarchies','Hierarchy','AccountsLoadByHierarchies'

) a
left join [Core].[DbEntityMethods] b on a.Guid=b.Guid where b.Guid is null
GO
print '���������'
if not exists (select * from [Core].[DbEntities] where [Guid]='D790C310-33CD-4DE9-9305-D906BFCA3793')
INSERT INTO [Core].[DbEntities]([Id],[Guid],[Name],[Code])
VALUES(4,'D790C310-33CD-4DE9-9305-D906BFCA3793',N'���������', 
N'BusinessObjects.Analitic, BusinessObjects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null')
GO
insert [Core].[DbEntityKinds]([Id],[Guid],[Name],[DbEntityId],[SubKind])
select a.[Id],a.[Guid],a.[Name],a.[DbEntityId],a.[SubKind] from
(select 262145 as Id,'3E7E72F0-F46E-DD11-9DD1-000C6E4EC13D' as Guid,'���������' as Name, 4 as DbEntityId, 1 as SubKind
union 
select 262146,'3F7E72F0-F46E-DD11-9DD1-000C6E4EC13D','�������� ������',4,2
union 
select 262148,'407E72F0-F46E-DD11-9DD1-000C6E4EC13D','�����',4,4
) a
left join [Core].[DbEntityKinds] b on a.Id=b.Id where b.Id is null
GO
delete from Core.DbEntityMethods where DbEntityId = 4;
insert [Core].[DbEntityMethods]([Guid],[DbEntityId],[Name],[Method], [Schema], [ProcedureName])
select a.[Guid],a.[DbEntityId],a.[Name],a.[Method], a.[Schema], a.[ProcedureName] from
(select 'E8065AE6-6086-DD11-A0B1-000C6E4EC13D' as [Guid], 4 as [DbEntityId],'��������  �� ��������������' as [Name],'Load' as [Method], 'Analitic' as [Schema],  'AnaliticLoad' as [ProcedureName]
union 
select 'E9065AE6-6086-DD11-A0B1-000C6E4EC13D', 4, '��� ������','LoadAll','Analitic','AnaliticsLoadAll'
union 
select 'EA065AE6-6086-DD11-A0B1-000C6E4EC13D', 4, '��������','Delete','Analitic','AnaliticDelete'
union 
select 'EB065AE6-6086-DD11-A0B1-000C6E4EC13D', 4, '��������','Create','Analitic','AnaliticInsert'
union 
select 'EC065AE6-6086-DD11-A0B1-000C6E4EC13D', 4, '����������','Update','Analitic','AnaliticUpdate'
union 
select 'ED065AE6-6086-DD11-A0B1-000C6E4EC13D', 4, '�������� �� ��������������','ExistsId','Analitic','AnaliticExistsId'
union 
select 'EE065AE6-6086-DD11-A0B1-000C6E4EC13D', 4, '�������� �� ����������� ��������������','ExistsGuid','Analitic','AnaliticExistsGuid'
union 
select 'EF065AE6-6086-DD11-A0B1-000C6E4EC13D', 4, '�������� �� ����������� ��������������','FindByName','Analitic','AnaliticsFindByName'
union 
select 'F0065AE6-6086-DD11-A0B1-000C6E4EC13D', 4, '�������','LoadTemplates','Analitic','AnaliticsLoadTemplates'
union 
select 'F1065AE6-6086-DD11-A0B1-000C6E4EC13D', 4, '��������� ���������','ChangeState','Analitic','AnaliticChangeState'
union 
select '51E57918-BED3-4DD9-8322-6B291E014F57', 4, '������ ��������','FirstHierarchy','Hierarchy','AnaliticFirstHierarchy'
union 
select '43421837-7489-DD11-A479-000C6E4EC13D', 4, '�������� �������� � ��������','LoadByHierarchies','Hierarchy','AnaliticsLoadByHierarchies'
union 
select '9C421837-7489-DD11-A479-000C6E4EC13D', 4, '�����','LinkLoadSource','Analitic','AnaliticChainsLoadBySource'
union 
select '9D421837-7489-DD11-A479-000C6E4EC13D', 4, '�����','LinkDelete','Analitic','AnaliticChainDelete'
union 
select '9E421837-7489-DD11-A479-000C6E4EC13D', 4, '�����','LinkInsert','Analitic','AnaliticChainInsert'
union 
select '9F421837-7489-DD11-A479-000C6E4EC13D', 4, '�����','LinkUpdate','Analitic','AnaliticChainUpdate'
) a
left join [Core].[DbEntityMethods] b on a.Guid=b.Guid where b.Guid is null
GO
print '�����'
if not exists (select * from [Core].[DbEntities] where [Guid]='C5314E13-39FF-4959-8E8C-90EF373A0911')
INSERT INTO [Core].[DbEntities]([Id],[Guid],[Name],[Code])
VALUES(7,'C5314E13-39FF-4959-8E8C-90EF373A0911',N'�����', 
N'BusinessObjects.Folder, BusinessObjects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null')
GO
insert [Core].[DbEntityKinds]([Id],[Guid],[Name],[DbEntityId],[SubKind])
select a.[Id],a.[Guid],a.[Name],a.[DbEntityId],a.[SubKind] from
(select 458753 as Id,'447E72F0-F46E-DD11-9DD1-000C6E4EC13D' as Guid,'�����' as Name, 7 as DbEntityId, 1 as SubKind
) a
left join [Core].[DbEntityKinds] b on a.Id=b.Id where b.Id is null
GO
delete from Core.DbEntityMethods where DbEntityId = 7;
insert [Core].[DbEntityMethods]([Guid],[DbEntityId],[Name],[Method], [Schema], [ProcedureName])
select a.[Guid],a.[DbEntityId],a.[Name],a.[Method], a.[Schema], a.[ProcedureName] from
(select '52BCC3BF-8586-DD11-A0B1-000C6E4EC13D' as [Guid], 7 as [DbEntityId],'��������  �� ��������������' as [Name],'Load' as [Method], 'Core' as [Schema],  'FolderLoad' as [ProcedureName]
union 
select '53BCC3BF-8586-DD11-A0B1-000C6E4EC13D', 7, '��� ������','LoadAll','Core','FoldersLoadAll'
union 
select '54BCC3BF-8586-DD11-A0B1-000C6E4EC13D', 7, '��������','Delete','Core','FolderDelete'
union 
select '55BCC3BF-8586-DD11-A0B1-000C6E4EC13D', 7, '��������','Create','Core','FolderInsert'
union 
select '56BCC3BF-8586-DD11-A0B1-000C6E4EC13D', 7, '����������','Update','Core','FolderUpdate'
union 
select '57BCC3BF-8586-DD11-A0B1-000C6E4EC13D', 7, '�������� �� ��������������','ExistsId','Core','FolderExistsId'
union 
select '58BCC3BF-8586-DD11-A0B1-000C6E4EC13D', 7, '�������� �� ����������� ��������������','ExistsGuid','Core','FolderExistsGuid'
union 
select '59BCC3BF-8586-DD11-A0B1-000C6E4EC13D', 7, '�������� �� ����������� ��������������','FindByName','Core','FoldersFindByName'
union 
select '5ABCC3BF-8586-DD11-A0B1-000C6E4EC13D', 7, '�������','LoadTemplates','Core','FoldersLoadTemplates'
union 
select '5BBCC3BF-8586-DD11-A0B1-000C6E4EC13D', 7, '��������� ���������','ChangeState','Core','FolderChangeState'
union 
select '3FE11559-872B-4871-96B5-88EDA603F42E', 7, '������ ��������','FirstHierarchy','Hierarchy','FolderFirstHierarchy'
union 
select '44421837-7489-DD11-A479-000C6E4EC13D', 7, '��������� �������� � ��������','LoadByHierarchies','Hierarchy','FoldersLoadByHierarchies'
union 
select 'A0421837-7489-DD11-A479-000C6E4EC13D', 7, '�����','LinkLoadSource','Core','FolderChainsLoadBySource'
union 
select 'A1421837-7489-DD11-A479-000C6E4EC13D', 7, '�����','LinkDelete','Core','FolderChainDelete'
union 
select 'A2421837-7489-DD11-A479-000C6E4EC13D', 7, '�����','LinkInsert','Core','FolderChainInsert'
union 
select 'A3421837-7489-DD11-A479-000C6E4EC13D', 7, '�����','LinkUpdate','Core','FolderChainUpdate'
) a
left join [Core].[DbEntityMethods] b on a.Guid=b.Guid where b.Guid is null
GO
print '������'
if not exists (select * from [Core].[DbEntities] where [Guid]='34363635-1C43-4F13-B289-2CD78F071903')
INSERT INTO [Core].[DbEntities]([Id],[Guid],[Name],[Code])
VALUES(2,'34363635-1C43-4F13-B289-2CD78F071903',N'������ �����', 
N'BusinessObjects.Product, BusinessObjects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null')
GO
insert [Core].[DbEntityKinds]([Id],[Guid],[Name],[DbEntityId],[SubKind])
select a.[Id],a.[Guid],a.[Name],a.[DbEntityId],a.[SubKind] from
(select 131073 as Id,'367E72F0-F46E-DD11-9DD1-000C6E4EC13D' as Guid,'������ �����' as Name,	1 as DbEntityId, 1 as SubKind
) a
left join [Core].[DbEntityKinds] b on a.Id=b.Id where b.Id is null
GO
delete from Core.DbEntityMethods where DbEntityId = 1;
insert [Core].[DbEntityMethods]([Guid],[DbEntityId],[Name],[Method], [Schema], [ProcedureName])
select a.[Guid],a.[DbEntityId],a.[Name],a.[Method], a.[Schema], a.[ProcedureName] from
(select '11618C48-7B75-4094-82B9-545EC81C1F4F' as [Guid], 1 as [DbEntityId],'��������  �� ��������������' as [Name],'Load' as [Method], 'Product' as [Schema],  'ProductLoad' as [ProcedureName]
union 
select 'E99574DB-9A4F-4437-B065-2185B1D24287', 1, '��� ������','LoadAll','Product','ProductsLoadAll'
union 
select 'CC47C1FD-5BC7-4AA7-A99F-F91B3755CDCA', 1, '��������','Delete','Product','ProductDelete'
union 
select 'EE15F609-9532-4E5F-BBD5-0FC7DD325495', 1, '��������','Create','Product','ProductInsert'
union 
select '3FC07F29-8F63-4B4A-BACB-E0BDD8AF64E3', 1, '����������','Update','Product','ProductUpdate'
union 
select '1DEFA724-BF03-494D-94AF-70AFC8C8318C', 1, '�������� �� ��������������','ExistsId','Product','ProductExistsId'
union 
select 'D7C3D49C-98AD-40C2-87F2-E6C4F98B811B', 1, '�������� �� ����������� ��������������','ExistsGuid','Product','ProductExistsGuid'
union 
select 'E7FE3EA4-AF55-44D1-8A33-64993646840D', 1, '�������� �� ����������� ��������������','FindByName','Product','ProductsFindByName'
union 
select 'E6EC2FBC-1EF0-444A-A426-0C65F24479D1', 1, '�������','LoadTemplates','Product','ProductsLoadTemplates'
union 
select 'F49E8876-DABD-44A9-8716-DD3667F6749B', 1, '��������� ���������','ChangeState','Product','ProductChangeState'
union 
select '8476F4DC-B18D-42EF-812B-CFB655866DEA', 1, '������ ��������','FirstHierarchy','Hierarchy','ProductFirstHierarchy'
union 
select '45421837-7489-DD11-A479-000C6E4EC13D', 1, '�������� �������� � ��������','LoadByHierarchies','Hierarchy','ProductsLoadByHierarchies'
union 
select 'A4421837-7489-DD11-A479-000C6E4EC13D', 1, '�����','LinkLoadSource','Product','ProductChainsLoadBySource'
union 
select 'A5421837-7489-DD11-A479-000C6E4EC13D', 1, '�����','LinkDelete','Product','ProductChainDelete'
union 
select 'A6421837-7489-DD11-A479-000C6E4EC13D', 1, '�����','LinkInsert','Product','ProductChainInsert'
union 
select 'A7421837-7489-DD11-A479-000C6E4EC13D', 1, '�����','LinkUpdate','Product','ProductChainUpdate'
) a
left join [Core].[DbEntityMethods] b on a.Guid=b.Guid where b.Guid is null
GO
print '��������� �����'
if not exists (select * from [Core].[DbEntities] where [Guid]='AE7BA5CC-007A-DD11-B4B9-000C6E4EC13D')
INSERT INTO [Core].[DbEntities]([Id],[Guid],[Name],[Code])
VALUES(24,'AE7BA5CC-007A-DD11-B4B9-000C6E4EC13D',N'��������� ����', 
N'BusinessObjects.AgentBankAccount, BusinessObjects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null')
GO
insert [Core].[DbEntityKinds]([Id],[Guid],[Name],[DbEntityId],[SubKind])
select a.[Id],a.[Guid],a.[Name],a.[DbEntityId],a.[SubKind] from
(select 1572865 as Id,'0478A6FB-007A-DD11-B4B9-000C6E4EC13D' as Guid,'��������� ����' as Name, 24 as DbEntityId, 1 as SubKind
) a
left join [Core].[DbEntityKinds] b on a.Id=b.Id where b.Id is null
GO
delete from Core.DbEntityMethods where DbEntityId = 24;
insert [Core].[DbEntityMethods]([Guid],[DbEntityId],[Name],[Method], [Schema], [ProcedureName])
select a.[Guid],a.[DbEntityId],a.[Name],a.[Method], a.[Schema], a.[ProcedureName] from
(select '3E8A7198-1688-DD11-A4B1-000C6E4EC13D' as [Guid], 24 as [DbEntityId],'��������  �� ��������������' as [Name],'Load' as [Method], 'Agent' as [Schema],  'AgentBankAccountLoad' as [ProcedureName]
union 
select '3F8A7198-1688-DD11-A4B1-000C6E4EC13D', 24, '��� ������','LoadAll','Agent','AgentBankAccountsLoadAll'
union 
select '408A7198-1688-DD11-A4B1-000C6E4EC13D', 24, '��������','Delete','Agent','AgentBankAccountDelete'
union 
select '418A7198-1688-DD11-A4B1-000C6E4EC13D', 24, '��������','Create','Agent','AgentBankAccountInsert'
union 
select '428A7198-1688-DD11-A4B1-000C6E4EC13D', 24, '����������','Update','Agent','AgentBankAccountUpdate'
union 
select '438A7198-1688-DD11-A4B1-000C6E4EC13D', 24, '�������� �� ��������������','ExistsId','Agent','AgentBankAccountExistsId'
union 
select '448A7198-1688-DD11-A4B1-000C6E4EC13D', 24, '�������� �� ����������� ��������������','ExistsGuid','Agent','AgentBankAccountExistsGuid'
union 
select '458A7198-1688-DD11-A4B1-000C6E4EC13D', 24, '�������� �� ����������� ��������������','FindByName','Agent','AgentBankAccountsFindByName'
union 
select '468A7198-1688-DD11-A4B1-000C6E4EC13D', 24, '�������','LoadTemplates','Agent','AgentBankAccountsLoadTemplates'
union 
select '478A7198-1688-DD11-A4B1-000C6E4EC13D', 24, '��������� ���������','ChangeState','Agent','AgentBankAccountChangeState'
) a
left join [Core].[DbEntityMethods] b on a.Guid=b.Guid where b.Guid is null
GO
print '������'
if not exists (select * from [Core].[DbEntities] where [Guid]='84AC9DED-611A-4E18-87D3-17A7BFD8E10A')
INSERT INTO [Core].[DbEntities]([Id],[Guid],[Name],[Code])
VALUES(5,'84AC9DED-611A-4E18-87D3-17A7BFD8E10A',N'������', 
N'BusinessObjects.Currency, BusinessObjects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null')
GO
insert [Core].[DbEntityKinds]([Id],[Guid],[Name],[DbEntityId],[SubKind])
select a.[Id],a.[Guid],a.[Name],a.[DbEntityId],a.[SubKind] from
(select 327681 as Id,'417E72F0-F46E-DD11-9DD1-000C6E4EC13D' as Guid,'������' as Name, 5 as DbEntityId, 1 as SubKind
) a
left join [Core].[DbEntityKinds] b on a.Id=b.Id where b.Id is null
GO
delete from Core.DbEntityMethods where DbEntityId = 5;
insert [Core].[DbEntityMethods]([Guid],[DbEntityId],[Name],[Method], [Schema], [ProcedureName])
select a.[Guid],a.[DbEntityId],a.[Name],a.[Method], a.[Schema], a.[ProcedureName] from
(select 'C058B157-8286-DD11-A0B1-000C6E4EC13D' as [Guid], 5 as [DbEntityId],'��������  �� ��������������' as [Name],'Load' as [Method], 'Core' as [Schema],  'CurrencyLoad' as [ProcedureName]
union 
select 'C158B157-8286-DD11-A0B1-000C6E4EC13D', 5, '��� ������','LoadAll','Core','CurrenciesLoadAll'
union 
select 'C258B157-8286-DD11-A0B1-000C6E4EC13D', 5, '��������','Delete','Core','CurrencyDelete'
union 
select 'C358B157-8286-DD11-A0B1-000C6E4EC13D', 5, '��������','Create','Core','CurrencyInsert'
union 
select 'C458B157-8286-DD11-A0B1-000C6E4EC13D', 5, '����������','Update','Core','CurrencyUpdate'
union 
select 'C558B157-8286-DD11-A0B1-000C6E4EC13D', 5, '�������� �� ��������������','ExistsId','Core','CurrencyExistsId'
union 
select 'C658B157-8286-DD11-A0B1-000C6E4EC13D', 5, '�������� �� ����������� ��������������','ExistsGuid','Core','CurrencyExistsGuid'
union 
select 'C758B157-8286-DD11-A0B1-000C6E4EC13D', 5, '�������� �� ����������� ��������������','FindByName','Core','CurrenciesFindByName'
union 
select 'C858B157-8286-DD11-A0B1-000C6E4EC13D', 5, '�������','LoadTemplates','Core','CurrenciesLoadTemplates'
union 
select 'C958B157-8286-DD11-A0B1-000C6E4EC13D', 5, '��������� ���������','ChangeState','Core','CurrencyChangeState'
union 
select '96421837-7489-DD11-A479-000C6E4EC13D', 5, '��������� �������� � ��������','LoadByHierarchies','Hierarchy','CurrenciesLoadByHierarchies'
union 
select 'A8421837-7489-DD11-A479-000C6E4EC13D', 5, '�����','LinkLoadSource','Core','CurrencyChainsLoadBySource'
union 
select 'A9421837-7489-DD11-A479-000C6E4EC13D', 5, '�����','LinkDelete','Core','CurrencyChainDelete'
union 
select 'AA421837-7489-DD11-A479-000C6E4EC13D', 5, '�����','LinkInsert','Core','CurrencyChainInsert'
union 
select 'AB421837-7489-DD11-A479-000C6E4EC13D', 5, '�����','LinkUpdate','Core','CurrencyChainUpdate'

) a
left join [Core].[DbEntityMethods] b on a.Guid=b.Guid where b.Guid is null
GO


print '������� ������'
if not exists (select * from [Core].[DbEntities] where [Guid]='06C8F3D0-1488-DD11-A4B1-000C6E4EC13D')
INSERT INTO [Core].[DbEntities]([Id],[Guid],[Name],[Code])
VALUES(22,'06C8F3D0-1488-DD11-A4B1-000C6E4EC13D',N'������� ������', 
N'BusinessObjects.CustomViewColumn, BusinessObjects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null')
GO
delete from Core.DbEntityMethods where DbEntityId = 22;
insert [Core].[DbEntityMethods]([Guid],[DbEntityId],[Name],[Method], [Schema], [ProcedureName])
select a.[Guid],a.[DbEntityId],a.[Name],a.[Method], a.[Schema], a.[ProcedureName] from
(select 'F8FE8F52-1488-DD11-A4B1-000C6E4EC13D' as [Guid], 22 as [DbEntityId],'��������  �� ��������������' as [Name],'Load' as [Method], 'Core' as [Schema],  'CustomViewColumnLoad' as [ProcedureName]
union 
select 'F9FE8F52-1488-DD11-A4B1-000C6E4EC13D', 22, '��� ������','LoadAll','Core','CustomViewColumnsLoadAll'
union 
select 'FAFE8F52-1488-DD11-A4B1-000C6E4EC13D', 22, '��������','Delete','Core','CustomViewColumnDelete'
union 
select 'FBFE8F52-1488-DD11-A4B1-000C6E4EC13D', 22, '��������','Create','Core','CustomViewColumnInsert'
union 
select 'FCFE8F52-1488-DD11-A4B1-000C6E4EC13D', 22, '����������','Update','Core','CustomViewColumnUpdate'
union 
select 'FDFE8F52-1488-DD11-A4B1-000C6E4EC13D', 22, '�������� �� ��������������','ExistsId','Core','CustomViewColumnExistsId'
union 
select 'FEFE8F52-1488-DD11-A4B1-000C6E4EC13D', 22, '�������� �� ����������� ��������������','ExistsGuid','Core','CustomViewColumnExistsGuid'
union 
select 'FFFE8F52-1488-DD11-A4B1-000C6E4EC13D', 22, '�������� �� ����������� ��������������','FindByName','Core','CustomViewColumnsFindByName'
union 
select '00FF8F52-1488-DD11-A4B1-000C6E4EC13D', 22, '�������','LoadTemplates','Core','CustomViewColumnsLoadTemplates'
union 
select '01FF8F52-1488-DD11-A4B1-000C6E4EC13D', 22, '��������� ���������','ChangeState','Core','CustomViewColumnChangeState'
) a
left join [Core].[DbEntityMethods] b on a.Guid=b.Guid where b.Guid is null
GO
print '������'
if not exists (select * from [Core].[DbEntities] where [Guid]='FC5D281F-3274-DD11-80D2-0018F30641D3')
INSERT INTO [Core].[DbEntities]([Id],[Guid],[Name],[Code])
VALUES(21,'FC5D281F-3274-DD11-80D2-0018F30641D3',N'������', 
N'BusinessObjects.CustomViewList, BusinessObjects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null')
GO
insert [Core].[DbEntityKinds]([Id],[Guid],[Name],[DbEntityId],[SubKind])
select a.[Id],a.[Guid],a.[Name],a.[DbEntityId],a.[SubKind] from
(select 1376257 as Id,'560B1489-B477-DD11-9B91-000C6E4EC13D' as Guid,'������' as Name, 21 as DbEntityId, 1 as SubKind
) a
left join [Core].[DbEntityKinds] b on a.Id=b.Id where b.Id is null
GO
delete from Core.DbEntityMethods where DbEntityId = 21;
insert [Core].[DbEntityMethods]([Guid],[DbEntityId],[Name],[Method], [Schema], [ProcedureName])
select a.[Guid],a.[DbEntityId],a.[Name],a.[Method], a.[Schema], a.[ProcedureName] from
(select '2B9EED36-1388-DD11-A4B1-000C6E4EC13D' as [Guid], 21 as [DbEntityId],'��������  �� ��������������' as [Name],'Load' as [Method], 'Core' as [Schema],  'CustomViewListLoad' as [ProcedureName]
union 
select '229EED36-1388-DD11-A4B1-000C6E4EC13D', 21, '��� ������','LoadAll','Core','CustomViewListsLoadAll'
union 
select '239EED36-1388-DD11-A4B1-000C6E4EC13D', 21, '��������','Delete','Core','CustomViewListDelete'
union 
select '249EED36-1388-DD11-A4B1-000C6E4EC13D', 21, '��������','Create','Core','CustomViewListInsert'
union 
select '259EED36-1388-DD11-A4B1-000C6E4EC13D', 21, '����������','Update','Core','CustomViewListUpdate'
union 
select '269EED36-1388-DD11-A4B1-000C6E4EC13D', 21, '�������� �� ��������������','ExistsId','Core','CustomViewListExistsId'
union 
select '279EED36-1388-DD11-A4B1-000C6E4EC13D', 21, '�������� �� ����������� ��������������','ExistsGuid','Core','CustomViewListExistsGuid'
union 
select '289EED36-1388-DD11-A4B1-000C6E4EC13D', 21, '�������� �� ����������� ��������������','FindByName','Core','CustomViewListsFindByName'
union 
select '299EED36-1388-DD11-A4B1-000C6E4EC13D', 21, '�������','LoadTemplates','Core','CustomViewListsLoadTemplates'
union 
select '2A9EED36-1388-DD11-A4B1-000C6E4EC13D', 21, '��������� ���������','ChangeState','Core','CustomViewListChangeState'
union 
select 'B0421837-7489-DD11-A479-000C6E4EC13D', 21, '�����','LinkLoadSource','Core','CustomViewListChainsLoadBySource'
union 
select 'B1421837-7489-DD11-A479-000C6E4EC13D', 21, '�����','LinkDelete','Core','CustomViewListChainDelete'
union 
select 'B2421837-7489-DD11-A479-000C6E4EC13D', 21, '�����','LinkInsert','Core','CustomViewListChainInsert'
union 
select 'B3421837-7489-DD11-A479-000C6E4EC13D', 21, '�����','LinkUpdate','Core','CustomViewListChainUpdate'

) a
left join [Core].[DbEntityMethods] b on a.Guid=b.Guid where b.Guid is null
GO

print '����������'
if not exists (select * from [Core].[DbEntities] where [Guid]='83F35BFB-274C-4CB9-813D-E033F0F8B410')
INSERT INTO [Core].[DbEntities]([Id],[Guid],[Name],[Code])
VALUES(15,'83F35BFB-274C-4CB9-813D-E033F0F8B410',N'����������', 
N'BusinessObjects.ProjectItem, BusinessObjects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null')
GO
insert [Core].[DbEntityKinds]([Id],[Guid],[Name],[DbEntityId],[SubKind])
select a.[Id],a.[Guid],a.[Name],a.[DbEntityId],a.[SubKind] from
(select 983041 as Id,'4B7E72F0-F46E-DD11-9DD1-000C6E4EC13D' as Guid,'����������' as Name, 15 as DbEntityId, 1 as SubKind
union select 983042,'A42E0767-E676-DD11-8819-8000600FE800','���������� ��������',15,2
union select 983044,'A62E0767-E676-DD11-8819-8000600FE800','����������',15,4
union select 983048,'A72E0767-E676-DD11-8819-8000600FE800','���������� ����������',15,8
union select 983056,'9E9BF305-E87D-DD11-B9B9-000C6E4EC13D','����� StimulSoft',15,16
) a
left join [Core].[DbEntityKinds] b on a.Id=b.Id where b.Id is null
GO
delete from Core.DbEntityMethods where DbEntityId = 15;
insert [Core].[DbEntityMethods]([Guid],[DbEntityId],[Name],[Method], [Schema], [ProcedureName])
select a.[Guid],a.[DbEntityId],a.[Name],a.[Method], a.[Schema], a.[ProcedureName] from
(select 'F75CD231-0888-DD11-A4B1-000C6E4EC13D' as [Guid], 15 as [DbEntityId],'��������  �� ��������������' as [Name],'Load' as [Method], 'Core' as [Schema],  'LibraryLoad' as [ProcedureName]
union 
select 'EE5CD231-0888-DD11-A4B1-000C6E4EC13D', 15, '��� ������','LoadAll','Core','LibrariesLoadAll'
union 
select 'EF5CD231-0888-DD11-A4B1-000C6E4EC13D', 15, '��������','Delete','Core','LibraryDelete'
union 
select 'F05CD231-0888-DD11-A4B1-000C6E4EC13D', 15, '��������','Create','Core','LibraryInsert'
union 
select 'F15CD231-0888-DD11-A4B1-000C6E4EC13D', 15, '����������','Update','Core','LibraryUpdate'
union 
select 'F25CD231-0888-DD11-A4B1-000C6E4EC13D', 15, '�������� �� ��������������','ExistsId','Core','LibraryExistsId'
union 
select 'F35CD231-0888-DD11-A4B1-000C6E4EC13D', 15, '�������� �� ����������� ��������������','ExistsGuid','Core','LibraryExistsGuid'
union 
select 'F45CD231-0888-DD11-A4B1-000C6E4EC13D', 15, '�������� �� ����������� ��������������','FindByName','Core','LibrariesFindByName'
union 
select 'F55CD231-0888-DD11-A4B1-000C6E4EC13D', 15, '�������','LoadTemplates','Core','LibrariesLoadTemplates'
union 
select 'F65CD231-0888-DD11-A4B1-000C6E4EC13D', 15, '��������� ���������','ChangeState','Core','LibraryChangeState'
union 
select '92421837-7489-DD11-A479-000C6E4EC13D', 15, '��������� �������� � ��������','LoadByHierarchies','Hierarchy','LibrariesLoadByHierarchies'
union 
select 'B4421837-7489-DD11-A479-000C6E4EC13D', 15, '�����','LinkLoadSource','Core','LibraryChainsLoadBySource'
union 
select 'B5421837-7489-DD11-A479-000C6E4EC13D', 15, '�����','LinkDelete','Core','LibraryChainDelete'
union 
select 'B6421837-7489-DD11-A479-000C6E4EC13D', 15, '�����','LinkInsert','Core','LibraryChainInsert'
union 
select 'B7421837-7489-DD11-A479-000C6E4EC13D', 15, '�����','LinkUpdate','Core','LibraryChainUpdate'
) a
left join [Core].[DbEntityMethods] b on a.Guid=b.Guid where b.Guid is null
GO
print '�����-����'
if not exists (select * from [Core].[DbEntities] where [Guid]='50D09DA2-2493-4AF4-B770-711E8C85A0DB')
INSERT INTO [Core].[DbEntities]([Id],[Guid],[Name],[Code])
VALUES(8,'50D09DA2-2493-4AF4-B770-711E8C85A0DB',N'�����-����', 
N'BusinessObjects.PriceList, BusinessObjects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null')
GO
insert [Core].[DbEntityKinds]([Id],[Guid],[Name],[DbEntityId],[SubKind])
select a.[Id],a.[Guid],a.[Name],a.[DbEntityId],a.[SubKind] from
(select 524289 as Id,'457E72F0-F46E-DD11-9DD1-000C6E4EC13D' as Guid,'�����-����' as Name, 8 as DbEntityId, 1 as SubKind
) a
left join [Core].[DbEntityKinds] b on a.Id=b.Id where b.Id is null
GO
delete from Core.DbEntityMethods where DbEntityId = 8;
insert [Core].[DbEntityMethods]([Guid],[DbEntityId],[Name],[Method], [Schema], [ProcedureName])
select a.[Guid],a.[DbEntityId],a.[Name],a.[Method], a.[Schema], a.[ProcedureName] from
(select '28C205D9-8A86-DD11-A0B1-000C6E4EC13D' as [Guid], 8 as [DbEntityId],'��������  �� ��������������' as [Name],'Load' as [Method], 'Price' as [Schema],  'PriceListLoad' as [ProcedureName]
union 
select '29C205D9-8A86-DD11-A0B1-000C6E4EC13D', 8, '��� ������','LoadAll','Price','PriceListsLoadAll'
union 
select '2AC205D9-8A86-DD11-A0B1-000C6E4EC13D', 8, '��������','Delete','Price','PriceListDelete'
union 
select '2BC205D9-8A86-DD11-A0B1-000C6E4EC13D', 8, '��������','Create','Price','PriceListInsert'
union 
select '2CC205D9-8A86-DD11-A0B1-000C6E4EC13D', 8, '����������','Update','Price','PriceListUpdate'
union 
select '2DC205D9-8A86-DD11-A0B1-000C6E4EC13D', 8, '�������� �� ��������������','ExistsId','Price','PriceListExistsId'
union 
select '2EC205D9-8A86-DD11-A0B1-000C6E4EC13D', 8, '�������� �� ����������� ��������������','ExistsGuid','Price','PriceListExistsGuid'
union 
select '2FC205D9-8A86-DD11-A0B1-000C6E4EC13D', 8, '�������� �� ����������� ��������������','FindByName','Price','PriceListsFindByName'
union 
select '30C205D9-8A86-DD11-A0B1-000C6E4EC13D', 8, '�������','LoadTemplates','Price','PriceListsLoadTemplates'
union 
select '31C205D9-8A86-DD11-A0B1-000C6E4EC13D', 8, '��������� ���������','ChangeState','Price','PriceListChangeState'
union 
select 'B8421837-7489-DD11-A479-000C6E4EC13D', 8, '�����','LinkLoadSource','Price','PriceListChainsLoadBySource'
union 
select 'B9421837-7489-DD11-A479-000C6E4EC13D', 8, '�����','LinkDelete','Price','PriceListChainDelete'
union 
select 'BA421837-7489-DD11-A479-000C6E4EC13D', 8, '�����','LinkInsert','Price','PriceListChainInsert'
union 
select 'BB421837-7489-DD11-A479-000C6E4EC13D', 8, '�����','LinkUpdate','Price','PriceListChainUpdate'
) a
left join [Core].[DbEntityMethods] b on a.Guid=b.Guid where b.Guid is null
GO
print '��� ����'
if not exists (select * from [Core].[DbEntities] where [Guid]='B60B2502-258D-47C4-A066-DE3080B4428D')
INSERT INTO [Core].[DbEntities]([Id],[Guid],[Name],[Code])
VALUES(9,'B60B2502-258D-47C4-A066-DE3080B4428D',N'��� ����', 
N'BusinessObjects.PriceName, BusinessObjects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null')
GO
insert [Core].[DbEntityKinds]([Id],[Guid],[Name],[DbEntityId],[SubKind])
select a.[Id],a.[Guid],a.[Name],a.[DbEntityId],a.[SubKind] from
(select 589825 as Id,'467E72F0-F46E-DD11-9DD1-000C6E4EC13D' as Guid,'��� ����' as Name, 9 as DbEntityId, 1 as SubKind
) a
left join [Core].[DbEntityKinds] b on a.Id=b.Id where b.Id is null
GO
delete from Core.DbEntityMethods where DbEntityId = 9;
insert [Core].[DbEntityMethods]([Guid],[DbEntityId],[Name],[Method], [Schema], [ProcedureName])
select a.[Guid],a.[DbEntityId],a.[Name],a.[Method], a.[Schema], a.[ProcedureName] from
(select 'F83420E4-8B86-DD11-A0B1-000C6E4EC13D' as [Guid], 9 as [DbEntityId],'��������  �� ��������������' as [Name],'Load' as [Method], 'Price' as [Schema],  'PriceNameLoad' as [ProcedureName]
union 
select 'F93420E4-8B86-DD11-A0B1-000C6E4EC13D', 9, '��� ������','LoadAll','Price','PriceNamesLoadAll'
union 
select 'FA3420E4-8B86-DD11-A0B1-000C6E4EC13D', 9, '��������','Delete','Price','PriceNameDelete'
union 
select 'FB3420E4-8B86-DD11-A0B1-000C6E4EC13D', 9, '��������','Create','Price','PriceNameInsert'
union 
select 'FC3420E4-8B86-DD11-A0B1-000C6E4EC13D', 9, '����������','Update','Price','PriceNameUpdate'
union 
select 'FD3420E4-8B86-DD11-A0B1-000C6E4EC13D', 9, '�������� �� ��������������','ExistsId','Price','PriceNameExistsId'
union 
select 'FE3420E4-8B86-DD11-A0B1-000C6E4EC13D', 9, '�������� �� ����������� ��������������','ExistsGuid','Price','PriceNameExistsGuid'
union 
select 'FF3420E4-8B86-DD11-A0B1-000C6E4EC13D', 9, '�������� �� ����������� ��������������','FindByName','Price','PriceNamesFindByName'
union 
select '003520E4-8B86-DD11-A0B1-000C6E4EC13D', 9, '�������','LoadTemplates','Price','PriceNamesLoadTemplates'
union 
select '013520E4-8B86-DD11-A0B1-000C6E4EC13D', 9, '��������� ���������','ChangeState','Price','PriceNameChangeState'
union 
select 'BC421837-7489-DD11-A479-000C6E4EC13D', 9, '�������� ������ ��� ���������','LinkLoadSource','Price','PriceNameChainsLoadBySource'
union 
select 'BD421837-7489-DD11-A479-000C6E4EC13D', 9, '�������� �����','LinkDelete','Price','PriceNameChainDelete'
union 
select 'BE421837-7489-DD11-A479-000C6E4EC13D', 9, '�������� �����','LinkInsert','Price','PriceNameChainInsert'
union 
select 'BF421837-7489-DD11-A479-000C6E4EC13D', 9, '���������� �����','LinkUpdate','Price','PriceNameChainUpdate'

) a
left join [Core].[DbEntityMethods] b on a.Guid=b.Guid where b.Guid is null
GO
print '�����'
if not exists (select * from [Core].[DbEntities] where [Guid]='D929544B-1C32-41A9-8FDC-40905AF371FB')
INSERT INTO [Core].[DbEntities]([Id],[Guid],[Name],[Code])
VALUES(14,'D929544B-1C32-41A9-8FDC-40905AF371FB',N'�����', 
N'BusinessObjects.ProjectItem, BusinessObjects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null')
GO
insert [Core].[DbEntityKinds]([Id],[Guid],[Name],[DbEntityId],[SubKind])
select a.[Id],a.[Guid],a.[Name],a.[DbEntityId],a.[SubKind] from
(select 917505 as Id,'4D7E72F0-F46E-DD11-9DD1-000C6E4EC13D' as Guid,'����� ���������' as Name, 14 as DbEntityId, 1 as SubKind
union
select 917506,'4E7E72F0-F46E-DD11-9DD1-000C6E4EC13D','�����',14,2
union
select 917508,'7855C84D-E77D-DD11-B9B9-000C6E4EC13D','�������� ����� ���������',14,4
) a
left join [Core].[DbEntityKinds] b on a.Id=b.Id where b.Id is null
GO
delete from Core.DbEntityMethods where DbEntityId = 14;
insert [Core].[DbEntityMethods]([Guid],[DbEntityId],[Name],[Method], [Schema], [ProcedureName])
select a.[Guid],a.[DbEntityId],a.[Name],a.[Method], a.[Schema], a.[ProcedureName] from
(select 'A263C106-0788-DD11-A4B1-000C6E4EC13D' as [Guid], 14 as [DbEntityId],'��������  �� ��������������' as [Name],'Load' as [Method], 'Core' as [Schema],  'ProjectItemLoad' as [ProcedureName]
union 
select 'A363C106-0788-DD11-A4B1-000C6E4EC13D', 14, '��� ������','LoadAll','Core','ProjectItemsLoadAll'
union 
select 'A463C106-0788-DD11-A4B1-000C6E4EC13D', 14, '��������','Delete','Core','ProjectItemDelete'
union 
select 'A563C106-0788-DD11-A4B1-000C6E4EC13D', 14, '��������','Create','Core','ProjectItemInsert'
union 
select 'A663C106-0788-DD11-A4B1-000C6E4EC13D', 14, '����������','Update','Core','ProjectItemUpdate'
union 
select 'A763C106-0788-DD11-A4B1-000C6E4EC13D', 14, '�������� �� ��������������','ExistsId','Core','ProjectItemExistsId'
union 
select 'A863C106-0788-DD11-A4B1-000C6E4EC13D', 14, '�������� �� ����������� ��������������','ExistsGuid','Core','ProjectItemExistsGuid'
union 
select 'A963C106-0788-DD11-A4B1-000C6E4EC13D', 14, '�������� �� ����������� ��������������','FindByName','Core','ProjectItemsFindByName'
union 
select 'AA63C106-0788-DD11-A4B1-000C6E4EC13D', 14, '�������','LoadTemplates','Core','ProjectItemsLoadTemplates'
union 
select 'AB63C106-0788-DD11-A4B1-000C6E4EC13D', 14, '��������� ���������','ChangeState','Core','ProjectItemChangeState'
union 
select 'C0421837-7489-DD11-A479-000C6E4EC13D', 14, '�������� ������ ��� ���������','LinkLoadSource','Core','ProjectItemChainsLoadBySource'
union 
select 'C1421837-7489-DD11-A479-000C6E4EC13D', 14, '�������� �����','LinkDelete','Core','ProjectItemChainDelete'
union 
select 'C2421837-7489-DD11-A479-000C6E4EC13D', 14, '�������� �����','LinkInsert','Core','ProjectItemChainInsert'
union 
select 'C3421837-7489-DD11-A479-000C6E4EC13D', 14, '���������� �����','LinkUpdate','Core','ProjectItemChainUpdate'

) a
left join [Core].[DbEntityMethods] b on a.Guid=b.Guid where b.Guid is null
GO
print '���� ������'
if not exists (select * from [Core].[DbEntities] where [Guid]='F0CE75EC-03E5-4AF9-AF7B-D6EDFF68CC3B')
INSERT INTO [Core].[DbEntities]([Id],[Guid],[Name],[Code])
VALUES(13,'F0CE75EC-03E5-4AF9-AF7B-D6EDFF68CC3B',N'���� ������', 
N'BusinessObjects.Rate, BusinessObjects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null')
GO
insert [Core].[DbEntityKinds]([Id],[Guid],[Name],[DbEntityId],[SubKind])
select a.[Id],a.[Guid],a.[Name],a.[DbEntityId],a.[SubKind] from
(select 851969 as Id,'4A7E72F0-F46E-DD11-9DD1-000C6E4EC13D' as Guid,'���� ������' as Name, 13 as DbEntityId, 1 as SubKind
) a
left join [Core].[DbEntityKinds] b on a.Id=b.Id where b.Id is null
GO
delete from Core.DbEntityMethods where DbEntityId = 13;
insert [Core].[DbEntityMethods]([Guid],[DbEntityId],[Name],[Method], [Schema], [ProcedureName])
select a.[Guid],a.[DbEntityId],a.[Name],a.[Method], a.[Schema], a.[ProcedureName] from
(select '0A43FA09-1A88-DD11-A4B1-000C6E4EC13D' as [Guid], 13 as [DbEntityId],'��������  �� ��������������' as [Name],'Load' as [Method], 'Core' as [Schema],  'RateLoad' as [ProcedureName]
union 
select '0B43FA09-1A88-DD11-A4B1-000C6E4EC13D', 13, '��� ������','LoadAll','Core','RatesLoadAll'
union 
select '0C43FA09-1A88-DD11-A4B1-000C6E4EC13D', 13, '��������','Delete','Core','RateDelete'
union 
select '0D43FA09-1A88-DD11-A4B1-000C6E4EC13D', 13, '��������','Create','Core','RateInsert'
union 
select '0E43FA09-1A88-DD11-A4B1-000C6E4EC13D', 13, '����������','Update','Core','RateUpdate'
union 
select '0F43FA09-1A88-DD11-A4B1-000C6E4EC13D', 13, '�������� �� ��������������','ExistsId','Core','RateExistsId'
union 
select '1043FA09-1A88-DD11-A4B1-000C6E4EC13D', 13, '�������� �� ����������� ��������������','ExistsGuid','Core','RateExistsGuid'
union 
select '1143FA09-1A88-DD11-A4B1-000C6E4EC13D', 13, '�������� �� ����������� ��������������','FindByName','Core','RatesFindByName'
union 
select '1243FA09-1A88-DD11-A4B1-000C6E4EC13D', 13, '�������','LoadTemplates','Core','RatesLoadTemplates'
union 
select '1343FA09-1A88-DD11-A4B1-000C6E4EC13D', 13, '��������� ���������','ChangeState','Core','RateChangeState'
union 
select 'C4421837-7489-DD11-A479-000C6E4EC13D', 13, '�������� ������ ��� ���������','LinkLoadSource','Core','RateChainsLoadBySource'
union 
select 'C5421837-7489-DD11-A479-000C6E4EC13D', 13, '�������� �����','LinkDelete','Core','RateChainDelete'
union 
select 'C6421837-7489-DD11-A479-000C6E4EC13D', 13, '�������� �����','LinkInsert','Core','RateChainInsert'
union 
select 'C7421837-7489-DD11-A479-000C6E4EC13D', 13, '���������� �����','LinkUpdate','Core','RateChainUpdate'

) a
left join [Core].[DbEntityMethods] b on a.Guid=b.Guid where b.Guid is null
GO
print '�� ���������'
if not exists (select * from [Core].[DbEntities] where [Guid]='D6396A74-AF2A-4C9A-AA24-FDA102DDAA43')
INSERT INTO [Core].[DbEntities]([Id],[Guid],[Name],[Code])
VALUES(10,'D6396A74-AF2A-4C9A-AA24-FDA102DDAA43',N'������� ���������', 
N'BusinessObjects.Unit, BusinessObjects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null')
GO
insert [Core].[DbEntityKinds]([Id],[Guid],[Name],[DbEntityId],[SubKind])
select a.[Id],a.[Guid],a.[Name],a.[DbEntityId],a.[SubKind] from
(select 655361 as Id,'477E72F0-F46E-DD11-9DD1-000C6E4EC13D' as Guid,'������� ���������' as Name, 10 as DbEntityId, 1 as SubKind
) a
left join [Core].[DbEntityKinds] b on a.Id=b.Id where b.Id is null
GO
delete from Core.DbEntityMethods where DbEntityId = 10;
insert [Core].[DbEntityMethods]([Guid],[DbEntityId],[Name],[Method], [Schema], [ProcedureName])
select a.[Guid],a.[DbEntityId],a.[Name],a.[Method], a.[Schema], a.[ProcedureName] from
(select 'BDD0BFD2-8C86-DD11-A0B1-000C6E4EC13D' as [Guid], 10 as [DbEntityId],'��������  �� ��������������' as [Name],'Load' as [Method], 'Core' as [Schema],  'UnitLoad' as [ProcedureName]
union 
select 'BCD0BFD2-8C86-DD11-A0B1-000C6E4EC13D', 10, '��� ������','LoadAll','Core','UnitsLoadAll'
union 
select 'BBD0BFD2-8C86-DD11-A0B1-000C6E4EC13D', 10, '��������','Delete','Core','UnitDelete'
union 
select 'BAD0BFD2-8C86-DD11-A0B1-000C6E4EC13D', 10, '��������','Create','Core','UnitInsert'
union 
select 'B9D0BFD2-8C86-DD11-A0B1-000C6E4EC13D', 10, '����������','Update','Core','UnitUpdate'
union 
select 'B8D0BFD2-8C86-DD11-A0B1-000C6E4EC13D', 10, '�������� �� ��������������','ExistsId','Core','UnitExistsId'
union 
select 'B7D0BFD2-8C86-DD11-A0B1-000C6E4EC13D', 10, '�������� �� ����������� ��������������','ExistsGuid','Core','UnitExistsGuid'
union 
select 'B6D0BFD2-8C86-DD11-A0B1-000C6E4EC13D', 10, '�������� �� ����������� ��������������','FindByName','Core','UnitsFindByName'
union 
select 'B5D0BFD2-8C86-DD11-A0B1-000C6E4EC13D', 10, '�������','LoadTemplates','Core','UnitsLoadTemplates'
union 
select 'B4D0BFD2-8C86-DD11-A0B1-000C6E4EC13D', 10, '��������� ���������','ChangeState','Core','UnitChangeState'
union 
select 'C71AABAB-0591-448B-A999-3FDC1480E111', 10, '������ ��������','FirstHierarchy','Hierarchy','UnitFirstHierarchy'
union 
select '93421837-7489-DD11-A479-000C6E4EC13D', 10, '��������� �������� � ��������','LoadByHierarchies','Hierarchy','UnitsLoadByHierarchies'
union 
select 'C8421837-7489-DD11-A479-000C6E4EC13D', 10, '�������� ������ ��� ���������','LinkLoadSource','Core','UnitChainsLoadBySource'
union 
select 'C9421837-7489-DD11-A479-000C6E4EC13D', 10, '�������� �����','LinkDelete','Core','UnitChainDelete'
union 
select 'CA421837-7489-DD11-A479-000C6E4EC13D', 10, '�������� �����','LinkInsert','Core','UnitChainInsert'
union 
select 'CB421837-7489-DD11-A479-000C6E4EC13D', 10, '���������� �����','LinkUpdate','Core','UnitChainUpdate'

) a
left join [Core].[DbEntityMethods] b on a.Guid=b.Guid where b.Guid is null
GO
print '��������'
if not exists (select * from [Core].[DbEntities] where [Guid]='6EEE3286-0272-DD11-B371-8000600FE800')
INSERT INTO [Core].[DbEntities]([Id],[Guid],[Name],[Code])
VALUES(20,'6EEE3286-0272-DD11-B371-8000600FE800',N'��������', 
N'BusinessObjects.Document, BusinessObjects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null')
GO
delete from Core.DbEntityMethods where DbEntityId = 20;
insert [Core].[DbEntityMethods]([Guid],[DbEntityId],[Name],[Method], [Schema], [ProcedureName])
select a.[Guid],a.[DbEntityId],a.[Name],a.[Method], a.[Schema], a.[ProcedureName] from
(select '615A2B1C-1288-DD11-A4B1-000C6E4EC13D' as [Guid], 20 as [DbEntityId],'��������  �� ��������������' as [Name],'Load' as [Method], 'Document' as [Schema],  'DocumentLoad' as [ProcedureName]
union 
select '585A2B1C-1288-DD11-A4B1-000C6E4EC13D', 20, '��� ������','LoadAll','Document','DocumentsLoadAll'
union 
select '595A2B1C-1288-DD11-A4B1-000C6E4EC13D', 20, '��������','Delete','Document','DocumentDelete'
union 
select '5A5A2B1C-1288-DD11-A4B1-000C6E4EC13D', 20, '��������','Create','Document','DocumentInsert'
union 
select '5B5A2B1C-1288-DD11-A4B1-000C6E4EC13D', 20, '����������','Update','Document','DocumentUpdate'
union 
select '5C5A2B1C-1288-DD11-A4B1-000C6E4EC13D', 20, '�������� �� ��������������','ExistsId','Document','DocumentExistsId'
union 
select '5D5A2B1C-1288-DD11-A4B1-000C6E4EC13D', 20, '�������� �� ����������� ��������������','ExistsGuid','Document','DocumentExistsGuid'
union 
select '5E5A2B1C-1288-DD11-A4B1-000C6E4EC13D', 20, '�������� �� ����������� ��������������','FindByName','Document','DocumentsFindByName'
union 
select '5F5A2B1C-1288-DD11-A4B1-000C6E4EC13D', 20, '�������','LoadTemplates','Document','DocumentsLoadTemplates'
union 
select '605A2B1C-1288-DD11-A4B1-000C6E4EC13D', 20, '��������� ���������','ChangeState','Document','DocumentChangeState'
union 
select 'CC421837-7489-DD11-A479-000C6E4EC13D', 20, '�������� ������ ��� ���������','LinkLoadSource','Document','DocumentChainsLoadBySource'
union 
select 'CD421837-7489-DD11-A479-000C6E4EC13D', 20, '�������� �����','LinkDelete','Document','DocumentChainDelete'
union 
select 'CE421837-7489-DD11-A479-000C6E4EC13D', 20, '�������� �����','LinkInsert','Document','DocumentChainInsert'
union 
select 'CF421837-7489-DD11-A479-000C6E4EC13D', 20, '���������� �����','LinkUpdate','Document','DocumentChainUpdate'

) a
left join [Core].[DbEntityMethods] b on a.Guid=b.Guid where b.Guid is null
GO
print '�����'
insert Core.FlagValues(Guid, Name, DbEntityId, Code, FlagValue, BranchId)
select a.Guid,a.Name,a.DbEntityId,a.Code, a.FlagValue, a.BranchId from
(select '4B421837-7489-DD11-A479-000C6E4EC13D' as Guid, 1 as DbEntityId, 'TEMPLATE' as Code, '������' as Name, 1 as FlagValue,  1 as BranchId
union 
select '4C421837-7489-DD11-A479-000C6E4EC13D', 1, 'READONLY', '������ ������', 2, 1
union 
select '4D421837-7489-DD11-A479-000C6E4EC13D', 1, 'HIDEN', '�������', 4, 1
union 
select '4E421837-7489-DD11-A479-000C6E4EC13D', 1, 'SYSTEM', '���������', 8, 1
union 
select '4F421837-7489-DD11-A479-000C6E4EC13D', 1, 'NONUSABLE', '�������� � �������������', 16, 1
) a
left join Core.FlagValues b on a.Guid=b.Guid where b.Guid is null
GO
insert Core.FlagValues(Guid, Name, DbEntityId, Code, FlagValue, BranchId)
select a.Guid,a.Name,a.DbEntityId,a.Code, a.FlagValue, a.BranchId from
(select '50421837-7489-DD11-A479-000C6E4EC13D' as Guid, 2 as DbEntityId, 'TEMPLATE' as Code, '������' as Name, 1 as FlagValue,  1 as BranchId
union 
select '51421837-7489-DD11-A479-000C6E4EC13DD', 2, 'READONLY', '������ ������', 2, 1
union 
select '52421837-7489-DD11-A479-000C6E4EC13D', 2, 'HIDEN', '�������', 4, 1
union 
select '53421837-7489-DD11-A479-000C6E4EC13D', 2, 'SYSTEM', '���������', 8, 1
union 
select '54421837-7489-DD11-A479-000C6E4EC13D', 2, 'NONUSABLE', '�������� � �������������', 16, 1
) a
left join Core.FlagValues b on a.Guid=b.Guid where b.Guid is null
GO
insert Core.FlagValues(Guid, Name, DbEntityId, Code, FlagValue, BranchId)
select a.Guid,a.Name,a.DbEntityId,a.Code, a.FlagValue, a.BranchId from
(select '3B7E72F0-F46E-DD11-9DD1-000C6E4EC13D' as Guid, 3 as DbEntityId, 'TEMPLATE' as Code, '������' as Name, 1 as FlagValue,  1 as BranchId
union 
select '46421837-7489-DD11-A479-000C6E4EC13D', 3, 'READONLY', '������ ������', 2, 1
union 
select '47421837-7489-DD11-A479-000C6E4EC13D', 3, 'HIDEN', '�������', 4, 1
union 
select '48421837-7489-DD11-A479-000C6E4EC13D', 3, 'SYSTEM', '���������', 8, 1
union 
select '4A421837-7489-DD11-A479-000C6E4EC13D', 3, 'NONUSABLE', '�������� � �������������', 16, 1
) a
left join Core.FlagValues b on a.Guid=b.Guid where b.Guid is null
GO
insert Core.FlagValues(Guid, Name, DbEntityId, Code, FlagValue, BranchId)
select a.Guid,a.Name,a.DbEntityId,a.Code, a.FlagValue, a.BranchId from
(select '55421837-7489-DD11-A479-000C6E4EC13D' as Guid, 4 as DbEntityId, 'TEMPLATE' as Code, '������' as Name, 1 as FlagValue,  1 as BranchId
union 
select '56421837-7489-DD11-A479-000C6E4EC13D', 4, 'READONLY', '������ ������', 2, 1
union 
select '57421837-7489-DD11-A479-000C6E4EC13D', 4, 'HIDEN', '�������', 4, 1
union 
select '58421837-7489-DD11-A479-000C6E4EC13D', 4, 'SYSTEM', '���������', 8, 1
union 
select '59421837-7489-DD11-A479-000C6E4EC13D', 4, 'NONUSABLE', '�������� � �������������', 16, 1
) a
left join Core.FlagValues b on a.Guid=b.Guid where b.Guid is null
GO
insert Core.FlagValues(Guid, Name, DbEntityId, Code, FlagValue, BranchId)
select a.Guid,a.Name,a.DbEntityId,a.Code, a.FlagValue, a.BranchId from
(select '5A421837-7489-DD11-A479-000C6E4EC13D' as Guid, 7 as DbEntityId, 'TEMPLATE' as Code, '������' as Name, 1 as FlagValue,  1 as BranchId
union 
select '5B421837-7489-DD11-A479-000C6E4EC13D', 7, 'READONLY', '������ ������', 2, 1
union 
select '5C421837-7489-DD11-A479-000C6E4EC13D', 7, 'HIDEN', '�������', 4, 1
union 
select '5D421837-7489-DD11-A479-000C6E4EC13D', 7, 'SYSTEM', '���������', 8, 1
union 
select '5E421837-7489-DD11-A479-000C6E4EC13D', 7, 'NONUSABLE', '�������� � �������������', 16, 1
) a
left join Core.FlagValues b on a.Guid=b.Guid where b.Guid is null
GO
insert Core.FlagValues(Guid, Name, DbEntityId, Code, FlagValue, BranchId)
select a.Guid,a.Name,a.DbEntityId,a.Code, a.FlagValue, a.BranchId from
(select '5F421837-7489-DD11-A479-000C6E4EC13D' as Guid, 24 as DbEntityId, 'TEMPLATE' as Code, '������' as Name, 1 as FlagValue,  1 as BranchId
union 
select '60421837-7489-DD11-A479-000C6E4EC13D', 24, 'READONLY', '������ ������', 2, 1
union 
select '61421837-7489-DD11-A479-000C6E4EC13D', 24, 'HIDEN', '�������', 4, 1
union 
select '62421837-7489-DD11-A479-000C6E4EC13D', 24, 'SYSTEM', '���������', 8, 1
union 
select '63421837-7489-DD11-A479-000C6E4EC13D', 24, 'NONUSABLE', '�������� � �������������', 16, 1
) a
left join Core.FlagValues b on a.Guid=b.Guid where b.Guid is null
GO
insert Core.FlagValues(Guid, Name, DbEntityId, Code, FlagValue, BranchId)
select a.Guid,a.Name,a.DbEntityId,a.Code, a.FlagValue, a.BranchId from
(select '64421837-7489-DD11-A479-000C6E4EC13D' as Guid, 5 as DbEntityId, 'TEMPLATE' as Code, '������' as Name, 1 as FlagValue,  1 as BranchId
union 
select '65421837-7489-DD11-A479-000C6E4EC13D', 5, 'READONLY', '������ ������', 2, 1
union 
select '66421837-7489-DD11-A479-000C6E4EC13D', 5, 'HIDEN', '�������', 4, 1
union 
select '67421837-7489-DD11-A479-000C6E4EC13D', 5, 'SYSTEM', '���������', 8, 1
union 
select '68421837-7489-DD11-A479-000C6E4EC13D', 5, 'NONUSABLE', '�������� � �������������', 16, 1
) a
left join Core.FlagValues b on a.Guid=b.Guid where b.Guid is null
GO
insert Core.FlagValues(Guid, Name, DbEntityId, Code, FlagValue, BranchId)
select a.Guid,a.Name,a.DbEntityId,a.Code, a.FlagValue, a.BranchId from
(select '69421837-7489-DD11-A479-000C6E4EC13D' as Guid, 6 as DbEntityId, 'TEMPLATE' as Code, '������' as Name, 1 as FlagValue,  1 as BranchId
union 
select '6A421837-7489-DD11-A479-000C6E4EC13D', 6, 'READONLY', '������ ������', 2, 1
union 
select '6B421837-7489-DD11-A479-000C6E4EC13D', 6, 'HIDEN', '�������', 4, 1
union 
select '6C421837-7489-DD11-A479-000C6E4EC13D', 6, 'SYSTEM', '���������', 8, 1
union 
select '6D421837-7489-DD11-A479-000C6E4EC13D', 6, 'NONUSABLE', '�������� � �������������', 16, 1
) a
left join Core.FlagValues b on a.Guid=b.Guid where b.Guid is null
GO
insert Core.FlagValues(Guid, Name, DbEntityId, Code, FlagValue, BranchId)
select a.Guid,a.Name,a.DbEntityId,a.Code, a.FlagValue, a.BranchId from
(select '6E421837-7489-DD11-A479-000C6E4EC13D' as Guid, 21 as DbEntityId, 'TEMPLATE' as Code, '������' as Name, 1 as FlagValue,  1 as BranchId
union 
select '6F421837-7489-DD11-A479-000C6E4EC13D', 21, 'READONLY', '������ ������', 2, 1
union 
select '71421837-7489-DD11-A479-000C6E4EC13D', 21, 'HIDEN', '�������', 4, 1
union 
select '72421837-7489-DD11-A479-000C6E4EC13D', 21, 'SYSTEM', '���������', 8, 1
union 
select '73421837-7489-DD11-A479-000C6E4EC13D', 21, 'NONUSABLE', '�������� � �������������', 16, 1
) a
left join Core.FlagValues b on a.Guid=b.Guid where b.Guid is null
GO
insert Core.FlagValues(Guid, Name, DbEntityId, Code, FlagValue, BranchId)
select a.Guid,a.Name,a.DbEntityId,a.Code, a.FlagValue, a.BranchId from
(select '74421837-7489-DD11-A479-000C6E4EC13D' as Guid, 15 as DbEntityId, 'TEMPLATE' as Code, '������' as Name, 1 as FlagValue,  1 as BranchId
union 
select '75421837-7489-DD11-A479-000C6E4EC13D', 15, 'READONLY', '������ ������', 2, 1
union 
select '76421837-7489-DD11-A479-000C6E4EC13D', 15, 'HIDEN', '�������', 4, 1
union 
select '77421837-7489-DD11-A479-000C6E4EC13D', 15, 'SYSTEM', '���������', 8, 1
union 
select '78421837-7489-DD11-A479-000C6E4EC13D', 15, 'NONUSABLE', '�������� � �������������', 16, 1
) a
left join Core.FlagValues b on a.Guid=b.Guid where b.Guid is null
GO
insert Core.FlagValues(Guid, Name, DbEntityId, Code, FlagValue, BranchId)
select a.Guid,a.Name,a.DbEntityId,a.Code, a.FlagValue, a.BranchId from
(select '79421837-7489-DD11-A479-000C6E4EC13D' as Guid, 8 as DbEntityId, 'TEMPLATE' as Code, '������' as Name, 1 as FlagValue,  1 as BranchId
union 
select '7A421837-7489-DD11-A479-000C6E4EC13D', 8, 'READONLY', '������ ������', 2, 1
union 
select '7B421837-7489-DD11-A479-000C6E4EC13D', 8, 'HIDEN', '�������', 4, 1
union 
select '7C421837-7489-DD11-A479-000C6E4EC13D', 8, 'SYSTEM', '���������', 8, 1
union 
select '7D421837-7489-DD11-A479-000C6E4EC13D', 8, 'NONUSABLE', '�������� � �������������', 16, 1
) a
left join Core.FlagValues b on a.Guid=b.Guid where b.Guid is null
GO
insert Core.FlagValues(Guid, Name, DbEntityId, Code, FlagValue, BranchId)
select a.Guid,a.Name,a.DbEntityId,a.Code, a.FlagValue, a.BranchId from
(select '7E421837-7489-DD11-A479-000C6E4EC13D' as Guid, 9 as DbEntityId, 'TEMPLATE' as Code, '������' as Name, 1 as FlagValue,  1 as BranchId
union 
select '7F421837-7489-DD11-A479-000C6E4EC13D', 9, 'READONLY', '������ ������', 2, 1
union 
select '80421837-7489-DD11-A479-000C6E4EC13D', 9, 'HIDEN', '�������', 4, 1
union 
select '81421837-7489-DD11-A479-000C6E4EC13D', 9, 'SYSTEM', '���������', 8, 1
union 
select '82421837-7489-DD11-A479-000C6E4EC13D', 9, 'NONUSABLE', '�������� � �������������', 16, 1
) a
left join Core.FlagValues b on a.Guid=b.Guid where b.Guid is null
GO
insert Core.FlagValues(Guid, Name, DbEntityId, Code, FlagValue, BranchId)
select a.Guid,a.Name,a.DbEntityId,a.Code, a.FlagValue, a.BranchId from
(select '83421837-7489-DD11-A479-000C6E4EC13D' as Guid, 14 as DbEntityId, 'TEMPLATE' as Code, '������' as Name, 1 as FlagValue,  1 as BranchId
union 
select '84421837-7489-DD11-A479-000C6E4EC13D', 14, 'READONLY', '������ ������', 2, 1
union 
select '85421837-7489-DD11-A479-000C6E4EC13D', 14, 'HIDEN', '�������', 4, 1
union 
select '86421837-7489-DD11-A479-000C6E4EC13D', 14, 'SYSTEM', '���������', 8, 1
union 
select '87421837-7489-DD11-A479-000C6E4EC13D', 14, 'NONUSABLE', '�������� � �������������', 16, 1
) a
left join Core.FlagValues b on a.Guid=b.Guid where b.Guid is null
GO
insert Core.FlagValues(Guid, Name, DbEntityId, Code, FlagValue, BranchId)
select a.Guid,a.Name,a.DbEntityId,a.Code, a.FlagValue, a.BranchId from
(select '88421837-7489-DD11-A479-000C6E4EC13D' as Guid, 10 as DbEntityId, 'TEMPLATE' as Code, '������' as Name, 1 as FlagValue,  1 as BranchId
union 
select '89421837-7489-DD11-A479-000C6E4EC13D', 10, 'READONLY', '������ ������', 2, 1
union 
select '8A421837-7489-DD11-A479-000C6E4EC13D', 10, 'HIDEN', '�������', 4, 1
union 
select '8B421837-7489-DD11-A479-000C6E4EC13D', 10, 'SYSTEM', '���������', 8, 1
union 
select '8C421837-7489-DD11-A479-000C6E4EC13D', 10, 'NONUSABLE', '�������� � �������������', 16, 1
) a
left join Core.FlagValues b on a.Guid=b.Guid where b.Guid is null
GO
insert Core.FlagValues(Guid, Name, DbEntityId, Code, FlagValue, BranchId)
select a.Guid,a.Name,a.DbEntityId,a.Code, a.FlagValue, a.BranchId from
(select '8D421837-7489-DD11-A479-000C6E4EC13D' as Guid, 20 as DbEntityId, 'TEMPLATE' as Code, '������' as Name, 1 as FlagValue,  1 as BranchId
union 
select '8E421837-7489-DD11-A479-000C6E4EC13D', 20, 'READONLY', '������ ������', 2, 1
union 
select '8F421837-7489-DD11-A479-000C6E4EC13D', 20, 'HIDEN', '�������', 4, 1
union 
select '90421837-7489-DD11-A479-000C6E4EC13D', 20, 'SYSTEM', '���������', 8, 1
union 
select '91421837-7489-DD11-A479-000C6E4EC13D', 20, 'NONUSABLE', '�������� � �������������', 16, 1
) a
left join Core.FlagValues b on a.Guid=b.Guid where b.Guid is null
GO