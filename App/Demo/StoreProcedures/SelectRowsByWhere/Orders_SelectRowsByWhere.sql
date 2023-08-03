SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- Stored Procedure usp_Orders_SelectRowsByWhere
-- Purpose: Get All Rows from the table Orders
-- Parameters:

	USE Northwind

	-- First delete the stored procedure if it already exists.
	If (EXISTS (SELECT * FROM dbo.sysobjects
			WHERE (Name = 'usp_Orders_SelectRowsByWhere') AND (Type = 'P')))
		DROP PROCEDURE usp_Orders_SelectRowsByWhere
	GO

-- This stored procedure was created 
-- on Saturday, December 02, 2006 at 10:33:54 PM.
-- Template: Retrieve All Rows
-- Purpose: Get All Rows from the table Orders

 CREATE PROCEDURE usp_Orders_SelectRowsByWhere
 @SqlWhere varchar(250)
 AS
	IF NOT @SqlWhere = ''
	    exec('SELECT 
    [Orders].[OrderID] as OrderID,
    [Orders].[CustomerID] as CustomerID,
    [Orders].[EmployeeID] as EmployeeID,
    [Orders].[OrderDate] as OrderDate,
    [Orders].[RequiredDate] as RequiredDate,
    [Orders].[ShippedDate] as ShippedDate,
    [Orders].[ShipVia] as ShipVia,
    [Orders].[Freight] as Freight,
    [Orders].[ShipName] as ShipName,
    [Orders].[ShipAddress] as ShipAddress,
    [Orders].[ShipCity] as ShipCity,
    [Orders].[ShipRegion] as ShipRegion,
    [Orders].[ShipPostalCode] as ShipPostalCode,
    [Orders].[ShipCountry] as ShipCountry  FROM [Orders]      LEFT OUTER JOIN 
 Customers AS T0  ON  T0.[CustomerID] = [Orders].[CustomerID]      LEFT OUTER JOIN 
 Employees AS T1  ON  T1.[EmployeeID] = [Orders].[EmployeeID]      LEFT OUTER JOIN 
 Shippers AS T2  ON  T2.[ShipperID] = [Orders].[ShipVia]  WHERE ' + @SqlWhere)
	ELSE 
	    exec('SELECT 
    [Orders].[OrderID] as OrderID,
    [Orders].[CustomerID] as CustomerID,
    [Orders].[EmployeeID] as EmployeeID,
    [Orders].[OrderDate] as OrderDate,
    [Orders].[RequiredDate] as RequiredDate,
    [Orders].[ShippedDate] as ShippedDate,
    [Orders].[ShipVia] as ShipVia,
    [Orders].[Freight] as Freight,
    [Orders].[ShipName] as ShipName,
    [Orders].[ShipAddress] as ShipAddress,
    [Orders].[ShipCity] as ShipCity,
    [Orders].[ShipRegion] as ShipRegion,
    [Orders].[ShipPostalCode] as ShipPostalCode,
    [Orders].[ShipCountry] as ShipCountry  FROM [Orders]      LEFT OUTER JOIN 
 Customers AS T0  ON  T0.[CustomerID] = [Orders].[CustomerID]      LEFT OUTER JOIN 
 Employees AS T1  ON  T1.[EmployeeID] = [Orders].[EmployeeID]      LEFT OUTER JOIN 
 Shippers AS T2  ON  T2.[ShipperID] = [Orders].[ShipVia] ')

	GO 

-- End Procedure
