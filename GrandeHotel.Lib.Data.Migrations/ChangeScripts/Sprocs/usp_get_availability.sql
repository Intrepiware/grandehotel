drop procedure if exists reservations.usp_get_availability;

go

create procedure reservations.usp_get_availability (
	@days int,
	@check_in_offset_minutes int
)
as
begin

	set nocount on;

	declare @today datetime = dateadd(day, datediff(day, '20190101', getdate()), '20190101'),
			@start_i int = case when datepart(hour, getdate()) < 12 then -1 else 0 end;


	with x (n) as (
		select @start_i 
		union all 
		select (x.n + 1) n 
		from x 
		where x.n < @days
	), check_in as (
		select	dateadd(minute, (x.n * 60 * 24) + @check_in_offset_minutes, @today) at time zone 'central standard time'  [check_in],
				-- Same as above plus (1 day) minus (1 min)
				dateadd(minute, (x.n * 60 * 24) + @check_in_offset_minutes + 1439, @today) at time zone 'central standard time' [check_out]
		from x
	) 
	select	room.room_id,
			room.name,
			check_in.check_in,
			check_in.check_out
	from check_in
		cross join reservations.room
	where not exists (
		select	1
		from reservations.booking
		where booking.room_id = room.room_id
			and check_in.check_in between booking.start_date and booking.end_date
	)

end;