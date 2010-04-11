Imports ScriptCoreLib.JavaScript.DOM.HTML
Imports ScriptCoreLib.JavaScript.DOM
Imports ScriptCoreLib.JavaScript
Imports ScriptCoreLib.JavaScript.Extensions
Imports ScriptCoreLib.JavaScript.Runtime
Imports CreatingXElementsWithVisualBasic.HTML.Pages
Imports ScriptCoreLib.JavaScript.Concepts
Imports ScriptCoreLib.Ultra.Components.HTML.Pages

NotInheritable Class Application

    Dim WithEvents button007 As IHTMLButton


    Public Sub New(ByVal e As IAboutJSC)
        Native.Document.title = "CreatingXElementsWithVisualBasic"

        ' http://msdn.microsoft.com/en-us/library/bb384832.aspx
        ' http://research.microsoft.com/en-us/um/people/emeijer/papers/xlinq%20xml%20programming%20refactored%20(the%20return%20of%20the%20monoids).htm


        Dim test1 =
            <div foo='bar'>
                <h1>Header!</h1>
                <p>hello</p>
                <p>world</p>
                <button id='button007'>Yes!</button>
                <span style='color: red;'>hello world</span>
            </div>

        test1.Element("h1").Value = "Xml Literals in Visual Basic"



        e.XMLSource.innerText = test1.ToString


        Dim t As New TreeNode(AddressOf VistaTreeNodePage.Create)

        t.Visualize(test1)
        t.Container.AttachTo(e.XMLVisualizer)


        e.HTMLVisualizer.innerHTML = test1.ToString


        button007 = e.HTMLVisualizer.ownerDocument.getElementById("button007")

    End Sub


    Private Sub button007_onclick(ByVal e As ScriptCoreLib.JavaScript.DOM.IEvent) Handles button007.onclick
        Native.Window.alert("button007")
    End Sub
End Class
