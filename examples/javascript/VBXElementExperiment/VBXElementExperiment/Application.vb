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
Imports VBXElementExperiment.Design
Imports VBXElementExperiment.HTML.Pages


''' <summary>
''' Your client side code running inside a web browser as JavaScript.
''' </summary>
Public NotInheritable Class Application
	Public ReadOnly service As New ApplicationWebService()

	''' <summary>
	''' This is a javascript application.
	''' </summary>
	''' <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
	Public Sub New(page As IApp)
        JavaScriptStringExtensions.ToDocumentTitle("Hello world")

        Native.Document.title = "UsingXLinqWithGoogleAppEngine"


        page.WebServiceContainer.Add(
            <div style='border: 1px solid gray; padding: 2em;'>
                <span style='color: red;'> Hello</span> world</div>
        )

        service.GetHTML(Sub(html As XElement) page.WebServiceContainer.Add(html))

		' Send data from JavaScript to the server tier
		service.WebMethod2(
			"A string from JavaScript.",
			Sub (value) value.ToDocumentTitle()
		)
	End Sub 


End Class

