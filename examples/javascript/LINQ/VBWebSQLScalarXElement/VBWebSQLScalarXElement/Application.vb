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
Imports VBWebSQLScalarXElement
Imports VBWebSQLScalarXElement.Design
Imports VBWebSQLScalarXElement.HTML.Pages
Imports ScriptCoreLib.Query.Experimental
Imports System.Data.SQLite


''' <summary>
''' Your client side code running inside a web browser as JavaScript.
''' </summary>
Public NotInheritable Class Application
    Inherits ApplicationWebService


    Shared Sub New()
        QueryExpressionBuilder.WithConnection =
             Sub(y)
                 ' readd nuget package, reload project?
                 Dim cc As New SQLiteConnection()
                 cc.Open()
                 y(cc)
                 cc.Dispose()
             End Sub

    End Sub

    Public Sub New(page As IApp)


        ' X:\jsc.svn\examples\javascript\xml\XClickCounter\XClickCounter\Application.cs

        AddHandler Native.body.onclick,
            Sub()

                Dim c As New Data.xxAvatar()

                c.Create()


                ' 0:10369ms errorCallback: { code = 5, message = could not prepare statement (1 no such table: xxAvatar) }
                c.Insert(New Data.xxAvatarRow With {.z = <div>hello<b>world</b></div>})

                c.Insert(<div style='color: red;'>hello <b>world</b></div>)





                Dim m As New IHTMLPre

                m.Add((From x In New Data.xxAvatar()
                       Order By x.Key Descending
                       Select x.z).FirstOrDefaultAsync())

                m.AttachToDocument()

            End Sub





    End Sub


End Class

