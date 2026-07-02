-- Creates the LifeV2 database if it does not exist.
-- EF Core migrations create the schema/tables; this only guarantees the DB exists.
IF DB_ID('LifeV2') IS NULL
BEGIN
    CREATE DATABASE [LifeV2];
END
GO
