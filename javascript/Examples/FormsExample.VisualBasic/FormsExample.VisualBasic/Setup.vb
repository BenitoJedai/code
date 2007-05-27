Imports System.Linq

Public Module Setup

    Public Drawing As System.Type = GetType(ScriptCoreLib.JavaScript.Drawing.AssemblyReferenceToken)
    public Form  As System.Type = GetType(ScriptCoreLib.JavaScript.Windows.Forms.AssemblyReferenceToken)


    Public Sub DefineEntryPoint(ByVal e As IEntryPoint)

        DefineControlEntryPoint(e, JavaScript.Class1.ControlAlias)
        DefineControlEntryPoint(e, JavaScript.Class1.ControlAlias, True)


    End Sub


    Sub DefineControlEntryPoint(ByVal e As IEntryPoint, ByVal ControlAlias As String, Optional ByVal packed As Boolean = False, Optional ByVal DefaultData As String = "")
        Dim w As New TextWriter

        w.WriteLine("<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">")
        w.WriteLine("<html>")
        w.WriteLine("<head>")
        w.WriteLine("<title>ScriptApplication</title>")

        w.WriteLine("<!-- created at " + System.DateTime.Now.ToString + " -->")

        If packed Then
            SharedHelper.DefineScript(w, (From i In SharedHelper.LocalModules Select i + ".js.packed").ToArray())

        Else
            SharedHelper.DefineScript(w, SharedHelper.LocalModules)
        End If


        w.WriteLine("<script></script>")

        w.WriteLine("</head>")
        w.WriteLine("<body>")

        SharedHelper.DefineSpawnPoint(w, ControlAlias, DefaultData)

        w.WriteLine("</body>")
        w.WriteLine("</html>")

        If packed Then
            e(ControlAlias + ".packed.htm") = w.Text
        Else
            e(ControlAlias + ".htm") = w.Text
        End If

        'e.Define(ControlAlias + ".htm", w.Text)
    End Sub
End Module

