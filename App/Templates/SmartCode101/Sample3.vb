Imports System
Imports SmartCode.Template

Public Class Sample3
    Inherits TemplateBase

    Public Sub New()
        Name = "Sample3"
        CreateOutputFile = True
        Description = "This template demonstrates using template script blocks."
        OutputFolder = "Samples\Sample3"
        IsProjectTemplate = False
    End Sub

    Public Overloads Overrides Function OutputFileName() As String
        Return Table.Code + "_Sample3.txt"
    End Function

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
