create table reservations.room (
	room_id uniqueidentifier not null,
	name varchar(50) not null,
	nightly_rate money not null
);

alter table reservations.room
add constraint df_room_room_id default newid() for room_id;

alter table reservations.room
add constraint PK_room primary key nonclustered (room_id);