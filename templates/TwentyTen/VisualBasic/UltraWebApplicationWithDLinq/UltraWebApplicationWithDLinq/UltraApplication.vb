Imports ScriptCoreLib.JavaScript.DOM.HTML
Imports ScriptCoreLib.JavaScript.Extensions
Imports ScriptCoreLib.JavaScript

Imports UltraWebApplicationWithDLinq.HTML.Pages

Public NotInheritable Class UltraApplication

    Dim a As IHTMLPage1

    Dim WithEvents button1 As New IHTMLButton
    Dim WithEvents button2 As New IHTMLButton

    Public Sub New(ByVal a As IHTMLPage1)

        Me.a = a


        Native.Document.title = "UltraWebApplicationWithDLinq"

        Me.button1 = a.Button1
        Me.button2 = a.Button2

        a.Button1.innerText = "Add"
        a.Button2.innerText = "Enumerate"

    End Sub




    Private Sub button1_onclick(ByVal e As ScriptCoreLib.JavaScript.DOM.IEvent) Handles button1.onclick

        button2.style.color = "red"

        Dim w As New UltraWebService

        w.Add("client time: " & DateTime.Now, AddressOf Enumerate)

    End Sub

    Sub YieldEnumerate(ByVal ID As String, ByVal Text1 As String)
        Dim x As New IHTMLDiv

        x.innerText = Text1

        x.AttachTo(a.Content)
    End Sub

    Sub Enumerate(ByVal dummy As String)
        a.Content.Clear()

        Dim w As New UltraWebService
        button2.style.color = ""

        w.Enumerate(
            Sub(Data As XElement)

                a.Content.Add(
                    <div>
                        <code>
                            <span>#</span><span><%= Data.<ID>(0).Value %></span>
                            <span> - </span>
                            <span><%= Data.<Text1>(0).Value %></span>
                        </code>
                    </div>
                )
            End Sub
        )

    End Sub
    Private Sub button2_onclick(ByVal e As ScriptCoreLib.JavaScript.DOM.IEvent) Handles button2.onclick


        Enumerate("")

    End Sub
End Class
