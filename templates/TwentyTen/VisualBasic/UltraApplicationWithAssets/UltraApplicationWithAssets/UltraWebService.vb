Imports ScriptCoreLib.Ultra.Library.Delegates


Partial Public NotInheritable Class UltraWebService
    Public Sub GetTime(ByVal x As String, ByVal result As StringAction)
        result(x & DateTime.Now)
    End Sub

    Public Sub GetData(ByVal x As XElement, ByVal yield As XElementAction)
        ' no generics for java just yet

        Dim r = From a In x.<Action>
                Select <ActionYield><%= a %></ActionYield>

        Dim y = <Document>
                    <Item>Hello World</Item>
                    <%= r %>
                </Document>

        'y.<Arguments>(0).Add(x)


        yield(y)


    End Sub


    Public Sub GetHTML(ByVal f As XElementAction)

    End Sub
End Class
