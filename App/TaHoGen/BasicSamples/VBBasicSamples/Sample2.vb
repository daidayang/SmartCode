Imports System
Imports SmartCode.Template
Imports TaHoGen
Imports System.IO
Imports System.Diagnostics
Imports TaHoGen.Targets
Imports System.Reflection

Public Class Sample2
    Inherits TemplateBase

    Public Sub New()


        'Run Once by Project, Available in the project Templates Tab, in the Setting Code Generator Dialog
        MyBase.IsProjectTemplate = True
        'The name of the template. For example: "Update Row By Primary Key".
        MyBase.Name = "Sample2"

        ' Whether a file is to be created as part of the code-generation process. 
        ' Although it is typical for a template to generate code to be placed in a single file, 
        ' there may be certain templates that need to generate multiple files.
        MyBase.CreateOutputFile = True

        'The description of what the template does. For example: "Generates a stored procedure to update a row by its primary key".
        Description = "Demonstrates the most basic template."

        'The relative path of the folder in which to place the file with the generated code
        'before generating code SmartCode asks in which folder to place the generated files. 
        'For example: "Stored Procedures\\UpdateByPK".
        MyBase.OutputFolder = "VBBasicSamples"
    End Sub

    Public Overloads Overrides Function OutputFileName() As String
        'The actual name of the file in which to store the generated code. 
        'This property would only be available after the ProduceOutString method(see below) is called.
        ' For example: "Client_UpdateRowByPK.sql".
        Return "Sample2.vb"
    End Function

    ' The routine that generates the code and places it in the internal System.String property "code". 
    ' To generate the code, this method uses the internal properties "Table" and "Domain", 
    ' which SmartCode will set prior to invoking it. 
    ' Table and Domain are properties of types Table and Domain, respectively
    Public Overloads Overrides Sub ProduceCode()
        Dim templateCode As String
        Dim templateReader As StreamReader

        'Read the contents of the template
        templateReader = New StreamReader(TemplateBase.TemplatesBaseDirectory + "/BasicSamples/VisualBasic/Sample2.sct")
        templateCode = templateReader.ReadToEnd


        'Compile it into a single assembly
        Dim templateAssembly As [Assembly] = TemplateCompiler.Compile(templateCode, "VBSample2.dll", True)

        'Did it succeed?
        If (templateAssembly Is Nothing) Then
            Throw New Exception("Template Compilation Failed!")
        End If

        'Set the properties for the template
        Dim properties As New PropertyTable
        'We are only setting the 'SampleStringProperty', the other one SampleBooleanProperty is setting to true as shown the Sample2.sct code
        properties.Item("SampleStringProperty") = "HelloPropertyName"
        Dim args As Object() = {properties}

        'Instantiate the template and assign the properties at the same time
        ' Create an instance of the StringBuilder type using Activator.CreateInstance.
        'Because VB Compilation add nice "My" thing is we need instanciate the class constructor with the full name
        'Try to use allways Namespace="MyNamespace"  ClassName="MyClassName" in CodeTemplate Tag
        'And instanciate with templateAssembly.GetType("MyNamespace.MyClassName", False, True)
        Dim templateType As Type = templateAssembly.GetType("Samples.Sample2", False, True)

        Dim generator As ITextGenerator = TryCast(Activator.CreateInstance(templateType, args), ITextGenerator)

        'Write to the string
        Dim output As New StringTarget

        'Attach the output of the generator to the console
        output.Attach(generator)

        'Generate the output itself
        output.Write()

        'Create the code and send it to SmartCode
        Dim code As String = output.ToString
        Me.WriteLine(code)

    End Sub
End Class
