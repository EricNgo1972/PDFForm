Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls


Partial Public Class PasswordForm
		Inherits XtraForm
		Public ReadOnly Property Password() As String
			Get
				Return textEditPassword.Text
			End Get
		End Property

		Public Sub New(ByVal fileName As String)
			InitializeComponent()
			labelDescription.Text = String.Format("File '{0}' is password protected. Please enter the password in the box below.",If(fileName.Length < 20, fileName, fileName.Substring(0, 17) & "..."))
			buttonOK.Enabled = False
		End Sub
		Private Sub tbPassword_EditValueChanging(ByVal sender As Object, ByVal e As ChangingEventArgs) Handles textEditPassword.EditValueChanging
			buttonOK.Enabled = Not String.IsNullOrEmpty(TryCast(e.NewValue, String))
		End Sub
	End Class

