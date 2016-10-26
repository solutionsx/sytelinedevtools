Imports Mongoose.FormServer.Protocol
Imports Mongoose.IDO.Protocol
Imports Mongoose.WinStudio
Imports Mongoose.IDO

Public Class FormExtractor

    Private ReadOnly _client As Client
    Public Sub New(client As Client)
        _client = client
    End Sub

    Public Function GetFormList(scope as Enums.ScopeTypes,groupname As String,username As string) As List(Of String)

        Dim scoperesponse =_client.LoadCollection("SLDevToolsForms","Name",String.Format("ScopeType={0}",CInt(scope)),"",0)
        Return scoperesponse.Items.Select(Of String)(Function(e) e.PropertyValues(0).Value).ToList 
         
    End Function

    Public Function GetFormByScope(formname As String,scope as Enums.ScopeTypes,groupname As String,username As string) As FormDef  
        Dim lfrd As New LoadFormRequestData
        lfrd.FormName = formname
        lfrd.Options = FormLoadOptions.AllDependencies 
        lfrd.ScopeInfo = New ScopeInfo(scope,groupName,username)
        Dim response = _client.FormsMetadata.LoadForm(lfrd)
       
        return response.Form
    End Function

    Public Function ExtractAllFormsForScope(scope as Enums.ScopeTypes, groupname As String, username As string) As List(Of FormDef)
        
        Dim formnames = GetFormList(scope, groupname, username)
        Dim formlist As New List(Of FormDef)
        Dim request As New IDORequestEnvelope

        #If DEBUG
            formnames = formnames.Take(200).ToList 
        #End If
        

        For each frmname In formnames
            request.Requests.Add(RequestType.LoadForm, GetLoadFormRequest(frmname, scope, groupname, username))
        Next

        Dim response = _client.GetResponse(request)

        For each item In response.Responses
            If item.RequestType = RequestType.LoadForm Then
                Try
                    Dim lfrd = DirectCast(item.GetResponsePayload, LoadFormResponseData)
                    Dim lform = lfrd.Form
                    If lform Is Nothing Then Continue For 

                    If lform.ScopeInfo.ScopeType = scope Then
                        formlist.Add(lform)
                    End If
                Catch ex As Exception

                End Try
            End If

        Next

        Return formlist
    End Function
    Private Function GetLoadFormRequest(formname As String,scope as Enums.ScopeTypes,groupname As String,username As string) As LoadFormRequestData 
        Dim lfrd As New LoadFormRequestData
        lfrd.FormName = formname
        lfrd.Options = FormLoadOptions.AllDependencies   
        lfrd.ScopeInfo = New ScopeInfo(scope,groupname,username)
        Return lfrd
    End Function

    Public Sub UploadFormToSyteline(formxml As string)
        Dim sfrd As New SaveFormRequestData 
        sfrd.TheForm = FormDef.FromXml(formxml)
        sfrd.ScopeInfo =New ScopeInfo(Enums.ScopeTypes.SITE_DEFAULT,"","")
        Dim request As New IDORequestEnvelope
        request.Requests.Add(RequestType.SaveForm,sfrd)
        Dim response = _client.GetResponse(request)

        
    End Sub

End Class
