
Namespace Ultra.Studio.StockData

    Public Module StockWebMethod2Data
        Public Element As XElement = _
    <Document>
        <Data>Hello world</Data>
        <Client>Unchanged text</Client>
    </Document>


        Public Element2 As XElement = _
<Document foo='bar'>
    <!-- this is just a comment -->
    <Header>
        <!-- this 
                is 
                    just 
                        a 
                            comment 
            -->

    </Header>
        Prefix
        <Data>Hello world</Data>
    <Client>Unchanged text</Client>
        Suffix
        <Footer>x<Foo>x</Foo><Bar>y</Bar></Footer>
        kenny
    </Document>
    End Module

End Namespace
