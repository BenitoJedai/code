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
Imports TestCoClassForLayoutVB.Design
Imports TestCoClassForLayoutVB.HTML.Pages

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


        Dim f As New SpecialLayout With {.info = "goo"}



        f.AttachToDocument()











        'document.title =


        JavaScriptStringExtensions.ToDocumentTitle("Hello world")
        ' Send data from JavaScript to the server tier
        service.WebMethod2(
            "A string from JavaScript.",
            Sub(value) value.ToDocumentTitle()
        )
    End Sub


End Class

