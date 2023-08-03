Imports System
Imports SmartCode.Template
Imports TaHoGen
Imports System.IO
Imports System.Diagnostics
Imports TaHoGen.Targets
Imports System.Reflection

Public Class Sample3
    Inherits TemplateBase

    Public Sub New()
        MyBase.IsProjectTemplate = True
        MyBase.Name = "Sample3"
        MyBase.CreateOutputFile = True
        Description = "Demonstrates the most basic template."
        MyBase.OutputFolder = "VBBasicSamples"
    End Sub

    Public Overloads Overrides Function OutputFileName() As String
        Return "Sample3.vb"
    End Function

    Public Overloads Overrides Sub ProduceCode()
        Dim templateCode As String
        Dim templateReader As StreamReader

        templateReader = New StreamReader(TemplateBase.TemplatesBaseDirectory + "/BasicSamples/VisualBasic/Sample3.sct")
        templateCode = templateReader.ReadToEnd
        Dim templateAssembly As [Assembly] = TemplateCompiler.Compile(templateCode, "VBSample3.dll", True)
        If (templateAssembly Is Nothing) Then
            Throw New Exception("Template Compilation Failed!")
        End If
        Dim properties As New PropertyTable
        properties.Item("SampleBooleanProperty") = True
        properties.Item("SampleStringProperty") = "HelloPropertyName"
        Dim args As Object() = {properties}
        Dim templateType As Type = templateAssembly.GetType("Samples.Sample3", False, True)

        Dim generator As ITextGenerator = TryCast(Activator.CreateInstance(templateType, args), ITextGenerator)
        Dim output As New StringTarget
        output.Attach(generator)
        output.Write()

        Dim code As String = output.ToString
        Me.WriteLine(output.ToString)

    End Sub
End Class
