Imports Microsoft.VisualBasic
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Drawing.Design
Imports DevExpress.Pdf


Public Class TextBoxFormFieldData
		Inherits FormFieldData
		Private fText As String
		Private fMultiline As Boolean
		Private fType As PdfAcroFormTextFieldType

		<Category(AppearanceCategory), Editor(GetType(MultilineStringEditor), GetType(UITypeEditor))> _
		Public Property Text() As String
			Get
				Return fText
			End Get
			Set(ByVal value As String)
				fText = value
				UpdateModel()
			End Set
		End Property

		<Category(BehaviorCategory)> _
		Public Property Multiline() As Boolean
			Get
				Return fMultiline
			End Get
			Set(ByVal value As Boolean)
				fMultiline = value
				UpdateModel()
			End Set
		End Property

		<Category(AppearanceCategory)> _
		Public Property Type() As PdfAcroFormTextFieldType
			Get
				Return fType
			End Get
			Set(ByVal value As PdfAcroFormTextFieldType)
				fType = value
				UpdateModel()
			End Set
		End Property

		Public Sub New(ByVal position As PdfDocumentPosition, ByVal controller As DocumentFormController)
			MyBase.New(position, controller)
		End Sub
		Protected Overrides Function CreateVisualFormField() As PdfAcroFormCommonVisualField
			Return New PdfAcroFormTextBoxField(Name, PageNumber, Rectangle.InnerRectangle) With {.Text = Text, .Multiline = Multiline, .Type = Type}
		End Function
	End Class

