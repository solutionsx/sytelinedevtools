Imports Mongoose.FormServer.Protocol
Imports Mongoose.IDO.Protocol
Imports Mongoose.WinStudio
Imports Mongoose.IDO
Imports NUnit.Framework
Imports System.IO
Imports Mongoose.WinStudio.Enums

Public Class FormExtractorTests

    Private Function GetClient As Client
        Dim oclient As New Client("http://172.16.89.163/IDORequestService/RequestService.aspx", IDOProtocol.Http)
        oclient.OpenSession("sa", "", "Demo")
        Return oclient
    End Function

    <Test>
    Public Sub GetFormList_WithSiteScope_ReturnsListOfSiteForms()
        Dim oclient = GetClient
        Try
            Dim extractor As New FormExtractor(oclient)
            Dim formlist = extractor.GetFormList(Enums.ScopeTypes.SITE_DEFAULT, "", "")
            Assert.Greater(formlist.Count, 0)
        Finally
            oclient.CloseSession
        End Try

    End Sub

    <Test>
    Public Sub ExtractFormXMLFor_IndirectLaborCodesFormVendorForm_ReturnsXMLForForm()
        Dim oclient = GetClient
        Try
            Dim extractor As New FormExtractor(oclient)
            Dim vendorfrm = extractor.GetFormByScope("Items", Enums.ScopeTypes.SYMIX_DEFAULT, "", "")
            Dim sitefrm = extractor.GetFormByScope("Items", Enums.ScopeTypes.SITE_DEFAULT, "", "")
            File.WriteAllText(Path.Combine(Path.GetTempPath, vendorfrm.Name + "_Vendor" + ".xml"), vendorfrm.ToFormattedXml)
            File.WriteAllText(Path.Combine(Path.GetTempPath, sitefrm.Name + "_Site" + ".xml"), sitefrm.ToFormattedXml)

            'Assert.IsNotNull(frmxml)
        Finally
            oclient.CloseSession
        End Try

    End Sub

    <Test>
    Public Sub ExtractAllForms_VendorScope_ReturnsListOfXDocumentsForForms()
        Dim oclient = GetClient
        Try
            Dim extractor As New FormExtractor(oclient)
            Dim forms = extractor.ExtractAllFormsForScope(Enums.ScopeTypes.SYMIX_DEFAULT, "", "")
            forms.ForEach(Sub(e) ObjectPersister.PersistObject("e:\SLFormObjects", GlobalObjectType.Form, e.Name, e))
            Assert.Greater(forms.Count, 0)
        Finally
            oclient.CloseSession
        End Try

    End Sub
End Class
