Imports ScriptCoreLib
Imports ScriptCoreLib.Delegates
Imports ScriptCoreLib.Extensions
Imports System
Imports System.Linq
Imports System.Xml.Linq
Imports ScriptCoreLib.Ultra.WebService
Imports ScriptCoreLib.Archive.ZIP

''' <summary>
''' Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
''' </summary>
Public Class ApplicationWebService
    ''' <summary>
    ''' This Method is a javascript callable method.
    ''' </summary>
    ''' <param name="e">A parameter from javascript.</param>
    ''' <param name="y">A callback to javascript.</param>
    Public Sub WebMethod2(e As String, y As Action(Of String))
        ' Send it back to the caller.
        y(e)
    End Sub
    Public Sub TransformHTML(ByVal x As XElement, ByVal f As XElementAction)

    End Sub
    Public Async Function GetHTML() As Threading.Tasks.Task(Of XElement)


        'Dim xml = XElement.Parse("<div style='color: blue;'>Google App Engine XElement</div>")

        'f(xml)


        Return <span style='color: blue;'>
                   <b>Google App Engine Application</b><span> XElement written in </span><b>Visual Basic</b>
               </span>


    End Function

    ' this method should not be called from javascript.
    Public Sub DownloadArchive(ByVal e As WebServiceHandler)

        Dim IsArchive = e.Context.Request.Path = "/archive.zip"

        If (IsArchive) Then

            'http://msdn.microsoft.com/en-us/library/dd293617.aspx
            Dim z As New ZIPFile From {
                {
                    "Content.xml",
                    <Document>
                        <Text>Hello World</Text>
                        <Description>XLinq in java on google app engine written in visual basic</Description>
                    </Document>
                }
            }

            e.Context.Response.ContentType = ZIPFile.ContentType

            Dim bytes = z.ToBytes()

            e.Context.Response.OutputStream.Write(bytes, 0, bytes.Length)

            e.CompleteRequest()

        End If

    End Sub

End Class

