SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- Stored Procedure usp_Orders_Update
-- Purpose: Update an existing row in table Orders by its primary key.

	USE Northwind

	-- First delete the stored procedure if it already exists.
	If (EXISTS (SELECT * FROM dbo.sysobjects
			WHERE (Name = 'usp_Orders_Update') AND (Type = 'P')))
		DROP PROCEDURE usp_Orders_Update
	GO

-- This stored procedure was created 
-- on Saturday, December 02, 2006 at 10:33:54 PM.
-- Template: Update Row By Primary Key
-- Purpose: Update an existing row in table Orders by its primary key.

 CREATE PROCEDURE usp_Orders_Update
	(
@OrderID int
    ,@CustomerID nchar(5)
    ,@EmployeeID int
    ,@OrderDate datetime
    ,@RequiredDate datetime
    ,@ShippedDate datetime
    ,@ShipVia int
    ,@Freight money
    ,@ShipName nvarchar(40)
    ,@ShipAddress nvarchar(60)
    ,@ShipCity nvarchar(15)
    ,@ShipRegion nvarchar(15)
    ,@ShipPostalCode nvarchar(10)
    ,@ShipCountry nvarchar(15)

)
	AS
	SET NOCOUNT ON
	UPDATE [Orders]
	SET
[CustomerID] = @CustomerID
    ,[EmployeeID] = @EmployeeID
    ,[OrderDate] = @OrderDate
    ,[RequiredDate] = @RequiredDate
    ,[ShippedDate] = @ShippedDate
    ,[ShipVia] = @ShipVia
    ,[Freight] = @Freight
    ,[ShipName] = @ShipName
    ,[ShipAddress] = @ShipAddress
    ,[ShipCity] = @ShipCity
    ,[ShipRegion] = @ShipRegion
    ,[ShipPostalCode] = @ShipPostalCode
    ,[ShipCountry] = @ShipCountry

	WHERE(
[OrderID] =  @OrderID

)

	GO 
-- End Procedure
