Option Explicit On

Imports ScriptCoreLib
Imports ScriptCoreLib.JavaScript
Imports ScriptCoreLib.JavaScript.Runtime

Imports ScriptCoreLib.JavaScript.DOM
Imports ScriptCoreLib.JavaScript.DOM.HTML
Imports ScriptCoreLib.Shared
Imports ScriptCoreLib.Shared.Drawing




<Assembly: ScriptTypeFilter(ScriptType.JavaScript, "JavaScript")> 
<Assembly: Script()> 



Namespace JavaScript


    <Script()> Public Class Class3

        Shared WithEvents btn As New IHTMLButton("visual basic: hello world")

        Shared WithEvents window As IWindow = Native.Window




        Protected Shared Sub window_onload(ByVal e As ScriptCoreLib.JavaScript.DOM.IEvent) Handles window.onload
            Native.Document.body.appendChild(btn)

            btn.style.color = Color.Red

            Console.Log("asp net hello world : vb")

        End Sub

        Protected Shared Sub btn_onclick(ByVal e As ScriptCoreLib.JavaScript.DOM.IEvent) Handles btn.onclick
            window.alert("The button was clicked!")

        End Sub
    End Class
End Namespace

Namespace MyServer

    Partial Class Default2
        Inherits System.Web.UI.Page

        Protected Sub Default2_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            jsc.server.WebTools.CompileAndRegisterClientScript(Me)

        End Sub
    End Class
End Namespace
