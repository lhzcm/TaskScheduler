use[Magnate]
GO
alter table t_experience_user_mission add roundid int not null default 0
alter table t_experience_user_mission add restone bigint not null default 0
GO
truncate table t_experience_mission_info
GO
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (1, 1, N'今日使用Y币投资(url=plansyslist.aspx?planid=1&missflag=1)【天下商队】(/url)一次', 0, 100000000, 1, 1, 0, N'阶段任务', N'plansyslist.aspx?planid=1&missflag=1')
GO
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (2, 2, N'今日使用至少2亿Y币投资(url=plansyslist.aspx?planid=8&missflag=1)【皇庆商队】(/url)*5次', 200000000, 200000000, 1, 1, 0, N'阶段任务', N'plansyslist.aspx?planid=8&missflag=1')
GO
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (3, 3, N'今日单次只投资任意商队，连续两次踩中畅销货物', 2, 300000000, 1, 1, 0, N'阶段任务', N'plansyslist.aspx?missflag=1')
GO
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (4, 4, N'今日选择(url=winrate.aspx?missflag=1)自由投资(/url)，投资10亿Y币（可选1个或多个货物）', 1000000000, 300000000, 1, 1, 0, N'阶段任务', N'winrate.aspx?missflag=1')
GO
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (5, 5, N'今日仅使用自己设置的(url=planuserlist.aspx?missflag=1)货单投资(/url)，单期投资≥20亿Y币，并且盈利', 2000000000, 300000000, 1, 1, 0, N'阶段任务', N'planuserlist.aspx?missflag=1') --奖励调整
GO
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (6, 6, N'活动期间累计产出300亿Y币', 30000000000, 500000000, 1, 1, 0, N'阶段任务', N'') --任务数值调整
GO
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (7, 7, N'今日单笔盈利50亿Y币', 5000000000, 500000000, 1, 1, 0, N'阶段任务', N'')--奖励调整
GO
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (8, 8, N'今日单笔盈利100亿Y币', 10000000000, 600000000, 1, 1, 0, N'阶段任务', N'')--任务数值和奖励调整
GO
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (9, 9, N'今日单笔盈利300亿Y币', 30000000000, 700000000, 1, 1, 0, N'阶段任务', N'')--任务数值和奖励调整
GO
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (10, 10, N'活动期间累计产出6000亿Y币', 600000000000, 1500000000, 1, 1, 0, N'阶段任务', N'')
GO
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (11, 1, N'今日累计获得10亿Y币', 500000000, 200000000, 2, 1, 0, N'阶段任务', N'')
GO
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (12, 2, N'今日连续踩中两次畅销货物【最多可以投资20种】', 2, 200000000, 2, 1, 0, N'阶段任务', N'')
GO
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (13, 3, N'今日连续踩中三次畅销货物【最多可以投资20种】', 3, 300000000, 2, 1, 0, N'阶段任务', N'')
GO
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (14, 4, N'今日单笔盈利50亿', 5000000000, 300000000, 2, 1, 0, N'阶段任务', N'')
GO
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (15, 5, N'今日仅使用货单投资，单笔盈利100亿', 10000000000, 600000000, 2, 1, 0, N'阶段任务', N'')
GO
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (16, 6, N'运用自己的策略投资，今日单笔盈利200亿', 20000000000, 600000000, 2, 1, 0, N'阶段任务', N'')
GO
--INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (17, 7, N'运用自己的策略投资，今日单笔盈利500亿', 50000000000, 700000000, 2, 1, 0, N'阶段任务', N'')
GO
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (18, 8, N'今日在大亨单笔盈利2000亿Y币', 200000000000, 2000000000, 2, 1, 0, N'阶段任务', N'')
GO
--INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (19, 1, N'今日乐币累计投入30亿', 3000000000, 300000000, 1, 3, 0, N'乐币任务', N'')
GO
--INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (20, 2, N'今日乐币累计盈利10亿', 1000000000, 500000000, 1, 3, 0, N'乐币任务', N'')
GO
--INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (21, 3, N'今日乐币累计盈利30亿', 3000000000, 800000000, 1, 3, 0, N'乐币任务', N'')
GO
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (22, 1, N'单次累计使用5亿乐币投资', 500000000, 0, 1, 4, 10000000000, N'隐藏任务', N'')
GO
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (23, 2, N'在大亨用乐币任意投资', 0, 0, 1, 4, 0, N'隐藏任务', N'')
GO
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (24, 3, N'在大亨用乐币任意投资', 0, 17000000000, 1, 4, 3000000000, N'隐藏任务', N'')
GO
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (25, 1, N'在大亨任意投入1亿以上乐币', 100000000, 10000000000, 3, 4, 3, N'隐藏任务', N'')
GO
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (26, 2, N'在大亨用乐币任意投资', 0, 45000000000, 3, 4, 5000000000, N'隐藏任务', N'')
GO
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (27, 3, N'今日使用乐币投资5局', 5, 200000000, 3, 4, 50000000000, N'隐藏任务', N'')
GO
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (28, 4, N'在大亨用乐币任意投资', 0, 0, 3, 4, -10000000000, N'隐藏任务', N'')
GO
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (29, 5, N'将当天乐币盈亏扭转为正', 0, 1000000000, 3, 4, -2000000000, N'隐藏任务', N'')
GO
--活动期间累计产出600亿Y币
update t_experience_mission_info set missionid += 1 where userflag = 1 and missionflag = 1 and missionid >= 8
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (30, 8, N'活动期间累计产出600亿Y币', 60000000000, 600000000, 1, 1, 0, N'阶段任务', N'')
GO

--今日累计产出500亿Y币
update t_experience_mission_info set missionid += 1 where userflag = 2 and missionflag = 1 and missionid >= 5
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (31, 5, N'今日累计产出500亿Y币', 50000000000, 300000000, 2, 1, 0, N'阶段任务', N'')

go
--今日累计产出800亿Y币
update t_experience_mission_info set missionid += 1 where userflag = 2 and missionflag = 1 and missionid >= 7
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (32, 7, N'今日累计产出800亿Y币', 80000000000, 500000000, 2, 1, 0, N'阶段任务', N'')

go
--今日累计投资50亿乐币
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (33, 11, N'今日累计投资50亿乐币', 5000000000, 200000000, 2, 1, 0, N'阶段任务', N'')
go
--今日累计投资100亿乐币
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (34, 12, N'今日累计投资100亿乐币', 10000000000, 300000000, 2, 1, 0, N'阶段任务', N'')
go
--今日单笔投资50亿乐币且盈利
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (35, 13, N'今日单笔投资50亿乐币且盈利', 5000000000, 500000000, 2, 1, 0, N'阶段任务', N'')
go
--今日单笔投资100亿乐币且盈利
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (36, 14, N'今日单笔投资100亿乐币且盈利', 10000000000, 500000000, 2, 1, 0, N'阶段任务', N'')
go
--今日单笔盈利500亿乐币
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (37, 15, N'今日单笔盈利500亿乐币', 50000000000, 1500000000, 2, 1, 0, N'阶段任务', N'')
go

