create table reservations.[user] (
	[user_id] int not null identity,
	[email] varchar(255) not null,
	[password] varchar(255) not null,
	[first_name] varchar(100) not null,
	[last_name] varchar(100) not null,
	[create_date] datetime not null
);

alter table reservations.[user]
add constraint pk_user primary key (user_id);

alter table reservations.[user]
add constraint uq_user_email unique(email);