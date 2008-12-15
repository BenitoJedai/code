Imports ScriptCoreLib.ActionScript.flash.display

Imports ScriptCoreLib.ActionScript.Extensions
Imports OrcasVisualBasicAvalonApplication.Shared

Namespace ActionScript

    <Script(), _
     SWF(width:=ApplicationCanvas.DefaultWidth, height:=ApplicationCanvas.DefaultHeight), _
     ScriptApplicationEntryPoint(width:=ApplicationCanvas.DefaultWidth, height:=ApplicationCanvas.DefaultHeight)> _
    Public Class ApplicationFlash
        Inherits Sprite

        Public Sub New()
            ' spawn the wpf control
            AvalonExtensions.AttachToContainer(New ApplicationCanvas(), Me)

        End Sub

    End Class

End Namespace