--弹窗
go
--单笔盈利500亿乐币
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (38, 6, N'单笔盈利500亿乐币', 50000000000, 0, 3, 4, 0, N'隐藏任务', N'')
go
--返还乐币
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (39, 7, N'福利返还', 0, 5000000000, 3, 4, 0, N'隐藏任务', N'')
go
--单笔盈利50亿乐币
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (40, 4, N'单笔盈利50亿乐币', 5000000000, 0, 1, 4, 0, N'隐藏任务', N'')
go

GO
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (41, 12, N'今日乐币累计投入30亿', 3000000000, 300000000, 1, 1, 0, N'阶段任务', N'')
GO
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (42, 13, N'今日乐币累计盈利10亿', 1000000000, 500000000, 1, 1, 0, N'阶段任务', N'')
GO
INSERT t_experience_mission_info ([rid], [missionid], [missioninfo], [limitstone], [restone], [userflag], [missionflag], [missionstone], [missiontitle], [btnurl]) VALUES (43, 14, N'今日乐币累计盈利30亿', 3000000000, 800000000, 1, 1, 0, N'阶段任务', N'')
go

----------------------------------------------------------  
-- Procedure Name: cc p_experience_user_mission_list ,1,0  
-- Author: dongshilin  
-- Date Generated: 2020年11月18日  
-- Description: 用户任务列表(体验币)  
----------------------------------------------------------  
ALTER procedure [dbo].[p_experience_user_mission_list]  
@userid int=0,  
@missionflag int=0, --1:阶段任务，2:日常任务,3,进阶任务,4,隐藏任务  
@nowmid int=0 output,
@nowmsid int = 0 output
as  
set nocount on  
set transaction isolation level read uncommitted  
set xact_abort on  
  
declare @ret int=0,@begin varchar(512)='',@end varchar(512)=''  
exec @ret=p_experience_vali @begin output,@end output  
if @ret<=0 or not exists(select 1 from t_experience_user where userid=@userid)  
    return  
declare @today date=getdate(),@roundid int=isnull((select top 1 roundid from t_invest where flag=1 order by roundid desc),0)
declare @tb table(rid int, missionid int,isdone int,missioninfo nvarchar(512),restone bigint,rate nvarchar(512)  
    ,limitstone bigint,missiontitle nvarchar(20),btnurl nvarchar(4000),ystone bigint, msid int, roundid int, hasrestone bigint)
if @missionflag=1
    insert into @tb(rid, missionid,isdone,missioninfo,restone,rate,limitstone,missiontitle,btnurl,ystone, msid, roundid, hasrestone)  
    select a.rid, a.missionid,a.isdone,b.missioninfo,b.restone,'',b.limitstone,b.missiontitle,b.btnurl,a.ystone,b.missionid, a.roundid, a.restone  
    from t_experience_user_mission a,  
    t_experience_mission_info b  
    where a.userid=@userid and a.missionid=b.rid and missionflag in(1,3)
else if @missionflag=4
    insert into @tb(rid,missionid,isdone,missioninfo,restone,rate,limitstone,missiontitle,btnurl,ystone,msid, roundid, hasrestone)  
    select a.rid,a.missionid,a.isdone,b.missioninfo,b.restone,'',b.limitstone,b.missiontitle,b.btnurl,a.ystone, b.missionid, a.roundid, a.restone  
    from t_experience_user_mission a,  
    t_experience_mission_info b  
    where a.userid=@userid and a.missionid=b.rid and missionflag=4
if Not exists(select 1 from @tb)  
    return  
update @tb set rate=case  
    when missionid=1 then ISNULL((select top 1 '1' from t_experience_sysplan_log  
        where userid=@userid and planid=1 and sysflag=1 and dateflag=@today),'0')+'/1'  
    when missionid=2 then  
        ISNULL((select CONVERT(varchar,COUNT(distinct(roundid))) from t_experience_sysplan_log  
            where userid=@userid and planid=8 and instone>=limitstone and sysflag=1 and dateflag=@today),'0')  
        +'/5'
     when missionid=3 then  
        ISNULL((select CONVERT(varchar,sysorder_keepwin) from t_experience_user
            where userid=@userid),'0')  
        +'/'+ CONVERT(varchar,limitstone) 
     when missionid=4 then  
         dbo.f_stone_tovarchar(ISNULL((select SUM(instone) from t_experience_sysplan_log  
            where userid=@userid and dateflag=@today and sysflag=3),0))+'/'+dbo.f_stone_tovarchar(limitstone) 
    when missionid in (5,22,25) then 
        '0/'+dbo.f_stone_tovarchar(limitstone)
    when missionid=15 then
        dbo.f_stone_tovarchar(ISNULL((select top 1 luckystone-stone from t_experience_invest_userfood 
            where userid=@userid and roundid 
            IN(select roundid from t_experience_sysplan_log b where userid=@userid and sysflag=2 
                and Not exists(select 1 from t_experience_sysplan_log 
                where userid=@userid and roundid=b.roundid and sysflag!=2)) 
            and luckystone-stone>0 and CONVERT(varchar(10),addtime,120)=@today order by luckystone-stone desc),0))
        +'/'+dbo.f_stone_tovarchar(limitstone)
    when missionid in(7,8,9,14,16,17,18) then
         dbo.f_stone_tovarchar(ISNULL((select top 1(luckystone-stone) from t_experience_invest_userfood  
            where userid=@userid and CONVERT(varchar(10),addtime,120)=@today 
            and luckystone-stone>0 order by luckystone-stone desc),0))+'/'+dbo.f_stone_tovarchar(limitstone)
    when missionid in(6,10,30) then
        dbo.f_stone_tovarchar(ISNULL((select SUM(luckystone) from t_experience_dayprofit  
            where userid=@userid and addtime between @begin and @end),0))+'/'+dbo.f_stone_tovarchar(limitstone) 
    when missionid=11 then
        dbo.f_stone_tovarchar(ISNULL((select luckystone from t_experience_dayprofit  
            where userid=@userid and daytime=@today),0))+'/'+dbo.f_stone_tovarchar(limitstone) 
    when missionid in(12,13) then
        ISNULL((select CONVERT(varchar,keepwin) from t_experience_user
            where userid=@userid),'0')  
        +'/'+ CONVERT(varchar,limitstone) 
    when missionid in (41,33) then
       dbo.f_stone_tovarchar(ISNULL((select stone from t_dayprofit  
            where userid=@userid and daytime=@today),0))+'/'+dbo.f_stone_tovarchar(limitstone)
    when missionid in(42,43,34) then
       dbo.f_stone_tovarchar(ISNULL((select sum(luckystone - stone) from t_invest_userfood  
            where userid=@userid and addtime >= @today and luckystone > stone),0))+'/'+dbo.f_stone_tovarchar(limitstone) 
    when missionid=27 then
        ISNULL((select CONVERT(varchar,COUNT(1)) from t_invest_userfood  
            where userid=@userid and CONVERT(varchar(10),addtime,120)=@today),'0')  
        +'/'+ CONVERT(varchar,limitstone) 
    when missionid=29 then '0/1'
    when missionid in(23,24,26,28) then  
        ISNULL((select CONVERT(varchar,1) from t_invest_userfood  
            where userid=@userid and roundid=@roundid),'0')  
        +'/1'
	when missionid in (31, 32) 
	    then dbo.f_stone_tovarchar(ISNULL((select SUM(luckystone) from t_experience_dayprofit  
        where userid=@userid and daytime = convert(date, getdate())),0))+'/'+dbo.f_stone_tovarchar(limitstone) 
	when missionid in (35, 36) 
	    then dbo.f_stone_tovarchar(ISNULL((select max(stone) from t_invest_userfood  
        where userid=@userid and addtime >= convert(date, getdate()) and luckystone > stone),0))+'/'+dbo.f_stone_tovarchar(limitstone)
	when missionid in (37, 38, 40)
	    then dbo.f_stone_tovarchar(ISNULL((select max(luckystone - stone) from t_invest_userfood  
        where userid=@userid and addtime >= convert(date, getdate()) and luckystone > stone),0))+'/'+dbo.f_stone_tovarchar(limitstone)
    end  

