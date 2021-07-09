Imports Microsoft.VisualBasic
Imports System
Imports System.Globalization
Imports System.ComponentModel
Imports DevExpress.Pdf

Public Class PdfRGBColorTypeConverter
		Inherits ExpandableObjectConverter
		Public Overrides Overloads Function ConvertTo(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As Object, ByVal destinationType As Type) As Object
			Dim color As PdfRGBColor = TryCast(value, PdfRGBColor)
			If destinationType Is GetType(String) AndAlso color IsNot Nothing Then
				Return String.Format(culture, "R={0:0.000}, G={1:0.000}, B={2:0.000}", color.R, color.G, color.B)
			End If
			Return MyBase.ConvertTo(context, culture, value, destinationType)
		End Function
	End Class

