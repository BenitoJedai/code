Imports ScriptCoreLib.Ultra.Library.Delegates


Partial Public NotInheritable Class UltraWebService

    ' Step 1. Google App Engine Java needs to resolve XElement type.
    ' Step 2. Implement Parse and ToSting for XElement

    Public Sub TransformHTML(ByVal x As XElement, ByVal f As XElementAction)

    End Sub
    Public Sub GetHTML(ByVal f As XElementAction)

        Dim xml = XElement.Parse("<div>Google App Engine XElement</div>")

        f(xml)




        'f(
        '    <div style='border-left: 2em solid gray; padding-left: 1em; margin-left: 1em;'>
        '        <h3>Hello world</h3>
        '        <p>Foo bar</p>
        '    </div>
        ')
    End Sub
End Class
