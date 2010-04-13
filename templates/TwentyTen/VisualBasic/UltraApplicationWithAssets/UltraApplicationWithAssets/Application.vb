Imports ScriptCoreLib.JavaScript.DOM.HTML
Imports ScriptCoreLib.JavaScript.DOM
Imports ScriptCoreLib.JavaScript
Imports ScriptCoreLib.JavaScript.Extensions

Imports UltraApplicationWithAssets.HTML.Pages
Imports UltraApplicationWithAssets.HTML.Audio.FromAssets
Imports ScriptCoreLib.JavaScript.Runtime
Imports ScriptCoreLib.JavaScript.Concepts
Imports ScriptCoreLib.Ultra.Components.HTML.Pages

Partial NotInheritable Class Application

    Public Sub New(ByVal a As IAboutJSC)

        Native.Document.title = "UltraApplicationWithAssets"

        Dim ww As New UltraWebService

        ww.GetHTML(Sub(html As XElement) a.WebServiceContainer.Add(html))



        'does not work yet
        'ww.GetHTML(AddressOf a.WebServiceContainer.Add)
        'a.WebServiceContainer.Add(AddressOf ww.GetHTML)



        AddHandler a.WebService_GetTime.onclick,
            Sub()
                Dim w As New UltraWebService()

                w.GetTime("time: ",
                    Sub(Result As String)
                        Dim p As New IHTMLPre With {
                            .innerText = Result
                        }

                        p.AttachTo(a.WebServiceContainer)
                    End Sub
                )

                w.GetData(
                    <Dynamic>
                        <Action>Hello World</Action>
                        <Action>Foo bar</Action>
                    </Dynamic>,
                    Sub(doc)
                        Dim t As New TreeNode(AddressOf VistaTreeNodePage.Create)

                        t.Visualize(doc)

                        t.Container.AttachTo(a.WebServiceContainer)
                    End Sub
                )
            End Sub


        AddHandler a.Inline1.onclick,
            Sub()

                Dim t As New Timer(
                    Sub()
                        a.Inline1.style.color = ""

                    End Sub
                )

                t.StartTimeout(1000)


                Try
                    Dim r As New rooster()

                    r.play()
                Catch
                End Try

                MsgBox("You should hear a rooster.")

            End Sub

    End Sub




End Class