if @missionflag=1
begin
    set @nowmid=ISNULL((select top 1 b.rid from t_experience_user_mission a,t_experience_mission_info b  
    where a.userid=@userid and a.missionid=b.rid and a.isdone=0 and b.missionflag in(1,3) order by b.missionid),0)
	set @nowmsid = ISNULL((select top 1 b.missionid from t_experience_user_mission a,t_experience_mission_info b  
    where a.userid=@userid and a.missionid=b.rid and a.isdone=0 and b.missionflag in(1,3) order by b.missionid),0)
end

else if @missionflag=4
begin
    set @nowmid=ISNULL((select top 1 b.rid from t_experience_user_mission a,t_experience_mission_info b  
    where a.userid=@userid and a.missionid=b.rid and a.isdone=0 and b.missionflag=4 order by b.missionid),0) 
	set @nowmsid = ISNULL((select top 1 b.missionid from t_experience_user_mission a,t_experience_mission_info b  
    where a.userid=@userid and a.missionid=b.rid and a.isdone=0 and b.missionflag=4 order by b.missionid),0) 
end

if @missionflag = 1 and @nowmid in (10,18)
    select top 1 @nowmsid = m.missionid from t_experience_user_mission as um join t_experience_mission_info as m on um .missionid = m.rid 
	    where um.userid = @userid and m.missionid > @nowmsid and um.isdone = 0 order by m.missionid asc

select * from @tb order by isdone asc,msid asc  

GO

----------------------------------------------------------  
-- Procedure Name: cc p_experience_user_mission, 0,1  
-- Author: dongshilin  
-- Date Generated: 2020年9月2日  
-- Description: 体验活动-做任务(体验币)  
----------------------------------------------------------  
ALTER procedure [dbo].[p_experience_user_mission]  
@userid int=0,  
@roundid int=0  
as  
set nocount on  
set transaction isolation level read uncommitted  
set xact_abort on  
  
declare @ret int=0,@begin varchar(512)='',@end varchar(512)=''  
exec @ret=p_experience_vali @begin output,@end output  
if @ret<=0 or Not exists(select 1 from t_experience_user where userid=@userid)  
    return  
--阶段任务  
declare @tb table(rid int identity,mid int)  
insert into @tb(mid)  
    select a.rid  
    from t_experience_user_mission a,t_experience_mission_info b  
    where userid=@userid and a.isdone=0 and a.missionid=b.rid and b.missionflag=1 order by b.missionid asc
declare @i int=0,@mid int=0,@missionid int=0,@limitstone bigint=0,  
    @restone bigint=0,@infoid int=0,@title nvarchar(512)='',  
    @today date=getdate(),@remark nvarchar(4000)='',@ystone bigint=0,@btnstr nvarchar(50)=''  
while 1=1  
begin  
    set @i+=1  
    set @mid=ISNULL((select mid from @tb where rid=@i),0)  
    if @mid<=0  
        break;  
    select @missionid=a.missionid,@limitstone=b.limitstone,@restone=b.restone,@infoid=b.missionid,@title=b.missioninfo  
        from t_experience_user_mission a,t_experience_mission_info b where a.rid=@mid and a.missionid=b.rid  
  
    if @missionid=1  
    begin  
        if not exists(select 1 from t_experience_sysplan_log  
            where userid=@userid and planid=1 and sysflag=1 and dateflag=@today)  
            break;
        update t_experience_user set sysorder_keepwin=0 where userid=@userid
    end  
    else if @missionid=2
    begin
        if Not exists(select 1 from t_experience_sysplan_log  
            where userid=@userid and instone>=@limitstone and planid=8 and sysflag=1 and dateflag=@today  
            having COUNT(distinct(roundid))>=5)  
            break;
        update t_experience_user set sysorder_keepwin=0 where userid=@userid 
    end  
    else if @missionid=3
    begin  
        if Not exists(select 1 from t_experience_user
            where userid=@userid and sysorder_keepwin>=@limitstone)  
            break;  
    end
    else if @missionid=4
    begin  
        if Not exists(select 1 from t_experience_sysplan_log
            where userid=@userid and dateflag=@today and sysflag=3 having SUM(instone)>=@limitstone)  
            break;  
    end
    else if @missionid=5
    begin  
        if Not exists(select 1 from t_experience_sysplan_log
            where userid=@userid and roundid=@roundid and sysflag=2 having SUM(instone)>=@limitstone)
            or exists(select 1 from t_experience_sysplan_log  
                where userid=@userid and sysflag!=2 and roundid=@roundid)
            or not exists(select 1 from t_experience_invest_userfood 
                where userid=@userid and roundid=@roundid and luckystone-stone>0)
            break;  
    end
    else if @missionid in(7,8,9,14,16,17,18)
    begin  
        if not exists(select 1 from t_experience_invest_userfood 
            where userid=@userid and CONVERT(varchar,addtime)=@today and luckystone-stone>=@limitstone)
		begin
		    if @missionid = 18
			    continue
			else
                break  
		end
    end
    else if @missionid in(6,10,30)
    begin  
        if Not exists(select 1 from t_experience_dayprofit  
            where userid=@userid and addtime between @begin and @end having SUM(luckystone)>=@limitstone)
		begin
		    if @missionid = 10
			    continue
			else
			    break;
		end
    end
    else if @missionid=11
    begin  
        if Not exists(select 1 from t_experience_dayprofit  
            where userid=@userid and daytime=@today and luckystone>=@limitstone)  
            break;  
    end
    else if @missionid in(12,13)
    begin  
        if Not exists(select 1 from t_experience_user
            where userid=@userid and keepwin>=@limitstone)  
            break;  
    end
    else if @missionid=15
    begin  
        if not exists(select 1 from t_experience_invest_userfood 
            where userid=@userid and roundid 
            IN(select roundid from t_experience_sysplan_log b where userid=@userid and sysflag=2 
                and Not exists(select 1 from t_experience_sysplan_log 
                where userid=@userid and roundid=b.roundid and sysflag!=2)) 
            and CONVERT(varchar(10),addtime,120)=@today and luckystone-stone>=@limitstone)
            break;
    end
    else if @missionid in (31, 32)
	begin
	    if Not exists(select 1 from t_experience_dayprofit 
            where userid=@userid and daytime = @today having SUM(luckystone) >= @limitstone)  
            break;  
	end
	else if @missionid in (33, 41)
	begin
	    if not exists(select 1 from t_dayprofit  
            where userid=@userid and daytime=@today and stone>=@limitstone)  
        break;  
	end
    else if @missionid in (35, 36)
	begin
	    if not exists(select 1 from t_invest_userfood where userid = @userid and addtime >= @today and luckystone > stone having max(stone) >= @limitstone)
		    break;
	end
	else if @missionid = 37
	begin
	    if not exists(select 1 from t_invest_userfood where userid = @userid and addtime >= @today having max(luckystone - stone) >= @limitstone)
		    break;
	end
    else if @missionid in(42,43, 34)
    begin  
        if not exists(select 1 from t_invest_userfood  
            where userid=@userid and addtime >= @today and luckystone > stone having sum(luckystone - stone) >= @limitstone)  
            break;  
    end
    else  
        break;  
    update t_experience_user_mission set isdone=1,updatetime=GETDATE(), roundid = @roundid, restone = @restone where rid=@mid and isdone=0  
    if @@ROWCOUNT>=0  
    begin  
        --发奖励  
        set @remark='完成阶段任务:'+@title  
        exec AsynProcQueue.dbo.p_if_stone_update_delay @userid,@restone,0,31,16,1,0,  
             @remark,'Magnate','p_experience_user_mission'  
        set @remark='恭喜您完成阶段任务:(red)'+@title+'(/red)并获得(red)'  
           +dbo.f_stone_tovarchar(@restone)+'(/red)乐币奖励！'  
        --set @btnstr=case when @missionid>=5 then '恭喜全部完成' else '继续下个任务' end  
        set @btnstr=case when Not exists(select 1 from t_experience_user_mission a,t_experience_mission_info b  
            where userid=@userid and isdone=0 and a.missionid=b.rid and b.missionflag in(1,2)) then '恭喜全部完成'  
            else '继续下个任务' end  
        insert into t_experience_dialog(userid,remark,btnstr) values(@userid,@remark,@btnstr)  
   end  
