Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Drawing
Imports DevExpress.Pdf
Imports DevExpress.XtraPdfViewer
Imports DevExpress.XtraEditors
Imports DevExpress.XtraBars.Navigation
Imports DevExpress.XtraVerticalGrid.Events


Partial Public Class PdfFormCreation
    Inherits XtraUserControl

    Private Shared ReadOnly demoDocumentPath As String = ""
    Private Shared ReadOnly editingTempFilePath As String = Path.GetTempFileName()

    Private controller As DocumentFormController
    Private selectedItem As FormFieldData
    Private dragManager As FormFieldDragItem
    Private currentCursor As Cursor = Nothing
    Private creatingFieldType As String
    Private accordionSizeInitialized As Boolean

    Public ReadOnly Property NoGap() As Boolean
        Get
            Return True
        End Get
    End Property
    Private ReadOnly Property CursorIsOverPage() As Boolean
        Get
            If pdfViewer.IsDocumentOpened Then
                Return pdfViewer.GetDocumentPosition(pdfViewer.PointToClient(MousePosition), False) IsNot Nothing
            Else
                Return False
            End If
        End Get
    End Property

    Public Sub New()
        InitializeComponent()
        Try

            'Dim demoSource As String = Path.GetTempFileName()
            'Using processor As New PdfDocumentProcessor()
            '    processor.LoadDocument(demoDocumentPath)
            '    processor.ResetFormData()
            '    processor.FlattenForm()
            '    processor.SaveDocument(demoSource)
            'End Using

            'controller = New DocumentFormController(pdfViewer, "", editingTempFilePath)

            'controller.UpdateDocument()

            controller = New DocumentFormController(pdfViewer)

            AddHandler accordionControl.MouseDown, AddressOf AccordionControlMouseDown
            AddHandler accordionControl.KeyDown, AddressOf ProcessCommonKeys
            AddHandler accordionControl.DragEnter, AddressOf AccordionControlDragEnter
            AddHandler accordionControl.QueryContinueDrag, AddressOf AccordionControlQueryContinueDrag
            AddHandler accordionControl.SizeChanged, AddressOf AccordionControlSizeChanged
            AddHandler pdfViewer.Paint, AddressOf PdfViewerPaint
            AddHandler pdfViewer.KeyDown, AddressOf ProcessCommonKeys
            AddHandler pdfViewer.KeyDown, AddressOf PdfViewerKeyDown
            AddHandler pdfViewer.MouseDown, AddressOf PdfViewerMouseDown
            AddHandler pdfViewer.MouseMove, AddressOf PdfViewerMouseMove
            AddHandler pdfViewer.MouseUp, AddressOf PdfViewerMouseUp
            AddHandler pdfViewer.DragEnter, AddressOf PdfViewerDragEnter
            AddHandler pdfViewer.DragOver, AddressOf PdfViewerDragOver
            AddHandler pdfViewer.DragDrop, AddressOf PdfViewerDragDrop


            AddHandler propertyGridControl.CellValueChanged, AddressOf PropertyGridControlValueChanged
            AddHandler propertyGridControl.KeyDown, AddressOf ProcessCommonKeys




        Catch ex As Exception
            Dim msg = ex.Message
        End Try
    End Sub

    Private Sub PdfFormCreation_Load(sender As Object, e As EventArgs) Handles Me.Load
        AuthorizeButtons()
    End Sub

    Private Sub SelectField(ByVal item As FormFieldData)
        If selectedItem IsNot item Then
            selectedItem = item
            dragManager = If(item Is Nothing, Nothing, New FormFieldDragItem(pdfViewer, item, controller))
            propertyGridControl.SelectedObject = Nothing
            propertyGridControl.SelectedObject = item
            pdfViewer.Focus()
        End If
    End Sub
    Private Sub CreateField(ByVal fieldType As String, ByVal location As Point)
        Dim field As FormFieldData = FormFieldData.Create(fieldType, pdfViewer.GetDocumentPosition(location, False), controller)
        If field IsNot Nothing Then
            controller.AddField(field)
            SelectField(field)
        End If
    End Sub
    Private Sub EndFieldCreation()
        creatingFieldType = Nothing
        currentCursor = Nothing
        pdfViewer.Cursor = Cursors.Default
        accordionControl.SelectedElement = Nothing
    End Sub
    Private Sub PropertyGridControlValueChanged(ByVal sender As Object, ByVal e As CellValueChangedEventArgs)
        dragManager.Update()
    End Sub
    Private Sub PdfViewerMouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
        If e.Button = MouseButtons.Left AndAlso String.IsNullOrEmpty(creatingFieldType) AndAlso (dragManager Is Nothing OrElse (Not dragManager.StartDrag(e.Location))) Then
            SelectField(controller.GetFormFieldFromPoint(e.Location))
            pdfViewer.Invalidate()
        End If
    End Sub
    Private Sub PdfViewerMouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
        If dragManager IsNot Nothing AndAlso currentCursor Is Nothing Then
            dragManager.UpdateCursor(e.Location)
        Else
            pdfViewer.Cursor = If(currentCursor IsNot Nothing AndAlso CursorIsOverPage, currentCursor, Cursors.Default)
        End If
        If e.Button = MouseButtons.Left AndAlso dragManager IsNot Nothing AndAlso String.IsNullOrEmpty(creatingFieldType) Then
            dragManager.ContinueDrag(e.Location)
        End If
    End Sub
    Private Sub PdfViewerMouseUp(ByVal sender As Object, ByVal e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            If CursorIsOverPage AndAlso (Not String.IsNullOrEmpty(creatingFieldType)) Then
                CreateField(creatingFieldType, e.Location)
                EndFieldCreation()
            ElseIf dragManager IsNot Nothing Then
                dragManager.EndDrag(e.Location)
                propertyGridControl.UpdateData()
            End If
        End If
    End Sub
    Private Sub PdfViewerDragDrop(ByVal sender As Object, ByVal e As DragEventArgs)
        CreateField(TryCast(e.Data.GetData(DataFormats.Text), String), pdfViewer.PointToClient(New Point(e.X, e.Y)))
    End Sub
    Private Sub PdfViewerDragOver(ByVal sender As Object, ByVal e As DragEventArgs)
        e.Effect = If(FormFieldType.IsValidType(TryCast(e.Data.GetData(DataFormats.Text), String)) AndAlso pdfViewer.GetDocumentPosition(pdfViewer.PointToClient(New Point(e.X, e.Y)), False) Is Nothing, DragDropEffects.None, DragDropEffects.All)
    End Sub
    Private Sub PdfViewerDragEnter(ByVal sender As Object, ByVal e As DragEventArgs)
        PdfViewerDragOver(sender, e)
    End Sub
    Private Sub PdfViewerKeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        Select Case e.KeyCode
            Case Keys.Escape
                EndFieldCreation()
            Case Keys.Delete
                If selectedItem IsNot Nothing Then
                    selectedItem.Dispose()
                    controller.RemoveField(selectedItem)
                    SelectField(Nothing)
                End If
            Case Keys.PageDown
                pdfViewer.CurrentPageNumber = Math.Min(pdfViewer.CurrentPageNumber + 1, pdfViewer.PageCount)
            Case Keys.PageUp
                pdfViewer.CurrentPageNumber = Math.Max(pdfViewer.CurrentPageNumber - 1, 1)
        End Select
        If selectedItem IsNot Nothing Then
            Select Case e.KeyCode
                Case Keys.Left
                    selectedItem.Rectangle.Move(-1, 0)
                Case Keys.Right
                    selectedItem.Rectangle.Move(1, 0)
                Case Keys.Up
                    selectedItem.Rectangle.Move(0, 1)
                Case Keys.Down
                    selectedItem.Rectangle.Move(0, -1)
            End Select
            dragManager.Update()
        End If
    End Sub
    Private Sub PdfViewerPaint(ByVal sender As Object, ByVal e As PaintEventArgs)
        controller.Draw(e.Graphics)
        If selectedItem IsNot Nothing Then
            controller.DrawSelectedItem(e.Graphics, selectedItem.GetClientRectangle(pdfViewer))
        End If
        If dragManager IsNot Nothing Then
            dragManager.DrawDragPoints(e.Graphics, SystemBrushes.Highlight)
        End If
    End Sub
    Private Sub AccordionControlMouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
        Dim hitInfo As AccordionControlHitInfo = accordionControl.CalcHitInfo(e.Location)
        Dim info As AccordionElementBaseViewInfo = hitInfo.ItemInfo
        If hitInfo.HitTest.HasFlag(AccordionControlHitTest.Item) AndAlso info IsNot Nothing AndAlso info.Element IsNot Nothing Then
            Dim name As String = info.Element.Name
            creatingFieldType = Nothing
            If name = accordionControlTextFieldElement.Name Then
                creatingFieldType = FormFieldType.TextBox
            ElseIf name = accordionControlComboBoxElement.Name Then
                creatingFieldType = FormFieldType.ComboBox
            ElseIf name = accordionControlCheckBoxElement.Name Then
                creatingFieldType = FormFieldType.CheckBox
            ElseIf name = accordionControlListBoxElement.Name Then
                creatingFieldType = FormFieldType.ListBox
            ElseIf name = accordionControlSignatureElement.Name Then
                creatingFieldType = FormFieldType.Signature
            End If
            If String.IsNullOrEmpty(creatingFieldType) Then
                EndFieldCreation()
            Else
                accordionControl.DoDragDrop(creatingFieldType, DragDropEffects.All)
            End If
        End If
    End Sub
    Private Sub AccordionControlDragEnter(ByVal sender As Object, ByVal e As DragEventArgs)
        e.Effect = DragDropEffects.None
    End Sub
    Private Sub AccordionControlQueryContinueDrag(ByVal sender As Object, ByVal e As QueryContinueDragEventArgs)
        If e.Action = DragAction.Drop Then
            If (Not CursorIsOverPage) Then
                e.Action = DragAction.Cancel
            Else
                EndFieldCreation()
            End If
        End If
        If e.Action = DragAction.Cancel Then
            currentCursor = Cursors.Cross
        End If
    End Sub
    Private Sub AccordionControlOpenElementClick(ByVal sender As Object, ByVal e As EventArgs) Handles accordionControlOpenElement.Click
        Using processor As New PdfDocumentProcessor()
            Using fileHelper As New PdfDocumentProcessorAndViewerFileHelper(processor, pdfViewer)
                If fileHelper.LoadDocumentWithDialog() Then
                    Try

                        AuthorizeButtons()
                        Dim tempSourceFilePath As String = Path.GetTempFileName()
                        processor.SaveDocument(tempSourceFilePath)
                        controller = New DocumentFormController(pdfViewer, tempSourceFilePath, editingTempFilePath)
                        controller.UpdateDocument()
                        SelectField(Nothing)
                        pdfViewer.ZoomMode = PdfZoomMode.PageLevel
                    Catch
                        XtraMessageBox.Show("An error occurred while processing the PDF document", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End Try
                End If
            End Using
        End Using
    End Sub

    Private Sub AuthorizeButtons()
        accordionControlSaveElement.Enabled = pdfViewer.IsDocumentOpened

        accordionControlTextFieldElement.Enabled = pdfViewer.IsDocumentOpened
        accordionControlCheckBoxElement.Enabled = pdfViewer.IsDocumentOpened
        accordionControlComboBoxElement.Enabled = pdfViewer.IsDocumentOpened
        accordionControlFileElement.Enabled = pdfViewer.IsDocumentOpened
        accordionControlFormFieldsElement.Enabled = pdfViewer.IsDocumentOpened
        accordionControlListBoxElement.Enabled = pdfViewer.IsDocumentOpened
        accordionControlRemoveElement.Enabled = pdfViewer.IsDocumentOpened
        accordionControlSignatureElement.Enabled = pdfViewer.IsDocumentOpened

    End Sub

    Private Sub AccordionControlSaveElementClick(ByVal sender As Object, ByVal e As EventArgs) Handles accordionControlSaveElement.Click
        If New PdfViewerFileHelper(pdfViewer).SaveDocument() Then
            pdfViewer.LoadDocument(editingTempFilePath)
        End If
    End Sub
    Private Sub AccordionControlRemoveElementClick(ByVal sender As Object, ByVal e As EventArgs) Handles accordionControlRemoveElement.Click
        controller.RemoveExistingForm()
    End Sub
    Private Sub AccordionControlSizeChanged(ByVal sender As Object, ByVal e As EventArgs)
        If (Not accordionSizeInitialized) Then
            layoutControlAccordionItem.Size = layoutControlAccordionItem.MinSize
            accordionSizeInitialized = True
        End If
    End Sub
    Private Sub ProcessCommonKeys(ByVal sender As Object, ByVal e As KeyEventArgs)
        If e.Control Then
            Select Case e.KeyCode
                Case Keys.S
                    AccordionControlSaveElementClick(Me, EventArgs.Empty)
                Case Keys.O
                    AccordionControlOpenElementClick(Me, EventArgs.Empty)
            End Select
        End If
    End Sub
    Private Sub UnsubscribeFromEvents()
        If pdfViewer IsNot Nothing Then
            RemoveHandler pdfViewer.Paint, AddressOf PdfViewerPaint
            RemoveHandler pdfViewer.KeyDown, AddressOf PdfViewerKeyDown
            RemoveHandler pdfViewer.KeyDown, AddressOf ProcessCommonKeys
            RemoveHandler pdfViewer.MouseDown, AddressOf PdfViewerMouseDown
            RemoveHandler pdfViewer.MouseMove, AddressOf PdfViewerMouseMove
            RemoveHandler pdfViewer.MouseUp, AddressOf PdfViewerMouseUp
            RemoveHandler pdfViewer.DragEnter, AddressOf PdfViewerDragEnter
            RemoveHandler pdfViewer.DragDrop, AddressOf PdfViewerDragDrop
            RemoveHandler pdfViewer.DragOver, AddressOf PdfViewerDragOver
        End If
        If accordionControl IsNot Nothing Then
            RemoveHandler accordionControl.MouseDown, AddressOf AccordionControlMouseDown
            RemoveHandler accordionControl.KeyDown, AddressOf ProcessCommonKeys
            RemoveHandler accordionControl.DragEnter, AddressOf AccordionControlDragEnter
            RemoveHandler accordionControl.QueryContinueDrag, AddressOf AccordionControlQueryContinueDrag
            RemoveHandler accordionControl.SizeChanged, AddressOf AccordionControlSizeChanged
        End If
        If propertyGridControl IsNot Nothing Then
            RemoveHandler propertyGridControl.CellValueChanged, AddressOf PropertyGridControlValueChanged
            RemoveHandler propertyGridControl.KeyDown, AddressOf ProcessCommonKeys
        End If
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean
        If keyData = Keys.Escape Then
            EndFieldCreation()
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function


End Class

