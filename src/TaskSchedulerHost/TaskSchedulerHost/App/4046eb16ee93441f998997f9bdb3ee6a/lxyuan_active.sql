use[lxyuan_activity]

GO
----------------------------------------------------------
-- Procedure Name: cc p_gift_sort,2,0
-- Author: liubin
-- Date Generated: 2020年07月21日
-- Description: 礼包排序
----------------------------------------------------------

ALTER procedure [dbo].[p_gift_sort]
@rid int = 0,	--需要移动的id
@move int = 1,	--移动类型 -2:置底, -1:下移, 1:上移, 2置顶,
@outmsg varchar(128) = '' output
as
set nocount on
set transaction isolation level read uncommitted
set xact_abort on

declare @sort int = 0, @distsort int = 0, @distrid int = 0
select @sort = sort from t_gift where rid = @rid and status = 0
if @@ROWCOUNT <= 0
begin
    set @outmsg = '没有找到该礼包'
	return 0
end
if @move = -2
    select top 1 @distrid = rid, @distsort = sort from t_gift where status = 0 order by sort asc
else if @move = 1
    select top 1 @distrid = rid, @distsort = sort from t_gift where sort > @sort and status = 0 order by sort asc
else if @move = -1
    select top 1 @distrid = rid, @distsort = sort from t_gift where sort < @sort and status = 0 order by sort desc
else if @move = 2
    select top 1 @distrid = rid, @distsort = sort from t_gift where status = 0 order by sort desc
if @@ROWCOUNT <= 0
begin
    set @outmsg ='当前物品已经不能进行移动了'
	return 0
end

select * from t_gift

if @move = -2
begin
    update t_gift set sort = @distsort - 1 where rid = @rid
	if @@ROWCOUNT <= 0
    begin
        set @outmsg = '置底失败'
	    return 0
    end 
end
else if @move = 2
begin
    begin tran
	update t_gift set sort -= 1 where sort > @sort
	if @@ROWCOUNT <= 0
	begin
	    set @outmsg = '置顶失败'
		rollback tran
		return 0
	end
	update t_gift set sort = @distsort where rid = @rid
	if @@ROWCOUNT <= 0
	begin
	    set @outmsg = '置顶失败'
		rollback tran
		return 0
	end

	commit tran
end
else
begin
    begin tran
    update t_gift set sort = @distsort where rid = @rid
    if @@ROWCOUNT <= 0
    begin
        set @outmsg = '移动失败'
	    return 0
    end
    update t_gift set sort = @sort where rid = @distrid
    if @@ROWCOUNT <= 0
    begin
        set @outmsg = '移动失败'
	    rollback tran
	    return 0
    end
    commit tran
end

set @outmsg = '移动成功'
return 1