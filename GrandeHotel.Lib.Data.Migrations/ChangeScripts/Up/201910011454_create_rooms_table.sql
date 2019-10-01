create table reservations.rooms (
	room_id uniqueidentifier not null,
	name varchar(50) not null,
	nightly_rate money not null
);

alter table reservations.rooms
add constraint df_rooms_room_id default newid() for room_id;

alter table reservations.rooms
add constraint PK_rooms primary key nonclustered (room_id);