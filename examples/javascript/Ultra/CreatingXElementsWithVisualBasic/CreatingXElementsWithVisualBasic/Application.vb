Imports ScriptCoreLib.JavaScript.DOM.HTML
Imports ScriptCoreLib.JavaScript.DOM
Imports ScriptCoreLib.JavaScript
Imports ScriptCoreLib.JavaScript.Extensions
Imports ScriptCoreLib.JavaScript.Runtime
Imports CreatingXElementsWithVisualBasic.HTML.Pages

NotInheritable Class Application



    Public Sub New(ByVal e As IAboutJSC)
        Native.Document.title = "CreatingXElementsWithVisualBasic"

        ' http://msdn.microsoft.com/en-us/library/bb384832.aspx


        Dim test1 =
            <div foo='bar'>
                <h1>Header!</h1>
                <p>hello</p>
                <p>world</p>
                <button>Yes!</button>
            </div>

        test1.Element("h1").Value = "Xml Literals in Visual Basic"

        e.XMLSource.innerText = test1.ToString



        e.XMLVisualizer.innerHTML = test1.ToString



    End Sub







End Class
