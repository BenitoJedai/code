Namespace Ultra.Studio.StockPages

    Public Module StockPageDefault
        Public CanvasDefaultPage As XElement = _
<body style='overflow: hidden;'>
    <link rel="stylesheet" href="App.css"/>
    <div id='ContentSize' style='overflow: hidden; position: absolute;
        left: 0px; right: 0px; bottom: 0px; top: 0px;'>
    </div>
    <div id='Content' style='overflow: hidden; position: absolute;
        left: 0px; right: 0px; bottom: 0px; top: 0px;'>
        <noscript>Error: This Application requires JavaScript.</noscript>
    </div>
</body>

        Public Element As XElement = _
<body>
    <noscript>Error: This Application requires JavaScript.</noscript>
    <link rel="stylesheet" href="App.css"/>

    <div id='PageContainer'>
        <h1 id='Header'>JSC - The .NET crosscompiler for web platforms.</h1>
        <p id='Content' style='padding: 2em; color: blue;'>Hello world</p>
    </div>
</body>

        ''' <summary>
        ''' This XElement is the template for Studio Toolbox HTML Document.
        ''' </summary>
        ''' <remarks></remarks>
        Public Page As XElement = _
<html>
    <head>
        <title>Page</title>
    </head>
    <body style='display: inline-block; width: 400px; height: 300px; border: 1px solid gray; position: relative;'>
        <div id='PageContainer' style='
            position: absolute; 
            left: 0px;
            top: 0px;
            right: 0px;
            bottom: 0px;
            '>
            <div id='Content' style='margin: 1em;'>
                Hello world
            </div>
        </div>
    </body>
</html>

    End Module

End Namespace
