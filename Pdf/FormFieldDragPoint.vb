Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Pdf


Public Enum FormFieldDragDirection
		None
		Move
		ResizeTop
		ResizeBottom
		ResizeLeft
		ResizeRight
		ResizeTopLeft
		ResizeTopRight
		ResizeBottomLeft
		ResizeBottomRight
	End Enum

	Public Class FormFieldDragPoint
		Private Const dragPointHalfSize As Double = 5

		Public Shared Function CreateDragPoints(ByVal formFieldRectangle As PdfRectangle) As FormFieldDragPoint()
			Dim points(8) As FormFieldDragPoint
			Dim left As Double = formFieldRectangle.Left
			Dim bottom As Double = formFieldRectangle.Bottom
			Dim right As Double = formFieldRectangle.Right
			Dim top As Double = formFieldRectangle.Top
			Dim centerX As Double = left + formFieldRectangle.Width / 2
			Dim centerY As Double = bottom + formFieldRectangle.Height / 2
			points(0) = New FormFieldDragPoint(left, top, FormFieldDragDirection.ResizeTopLeft)
			points(1) = New FormFieldDragPoint(centerX, bottom, FormFieldDragDirection.ResizeBottom)
			points(2) = New FormFieldDragPoint(left, bottom, FormFieldDragDirection.ResizeBottomLeft)
			points(3) = New FormFieldDragPoint(right, bottom, FormFieldDragDirection.ResizeBottomRight)
			points(4) = New FormFieldDragPoint(left, centerY, FormFieldDragDirection.ResizeLeft)
			points(5) = New FormFieldDragPoint(right, centerY, FormFieldDragDirection.ResizeRight)
			points(6) = New FormFieldDragPoint(centerX, top, FormFieldDragDirection.ResizeTop)
			points(7) = New FormFieldDragPoint(right, top, FormFieldDragDirection.ResizeTopRight)
			points(8) = New FormFieldDragPoint(formFieldRectangle, FormFieldDragDirection.Move)
			Return points
		End Function

		Private ReadOnly fDirection As FormFieldDragDirection
		Private ReadOnly dragRectangle As PdfRectangle

		Public ReadOnly Property Direction() As FormFieldDragDirection
			Get
				Return fDirection
			End Get
		End Property
		Public ReadOnly Property Rectangle() As PdfRectangle
			Get
				Return dragRectangle
			End Get
		End Property

		Private Sub New(ByVal x As Double, ByVal y As Double, ByVal direction As FormFieldDragDirection)
			dragRectangle = New PdfRectangle(x - dragPointHalfSize, y - dragPointHalfSize, x + dragPointHalfSize, y + dragPointHalfSize)
			fDirection = direction
		End Sub
		Private Sub New(ByVal dragRectangle As PdfRectangle, ByVal direction As FormFieldDragDirection)
			Me.dragRectangle = dragRectangle
			fDirection = direction
		End Sub
		Public Function Contains(ByVal point As PdfPoint) As Boolean
			Return dragRectangle.Contains(point)
		End Function
	End Class
 