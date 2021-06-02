use[lxyuan_giftcenter]

GO
----------------------------------------------------------    
-- function Name: cc f_stone_tovarchar
-- Author: zhongzhenyu    
-- Date Generated: 2017年11月7日    
-- Description: 格式化乐币    
----------------------------------------------------------    
create function [dbo].[f_stone_tovarchar](@instone bigint)returns varchar(128)
as  
begin
    declare @charstone varchar(128),@temp1 decimal(16,2),@temp2 decimal(16,2)  
    if @instone>=100000000  
    begin  
        if @instone%100000000=0  
            set @charstone=CONVERT(varchar,@instone/100000000)+'亿'  
        else if @instone%10000000=0  
        begin  
            set @temp1=@instone/100000000.0  
            set @charstone=CONVERT(varchar,@temp1)+'亿'  
        end  
        else  
        begin  
            set @temp2=@instone/100000000.00  
            set @charstone=CONVERT(varchar,@temp2)+'亿'  
        end  
    end  
    else if @instone>=10000  
    begin  
        if @instone%10000=0  
            set @charstone=CONVERT(varchar,@instone/10000)+'万'  
        else if @instone%1000=0  
        begin  
            set @temp1=@instone/10000.0  
            set @charstone=CONVERT(varchar,@temp1)+'万'  
        end  
        else  
        begin  
            set @temp2=@instone/10000.00  
            set @charstone=CONVERT(varchar,@temp2)+'万'  
        end  
    end  
    else  
        set @charstone=CONVERT(varchar,@instone)  
            
    return @charstone
end

GO
----------------------------------------------------------  
-- Procedure Name: cc p_job_assign_bynewuser,0,1  
-- Author: liubin  
-- Date Generated: 2020年10月20日  
-- Description: 把符合条件的新用户加入到指定礼包列表里  
----------------------------------------------------------  
  
ALTER procedure [dbo].[p_job_assign_bynewuser]  
as  
set nocount on  
set transaction isolation level read uncommitted  
set xact_abort ON  
  
declare @date date = getdate(), @gid int = 106, @count int = 0, @i int = 0, @userid int = 0, @point int = 0  
declare @content varchar(1024) = '恭喜您获得首充福利礼包特权！(url=http://cz.lexun.com/WelfarePackage/index.php?)原价17元礼包，您只要10元即可购买！仅限今日！购买后立刻获赠【限定乐秀x30天、10亿乐币】！(/url)心动不如立即行动吧！'  
create table #tb (rid int identity primary key,  userid int)  
  
--判断106礼包是否下架  
if exists (select 1 from t_gift where rid = @gid and starttime <= getdate() and endtime > getdate() and status = 0)  
begin
    --查询符合条件的用户  
    insert into #tb(userid)  
    select userid from LinkSRV214.lxmyzone.dbo.t_user_lastlogin where lastlogindate >= @date  
    and userid not in (select userid from t_user_gift_log group by userid)  
    and userid not in (select userid from t_user_gift_assign where gid = @gid) and userid >= 10000
	set @count = @@ROWCOUNT  
    --添加到目标礼包  
    insert into t_user_gift_assign(gid, userid) select @gid, userid from #tb 
    --发内线  
    while @i < @count  
    begin  
        set @i += 1  
        select @userid = userid from #tb where rid = @i
		if not exists(select 1 from t_rejectmsg_user where userid = @userid)
           exec AsynProcQueue.dbo.p_if_sendmsg 10018,@userid,'','','',1,@content,1,0,'福利礼包中心新用户提示','lxyuan_giftcenter','p_job_assign_bynewuser'  
    end  
end

--每月份未购礼包用户福利
set @gid = 285
declare @month date = convert(char(8),getdate(),120) + '01'
declare @money decimal(9,2) = 0, @virmoney decimal(9,2) = 0, @stone bigint = 0
select @money = price, @virmoney = virprice, @stone = stone from t_gift g join t_gift_extra e on g.rid = e.gid where rid = @gid

set @i = 0
set @content = '恭喜您获得'+ltrim(datepart(mm,@month))+'月首充礼包特权！原价'+ltrim(convert(int, @virmoney))+'元，(u)您只要'+ltrim(convert(int, @money))+'元即可购买【'+ltrim(datepart(mm,@month))+'月乐秀-喜剧之王】！且额外获赠【'+dbo.f_stone_tovarchar(@stone)+'乐币】(/u)！限量出售，先到先得！(url=http://cz.lexun.com/WelfarePackage/index.php?)点此立即前往购买(/url)'
truncate table #tb
--判断285礼包是否下架 
if exists (select 1 from t_gift where rid = @gid and starttime <= getdate() and endtime > getdate() and status = 0)  
begin
    --查询符合条件的用户
	insert into #tb(userid) select userid from LinkSRV214.lxmyzone.dbo.t_user_lastlogin where lastlogindate >= @date
	and userid not in (select userid from t_user_gift_log where dateflag >= @month and dateflag < dateadd(MM, 1, @month) group by userid)
	and userid not in (select userid from t_user_gift_assign where gid = @gid) and userid >= 10000
	set @count = @@ROWCOUNT  
    --添加到目标礼包  
    insert into t_user_gift_assign(gid, userid) select @gid, userid from #tb 
    --发内线  
    while @i < @count  
    begin  
        set @i += 1  
        select @userid = userid from #tb where rid = @i
		exec LinkSRV9.gamehotelpage.dbo.p_active_user_getpoint @userid, 0, @point out
		if @point >= 10 and not exists(select 1 from t_rejectmsg_user where userid = @userid)
            exec AsynProcQueue.dbo.p_if_sendmsg 10018,@userid,'','','',1,@content,1,0,'福利礼包中心新用户提示','lxyuan_giftcenter','p_job_assign_bynewuser' 
    end  
end

go