Public Class frmOpen
    Private Sub btnFilter_Click(sender As Object, e As EventArgs) Handles btnFilter.Click
        RefreshList()
    End Sub

    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
        If lbForms.SelectedItem Is Nothing Then
            MessageBox.Show("Select Form to Open", "Open Form", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        FormItem.OpenItem(lbForms.SelectedItem)
        Me.DialogResult = DialogResult.OK
    End Sub
    Private Sub frmOpen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshList()
    End Sub
    Private Sub RefreshList()
        lbForms.DataSource = Nothing
        lbForms.DataSource = FormItem.GetFormMenuItems(txtFilter.Text)
        lbForms.DisplayMember = "Description"
    End Sub


    Private Sub lbForms_DoubleClick(sender As Object, e As EventArgs) Handles lbForms.DoubleClick
        If lbForms.SelectedItem IsNot Nothing Then
            FormItem.OpenItem(lbForms.SelectedItem)
            Me.DialogResult = DialogResult.OK
        End If
    End Sub


    Private Sub lbForms_KeyPress(sender As Object, e As KeyPressEventArgs) Handles lbForms.KeyPress
        If lbForms.SelectedItem IsNot Nothing Then
            If e.KeyChar = Chr(13) Then
                FormItem.OpenItem(lbForms.SelectedItem)
                Me.DialogResult = DialogResult.OK
            End If
        End If
    End Sub
End Class
''' <summary>
''' Class For Display in List Box
''' Used to Get List of Forms
''' Used to open instance of form
''' </summary>
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

    Public Shared Function GetFormMenuItems(InFilter As String) As List(Of FormItem)
        Dim lFormItems As New List(Of FormItem)
        For Each oType In System.Reflection.Assembly.GetExecutingAssembly.GetTypes
            Dim oInterface As Type = Nothing
            Dim oFormItem As New FormItem
            If oType.BaseType Is GetType(Form) Then
                Try
                    Dim oFormMenuItem As FormMenuItem = Nothing
                    For Each oAttr As Attribute In oType.GetCustomAttributes(True)
                        If oAttr.ToString = "SyteLineDevTools.UI.FormMenuItem" Then
                            oFormMenuItem = CType(oAttr, FormMenuItem)
                            Exit For
                        End If
                    Next
                    If oFormMenuItem IsNot Nothing Then
                        oFormItem.Description = oFormMenuItem.Description
                        If InFilter = "" OrElse oFormItem.Description.ToUpper Like "*" & InFilter.ToUpper & "*" Then
                            oFormItem.Form = oType
                            lFormItems.Add(oFormItem)
                        End If
                    End If
                Catch
                End Try
            End If
        Next
        Return lFormItems.OrderBy(Function(i) i.Description).ToList
    End Function

    Public Shared Sub OpenItem(InItem As FormItem)
        Dim oFormType = CType(InItem.Form, Type)
        Dim oForm = oFormType.GetConstructor({}).Invoke({})
        CType(oForm, Form).ShowIcon = False
        CType(oForm, Form).MdiParent = frmMain
        CType(oForm, Form).Show()
    End Sub

End Class