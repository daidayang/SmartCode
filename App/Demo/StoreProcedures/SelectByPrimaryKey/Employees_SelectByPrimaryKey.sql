SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- Stored Procedure usp_Employees_SelectByPrimaryKey
-- Purpose: Get an existing row in table Employees
-- Parameters:

	USE Northwind

	-- First delete the stored procedure if it already exists.
	If (EXISTS (SELECT * FROM dbo.sysobjects
			WHERE (Name = 'usp_Employees_SelectByPrimaryKey') AND (Type = 'P')))
		DROP PROCEDURE usp_Employees_SelectByPrimaryKey
	GO

-- This stored procedure was created 
-- on Saturday, December 02, 2006 at 9:57:54 PM.
-- Template: Retrieve Row by Primary Key
-- Purpose: Get an existing row in table Employees

 CREATE PROCEDURE usp_Employees_SelectByPrimaryKey
 (
     @EmployeeID int  
)
 AS
SELECT 
    [Employees].[EmployeeID] as EmployeeID,
    [Employees].[LastName] as LastName,
    [Employees].[FirstName] as FirstName,
    [Employees].[Title] as Title,
    [Employees].[TitleOfCourtesy] as TitleOfCourtesy,
    [Employees].[BirthDate] as BirthDate,
    [Employees].[HireDate] as HireDate,
    [Employees].[Address] as Address,
    [Employees].[City] as City,
    [Employees].[Region] as Region,
    [Employees].[PostalCode] as PostalCode,
    [Employees].[Country] as Country,
    [Employees].[HomePhone] as HomePhone,
    [Employees].[Extension] as Extension,
    [Employees].[Photo] as Photo,
    [Employees].[Notes] as Notes,
    [Employees].[ReportsTo] as ReportsTo,
    [Employees].[PhotoPath] as PhotoPath,
    [T0].[LastName] as ReportsTo_LastName,
    [T0].[FirstName] as ReportsTo_FirstName  FROM [Employees]      LEFT OUTER JOIN 
 Employees AS T0  ON  T0.[EmployeeID] = [Employees].[ReportsTo] 
	WHERE 
(
  [Employees].[EmployeeID] = @EmployeeID 
)

	GO 

-- End Procedure
