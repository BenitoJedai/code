Imports ScriptCoreLib.JavaScript.DOM.HTML
Imports ScriptCoreLib.JavaScript.Extensions
Imports ScriptCoreLib.JavaScript

Imports UltraWebApplicationWithAssets.HTML.Pages.FromAssets

Public NotInheritable Class UltraApplication

    Dim a As New HTMLPage1

    Dim WithEvents button1 As New IHTMLButton

    Public Sub New(ByVal e As IHTMLElement)

        Native.Document.title = "UltraWebApplicationWithAssets"

        Me.button1 = a.Button1

        a.Button1.innerText = "Click to show time"
        a.Button2.disabled = True

        a.Container.AttachToDocument()
    End Sub




    Private Sub button1_onclick(ByVal e As ScriptCoreLib.JavaScript.DOM.IEvent) Handles button1.onclick
        a.Content.Add(New IHTMLDiv((DateTime.Now)))
    End Sub
End Class
