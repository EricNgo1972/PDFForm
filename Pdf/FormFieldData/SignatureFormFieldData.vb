Imports Microsoft.VisualBasic
Imports System.Drawing.Design
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports DevExpress.Pdf
Public Class SignatureFormFieldData
    Inherits FormFieldData
    Private fText As String
    Private fLineAlignment As PdfAcroFormStringAlignment

    <Category(AppearanceCategory), Editor(GetType(MultilineStringEditor), GetType(UITypeEditor))>
    Public Property Text() As String
        Get
            Return fText
        End Get
        Set(ByVal value As String)
            fText = value
            UpdateModel()
        End Set
    End Property

    <Category(AppearanceCategory)>
    Public Property LineAlignment() As PdfAcroFormStringAlignment
        Get
            Return fLineAlignment
        End Get
        Set(ByVal value As PdfAcroFormStringAlignment)
            fLineAlignment = value
            UpdateModel()
        End Set
    End Property

    Public Sub New(ByVal position As PdfDocumentPosition, ByVal controller As DocumentFormController)
        MyBase.New(position, controller)
    End Sub
    Protected Overrides Function CreateVisualFormField() As PdfAcroFormCommonVisualField
        Return New PdfAcroFormSignatureField(Name, PageNumber, Rectangle.InnerRectangle) With {.Text = Text, .LineAlignment = LineAlignment}
    End Function
End Class

