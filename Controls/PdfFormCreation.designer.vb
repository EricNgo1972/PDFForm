Imports DevExpress
Imports Microsoft.VisualBasic
Imports System.IO

Partial Public Class PdfFormCreation
    ''' <summary> 
    ''' Required designer variable.
    ''' </summary>
    Private components As System.ComponentModel.IContainer = Nothing

    ''' <summary> 
    ''' Clean up any resources being used.
    ''' </summary>
    ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            UnsubscribeFromEvents()
            If pdfViewer IsNot Nothing Then
                pdfViewer.CloseDocument()
            End If
            File.Delete(editingTempFilePath)
            If components IsNot Nothing Then
                components.Dispose()
            End If
            If controller IsNot Nothing Then
                controller.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

#Region "Component Designer generated code"

    ''' <summary> 
    ''' Required method for Designer support - do not modify 
    ''' the contents of this method with the code editor.
    ''' </summary>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PdfFormCreation))
        Dim SuperToolTip1 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipTitleItem1 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
        Dim SuperToolTip2 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipTitleItem2 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
        Me.layoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.propertyGridControl = New DevExpress.XtraVerticalGrid.PropertyGridControl()
        Me.accordionControl = New DevExpress.XtraBars.Navigation.AccordionControl()
        Me.accordionControlFileElement = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.accordionControlOpenElement = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.accordionControlSaveElement = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.accordionControlRemoveElement = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.accordionControlFormFieldsElement = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.accordionControlTextFieldElement = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.accordionControlComboBoxElement = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.accordionControlListBoxElement = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.accordionControlCheckBoxElement = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.accordionControlSignatureElement = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.pdfViewer = New DevExpress.XtraPdfViewer.PdfViewer()
        Me.layoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.layoutControlPdfViewerItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.layoutControlSideMenuGroup = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.layoutControlAccordionItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.layoutControlPropertyGridItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.splitterItem2 = New DevExpress.XtraLayout.SplitterItem()
        Me.splitterItem1 = New DevExpress.XtraLayout.SplitterItem()
        Me.accordionContentContainer1 = New DevExpress.XtraBars.Navigation.AccordionContentContainer()
        Me.accordionControlElement2 = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        CType(Me.layoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.layoutControl1.SuspendLayout()
        CType(Me.propertyGridControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.accordionControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutControlPdfViewerItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutControlSideMenuGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutControlAccordionItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutControlPropertyGridItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.splitterItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.splitterItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'layoutControl1
        '
        Me.layoutControl1.Controls.Add(Me.propertyGridControl)
        Me.layoutControl1.Controls.Add(Me.accordionControl)
        Me.layoutControl1.Controls.Add(Me.pdfViewer)
        Me.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.layoutControl1.Name = "layoutControl1"
        Me.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(1012, 341, 775, 534)
        Me.layoutControl1.Root = Me.layoutControlGroup1
        Me.layoutControl1.Size = New System.Drawing.Size(947, 673)
        Me.layoutControl1.TabIndex = 0
        Me.layoutControl1.Text = "layoutControl1"
        '
        'propertyGridControl
        '
        Me.propertyGridControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.propertyGridControl.Cursor = System.Windows.Forms.Cursors.Default
        Me.propertyGridControl.Location = New System.Drawing.Point(684, 322)
        Me.propertyGridControl.Name = "propertyGridControl"
        Me.propertyGridControl.Size = New System.Drawing.Size(239, 327)
        Me.propertyGridControl.TabIndex = 2
        '
        'accordionControl
        '
        Me.accordionControl.AllowDrop = True
        Me.accordionControl.AllowItemSelection = True
        Me.accordionControl.Elements.AddRange(New DevExpress.XtraBars.Navigation.AccordionControlElement() {Me.accordionControlFileElement, Me.accordionControlFormFieldsElement})
        Me.accordionControl.Location = New System.Drawing.Point(684, 24)
        Me.accordionControl.Name = "accordionControl"
        Me.accordionControl.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Hidden
        Me.accordionControl.Size = New System.Drawing.Size(239, 284)
        Me.accordionControl.StyleController = Me.layoutControl1
        Me.accordionControl.TabIndex = 1
        Me.accordionControl.Text = "accordionControl1"
        '
        'accordionControlFileElement
        '
        Me.accordionControlFileElement.Elements.AddRange(New DevExpress.XtraBars.Navigation.AccordionControlElement() {Me.accordionControlOpenElement, Me.accordionControlSaveElement, Me.accordionControlRemoveElement})
        Me.accordionControlFileElement.Expanded = True
        Me.accordionControlFileElement.Name = "accordionControlFileElement"
        Me.accordionControlFileElement.Text = "File"
        '
        'accordionControlOpenElement
        '
        Me.accordionControlOpenElement.ImageOptions.SvgImage = CType(resources.GetObject("accordionControlOpenElement.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.accordionControlOpenElement.Name = "accordionControlOpenElement"
        Me.accordionControlOpenElement.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
        ToolTipTitleItem1.Text = "Open... (Ctrl+O)"
        SuperToolTip1.Items.Add(ToolTipTitleItem1)
        Me.accordionControlOpenElement.SuperTip = SuperToolTip1
        Me.accordionControlOpenElement.Text = "Open..."
        '
        'accordionControlSaveElement
        '
        Me.accordionControlSaveElement.ImageOptions.SvgImage = CType(resources.GetObject("accordionControlSaveElement.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.accordionControlSaveElement.Name = "accordionControlSaveElement"
        Me.accordionControlSaveElement.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
        ToolTipTitleItem2.Text = "Save as... (Ctrl+S)"
        SuperToolTip2.Items.Add(ToolTipTitleItem2)
        Me.accordionControlSaveElement.SuperTip = SuperToolTip2
        Me.accordionControlSaveElement.Text = "Save As..."
        '
        'accordionControlRemoveElement
        '
        Me.accordionControlRemoveElement.ImageOptions.SvgImage = CType(resources.GetObject("accordionControlRemoveElement.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.accordionControlRemoveElement.Name = "accordionControlRemoveElement"
        Me.accordionControlRemoveElement.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
        Me.accordionControlRemoveElement.Text = "Remove Interactive Form"
        '
        'accordionControlFormFieldsElement
        '
        Me.accordionControlFormFieldsElement.Elements.AddRange(New DevExpress.XtraBars.Navigation.AccordionControlElement() {Me.accordionControlTextFieldElement, Me.accordionControlComboBoxElement, Me.accordionControlListBoxElement, Me.accordionControlCheckBoxElement, Me.accordionControlSignatureElement})
        Me.accordionControlFormFieldsElement.Expanded = True
        Me.accordionControlFormFieldsElement.Name = "accordionControlFormFieldsElement"
        Me.accordionControlFormFieldsElement.Text = "Form Fields"
        '
        'accordionControlTextFieldElement
        '
        Me.accordionControlTextFieldElement.ImageOptions.SvgImage = CType(resources.GetObject("accordionControlTextFieldElement.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.accordionControlTextFieldElement.Name = "accordionControlTextFieldElement"
        Me.accordionControlTextFieldElement.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
        Me.accordionControlTextFieldElement.Text = "TextBox Field"
        '
        'accordionControlComboBoxElement
        '
        Me.accordionControlComboBoxElement.ImageOptions.SvgImage = CType(resources.GetObject("accordionControlComboBoxElement.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.accordionControlComboBoxElement.Name = "accordionControlComboBoxElement"
        Me.accordionControlComboBoxElement.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
        Me.accordionControlComboBoxElement.Text = "ComboBox Field"
        '
        'accordionControlListBoxElement
        '
        Me.accordionControlListBoxElement.ImageOptions.SvgImage = CType(resources.GetObject("accordionControlListBoxElement.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.accordionControlListBoxElement.Name = "accordionControlListBoxElement"
        Me.accordionControlListBoxElement.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
        Me.accordionControlListBoxElement.Text = "ListBox Field"
        '
        'accordionControlCheckBoxElement
        '
        Me.accordionControlCheckBoxElement.ImageOptions.SvgImage = CType(resources.GetObject("accordionControlCheckBoxElement.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.accordionControlCheckBoxElement.Name = "accordionControlCheckBoxElement"
        Me.accordionControlCheckBoxElement.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
        Me.accordionControlCheckBoxElement.Text = "CheckBox Field"
        '
        'accordionControlSignatureElement
        '
        Me.accordionControlSignatureElement.ImageOptions.SvgImage = CType(resources.GetObject("accordionControlSignatureElement.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.accordionControlSignatureElement.Name = "accordionControlSignatureElement"
        Me.accordionControlSignatureElement.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
        Me.accordionControlSignatureElement.Text = "Signature Field"
        '
        'pdfViewer
        '
        Me.pdfViewer.AllowDrop = True
        Me.pdfViewer.CursorMode = DevExpress.XtraPdfViewer.PdfCursorMode.Custom
        Me.pdfViewer.Location = New System.Drawing.Point(12, 12)
        Me.pdfViewer.Name = "pdfViewer"
        Me.pdfViewer.NavigationPanePageVisibility = DevExpress.XtraPdfViewer.PdfNavigationPanePageVisibility.None
        Me.pdfViewer.Size = New System.Drawing.Size(646, 649)
        Me.pdfViewer.TabIndex = 0
        Me.pdfViewer.ZoomMode = DevExpress.XtraPdfViewer.PdfZoomMode.PageLevel
        '
        'layoutControlGroup1
        '
        Me.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.layoutControlGroup1.GroupBordersVisible = False
        Me.layoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.layoutControlPdfViewerItem, Me.layoutControlSideMenuGroup, Me.splitterItem1})
        Me.layoutControlGroup1.Name = "Root"
        Me.layoutControlGroup1.Size = New System.Drawing.Size(947, 673)
        Me.layoutControlGroup1.TextVisible = False
        '
        'layoutControlPdfViewerItem
        '
        Me.layoutControlPdfViewerItem.Control = Me.pdfViewer
        Me.layoutControlPdfViewerItem.Location = New System.Drawing.Point(0, 0)
        Me.layoutControlPdfViewerItem.Name = "layoutControlPdfViewerItem"
        Me.layoutControlPdfViewerItem.Size = New System.Drawing.Size(650, 653)
        Me.layoutControlPdfViewerItem.TextSize = New System.Drawing.Size(0, 0)
        Me.layoutControlPdfViewerItem.TextVisible = False
        '
        'layoutControlSideMenuGroup
        '
        Me.layoutControlSideMenuGroup.CustomizationFormText = "layoutControlSideMenuGroup"
        Me.layoutControlSideMenuGroup.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.layoutControlAccordionItem, Me.layoutControlPropertyGridItem, Me.splitterItem2})
        Me.layoutControlSideMenuGroup.Location = New System.Drawing.Point(660, 0)
        Me.layoutControlSideMenuGroup.Name = "layoutControlSideMenuGroup"
        Me.layoutControlSideMenuGroup.Size = New System.Drawing.Size(267, 653)
        Me.layoutControlSideMenuGroup.TextVisible = False
        '
        'layoutControlAccordionItem
        '
        Me.layoutControlAccordionItem.Control = Me.accordionControl
        Me.layoutControlAccordionItem.Location = New System.Drawing.Point(0, 0)
        Me.layoutControlAccordionItem.MaxSize = New System.Drawing.Size(0, 288)
        Me.layoutControlAccordionItem.MinSize = New System.Drawing.Size(54, 288)
        Me.layoutControlAccordionItem.Name = "layoutControlAccordionItem"
        Me.layoutControlAccordionItem.Size = New System.Drawing.Size(243, 288)
        Me.layoutControlAccordionItem.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.layoutControlAccordionItem.TextSize = New System.Drawing.Size(0, 0)
        Me.layoutControlAccordionItem.TextVisible = False
        '
        'layoutControlPropertyGridItem
        '
        Me.layoutControlPropertyGridItem.Control = Me.propertyGridControl
        Me.layoutControlPropertyGridItem.ControlAlignment = System.Drawing.ContentAlignment.BottomCenter
        Me.layoutControlPropertyGridItem.Location = New System.Drawing.Point(0, 298)
        Me.layoutControlPropertyGridItem.Name = "layoutControlPropertyGridItem"
        Me.layoutControlPropertyGridItem.OptionsTableLayoutItem.RowIndex = 1
        Me.layoutControlPropertyGridItem.Size = New System.Drawing.Size(243, 331)
        Me.layoutControlPropertyGridItem.TextSize = New System.Drawing.Size(0, 0)
        Me.layoutControlPropertyGridItem.TextVisible = False
        '
        'splitterItem2
        '
        Me.splitterItem2.AllowHotTrack = True
        Me.splitterItem2.Location = New System.Drawing.Point(0, 288)
        Me.splitterItem2.Name = "splitterItem2"
        Me.splitterItem2.Size = New System.Drawing.Size(243, 10)
        '
        'splitterItem1
        '
        Me.splitterItem1.AllowHotTrack = True
        Me.splitterItem1.Location = New System.Drawing.Point(650, 0)
        Me.splitterItem1.Name = "splitterItem1"
        Me.splitterItem1.Size = New System.Drawing.Size(10, 653)
        '
        'accordionContentContainer1
        '
        Me.accordionContentContainer1.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.accordionContentContainer1.Appearance.Options.UseBackColor = True
        Me.accordionContentContainer1.Name = "accordionContentContainer1"
        Me.accordionContentContainer1.Size = New System.Drawing.Size(216, 76)
        Me.accordionContentContainer1.TabIndex = 1
        '
        'accordionControlElement2
        '
        Me.accordionControlElement2.ContentContainer = Me.accordionContentContainer1
        Me.accordionControlElement2.Name = "accordionControlElement2"
        Me.accordionControlElement2.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
        Me.accordionControlElement2.Text = "Element2"
        '
        'PdfFormCreation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.layoutControl1)
        Me.Name = "PdfFormCreation"
        Me.Size = New System.Drawing.Size(947, 673)
        CType(Me.layoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.layoutControl1.ResumeLayout(False)
        CType(Me.propertyGridControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.accordionControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutControlPdfViewerItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutControlSideMenuGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutControlAccordionItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutControlPropertyGridItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.splitterItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.splitterItem1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private layoutControl1 As XtraLayout.LayoutControl
    Private accordionControl As XtraBars.Navigation.AccordionControl
    Private accordionControlFileElement As XtraBars.Navigation.AccordionControlElement
    Private pdfViewer As XtraPdfViewer.PdfViewer
    Private layoutControlGroup1 As XtraLayout.LayoutControlGroup
    Private layoutControlPdfViewerItem As XtraLayout.LayoutControlItem
    Private layoutControlAccordionItem As XtraLayout.LayoutControlItem
    Private WithEvents accordionControlOpenElement As XtraBars.Navigation.AccordionControlElement
    Private WithEvents accordionControlSaveElement As XtraBars.Navigation.AccordionControlElement
    Private accordionControlFormFieldsElement As XtraBars.Navigation.AccordionControlElement
    Private accordionControlTextFieldElement As XtraBars.Navigation.AccordionControlElement
    Private accordionContentContainer1 As XtraBars.Navigation.AccordionContentContainer
    Private accordionControlElement2 As XtraBars.Navigation.AccordionControlElement
    Private accordionControlComboBoxElement As XtraBars.Navigation.AccordionControlElement
    Private accordionControlListBoxElement As XtraBars.Navigation.AccordionControlElement
    Private propertyGridControl As XtraVerticalGrid.PropertyGridControl
    Private layoutControlSideMenuGroup As XtraLayout.LayoutControlGroup
    Private layoutControlPropertyGridItem As XtraLayout.LayoutControlItem
    Private WithEvents accordionControlRemoveElement As XtraBars.Navigation.AccordionControlElement
    Private accordionControlCheckBoxElement As XtraBars.Navigation.AccordionControlElement
    Private accordionControlSignatureElement As XtraBars.Navigation.AccordionControlElement
    Private splitterItem1 As XtraLayout.SplitterItem
    Private splitterItem2 As XtraLayout.SplitterItem


End Class

