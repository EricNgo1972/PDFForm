Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports DevExpress.Pdf


Public Class FormFieldRectangle
		Private fInnerRectangle As New PdfRectangle(0, 0, 0, 0)
		Private ReadOnly controller As DocumentFormController

		Public Property Top() As Integer
			Get
				Return CInt(Fix(InnerRectangle.Top))
			End Get
			Set(ByVal value As Integer)
				InnerRectangle = New PdfRectangle(InnerRectangle.Left, InnerRectangle.Bottom, InnerRectangle.Right, value)
				controller.UpdateDocument()
			End Set
		End Property
		Public Property Bottom() As Integer
			Get
				Return CInt(Fix(fInnerRectangle.Bottom))
			End Get
			Set(ByVal value As Integer)
				InnerRectangle = New PdfRectangle(InnerRectangle.Left, value, InnerRectangle.Right, InnerRectangle.Top)
				controller.UpdateDocument()
			End Set
		End Property
		Public Property Left() As Integer
			Get
				Return CInt(Fix(InnerRectangle.Left))
			End Get
			Set(ByVal value As Integer)
				InnerRectangle = New PdfRectangle(value, InnerRectangle.Bottom, InnerRectangle.Right, InnerRectangle.Top)
				controller.UpdateDocument()
			End Set
		End Property
		Public Property Right() As Integer
			Get
				Return CInt(Fix(InnerRectangle.Right))
			End Get
			Set(ByVal value As Integer)
				InnerRectangle = New PdfRectangle(InnerRectangle.Left, InnerRectangle.Bottom, value, InnerRectangle.Top)
				controller.UpdateDocument()
			End Set
		End Property

		<Browsable(False)> _
		Public Property InnerRectangle() As PdfRectangle
			Get
				Return fInnerRectangle
			End Get
			Set(ByVal value As PdfRectangle)
				fInnerRectangle = value
			End Set
		End Property

		Public Sub New(ByVal startPoint As PdfPoint, ByVal endPoint As PdfPoint, ByVal controller As DocumentFormController)
			Me.controller = controller
			fInnerRectangle = New PdfRectangle(Math.Min(startPoint.X, endPoint.X), Math.Min(startPoint.Y, endPoint.Y), Math.Max(startPoint.X, endPoint.X), Math.Max(startPoint.Y, endPoint.Y))
		End Sub
		Public Overrides Function ToString() As String
			Return String.Format("Left={0}, Bottom={1}, Right={2}, Top={3}", Left, Bottom, Right, Top)
		End Function
		Public Sub Move(ByVal x As Double, ByVal y As Double)
			InnerRectangle = New PdfRectangle(InnerRectangle.Left + x, InnerRectangle.Bottom + y, InnerRectangle.Right + x, InnerRectangle.Top + y)
			controller.UpdateDocument()
		End Sub
	End Class
