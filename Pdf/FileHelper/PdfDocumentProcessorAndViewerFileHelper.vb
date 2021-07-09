Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Windows.Forms
Imports DevExpress.Pdf
Imports DevExpress.XtraEditors
Imports DevExpress.XtraPdfViewer


Public Class PdfDocumentProcessorAndViewerFileHelper
		Inherits PdfFileHelper
		Implements IDisposable
		Private ReadOnly processorDocument As PdfDocumentProcessorFileHelper
		Private ReadOnly viewerDocument As PdfViewerFileHelper
		Private ReadOnly documentProcessor As PdfDocumentProcessor
		Private ReadOnly viewer As PdfViewer
		Private password As String

		Protected Overrides Property Creator() As String
			Get
				Return documentProcessor.Document.Creator
			End Get
			Set(ByVal value As String)
				documentProcessor.Document.Creator = value
			End Set
		End Property
		Protected Overrides Property Producer() As String
			Get
				Return documentProcessor.Document.Producer
			End Get
			Set(ByVal value As String)
				documentProcessor.Document.Producer = value
			End Set
		End Property

		Public Sub New(ByVal documentProcessor As PdfDocumentProcessor, ByVal viewer As PdfViewer)
			Me.documentProcessor = documentProcessor
			Me.viewer = viewer
			processorDocument = New PdfDocumentProcessorFileHelper(documentProcessor)
			viewerDocument = New PdfViewerFileHelper(viewer)
			AddHandler documentProcessor.PasswordRequested, AddressOf OnDocumentServerPasswordRequested
			AddHandler viewer.PasswordRequested, AddressOf OnViewerPasswordRequested
		End Sub
		Public Function LoadDocumentWithDialog() As Boolean
			Return PerformDocumentFromOpenDialog(AddressOf LoadDocument)
		End Function
		Public Function AppendDocument() As Boolean
			Try
				Using stream As New MemoryStream()
					If PerformDocumentFromOpenDialog(AddressOf AppendDocument) Then
						documentProcessor.SaveDocument(stream, True)
						stream.Position = 0
						viewerDocument.LoadDocument(stream)
						Return True
					End If
				End Using
			Catch
				XtraMessageBox.Show("Not enough memory to append a document.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
			End Try
			Return False
		End Function
		Public Overrides Sub LoadDocument(ByVal path As String, ByVal detach As Boolean)
			processorDocument.LoadDocument(path, detach)
			viewerDocument.LoadDocument(path, detach)
		End Sub
		Public Overrides Sub LoadDocument(ByVal stream As Stream)
			processorDocument.LoadDocument(stream)
			viewerDocument.LoadDocument(stream)
		End Sub
		Protected Overrides Sub SaveDocument(ByVal filePath As String, ByVal options As PdfSaveOptions)
			documentProcessor.SaveDocument(filePath, options)
		End Sub
		Private Sub AppendDocument(ByVal appendDocumentPath As String)
			documentProcessor.AppendDocument(appendDocumentPath)
		End Sub
		Private Function PerformDocumentFromOpenDialog(ByVal action As Action(Of String)) As Boolean
			Dim filePath As String = ShowFileDialog(Of OpenFileDialog)()
			If (Not String.IsNullOrEmpty(filePath)) Then
				Do
					Try
						action(filePath)
						Return True
					Catch e1 As PdfIncorrectPasswordException
						If password Is Nothing Then
							Exit Do
						End If
						XtraMessageBox.Show("The password is incorrect. Please make sure that Caps Lock is not enabled.", filePath, MessageBoxButtons.OK, MessageBoxIcon.Information)
					Catch e2 As OutOfMemoryException
						XtraMessageBox.Show(String.Format("Not enough memory to load the file." & Constants.vbCrLf & "{0}", filePath), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
						Exit Do
					Catch
						Dim message As String = "Unable to load the PDF document because the following file is not available or it is not a valid PDF document." & Constants.vbCrLf & "{0}" & Constants.vbCrLf & "Please ensure that the application can access this file and that it is valid, or specify a different file."
						XtraMessageBox.Show(String.Format(message, filePath), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
						Exit Do
					End Try
				Loop
			End If
			Return False
		End Function
		Private Sub OnDocumentServerPasswordRequested(ByVal sender As Object, ByVal e As PdfPasswordRequestedEventArgs)
			Using form As New PasswordForm(Path.GetFileName(e.FileName))
				form.StartPosition = FormStartPosition.CenterParent
				If form.ShowDialog() = DialogResult.OK Then
					e.PasswordString = form.Password
					password = form.Password
				Else
					password = Nothing
				End If
			End Using
		End Sub
		Private Sub OnViewerPasswordRequested(ByVal sender As Object, ByVal e As PdfPasswordRequestedEventArgs)
			e.PasswordString = password
		End Sub
		Public Sub Dispose() Implements IDisposable.Dispose
			RemoveHandler documentProcessor.PasswordRequested, AddressOf OnDocumentServerPasswordRequested
			RemoveHandler viewer.PasswordRequested, AddressOf OnViewerPasswordRequested
		End Sub
	End Class
 