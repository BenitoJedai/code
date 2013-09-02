Imports VBAsyncExperiment
Imports VBAsyncExperiment.Design
Imports VBAsyncExperiment.HTML.Pages
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
Imports System.ComponentModel
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Xml.Linq

''' <summary>
''' Your client side code running inside a web browser as JavaScript.
''' </summary>
Public NotInheritable Class Application
	Public ReadOnly service As New ApplicationWebService()

	Public ReadOnly content As New ApplicationControl()

    ''' <summary>
    ''' This is a javascript application.
    ''' </summary>
    ''' <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
    Public Sub New(page As IApp)
        content.AttachControlTo(page.Content)
        content.AutoSizeControlTo(page.ContentSize)
        JavaScriptStringExtensions.ToDocumentTitle("Hello world")
        ' Send data from JavaScript to the server tier
        service.WebMethod2(
            "A string from JavaScript.",
            Sub(value) value.ToDocumentTitle()
        )
    End Sub


End Class

