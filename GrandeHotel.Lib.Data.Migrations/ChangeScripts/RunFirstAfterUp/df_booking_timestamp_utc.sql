if exists (select 1 from dbo.sysobjects where id=object_id('reservations.df_booking_timestamp_utc'))
begin
	alter table reservations.booking
	drop constraint df_booking_timestamp_utc;
end;

alter table reservations.booking
add constraint df_booking_timestamp_utc default SYSUTCDATETIME() for timestamp_utc;