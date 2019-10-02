create table reservations.booking (
	booking_id uniqueidentifier not null,
	room_id uniqueidentifier not null,
	start_date datetimeoffset not null,
	end_date datetimeoffset not null,
	amount money not null
);

alter table reservations.booking
add constraint df_booking_booking_id default newid() for booking_id;

alter table reservations.booking
add constraint pk_booking primary key nonclustered (booking_id);

alter table reservations.booking
add constraint fk_booking_room_id_rooms_room_id foreign key (room_id) references reservations.room (room_id);