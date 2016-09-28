Public Class DataAccess
    Public Shared Function GetDbContext(Server As String, Database As String, UserID As String, Password As String) As System.Data.Linq.DataContext
        Dim strConnection = String.Format("Server={0};Database={1};User Id={2};Password={3}", Server, Database, UserID, Password)
        Dim db As New System.Data.Linq.DataContext(strConnection)

        If Not db.DatabaseExists Then
            Throw New Exception("Invalid Database Connection")
            Return Nothing
        End If
        Return db
    End Function

    Public Shared Function ExecuteQuery(ByVal InDataContext As System.Data.Linq.DataContext, Site As String, ByVal Query As String, ByVal DataTableName As String, ByVal InSPParms As List(Of SPParameter), ByVal ParamArray parms() As Object) As DataTable
        InDataContext.Connection.Open()
        Dim strSetSiteSp As String = String.Empty
        If Site <> "" Then strSetSiteSp = "exec SetSiteSp '" & Site & "', NULL " & vbCrLf
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
End Class
