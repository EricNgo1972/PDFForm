Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports DevExpress.Pdf


Public Class ComboBoxFormFieldData
		Inherits ArrayBasedFormFieldData
		Private Class ComboBoxTypeConverter
			Inherits TypeConverter
			Public Overrides Overloads Function GetStandardValues(ByVal context As ITypeDescriptorContext) As TypeConverter.StandardValuesCollection
				Dim formFieldData As ComboBoxFormFieldData = TryCast(context.Instance, ComboBoxFormFieldData)
				If formFieldData IsNot Nothing Then
					Return New StandardValuesCollection(formFieldData.Items)
				End If
				Return MyBase.GetStandardValues(context)
			End Function
			Public Overrides Overloads Function GetStandardValuesSupported(ByVal context As ITypeDescriptorContext) As Boolean
				Return True
			End Function
		End Class

		Private Shared Function Contains(ByVal array() As String, ByVal item As String) As Boolean
			For i As Integer = 0 To array.Length - 1
				If array(i) = item Then
					Return True
				End If
			Next i
			Return False
		End Function

		Private fSelectedItem As String = String.Empty
		Private fEditable As Boolean

		<Category(AppearanceCategory), TypeConverter(GetType(ComboBoxTypeConverter))> _
		Public Property SelectedItem() As String
			Get
				Return fSelectedItem
			End Get
			Set(ByVal value As String)
				CheckItem(Editable, value)
				fSelectedItem = value
				UpdateModel()
			End Set
		End Property

		<Category(BehaviorCategory)> _
		Public Property Editable() As Boolean
			Get
				Return fEditable
			End Get
			Set(ByVal value As Boolean)
				CheckItem(value, fSelectedItem)
				fEditable = value
				UpdateModel()
			End Set
		End Property

		Public Sub New(ByVal position As PdfDocumentPosition, ByVal controller As DocumentFormController)
			MyBase.New(position, controller)
		End Sub
		Private Sub CheckItem(ByVal editable As Boolean, ByVal selectedItem As String)
			If (Not editable) AndAlso (Not String.IsNullOrEmpty(selectedItem)) AndAlso (Items Is Nothing OrElse (Not Contains(Items, selectedItem))) Then
				Throw New ArgumentException("Selected item should be in item list if combo box is marked as non editable.")
			End If
		End Sub
		Protected Overrides Function CreateChoiceField() As PdfAcroFormChoiceField
			Return New PdfAcroFormComboBoxField(Name, PageNumber, Rectangle.InnerRectangle) With {.Editable = Editable}
		End Function
		Protected Overrides Sub SelectItems(ByVal choiceField As PdfAcroFormChoiceField)
			choiceField.SelectValue(SelectedItem)
		End Sub
		Protected Overrides Sub ClearSelected()
			If (Not Editable) Then
				SelectedItem = Nothing
			End If
		End Sub
	End Class

