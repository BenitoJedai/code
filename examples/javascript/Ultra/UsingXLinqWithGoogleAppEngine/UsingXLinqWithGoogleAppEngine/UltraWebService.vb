Imports ScriptCoreLib.Ultra.Library.Delegates
Imports ScriptCoreLib.Ultra.WebService
Imports ScriptCoreLib.Archive.ZIP
Imports System.IO


Partial Public NotInheritable Class UltraWebService

    ' Step 1. Google App Engine Java needs to resolve XElement type.
    ' Step 2. Implement Parse and ToSting for XElement
    ' Step 3. Implement XLinq API:
    ' -    L_000c: call class [System.Xml.Linq]System.Xml.Linq.XName [System.Xml.Linq]System.Xml.Linq.XName::Get(string, string)
    ' -    L_0022: call class [System.Xml.Linq]System.Xml.Linq.XName [System.Xml.Linq]System.Xml.Linq.XName::Get(string, string)
    ' -    L_002c: newobj instance void [System.Xml.Linq]System.Xml.Linq.XAttribute::.ctor(class [System.Xml.Linq]System.Xml.Linq.XName, object)
    ' -    L_0031: callvirt instance void [System.Xml.Linq]System.Xml.Linq.XContainer::Add(object)


    Public Sub TransformHTML(ByVal x As XElement, ByVal f As XElementAction)

    End Sub
    Public Sub GetHTML(ByVal f As XElementAction)

        'Dim xml = XElement.Parse("<div style='color: blue;'>Google App Engine XElement</div>")

        'f(xml)




        f(
            <div style='color: blue;'>
                <b>Google App Engine Application</b><span> XElement written in </span><b>Visual Basic</b>
            </div>
        )
    End Sub

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
