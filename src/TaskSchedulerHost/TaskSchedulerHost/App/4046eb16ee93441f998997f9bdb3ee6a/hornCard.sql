use[honor_card]

GO

----------------------------------------------------------
-- Procedure Name: cc p_act_phonecharge_order_update,0,1
-- Author: Lkl
-- Date Generated: 2021年04月28日
-- Description: 生成订单
----------------------------------------------------------

ALTER procedure [dbo].[p_act_phonecharge_order_update]
@rid int = 0,
@status int = 0,
@no varchar(256) = '',
@pws varchar(256) = '',
@outmsg varchar(128) = '' output
as
set nocount on
set transaction isolation level read uncommitted
set xact_abort on

declare @date date = getdate()
declare @userid int = 0, @money decimal(6,2) = 0, @phone char(11) = ''
declare @type int = 0
--查询订单信息
select @userid = userid, @money = money, @type = type, @phone = phone from t_act_phonecharge_order where rid = @rid and status = 0
if @@ROWCOUNT <= 0 return

--更新订单状态
update t_act_phonecharge_order set status = @status where rid = @rid
if @@ROWCOUNT <= 0 return

if @status = 1 and @type = 2
begin
    insert into  t_act_phonecharge_order_jdcard(orderid, userid, no, pws) values(@rid, @userid, @no, @pws)
	if @@ROWCOUNT <= 0 return
	--发送卡密短信給用户
	declare @content varchar(1024) = '您兑换的50元京东卡已审核通过，请尽快在京东礼品卡中兑换使用。卡号：' + @no + '，卡密：' + @pws + '，请勿将此短信截图或复制给他人。'
	exec [AsynProcQueue].dbo.p_if_sendsms 16, 'E503B26687254BD790BDC5567C6779C5', @phone, @content, 'honor_card', 'p_act_phonecharge_order_update'
end

if @status = 1 and @type in (3,4)
begin
declare @ciid int = case @type when 3 then 215 else 214 end

declare @params varchar(1024) = '{"userid":'+ltrim(@userid)+', "ciid":' + ltrim(@ciid) + '}'
exec AsynProcQueue.dbo.p_if_asynproc_add @dbname = 'wawajivirtual', @procname = 'p_if_gift_add',
@params = @params, @remark = '尊贵卡礼券兑换', @dbsource = '192.168.1.50', @invokedatabase = 'honor_card', @invokeproc = 'p_act_phonecharge_order_update'

end

--充值失败退还话费
if @status = -1
begin
    --添加记录
    insert into t_act_phonecharge_user_log(userid, rand, probability, stonein, type, money, remark)
    values(@userid, 0, 0, 0, 2000, @money, '充值话费失败退还')
    if @@ROWCOUNT <= 0 return
	--更新用户金额
	update t_act_phonecharge_user set money += @money, updatetime = getdate() where userid = @userid
end

