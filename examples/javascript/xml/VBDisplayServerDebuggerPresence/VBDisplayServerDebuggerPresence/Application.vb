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
Imports VBDisplayServerDebuggerPresence
Imports VBDisplayServerDebuggerPresence.Design
Imports VBDisplayServerDebuggerPresence.HTML.Pages

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

    End Sub


End Class

