Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.ComponentModel
Imports System.Drawing.Design
Imports DevExpress.Pdf


Public Class FormFieldAppearance
		Implements IDisposable
		Private Shared Function ToPdfColor(ByVal color As Color) As PdfRGBColor
			Return If(color.IsEmpty, Nothing, New PdfRGBColor(color.R / 255.0, color.G / 255.0, color.B / 255.0))
		End Function

		Private ReadOnly controller As DocumentFormController
		Private fFont As New Font("Microsoft Sans Serif", 12)
		Private fBackgroundColor As Color
		Private fForeColor As Color
		Private fBorderColor As Color
		Private fBorderWidth As Double
		Private fBorderStyle As PdfAcroFormBorderStyle

		<TypeConverter(GetType(ColorConverter))> _
		Public Property BackgroundColor() As Color
			Get
				Return fBackgroundColor
			End Get
			Set(ByVal value As Color)
				fBackgroundColor = value
				controller.UpdateDocument()
			End Set
		End Property

		<TypeConverter(GetType(ColorConverter))> _
		Public Property ForeColor() As Color
			Get
				Return fForeColor
			End Get
			Set(ByVal value As Color)
				fForeColor = value
				controller.UpdateDocument()
			End Set
		End Property

		<TypeConverter(GetType(ColorConverter))> _
		Public Property BorderColor() As Color
			Get
				Return fBorderColor
			End Get
			Set(ByVal value As Color)
				fBorderColor = value
				controller.UpdateDocument()
			End Set
		End Property

		<Editor(GetType(FormFieldFontEditor), GetType(UITypeEditor))> _
		Public Property Font() As Font
			Get
				Return fFont
			End Get
			Set(ByVal value As Font)
				If fFont IsNot Nothing Then
					fFont.Dispose()
				End If
				fFont = value
				controller.UpdateDocument()
			End Set
		End Property
		Public Property BorderWidth() As Double
			Get
				Return fBorderWidth
			End Get
			Set(ByVal value As Double)
				fBorderWidth = value
				controller.UpdateDocument()
			End Set
		End Property
		Public Property BorderStyle() As PdfAcroFormBorderStyle
			Get
				Return fBorderStyle
			End Get
			Set(ByVal value As PdfAcroFormBorderStyle)
				fBorderStyle = value
				controller.UpdateDocument()
			End Set
		End Property

		Public Sub New(ByVal controller As DocumentFormController)
			Me.controller = controller
		End Sub
		Public Function CreateAcroFormFieldAppearance() As PdfAcroFormFieldAppearance
			Dim acroFormFieldAppearance As New PdfAcroFormFieldAppearance()
			acroFormFieldAppearance.BackgroundColor = ToPdfColor(BackgroundColor)
			acroFormFieldAppearance.ForeColor = ToPdfColor(ForeColor)
			acroFormFieldAppearance.BorderAppearance = New PdfAcroFormBorderAppearance() With {.Color = ToPdfColor(BorderColor), .Width = BorderWidth, .Style = BorderStyle}
			Dim font As Font = Me.Font
			If font Is Nothing Then
				acroFormFieldAppearance.FontFamily = Nothing
				acroFormFieldAppearance.FontSize = 0
				acroFormFieldAppearance.FontStyle = PdfFontStyle.Regular
			Else
				acroFormFieldAppearance.FontFamily = font.FontFamily.Name
				acroFormFieldAppearance.FontSize = font.SizeInPoints
				acroFormFieldAppearance.FontStyle = CType(font.Style, PdfFontStyle)
			End If
			Return acroFormFieldAppearance
		End Function
		Public Sub Dispose() Implements IDisposable.Dispose
			If fFont IsNot Nothing Then
				fFont.Dispose()
			End If
		End Sub
		Public Overrides Function ToString() As String
			Return "Appearance"
		End Function
	End Class
