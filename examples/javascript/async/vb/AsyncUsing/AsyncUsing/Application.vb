Imports ScriptCoreLib
Imports ScriptCoreLib.Delegates
Imports ScriptCoreLib.Extensions
Imports ScriptCoreLib.JavaScript
Imports ScriptCoreLib.JavaScript.Components
Imports ScriptCoreLib.JavaScript.DOM
Imports ScriptCoreLib.JavaScript.DOM.HTML
Imports ScriptCoreLib.JavaScript.Extensions
Imports ScriptCoreLib.JavaScript.Windows.Forms
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Xml.Linq
Imports AsyncUsing
Imports AsyncUsing.Design
Imports AsyncUsing.HTML.Pages
Imports ScriptCoreLib.JavaScript.Native


''' <summary>
''' Your client side code running inside a web browser as JavaScript.
''' </summary>
Public NotInheritable Class Application
    Inherits ApplicationWebService


    Dim WithEvents body As IHTMLElement = Native.body



    Public Sub New(page As IApp)




    End Sub

    Class foo
        Implements IDisposable

        Sub New()
            css.style.backgroundColor = "yellow"

        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            css.style.backgroundColor = "transparent"
        End Sub
    End Class

    Private Async Sub body_onclick(obj As IEvent) Handles body.onclick


        Using New foo
            Await Task.Delay(300)

            ' jsc does not yet actually call Dispose! need to fix it
        End Using



    End Sub
End Class

