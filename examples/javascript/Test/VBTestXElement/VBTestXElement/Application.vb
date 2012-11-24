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
Imports VBTestXElement.Design
Imports VBTestXElement.HTML.Pages
Imports System.Runtime.CompilerServices


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

        Dim xml = VisualStudioTemplates.VisualCSharpProjectReferences.Clone


        Dim pre2 As New IHTMLPre

        pre2.innerText = xml.ToString().Replace("</", Environment.NewLine + "</")
        pre2.AttachToDocument()

        '        arg[0] is typeof System.Xml.Linq.XName
        'script: error JSC1000: No implementation found for this native method, please implement [System.Xml.Linq.XContainer.Descendants(System.Xml.Linq.XName)]

        'VisualStudioTemplates.VisualCSharpProjectReferences...<ItemGroup>.Add(New XElement("foo"))
        'VisualStudioTemplates.VisualCSharpProjectReferences.<Reference>.Add(New XElement("foo"))
        xml.<Reference>.Add(<foo>bar</foo>)

        Dim pre3 As New IHTMLPre

        pre3.innerText = xml.ToString().Replace("</", Environment.NewLine + "</")
        pre3.AttachToDocument()

        Dim pre4 As New IHTMLPre

        xml = VisualStudioTemplates.VisualCSharpProjectReferences.Clone


        pre4.innerText = xml.ToString().Replace("</", Environment.NewLine + "</")
        pre4.AttachToDocument()


        ' Send data from JavaScript to the server tier
        service.WebMethod2(
            "A string from JavaScript.",
            Sub(value) value.ToDocumentTitle()
        )
    End Sub


End Class

Public Module VisualStudioTemplates

    ' http://www.simple-talk.com/dotnet/.net-tools/extending-msbuild/

    Public VisualCSharpProjectReferences As XElement = _
    <ItemGroup>
        <Reference>

        </Reference>
    </ItemGroup>

    <Extension()>
    Public Function Clone(ByVal xml As XElement) As XElement
        Return XElement.Parse(xml.ToString())

    End Function
End Module
