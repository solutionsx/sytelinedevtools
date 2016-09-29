Imports System.Text

Public Class ExtGenBuilder

    Private ReadOnly _db As System.Data.Linq.DataContext
    Public Sub New(db As System.Data.Linq.DataContext)
        _db = db
    End Sub
    Public Function GetStoredProcedures() As List(Of String)
        Return _db.ExecuteQuery(Of String)("select name from sys.objects where type = 'P' order by name").ToList
    End Function
    Public Function GetStoredProcedureParms(Name As String) As List(Of SPParameter)
        Dim strSQL As String = "SELECT ORDINAL_POSITION, PARAMETER_MODE, PARAMETER_NAME, DATA_TYPE, ISNULL(CHARACTER_MAXIMUM_LENGTH, NUMERIC_PRECISION) AS PRECISION, NUMERIC_SCALE AS SCALE FROM INFORMATION_SCHEMA.PARAMETERS WHERE SPECIFIC_NAME = {0}"
        Return _db.ExecuteQuery(Of SPParameter)(strSQL, Name).ToList
    End Function

    Public Function BuildExtGen(Site As String, Name As String, lSPParameters As List(Of SPParameter), lExecuteParms As List(Of Object)) As String
        Dim oInfobarParameter As SPParameter = Nothing
        Dim sbSQL As New StringBuilder
        sbSQL.Append(String.Format("Create PROCEDURE [dbo].[EXTGEN_{0}] (", Name))
        Dim blnFirstParm As Boolean = True
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

            Dim dataTableOutput = DataAccess.ExecuteQuery(_db, Site, "Exec " & Name, "OUTPUT", lSPParameters, lExecuteParms.ToArray)
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

            sbSQL.Append(vbCrLf & "EXEC @Severity = " & Name)
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
        Return sbSQL.ToString
    End Function
End Class
