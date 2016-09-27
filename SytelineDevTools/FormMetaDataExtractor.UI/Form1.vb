Imports FormMetaDataExtractor.UI
Imports Mongoose.IDO
Imports Mongoose.WinStudio
Imports Mongoose.WinStudio.Enums
Imports SyteLineDevTools
<FormMenuItem(Description:="Form Scripting")>
Public Class Form1
    Private Function GetClient() As Client
        Dim oclient As New Client(txtServerSpec.Text, IDOProtocol.Http)
        oclient.OpenSession(txtUser.Text, txtPassword.Text, txtConfig.Text)

        Return oclient
    End Function
    Private Sub btnVendor_Click(sender As Object, e As EventArgs) Handles btnVendor.Click
        ExtractForScope(Enums.ScopeTypes.SYMIX_DEFAULT)
    End Sub
    Private Sub btnNonVendor_Click(sender As Object, e As EventArgs) Handles btnNonVendor.Click
        ExtractForScope(Enums.ScopeTypes.SITE_DEFAULT)
    End Sub

    Private Sub ExtractForScope(scope As Enums.ScopeTypes)
        Dim oclient = GetClient()

        Try
            Dim extractor As New FormExtractor(oclient)
            Dim forms = extractor.ExtractAllFormsForScope(scope, "", "")
            forms.ForEach(Sub(f) ObjectPersister.PersistObject(txtOutputPath.Text, GlobalObjectType.Form, f.Name, f))

        Finally
            oclient.CloseSession()
        End Try
    End Sub


End Class
