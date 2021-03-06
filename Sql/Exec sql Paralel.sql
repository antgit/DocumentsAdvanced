/*    
http://www.sqlservercentral.com/Forums/Topic779498-1630-1.aspx
    [Author]    James Ma
    
    [Disclaimer] The user of this code must take full responsibility for any changes to the system. The author is
     exempt from any liabilities if this script could cause any damage.
    
    [Setup]        Don't change anything here and make sure you're sysadmin. Then simply run this script. 
    
    [Smoke Test] Go to the last page of this script, highlight the smoke test script and run.
    
    [Uninstall] Go to the last page of this script, highlight the uninstallation script and run.
    
    [Usage] Simply init, exec sqls, wait, and end. For example,
    
        use AdventureWorks;
        declare @rc int;

        -- Call init before launch sqls
        exec @rc=sp_exec_init

        -- The following sqls will run in parallel
        if @rc=0 begin
            exec sp_exec 'select @@servername waitfor delay ''00:00:10'''
            exec sp_exec 'use AdventureWorks
                    select * from Person.Address
                    select @@servername waitfor delay ''00:00:10''',
            exec sp_exec 'select @@servername waitfor delay ''00:00:10'''
            exec sp_exec 'select @@servername waitfor delay ''00:00:10'''

            -- Wait until all 'done'.
            exec sp_exec_wait

            exec sp_exec 'select @@servername waitfor delay ''00:00:10'''
            exec sp_exec 'select @@servername waitfor delay ''00:00:10'''
        end
        
        exec sp_exec_end;
        
    
    [Change History]
         Dateâ€ˆ        Byâ€ˆ   Description
        --------    ------------    ------------------------------
        20090609â€ˆ    James Maâ€ˆ   Version 1.0.

*/

use master;
go
create database pmaster;
go
ALTER DATABASE [pmaster] SET RECOVERY SIMPLE WITH NO_WAIT
GO
use pmaster;
go
EXEC sp_grantlogin [NT Authority\System]
EXEC sp_addsrvrolemember @loginame = [NT Authority\System], @rolename = 'sysadmin'
Go
EXEC sp_changedbowner 'NT AUTHORITY\SYSTEM'
go
use master
go
ALTER DATABASE pmaster SET ENABLE_BROKER
alter database pmaster set trustworthy on
go


