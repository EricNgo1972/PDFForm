<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DesignPdfForm
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DesignPdfForm))
        Me.PdfFormCreation1 = New SPC.UI.Win.PDF.PdfFormCreation()
        Me.SuspendLayout()
        '
        'PdfFormCreation1
        '
        Me.PdfFormCreation1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PdfFormCreation1.Location = New System.Drawing.Point(0, 0)
        Me.PdfFormCreation1.Name = "PdfFormCreation1"
        Me.PdfFormCreation1.Size = New System.Drawing.Size(1119, 826)
        Me.PdfFormCreation1.TabIndex = 0
        '
        'DesignPdfForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1119, 826)
        Me.Controls.Add(Me.PdfFormCreation1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "DesignPdfForm"
        Me.Text = "Design Pdf AcroForm"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PdfFormCreation1 As PdfFormCreation
End Class
