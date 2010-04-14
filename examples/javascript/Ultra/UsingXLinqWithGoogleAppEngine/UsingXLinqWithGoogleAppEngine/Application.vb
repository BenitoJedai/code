Imports ScriptCoreLib.JavaScript.DOM.HTML
Imports ScriptCoreLib.JavaScript.DOM
Imports ScriptCoreLib.JavaScript
Imports ScriptCoreLib.JavaScript.Extensions

Imports UsingXLinqWithGoogleAppEngine.HTML.Pages
Imports ScriptCoreLib.JavaScript.Runtime
Imports ScriptCoreLib.JavaScript.Concepts
Imports ScriptCoreLib.Ultra.Components.HTML.Pages

Partial NotInheritable Class Application

    Public Sub New(ByVal a As IAboutJSC)

        Native.Document.title = "UsingXLinqWithGoogleAppEngine"

        Dim ww As New UltraWebService

        a.WebServiceContainer.Add(
            <div style='border: 1px solid gray; padding: 2em;'>Hello world</div>
        )

        ww.GetHTML(Sub(html As XElement) a.WebServiceContainer.Add(html))




        AddHandler a.Inline1.onclick,
            Sub()

                Dim t As New Timer(
                    Sub()
                        a.Inline1.style.color = ""

                    End Sub
                )

                t.StartTimeout(1000)




            End Sub

    End Sub




End Class