USE [pmaster]
GO
/****** Object: Table [dbo].[sysparameter] Script Date: 06/11/2009 15:58:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sysparameter](
    [parameter_name] [varchar](512) NOT NULL,
    [parameter_value] [varchar](max) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object: StoredProcedure [dbo].[p_printerror] Script Date: 06/11/2009 15:58:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[p_printerror]
AS
BEGIN
 SET NOCOUNT ON;

 -- Print error information. 
 PRINT 'Error ' + CONVERT(varchar(50), ERROR_NUMBER()) +
 ', Severity ' + CONVERT(varchar(5), ERROR_SEVERITY()) +
 ', State ' + CONVERT(varchar(5), ERROR_STATE()) + 
 ', Procedure ' + ISNULL(ERROR_PROCEDURE(), '-') + 
 ', Line ' + CONVERT(varchar(5), ERROR_LINE());
 PRINT ERROR_MESSAGE();
END;
GO
/****** Object: StoredProcedure [dbo].[_p_exec_dropqueue] Script Date: 06/11/2009 15:58:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[_p_exec_dropqueue]
     @master_spid smallint = null
    ,@options varchar(1024) = ''
as
set nocount on

set nocount on
if @master_spid is null set @master_spid=@@SPID;

declare @sql varchar(max) set @sql=replace('set nocount on;
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].[_p_exec_worker_<id>]'') AND type in (N''P'', N''PC''))
    DROP PROCEDURE [dbo].[_p_exec_worker_<id>]
IF EXISTS (SELECT * FROM sys.services WHERE name = N''//pmaster/exec/TargetService_<id>'')
    DROP SERVICE [//pmaster/exec/TargetService_<id>];
IF EXISTS (SELECT * FROM sys.service_queues WHERE name = N''pmaster_exec_TargetQueue_<id>'')
    DROP QUEUE [dbo].[pmaster_exec_TargetQueue_<id>];'
    ,'<id>',convert(varchar,@master_spid));
    
if @options like '%debug%' print @sql;
if @options not like '%printonly%' exec(@sql);
return @@error;
GO
/****** Object: StoredProcedure [dbo].[_p_exec_createqueue] Script Date: 06/11/2009 15:58:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[_p_exec_createqueue]
     @master_spid smallint = null
    ,@worker_num smallint = 4
    ,@options varchar(1024) = ''
as
set nocount on
if @master_spid is null set @master_spid=@@SPID;

declare @sql varchar(max) 
select @sql=replace(replace(replace(ROUTINE_DEFINITION
                ,'dbo._p_exec_worker','dbo._p_exec_worker_'+convert(varchar,@master_spid))
                ,'[dbo].[_p_exec_worker]','dbo._p_exec_worker_'+convert(varchar,@master_spid))
                ,'dbo.pmaster_exec_TargetQueue','dbo.pmaster_exec_TargetQueue_'+convert(varchar,@master_spid))
from INFORMATION_SCHEMA.ROUTINES where ROUTINE_SCHEMA='dbo' and ROUTINE_NAME='_p_exec_worker'
if @options like '%debug%' print @sql;
if @options not like '%printonly%' exec(@sql);

set @sql=replace(replace('set nocount on;
CREATE QUEUE pmaster_exec_TargetQueue_<id> WITH STATUS=ON,
    ACTIVATION (PROCEDURE_NAME=_p_exec_worker_<id>,MAX_QUEUE_READERS=<wn>,EXECUTE AS SELF);
CREATE SERVICE [//pmaster/exec/TargetService_<id>]
    ON QUEUE pmaster_exec_TargetQueue_<id>([//pmaster/exec/Contract]);'
    ,'<id>',convert(varchar,@master_spid))
    ,'<wn>',convert(varchar,@worker_num));
if @options like '%debug%' print @sql;
if @options not like '%printonly%' exec(@sql);
return @@error;
GO
/****** Object: Table [dbo].[exec_master] Script Date: 06/11/2009 15:58:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[exec_master](
    [master_spid] [smallint] NOT NULL,
    [worker_num] [smallint] NOT NULL,
    [create_time] [datetime] NOT NULL,
    [dialog_handle] [uniqueidentifier] NULL,
CONSTRAINT [PK_exec_master] PRIMARY KEY CLUSTERED 
(
    [master_spid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object: Table [dbo].[exec_log] Script Date: 06/11/2009 15:58:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[exec_log](
    [log_id] [int] IDENTITY(1,1) NOT NULL,
    [log_spid] [smallint] NOT NULL,
    [exec_queue_id] [bigint] NULL,
    [log_msg] [varchar](max) NULL,
    [log_time] [datetime] NOT NULL,
CONSTRAINT [PK_exec_log] PRIMARY KEY CLUSTERED 
(
    [log_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object: StoredProcedure [dbo].[p_exec_cleanqueue] Script Date: 06/11/2009 15:58:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[p_exec_cleanqueue]
as
set nocount on

declare @conversation uniqueidentifier;
while exists (select * from sys.conversation_endpoints where [state] in ('CD','DI')) begin
    set @conversation = (select top 1 conversation_handle from sys.conversation_endpoints where [state] in ('CD','DI'))
    end conversation @conversation with cleanup
end

return @@error
GO
/****** Object: UserDefinedFunction [dbo].[f_exec_option] Script Date: 06/11/2009 15:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[f_exec_option](@options varchar(max),@arg varchar(512)) returns varchar(512)
as
begin
    declare @startPos int,@endPos int
    set @startPos=charindex(@arg,@options,1)
    if @startPos=0 return null
    set @endPos=charindex(';',@options,@startPos)
    if (@endPos=0) set @endPos=len(@options)+1
    return substring(@options,@startPos+len(@arg)+1,(@endPos-@startPos-len(@arg)-1))
end
GO
/****** Object: Table [dbo].[exec_queue] Script Date: 06/11/2009 15:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[exec_queue](
    [exec_queue_id] [bigint] IDENTITY(-9223372036854775808,1) NOT NULL,
    [master_spid] [smallint] NOT NULL,
    [worker_spid] [smallint] NULL,
    [wait_type] [tinyint] NOT NULL,
    [create_time] [datetime] NOT NULL,
    [send_time] [datetime] NULL,
    [worker_start_time] [datetime] NULL,
    [worker_end_time] [datetime] NULL,
    [return_code] [int] NULL,
    [return_msg] [nvarchar](max) NULL,
CONSTRAINT [PK_exec_queue] PRIMARY KEY CLUSTERED 
(
    [exec_queue_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_exec_queue_master_spid] ON [dbo].[exec_queue] 
(
    [master_spid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object: StoredProcedure [dbo].[p_exec_log] Script Date: 06/11/2009 15:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[p_exec_log]
     @exec_queue_id bigint
    ,@msg nvarchar(max)
as
set nocount on

insert dbo.exec_log(log_spid,exec_queue_id,log_msg,log_time)
select @@SPID,@exec_queue_id,@msg,GETDATE()

return @@error
GO
/****** Object: StoredProcedure [dbo].[_p_exec_worker] Script Date: 06/11/2009 15:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[_p_exec_worker]
as
set nocount on

DECLARE @RecvReqDlgHandle UNIQUEIDENTIFIER;
DECLARE @RecvReqMsg NVARCHAR(max); set @RecvReqMsg=null
DECLARE @RecvReqMsgName sysname;

BEGIN TRANSACTION;
WAITFOR(RECEIVE TOP(1)
            @RecvReqDlgHandle = conversation_handle,
            @RecvReqMsg = message_body,
            @RecvReqMsgName = message_type_name
        FROM dbo.pmaster_exec_TargetQueue
    ), TIMEOUT 1000;
COMMIT TRANSACTION;

IF (@RecvReqMsgName = N'//pmaster/exec/RequestMessage') AND (@RecvReqMsg is not null) 
        and (@RecvReqMsg like '--//pmaster/exec/request:%') 
begin
    declare @header varchar(2048),@qid bigint,@msg nvarchar(max),@rc int;
    ----//pmaster/exec/request: is the prefix
    set @header=substring(@RecvReqMsg,26,charindex(N'--',@RecvReqMsg,26)-26);
    set @qid=dbo.f_exec_option(@header,'@exec_queue_id');
    update dbo.exec_queue set worker_spid=@@SPID,worker_start_time=GETDATE() where exec_queue_id=@qid;

    begin try
        --exec dbo.p_exec_log @RecvReqMsg
        exec @rc=sp_executesql @RecvReqMsg;
        set @msg='';
    end try
    begin catch
        select @rc=ERROR_NUMBER(),@msg=ERROR_MESSAGE();
        exec dbo.p_exec_log @qid,@RecvReqMsg
    end catch

    update dbo.exec_queue set return_code=@rc,return_msg=@msg,worker_end_time=GETDATE() where exec_queue_id=@qid;
end

return @@error
GO
/****** Object: StoredProcedure [dbo].[_p_exec_clean] Script Date: 06/11/2009 15:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[_p_exec_clean]
     @master_spid smallint = null
    ,@options varchar(1024) = ''
as
set nocount on
if @master_spid is null set @master_spid=@@SPID;

declare @conversation uniqueidentifier

select @conversation=dialog_handle from dbo.exec_master where master_spid=@master_spid;
if @conversation is not null end conversation @conversation with cleanup;
update dbo.exec_master set dialog_handle=null where master_spid=@master_spid

exec dbo._p_exec_dropqueue @master_spid,@options

delete from dbo.exec_queue where master_spid=@master_spid
delete from dbo.exec_master where master_spid=@master_spid

while exists (select * from sys.conversation_endpoints where [state] in ('CD','DI')) begin
    set @conversation = (select top 1 conversation_handle from sys.conversation_endpoints where [state] in ('CD','DI'))
    end conversation @conversation with cleanup
end

return @@error
GO
/****** Object: StoredProcedure [dbo].[p_exec] Script Date: 06/11/2009 15:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[p_exec]
/*
    [Purpose]    Execute sql in parallel
    
    [Owner]        James Ma
    
    [Usage in an example] Simply init, exec sqls, and wait.
    
        use AdventureWorks;
        declare @rc int,@db varchar(256) set @db=DB_NAME();

        -- init before launch sqls
        exec @rc=sp_exec_init

        -- The following sqls will run in parallel
        if @rc=0 begin
            exec sp_exec 'select @@servername waitfor delay ''00:00:10''',0,@db
            exec sp_exec 'use AdventureWorks
                    select * from Person.Address
                    select @@servername waitfor delay ''00:00:10''',1
            exec sp_exec 'select @@servername waitfor delay ''00:00:10''',2
            exec sp_exec 'select @@servername waitfor delay ''00:00:10''',2

            -- Wait until all 'done'.
            exec sp_exec_wait
        end
        
        -- end the exec in the end
        exec sp_exec_end
    
    
    [Change History]
         Dateâ€ˆ        Byâ€ˆ   Description
        --------    ------------    ------------------------------
        20090609â€ˆ       James Maâ€ˆ   Version 1.0.

*/
     @sql nvarchar(max)
    ,@wait_type tinyint = 2   -- 0: send only and no wait; 
                            -- 1: wait till worker starts;
                            -- 2: wait till worker finishes.
    ,@db varchar(256) = null
