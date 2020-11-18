drop proc if exists reservations.usp_create_booking;

go

create proc reservations.usp_create_booking (
	@start_date datetimeoffset,
	@end_date datetimeoffset,
	@room_id uniqueidentifier
)
as
begin
	set nocount on;

	declare @rate money = (select top 1 nightly_rate
							from reservations.room
							where room.room_id = @room_id);

	if @rate is null throw 51000, 'Booking Exception: Room Id is invalid', 1;

	set transaction isolation level serializable;
	begin tran
	if not exists (select 1 from reservations.booking where start_date <= @end_date and end_date > @start_date and room_id = @room_id)
	begin
		declare @booking_id uniqueidentifier = newid();
		insert into reservations.booking(booking_id, room_id, start_date, end_date, amount, create_date)
		select	@booking_id, 
				@room_id, 
				@start_date, 
				@end_date,
				room.nightly_rate,
				getutcdate()
		from reservations.room
		where room_id = @room_id;
		commit;
		select @booking_id;
	end
	else
	begin
		rollback;
		throw 51000, 'Booking Exception: Booking conflicts with another booking', 1;
	end
end;

go