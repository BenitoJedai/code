Imports ScriptCoreLib.JavaScript.DOM.HTML
Imports ScriptCoreLib.JavaScript.DOM
Imports ScriptCoreLib.JavaScript
Imports ScriptCoreLib.JavaScript.Extensions


Imports ScriptCoreLib.JavaScript.Runtime
Imports ScriptCoreLib.JavaScript.Concepts
Imports ScriptCoreLib.Ultra.Components.HTML.Pages

Partial NotInheritable Class Application

    Public Sub New(ByVal a As IApplicationLoader)

        Native.Document.title = "UsingXComment"

        a.LoadingAnimation.Orphanize()



        Dim x = <Data id='f'>
                    hello world
                    <!-- comment 1 -->
                    <Item id='e' data='colo: 66;'>Hello</Item>
                </Data>

        Dim t As New IHTMLTextArea

        t.value = x.ToString
        t.AttachTo(a.Content)
        t.style.SetSize(600, 400)

        For Each n In x.Nodes().ToArray
            t.value &= Environment.NewLine & "!Node"


            Dim _XText = TryCast(n, XText)

            If Not _XText Is Nothing Then
                t.value &= Environment.NewLine & "XText " & _XText.Value
            End If

            Dim _XComment = TryCast(n, XComment)

            If Not _XComment Is Nothing Then
                t.value &= Environment.NewLine & "XComment " & _XComment.Value
            End If

            Dim _XElement = TryCast(n, XElement)

            If Not _XElement Is Nothing Then
                t.value &= Environment.NewLine & "XElement " & _XElement.ToString

                For Each aa In _XElement.Attributes.ToArray


                    t.value &= Environment.NewLine & "XAttribute " & aa.Name.LocalName & " = " & aa.Value


                Next

            End If

        Next


    End Sub




End Class
