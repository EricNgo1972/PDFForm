Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports DevExpress.Pdf


Public Class ListBoxFormFieldData
		Inherits ArrayBasedFormFieldData
		Private fMultiSelect As Boolean
		Private fSelectedIndices() As Integer

		<Category(BehaviorCategory)> _
		Public Property MultiSelect() As Boolean
			Get
				Return fMultiSelect
			End Get
			Set(ByVal value As Boolean)
				fMultiSelect = value
				UpdateModel()
			End Set
		End Property

		<Category(AppearanceCategory)> _
		Public Property SelectedIndices() As Integer()
			Get
				Return fSelectedIndices
			End Get
			Set(ByVal value As Integer())
				If Items Is Nothing Then
					Throw New ArgumentException("Selected value cannot be set if collection is empty.")
				End If
				If value IsNot Nothing Then
					Dim length As Integer = Items.Length
					For Each val As Integer In value
						If val < 0 OrElse val >= length Then
							Throw New ArgumentException(String.Format("Indices should be in range from 0 to {0}.", length - 1))
						End If
					Next val
				End If
				fSelectedIndices = value
				UpdateModel()
			End Set
		End Property

		Public Sub New(ByVal position As PdfDocumentPosition, ByVal controller As DocumentFormController)
			MyBase.New(position, controller)
		End Sub
		Protected Overrides Function CreateChoiceField() As PdfAcroFormChoiceField
			Return New PdfAcroFormListBoxField(Name, PageNumber, Rectangle.InnerRectangle) With {.MultiSelect = MultiSelect}
		End Function
		Protected Overrides Sub ClearSelected()
			SelectedIndices = Nothing
		End Sub
		Protected Overrides Sub SelectItems(ByVal choiceField As PdfAcroFormChoiceField)
			If SelectedIndices IsNot Nothing Then
				For Each selectedItem As Integer In SelectedIndices
					choiceField.SetSelected(selectedItem, True)
				Next selectedItem
			End If
		End Sub
	End Class
