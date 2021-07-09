Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.ComponentModel
Imports DevExpress.Pdf
Imports DevExpress.XtraPdfViewer


Public NotInheritable Class FormFieldType
		Public Const TextBox As String = "textBox"
		Public Const ComboBox As String = "comboBox"
		Public Const ListBox As String = "listBox"
		Public Const CheckBox As String = "checkBox"
		Public Const Signature As String = "signature"

		Private Sub New()
		End Sub
		Public Shared Function IsValidType(ByVal type As String) As Boolean
			Return (Not String.IsNullOrEmpty(type)) AndAlso (type = TextBox OrElse type = Signature OrElse type = ListBox OrElse type = ComboBox OrElse type = CheckBox)
		End Function
	End Class

	Public MustInherit Class FormFieldData
		Implements IDisposable
		Protected Const AppearanceCategory As String = "Appearance"
		Protected Const LayoutCategory As String = "Layout"
		Protected Const DesignCategory As String = "Design"
		Protected Const BehaviorCategory As String = "Behavior"

		Public Shared Function Create(ByVal fieldType As String, ByVal position As PdfDocumentPosition, ByVal controller As DocumentFormController) As FormFieldData
			If position IsNot Nothing Then
				Select Case fieldType
					Case FormFieldType.ComboBox
						Return New ComboBoxFormFieldData(position, controller)
					Case FormFieldType.ListBox
						Return New ListBoxFormFieldData(position, controller)
					Case FormFieldType.CheckBox
						Return New CheckBoxFormFieldData(position, controller)
					Case FormFieldType.Signature
						Return New SignatureFormFieldData(position, controller)
					Case FormFieldType.TextBox
						Return New TextBoxFormFieldData(position, controller)
				End Select
			End If
			Return Nothing
		End Function

		Private ReadOnly controller As DocumentFormController
		Private ReadOnly fRectangle As FormFieldRectangle
		Private ReadOnly fAppearance As FormFieldAppearance
		Private fPageNumber As Integer
		Private fName As String
		Private fTextAlignment As PdfAcroFormStringAlignment = PdfAcroFormStringAlignment.Near
		Private fRequired As Boolean
		Private fReadOnly As Boolean
		Private fPrint As Boolean = True
		Private fToolTip As String

		<Category(AppearanceCategory), TypeConverter(GetType(ExpandableObjectConverter))> _
		Public ReadOnly Property Appearance() As FormFieldAppearance
			Get
				Return fAppearance
			End Get
		End Property

		<Category(DesignCategory)> _
		Public Property Name() As String
			Get
				Return fName
			End Get
			Set(ByVal value As String)
				Dim oldName As String = fName
				fName = value
				Try
					UpdateModel()
				Catch
					fName = oldName
					Throw
				End Try
			End Set
		End Property

		<Category(LayoutCategory)> _
		Public Property PageNumber() As Integer
			Get
				Return fPageNumber
			End Get
			Set(ByVal value As Integer)
				If value < 1 OrElse value > controller.PageCount Then
					Throw New ArgumentOutOfRangeException("PageNumber", String.Format("The page number should be in the range from 1 to {0}.", controller.PageCount))
				End If
				fPageNumber = value
				UpdateModel()
			End Set
		End Property

		<Category(LayoutCategory), TypeConverter(GetType(ExpandableObjectConverter))> _
		Public ReadOnly Property Rectangle() As FormFieldRectangle
			Get
				Return fRectangle
			End Get
		End Property

		<Category(AppearanceCategory)> _
		Public Property TextAlignment() As PdfAcroFormStringAlignment
			Get
				Return fTextAlignment
			End Get
			Set(ByVal value As PdfAcroFormStringAlignment)
				fTextAlignment = value
				UpdateModel()
			End Set
		End Property

		<Category(BehaviorCategory)> _
		Public Property Required() As Boolean
			Get
				Return fRequired
			End Get
			Set(ByVal value As Boolean)
				fRequired = value
				UpdateModel()
			End Set
		End Property

		<Category(BehaviorCategory)> _
		Public Property [ReadOnly]() As Boolean
			Get
				Return fReadOnly
			End Get
			Set(ByVal value As Boolean)
				fReadOnly = value
				UpdateModel()
			End Set
		End Property

		<Category(BehaviorCategory)> _
		Public Property Print() As Boolean
			Get
				Return fPrint
			End Get
			Set(ByVal value As Boolean)
				fPrint = value
				UpdateModel()
			End Set
		End Property

		<Category(BehaviorCategory)> _
		Public Property ToolTip() As String
			Get
				Return fToolTip
			End Get
			Set(ByVal value As String)
				fToolTip = value
				UpdateModel()
			End Set
		End Property

		Public Sub New(ByVal position As PdfDocumentPosition, ByVal controller As DocumentFormController)
			Me.controller = controller
			fName = controller.GetNextName()
			fPageNumber = position.PageNumber
			fRectangle = New FormFieldRectangle(position.Point, ExpandToRectangle(position.Point), controller)
			fAppearance = New FormFieldAppearance(controller)
		End Sub
		Protected Overridable Function ExpandToRectangle(ByVal point As PdfPoint) As PdfPoint
			Return New PdfPoint(point.X + 100, point.Y - 50)
		End Function
		Public Function GetClientRectangle(ByVal viewer As PdfViewer) As Rectangle
			Dim innerRectangle As PdfRectangle = Rectangle.InnerRectangle
			Dim startPoint As PointF = viewer.GetClientPoint(New PdfDocumentPosition(PageNumber, innerRectangle.TopLeft))
			Dim endPoint As PointF = viewer.GetClientPoint(New PdfDocumentPosition(PageNumber, innerRectangle.BottomRight))
			Return System.Drawing.Rectangle.Round(RectangleF.FromLTRB(Math.Min(startPoint.X, endPoint.X), Math.Min(startPoint.Y, endPoint.Y), Math.Max(startPoint.X, endPoint.X), Math.Max(startPoint.Y, endPoint.Y)))
		End Function
		Public Function ContainsPosition(ByVal position As PdfDocumentPosition) As Boolean
			If PageNumber <> position.PageNumber Then
				Return False
			End If
			Dim innerRect As PdfRectangle = Rectangle.InnerRectangle
			Dim point As PdfPoint = position.Point
			Return innerRect.Bottom <= point.Y AndAlso innerRect.Top >= point.Y AndAlso innerRect.Left <= point.X AndAlso innerRect.Right >= point.X
		End Function
		Public Function CreateAcroFormField() As PdfAcroFormField
			Dim formField As PdfAcroFormCommonVisualField = CreateVisualFormField()
			formField.TextAlignment = TextAlignment
			formField.Appearance = Appearance.CreateAcroFormFieldAppearance()
			formField.ReadOnly = [ReadOnly]
			formField.Required = Required
			formField.Print = Print
			formField.ToolTip = ToolTip
			Return formField
		End Function
		Public Sub Dispose() Implements IDisposable.Dispose
			If fAppearance IsNot Nothing Then
				fAppearance.Dispose()
			End If
		End Sub
		Protected Sub UpdateModel()
			controller.UpdateDocument()
		End Sub
		Protected MustOverride Function CreateVisualFormField() As PdfAcroFormCommonVisualField
	End Class
