Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports DevExpress.Pdf

Public Class CheckBoxFormFieldData
		Inherits FormFieldData
		Private fExportValue As String = "Yes"
		Private buttonStyle As PdfAcroFormButtonStyle
		Private fIsChecked As Boolean
		Private fShouldGeneratePressedAppearance As Boolean

		<Category(AppearanceCategory)> _
		Public Property Style() As PdfAcroFormButtonStyle
			Get
				Return buttonStyle
			End Get
			Set(ByVal value As PdfAcroFormButtonStyle)
				buttonStyle = value
				UpdateModel()
			End Set
		End Property

		<Category(AppearanceCategory)> _
		Public Property IsChecked() As Boolean
			Get
				Return fIsChecked
			End Get
			Set(ByVal value As Boolean)
				fIsChecked = value
				UpdateModel()
			End Set
		End Property

		<Category(AppearanceCategory)> _
		Public Property ShouldGeneratePressedAppearance() As Boolean
			Get
				Return fShouldGeneratePressedAppearance
			End Get
			Set(ByVal value As Boolean)
				fShouldGeneratePressedAppearance = value
				UpdateModel()
			End Set
		End Property

		<Category(BehaviorCategory)> _
		Public Property ExportValue() As String
			Get
				Return fExportValue
			End Get
			Set(ByVal value As String)
				If String.IsNullOrEmpty(value) Then
					Throw New ArgumentException("The export value can't be null or empty string.")
				End If
				fExportValue = value
				UpdateModel()
			End Set
		End Property

		Public Sub New(ByVal position As PdfDocumentPosition, ByVal controller As DocumentFormController)
			MyBase.New(position, controller)
			Style = PdfAcroFormButtonStyle.Check
			ShouldGeneratePressedAppearance = True
		End Sub
		Protected Overrides Function CreateVisualFormField() As PdfAcroFormCommonVisualField
			Return New PdfAcroFormCheckBoxField(Name, PageNumber, Rectangle.InnerRectangle) With {.ButtonStyle = Style, .IsChecked = IsChecked, .ShouldGeneratePressedAppearance = ShouldGeneratePressedAppearance, .ExportValue = ExportValue}
		End Function
	End Class
