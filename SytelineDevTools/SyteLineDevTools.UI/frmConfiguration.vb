Imports Mongoose.IDO

<SyteLineDevTools.UI.FormMenuItem(Description:="Configuration")>
Public Class frmConfiguration
    Dim _Client As Client
    Private Function GetClient() As Client
        Dim oclient As New Client("http://SL90030/IDORequestService/requestservice.aspx", IDOProtocol.Http)
        oclient.OpenSession("sa", "", "Demo")

        Return oclient
    End Function
    Private Sub frmConfiguration_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _Client = GetClient()
        Dim oScripting As New IDODataScripting(_Client)

        'Dim strXML As String = oScripting.ExportData("IDOCollections", "CollectionName,DevelopmentFlag,ServerName,ReplaceFlag", "CollectionName='SLDevToolsForms'", IDODataScripting.ActionList.Insert, {"CollectionName", "DevelopmentFlag", "ServerName"}.ToList,
        '                                             "IdoCollectionDeleteSp(CollectionName,1,0),IdoCollectionDeleteSp(CollectionName,0,0),REF", "", "", Nothing, New IDOInvoke("IdoCollections", "IdoCollectionCreateSp", {"P(CollectionName)", "P(ServerName)", "P(CollectionDesc)", "P(LockBy)", "P(Extends)", "P(ReplaceFlag)", "P(LabelStringID)", "", ""}.ToList), Nothing)
        Dim strXML As String = String.Empty

        Using stream = oScripting.GetType.Assembly.GetManifestResourceStream("SyteLineDevTools.IDOCollections.txt")
            Using reader = New IO.StreamReader(stream)
                strXML = reader.ReadToEnd
            End Using
        End Using
        oScripting.ImportData(strXML)

        'Tables

        'Properties


        _Client.CloseSession()
    End Sub
End Class