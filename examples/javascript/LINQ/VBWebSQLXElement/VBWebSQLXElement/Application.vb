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
Imports VBWebSQLXElement
Imports VBWebSQLXElement.Design
Imports VBWebSQLXElement.HTML.Pages
Imports System.Data.SQLite
Imports WebSQLXElement
Imports ScriptCoreLib.Query.Experimental

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

        page.Header.With(
            Async Sub(h)
                ' X:\jsc.svn\examples\javascript\linq\WebSQLXElement\WebSQLXElement\Application.cs


                Dim cc0 As New SQLiteConnection("")

                cc0.Open()

                Dim n = New xApplicationPerformance()

                n.Create(cc0)


                Await n.InsertAsync(cc0,
                    New WebSQLXElement.xRow With {
                        .xmlString =
                            <div>
                                <h1>hello</h1>
                                <h2>world</h2>
                            </div>
                }
                )

                Dim button As New IHTMLButton With {.innerText = "select all"}
                button.AttachToDocument()

                Await button.async.onclick

                button.Orphanize

                Dim z = n.AsEnumerableAsync(cc0)

                For Each item In Await z

                    Dim x As New IHTMLDiv With {.innerHTML = item.xmlString}

                    x.AttachToDocument()



                Next



            End Sub
        )

    End Sub


End Class
