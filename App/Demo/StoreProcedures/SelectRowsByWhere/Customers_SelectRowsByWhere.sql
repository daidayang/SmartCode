SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- Stored Procedure usp_Customers_SelectRowsByWhere
-- Purpose: Get All Rows from the table Customers
-- Parameters:

	USE Northwind

	-- First delete the stored procedure if it already exists.
	If (EXISTS (SELECT * FROM dbo.sysobjects
			WHERE (Name = 'usp_Customers_SelectRowsByWhere') AND (Type = 'P')))
		DROP PROCEDURE usp_Customers_SelectRowsByWhere
	GO

-- This stored procedure was created 
-- on Saturday, December 02, 2006 at 10:33:54 PM.
-- Template: Retrieve All Rows
-- Purpose: Get All Rows from the table Customers

 CREATE PROCEDURE usp_Customers_SelectRowsByWhere
 @SqlWhere varchar(250)
 AS
	IF NOT @SqlWhere = ''
	    exec('SELECT 
    [Customers].[CustomerID] as CustomerID,
    [Customers].[CompanyName] as CompanyName,
    [Customers].[ContactName] as ContactName,
    [Customers].[ContactTitle] as ContactTitle,
    [Customers].[Address] as Address,
    [Customers].[City] as City,
    [Customers].[Region] as Region,
    [Customers].[PostalCode] as PostalCode,
    [Customers].[Country] as Country,
    [Customers].[Phone] as Phone,
    [Customers].[Fax] as Fax  FROM [Customers]  WHERE ' + @SqlWhere)
	ELSE 
	    exec('SELECT 
    [Customers].[CustomerID] as CustomerID,
    [Customers].[CompanyName] as CompanyName,
    [Customers].[ContactName] as ContactName,
    [Customers].[ContactTitle] as ContactTitle,
    [Customers].[Address] as Address,
    [Customers].[City] as City,
    [Customers].[Region] as Region,
    [Customers].[PostalCode] as PostalCode,
    [Customers].[Country] as Country,
    [Customers].[Phone] as Phone,
    [Customers].[Fax] as Fax  FROM [Customers] ')

	GO 

-- End Procedure
