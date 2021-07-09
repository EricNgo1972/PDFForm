Imports Microsoft.VisualBasic
Imports System.IO
Imports DevExpress.Pdf


Public Class PdfDocumentProcessorFileHelper
		Inherits PdfFileHelper
		Private ReadOnly documentProcessor As PdfDocumentProcessor

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

		Public Sub New(ByVal processor As PdfDocumentProcessor)
			Me.documentProcessor = processor
		End Sub
		Public Overrides Sub LoadDocument(ByVal path As String, ByVal detach As Boolean)
			documentProcessor.LoadDocument(path, detach)
		End Sub
		Public Overrides Sub LoadDocument(ByVal stream As Stream)
			documentProcessor.LoadDocument(stream, True)
		End Sub
		Protected Overrides Sub SaveDocument(ByVal filePath As String, ByVal options As PdfSaveOptions)
			documentProcessor.SaveDocument(filePath, options)
		End Sub
	End Class

