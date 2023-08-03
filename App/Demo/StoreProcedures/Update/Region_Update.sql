SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- Stored Procedure usp_Region_Update
-- Purpose: Update an existing row in table Region by its primary key.

	USE Northwind

	-- First delete the stored procedure if it already exists.
	If (EXISTS (SELECT * FROM dbo.sysobjects
			WHERE (Name = 'usp_Region_Update') AND (Type = 'P')))
		DROP PROCEDURE usp_Region_Update
	GO

-- This stored procedure was created 
-- on Saturday, December 02, 2006 at 10:33:54 PM.
-- Template: Update Row By Primary Key
-- Purpose: Update an existing row in table Region by its primary key.

 CREATE PROCEDURE usp_Region_Update
	(
@RegionID int
    ,@RegionDescription nchar(50)

)
	AS
	SET NOCOUNT ON
	UPDATE [Region]
	SET
[RegionDescription] = @RegionDescription

	WHERE(
[RegionID] =  @RegionID

)

	GO 
-- End Procedure
