Imports ScriptCoreLib.ActionScript.flash.display
Imports ScriptCoreLib.ActionScript.flash.filters
Imports ScriptCoreLib.ActionScript.flash.text
Imports ScriptCoreLib.ActionScript.flash.events
Imports ScriptCoreLib.ActionScript.flash.utils
Imports ScriptCoreLib.ActionScript.flash.geom


<Script(), _
 SWF(width:=OrcasVisualBasicFlashApplication.DefaultWidth, height:=OrcasVisualBasicFlashApplication.DefaultHeight), _
 ScriptApplicationEntryPoint(width:=OrcasVisualBasicFlashApplication.DefaultWidth, height:=OrcasVisualBasicFlashApplication.DefaultHeight)> _
Public Class OrcasVisualBasicFlashApplication
    Inherits Sprite

    Public Const DefaultWidth As Integer = 400
    Public Const DefaultHeight As Integer = 300

    Dim WithEvents Greetings As TextField


    <Script()> _
    Class Rotator
        Inherits Sprite

        Public WithEvents RotatingTimer As New Timer(1000 / 24, 0)

        Sub New(Optional ByVal zoom As Double = 1)

            Dim size = DefaultWidth / 8 * zoom

            graphics.beginFill(&HFF0000, 0.5)
            graphics.drawRect(-size / 2, -size / 2, size, size)
            graphics.endFill()



            graphics.lineStyle(4, &HFF00, 1)
            graphics.drawRect(-size / 2, -size / 2, size, size)


            rotation = New Random().NextDouble() * 360

            RotatingTimer.start()

        End Sub

        Private Sub RotatingTimer_timer(ByVal obj As ScriptCoreLib.ActionScript.flash.events.TimerEvent) Handles RotatingTimer.timer
            rotation += 1


        End Sub
    End Class



    <Script()> Class Zen
        Public Sub New(Optional ByVal message As String = Nothing)

        End Sub

        Sub Add(Optional ByVal source As IEnumerable(Of Zen) = Nothing)

        End Sub

    End Class


    Public Sub New()
        ' http://oakleafblog.blogspot.com/2007/06/will-visual-basic-90-have-collection.html
        Dim z As New Zen
        Dim c As Zen() = {New Zen, New Zen("xxx")}

        z.Add()
        z.Add(c)


        graphics.beginFill(&HFFFFFF, 1)
        graphics.drawRect(0, 0, DefaultWidth, DefaultHeight)
        graphics.endFill()



        ' http://msdn2.microsoft.com/en-us/library/bb385125.aspx

        Me.Greetings = New TextField() With { _
            .defaultTextFormat = New TextFormat With {.size = 16}, _
            .filters = New BitmapFilter() {New DropShadowFilter()}, _
            .text = "From Visual Basic To ActionScript via jsc / click somewhere", _
            .autoSize = TextFieldAutoSize.LEFT, _
            .width = 300}.AttachTo(Me)




        Dim s As New Rotator(4) With {.x = DefaultWidth / 2, .y = DefaultHeight / 2}

        s.AttachTo(Me)

        
        

    End Sub

    Private Sub Greetings_mouseOut(ByVal obj As ScriptCoreLib.ActionScript.flash.events.MouseEvent) Handles Greetings.mouseOut
        Greetings.textColor = &H0
    End Sub


    Private Sub Greetings_mouseOver(ByVal obj As ScriptCoreLib.ActionScript.flash.events.MouseEvent) Handles Greetings.mouseOver
        Greetings.textColor = &HFF00
    End Sub




    Private Sub OrcasVisualBasicFlashApplication_click(ByVal obj As ScriptCoreLib.ActionScript.flash.events.MouseEvent) Handles Me.click
        Dim s As New Rotator With {.x = obj.stageX, .y = obj.stageY}

        s.AttachTo(Me)


    End Sub
End Class
