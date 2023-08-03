SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- Stored Procedure usp_Products_Insert
-- Purpose: Insert one row in table Products
-- Parameters:

	USE Northwind

	-- First delete the stored procedure if it already exists.
	If (EXISTS (SELECT * FROM dbo.sysobjects
			WHERE (Name = 'usp_Products_Insert') AND (Type = 'P')))
		DROP PROCEDURE usp_Products_Insert
	GO

-- This stored procedure was created 
-- on Saturday, December 02, 2006 at 10:33:54 PM.
-- Template: Insert One Row
-- Purpose: Insert one row in table Products

 CREATE PROCEDURE usp_Products_Insert
    @ProductName nvarchar(40) ,
    @SupplierID int ,
    @CategoryID int ,
    @QuantityPerUnit nvarchar(20) ,
    @UnitPrice money ,
    @UnitsInStock smallint ,
    @UnitsOnOrder smallint ,
    @ReorderLevel smallint ,
    @Discontinued bit 
     AS 
         INSERT INTO [Products]
     (
           [ProductName] , 
           [SupplierID] , 
           [CategoryID] , 
           [QuantityPerUnit] , 
           [UnitPrice] , 
           [UnitsInStock] , 
           [UnitsOnOrder] , 
           [ReorderLevel] , 
           [Discontinued] 
	)
     VALUES 
     (
         @ProductName ,
         @SupplierID ,
         @CategoryID ,
         @QuantityPerUnit ,
         @UnitPrice ,
         @UnitsInStock ,
         @UnitsOnOrder ,
         @ReorderLevel ,
         @Discontinued
	)

     GO 

-- End Procedure
