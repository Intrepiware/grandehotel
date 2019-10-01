if not exists(select 1 from sys.schemas where name = N'reservations')
begin
	exec('create schema reservations;');
end
