USE [act_funcard]
GO
----------------------------------------------------------
-- Procedure Name: cc p_act_time_validate,0,1
-- Author: liubin
-- Date Generated: 2020年06月17日
-- Description: 活动时间验证
----------------------------------------------------------
ALTER procedure [dbo].[p_act_time_validate]
@outmsg varchar(128) = '' output
as
set nocount on
set transaction isolation level read uncommitted
set xact_abort on

declare @datetime datetime = getdate()

--活动时间 2020年01月13 - 2020年01月26

if @datetime >= '2021-02-22' and @datetime <= '2021-03-07'
begin
    set @outmsg = '活动进行中'
	return 1
end

set @outmsg = '活动已经结束'
return 0
