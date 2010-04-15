Imports ScriptCoreLib.Ultra.Library.Delegates


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
End Class
