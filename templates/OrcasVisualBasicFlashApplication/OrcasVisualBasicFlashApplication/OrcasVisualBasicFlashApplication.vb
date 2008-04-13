Imports ScriptCoreLib.ActionScript.flash.display
Imports ScriptCoreLib.ActionScript.flash.text


<Script()> Public Class OrcasVisualBasicFlashApplication
    Inherits Sprite

    Dim WithEvents Greetings As TextField

    Public Sub New()

        ' http://msdn2.microsoft.com/en-us/library/bb385125.aspx

        Me.Greetings = New TextField() With { _
            .text = "hello world from visual basic", _
            .width = 300}.AttachTo(Me)










    End Sub

    Private Sub Greetings_mouseOut(ByVal obj As ScriptCoreLib.ActionScript.flash.events.MouseEvent) Handles Greetings.mouseOut
        Greetings.textColor = &H0
    End Sub


    Private Sub Greetings_mouseOver(ByVal obj As ScriptCoreLib.ActionScript.flash.events.MouseEvent) Handles Greetings.mouseOver
        Greetings.textColor = &HFF00
    End Sub
End Class
