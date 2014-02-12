Imports VBTestXMoneseAPI
Imports VBTestXMoneseAPI.Design
Imports VBTestXMoneseAPI.HTML.Pages
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
Imports System.ComponentModel
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports System.Xml.Linq

''' <summary>
''' Your client side code running inside a web browser as JavaScript.
''' </summary>
Public NotInheritable Class Application
	Inherits ApplicationWebService
	Public ReadOnly content As New ApplicationControl()

	''' <summary>
	''' This is a javascript application.
	''' </summary>
	''' <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
	Public Sub New(page As IApp)
		content.AttachControlToDocument()
		JavaScriptStringExtensions.ToDocumentTitle("Hello world")
		' Send data from JavaScript to the server tier
		Me.WebMethod2(
			"A string from JavaScript.",
			Sub (value) value.ToDocumentTitle()
		)
	End Sub 


End Class

