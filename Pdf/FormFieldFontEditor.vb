Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Design
Imports System.Windows.Forms


Public Class FormFieldFontEditor
		Inherits UITypeEditor
		Public Overrides Overloads Function GetEditStyle(ByVal context As ITypeDescriptorContext) As UITypeEditorEditStyle
			Return UITypeEditorEditStyle.Modal
		End Function

		Public Overrides Overloads Function EditValue(ByVal context As ITypeDescriptorContext, ByVal provider As IServiceProvider, ByVal value As Object) As Object
			Using dialog As New FontDialog()
				dialog.ShowEffects = False
				Dim font As Font = TryCast(value, Font)
				If font IsNot Nothing Then
					dialog.Font = font
				End If
				If dialog.ShowDialog() = DialogResult.OK Then
					Return dialog.Font
				End If
				Return MyBase.EditValue(context, provider, value)
			End Using
		End Function
	End Class