end  
 
--乐币任务  
declare @tb3 table(rid int identity,mid int)  
set @i=0  
insert into @tb3(mid)  
    select a.rid  
    from t_experience_user_mission a,t_experience_mission_info b  
    where userid=@userid and a.isdone=0 and a.missionid=b.rid and b.missionflag=3  
while 1=1  
begin  
    set @i+=1  
    set @mid=ISNULL((select mid from @tb3 where rid=@i),0)  
    if @mid<=0  
        break;  
    select @missionid=a.missionid,@limitstone=b.limitstone,@restone=b.restone,@infoid=b.missionid,@title=b.missioninfo  
        from t_experience_user_mission a,t_experience_mission_info b where a.rid=@mid and a.missionid=b.rid  
  
    if @missionid=19
    begin  
        if not exists(select 1 from t_dayprofit  
            where userid=@userid and daytime=@today and stone>=@limitstone)  
            break;  
    end
    else if @missionid in(20,21)
    begin  
        if not exists(select 1 from t_dayprofit  
            where userid=@userid and daytime=@today and profitstone>=@limitstone)  
            break;  
    end
    else  
        break;  
    update t_experience_user_mission set isdone=1,updatetime=GETDATE(), roundid = @roundid, restone = @restone where rid=@mid and isdone=0  
    if @@ROWCOUNT>=0  
    begin  
        --发奖励  
        set @remark='完成乐币任务:'+@title  
        exec AsynProcQueue.dbo.p_if_stone_update_delay @userid,@restone,0,31,16,1,0,  
             @remark,'Magnate','p_experience_user_mission'  
        set @remark='恭喜您完成乐币任务:(red)'+@title+'(/red)并获得(red)'  
           +dbo.f_stone_tovarchar(@restone)+'(/red)乐币奖励！'  
        set @btnstr=case when Not exists(select 1 from t_experience_user_mission a,t_experience_mission_info b  
            where userid=@userid and isdone=0 and a.missionid=b.rid and b.missionflag in(1,2)) then '恭喜全部完成'  
            else '继续下个任务' end  
        insert into t_experience_dialog(userid,remark,btnstr) values(@userid,@remark,@btnstr)  
   end  
end  

--隐藏任务  
declare @tb4 table(rid int identity,mid int)  
set @i=0  
insert into @tb4(mid)  
    select a.rid  
    from t_experience_user_mission a,t_experience_mission_info b  
    where userid=@userid and a.isdone=0 and a.missionid=b.rid and b.missionflag=4  
while 1=1  
begin  
    set @i+=1  
    set @mid=ISNULL((select mid from @tb4 where rid=@i),0)  
    if @mid<=0  
        break;  
    select @missionid=a.missionid,@limitstone=b.limitstone,@restone=b.restone,@infoid=b.missionid,@title=b.missioninfo  
        from t_experience_user_mission a,t_experience_mission_info b where a.rid=@mid and a.missionid=b.rid  
  
    if @missionid=22
    begin  
        if not exists(select 1 from t_invest_userfood  
            where userid=@userid and roundid=@roundid and stone>=@limitstone)  
            continue;  
    end  
    else if @missionid in(23,24,25,26,28)  
    begin  
        if Not exists(select 1 from t_invest_userfood  
            where userid=@userid and roundid=@roundid and stone>@limitstone)  
            continue;  
    end
    else if @missionid=27
    begin  
        if Not exists(select 1 from t_invest_userfood  
            where userid=@userid and CONVERT(varchar(10),addtime,120)=@today having COUNT(1)>=@limitstone)  
            continue;  
    end
    else if @missionid=29
    begin  
        if Not exists(select 1 from t_dayprofit
            where userid=@userid and daytime=@today and profitstone>0)  
            continue;  
    end
	else if @missionid = 40
	begin
	    declare @balance bigint = 0
		select @balance = luckystone - stone from t_invest_userfood where userid = @userid and roundid = @roundid
	    if @balance < @limitstone
		    continue
		set @restone = @balance * 0.02
	end
    else if @missionid = 38
	begin
		if not exists (select 1 from t_invest_userfood where userid = @userid and roundid = @roundid and luckystone - stone >= @limitstone )
		    continue
	end
    else  
        break;  
    update t_experience_user_mission set isdone=1,updatetime=GETDATE(), roundid = @roundid, restone = @restone  where rid=@mid and isdone=0  
    if @@ROWCOUNT>=0  
    begin  
        --发奖励  
        set @remark='完成隐藏任务:'+@title
        if @missionid=22
        begin
            declare @returnstone bigint=0
            set @returnstone=ISNULL((select luckystone-stone from t_invest_userfood  
                where userid=@userid and roundid=@roundid),0)
            if @returnstone<=-500000000
                set @restone=500000000
            else if @returnstone<0
                set @restone=ABS(@returnstone)
        end
        else if @missionid in(23,28)
            set @restone=ISNULL((select ystone from t_experience_user_mission where rid=@mid),0)
		if @missionid = 38
		begin
		    set @remark= '恭喜您完成隐藏任务:(red)'+@title+'(/red)并获得福利特权：(red)5期内达成任务目标，任选一期盈利额外2%的乐币奖励(/red)'
            set @btnstr='朕已阅'  
            insert into t_experience_dialog(userid,remark,btnstr) values(@userid,@remark,@btnstr)  
			continue
		end
        if @missionid in(23,24,25,26,28)
            exec AsynProcQueue.dbo.p_if_gamecoin_update @userid,@restone,0,31,'4463E91D53',@sid=1,@remark=@remark  
                ,@invokedatabase='Magnate',@invokeproc='p_experience_user_mission'  
        else  
            exec AsynProcQueue.dbo.p_if_stone_update_delay @userid,@restone,0,31,16,1,0,  
                 @remark,'Magnate','p_experience_user_mission'  
        set @remark='恭喜您完成隐藏任务:(red)'+@title+'(/red)并获得(red)'  
           +dbo.f_stone_tovarchar(@restone)+'(/red)'+case when @missionid in(23,24,25,26,28) 
               then 'Y' else '乐' end+'币奖励！'  
        set @btnstr='朕已阅'  
        insert into t_experience_dialog(userid,remark,btnstr) values(@userid,@remark,@btnstr)  
   end  
