
Public Module Setup
    Public Sub DefineEntryPoint(ByVal e As IEntryPoint)

        DefineControlEntryPoint(e, JavaScript.Class1.ControlAlias)


    End Sub

    Sub DefineControlEntryPoint(ByVal e As IEntryPoint, ByVal ControlAlias As String, Optional ByVal DefaultData As String = "")
        Dim w As New TextWriter

        w.WriteLine("<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">")
        w.WriteLine("<html>")
        w.WriteLine("<head>")

        w.WriteLine("<!-- created at " + System.DateTime.Now.ToString + " -->")

        SharedHelper.DefineScript(w, SharedHelper.LocalModules)

        w.WriteLine("<script></script>")

        w.WriteLine("</head>")
        w.WriteLine("<body>")

        SharedHelper.DefineSpawnPoint(w, ControlAlias, DefaultData)

        w.WriteLine("</body>")
        w.WriteLine("</html>")

        e.Define(ControlAlias + ".htm", w.Text)
    End Sub
End Module

