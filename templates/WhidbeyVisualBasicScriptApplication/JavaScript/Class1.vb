Imports ScriptCoreLib.JavaScript.DOM.HTML
Imports ScriptCoreLib.JavaScript.DOM
Imports ScriptCoreLib.JavaScript
Imports ScriptCoreLib.Shared.Drawing
Imports ScriptCoreLib.Shared.Query







<Assembly: Script()> 
<Assembly: ScriptTypeFilter(ScriptType.JavaScript, "*.JavaScript")> 



Namespace JavaScript


    '<Script()> Public Class Class1

    <Script()> Module Stuff
        Function CreateFieldset(ByVal name As String, ByVal ParamArray controls() As INode) As IHTMLElement
            CreateFieldset = New IHTMLElement(IHTMLElement.HTMLElementEnum.fieldset)

            Dim legend As New IHTMLElement(IHTMLElement.HTMLElementEnum.legend)
            legend.innerText = name

            CreateFieldset.appendChild(legend)
            CreateFieldset.appendChild(controls)

        End Function
    End Module


    <Script()> Public Class Class1

        Dim Text As New IHTMLSpan

        Dim WithEvents Control As New IHTMLDiv("hello from visual basic! Click here!")
        Dim WithEvents ButtonBlue As New IHTMLButton("Blue")
        Dim WithEvents ButtonRed As New IHTMLButton("Red")

        Dim WithEvents LongTask As New IHTMLButton("long task (3sec)")
        Dim LongTaskDisplay As IHTMLImage = New IHTMLImage("gfx/loadingBar.gif")

        Public Const ControlAlias As String = "fx.Class1"



        Dim WithEvents ColorSelector As New IHTMLSelect

        Dim WithEvents IncludeKnownColors As IHTMLInput

        Dim AllColors As Color()

        Sub New()
            LongTaskDisplay.Hide()


            Control.appendChild(New IHTMLBreak, LongTask, Me.ButtonBlue, Me.ButtonRed, New IHTMLBreak, Me.Text, LongTaskDisplay)


            Me.AllColors = New Color() { _
                Color.System.ThreeDFace, _
                Color.System.AppWorkspace, _
                Color.FromKnownName("cyan"), _
                Color.Black, _
                Color.White, _
                Color.Red, _
                Color.Green, _
                Color.Blue _
            }



            Me.IncludeKnownColors = New IHTMLInput(HTMLInputTypeEnum.checkbox)

            Dim IncludeKnownColors_Label = New IHTMLLabel("Include known colors", IncludeKnownColors)





            Control.appendChild(CreateFieldset("Change the color of the background", _
                IncludeKnownColors, IncludeKnownColors_Label, ColorSelector))

            IncludeKnownColors_onclick()

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



        Shared Function ColorToString(ByVal e As Color) As String
            ColorToString = e.ToString

        End Function


        <Script()> Class Incrementor

            Public Value As Integer = 0

            Public Function Increment() As Incrementor
                Value += 1

                Return Me
            End Function
        End Class

        Shared Sub SetOptionValueToColor(ByVal e As IHTMLOption)
            e.style.color = e.value

        End Sub
        Private Sub IncludeKnownColors_onclick() Handles IncludeKnownColors.onclick

            Me.ColorSelector.removeChildren()

            Dim x = New Incrementor


            Dim z = From i In (From ix In AllColors Select New With {.index = x.Increment.Value, .ix = ix}) _
                      Where String.IsNullOrEmpty(i.ix.KnownName) Or IncludeKnownColors.checked _
                      Select _
                        New IHTMLOption() _
                        With { _
                            .value = i.ix.ToString, _
                            .innerHTML = i.index.ToString & " - " & ColorToString(i.ix) _
                        }




            z.ForEach(AddressOf SetOptionValueToColor)

            Me.ColorSelector.appendChild(z.ToArray)





        End Sub
    End Class
End Namespace