KindId & 0x0000FFFF,
(KindId & 0x00FF0000) / power(2,16)

declare @NewKey int
declare @key1 smallint
declare @key2 smallint
select @key1 = 1
select @key2 =1
select @key1 * power(2, 16) + @key2


declare @id bigint = 4294978444
	declare @c2 bigint = 2;
	select @id / power(@c2, 32); 
	select  @id & (power(@c2, 32) - 1);

--declare @id bigint = 4294978444	
--declare @c2 bigint = 2;
select @id & 0x0000000000FFFF,
(@id & 0x000000FF00000000) / power(@c2,32)

select 
cast(1 as int) * power(cast(2 as bigint), 32) + cast(AG_REALID as int)
from a2agent.AGENTS
declare @dbCode bigint = 1
declare @c2 bigint;
set @c2 = 2;
select @dbCode * power(cast(2 as bigint), 32) + 11148

declare @NewKey bigint
declare @key1 int
declare @key2 int
select @key1 = 1
select @key2 =11148
select @key1 * power(cast(2 as bigint), 32) + @key2


Äëÿ bigint

select 
(ag_id) / power(cast(2 as bigint),32),
ag_id & 0x0000000000FFFF,

ag_id / power(cast(2 as bigint), 32),
ag_id & (power(cast(2 as bigint), 32) - 1),

[a2sys].[dbid2string](ag_id),
*
from a2agent.AGENTS


--- SalesCenterDW
declare @NewKey bigint
declare @key1 int
declare @key2 int
select @key1 = 201 + 2011 * 10000 
select @key2 =11148
select @key1 
select @key1  * power(cast(2 as bigint), 32) + @key2
GO
declare @id bigint = 86372655610997644
	declare @c2 bigint = 2;
	select @id / power(@c2, 32); 
	select  @id & (power(@c2, 32) - 1);
	select @id / power(@c2, 32);
	select @id / power(@c2, 32)/10000
-- (isnull([BrancheId]*power(CONVERT([bigint],(2),(0)),(32))+[Id],(0)))
--(isnull([BrancheId]*power(CONVERT([bigint],(2),(0)),(32))+[Id],(0)))