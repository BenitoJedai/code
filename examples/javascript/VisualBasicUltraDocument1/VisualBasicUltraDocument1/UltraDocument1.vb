Imports ScriptCoreLib.JavaScript.DOM.HTML
Imports ScriptCoreLib.JavaScript.Extensions
Imports ScriptCoreLib.Shared.Drawing
Imports System.Net

Public NotInheritable Class UltraDocument1

    Dim ChangeColor As New IHTMLButton
    Dim GetData1 As New IHTMLButton
    Dim x As New IHTMLDiv

    Public Sub New(ByVal e As IHTMLElement)

        x.innerText = "hello world"

        x.AttachToDocument()

        ChangeColor.innerText = "ChangeColor"
        ChangeColor.AttachTo(x)

        GetData1.innerText = "GetData1"
        GetData1.AttachTo(x)

        AddHandler GetData1.onclick, AddressOf GetData1_onclick
        AddHandler ChangeColor.onclick, AddressOf ChangeColor_onclick

    End Sub

    Private Sub ChangeColor_onclick(ByVal e As ScriptCoreLib.JavaScript.DOM.IEvent)
        x.style.color = Color.Red
    End Sub


    Private Sub GetData1_onclick(ByVal e As ScriptCoreLib.JavaScript.DOM.IEvent)
        Dim ws As New MyWebService

        ws.GetData1("http://example.com", AddressOf GotData)


    End Sub

    Public Sub GotData(ByVal data As String)
        Dim xx As New IHTMLCode

        Dim s = "<body>"

        Dim i = data.IndexOf(s)
        Dim j = data.IndexOf("</BODY>")

        Dim z = i + s.Length
        xx.innerHTML = data.Substring(z, j - z)
        xx.AttachTo(x)

    End Sub
End Class

Public NotInheritable Class MyWebService

    Public Delegate Sub StringAction(ByVal e As String)

    Public Sub GetData1(ByVal address As String, ByVal r As StringAction)

        Dim c As New WebClient

        Dim x = c.DownloadString(address:=address)

        r(x)

    End Sub

End Class
