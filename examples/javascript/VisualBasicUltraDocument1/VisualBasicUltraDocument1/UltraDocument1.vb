Imports ScriptCoreLib.JavaScript.DOM.HTML
Imports ScriptCoreLib.JavaScript.Extensions
Imports ScriptCoreLib.Shared.Drawing
Imports System.Net
Imports ScriptCoreLib.ActionScript.flash.display
Imports ScriptCoreLib.ActionScript.Extensions


Public NotInheritable Class UltraDocument1

    Dim AddShape As New IHTMLButton
    Dim AddHandlers As New IHTMLButton
    Dim ChangeColor As New IHTMLButton
    Dim GetData1 As New IHTMLButton
    Dim x As New IHTMLDiv
    Dim s As New UltraSprite
    Public Sub New(ByVal e As IHTMLElement)

        x.innerText = "hello world"

        x.AttachToDocument()

        ChangeColor.innerText = "ChangeColor"
        ChangeColor.AttachTo(x)

        GetData1.innerText = "GetData1"
        GetData1.AttachTo(x)

        AddShape.innerText = "AddShape"
        AddShape.AttachTo(x)

        AddHandlers.innerText = "AddHandlers"
        AddHandlers.AttachTo(x)

        AddHandler GetData1.onclick, AddressOf GetData1_onclick
        AddHandler ChangeColor.onclick, AddressOf ChangeColor_onclick
        AddHandler AddShape.onclick, AddressOf AddShape_onclick
        AddHandler AddHandlers.onclick, AddressOf AddHandlers_onclick

        s.AttachSpriteTo(x)
    End Sub

    Public Sub AtXClick()
        x.style.color = Color.Blue



    End Sub
    Private Sub AddHandlers_onclick(ByVal e As ScriptCoreLib.JavaScript.DOM.IEvent)

        s.AddXClick(AddressOf AtXClick)
        'AddHandler s.XClick, AddressOf AtXClick
    End Sub

    Private Sub AddShape_onclick(ByVal e As ScriptCoreLib.JavaScript.DOM.IEvent)


        ' delay did not work?
        s.AddShape1()




    End Sub


    Private Sub ChangeColor_onclick(ByVal e As ScriptCoreLib.JavaScript.DOM.IEvent)
        x.style.color = Color.Red
    End Sub


    Private Sub GetData1_onclick(ByVal e As ScriptCoreLib.JavaScript.DOM.IEvent)
        Dim ws As New MyWebService

        ws.GetData1("http://example.com", AddressOf GotData)


    End Sub

    Public Sub GotData(ByVal data As String)
        Dim xx As New IHTMLCode

        Dim s = "<body>"

        Dim i = data.IndexOf(s)
        Dim j = data.IndexOf("</BODY>")

        Dim z = i + s.Length
        xx.innerHTML = data.Substring(z, j - z)
        xx.AttachTo(x)

    End Sub
End Class

Public NotInheritable Class MyWebService

    Public Delegate Sub StringAction(ByVal e As String)

    Public Sub GetData1(ByVal address As String, ByVal r As StringAction)

        Dim c As New WebClient

        Dim x = c.DownloadString(address:=address)

        r(x)

    End Sub

End Class

Public NotInheritable Class UltraSprite
    Inherits Sprite

    Public Sub New()



    End Sub


    Class __AddShape1
        Public xx As Integer
        Public xxx As Integer

    End Class


    Public Event XClick As Action

    Public Sub AddXClick(ByVal e As Action)
        AddHandler XClick, e
        AddShape2()

    End Sub

    Public Sub RaiseXClick()

        RaiseEvent XClick()

        AddShape2()

    End Sub


    Dim AddShape1__ As __AddShape1

    Public Sub AddShape1()
        If (AddShape1__ Is Nothing) Then
            AddShape1__ = New __AddShape1
        End If

        Dim x As New Sprite

        AddHandler x.click, AddressOf RaiseXClick

        AddShape1__.xx += &H20
        AddShape1__.xxx += 16

        x.graphics.beginFill(Convert.ToUInt32(AddShape1__.xx))
        x.graphics.drawRect(AddShape1__.xxx, 8, 100, 100)

        x.AttachTo(Me)
    End Sub

    Dim AddShape2__ As __AddShape1

    Public Sub AddShape2()
        If (AddShape2__ Is Nothing) Then
            AddShape2__ = New __AddShape1
        End If

        Dim x As New Sprite


        AddShape2__.xx += &H20
        AddShape2__.xxx += 16

        x.graphics.beginFill(Convert.ToUInt32(AddShape2__.xx))
        x.graphics.drawRect(AddShape2__.xxx, 108, 100, 100)

        x.AttachTo(Me)
    End Sub
End Class
