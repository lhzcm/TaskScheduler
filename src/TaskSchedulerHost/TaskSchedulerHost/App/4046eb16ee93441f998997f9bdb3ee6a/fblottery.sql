USE [fblottery]
GO



GO

----------------------------------------------------------
-- Procedure Name: cc p_act_20201111_user_task_update,0,1
-- Author: zhongzhenyu
-- Date Generated: 2020年11月20日
-- Description: 用户活动任务更新
----------------------------------------------------------

ALTER procedure [dbo].[p_act_20201111_user_task_update]
@userid int = 0,
@type int = 0,
@nums bigint = 0
as
set nocount on
set transaction isolation level read committed
set xact_abort on

declare @date date = getdate()

--初始化任务
exec p_act_20201111_user_task_init @userid

update t_act_20201111_user_task set nums = case
    when iscount = 1
	then nums+@nums
	else case
	    when @nums >= nums
		    then @nums
	        else nums 
		end
    end,
status = case
    when iscount = 1
	then case
	    when nums+@nums >= condition
	    then 1
		else 0
	end
	else case
	    when @nums >= condition
		then 1
	else 0
	end
end
where userid = @userid and status = 0 and expire > getdate() and @type = type & @type and (kind not in (0,1) or
not exists(select 1 from t_act_20201111_user_task where userid = @userid and dateflag = @date and kind in (0,1) and status = 1 having sum(stone) >= 1100000000))

