SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- Stored Procedure usp_Products_SelectByPrimaryKey
-- Purpose: Get an existing row in table Products
-- Parameters:

	USE Northwind

	-- First delete the stored procedure if it already exists.
	If (EXISTS (SELECT * FROM dbo.sysobjects
			WHERE (Name = 'usp_Products_SelectByPrimaryKey') AND (Type = 'P')))
		DROP PROCEDURE usp_Products_SelectByPrimaryKey
	GO

-- This stored procedure was created 
-- on Saturday, December 02, 2006 at 10:33:54 PM.
-- Template: Retrieve Row by Primary Key
-- Purpose: Get an existing row in table Products

 CREATE PROCEDURE usp_Products_SelectByPrimaryKey
 (
     @ProductID int  
)
 AS
SELECT 
    [Products].[ProductID] as ProductID,
    [Products].[ProductName] as ProductName,
    [Products].[SupplierID] as SupplierID,
    [Products].[CategoryID] as CategoryID,
    [Products].[QuantityPerUnit] as QuantityPerUnit,
    [Products].[UnitPrice] as UnitPrice,
    [Products].[UnitsInStock] as UnitsInStock,
    [Products].[UnitsOnOrder] as UnitsOnOrder,
    [Products].[ReorderLevel] as ReorderLevel,
    [Products].[Discontinued] as Discontinued  FROM [Products]      LEFT OUTER JOIN 
 Categories AS T0  ON  T0.[CategoryID] = [Products].[CategoryID]      LEFT OUTER JOIN 
 Suppliers AS T1  ON  T1.[SupplierID] = [Products].[SupplierID] 
	WHERE 
(
  [Products].[ProductID] = @ProductID 
)

	GO 

-- End Procedure
