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
    Inherits ApplicationWebService


    'Public ReadOnly service As New ApplicationWebService()

    Dim page As IApp
    Dim WithEvents GetNewHTML As IHTMLButton

    ''' <summary>
    ''' This is a javascript application.
    ''' </summary>
    ''' <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
    Public Sub New(page As IApp)
        JavaScriptStringExtensions.ToDocumentTitle("Hello world")

        'Native.Document.title = "UsingXLinqWithGoogleAppEngine"
        Me.page = page




        page.WebServiceContainer.Add(
            <div style='border: 1px solid gray; padding: 2em;'>
                <span style='color: red;'> Hello</span> world</div>
        )

        '        02000014 VBXElementExperiment.Application+_Closure$__1
        'script: error JSC1000: Method: _Lambda$__1, Type: VBXElementExperiment.Application+_Closure$__1; emmiting failed : System.ArgumentNullException: Value cannot be null.
        '        Parameter name : source()
        '   at System.Linq.Enumerable.First[TSource](IEnumerable`1 source, Func`2 predicate)
        '   at jsc.IL2ScriptGenerator.<CreateInstructionHandlers>b__c(IdentWriter w, Prestatement p, ILInstruction i, ILFlowStackItem[] s) in x:\jsc.internal.svn\compiler\jsc\Languages\JavaScript\IL2ScriptGenerator.OpCodes.cs:line 567
        '        VBXElementExperiment()


        'page.GetNewHtml.WhenClicked(
        '    Async Sub()

        '        Dim x = Me.GetHTML()

        '        page.foo = x

        '    End Sub
        ')

        GetNewHTML = page.GetNewHtml





        'GetHTML(Sub(html As XElement) page.WebServiceContainer.Add(html))

        ' Send data from JavaScript to the server tier
        WebMethod2(
            "A string from JavaScript.",
            Sub(value) value.ToDocumentTitle()
        )
    End Sub


    Private Async Sub GetNewHTML_onclick(obj As IEvent) Handles GetNewHTML.onclick

        Dim x = Await Me.GetHTML()

        page.foo = x

    End Sub
End Class

