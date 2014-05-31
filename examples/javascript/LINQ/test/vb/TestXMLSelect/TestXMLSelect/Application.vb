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
Imports TestXMLSelect
Imports TestXMLSelect.Design
Imports TestXMLSelect.HTML.Pages

''' <summary>
''' Your client side code running inside a web browser as JavaScript.
''' </summary>
Public NotInheritable Class Application
    Inherits ApplicationWebService


    Dim WithEvents body As IHTMLBody = Native.body



    Public Sub New(page As IApp)

        Dim c = 77

        ' X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Xml\Linq\XContainer.cs

        page.header = <h1 style='color:blue;'>this is a header <%= c %></h1>


        ' bind it
        page.content = Me.content

        'now automagic?
        'page.data = Me.data

        'Me.content.AttachToDocument()


    End Sub

    Private Sub body_onclick(obj As IEvent) Handles body.onclick
        Me.WebMethod2()
    End Sub
End Class