as
set nocount on
declare @master_spid smallint set @master_spid=@@SPID;
declare @rc int set @rc=null;
DECLARE @InitDlgHandle UNIQUEIDENTIFIER,@exec_queue_id bigint,@header varchar(2048)

begin try
    begin transaction;
    
    insert dbo.exec_queue(master_spid,wait_type,create_time)
    select @master_spid,@wait_type,GETDATE();
    set @exec_queue_id=SCOPE_IDENTITY();
    declare @tmp nvarchar(2048)
    
    select @InitDlgHandle=dialog_handle from dbo.exec_master where master_spid=@master_spid
    if @InitDlgHandle is null begin
        set @tmp=replace('set nocount on
BEGIN DIALOG @InitDlgHandle
    FROM SERVICE [//pmaster/exec/InitiatorService]
    TO SERVICE N''//pmaster/exec/TargetService_<Id>''
    ON CONTRACT [//pmaster/exec/Contract]
    WITH ENCRYPTION = OFF;','<Id>',@master_spid);
        exec sp_executesql @tmp,N'@InitDlgHandle UNIQUEIDENTIFIER output',@InitDlgHandle output;
        
        update dbo.exec_master set dialog_handle=@InitDlgHandle where master_spid=@master_spid
    end;
    
    set @sql=replace(replace(replace(replace(replace(
N'--//pmaster/exec/request:@exec_queue_id=<qid>;@master_spid=<id>;@wait_type=<wt>--
use [<db>];
<sql>'
,'<id>',convert(varchar,@master_spid)),'<qid>',CONVERT(varchar,@exec_queue_id)),'<wt>',CONVERT(varchar,@wait_type))
,'<db>',isnull(@db,DB_NAME())),'<sql>',@sql);

    SEND ON CONVERSATION @InitDlgHandle MESSAGE TYPE [//pmaster/exec/RequestMessage](@sql);
    update dbo.exec_queue set send_time=getdate() where exec_queue_id=@exec_queue_id;
    commit transaction;
    set @rc=@@error;
end try
begin catch
    exec dbo.p_printerror;
    set @rc=ERROR_NUMBER();
    IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
end catch

return @rc
GO
/****** Object: StoredProcedure [dbo].[p_exec_wait] Script Date: 06/11/2009 15:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[p_exec_wait]
as
set nocount on
declare @master_spid smallint set @master_spid=@@SPID

while exists(select * from dbo.exec_queue where master_spid=@master_spid 
                and (    (wait_type=0 and worker_spid is null) or
                        (wait_type=1 and worker_start_time is null) or
                        (wait_type=2 and worker_end_time is null)))
    waitfor delay '00:00:03';
    
exec dbo.p_exec_cleanqueue;

return @@error
GO
/****** Object: StoredProcedure [dbo].[p_exec_reset] Script Date: 06/11/2009 15:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[p_exec_reset]
as
set nocount on

declare @rc int set @rc=null;

BEGIN TRY
    declare @master_spid smallint
    
    BEGIN TRANSACTION;
    
    declare curId cursor local forward_only 
        for select master_spid from dbo.exec_master
    
    open curId
    fetch next from curId into @master_spid
    while (@@FETCH_STATUS=0) begin
        exec dbo._p_exec_clean @master_spid,''
        fetch next from curId into @master_spid
    end
    
    COMMIT TRANSACTION;
    set @rc=0;
END TRY
BEGIN CATCH
 exec dbo.p_printerror;
    set @rc = -1;
    IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
END CATCH;

exec dbo.p_exec_cleanqueue;

return @rc
GO
/****** Object: StoredProcedure [dbo].[p_exec_init] Script Date: 06/11/2009 15:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[p_exec_init]
     @worker_num smallint = 4
as
set nocount on
declare @master_spid smallint set @master_spid=@@SPID 

exec dbo.p_exec_cleanqueue;

--last session has not ended yet, cannot proceed!
if exists(select * from dbo.exec_queue where master_spid=@master_spid and worker_spid is null)
begin
    RAISERROR ('Previous running has not finished, cannot init the pmaster exec system!',16,1);
    return -1;
end

if (@worker_num<1 or @worker_num>16) 
begin
    RAISERROR ('@worker_num parameter must be between 1 and 16.',16,1);
    return -1;
end

declare @rc int set @rc=null

begin try
    begin transaction;
    exec dbo._p_exec_clean @master_spid,''
    exec dbo._p_exec_createqueue @master_spid,@worker_num,'';
    insert dbo.exec_master(master_spid,worker_num,create_time)
    select @master_spid,@worker_num,GETDATE();
    commit transaction;
    set @rc=@@error;
end try
begin catch
    exec dbo.p_printerror;
    set @rc=-1;
    IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
end catch

return @rc;
GO
/****** Object: StoredProcedure [dbo].[p_exec_end] Script Date: 06/11/2009 15:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[p_exec_end]
as
set nocount on
declare @master_spid smallint set @master_spid=@@SPID

while exists(select * from dbo.exec_queue where master_spid=@master_spid and worker_spid is null)
    waitfor delay '00:00:03';
    
begin try
    begin transaction;
    exec dbo._p_exec_clean @master_spid,'';
    commit transaction;
end try
begin catch
    exec dbo.p_printerror;
    IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
    return Error_number();
end catch

exec dbo.p_exec_cleanqueue;

return @@error
GO
/****** Object: ForeignKey [FK_exec_queue_exec_master] Script Date: 06/11/2009 15:58:26 ******/
ALTER TABLE [dbo].[exec_queue] WITH CHECK ADD CONSTRAINT [FK_exec_queue_exec_master] FOREIGN KEY([master_spid])
REFERENCES [dbo].[exec_master] ([master_spid])
GO
ALTER TABLE [dbo].[exec_queue] CHECK CONSTRAINT [FK_exec_queue_exec_master]
GO


USE pmaster
GO
CREATE MESSAGE TYPE [//pmaster/exec/RequestMessage] VALIDATION = NONE;
CREATE MESSAGE TYPE [//pmaster/exec/ReplyMessage] VALIDATION = NONE;
GO

CREATE CONTRACT [//pmaster/exec/Contract](
    [//pmaster/exec/RequestMessage] SENT BY INITIATOR,
    [//pmaster/exec/ReplyMessage] SENT BY TARGET);
GO

CREATE QUEUE pmaster_exec_TargetQueue WITH STATUS=ON,
    ACTIVATION (PROCEDURE_NAME=_p_exec_worker,MAX_QUEUE_READERS=16,EXECUTE AS SELF );
CREATE SERVICE [//pmaster/exec/TargetService] ON QUEUE pmaster_exec_TargetQueue([//pmaster/exec/Contract]);
GO
CREATE QUEUE pmaster_exec_InitiatorQueue;
CREATE SERVICE [//pmaster/exec/InitiatorService] ON QUEUE pmaster_exec_InitiatorQueue;
GO


use master
go

create procedure dbo.sp_exec_init
    @worker_num smallint = 4
as
set nocount on;
declare @rc int;
exec @rc=pmaster.dbo.p_exec_init @worker_num;
return @rc;
go
EXECUTE sp_ms_marksystemobject 'sp_exec_init'
go

create procedure dbo.sp_exec
/*
    [Purpose]    Execute sql in parallel
    
    [Owner]        James Ma
    
    [Usage in an example] Simply init, exec sqls, and wait.
    
        use AdventureWorks;
        declare @rc int,@db varchar(256) set @db=DB_NAME();

        -- Call init before launch sqls
        exec @rc=sp_exec_init

        -- The following sqls will run in parallel
        if @rc=0 begin
            exec sp_exec 'select @@servername waitfor delay ''00:00:10''',0,@db
            exec sp_exec 'use AdventureWorks
                    select * from Person.Address
                    select @@servername waitfor delay ''00:00:10''',1
            exec sp_exec 'select @@servername waitfor delay ''00:00:10''',2
            exec sp_exec 'select @@servername waitfor delay ''00:00:10''',2

            -- Wait until all 'done'.
            exec sp_exec_wait;
        end
        
        exec sp_exec_end;
        
        
        
    [Change History]
         Dateâ€ˆ        Byâ€ˆ   Description
        --------    ------------    ------------------------------
        20090609â€ˆ       James Maâ€ˆ   Version 1.0.

*/
     @sql nvarchar(max)
    ,@wait_type tinyint = 2   -- 0: send only and no wait; 
                            -- 1: wait till worker starts;
                            -- 2: wait till worker finishes.
    ,@db varchar(256) = null
as
set nocount on;
declare @rc int;
exec @rc=pmaster.dbo.p_exec @sql,@wait_type,@db;
return @rc;
go
EXECUTE sp_ms_marksystemobject 'sp_exec'
go

create procedure dbo.sp_exec_wait
as
set nocount on;
declare @rc int;
exec @rc=pmaster.dbo.p_exec_wait;
return @rc;
go
EXECUTE sp_ms_marksystemobject 'sp_exec_wait'
go

create procedure dbo.sp_exec_end
as
set nocount on;
declare @rc int;
exec @rc=pmaster.dbo.p_exec_end;
return @rc;
go
EXECUTE sp_ms_marksystemobject 'sp_exec_end'
go


/* Smoke Test

use tempdb

declare @rc int
exec @rc=sp_exec_init 8
if (@rc=0) begin
    exec sp_exec 'select @@servername waitfor delay ''00:00:30'''
    exec sp_exec 'select @@servername waitfor delay ''00:00:30'''
    exec sp_exec 'select @@servername waitfor delay ''00:00:30'''
    exec sp_exec 'select @@servername waitfor delay ''00:00:30'''
    exec sp_exec_wait

    exec sp_exec 'select @@servername waitfor delay ''00:00:30'''
    exec sp_exec 'select @@servername waitfor delay ''00:00:30'''
    exec sp_exec_wait
end
exec sp_exec_end

--use another window to monitor the progress
select * from pmaster.sys.conversation_endpoints
select * from pmaster.dbo.exec_queue
select * from pmaster.dbo.exec_log

*/


/* Uninstall & Cleanup

    [Disclaimer] The user of this code must take full responsibility for any changes to the system. The author is
     exempt from any liability if this script could cause any damage.
     
    [Warning] Are you sure of what you are going to drop?

------use master
------go

------drop procedure dbo.sp_exec 
------drop procedure dbo.sp_exec_init 
------drop procedure dbo.sp_exec_wait
------drop procedure dbo.sp_exec_end
------go

------DROP DATABASE [pmaster]
------GO

*/






