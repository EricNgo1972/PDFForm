Imports Microsoft.VisualBasic
Imports System.IO
Imports DevExpress.Pdf
Imports DevExpress.XtraPdfViewer


Public Class PdfViewerFileHelper
		Inherits PdfFileHelper
		Private ReadOnly viewer As PdfViewer

		Protected Overrides Property Creator() As String
			Get
				Return viewer.DocumentCreator
			End Get
			Set(ByVal value As String)
				viewer.DocumentCreator = value
			End Set
		End Property
		Protected Overrides Property Producer() As String
			Get
				Return viewer.DocumentProducer
			End Get
			Set(ByVal value As String)
				viewer.DocumentProducer = value
			End Set
		End Property
		Public Sub New(ByVal processor As PdfViewer)
			Me.viewer = processor
		End Sub
		Public Overrides Sub LoadDocument(ByVal path As String, ByVal detach As Boolean)
			viewer.LoadDocument(path)
		End Sub
		Protected Overrides Sub SaveDocument(ByVal filePath As String, ByVal options As PdfSaveOptions)
			viewer.SaveDocument(filePath, options)
		End Sub
		Public Overrides Sub LoadDocument(ByVal stream As Stream)
			viewer.LoadDocument(stream)
		End Sub
	End Class
