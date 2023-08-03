SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- Stored Procedure usp_Employees_Update
-- Purpose: Update an existing row in table Employees by its primary key.

	USE Northwind

	-- First delete the stored procedure if it already exists.
	If (EXISTS (SELECT * FROM dbo.sysobjects
			WHERE (Name = 'usp_Employees_Update') AND (Type = 'P')))
		DROP PROCEDURE usp_Employees_Update
	GO

-- This stored procedure was created 
-- on Saturday, December 02, 2006 at 9:57:54 PM.
-- Template: Update Row By Primary Key
-- Purpose: Update an existing row in table Employees by its primary key.

 CREATE PROCEDURE usp_Employees_Update
	(
    @EmployeeID int
    ,    @LastName nvarchar(20)
    ,    @FirstName nvarchar(10)
    ,    @Title nvarchar(30)
    ,    @TitleOfCourtesy nvarchar(25)
    ,    @BirthDate datetime
    ,    @HireDate datetime
    ,    @Address nvarchar(60)
    ,    @City nvarchar(15)
    ,    @Region nvarchar(15)
    ,    @PostalCode nvarchar(10)
    ,    @Country nvarchar(15)
    ,    @HomePhone nvarchar(24)
    ,    @Extension nvarchar(4)
    ,    @Photo image
    ,    @Notes ntext
    ,    @ReportsTo int
    ,    @PhotoPath nvarchar(255)

)
	AS
	SET NOCOUNT ON
	UPDATE [Employees]
	SET
[LastName] = @LastName
    ,[FirstName] = @FirstName
    ,[Title] = @Title
    ,[TitleOfCourtesy] = @TitleOfCourtesy
    ,[BirthDate] = @BirthDate
    ,[HireDate] = @HireDate
    ,[Address] = @Address
    ,[City] = @City
    ,[Region] = @Region
    ,[PostalCode] = @PostalCode
    ,[Country] = @Country
    ,[HomePhone] = @HomePhone
    ,[Extension] = @Extension
    ,[Photo] = @Photo
    ,[Notes] = @Notes
    ,[ReportsTo] = @ReportsTo
    ,[PhotoPath] = @PhotoPath

	WHERE(
[EmployeeID] =  @EmployeeID

)

	GO 
-- End Procedure
