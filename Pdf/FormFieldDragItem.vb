Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.Pdf
Imports DevExpress.XtraPdfViewer


Public Class FormFieldDragItem
		Private item As FormFieldData
		Private dragPoints() As FormFieldDragPoint
		Private dragDirection As FormFieldDragDirection
		Private startDragLocation As PdfPoint
		Private viewer As PdfViewer
		Private controller As DocumentFormController

		Private ReadOnly Property IsDragging() As Boolean
			Get
				Return dragDirection <> FormFieldDragDirection.None
			End Get
		End Property

		Public Sub New(ByVal viewer As PdfViewer, ByVal item As FormFieldData, ByVal controller As DocumentFormController)
			Me.item = item
			Me.viewer = viewer
			Me.controller = controller
			Update()
		End Sub
		Public Sub UpdateCursor(ByVal clientPoint As PointF)
			Dim position As PdfDocumentPosition = viewer.GetDocumentPosition(clientPoint)
			If position.PageNumber = item.PageNumber Then
				For Each point As FormFieldDragPoint In dragPoints
					If point.Contains(position.Point) Then
						Select Case point.Direction
							Case FormFieldDragDirection.Move
								viewer.Cursor = Cursors.SizeAll
							Case FormFieldDragDirection.ResizeBottom, FormFieldDragDirection.ResizeTop
								viewer.Cursor = Cursors.SizeNS
							Case FormFieldDragDirection.ResizeTopLeft, FormFieldDragDirection.ResizeBottomRight
								viewer.Cursor = Cursors.SizeNWSE
							Case FormFieldDragDirection.ResizeBottomLeft, FormFieldDragDirection.ResizeTopRight
								viewer.Cursor = Cursors.SizeNESW
							Case FormFieldDragDirection.ResizeLeft, FormFieldDragDirection.ResizeRight
								viewer.Cursor = Cursors.SizeWE
						End Select
						Return
					End If
				Next point
				viewer.Cursor = Cursors.Default
			End If
		End Sub
		Public Sub Update()
			dragPoints = FormFieldDragPoint.CreateDragPoints(item.Rectangle.InnerRectangle)
			viewer.Invalidate()
		End Sub
		Public Function StartDrag(ByVal clientPoint As PointF) As Boolean
			Dim position As PdfDocumentPosition = viewer.GetDocumentPosition(clientPoint, False)
			If position IsNot Nothing AndAlso position.PageNumber = item.PageNumber Then
				startDragLocation = position.Point
				For Each dragPoint As FormFieldDragPoint In dragPoints
					If dragPoint.Contains(startDragLocation) Then
						dragDirection = dragPoint.Direction
						Return True
					End If
				Next dragPoint
			End If
			Return False
		End Function
		Public Sub ContinueDrag(ByVal clientPoint As PointF)
			If IsDragging Then
				Dim position As PdfDocumentPosition = viewer.GetDocumentPosition(clientPoint, True)
				If position.PageNumber = item.PageNumber Then
					Dim dx As Double = position.Point.X - startDragLocation.X
					Dim dy As Double = position.Point.Y - startDragLocation.Y
					startDragLocation = position.Point
					Dim formFieldRectangle As PdfRectangle = item.Rectangle.InnerRectangle
					Dim left As Double = formFieldRectangle.Left
					Dim bottom As Double = formFieldRectangle.Bottom
					Dim right As Double = formFieldRectangle.Right
					Dim top As Double = formFieldRectangle.Top
					Select Case dragDirection
						Case FormFieldDragDirection.Move
							formFieldRectangle = New PdfRectangle(left + dx, bottom + dy, right + dx, top + dy)
						Case FormFieldDragDirection.ResizeBottom
							formFieldRectangle = New PdfRectangle(left, Math.Min(bottom + dy, top - 1), right, top)
						Case FormFieldDragDirection.ResizeBottomLeft
							formFieldRectangle = New PdfRectangle(Math.Min(left + dx, right - 1), Math.Min(bottom + dy, top - 1), right, top)
						Case FormFieldDragDirection.ResizeBottomRight
							formFieldRectangle = New PdfRectangle(left, Math.Min(bottom + dy, top - 1), Math.Max(left + 1, right + dx), top)
						Case FormFieldDragDirection.ResizeLeft
							formFieldRectangle = New PdfRectangle(Math.Min(left + dx, right - 1), bottom, right, top)
						Case FormFieldDragDirection.ResizeRight
							formFieldRectangle = New PdfRectangle(left, bottom, Math.Max(left + 1, right + dx), top)
						Case FormFieldDragDirection.ResizeTop
							formFieldRectangle = New PdfRectangle(left, bottom, right, Math.Max(bottom + 1, top + dy))
						Case FormFieldDragDirection.ResizeTopLeft
							formFieldRectangle = New PdfRectangle(Math.Min(left + dx, right - 1), bottom, right, Math.Max(bottom + 1, top + dy))
						Case FormFieldDragDirection.ResizeTopRight
							formFieldRectangle = New PdfRectangle(left, bottom, Math.Max(left + 1, right + dx), Math.Max(bottom + 1, top + dy))
						Case Else
							Return
					End Select
					item.Rectangle.InnerRectangle = formFieldRectangle
					Update()
				End If
			End If
		End Sub
		Public Sub EndDrag(ByVal clientPoint As PointF)
			If IsDragging Then
				dragDirection = FormFieldDragDirection.None
				controller.UpdateDocument()
			End If
		End Sub
		Public Sub DrawDragPoints(ByVal g As Graphics, ByVal foreBrush As Brush)
			For i As Integer = 0 To 7
				Dim dragRect As PdfRectangle = dragPoints(i).Rectangle
				Dim topLeft As PointF = viewer.GetClientPoint(New PdfDocumentPosition(item.PageNumber, dragRect.TopLeft))
				Dim bottomRight As PointF = viewer.GetClientPoint(New PdfDocumentPosition(item.PageNumber, dragRect.BottomRight))
				Dim clientRect As RectangleF = RectangleF.FromLTRB(topLeft.X, topLeft.Y, bottomRight.X, bottomRight.Y)
				g.FillRectangle(foreBrush, clientRect)
			Next i
		End Sub
	End Class

