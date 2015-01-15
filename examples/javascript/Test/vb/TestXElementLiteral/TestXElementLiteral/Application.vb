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
Imports TestXElementLiteral
Imports TestXElementLiteral.Design
Imports TestXElementLiteral.HTML.Pages

''' <summary>
''' Your client side code running inside a web browser as JavaScript.
''' </summary>
Public NotInheritable Class Application
    Inherits ApplicationWebService
    ''' <summary>
    ''' This is a javascript application.
    ''' </summary>
    ''' <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
    Public Sub New(page As IApp)

        ' http://msdn.microsoft.com/en-us/library/1xk7dw1d(v=vs.90).aspx
        'Error BC30800	Method arguments must be enclosed In parentheses.	TestXElementLiteral	Application.vb	33

        'Native.Document.body.Add <div>hello world</div>
        ' <div>hello world</div>
        Native.body.Add(<div>hello <b>world</b></div>)

        '(Native.css +IHTMLElement.HTMLElementEnum.b).
        'Native.css.Item(" b").style.color = "red"
        Native.css(" b").style.color = "red"






        'pa

    End Sub


End Class

