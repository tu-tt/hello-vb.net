Imports System.Web.Optimization

Public Module BundleConfig
    ' For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
    Public Sub RegisterBundles(ByVal bundles As BundleCollection)
        bundles.Add(New ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"))

        ' Use the development version of Modernizr to develop with and learn from. Then, when you're
        ' ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
        bundles.Add(New ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"))
        bundles.Add(New Bundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.bundle.min.js"))

        bundles.Add(New StyleBundle("~/Content/css").Include("~/Content/bootstrap.css"))
        bundles.Add(New StyleBundle("~/Content/font-awesome").Include("~/Content/font-awesome.min.css"))
    End Sub
End Module