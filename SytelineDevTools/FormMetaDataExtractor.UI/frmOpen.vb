Public Class frmOpen
    Private Sub btnFilter_Click(sender As Object, e As EventArgs) Handles btnFilter.Click
        RefreshList()
    End Sub

    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
        If lbForms.SelectedItem Is Nothing Then
            MessageBox.Show("Select Form to Open", "Open Form", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        OpenSelectedItem()
    End Sub
    Private Sub OpenSelectedItem()
        Dim oFormType = CType(CType(lbForms.SelectedItem, FormItem).Form, Type)
        Dim oForm = oFormType.GetConstructor({}).Invoke({})
        CType(oForm, Form).MdiParent = frmMain
        CType(oForm, Form).Show()
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub frmOpen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshList()
    End Sub
    Private Sub RefreshList()
        lbForms.DataSource = Nothing
        lbForms.DataSource = GetFormMenuItems()
        lbForms.DisplayMember = "Description"
    End Sub
    Private Function GetFormMenuItems() As List(Of FormItem)
        Dim lFormItems As New List(Of FormItem)
        For Each oType In Me.GetType().Assembly.GetTypes
            Dim oInterface As Type = Nothing
            Dim oFormItem As New FormItem
            If oType.BaseType Is GetType(Form) Then
                Try
                    Dim oFormMenuItem As FormMenuItem = Nothing
                    For Each oAttr As Attribute In oType.GetCustomAttributes(True)
                        If oAttr.ToString = "FormMetaDataExtractor.UI.FormMenuItem" Then
                            oFormMenuItem = CType(oAttr, FormMenuItem)
                            Exit For
                        End If
                    Next
                    If oFormMenuItem IsNot Nothing Then
                        oFormItem.Description = oFormMenuItem.Description
                        If txtFilter.Text = "" OrElse oFormItem.Description.ToUpper Like "*" & txtFilter.Text.ToUpper & "*" Then
                            oFormItem.Form = oType
                            lFormItems.Add(oFormItem)
                        End If
                    End If
                Catch
                End Try
            End If
        Next
        Return lFormItems
    End Function


    Private Sub lbForms_DoubleClick(sender As Object, e As EventArgs) Handles lbForms.DoubleClick
        If lbForms.SelectedItem IsNot Nothing Then
            OpenSelectedItem()
        End If
    End Sub


    Private Sub lbForms_KeyPress(sender As Object, e As KeyPressEventArgs) Handles lbForms.KeyPress
        If lbForms.SelectedItem IsNot Nothing Then
            If e.KeyChar = Chr(13) Then
                OpenSelectedItem()
            End If
        End If
    End Sub
End Class
Public Class FormItem

    Private _Description As String
    Public Property Description() As String
        Get
            Return _Description
        End Get
        Set(ByVal value As String)
            _Description = value
        End Set
    End Property
    Private _Form As Type
    Public Property Form() As Type
        Get
            Return _Form
        End Get
        Set(ByVal value As Type)
            _Form = value
        End Set
    End Property
End Class