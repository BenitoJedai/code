Imports ScriptCoreLib.ActionScript.flash.display
Imports ScriptCoreLib.ActionScript.flash.filters
Imports ScriptCoreLib.ActionScript.flash.text
Imports ScriptCoreLib.ActionScript.flash.events
Imports ScriptCoreLib.ActionScript.flash.utils
Imports ScriptCoreLib.ActionScript.flash.geom

Namespace ActionScript


    <Script(), SWF()> _
    Public Class FeatureTest_FlashHelloWorldVB
        Inherits Sprite

        Dim WithEvents Control As New TextField

        Public Sub New()
            Control.width = 400

            Control.text = "hello world from visual basic"
            Control.AttachTo(Me)
        End Sub

        Private Sub Control_click(ByVal obj As ScriptCoreLib.ActionScript.flash.events.MouseEvent) Handles Control.click
            Control.setTextFormat(New TextFormat With {.color = &HFF})

        End Sub
    End Class
End Namespace