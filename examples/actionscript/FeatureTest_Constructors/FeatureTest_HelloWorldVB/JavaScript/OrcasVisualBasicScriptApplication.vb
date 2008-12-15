Imports ScriptCoreLib
Imports ScriptCoreLib.Shared.Lambda
Imports ScriptCoreLib.Shared.Drawing
Imports ScriptCoreLib.JavaScript.DOM.HTML
Imports ScriptCoreLib.JavaScript.DOM
Imports ScriptCoreLib.JavaScript
Imports ScriptCoreLib.JavaScript.Extensions

Imports ScriptCoreLib.Shared

<Assembly: Script()> 
<Assembly: ScriptTypeFilter(ScriptType.JavaScript, "*.JavaScript")> 


Namespace JavaScript
    <Script(), ScriptApplicationEntryPoint()> Public Class FeatureTest_HelloWorldVB

        Dim WithEvents Control As New IHTMLDiv("hello from visual basic! Click here!")
        Sub New()
            Control.AttachToDocument()
        End Sub
        Shared Sub New()
            GetType(FeatureTest_HelloWorldVB).SpawnTo(AddressOf Spawn)
        End Sub
        Shared Function Spawn(ByVal i As IHTMLElement) As FeatureTest_HelloWorldVB
            Spawn = New FeatureTest_HelloWorldVB
        End Function
        Private Sub Control_onclick(ByVal e As ScriptCoreLib.JavaScript.DOM.IEvent) Handles Control.onclick
            Me.Control.style.color = Color.Blue
        End Sub

    End Class
End Namespace