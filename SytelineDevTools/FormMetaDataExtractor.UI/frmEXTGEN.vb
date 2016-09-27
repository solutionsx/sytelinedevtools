<FormMenuItem(Description:="EXTGEN Scripting")>
Public Class frmEXTGEN
    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        'Validation Connection

        'Store Connection

        'Load List of SPs
        trSPs.Nodes.Clear()
        Dim oRoot = trSPs.Nodes.Add("Stored Procedures")

        chkList.Checked = True
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
        'Loop through each Input Parameter

        Dim btnGenerate As New Button
        btnGenerate.Name = "btnGenerate"
        btnGenerate.Text = "Generate"
        'AddHandler btnGenerate.Click, AddressOf 
        flpInput.Controls.Add(btnGenerate)
        chkInput.Checked = True
    End Sub
End Class