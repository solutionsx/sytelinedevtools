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
        Dim scoperequest As New LoadCollectionRequestData
        scoperequest.IDOName = "SXForms"
        scoperequest.PropertyList.Add("Name")
        scoperequest.Filter = String.Format("ScopeType={0}",CInt(scope))
        
        Dim scoperesponse =_client.LoadCollection(scoperequest)
        Return scoperesponse.Items.Select(Of String)(Function(e) e.PropertyValues(0).Value).ToList 
         
        'Dim request As New LoadGlobalObjectsRequestData 
        'request.GetNameListOnly = True
        'request.ScopeInfo = New ScopeInfo(scope,groupName,username)
        'request.ObjectType = Enums.GlobalObjectType.Form 
        'request.StringTableName = "Strings"
       
        'Dim response = _client.FormsMetadata.LoadGlobalObjects(request)
        
        'return response.NameList 
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

        formnames = formnames.Take(100).ToList 

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
End Class
