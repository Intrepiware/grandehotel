CREATE LOGIN [grandehotel_api] WITH PASSWORD=N'testtest', DEFAULT_DATABASE=[master], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
CREATE USER [grandehotel_api] FOR LOGIN [grandehotel_api]
