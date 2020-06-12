alter table reservations.[user]
add constraint df_user_create_date
default(getdate()) for create_date