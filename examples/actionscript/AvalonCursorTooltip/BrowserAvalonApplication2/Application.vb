Imports BrowserAvalonApplication2.HTML.Pages
Imports ScriptCoreLib
Imports ScriptCoreLib.Delegates
Imports ScriptCoreLib.Extensions
Imports ScriptCoreLib.JavaScript
Imports ScriptCoreLib.JavaScript.Components
Imports ScriptCoreLib.JavaScript.DOM
Imports ScriptCoreLib.JavaScript.DOM.HTML
Imports ScriptCoreLib.JavaScript.Extensions
Imports System
Imports System.Linq
Imports System.Text
Imports System.Xml.Linq

''' <summary>
''' This type will run as JavaScript.
''' </summary>
Public NotInheritable Class Application
	Public ReadOnly service As New ApplicationWebService()

	Public ReadOnly content As New ApplicationCanvas()

	''' <summary>
	''' This is a javascript application.
	''' </summary>
	''' <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
	Public Sub New(page As IDefaultPage)
		AvalonUltraExtensions.AutoSizeTo(AvalonExtensions.AttachToContainer(content, page.Content), page.ContentSize)
		JavaScriptStringExtensions.ToDocumentTitle("Hello world")
		' Send data from JavaScript to the server tier
		service.WebMethod2(
			"A string from JavaScript.",
			Sub (value) JavaScriptStringExtensions.ToDocumentTitle(value)
		)
	End Sub 


End Class

