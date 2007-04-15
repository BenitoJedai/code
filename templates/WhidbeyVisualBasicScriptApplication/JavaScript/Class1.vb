<Assembly: Script()> 
<Assembly: ScriptTypeFilter(ScriptType.JavaScript, "*.JavaScript")> 

' imports ScriptCoreLib.JavaScript.DOM.HTML


Namespace JavaScript

    <Script()> Public Class Class1

        Dim Text As New IHTMLSpan

        Dim WithEvents Control As New IHTMLDiv("hello from visual basic! Click here!")
        Dim WithEvents ButtonBlue As New IHTMLButton("Blue")
        Dim WithEvents ButtonRed As New IHTMLButton("Red")

        Dim WithEvents LongTask As New IHTMLButton("long task (3sec)")
        Dim LongTaskDisplay As IHTMLImage = New IHTMLImage("gfx/loadingBar.gif")

        Public Const ControlAlias As String = "fx.Class1"



        Dim WithEvents ColorSelector As New IHTMLSelect


        Sub New()
            LongTaskDisplay.Hide()


            Control.appendChild(New IHTMLBreak, LongTask, Me.ButtonBlue, Me.ButtonRed, New IHTMLBreak, Me.Text, LongTaskDisplay)



            ColorSelector.Add(Color.System.ThreeDFace.ToString())
            ColorSelector.Add(Color.System.AppWorkspace.ToString)

            ColorSelector.Add("black")
            ColorSelector.Add("red")
            ColorSelector.Add("green")
            ColorSelector.Add("blue")

            Dim frame As New IHTMLElement(IHTMLElement.HTMLElementEnum.fieldset)
            Dim legend As New IHTMLElement(IHTMLElement.HTMLElementEnum.legend)


            legend.innerText = "Change the color of the background"
            frame.appendChild(legend, ColorSelector)




            Control.appendChild(frame)





        End Sub


        Shared Sub New()

            Native.Spawn(ControlAlias, AddressOf Spawn)



        End Sub

        Shared Sub Spawn(ByVal e As IHTMLElement)
            Dim x As New Class1

            e.insertPreviousSibling(x.Control)


        End Sub


        Private Sub Control_onclick(ByVal e As ScriptCoreLib.JavaScript.DOM.IEvent) Handles Control.onclick
            Static Counter As Integer

            Counter += 1


            Me.Text.innerHTML = "you have clicked me " + Counter.ToString() + " times"


        End Sub

        Private Sub ButtonBlue_onclick(ByVal e As ScriptCoreLib.JavaScript.DOM.IEvent) Handles ButtonBlue.onclick
            Control.style.color = Color.Blue

        End Sub

        Private Sub ButtonRed_onclick(ByVal e As ScriptCoreLib.JavaScript.DOM.IEvent) Handles ButtonRed.onclick
            Control.style.color = Color.Red

        End Sub

        Private Sub LongTask_onclick(ByVal e As ScriptCoreLib.JavaScript.DOM.IEvent) Handles LongTask.onclick
            Me.LongTaskDisplay.Show()




            ScriptCoreLib.JavaScript.Runtime.Fader.FadeOut(Me.LongTaskDisplay, 3000, 300)


        End Sub

        Private Sub ColorSelector_onchange(ByVal e As ScriptCoreLib.JavaScript.DOM.IEvent) Handles ColorSelector.onchange
            Native.Document.body.style.backgroundColor = Color.FromKnownName(ColorSelector.value)


        End Sub
    End Class
End Namespace