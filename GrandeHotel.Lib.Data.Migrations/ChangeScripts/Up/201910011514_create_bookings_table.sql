create table reservations.bookings (
	booking_id uniqueidentifier not null,
	room_id uniqueidentifier not null,
	start_date datetimeoffset not null,
	end_date datetimeoffset not null,
	amount money not null
);

alter table reservations.bookings
add constraint df_bookings_booking_id default newid() for booking_id;

alter table reservations.bookings
add constraint pk_bookings primary key nonclustered (booking_id);

alter table reservations.bookings
add constraint fk_bookings_room_id_rooms_room_id foreign key (room_id) references reservations.rooms (room_id);