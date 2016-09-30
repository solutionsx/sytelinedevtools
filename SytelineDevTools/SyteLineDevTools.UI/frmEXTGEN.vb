Imports System.Data.Linq
Imports System.Text
Imports SyteLineDevTools

<FormMenuItem(Description:="EXTGEN/Wrapper Scripting")>
Public Class frmEXTGEN
    Dim _StoredProcedures As New List(Of String)
    Dim _SPFilter As String
    Dim _Builder As ExtGenBuilder
    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        'Validation Connection
        Dim db = DataAccess.GetDbContext(txtServerSpec.Text, txtDatabase.Text, txtUser.Text, txtPassword.Text)

        'Load List of SPs
        _Builder = New ExtGenBuilder(db)

        flpInput.Controls.Clear()
        _StoredProcedures = _Builder.GetStoredProcedures()
        RefreshSPList
        chkList.Checked = True
    End Sub
    Private Sub RefreshSPList()
        trSPs.Nodes.Clear()
        Dim oRoot = trSPs.Nodes.Add("Stored Procedures")
        Dim collSPs As New List(Of String)
        If _SPFilter = "" Then
            collSPs = _StoredProcedures
        Else
            'collSPs = (From sp In _StoredProcedures Where sp.ToUpper Like "*" & _SPFilter.ToUpper & "*")
            For Each sp In _StoredProcedures
                If sp.ToUpper Like "*" & _SPFilter.ToUpper & "*" Then
                    collSPs.Add(sp)
                End If
            Next
        End If
        For Each strName In collSPs
            oRoot.Nodes.Add(strName)
        Next
    End Sub
    Private Sub chkConnect_CheckedChanged(sender As Object, e As EventArgs) Handles chkConnect.CheckedChanged
        SplitContainer1.SplitterDistance = If(chkConnect.Checked, 225, 15)
        If chkConnect.Checked Then
            chkList.Checked = False
            chkInput.Checked = False
        End If
    End Sub

    Private Sub frmEXTGEN_Load(sender As Object, e As EventArgs) Handles Me.Load
        SplitContainer2.SplitterDistance = 15
        SplitContainer3.SplitterDistance = 15
    End Sub

    Private Sub chkList_CheckedChanged(sender As Object, e As EventArgs) Handles chkList.CheckedChanged
        SplitContainer2.SplitterDistance = If(chkList.Checked, 225, 15)
        If chkList.Checked Then
            chkConnect.Checked = False
            chkInput.Checked = False
        End If
    End Sub

    Private Sub chkInput_CheckedChanged(sender As Object, e As EventArgs) Handles chkInput.CheckedChanged
        SplitContainer3.SplitterDistance = If(chkInput.Checked, 225, 15)
        If chkInput.Checked Then
            chkConnect.Checked = False
            chkList.Checked = False
        End If
    End Sub

    Private Sub TreeView1_NodeMouseDoubleClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles trSPs.NodeMouseDoubleClick
        flpInput.Controls.Clear()
        If e.Node.Parent Is Nothing Then
            Exit Sub
        End If
        Dim CheckBoxEXTGEN As New CheckBox
        CheckBoxEXTGEN.Size = New Size(100, 25)
        CheckBoxEXTGEN.Text = "Make EXTGEN"
        CheckBoxEXTGEN.Checked = True
        flpInput.SetFlowBreak(CheckBoxEXTGEN, True)
        flpInput.Controls.Add(CheckBoxEXTGEN)
        Dim lParameters As List(Of SPParameter) = _Builder.GetStoredProcedureParms(e.Node.Text)
        For Each oParameter As SPParameter In lParameters
            Debug.Print(oParameter.PARAMETER_NAME)
            Dim Label1 As New Label
            Label1.Text = oParameter.PARAMETER_NAME
            Label1.AutoSize = True
            Dim TextBox1 As New TextBox
            TextBox1.Tag = oParameter
            TextBox1.Size = New Size(100, 50)
            TextBox1.Enabled = False
            Dim CheckBox1 As New CheckBox
            CheckBox1.Size = New Size(75, 25)
            CheckBox1.Text = "NULL"
            CheckBox1.Tag = TextBox1
            CheckBox1.Checked = True
            AddHandler CheckBox1.CheckedChanged, AddressOf NullableCheckBox_Changed
            flpInput.Controls.Add(Label1)
            flpInput.Controls.Add(TextBox1)
            flpInput.SetFlowBreak(CheckBox1, True)
            flpInput.Controls.Add(CheckBox1)
        Next

        Dim btnGenerate As New Button
        btnGenerate.Name = "btnGenerate"
        btnGenerate.Text = "Generate " & e.Node.Text
        btnGenerate.Tag = lParameters
        AddHandler btnGenerate.Click, AddressOf btnGenerate_Click
        flpInput.Controls.Add(btnGenerate)
        chkInput.Checked = True
    End Sub
    Private Sub NullableCheckBox_Changed(sender As Object, e As EventArgs)
        Dim oCheckBox = CType(sender, CheckBox)
        CType(oCheckBox.Tag, TextBox).Enabled = Not oCheckBox.Checked
    End Sub
    Private Sub btnGenerate_Click(sender As Object, e As EventArgs)

        Dim sName = CType(sender, Button).Text.Substring(9)
        Dim lSPParameters = CType(CType(sender, Button).Tag, List(Of SPParameter))
        Dim lExecuteParms As New List(Of Object)
        Dim CheckBoxEXTGEN = CType(flpInput.Controls(0), CheckBox)
        For i = 3 To flpInput.Controls.Count - 1 Step 3
            If flpInput.Controls(i - 2).GetType Is GetType(Label) Then
                Dim Label1 = CType(flpInput.Controls(i - 2), Label)
                Dim TextBox1 = CType(flpInput.Controls(i - 1), TextBox)
                Dim CheckBox1 = CType(flpInput.Controls(i), CheckBox)
                If CheckBox1.Checked Then
                    lExecuteParms.Add(DBNull.Value)
                Else
                    lExecuteParms.Add(TextBox1.Text)
                End If
            End If
        Next
        txtOutput.Text = _Builder.BuildExtGen(txtSite.Text, sName, CheckBoxEXTGEN.Checked, lSPParameters, lExecuteParms)

    End Sub


    Private Sub txtOutput_KeyDown(sender As Object, e As KeyEventArgs) Handles txtOutput.KeyDown
        If e.Control And e.KeyCode = Keys.A Then
            txtOutput.SelectAll()
        End If
    End Sub

    Private Sub trSPs_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles trSPs.NodeMouseClick
        If e.Button = MouseButtons.Right Then
            SPFilterMenuStrip.Show(PointToScreen(e.Location))
        End If
    End Sub

    Private Sub FilterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FilterToolStripMenuItem.Click
        _SPFilter = InputBox("Filter", "Stored Procedures Filtering")
        RefreshSPList()
    End Sub

    Private Sub ClearFilterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearFilterToolStripMenuItem.Click
        _SPFilter = ""
        RefreshSPList()
    End Sub
End Class