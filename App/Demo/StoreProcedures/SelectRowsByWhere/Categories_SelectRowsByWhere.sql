SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- Stored Procedure usp_Categories_SelectRowsByWhere
-- Purpose: Get All Rows from the table Categories
-- Parameters:

	USE Northwind

	-- First delete the stored procedure if it already exists.
	If (EXISTS (SELECT * FROM dbo.sysobjects
			WHERE (Name = 'usp_Categories_SelectRowsByWhere') AND (Type = 'P')))
		DROP PROCEDURE usp_Categories_SelectRowsByWhere
	GO

-- This stored procedure was created 
-- on Saturday, December 02, 2006 at 10:33:54 PM.
-- Template: Retrieve All Rows
-- Purpose: Get All Rows from the table Categories

 CREATE PROCEDURE usp_Categories_SelectRowsByWhere
 @SqlWhere varchar(250)
 AS
	IF NOT @SqlWhere = ''
	    exec('SELECT 
    [Categories].[CategoryID] as CategoryID,
    [Categories].[CategoryName] as CategoryName,
    [Categories].[Description] as Description,
    [Categories].[Picture] as Picture  FROM [Categories]  WHERE ' + @SqlWhere)
	ELSE 
	    exec('SELECT 
    [Categories].[CategoryID] as CategoryID,
    [Categories].[CategoryName] as CategoryName,
    [Categories].[Description] as Description,
    [Categories].[Picture] as Picture  FROM [Categories] ')

	GO 

-- End Procedure