end


GO

update t_experience_mission_info set missionstone = 200000000000 where rid = 38
update t_experience_mission_info set missionstone = 5000000000 where rid = 39
update t_experience_mission_info set missionstone = 5000000000 where rid = 40

GO

----------------------------------------------------------  
-- Procedure Name: cc p_experience_mission_create, 0,1  
-- Author: dongshilin  
-- Date Generated: 2020年11月17日  
-- Description: 体验活动-开启其他任务(体验币)  
----------------------------------------------------------  
ALTER procedure [dbo].[p_experience_mission_create]  
@userid int=0,  
@roundid int=0  
as  
set nocount on  
set transaction isolation level read uncommitted  
set xact_abort on  
  
declare @ret int=0,@begin varchar(512)='',@end varchar(512)=''  
exec @ret=p_experience_vali @begin output,@end output  
if @ret<=0 or not exists(select 1 from t_experience_user where userid=@userid)  
    return  
declare @userflag int=0  
set @userflag=ISNULL((select userflag from t_experience_user where userid=@userid),0)  
if @userflag in(0,2)  
    return  
declare @tb table(rid int identity,missionid int)  
insert into @tb(missionid)  
    select rid from t_experience_mission_info  
        where userflag=@userflag and missionflag in(3,4)  
        and rid not in(select missionid from t_experience_user_mission where userid=@userid)  
declare @i int=0,@missionid int=0,@missioninfo nvarchar(512)=''  
    ,@missiontitle nvarchar(50)='',@missionstone bigint=0,@remark nvarchar(4000)='',@today date=getdate()
    ,@ystone bigint=0
declare @u_nowmission int=0,@u_profitstone bigint=0
set @u_nowmission=ISNULL((select top 1 b.rid  from t_experience_user_mission a,t_experience_mission_info b 
    where a.userid=@userid and isdone=1 and a.missionid=b.rid
    and b.missionflag=1 order by a.missionid desc),0)
while 1=1  
begin  
    set @i+=1  
    set @missionid=ISNULL((select missionid from @tb where rid=@i),0)  
    if @missionid<=0  
        break;  
    select @missioninfo=missioninfo,@missiontitle=missiontitle,@missionstone=missionstone  
        from t_experience_mission_info where rid=@missionid  
    if @missionid in(19,20,21)  
    begin  
        if exists(select 1 from t_experience_user_mission a,t_experience_mission_info b  
            where userid=@userid and isdone=0 and a.missionid=b.rid and a.missionid != 10  
            and b.missionflag=1)
            continue  
    end  
    else if @missionid in(22,27)
    begin
        declare @missionstone2 bigint=case when @missionid=22 then 30000000000 else 100000000000 end
        if Not exists(select 1 from t_experience_invest_userfood  
            where userid=@userid and roundid=@roundid and (luckystone-stone)>=@missionstone)  
            and Not exists(select 1 from t_experience_dayprofit  
            where userid=@userid and addtime between @begin and @end having SUM(luckystone)>=@missionstone2)  
            continue 
    end  
    else if @missionid in(23,28)
    begin
        set @u_profitstone=case when @missionid=23 then ISNULL((select profitstone from t_experience_dayprofit 
            where userid=@userid and daytime=@today),0)
            else ISNULL((select luckystone-stone from t_experience_invest_userfood 
            where userid=@userid and roundid=@roundid),0) end
        if @u_nowmission not in(4,14) or @u_profitstone>@missionstone 
            continue
        if @missionid=23 and exists(select 1 from t_experience_user_mission 
            where userid=@userid and missionid=@u_nowmission and isdone=1 and updatetime>=DATEADD(SECOND,-5,GETDATE()))
            continue
        set @ystone=ABS(@u_profitstone)
    end
    else if @missionid in(24,26)
    begin
        declare @ucoin bigint=0  
        exec GameCoin.dbo.p_if_getusercoin @userid,@ucoin output  
        if not exists(select 1 from t_experience_help_log where userid=@userid  
            and dateflag=@today and helpid=3 having COUNT(1)>=3)  
            or @ucoin>@missionstone
            continue
    end
    else if @missionid=25
    begin
        if Not exists(select 1 from t_experience_user where userid=@userid and keepwin2>=@missionstone)
            continue
    end
    else if @missionid=29
    begin
        if not exists(select 1 from t_dayprofit where userid=@userid and daytime=@today and profitstone<=@missionstone)
            continue
    end
	else if @missionid = 40
	begin
	    if not exists(select 1 from t_experience_invest_userfood  
            where userid = @userid and roundid = @roundid and  (luckystone-stone) >= @missionstone) 
		    continue
	end
	else if @missionid = 38
	begin
	    if not exists(select 1 from t_experience_user_mission where userid = @userid and missionid = 18 and isdone  = 1)
		    continue
	end
    else if @missionid = 39
	begin
	    declare @curmissionid int = 0, @updatetim datetime = getdate()
		select top 1 @curmissionid = a.missionid from t_experience_user_mission a, t_experience_mission_info b 
		    where a.userid = @userid and a.missionid=b.rid and missionflag = 1 and isdone = 0 and b.rid >= 33 order by b.missionid asc
		select @updatetim = isnull(updatetime,getdate()) from t_experience_user_mission where userid = @userid and missionid = 34
	    if @curmissionid != 35 or 
		    not exists(select 1 from t_invest_userfood where userid = @userid and addtime >= @updatetim having sum(stone) >= @missionstone and sum(luckystone) = 0)
		    continue
	end
    else  
        break
	if @missionid != 39
	begin
        insert into t_experience_user_mission(userid,missionid,ystone)  
            values(@userid,@missionid,@ystone)  
        set @remark='恭喜您触发'+@missiontitle+'：(red)'  
        +@missioninfo+'，快来完成吧！'  
        insert into t_experience_dialog(userid,remark,btnstr,missionid) values(@userid,@remark,'朕已阅',@missionid)
	end
	else
	begin
	    insert into t_experience_user_mission(userid,missionid,ystone,isdone)  
            values(@userid,@missionid,@ystone,1)  
        --发奖励  
        set @remark='触发并完成隐藏福利任务'  
        exec AsynProcQueue.dbo.p_if_stone_update_delay @userid,5000000000,0,31,16,1,0,  
             @remark,'Magnate','p_experience_mission_create'  
        set @remark='恭喜触发隐藏福利，获得'  
           +dbo.f_stone_tovarchar(5000000000)+'(/red)乐币奖励！'  
        declare @btnstr varchar(128) = '开心领下'  
        insert into t_experience_dialog(userid,remark,btnstr) values(@userid,@remark,@btnstr)  
	end
end

GO

