SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- Stored Procedure usp_Products_DeleteByPrimaryKey
-- Purpose: Generates a stored procedure to delete a row by its primary key 
-- Parameters:

	USE Northwind

	-- First delete the stored procedure if it already exists.
	If (EXISTS (SELECT * FROM dbo.sysobjects
			WHERE (Name = 'usp_Products_DeleteByPrimaryKey') AND (Type = 'P')))
		DROP PROCEDURE usp_Products_DeleteByPrimaryKey
	GO

-- This stored procedure was created 
-- on Saturday, December 02, 2006 at 10:33:54 PM.
-- Template: Delete Row by Primary Key
-- Purpose: Generates a stored procedure to delete a row by its primary key 

 CREATE PROCEDURE usp_Products_DeleteByPrimaryKey
 (
     @ProductID int  
)
 AS
 SET NOCOUNT ON
 DELETE FROM [Products]
	WHERE 
(
  [ProductID] = @ProductID 
)

	GO 

-- End Procedure
