insert into reservations.room(room_id, name, nightly_rate) values 
('00000000-0000-0000-0000-000000000001', 'Room 101', 1)

insert into reservations.booking(booking_id, room_id, start_date, end_date, amount, create_date) values
('00000000-0000-0000-0000-000000000010', '00000000-0000-0000-0000-000000000001', '2019-01-01 12:00:00-06:00', '2019-01-05 11:59:00-06:00', 1, getutcdate())