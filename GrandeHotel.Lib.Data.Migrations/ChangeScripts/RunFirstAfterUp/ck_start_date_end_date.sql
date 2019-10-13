if exists (select 1 from dbo.sysobjects where id=object_id('reservations.ck_booking_start_date_end_date'))
begin
	alter table reservations.booking
	drop constraint ck_booking_start_date_end_date;
end;

alter table reservations.booking
add constraint ck_booking_start_date_end_date check (end_date > start_date)
