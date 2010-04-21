Imports ScriptCoreLib.JavaScript.DOM.HTML
Imports ScriptCoreLib.JavaScript.DOM
Imports ScriptCoreLib.JavaScript
Imports ScriptCoreLib.JavaScript.Extensions

Imports UsingXLinqForPopups.HTML.Pages
Imports UsingXLinqForPopups.HTML.Audio.FromAssets
Imports ScriptCoreLib.JavaScript.Runtime
Imports ScriptCoreLib.JavaScript.Concepts
Imports ScriptCoreLib.Ultra.Components.HTML.Pages

Partial NotInheritable Class Application

    Public Sub New(ByVal a As IAboutJSC)

        Native.Document.title = "UsingXLinqForPopups"

        Dim ww As New UltraWebService

        a.WebServiceContainer.Add(
            <div style='border: 1px solid gray; padding: 2em;'>Hello world</div>
        )

        ww.GetHTML(Sub(html As XElement) a.WebServiceContainer.Add(html))

        AddHandler a.NewWindow.onclick,
            Sub()
                Dim WindowTarget = "_blank"

                If (a.Docked.checked) Then
                    WindowTarget = "Dock1"

                    a.Dock1.name = WindowTarget
                    a.Dock1.frameborder = "0"

                End If

                Dim w1 = Native.Window.open("about:blank", WindowTarget, 600, 300, False)




                ' http://www.devguru.com/Technologies/ecmascript/QuickRef/doc_close.html
                w1.document.open("text/html", "replace")

                '<%= Global.ScriptCoreLib.Ultra.WebService.WebElements.PageShadowContainer %>

                Dim html =
                    <body style='border: 0; padding: 0; margin: 0;'>
                        <div id='PageShadowContainer'>
                            <div style='background-color: #909090; height: 1px;'>
                            </div>
                            <div style='background-color: #A0A0A0; height: 1px;'>
                            </div>
                            <div style='background-color: #B0B0B0; height: 1px;'>
                            </div>
                            <div style='background-color: #C0C0C0; height: 1px;'>
                            </div>
                            <div style='background-color: #D0D0D0; height: 1px;'>
                            </div>
                            <div style='background-color: #E0E0E0; height: 1px;'>
                            </div>
                            <div style='background-color: #F0F0F0; height: 1px;'>
                            </div>
                        </div>
                        <h1>Window</h1>
                    </body>

                w1.document.write(
                    html.ToString
                )
                w1.document.close()

                If (a.Editable.checked) Then
                    w1.document.DesignMode = True

                End If

                w1.document.body.Add(
                  <div>Hello World</div>
              )

                Dim b1 As New IHTMLButton(w1.document) With {.innerText = "Close"}

                b1.AttachTo(w1.document.body)

                AddHandler b1.onclick,
                    Sub()
                        w1.close()
                    End Sub

                Dim b2 As New IHTMLButton(w1.document) With {.innerText = "More"}

                b2.AttachTo(w1.document.body)

                AddHandler b2.onclick,
                    Sub()
                        w1.document.body.Add(
                            <div>Hello World</div>
                        )

                    End Sub

                'AddHandler w1.onbeforeunload,
                '    Sub()
                '        w1.document.DesignMode = False

                '    End Sub

                AddHandler Native.Window.onbeforeunload,
                    Sub()
                        w1.close()

                    End Sub

                'AddHandler Native.Window.onfocus,
                '    Sub()
                '        w1.document.body.style.backgroundColor = JSColor.White

                '    End Sub

                'AddHandler Native.Window.onblur,
                '    Sub()
                '        w1.document.body.style.backgroundColor = JSColor.None

                '    End Sub
            End Sub


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
