SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- Stored Procedure usp_Categories_SelectByPrimaryKey
-- Purpose: Get an existing row in table Categories
-- Parameters:

	USE Northwind

	-- First delete the stored procedure if it already exists.
	If (EXISTS (SELECT * FROM dbo.sysobjects
			WHERE (Name = 'usp_Categories_SelectByPrimaryKey') AND (Type = 'P')))
		DROP PROCEDURE usp_Categories_SelectByPrimaryKey
	GO

-- This stored procedure was created 
-- on Saturday, December 02, 2006 at 10:33:54 PM.
-- Template: Retrieve Row by Primary Key
-- Purpose: Get an existing row in table Categories

 CREATE PROCEDURE usp_Categories_SelectByPrimaryKey
 (
     @CategoryID int  
)
 AS
SELECT 
    [Categories].[CategoryID] as CategoryID,
    [Categories].[CategoryName] as CategoryName,
    [Categories].[Description] as Description,
    [Categories].[Picture] as Picture  FROM [Categories] 
	WHERE 
(
  [Categories].[CategoryID] = @CategoryID 
)

	GO 

-- End Procedure
