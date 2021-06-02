use[game_dmm]

GO
----------------------------------------------------------
-- Procedure Name: cc f_guess_odds,0,1
-- Author: liubin
-- Date Generated: 2019年05月29日
-- Description: 投注赔率
----------------------------------------------------------
ALTER function [dbo].[f_guess_odds]
(
@itemid int = 0,
@itemval int = 0,
@userid int = 0
) returns decimal(7,4)
as
begin
	declare @odds decimal(7,4) = 0
	declare @date date = getdate()
	--活动期间赔率
	if @date between '2021-01-08' and '2021-03-18'
	begin
	    if @itemid <= 2
	    begin
		    set @odds = 1.95
	    end
	    else if @itemid = 3
	    begin
			if @itemval in (3,18) set @odds = 183
		    if @itemval in (4,17) set @odds = 66.08
		    if @itemval in (5,16) set @odds = 32.42
		    if @itemval in (6,15) set @odds = 19.54
		    if @itemval in (7,14) set @odds = 12.91
		    if @itemval in (8,13) set @odds = 9.20
		    if @itemval in (9,12) set @odds = 7.77
		    if @itemval in (10,11) set @odds = 7.2
	    end
	    else if @itemid = 4
	    begin
	        if @itemval = 0 set @odds = 2.28
		    else set @odds = 13
	    end
	    else if @itemid = 5
	    begin
		    if @itemval = 0 set @odds = 28
		    else set @odds = 185
	    end
	    else if @itemid = 6
	    begin
	        if @itemval = 0 set @odds = 7.65
		    else set @odds = 34.5
	    end
	    else if @itemid = 7 or @itemid = 8
	    begin
	        set @odds = 1.6
	    end
	    else if @itemid = 9
	    begin
	        if @itemval in (1,10) set @odds = 7.4
		    if @itemval in (2,9) set @odds = 8.0
		    if @itemval in (3,8) set @odds = 9.0
		    if @itemval in (4,7) set @odds = 11.1
		    if @itemval in (5,6) set @odds = 12.4
	    end
	    else if @itemid = 10
	    begin
	        if @itemval = 1 set @odds = 5.9
		    if @itemval = 2 set @odds = 3.7
		    if @itemval = 3 set @odds = 3.3
		    if @itemval = 4 set @odds = 3.7
		    if @itemval = 5 set @odds = 5.9
	    end
	    else if @itemid = 11
	    begin
	   	    if @itemval = 1 set @odds = 1.0153
		    if @itemval = 2 set @odds = 1.1291
		    if @itemval = 3 set @odds = 1.4460
		    if @itemval = 4 set @odds = 1.3050
	    	if @itemval = 5 set @odds = 1.0398
	    end
	end
	--普通时候的赔率
	else
	begin
	    if @itemid <= 2
	    begin
		    set @odds = 1.94
	    end
	    else if @itemid = 3
	    begin
		    if @itemval in (3,18) set @odds = 183
		    if @itemval in (4,17) set @odds = 69.2
		    if @itemval in (5,16) set @odds = 34.1
		    if @itemval in (6,15) set @odds = 20.5
		    if @itemval in (7,14) set @odds = 13.6
		    if @itemval in (8,13) set @odds = 9.7
		    if @itemval in (9,12) set @odds = 8.2
		    if @itemval in (10,11) set @odds = 7.6
	    end
	    else if @itemid = 4
	    begin
	        if @itemval = 0 set @odds = 2.28
		    else set @odds = 13
	    end
	    else if @itemid = 5
	    begin
		    if @itemval = 0 set @odds = 26
		    else set @odds = 180
	    end
	    else if @itemid = 6
	    begin
	        if @itemval = 0 set @odds = 7.5
		    else set @odds = 34
	    end
	    else if @itemid = 7 or @itemid = 8
	    begin
	        set @odds = 1.6
	    end
	    else if @itemid = 9
	    begin
	        if @itemval in (1,10) set @odds = 7.4
		    if @itemval in (2,9) set @odds = 8.0
		    if @itemval in (3,8) set @odds = 9.0
		    if @itemval in (4,7) set @odds = 11.1
		    if @itemval in (5,6) set @odds = 12.4
	    end
	    else if @itemid = 10
	    begin
	        if @itemval = 1 set @odds = 5.9
		    if @itemval = 2 set @odds = 3.7
		    if @itemval = 3 set @odds = 3.3
		    if @itemval = 4 set @odds = 3.7
		    if @itemval = 5 set @odds = 5.9
	    end
	    else if @itemid = 11
	    begin
	   	    if @itemval = 1 set @odds = 1.0153
		    if @itemval = 2 set @odds = 1.1291
		    if @itemval = 3 set @odds = 1.4460
		    if @itemval = 4 set @odds = 1.3050
	    	if @itemval = 5 set @odds = 1.0398
	    end
		else if @itemid = 12 and @itemval between 1 and 20
		    set @odds = 28.6
	end

	return @odds
end


GO

