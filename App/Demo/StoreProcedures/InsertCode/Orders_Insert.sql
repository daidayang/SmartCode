SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- Stored Procedure usp_Orders_Insert
-- Purpose: Insert one row in table Orders
-- Parameters:

	USE Northwind

	-- First delete the stored procedure if it already exists.
	If (EXISTS (SELECT * FROM dbo.sysobjects
			WHERE (Name = 'usp_Orders_Insert') AND (Type = 'P')))
		DROP PROCEDURE usp_Orders_Insert
	GO

-- This stored procedure was created 
-- on Saturday, December 02, 2006 at 10:33:54 PM.
-- Template: Insert One Row
-- Purpose: Insert one row in table Orders

 CREATE PROCEDURE usp_Orders_Insert
    @CustomerID nchar(5) ,
    @EmployeeID int ,
    @OrderDate datetime ,
    @RequiredDate datetime ,
    @ShippedDate datetime ,
    @ShipVia int ,
    @Freight money ,
    @ShipName nvarchar(40) ,
    @ShipAddress nvarchar(60) ,
    @ShipCity nvarchar(15) ,
    @ShipRegion nvarchar(15) ,
    @ShipPostalCode nvarchar(10) ,
    @ShipCountry nvarchar(15) 
     AS 
         INSERT INTO [Orders]
     (
           [CustomerID] , 
           [EmployeeID] , 
           [OrderDate] , 
           [RequiredDate] , 
           [ShippedDate] , 
           [ShipVia] , 
           [Freight] , 
           [ShipName] , 
           [ShipAddress] , 
           [ShipCity] , 
           [ShipRegion] , 
           [ShipPostalCode] , 
           [ShipCountry] 
	)
     VALUES 
     (
         @CustomerID ,
         @EmployeeID ,
         @OrderDate ,
         @RequiredDate ,
         @ShippedDate ,
         @ShipVia ,
         @Freight ,
         @ShipName ,
         @ShipAddress ,
         @ShipCity ,
         @ShipRegion ,
         @ShipPostalCode ,
         @ShipCountry
	)

     GO 

-- End Procedure