----------------------------------------------------------
-- Procedure Name: cc p_experience_userinsert, 0,1
-- Author: dongshilin
-- Date Generated: 2020年11月17日
-- Description: 体验活动-打开礼包(体验币)
----------------------------------------------------------
ALTER procedure [dbo].[p_experience_userinsert]
@userid int=0,
@outmsg nvarchar(512)='' output,
@addstone bigint = 0 output,
@userflag int=0 output --1:新/玩大亨少的用户200亿，2：现有大户 300亿，3：流失/没币用户 500亿，0：非目标用户
as
set nocount on
set transaction isolation level read uncommitted
set xact_abort on

--if not exists(select 1 from t_experience_chance_user where userid=@userid)
--    return 0
declare @ret int=0
exec @ret=p_experience_vali
if @ret<=0 or @userid<10000
begin
    set @outmsg='礼包已过期'
    return 0
end
declare @today date=getdate()
set @userflag=ISNULL((select userflag from t_experience_chance_user where userid=@userid),0)
set @addstone=0
if @userflag>0 and exists(select 1 from t_experience_alter_user where userid=@userid)
begin
    set @outmsg='您已经打开过礼包了,快去做任务吧'
    return 0
end
else if @userflag=0 and exists(select 1 from t_experience_alter_user where userid=@userid and dateflag=@today)
begin
    set @outmsg='您今天已经打开过礼包了，明日再来吧'
    return 0
end

if @userflag>0
begin
    if not exists(select 1 from t_experience_user where userid=@userid)
    begin
        set @addstone=case when @userflag=1 then 20000000000
            when @userflag=2 then 30000000000
            when @userflag=3 then 50000000000
            else 0 end
        if @addstone>0
        begin
            insert into t_experience_user(userid,userflag)
                values(@userid,@userflag)
            set @userflag=case when @userflag=1 then 1 else 2 end --区分初始化任务
            insert into t_experience_user_mission(userid,missionid)
                select @userid,rid from t_experience_mission_info
                where userflag=@userflag and missionflag=1
        end
    end
end
else
begin
    if exists(select 1 from t_experience_alter_user
         where userid not in(select userid from t_experience_chance_user) having sum(addstone)>=100000000000)
    begin
        set @outmsg='您今天的幸运值不够，Y币大礼包内空空如也'
        return 0
    end
    set @addstone=10000000*(CAST(RAND()*6 as int)+5)
end
insert into t_experience_alter_user(userid,addstone) values(@userid,@addstone)
declare @prerestone bigint=0
set @prerestone=ISNULL((select SUM(b.restone) from t_experience_user_mission a,t_experience_mission_info b
    where a.userid=@userid and a.missionid=b.rid and b.missionid!=24),0)

set @outmsg=case
    when @addstone>=20000000000 then '万里挑一的幸运儿，恭喜您领取到(red)'
        +dbo.f_stone_tovarchar(@addstone)+'(/red)Y币，赶紧做任务赚乐币吧！全部任务完成可得(red)'
        +dbo.f_stone_tovarchar(@prerestone)+'(/red)乐币哦！'
    else '阿欧~功力不足，仅领取到(red)'+dbo.f_stone_tovarchar(@addstone)+'(/red)Y币~可以在游戏中使用Y币任意畅玩哦～）' end

return 1

GO

----------------------------------------------------------  
-- Procedure Name: cc p_experience_user_info ,1,0  
-- Author: dongshilin  
-- Date Generated: 2020年11月18日  
-- Description: 用户任务信息(体验币)  
----------------------------------------------------------  
ALTER procedure [dbo].[p_experience_user_info]  
@userid int=0,  
@userflag int=0 output,  
@nowtime varchar(20)='' output,  
@nowrestone bigint=0 output,  
@prerestone bigint=0 output,  
@todaywin bigint=0 output  
as  
set nocount on  
set transaction isolation level read uncommitted  
set xact_abort on  
  
declare @ret int=0,@begin varchar(512)='',@end varchar(512)=''  
exec @ret=p_experience_vali @begin output,@end output  
if @ret<=0 or not exists(select 1 from t_experience_user where userid=@userid)  
    return 0  
set @userflag=ISNULL((select userflag from t_experience_user where userid=@userid),0)  
set @nowtime=CONVERT(varchar,GETDATE(),120)  
declare @today date=getdate()  
set @nowrestone=ISNULL((select SUM(b.restone) from t_experience_user_mission a,t_experience_mission_info b  
    where a.userid=@userid and a.missionid=b.rid and a.isdone=1 and a.missionid not IN(23,24,25,26,28)),0)  
set @prerestone=ISNULL((select SUM(b.restone) from t_experience_user_mission a,t_experience_mission_info b  
    where a.userid=@userid and a.missionid=b.rid and a.missionid not IN(23,24,25,26,28)),0)  
set @todaywin=ISNULL((select luckystone from t_experience_dayprofit where userid=@userid and daytime=@today),0)--改成了产出  
return 1

GO

----------------------------------------------------------  
-- Procedure Name: cc p_experience_user_mission_getreward ,1,0  
-- Author: lkl  
-- Date Generated: 2021年01月5日  
-- Description: 用户领取任务奖励 
----------------------------------------------------------  
create procedure [dbo].[p_experience_user_mission_getreward]  
@userid int=0,  
@roundid int = 0,			--领取奖励期数
@umisid int=0,				--用户任务id
@outmsg varchar(128) = '' out
as  
set nocount on  
set transaction isolation level read uncommitted  
set xact_abort on  
  
declare @finishroundid int = 0, @missid int = 0

select @finishroundid = roundid, @missid = missionid from t_experience_user_mission where @umisid = rid and userid = @userid and restone = 0 and missionid = 38 and isdone = 1
if @@ROWCOUNT <= 0
begin
    set @outmsg = '领取失败，该任务未完成或奖励已领取'
	return 0
end
declare @balance bigint = 0
select @balance = luckystone - stone from t_invest_userfood where roundid = @roundid and userid = @userid
if @@ROWCOUNT <= 0
begin
    set @outmsg = '领取失败，没有找到该任务奖励'
	return 0
end
if @balance <= 0
begin
    set @outmsg = '领取失败，必须选择盈利的期数'
	return 0
end
declare @reward bigint = @balance * 0.02

update t_experience_user_mission set restone = @reward where userid = @userid and @umisid = rid and restone = 0
if @@ROWCOUNT <= 0
begin
    set @outmsg = '领取奖励失败'
	return 0
end

--发放奖励
 exec AsynProcQueue.dbo.p_if_stone_update_delay @userid,@reward,0,31,16,1,0,'领取隐藏任务奖励','Magnate','p_experience_user_mission'

set @outmsg = '领取奖励成功，获得' +dbo.f_stone_tovarchar(@reward)+'乐币'
return 1

GO
----------------------------------------------------------  
-- Procedure Name: cc p_experience_user_mission_getreward_roundids ,1,0  
-- Author: lkl  
-- Date Generated: 2021年01月5日  
-- Description: 获取用户可以领取任务奖励的期数 
----------------------------------------------------------  
create procedure [dbo].[p_experience_user_mission_getreward_roundids]  
@sroundid int=0			--开始id
as  
set nocount on  
set transaction isolation level read uncommitted  
set xact_abort on  
  
declare @limitstone bigint = 0

select @limitstone = limitstone from t_experience_mission_info where rid = 38

select top 5 * from t_invest_userfood where roundid >= @sroundid and luckystone - stone >= @limitstone and flag = 4

