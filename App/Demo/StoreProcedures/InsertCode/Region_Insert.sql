SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- Stored Procedure usp_Region_Insert
-- Purpose: Insert one row in table Region
-- Parameters:

	USE Northwind

	-- First delete the stored procedure if it already exists.
	If (EXISTS (SELECT * FROM dbo.sysobjects
			WHERE (Name = 'usp_Region_Insert') AND (Type = 'P')))
		DROP PROCEDURE usp_Region_Insert
	GO

-- This stored procedure was created 
-- on Saturday, December 02, 2006 at 10:33:54 PM.
-- Template: Insert One Row
-- Purpose: Insert one row in table Region

 CREATE PROCEDURE usp_Region_Insert
    @RegionID int ,
    @RegionDescription nchar(50) 
     AS 
         INSERT INTO [Region]
     (
           [RegionID] , 
           [RegionDescription] 
	)
     VALUES 
     (
         @RegionID ,
         @RegionDescription
	)

     GO 

-- End Procedure
