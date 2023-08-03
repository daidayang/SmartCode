Imports System
Imports SmartCode.Template

Public Class Sample1
    Inherits TemplateBase

    Public Sub New()

        'The name of the template. For example: "Update Row By Primary Key".
        MyBase.Name = "Sample1"

        ' Whether a file is to be created as part of the code-generation process. 
        ' Although it is typical for a template to generate code to be placed in a single file, 
        ' there may be certain templates that need to generate multiple files.
        MyBase.CreateOutputFile = True

        'The description of what the template does. For example: "Generates a stored procedure to update a row by its primary key".
        Description = "Sample1 Description"

        'The relative path of the folder in which to place the file with the generated code
        'before generating code SmartCode asks in which folder to place the generated files. 
        'For example: "Stored Procedures\\UpdateByPK".
        MyBase.OutputFolder = "Folder\Subfolder"
    End Sub

    Public Overloads Overrides Function OutputFileName() As String
        'The actual name of the file in which to store the generated code. 
        'This property would only be available after the ProduceOutString method(see below) is called.
        ' For example: "Client_UpdateRowByPK.sql".
        Return Table.Code + "_Sample1.txt"
    End Function

    ' The routine that generates the code and places it in the internal System.String property "code". 
    ' To generate the code, this method uses the internal properties "Table" and "Domain", 
    ' which SmartCode will set prior to invoking it. 
    ' Table and Domain are properties of types Table and Domain, respectively
    Public Overloads Overrides Sub ProduceCode()
        WriteLine("This is some static content.")
        WriteLine("Table: " + Table.Name)
        For Each column As SmartCode.Model.ColumnSchema In Table.Columns
            WriteLine("Column: {0}", column.Name)
        Next
        WriteLine("This is more static content.")
        WriteLine()
    End Sub
End Class
