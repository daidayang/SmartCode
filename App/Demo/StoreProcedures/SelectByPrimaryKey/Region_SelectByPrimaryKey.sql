SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- Stored Procedure usp_Region_SelectByPrimaryKey
-- Purpose: Get an existing row in table Region
-- Parameters:

	USE Northwind

	-- First delete the stored procedure if it already exists.
	If (EXISTS (SELECT * FROM dbo.sysobjects
			WHERE (Name = 'usp_Region_SelectByPrimaryKey') AND (Type = 'P')))
		DROP PROCEDURE usp_Region_SelectByPrimaryKey
	GO

-- This stored procedure was created 
-- on Saturday, December 02, 2006 at 10:33:54 PM.
-- Template: Retrieve Row by Primary Key
-- Purpose: Get an existing row in table Region

 CREATE PROCEDURE usp_Region_SelectByPrimaryKey
 (
     @RegionID int  
)
 AS
SELECT 
    [Region].[RegionID] as RegionID,
    [Region].[RegionDescription] as RegionDescription  FROM [Region] 
	WHERE 
(
  [Region].[RegionID] = @RegionID 
)

	GO 

-- End Procedure
