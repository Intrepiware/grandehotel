ALTER ROLE [db_datareader] ADD MEMBER [grandehotel_api]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [grandehotel_api]
GO
grant execute on [reservations].[usp_get_availability] to [grandehotel_api];
GO
grant execute on [reservations].[usp_create_booking] to [grandehotel_api];