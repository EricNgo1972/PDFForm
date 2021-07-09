Imports Microsoft.VisualBasic
Imports System
Imports System.Globalization
Imports System.ComponentModel
Imports DevExpress.Pdf


Public Class PdfRectangleTypeConverter
		Inherits TypeConverter
		Public Overrides Overloads Function GetProperties(ByVal context As ITypeDescriptorContext, ByVal value As Object, ByVal attributes() As Attribute) As PropertyDescriptorCollection
			If TypeOf value Is PdfRectangle Then
				Dim collection As PropertyDescriptorCollection = TypeDescriptor.GetProperties(value)
				Return New PropertyDescriptorCollection(New PropertyDescriptor() { collection("Left"), collection("Bottom"), collection("Right"), collection("Top") })
			End If
			Return MyBase.GetProperties(context, value, attributes)
		End Function
		Public Overrides Overloads Function GetPropertiesSupported(ByVal context As ITypeDescriptorContext) As Boolean
			Return True
		End Function
		Public Overrides Overloads Function ConvertTo(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As Object, ByVal destinationType As Type) As Object
			Dim rectangle As PdfRectangle = TryCast(value, PdfRectangle)
			If destinationType Is GetType(String) AndAlso rectangle IsNot Nothing Then
				Return String.Format("Left={0}, Bottom={1}, Right={2}, Top={3}", CInt(Fix(rectangle.Left)), CInt(Fix(rectangle.Bottom)), CInt(Fix(rectangle.Right)), CInt(Fix(rectangle.Top)))
			End If
			Return MyBase.ConvertTo(context, culture, value, destinationType)
		End Function
	End Class

