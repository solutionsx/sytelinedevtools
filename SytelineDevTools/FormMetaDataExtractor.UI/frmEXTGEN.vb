Imports System.Data.Linq
Imports System.Text

<FormMenuItem(Description:="EXTGEN Scripting")>
Public Class frmEXTGEN
    Dim _StoredProcedures As New List(Of String)
    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        'Validation Connection
        Dim db = GetDbConnection()
        If db Is Nothing Then Exit Sub
        'Load List of SPs
        trSPs.Nodes.Clear()
        Dim oRoot = trSPs.Nodes.Add("Stored Procedures")
        _StoredProcedures = db.ExecuteQuery(Of String)("select name from sys.objects where type = 'P' order by name").ToList
        For Each strName In _StoredProcedures
            oRoot.Nodes.Add(strName)
        Next
        db.Dispose()
        chkList.Checked = True
    End Sub
    Private Function GetDbConnection() As DataContext
        Dim strConnection = String.Format("Server={0};Database={1};User Id={2};Password={3}", txtServerSpec.Text, txtDatabase.Text, txtUser.Text, txtPassword.Text)
        Dim db As New DataContext(strConnection)

        If Not db.DatabaseExists Then
            MessageBox.Show("Invalid Connection", "Connection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return Nothing
        End If
        Return db
    End Function
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
        'Loop through each Input Parameter
        Dim strSQL As String = "SELECT ORDINAL_POSITION, PARAMETER_MODE, PARAMETER_NAME, DATA_TYPE, ISNULL(CHARACTER_MAXIMUM_LENGTH, NUMERIC_PRECISION) AS PRECISION, NUMERIC_SCALE AS SCALE FROM INFORMATION_SCHEMA.PARAMETERS WHERE SPECIFIC_NAME = {0}"

        Dim db = GetDbConnection()
        If db Is Nothing Then Exit Sub
        Dim lParameters As List(Of SPParameter) = db.ExecuteQuery(Of SPParameter)(strSQL, e.Node.Text).ToList
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

        Dim db = GetDbConnection()
        If db Is Nothing Then Exit Sub

        Dim sName = CType(sender, Button).Text.Substring(9)
        Dim oInfobarParameter As SPParameter = Nothing
        Dim sbSQL As New StringBuilder
        sbSQL.Append(String.Format("Create PROCEDURE [dbo].[EXTGEN_{0}] (", sName))
        Dim blnFirstParm As Boolean = True
        Dim lSPParameters = CType(CType(sender, Button).Tag, List(Of SPParameter))
        For Each oParameter In lSPParameters
            sbSQL.Append(vbCrLf & If(blnFirstParm, "", ",") & oParameter.PARAMETER_NAME & " " & oParameter.DATA_TYPE)
            If oParameter.PRECISION IsNot Nothing Then
                If oParameter.PRECISION > 0 Then
                    sbSQL.Append("(" & oParameter.PRECISION.ToString)
                    If oParameter.SCALE IsNot Nothing Then
                        sbSQL.Append(", " & oParameter.SCALE.ToString)
                    End If
                    sbSQL.Append(")")
                ElseIf oParameter.PRECISION = -1 Then
                    sbSQL.Append("(MAX)")
                End If
            End If
            If oParameter.PARAMETER_MODE.ToUpper Like "*OUT" Then
                sbSQL.Append(" OUTPUT")
            End If

            If oParameter.PARAMETER_NAME.ToUpper Like "*INFOBAR*" Then
                oInfobarParameter = oParameter
            End If
            blnFirstParm = False
        Next
        sbSQL.Append(vbCrLf & ") AS " & vbCrLf & "BEGIN")
        If oInfobarParameter IsNot Nothing Then
            sbSQL.Append(vbCrLf & String.Format("If {0} = 'FirstPass'", oInfobarParameter.PARAMETER_NAME))
            sbSQL.Append(vbCrLf & "Return 1")
            sbSQL.Append(vbCrLf)
            sbSQL.Append(vbCrLf & "--Preprocessing Here")
            sbSQL.Append(vbCrLf)
            sbSQL.Append(vbCrLf & "Declare @Severity	int")
            sbSQL.Append(vbCrLf)
            sbSQL.Append(vbCrLf & "Set @Infobar = 'FirstPass'")


            Dim lExecuteParms As New List(Of Object)

            For i = 2 To flpInput.Controls.Count - 1 Step 3
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

            Dim dataTableOutput = ExecuteQuery(db, "Exec " & sName, "OUTPUT", lSPParameters, lExecuteParms.ToArray)
            If dataTableOutput IsNot Nothing Then
                sbSQL.Append(vbCrLf & "declare @ReportSET table (")
                blnFirstParm = True
                For Each oColumn As DataColumn In dataTableOutput.Columns
                    sbSQL.Append(vbCrLf & vbTab)
                    If Not blnFirstParm Then sbSQL.Append(",")
                    sbSQL.Append("[" & oColumn.ColumnName & "]")
                    sbSQL.Append(" ")
                    Select Case oColumn.DataType
                        Case GetType(String)
                            sbSQL.Append("NVARCHAR(MAX)")
                        Case GetType(Integer)
                            sbSQL.Append("INT")
                        Case GetType(Decimal)
                            sbSQL.Append("DECIMAL(18,8)")
                        Case GetType(Byte)
                            sbSQL.Append("TinyInt")
                        Case GetType(Guid)
                            sbSQL.Append("UniqueIdentifier")
                        Case GetType(Date)
                            sbSQL.Append("Date")
                    End Select

                    blnFirstParm = False
                Next
                sbSQL.Append(vbCrLf & ")")
                sbSQL.Append(vbCrLf & " Insert Into @ReportSET")
            End If

            sbSQL.Append(vbCrLf & "EXEC @Severity = " & sName)
            blnFirstParm = True
            For Each oParameter In lSPParameters
                sbSQL.Append(vbCrLf & vbTab)
                If Not blnFirstParm Then sbSQL.Append(",")
                sbSQL.Append(oParameter.PARAMETER_NAME)
                If oParameter.PARAMETER_MODE.ToUpper Like "*OUT" Then
                    sbSQL.Append(" OUTPUT")
                End If
                blnFirstParm = False
            Next

        End If
        sbSQL.Append(vbCrLf)
        sbSQL.Append(vbCrLf & "--Post processing Here")
        sbSQL.Append(vbCrLf)
        sbSQL.Append(vbCrLf & "Return @Severity")
        sbSQL.Append(vbCrLf & "END")
        txtOutput.Text = sbSQL.ToString

    End Sub

    Public Function ExecuteQuery(ByVal InDataContext As System.Data.Linq.DataContext, ByVal Query As String, ByVal DataTableName As String, ByVal InSPParms As List(Of SPParameter), ByVal ParamArray parms() As Object) As DataTable
        InDataContext.Connection.Open()
        Dim strSetSiteSp As String = String.Empty
        If txtSite.Text <> "" Then strSetSiteSp = "exec SetSiteSp '" & txtSite.Text & "', NULL " & vbCrLf
        Dim oCommand As New Data.SqlClient.SqlCommand(strSetSiteSp & Query, CType(InDataContext.Connection, Data.SqlClient.SqlConnection))
        If parms IsNot Nothing Then
            Dim intParmIndex As Integer = 0
            For Each oParm In parms
                oCommand.Parameters.Add(New Data.SqlClient.SqlParameter(InSPParms(intParmIndex).PARAMETER_NAME, oParm))
                intParmIndex += 1
            Next
        End If
        Try
            Dim dt As New DataTable(DataTableName)
            Dim oResult = oCommand.ExecuteReader()
            dt.Load(oResult)
            InDataContext.Connection.Close()
            Return dt
        Catch ex As Exception
            Try
                InDataContext.Connection.Close()
            Catch ex2 As Exception
            End Try
        End Try
        Return Nothing
    End Function

    Private Sub txtOutput_KeyDown(sender As Object, e As KeyEventArgs) Handles txtOutput.KeyDown
        If e.Control And e.KeyCode = Keys.A Then
            txtOutput.SelectAll()
        End If
    End Sub

End Class
Public Class SPParameter
    Public Property ORDINAL_POSITION As Integer
    Public Property PARAMETER_MODE As String
    Public Property PARAMETER_NAME As String
    Public Property DATA_TYPE As String
    Public Property PRECISION As Integer?
    Public Property SCALE As Integer?
End Class