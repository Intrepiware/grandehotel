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
	set transaction isolation level serializable;
	begin tran
	if not exists (select 1 from reservations.booking where start_date <= @end_date and end_date > @start_date and room_id = @room_id)
	begin
		declare @booking_id uniqueidentifier = newid();
		insert into reservations.booking(booking_id, room_id, start_date, end_date, amount)
		values (@booking_id, @room_id, @start_date, @end_date, 1);
		commit;
		select @booking_id;
	end
	else
	begin
		rollback;
		throw 51000, 'Booking conflicts with another booking', 1;
	end
end;

go