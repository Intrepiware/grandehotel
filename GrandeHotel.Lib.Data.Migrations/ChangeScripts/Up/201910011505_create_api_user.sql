BEGIN TRY
    CREATE LOGIN [grandehotel_api] WITH PASSWORD=N'testtest', DEFAULT_DATABASE=[master], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
END TRY
BEGIN CATCH

    RAISERROR('User ''grandehotel_api'' already exists', 1, 1);
END CATCH
GO
CREATE USER [grandehotel_api] FOR LOGIN [grandehotel_api]
