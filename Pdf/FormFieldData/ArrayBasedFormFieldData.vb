Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports DevExpress.Pdf


Public MustInherit Class ArrayBasedFormFieldData
		Inherits FormFieldData
		Private fItems() As String

		<Category("Data")> _
		Public Property Items() As String()
			Get
				Return fItems
			End Get
			Set(ByVal value As String())
				fItems = value
				ClearSelected()
				UpdateModel()
			End Set
		End Property

		Protected Sub New(ByVal position As PdfDocumentPosition, ByVal controller As DocumentFormController)
			MyBase.New(position, controller)
		End Sub
		Protected Overrides Function CreateVisualFormField() As PdfAcroFormCommonVisualField
			Dim choiceField As PdfAcroFormChoiceField = CreateChoiceField()
			If Items IsNot Nothing Then
				choiceField.ClearValues()
				For Each item As String In Items
					choiceField.AddValue(item)
				Next item
			End If
			SelectItems(choiceField)
			Return choiceField
		End Function
		Protected MustOverride Sub ClearSelected()
		Protected MustOverride Sub SelectItems(ByVal choiceField As PdfAcroFormChoiceField)
		Protected MustOverride Function CreateChoiceField() As PdfAcroFormChoiceField
	End Class

