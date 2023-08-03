SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- Stored Procedure usp_Employees_Insert
-- Purpose: Insert one row in table Employees
-- Parameters:

	USE Northwind

	-- First delete the stored procedure if it already exists.
	If (EXISTS (SELECT * FROM dbo.sysobjects
			WHERE (Name = 'usp_Employees_Insert') AND (Type = 'P')))
		DROP PROCEDURE usp_Employees_Insert
	GO

-- This stored procedure was created 
-- on Saturday, December 02, 2006 at 9:57:54 PM.
-- Template: Insert One Row
-- Purpose: Insert one row in table Employees

 CREATE PROCEDURE usp_Employees_Insert
    @LastName nvarchar(20) ,
    @FirstName nvarchar(10) ,
    @Title nvarchar(30) ,
    @TitleOfCourtesy nvarchar(25) ,
    @BirthDate datetime ,
    @HireDate datetime ,
    @Address nvarchar(60) ,
    @City nvarchar(15) ,
    @Region nvarchar(15) ,
    @PostalCode nvarchar(10) ,
    @Country nvarchar(15) ,
    @HomePhone nvarchar(24) ,
    @Extension nvarchar(4) ,
    @Photo image ,
    @Notes ntext ,
    @ReportsTo int ,
    @PhotoPath nvarchar(255) 
     AS 
         INSERT INTO [Employees]
     (
           [LastName] , 
           [FirstName] , 
           [Title] , 
           [TitleOfCourtesy] , 
           [BirthDate] , 
           [HireDate] , 
           [Address] , 
           [City] , 
           [Region] , 
           [PostalCode] , 
           [Country] , 
           [HomePhone] , 
           [Extension] , 
           [Photo] , 
           [Notes] , 
           [ReportsTo] , 
           [PhotoPath] 
	)
     VALUES 
     (
         @LastName ,
         @FirstName ,
         @Title ,
         @TitleOfCourtesy ,
         @BirthDate ,
         @HireDate ,
         @Address ,
         @City ,
         @Region ,
         @PostalCode ,
         @Country ,
         @HomePhone ,
         @Extension ,
         @Photo ,
         @Notes ,
         @ReportsTo ,
         @PhotoPath
	)

     GO 

-- End Procedure
