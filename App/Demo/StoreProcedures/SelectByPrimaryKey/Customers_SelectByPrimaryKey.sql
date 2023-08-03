SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- Stored Procedure usp_Customers_SelectByPrimaryKey
-- Purpose: Get an existing row in table Customers
-- Parameters:

	USE Northwind

	-- First delete the stored procedure if it already exists.
	If (EXISTS (SELECT * FROM dbo.sysobjects
			WHERE (Name = 'usp_Customers_SelectByPrimaryKey') AND (Type = 'P')))
		DROP PROCEDURE usp_Customers_SelectByPrimaryKey
	GO

-- This stored procedure was created 
-- on Saturday, December 02, 2006 at 10:33:54 PM.
-- Template: Retrieve Row by Primary Key
-- Purpose: Get an existing row in table Customers

 CREATE PROCEDURE usp_Customers_SelectByPrimaryKey
 (
     @CustomerID nchar(5)  
)
 AS
SELECT 
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
    [Customers].[Fax] as Fax  FROM [Customers] 
	WHERE 
(
  [Customers].[CustomerID] = @CustomerID 
)

	GO 

-- End Procedure
