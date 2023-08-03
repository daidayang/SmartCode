SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- Stored Procedure usp_Categories_Update
-- Purpose: Update an existing row in table Categories by its primary key.

	USE Northwind

	-- First delete the stored procedure if it already exists.
	If (EXISTS (SELECT * FROM dbo.sysobjects
			WHERE (Name = 'usp_Categories_Update') AND (Type = 'P')))
		DROP PROCEDURE usp_Categories_Update
	GO

-- This stored procedure was created 
-- on Saturday, December 02, 2006 at 10:33:54 PM.
-- Template: Update Row By Primary Key
-- Purpose: Update an existing row in table Categories by its primary key.

 CREATE PROCEDURE usp_Categories_Update
	(
@CategoryID int
    ,@CategoryName nvarchar(15)
    ,@Description ntext
    ,@Picture image

)
	AS
	SET NOCOUNT ON
	UPDATE [Categories]
	SET
[CategoryName] = @CategoryName
    ,[Description] = @Description
    ,[Picture] = @Picture

	WHERE(
[CategoryID] =  @CategoryID

)

	GO 
-- End Procedure
