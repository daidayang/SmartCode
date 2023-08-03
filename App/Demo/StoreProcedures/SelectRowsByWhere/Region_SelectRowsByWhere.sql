SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- Stored Procedure usp_Region_SelectRowsByWhere
-- Purpose: Get All Rows from the table Region
-- Parameters:

	USE Northwind

	-- First delete the stored procedure if it already exists.
	If (EXISTS (SELECT * FROM dbo.sysobjects
			WHERE (Name = 'usp_Region_SelectRowsByWhere') AND (Type = 'P')))
		DROP PROCEDURE usp_Region_SelectRowsByWhere
	GO

-- This stored procedure was created 
-- on Saturday, December 02, 2006 at 10:33:54 PM.
-- Template: Retrieve All Rows
-- Purpose: Get All Rows from the table Region

 CREATE PROCEDURE usp_Region_SelectRowsByWhere
 @SqlWhere varchar(250)
 AS
	IF NOT @SqlWhere = ''
	    exec('SELECT 
    [Region].[RegionID] as RegionID,
    [Region].[RegionDescription] as RegionDescription  FROM [Region]  WHERE ' + @SqlWhere)
	ELSE 
	    exec('SELECT 
    [Region].[RegionID] as RegionID,
    [Region].[RegionDescription] as RegionDescription  FROM [Region] ')

	GO 

-- End Procedure
