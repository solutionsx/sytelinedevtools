Imports Mongoose.FormServer.Protocol
Imports Mongoose.IDO.Protocol
Imports Mongoose.WinStudio
Imports Mongoose.IDO
Public Class IDOSettings

    Private ReadOnly _client As Client
    Public Sub New(client As Client)
        _client = client
    End Sub
    Public Function LoadIDOSettings() As Boolean
        Dim blnResult As Boolean = True
        blnResult = LoadIDOServer()
        If Not blnResult Then Return blnResult

        Return blnResult
    End Function
    Private Function LoadIDOServer() As Boolean
        Dim blnResult As Boolean = True



        Dim oResp As LoadCollectionResponseData
        Dim sProps As String = "SessionID, Seq, FromAddress, ToAddress, CC, BCC, Subject, Body, MessageTime, Name"
        Dim sFilter As String = "SessionID = '" & Guid.NewGuid.ToString() & "'"
        oResp = _client.LoadCollection("FSTmpEmails", sProps, sFilter, "Seq", 1)

        If oResp.Items.Count > 0 Then

            Dim oDelete As New UpdateCollectionRequestData("FSTmpEmails")
            Dim oItem As IDOItem = oResp.Items(0)
            oItem.PropertyValues("Test").Value = ""
            Dim oUpdItem As New IDOUpdateItem(UpdateAction.Insert, oItem.ItemID)

            oDelete.Items.Add(oUpdItem)
            _client.UpdateCollection(oDelete)
        End If
        Return blnResult
    End Function
End Class
