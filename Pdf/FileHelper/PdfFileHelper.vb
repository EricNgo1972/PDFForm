Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Windows.Forms
Imports System.Security.Cryptography
Imports DevExpress.Pdf
Imports DevExpress.XtraEditors

Public MustInherit Class PdfFileHelper
    Private Const SaveErrorMessage As String = "Unable to save the PDF document." & Constants.vbCrLf & "{0}"
    Public Const DemoOpeningErrorMessage As String = "The demo data has been corrupted."

    Protected Shared Function ShowFileDialog(Of T As {FileDialog, New})() As String
        Using fileDialog As New T()
            fileDialog.Filter = "PDF Files (*.pdf)|*.pdf"
            fileDialog.RestoreDirectory = True
            If fileDialog.ShowDialog() = DialogResult.OK Then
                Return fileDialog.FileName
            End If
            Return Nothing
        End Using
    End Function

    Protected MustOverride Property Creator() As String
    Protected MustOverride Property Producer() As String

    Public Function LoadDemoDocument(ByVal documentName As String) As Boolean
        Return LoadDemoDocument(documentName, False)
    End Function
    Public Function LoadDemoDocument(ByVal documentName As String, ByVal detach As Boolean) As Boolean
        Try
            LoadDocument(documentName, detach)
            'LoadDocument(DemoUtils.GetRelativePath(documentName), detach)
            Return True
        Catch
            XtraMessageBox.Show(DemoOpeningErrorMessage, "Error")
        End Try
        Return False
    End Function
    Public Function SaveDocument() As Boolean
        Return SaveDocument(New PdfSaveOptions())
    End Function
    Public Function SaveDocument(ByVal options As PdfSaveOptions) As Boolean
        Dim fileName As String = ShowFileDialog(Of SaveFileDialog)()
        If (Not String.IsNullOrEmpty(fileName)) Then
            Try
                Creator = "PDF Document Processor Demo"
                Producer = "Developer Express Inc., " & AssemblyInfo.Version
                SaveDocument(fileName, options)
                Return True
            Catch exception As CryptographicException
                XtraMessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch
                XtraMessageBox.Show(String.Format(SaveErrorMessage, fileName), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End If
        Return False
    End Function
    Public Sub LoadDocument(ByVal path As String)
        LoadDocument(path, False)
    End Sub

    Public MustOverride Sub LoadDocument(ByVal path As String, ByVal detach As Boolean)
    Public MustOverride Sub LoadDocument(ByVal stream As Stream)
    Protected MustOverride Sub SaveDocument(ByVal filePath As String, ByVal options As PdfSaveOptions)
End Class