GO



--清楚之前的用户ID
GO
TRUNCATE TABLE t_experience_chance_user
GO

--清除数据
GO
exec p_experience_sysclear
GO

--添加测试id
GO
INSERT [dbo].[t_experience_chance_user] ([userid], [userflag]) VALUES (33316, 1)
GO
INSERT [dbo].[t_experience_chance_user] ([userid], [userflag]) VALUES (33320, 3)
GO
INSERT [dbo].[t_experience_chance_user] ([userid], [userflag]) VALUES (51168, 1)
GO
INSERT [dbo].[t_experience_chance_user] ([userid], [userflag]) VALUES (360008, 1)
GO
INSERT [dbo].[t_experience_chance_user] ([userid], [userflag]) VALUES (855114, 3)
GO
INSERT [dbo].[t_experience_chance_user] ([userid], [userflag]) VALUES (4665810, 2)
GO
INSERT [dbo].[t_experience_chance_user] ([userid], [userflag]) VALUES (9364992, 2)
GO
INSERT [dbo].[t_experience_chance_user] ([userid], [userflag]) VALUES (12089265, 3)
GO
INSERT [dbo].[t_experience_chance_user] ([userid], [userflag]) VALUES (27179966, 3)
GO
INSERT [dbo].[t_experience_chance_user] ([userid], [userflag]) VALUES (47433526, 1)
GO
INSERT [dbo].[t_experience_chance_user] ([userid], [userflag]) VALUES (47932348, 1)
GO
INSERT [dbo].[t_experience_chance_user] ([userid], [userflag]) VALUES (48025767, 1)
GO
INSERT [dbo].[t_experience_chance_user] ([userid], [userflag]) VALUES (48026853, 3)
GO
INSERT [dbo].[t_experience_chance_user] ([userid], [userflag]) VALUES (48034228, 3)
GO
INSERT [dbo].[t_experience_chance_user] ([userid], [userflag]) VALUES (48098814, 2)
GO


--活动时间
GO

ALTER PROCEDURE [dbo].[p_experience_vali]
@begin varchar(128)='' output,
@end varchar(128)='' output,
@acttime varchar(512)='' output
as
set nocount on
set transaction isolation level read uncommitted
set xact_abort on

set @begin='2021-01-26 17:00:00'
set @end='2021-01-31 23:59:59'
set @acttime='1月26日 17点-1月31日 24点'
if GETDATE()<@begin
    return -1
if GETDATE()>@end
    return -2
return 1


GO

--bug修复更改
GO

update t_experience_mission_info set limitstone = 1000000000 where rid = 11 
update t_experience_mission_info set missioninfo = '今日单笔盈利50亿Y币' where rid = 14
update t_experience_mission_info set btnurl = 'planuserlist.aspx?missflag=1', missioninfo='今日仅使用(url=planuserlist.aspx?missflag=1)货单投资(/url)，单笔盈利100亿Y币'  where rid = 15
update t_experience_mission_info set missioninfo='运用自己的策略投资，今日单笔盈利200亿Y币'  where rid = 16
update t_experience_mission_info set missioninfo = '今日累计在大亨盈利100亿乐币' where rid = 34
update t_experience_mission_info set restone = 200000000 where rid = 3

GO
update t_experience_mission_info set missioninfo = '今日连续踩中两次畅销货物【单次最多可以投资20种】' where rid = 12
update t_experience_mission_info set missioninfo = '今日连续踩中三次畅销货物【单次最多可以投资20种】' where rid = 13
update t_experience_mission_info set missioninfo = '意外惊喜：福利返还' where rid = 39
update t_experience_mission_info set btnurl = 'winrate.aspx?missflag=1' where rid in (12, 13)
GO

----------------------------------------------------------
-- Procedure Name: cc p_experience_user_info ,1,0
-- Author: dongshilin
-- Date Generated: 2020年11月18日
-- Description: 用户任务信息(体验币)
----------------------------------------------------------
ALTER procedure [dbo].[p_experience_user_info]
@userid int=0,
@userflag int=0 output,
@nowtime varchar(20)='' output,
@nowrestone bigint=0 output,
@prerestone bigint=0 output,
@todaywin bigint=0 output
as
set nocount on
set transaction isolation level read uncommitted
set xact_abort on

declare @ret int=0,@begin varchar(512)='',@end varchar(512)=''
exec @ret=p_experience_vali @begin output,@end output
if @ret<=0 or not exists(select 1 from t_experience_user where userid=@userid)
    return 0
set @userflag=ISNULL((select userflag from t_experience_user where userid=@userid),0)
set @nowtime=CONVERT(varchar,GETDATE(),120)
declare @today date=getdate()
set @nowrestone=ISNULL((select SUM(b.restone) from t_experience_user_mission a,t_experience_mission_info b
    where a.userid=@userid and a.missionid=b.rid and a.isdone=1 and a.missionid not IN(23,24,25,26,28)),0)
set @prerestone=ISNULL((select SUM(b.restone) from t_experience_user_mission a,t_experience_mission_info b
    where a.userid=@userid and a.missionid=b.rid and a.missionid not IN(22,23,24,25,26,27,28,29,38,39,40)),0)
set @todaywin=ISNULL((select luckystone from t_experience_dayprofit where userid=@userid and daytime=@today),0)--改成了产出
return 1

GO

ALTER function [dbo].[f_stone_tovarchar](@instone bigint)returns varchar(128)  
as    
begin  
    declare @charstone varchar(128),@temp1 decimal(17,1),@temp2 decimal(18,2)    
    if @instone>=100000000    
    begin    
		set @temp2=@instone/100000000.00 
		if @temp2 % 1 = 0.00
	        set @charstone=CONVERT(varchar, @instone/100000000)+'亿'
		else if @temp2 % 0.1 = 0.00
		begin
		    set @temp1 = @temp2
			set @charstone=CONVERT(varchar,@temp1)+'亿' 
		end
		else
		    set @charstone=CONVERT(varchar, @temp2)+'亿'
    end    
    else if @instone>=10000    
    begin
	    set @temp2=@instone/10000.00 
		if @temp2 % 1 = 0.00
	        set @charstone=CONVERT(varchar, @instone/10000)+'万'
		else if @temp2 % 0.1 = 0.00
		begin
		    set @temp1 = @temp2
			set @charstone=CONVERT(varchar,@temp1)+'万' 
		end
		else
		    set @charstone=CONVERT(varchar, @temp2)+'万'
	end
	else
	    set @charstone=CONVERT(varchar, @instone)
    return @charstone  
end 

GO

--更新用户数据
delete from t_experience_chance_user

GO
--大户（2）
insert into t_experience_chance_user(userid, userflag) values (12245544,2),(14751155,2),(13588,2),(38228822,2),(12244499,2),(198058,2),(47376556,2),(17441199,2),(24232455,2),(24298866,2),(12300088,2),(12301122,2),(14748888,2),(17441177,2),
(24715489,2),(34460543,2),(12231111,2),(38276644,2),(43303708,2),(38258866,2),(16888,2),(7932620,2),(18881888,2),(38201545,2),(24315500,2),(9898899,2),(198806,2),(12245500,2),(7842600,2),(21494422,2),(47561312,2),(42867839,2),(38221188,2),(14875588,2),
(14868811,2),(22222222,2),(44631,2),(2436893,2),(14871166,2),(21261144,2),(800018,2),(40925018,2),(6242644,2),(38248888,2),(711116,2),(48228176,2),(38222233,2),(19890707,2),(38265566,2),(14894466,2),(38235599,2),(12348,2),(46883924,2),(33043020,2),(48034228,2)

