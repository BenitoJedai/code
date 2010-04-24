Imports ScriptCoreLib.JavaScript.DOM.HTML
Imports ScriptCoreLib.JavaScript.DOM
Imports ScriptCoreLib.JavaScript
Imports ScriptCoreLib.JavaScript.Extensions

Imports ClientDatabase.HTML.Pages
Imports ClientDatabase.HTML.Audio.FromAssets
Imports ScriptCoreLib.JavaScript.Runtime
Imports ScriptCoreLib.JavaScript.Concepts
Imports ScriptCoreLib.Ultra.Components.HTML.Pages

Partial NotInheritable Class Application

    Public Sub New(ByVal a As IAbout)

        Native.Document.title = "ClientDatabase"

        If (Native.Window.openDatabase Is Nothing) Then
            a.Content.Add(
                  <div style='border: 1px solid gray; padding: 2em; color: red;'>NO HTML5 Database Storage</div>
              )
        Else
            a.Content.Add(
                  <div style='border: 1px solid gray; padding: 2em;'>HTML5 Database Storage</div>
            )
        End If

        a.Content.Add(
            New Audio().Container
        )



    End Sub




End Class
