Imports DevExpress
Imports Microsoft.VisualBasic
Imports System

Partial Public Class PasswordForm
    ''' <summary>
    ''' Required designer variable.
    ''' </summary>
    Private components As System.ComponentModel.IContainer = Nothing

    ''' <summary>
    ''' Clean up any resources being used.
    ''' </summary>
    ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso (components IsNot Nothing) Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

#Region "Windows Form Designer generated code"

    ''' <summary>
    ''' Required method for Designer support - do not modify
    ''' the contents of this method with the code editor.
    ''' </summary>
    Private Sub InitializeComponent()
        Me.textEditPassword = New DevExpress.XtraEditors.TextEdit()
        Me.labelDescription = New DevExpress.XtraEditors.LabelControl()
        Me.butttonCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.labePassword = New DevExpress.XtraEditors.LabelControl()
        Me.buttonOK = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.textEditPassword.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        ' 
        ' textEditPassword
        ' 
        Me.textEditPassword.Location = New System.Drawing.Point(68, 48)
        Me.textEditPassword.Name = "textEditPassword"
        Me.textEditPassword.Properties.PasswordChar = "*"c
        Me.textEditPassword.Size = New System.Drawing.Size(236, 20)
        Me.textEditPassword.TabIndex = 0
        '			Me.textEditPassword.EditValueChanging += New DevExpress.XtraEditors.Controls.ChangingEventHandler(Me.tbPassword_EditValueChanging);
        ' 
        ' labelDescription
        ' 
        Me.labelDescription.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.labelDescription.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.labelDescription.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.labelDescription.Location = New System.Drawing.Point(12, 12)
        Me.labelDescription.Name = "labelDescription"
        Me.labelDescription.Size = New System.Drawing.Size(292, 36)
        Me.labelDescription.TabIndex = 1
        Me.labelDescription.Text = "File '' is password protected. Please enter the password in the box below."
        ' 
        ' butttonCancel
        ' 
        Me.butttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.butttonCancel.Location = New System.Drawing.Point(310, 45)
        Me.butttonCancel.Name = "butttonCancel"
        Me.butttonCancel.Size = New System.Drawing.Size(75, 23)
        Me.butttonCancel.TabIndex = 2
        Me.butttonCancel.Text = "Cancel"
        ' 
        ' labePassword
        ' 
        Me.labePassword.Location = New System.Drawing.Point(12, 51)
        Me.labePassword.Name = "labePassword"
        Me.labePassword.Size = New System.Drawing.Size(50, 13)
        Me.labePassword.TabIndex = 3
        Me.labePassword.Text = "Password:"
        ' 
        ' buttonOK
        ' 
        Me.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.buttonOK.Location = New System.Drawing.Point(310, 12)
        Me.buttonOK.Name = "buttonOK"
        Me.buttonOK.Size = New System.Drawing.Size(75, 23)
        Me.buttonOK.TabIndex = 2
        Me.buttonOK.Text = "OK"
        ' 
        ' PasswordForm
        ' 
        Me.AcceptButton = Me.buttonOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.butttonCancel
        Me.ClientSize = New System.Drawing.Size(391, 81)
        Me.Controls.Add(Me.labePassword)
        Me.Controls.Add(Me.buttonOK)
        Me.Controls.Add(Me.butttonCancel)
        Me.Controls.Add(Me.labelDescription)
        Me.Controls.Add(Me.textEditPassword)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "PasswordForm"
        Me.ShowIcon = False
        Me.Text = "Password needed"
        CType(Me.textEditPassword.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private WithEvents textEditPassword As XtraEditors.TextEdit
    Private labelDescription As XtraEditors.LabelControl
    Private butttonCancel As XtraEditors.SimpleButton
    Private labePassword As XtraEditors.LabelControl
    Private buttonOK As XtraEditors.SimpleButton
End Class

