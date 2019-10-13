alter table reservations.booking
add [timestamp_utc] datetime null;

go

update reservations.booking
set [timestamp_utc] = SYSUTCDATETIME();

go

alter table reservations.booking
alter column [timestamp_utc] datetime not null;

