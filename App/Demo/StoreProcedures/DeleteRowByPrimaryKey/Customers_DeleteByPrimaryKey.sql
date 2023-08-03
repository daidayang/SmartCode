SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- Stored Procedure usp_Customers_DeleteByPrimaryKey
-- Purpose: Generates a stored procedure to delete a row by its primary key 
-- Parameters:

	USE Northwind

	-- First delete the stored procedure if it already exists.
	If (EXISTS (SELECT * FROM dbo.sysobjects
			WHERE (Name = 'usp_Customers_DeleteByPrimaryKey') AND (Type = 'P')))
		DROP PROCEDURE usp_Customers_DeleteByPrimaryKey
	GO

-- This stored procedure was created 
-- on Saturday, December 02, 2006 at 10:33:54 PM.
-- Template: Delete Row by Primary Key
-- Purpose: Generates a stored procedure to delete a row by its primary key 

 CREATE PROCEDURE usp_Customers_DeleteByPrimaryKey
 (
     @CustomerID nchar(5)  
)
 AS
 SET NOCOUNT ON
 DELETE FROM [Customers]
	WHERE 
(
  [CustomerID] = @CustomerID 
)

	GO 

-- End Procedure
