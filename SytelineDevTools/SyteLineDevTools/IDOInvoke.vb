Public Class IDOInvoke
    Public Sub New(InIDOName As String, InMethod As String, InParameters As List(Of String))
        _IDOName = InIDOName
        _Method = InMethod
        _Parameters = InParameters
    End Sub
    Private _IDOName As String
    Public Property IDOName() As String
        Get
            Return _IDOName
        End Get
        Set(ByVal value As String)
            _IDOName = value
        End Set
    End Property
    Private _Method As String
    Public Property Method() As String
        Get
            Return _Method
        End Get
        Set(ByVal value As String)
            _Method = value
        End Set
    End Property
    Private _Parameters As List(Of String)
    Public Property Parameters() As List(Of String)
        Get
            Return _Parameters
        End Get
        Set(ByVal value As List(Of String))
            _Parameters = value
        End Set
    End Property
    Public Sub ExportJson(writer As Newtonsoft.Json.JsonTextWriter, InvokeType As String)
        writer.WritePropertyName(InvokeType)
        writer.WriteStartObject()
        writer.WritePropertyName("IDOName")
        writer.WriteValue(IDOName)
        writer.WritePropertyName("Method")
        writer.WriteValue(Method)
        writer.WritePropertyName("Parameters")
        writer.WriteStartArray()
        For Each p In Parameters
            writer.WriteValue(p)
        Next
        writer.WriteEndArray()
        writer.WriteEndObject()
    End Sub
End Class