GO
--流失用户（3）
insert into t_experience_chance_user(userid, userflag) values
(35428855,3),(21499944,3),(18923606,3),(14871100,3),(65994,3),(40305366,3),(14105,3),(14637777,3),(43356599,3),(88878,3),(40264176,3),
(33483557,3),(10567,3),(21098300,3),(18666684,3),(47803467,3),(48057187,3),(12271166,3),(43867605,3),(47788086,3),(168888,3),(14875522,3),
(95888,3),(21503366,3),(1633686,3),(166527,3),(47884616,3),(47987992,3),(2601454,3),(41750734,3),(7966283,3),(46886612,3),(14771199,3),
(47627348,3),(910908,3),(14886677,3),(42867746,3),(9836666,3),(43643034,3),(44351805,3),(38306699,3),(14894499,3),(45308362,3),(19731228,3),
(55353,3),(48051818,3),(38236622,3),(32166688,3),(999999,3),(956379,3),(48084585,3),(159159,3),(14886611,3),(38287700,3),(38287722,3),
(47975812,3),(3180688,3),(37669955,3),(1300000,3),(12251155,3),(14751122,3),(881108,3),(198558,3),(543210,3),(36792397,3),(522225,3),
(77707,3),(100065,3),(24290066,3),(17459966,3),(160000,3),(47619716,3),(35209999,3),(158887,3),(47498334,3),(24315511,3),(9100022,3),
(39351812,3),(45988663,3),(44846,3),(80009,3),(42127098,3),(26249507,3),(100034,3),(47601617,3),(42110,3),(20157777,3),(21493377,3),
(20158800,3),(47792915,3),(38892712,3),(800005,3),(188884,3),(22020,3),(20155500,3),(20156644,3),(40278,3),(24007360,3),(20154477,3),
(61888,3),(12292244,3),(12292200,3),(900306,3),(24306611,3),(38236644,3),(45871384,3),(13496185,3),(38319966,3),(38319988,3),(7211419,3),
(38238800,3),(24011140,3),(35951909,3),(14868866,3),(6766268,3),(45913989,3),(47420216,3),(9966688,3),(33235555,3),(38230055,3),
(22796267,3),(17127066,3),(38244400,3),(38236600,3),(14879911,3),(20196796,3),(581937,3),(12292233,3),(38236633,3),(38239988,3),
(38238855,3),(17674477,3),(38249933,3),(4498821,3),(38258811,3),(12244477,3),(36490257,3),(38266600,3),(19389620,3),(38276655,3),
(38244455,3),(860523,3),(47411881,3),(43656087,3),(24306655,3),(43854689,3),(21498800,3),(47529161,3),(38249977,3),(882888,3),(40737870,3),
(38262277,3),(38263311,3),(17676600,3),(47893308,3),(1171714,3),(5889116,3),(45797365,3),(24314477,3),(23165638,3),(47973310,3),
(66699,3),(80520,3),(47558687,3),(36635218,3),(47498221,3),(43772782,3),(44465871,3),(92168,3),(47003130,3),(44979436,3),(47749583,3),
(41724081,3),(20745571,3),(47836511,3),(8735715,3),(46268911,3),(2968870,3),(14504444,3),(10189344,3),(38495537,3),(47955162,3),
(34760808,3),(44935924,3),(6802243,3),(43624196,3),(45636946,3),(20110,3),(45215390,3),(10307749,3),(47914366,3),(42710461,3),
(47972225,3),(14639999,3),(36057061,3),(42403473,3),(22935644,3),(47864652,3),(46332310,3),(42029019,3),(45108929,3),(35437777,3),
(46750415,3),(35900022,3),(14751144,3),(24314466,3),(14868822,3),(39243332,3),(14871111,3),(47775816,3),(46978867,3),(12224455,3),
(38247722,3),(37058034,3),(20752000,3),(21484488,3),(21421606,3),(24314488,3),(2137094,3),(47423703,3),(33951561,3),(38235577,3),
(12245555,3),(5166087,3),(120012,3),(23196200,3),(47966695,3),(760306,3),(19811118,3),(9825555,3),(9853333,3),(588888,3),(12312808,3),
(66760,3),(12300099,3),(47959393,3),(9203788,3),(888008,3),(910921,3),(28777922,3),(45884391,3),(35191506,3),(1006816,3),(35222692,3),
(14867777,3),(14868844,3),(52019,3),(695595,3),(424306,3),(44317385,3),(47928390,3),(24302200,3),(29356,3),(388388,3),(18356224,3),
(881006,3),(46010469,3),(14894455,3),(13374418,3),(16893790,3),(22192219,3),(888888,3),(17446600,3),(14631111,3),(14875533,3),(47649668,3),
(46471631,3),(14746688,3),(10051,3),(47423064,3),(5231617,3),(14871188,3),(10245,3),(82888,3),(52027,3),(4351553,3),(12223399,3),(88888,3),
(20357191,3),(14894477,3),(47819392,3),(69996,3),(47370437,3),(47865537,3),(32167799,3),(6297853,3),(19144466,3),(860228,3),(8446271,3),
(40390039,3),(36024462,3),(70007,3),(38273355,3),(35907799,3),(52109,3),(14737733,3),(13337,3),(14871133,3),(77877,3),(19761118,3),
(701111,3),(11601320,3),(10000003,3),(502793,3),(20932038,3),(14894422,3),(22225,3),(47849775,3),(47829257,3),(47495381,3),(37393998,3),
(10198,3),(760307,3),(21493355,3),(46244789,3),(14749911,3),(43862767,3),(800202,3),(47969586,3),(38227777,3),(830630,3),(47377113,3),
(539218,3),(360008,3)

GO
--新用户（1）
insert into t_experience_chance_user(userid, userflag) values
(11567,1),(51168,1),(53077,1),(91919,1),(169886,1),(199166,1),(293868,1),(388888,1),(703255,1),(788888,1),(1888326,1),(2653037,1),
(7642561,1),(9085151,1),(10989373,1),(14751188,1),(14879977,1),(19258806,1),(19921030,1),(21583244,1),(21936808,1),(24294411,1),(24314433,1),
(28594141,1),(30995277,1),(31639038,1),(33351820,1),(33792495,1),(34752357,1),(35843139,1),(36284832,1),(36492506,1),(38245533,1),
(38273399,1),(38276666,1),(38286633,1),(38312244,1),(39004973,1),(39551398,1),(40873834,1),(41600235,1),(42551304,1),(42682882,1),
(43096270,1),(43254901,1),(43295878,1),(44292786,1),(44887916,1),(45701981,1),(45765560,1),(46470678,1),(46488814,1),(46794244,1),
(47304474,1),(47351534,1),(47568312,1),(47614726,1),(47788723,1),(47836377,1),(47892911,1),(47931704,1),(47931989,1),(47932745,1),
(47956319,1),(47958062,1),(47960201,1),(47962131,1),(47967745,1),(47984528,1),(47993145,1),(48003641,1),(48038995,1),(48063900,1),(48109023,1),(27179966,1)