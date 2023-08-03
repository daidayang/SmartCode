SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- Stored Procedure usp_Products_Update
-- Purpose: Update an existing row in table Products by its primary key.

	USE Northwind

	-- First delete the stored procedure if it already exists.
	If (EXISTS (SELECT * FROM dbo.sysobjects
			WHERE (Name = 'usp_Products_Update') AND (Type = 'P')))
		DROP PROCEDURE usp_Products_Update
	GO

-- This stored procedure was created 
-- on Saturday, December 02, 2006 at 10:33:54 PM.
-- Template: Update Row By Primary Key
-- Purpose: Update an existing row in table Products by its primary key.

 CREATE PROCEDURE usp_Products_Update
	(
@ProductID int
    ,@ProductName nvarchar(40)
    ,@SupplierID int
    ,@CategoryID int
    ,@QuantityPerUnit nvarchar(20)
    ,@UnitPrice money
    ,@UnitsInStock smallint
    ,@UnitsOnOrder smallint
    ,@ReorderLevel smallint
    ,@Discontinued bit

)
	AS
	SET NOCOUNT ON
	UPDATE [Products]
	SET
[ProductName] = @ProductName
    ,[SupplierID] = @SupplierID
    ,[CategoryID] = @CategoryID
    ,[QuantityPerUnit] = @QuantityPerUnit
    ,[UnitPrice] = @UnitPrice
    ,[UnitsInStock] = @UnitsInStock
    ,[UnitsOnOrder] = @UnitsOnOrder
    ,[ReorderLevel] = @ReorderLevel
    ,[Discontinued] = @Discontinued

	WHERE(
[ProductID] =  @ProductID

)

	GO 
-- End Procedure
