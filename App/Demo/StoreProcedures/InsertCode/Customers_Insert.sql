SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- Stored Procedure usp_Customers_Insert
-- Purpose: Insert one row in table Customers
-- Parameters:

	USE Northwind

	-- First delete the stored procedure if it already exists.
	If (EXISTS (SELECT * FROM dbo.sysobjects
			WHERE (Name = 'usp_Customers_Insert') AND (Type = 'P')))
		DROP PROCEDURE usp_Customers_Insert
	GO

-- This stored procedure was created 
-- on Saturday, December 02, 2006 at 10:33:54 PM.
-- Template: Insert One Row
-- Purpose: Insert one row in table Customers

 CREATE PROCEDURE usp_Customers_Insert
    @CustomerID nchar(5) ,
    @CompanyName nvarchar(40) ,
    @ContactName nvarchar(30) ,
    @ContactTitle nvarchar(30) ,
    @Address nvarchar(60) ,
    @City nvarchar(15) ,
    @Region nvarchar(15) ,
    @PostalCode nvarchar(10) ,
    @Country nvarchar(15) ,
    @Phone nvarchar(24) ,
    @Fax nvarchar(24) 
     AS 
         INSERT INTO [Customers]
     (
           [CustomerID] , 
           [CompanyName] , 
           [ContactName] , 
           [ContactTitle] , 
           [Address] , 
           [City] , 
           [Region] , 
           [PostalCode] , 
           [Country] , 
           [Phone] , 
           [Fax] 
	)
     VALUES 
     (
         @CustomerID ,
         @CompanyName ,
         @ContactName ,
         @ContactTitle ,
         @Address ,
         @City ,
         @Region ,
         @PostalCode ,
         @Country ,
         @Phone ,
         @Fax
	)

     GO 

-- End Procedure
