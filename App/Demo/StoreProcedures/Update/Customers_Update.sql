SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- Stored Procedure usp_Customers_Update
-- Purpose: Update an existing row in table Customers by its primary key.

	USE Northwind

	-- First delete the stored procedure if it already exists.
	If (EXISTS (SELECT * FROM dbo.sysobjects
			WHERE (Name = 'usp_Customers_Update') AND (Type = 'P')))
		DROP PROCEDURE usp_Customers_Update
	GO

-- This stored procedure was created 
-- on Saturday, December 02, 2006 at 10:33:54 PM.
-- Template: Update Row By Primary Key
-- Purpose: Update an existing row in table Customers by its primary key.

 CREATE PROCEDURE usp_Customers_Update
	(
@CustomerID nchar(5)
    ,@CompanyName nvarchar(40)
    ,@ContactName nvarchar(30)
    ,@ContactTitle nvarchar(30)
    ,@Address nvarchar(60)
    ,@City nvarchar(15)
    ,@Region nvarchar(15)
    ,@PostalCode nvarchar(10)
    ,@Country nvarchar(15)
    ,@Phone nvarchar(24)
    ,@Fax nvarchar(24)

)
	AS
	SET NOCOUNT ON
	UPDATE [Customers]
	SET
[CompanyName] = @CompanyName
    ,[ContactName] = @ContactName
    ,[ContactTitle] = @ContactTitle
    ,[Address] = @Address
    ,[City] = @City
    ,[Region] = @Region
    ,[PostalCode] = @PostalCode
    ,[Country] = @Country
    ,[Phone] = @Phone
    ,[Fax] = @Fax

	WHERE(
[CustomerID] =  @CustomerID

)

	GO 
-- End Procedure
