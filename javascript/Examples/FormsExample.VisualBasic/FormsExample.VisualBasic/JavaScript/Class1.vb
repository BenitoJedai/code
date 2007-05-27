Imports ScriptCoreLib.Shared.Query
Imports ScriptCoreLib.Shared.Drawing
Imports ScriptCoreLib.JavaScript.DOM.HTML
Imports ScriptCoreLib.JavaScript.DOM
Imports ScriptCoreLib.JavaScript
Imports ScriptCoreLib.JavaScript.Windows.Forms



Imports System.Runtime.CompilerServices

<Assembly: Script()> 
<Assembly: ScriptTypeFilter(ScriptType.JavaScript, "*")> 

' imports ScriptCoreLib.JavaScript.DOM.HTML


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

        <Extension()> Function Words(ByVal e As IHTMLTextArea) As String()

            Dim c As Char() = {","c}

            Words = e.value.Split(c)

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


        Dim WithEvents Users1 As New IHTMLTextArea("neo, morpheous, trinity, Agent Smith")
        Dim WithEvents Users2 As New IHTMLTextArea("Rhineheart, Agent Jones, Agent Brown, Dozer, Switch, Mouse")
        Dim WithEvents Users3 As New IHTMLTextArea("Apoc, Tank, Cypher, Oracle")

        Dim WithEvents Filter As New IHTMLInput(HTMLInputTypeEnum.text, "ag")
        Dim WithEvents Results As New IHTMLElement(IHTMLElement.HTMLElementEnum.ol)




        Sub New()
            LongTaskDisplay.Hide()


            Control.appendChild(New IHTMLBreak, LongTask, Me.ButtonBlue, Me.ButtonRed, New IHTMLBreak, Me.Text, LongTaskDisplay)



            ColorSelector.Add(Color.System.ThreeDFace.ToString())
            ColorSelector.Add(Color.System.AppWorkspace.ToString)

            ColorSelector.Add("black")
            ColorSelector.Add("red")
            ColorSelector.Add("green")
            ColorSelector.Add("blue")


            Dim userlist_style = IStyleSheet.Default.AddRule(".userlist").style


            userlist_style.border = "1px solid blue"
            userlist_style.margin = "1em"

            IStyleSheet.Default.AddRule(".userlist:hover").style.borderColor = Color.Red



            Users1.className = "userlist"
            Users2.className = "userlist"
            Users3.className = "userlist"



            Control.appendChild(CreateFieldset("Change the color of the background", ColorSelector))
            Control.appendChild(CreateFieldset("Linq to objects", Users1, Users2, Users3))

            Control.appendChild(CreateFieldset("Names shall contain:", Filter))
            Control.appendChild(CreateFieldset("Results: ", Me.Results))

            UpdateView()


            Dim u = New UserControl1

            Extensions.GetHTMLTarget(u).attachToDocument()




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


        Sub AddAsResult(ByVal e As String)

            Dim x As New IHTMLElement(IHTMLElement.HTMLElementEnum.li, e)

            Me.Results.appendChild(x)


        End Sub

        ReadOnly Property FilterValue() As String
            Get
                FilterValue = Me.Filter.value.ToLower
            End Get

        End Property

        Sub UpdateView() Handles Users1.onchange, Users2.onchange, Users3.onchange, Users1.onkeyup, Users2.onkeyup, Users3.onkeyup



            Dim items = From z In Users1.Words().Concat(Users2.Words).Concat(Users3.Words) Where z.ToLower.Contains(FilterValue)



            Me.Results.removeChildren()




            Enumerable.ForEach(items, AddressOf AddAsResult)


        End Sub


    End Class
End Namespace