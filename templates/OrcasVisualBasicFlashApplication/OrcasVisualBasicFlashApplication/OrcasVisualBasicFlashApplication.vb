Imports ScriptCoreLib.ActionScript.flash.display
Imports ScriptCoreLib.ActionScript.flash.text
Imports ScriptCoreLib.ActionScript.flash.events
Imports ScriptCoreLib.ActionScript.flash.utils


<Script()> Public Class OrcasVisualBasicFlashApplication
    Inherits Sprite

    Dim WithEvents Greetings As TextField
    Dim WithEvents RotatingTimer As New Timer(1000 / 24, 0)

    Dim s As New Sprite

    Public Sub New()

        ' http://msdn2.microsoft.com/en-us/library/bb385125.aspx

        Me.Greetings = New TextField() With { _
            .text = "hello world from visual basic", _
            .width = 300}.AttachTo(Me)




        s.AttachTo(Me)

        With s
            .x = 100
            .y = 100
        End With

        s.graphics.lineStyle(4, &HFF00, 1)
        s.graphics.drawRect(0, 0, 64, 32)



        RotatingTimer.start()



    End Sub

    Private Sub Greetings_mouseOut(ByVal obj As ScriptCoreLib.ActionScript.flash.events.MouseEvent) Handles Greetings.mouseOut
        Greetings.textColor = &H0
    End Sub


    Private Sub Greetings_mouseOver(ByVal obj As ScriptCoreLib.ActionScript.flash.events.MouseEvent) Handles Greetings.mouseOver
        Greetings.textColor = &HFF00
    End Sub

    Private Sub RotatingTimer_timer(ByVal obj As ScriptCoreLib.ActionScript.flash.events.TimerEvent) Handles RotatingTimer.timer
        s.rotation += 1

    End Sub
End Class
