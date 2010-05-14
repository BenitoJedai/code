Namespace Ultra.Studio.StockPages

    Public Module StockPageDefault
        Public Element As XElement = _
<body>
    <div id='PageContainer'>
        <h1 id='Header'>Hello world</h1>
        <p id='Content' style='padding: 2em; color: blue;'>This project was composed in your browser!</p>
    </div>
</body>

        Public Page As XElement = _
<html>
    <head>
        <title>Page</title>
    </head>
    <body style='display: inline-block; width: 400px; height: 300px; border: 1px solid gray; padding: 1em;'>
        <div id='PageContainer'>Hello world</div>
    </body>
</html>

    End Module

End Namespace
