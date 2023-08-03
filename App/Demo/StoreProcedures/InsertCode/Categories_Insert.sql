SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- Stored Procedure usp_Categories_Insert
-- Purpose: Insert one row in table Categories
-- Parameters:

	USE Northwind

	-- First delete the stored procedure if it already exists.
	If (EXISTS (SELECT * FROM dbo.sysobjects
			WHERE (Name = 'usp_Categories_Insert') AND (Type = 'P')))
		DROP PROCEDURE usp_Categories_Insert
	GO

-- This stored procedure was created 
-- on Saturday, December 02, 2006 at 10:33:54 PM.
-- Template: Insert One Row
-- Purpose: Insert one row in table Categories

 CREATE PROCEDURE usp_Categories_Insert
    @CategoryName nvarchar(15) ,
    @Description ntext ,
    @Picture image 
     AS 
         INSERT INTO [Categories]
     (
           [CategoryName] , 
           [Description] , 
           [Picture] 
	)
     VALUES 
     (
         @CategoryName ,
         @Description ,
         @Picture
	)

     GO 

-- End Procedure
