Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Collections.Generic
Imports DevExpress.Pdf
Imports DevExpress.XtraPdfViewer


Public Class DocumentFormController
    Implements IDisposable
    Private Const prefix As String = "dxFormField"

    Private ReadOnly editingTempFilePath As String
    Private ReadOnly sourceTempFilePath As String
    Private ReadOnly fields As IList(Of FormFieldData) = New List(Of FormFieldData)()
    Private ReadOnly existingNames As New HashSet(Of String)()
    Private ReadOnly pdfViewer As PdfViewer
    Private ReadOnly inactive As New Pen(SystemColors.ActiveBorder) With {.DashStyle = DashStyle.Dash}
    Private ReadOnly highlight As New Pen(SystemColors.Highlight, 2) With {.DashStyle = DashStyle.Dash}

    Private counter As Integer = 1

    Public ReadOnly Property PageCount() As Integer
        Get
            Return pdfViewer.PageCount
        End Get
    End Property

    Public Sub New(ByVal pdfViewer As PdfViewer)
        Me.pdfViewer = pdfViewer
    End Sub

    Public Sub New(ByVal pdfViewer As PdfViewer, ByVal sourceTempFilePath As String, ByVal editingTempFilePath As String)
        Me.pdfViewer = pdfViewer
        Me.sourceTempFilePath = sourceTempFilePath
        Me.editingTempFilePath = editingTempFilePath
        Using processor As New PdfDocumentProcessor()
            processor.LoadDocument(sourceTempFilePath)
            Dim form As PdfInteractiveForm = processor.Document.AcroForm
            If form IsNot Nothing Then
                For Each field As PdfInteractiveFormField In form.Fields
                    existingNames.Add(field.Name)
                Next field
            End If
        End Using
    End Sub
    Public Sub UpdateDocument()
        Dim scrollPosition As Single = pdfViewer.VerticalScrollPosition
        pdfViewer.CloseDocument()
        Try
            Using processor As New PdfDocumentProcessor()
                processor.LoadDocument(sourceTempFilePath)
                Dim acroFields As New List(Of PdfAcroFormField)(fields.Count)
                For Each item As FormFieldData In fields
                    acroFields.Add(item.CreateAcroFormField())
                Next item
                processor.AddFormFields(acroFields.ToArray())
                processor.SaveDocument(editingTempFilePath)
            End Using
        Finally
            CType(New PdfViewerFileHelper(pdfViewer), PdfViewerFileHelper).LoadDocument(editingTempFilePath)
            pdfViewer.VerticalScrollPosition = scrollPosition
        End Try
    End Sub
    Public Function GetNextName() As String
        Dim name As String
        Do
            name = prefix & counter
            counter += 1
        Loop While existingNames.Contains(name)
        Return name
    End Function
    Public Sub AddField(ByVal field As FormFieldData)
        existingNames.Add(field.Name)
        fields.Add(field)
        UpdateDocument()
    End Sub
    Public Sub RemoveField(ByVal field As FormFieldData)
        fields.Remove(field)
        existingNames.Remove(field.Name)
        UpdateDocument()

    End Sub
    Public Function GetFormFieldFromPoint(ByVal point As Point) As FormFieldData
        For Each item As FormFieldData In fields
            If item.ContainsPosition(pdfViewer.GetDocumentPosition(point)) Then
                Return item
            End If
        Next item
        Return Nothing

    End Function
    Public Sub RemoveExistingForm()
        Using processor As New PdfDocumentProcessor()
            processor.LoadDocument(sourceTempFilePath)
            processor.RemoveForm()
            processor.SaveDocument(sourceTempFilePath)
        End Using
        UpdateDocument()
    End Sub

    Public Sub Draw(ByVal graphics As Graphics)
        For Each item As FormFieldData In fields
            DrawItem(graphics, item.GetClientRectangle(pdfViewer))
        Next item
    End Sub
    Public Sub DrawSelectedItem(ByVal graphics As Graphics, ByVal itemRect As Rectangle)
        graphics.DrawRectangle(highlight, itemRect)
    End Sub
    Public Sub DrawItem(ByVal graphics As Graphics, ByVal itemRect As Rectangle)
        graphics.DrawRectangle(inactive, itemRect)
    End Sub
    Public Sub Dispose() Implements IDisposable.Dispose
        For Each field As FormFieldData In fields
            field.Dispose()
        Next field
        fields.Clear()
        If File.Exists(sourceTempFilePath) Then
            File.Delete(sourceTempFilePath)
        End If
        inactive.Dispose()
        highlight.Dispose()
    End Sub
End Class

