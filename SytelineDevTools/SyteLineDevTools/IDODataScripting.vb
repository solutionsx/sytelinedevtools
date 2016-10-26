Imports Mongoose.FormServer.Protocol
Imports Mongoose.IDO.Protocol
Imports Mongoose.WinStudio
Imports Mongoose.IDO
Public Class IDODataScripting

    Private ReadOnly _client As Client
    Public Sub New(client As Client)
        _client = client
    End Sub
    Public Enum ActionList
        Delete = 1
        Insert = 2
        '3 = Delete then Insert (different than update)
        Update = 4
    End Enum
    Public Function GetIDOs() As List(Of String)
        Return _client.GetIDONames.ToList
    End Function
    Public Function GetIDOProperties(CollectionName As String) As List(Of String)
        Dim response = _client.LoadCollection("IDOProperties", New PropertyList("PropertyName"), String.Format("CollectionName='{0}'", CollectionName.Replace("'", "''")), "", -1)
        Return response.Items.Select(Of String)(Function(e) e.PropertyValues(0).Value).ToList
    End Function
    Public Function ExportData(CollectionName As String, propertyList As String, filter As String, Action As ActionList, keyPropertyList As List(Of String), CustomDelete As String, CustomInsert As String, CustomUpdate As String, DeleteInvoke As IDOInvoke, InsertInvoke As IDOInvoke, UpdateInvoke As IDOInvoke) As String
        Dim response = _client.LoadCollection(CollectionName, New PropertyList(propertyList), filter, "", -1)

        Dim sb As New Text.StringBuilder
        Dim sw As New IO.StringWriter(sb)
        Using writer = New Newtonsoft.Json.JsonTextWriter(sw)
            writer.Formatting = Newtonsoft.Json.Formatting.Indented
            writer.WriteStartObject()

            writer.WritePropertyName("CollectionName")
            writer.WriteValue(CollectionName)

            writer.WritePropertyName("Action")
            writer.WriteValue(CInt(Action))

            writer.WritePropertyName("CustomDelete")
            writer.WriteValue(CustomDelete)

            writer.WritePropertyName("CustomInsert")
            writer.WriteValue(CustomInsert)

            writer.WritePropertyName("CustomUpdate")
            writer.WriteValue(CustomUpdate)

            writer.WritePropertyName("DeleteInvoke")
            writer.WriteStartObject()
            writer.WriteEndObject()

            If DeleteInvoke IsNot Nothing Then
                DeleteInvoke.ExportJson(writer, "DeleteInvoke")
            End If

            If InsertInvoke IsNot Nothing Then
                InsertInvoke.ExportJson(writer, "InsertInvoke")
            End If

            If UpdateInvoke IsNot Nothing Then
                UpdateInvoke.ExportJson(writer, "UpdateInvoke")
            End If

            writer.WritePropertyName("KeyProperties")
            writer.WriteStartArray()
            For Each sKey In keyPropertyList
                writer.WriteValue(sKey)
            Next
            writer.WriteEnd()

            writer.WritePropertyName("Items")
            writer.WriteStartObject()
            For Each oItem In response.Items
                writer.WritePropertyName("Item")
                writer.WriteStartObject()
                For i = 0 To response.PropertyList.Count - 1
                    writer.WritePropertyName(response.PropertyList(i))
                    writer.WriteValue(oItem.PropertyValues(i).ToString)
                Next
                writer.WriteEnd()
            Next
            writer.WriteEnd()

            writer.WriteEndObject()
        End Using
        Return sb.ToString

    End Function
    Public Function ImportData(Text As String) As Boolean

        Dim jObj = Newtonsoft.Json.Linq.JObject.Parse(Text)
        Dim sCollectionName As String = jObj.SelectToken("CollectionName")
        Dim KeyProperties As List(Of String) = jObj.SelectToken("KeyProperties").Children.Select(Of String)(Function(p) p.ToString).ToList
        Dim iAction As Integer = CInt(jObj("Action"))
        Dim requestMethods As New RequestData
        Dim requestDelete As New UpdateCollectionRequestData(sCollectionName)
        Dim requestInsert As New UpdateCollectionRequestData(sCollectionName)
        Dim requestUpdate As New UpdateCollectionRequestData(sCollectionName)

        Dim Items = jObj.SelectToken("Items")
        For Each Item In Items.SelectTokens("Item")
            requestDelete.CustomDelete = jObj.SelectToken("CustomDelete")
            requestInsert.CustomInsert = jObj.SelectToken("CustomInsert")
            requestUpdate.CustomUpdate = jObj.SelectToken("CustomUpdate")


            Dim blnDeleteInvoke As Boolean = False
            Dim strDeleteIDOName As String = String.Empty
            Dim strDeleteMethod As String = String.Empty
            Dim DeleteInvokeParms As New List(Of String)
            Try
                Dim oDeleteInvoke = jObj.SelectToken("DeleteInvoke")
                strDeleteIDOName = oDeleteInvoke.SelectToken("IDOName")
                strDeleteMethod = oDeleteInvoke.SelectToken("Method")
                For Each oParm In oDeleteInvoke.SelectToken("Parameters").Children.Select(Of String)(Function(p) p.ToString).ToList
                    DeleteInvokeParms.Add(oParm)
                Next
                blnDeleteInvoke = True
            Catch
            End Try

            Dim blnInsertInvoke As Boolean = False
            Dim strInsertIDOName As String = String.Empty
            Dim strInsertMethod As String = String.Empty
            Dim InsertInvokeParms As New List(Of String)
            Try
                Dim oInsertInvoke = jObj.SelectToken("InsertInvoke")
                strInsertIDOName = oInsertInvoke.SelectToken("IDOName")
                strInsertMethod = oInsertInvoke.SelectToken("Method")
                For Each oParm In oInsertInvoke.SelectToken("Parameters").Children.Select(Of String)(Function(p) p.ToString).ToList
                    InsertInvokeParms.Add(oParm)
                Next
                blnInsertInvoke = True
            Catch
            End Try


            Dim blnUpdateInvoke As Boolean = False
            Dim strUpdateIDOName As String = String.Empty
            Dim strUpdateMethod As String = String.Empty
            Dim UpdateInvokeParms As New List(Of String)
            Try
                Dim oUpdateInvoke = jObj.SelectToken("UpdateInvoke")
                strUpdateIDOName = oUpdateInvoke.SelectToken("IDOName")
                strUpdateMethod = oUpdateInvoke.SelectToken("Method")
                For Each oParm In oUpdateInvoke.SelectToken("Parameters").Children.Select(Of String)(Function(p) p.ToString).ToList
                    UpdateInvokeParms.Add(oParm)
                Next
                blnUpdateInvoke = True
            Catch
            End Try

            Dim response As LoadCollectionResponseData
            Dim strCurRecordFilter As String = String.Empty
            Dim blnRecordExists As Boolean = False
            Dim strItemID As String = String.Empty
            If iAction And ActionList.Delete Or iAction And ActionList.Update Then
                Dim blnFirst As Boolean = True
                For Each oProp As Newtonsoft.Json.Linq.JProperty In Item.Children
                    If KeyProperties.Contains(oProp.Name) Then
                        strCurRecordFilter &= If(blnFirst, "", " and ") & oProp.Name & "='" & DirectCast(oProp.Value, Newtonsoft.Json.Linq.JValue).Value.ToString.Replace("'", "''") & "'"
                        blnFirst = False
                    End If
                Next
                response = _client.LoadCollection(sCollectionName, New PropertyList(String.Join(",", KeyProperties)), strCurRecordFilter, "", -1)
                If response.Items.Count > 0 Then
                    strItemID = response.Items(0).ItemID
                    blnRecordExists = True
                End If
            End If
            If iAction And ActionList.Delete And blnRecordExists Then
                Dim oDel As New IDOUpdateItem
                oDel.Action = UpdateAction.Delete
                'add Delete item to request
                Dim dPropValues As New Dictionary(Of String, String)
                For Each oProp As Newtonsoft.Json.Linq.JProperty In Item.Children
                    Debug.Print(oProp.ToString)
                    oDel.Properties.Add(oProp.Name, DirectCast(oProp.Value, Newtonsoft.Json.Linq.JValue).Value.ToString)
                    dPropValues.Add(oProp.Name, DirectCast(oProp.Value, Newtonsoft.Json.Linq.JValue).Value.ToString)
                Next
                oDel.ItemNumber = 0
                oDel.ItemID = strItemID
                'oDel.Properties.Add("_ItemId", strItemID)
                If blnDeleteInvoke Then
                    ExecuteIDOInvoke(strDeleteIDOName, strDeleteMethod, DeleteInvokeParms, dPropValues)
                Else
                    requestDelete.Items.Add(oDel)
                End If
            End If
            If iAction And ActionList.Insert Then
                Dim oAdd As New IDOUpdateItem
                oAdd.Action = UpdateAction.Insert
                'Add Insert item to request
                Dim dPropValues As New Dictionary(Of String, String)
                For Each oProp As Newtonsoft.Json.Linq.JProperty In Item.Children.ToList
                    Debug.Print(oProp.ToString)
                    oAdd.Properties.Add(oProp.Name, DirectCast(oProp.Value, Newtonsoft.Json.Linq.JValue).Value.ToString)
                    dPropValues.Add(oProp.Name, DirectCast(oProp.Value, Newtonsoft.Json.Linq.JValue).Value.ToString)
                Next
                oAdd.ItemNumber = 0
                If blnInsertInvoke Then
                    ExecuteIDOInvoke(strInsertIDOName, strInsertMethod, InsertInvokeParms, dPropValues)
                Else
                    requestInsert.Items.Add(oAdd)
                End If
            End If
            If iAction And ActionList.Update And blnRecordExists Then
                Dim oUpd As New IDOUpdateItem
                oUpd.Action = UpdateAction.Update
                'Add Update to item request
                Dim dPropValues As New Dictionary(Of String, String)
                For Each oProp As Newtonsoft.Json.Linq.JProperty In Item.Children
                    Debug.Print(oProp.ToString)
                    oUpd.Properties.Add(oProp.Name, DirectCast(oProp.Value, Newtonsoft.Json.Linq.JValue).Value.ToString)
                    dPropValues.Add(oProp.Name, DirectCast(oProp.Value, Newtonsoft.Json.Linq.JValue).Value.ToString)
                Next
                oUpd.ItemNumber = 0
                oUpd.ItemID = strItemID
                'oUpd.Properties.Add("_ItemId", strItemID)
                If blnUpdateInvoke Then
                    ExecuteIDOInvoke(strUpdateIDOName, strUpdateMethod, UpdateInvokeParms, dPropValues)
                Else
                    requestUpdate.Items.Add(oUpd)
                End If

            End If
        Next

        Try
            _client.UpdateCollection(requestDelete)
        Catch ex As Exception
            Return False
        End Try
        Try
            _client.UpdateCollection(requestInsert)
        Catch ex As Exception
            Return False
        End Try
        Try
            _client.UpdateCollection(requestUpdate)
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Private Sub ExecuteIDOInvoke(strIDOName As String, strMethod As String, InvokeParms As List(Of String), dPropValues As Dictionary(Of String, String))
        Dim oMethodInsert As New Mongoose.IDO.Protocol.InvokeRequestData
        oMethodInsert.IDOName = strIDOName
        oMethodInsert.MethodName = strMethod
        oMethodInsert.Parameters = New InvokeParameterList
        For Each pValue In InvokeParms
            If pValue.ToUpper.StartsWith("P(") And pValue.EndsWith(")") Then
                Try
                    oMethodInsert.Parameters.Add(dPropValues(pValue.Substring(2, pValue.Length - 3)))
                Catch ex As Exception
                    oMethodInsert.Parameters.Add("")
                End Try
            Else
                oMethodInsert.Parameters.Add(pValue)
            End If
        Next
        _client.Invoke(oMethodInsert)
    End Sub
End Class
