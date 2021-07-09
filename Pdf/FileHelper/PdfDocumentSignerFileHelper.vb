Imports Microsoft.VisualBasic
Imports DevExpress.Pdf
Imports DevExpress.XtraPdfViewer
Imports System
Imports System.IO
Imports System.Windows.Forms


Public Class PdfDocumentSignerFileHelper
		Inherits PdfFileHelper
		Implements IDisposable
		Private ReadOnly viewerDocument As PdfViewerFileHelper
		Private ReadOnly viewer As PdfViewer
		Private signer As PdfDocumentSigner

		Protected Overrides Property Creator() As String
			Get
				Return ""
			End Get
			Set(ByVal value As String)
			End Set
		End Property
		Protected Overrides Property Producer() As String
			Get
				Return ""
			End Get
			Set(ByVal value As String)
			End Set
		End Property

		Public Sub New(ByVal viewer As PdfViewer)
			Me.viewer = viewer
			viewerDocument = New PdfViewerFileHelper(viewer)
		End Sub
		Public Overrides Sub LoadDocument(ByVal path As String, ByVal detach As Boolean)
			DisposeSigner()
			If detach Then
				Dim stream = New MemoryStream()
				Using fileStream = File.OpenRead(path)
					fileStream.CopyTo(stream)
				End Using
				signer = New PdfDocumentSigner(stream)
			Else
				signer = New PdfDocumentSigner(path)
			End If
			viewerDocument.LoadDocument(path, detach)
		End Sub
		Public Overrides Sub LoadDocument(ByVal stream As Stream)
			DisposeSigner()
			signer = New PdfDocumentSigner(stream)
			viewerDocument.LoadDocument(stream)
		End Sub
		Public Sub SignDocument(ByVal builder As PdfSignatureBuilder)
			If signer IsNot Nothing Then
				signer.SaveDocument(ShowFileDialog(Of SaveFileDialog)(), builder)
			End If
		End Sub
		Protected Overrides Sub SaveDocument(ByVal filePath As String, ByVal options As PdfSaveOptions)
			If signer IsNot Nothing Then
				signer.SaveDocument(filePath)
			End If
		End Sub
		Public Sub Dispose() Implements IDisposable.Dispose
			DisposeSigner()
		End Sub
		Private Sub DisposeSigner()
			If signer IsNot Nothing Then
				signer.Dispose()
			End If
		End Sub
	End Class

