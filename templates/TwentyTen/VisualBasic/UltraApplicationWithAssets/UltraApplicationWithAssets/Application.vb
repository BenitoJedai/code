Imports ScriptCoreLib.JavaScript.DOM.HTML
Imports ScriptCoreLib.JavaScript.DOM
Imports ScriptCoreLib.JavaScript
Imports ScriptCoreLib.JavaScript.Extensions

Imports UltraApplicationWithAssets.HTML.Pages.FromAssets
Imports UltraApplicationWithAssets.HTML.Audio.FromAssets
Imports ScriptCoreLib.JavaScript.Runtime

NotInheritable Class Application

    Dim a As New AboutJSC

    Public Sub New(ByVal e As IHTMLElement)
        Native.Document.title = "UltraApplicationWithAssets"
        a.Container.AttachToDocument()


        AddHandler a.WebService_GetTime.onclick, AddressOf WebService_GetTime_onclick
        AddHandler a.Inline1.onclick, AddressOf Inline1_onclick

    End Sub

    Sub WebService_GetTime_onclick(ByVal e As IEvent)
        Dim w As New UltraWebService()

        w.GetTime("time: ", AddressOf GetTimeHandler)
    End Sub

    Sub GetTimeHandler(ByVal result As String)
        Dim p As New IHTMLPre With {
                .innerText = result
            }

        p.AttachTo(a.WebServiceContainer)
    End Sub

    Sub TimerHandler()
        a.Inline1.style.color = ""
    End Sub
    Sub Inline1_onclick(ByVal e As IEvent)
        Dim t As New Timer(AddressOf TimerHandler)

        t.StartTimeout(1000)

        Try
            Dim r As New rooster()

            r.play()
        Catch
        End Try
    End Sub






End Class
