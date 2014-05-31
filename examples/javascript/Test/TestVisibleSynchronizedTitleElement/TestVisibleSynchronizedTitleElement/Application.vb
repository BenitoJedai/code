Imports ScriptCoreLib
Imports ScriptCoreLib.Delegates
Imports ScriptCoreLib.Extensions
Imports ScriptCoreLib.JavaScript
Imports ScriptCoreLib.JavaScript.Components
Imports ScriptCoreLib.JavaScript.DOM
Imports ScriptCoreLib.JavaScript.DOM.HTML
Imports ScriptCoreLib.JavaScript.Extensions
Imports ScriptCoreLib.JavaScript.Windows.Forms
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Xml.Linq
Imports TestVisibleSynchronizedTitleElement
Imports TestVisibleSynchronizedTitleElement.Design
Imports TestVisibleSynchronizedTitleElement.HTML.Pages

''' <summary>
''' Your client side code running inside a web browser as JavaScript.
''' </summary>
Public NotInheritable Class Application
    Inherits ApplicationWebService

    Dim WithEvents button1 As IHTMLButton


    Public Sub New(page As IApp)

        button1 = page.button1


        'Invoke()


        'AddHandler page.button1.onclick, 



    End Sub

    Private Sub button1_onclick(obj As IEvent) Handles button1.onclick

        'RaiseEvent XApp_button1_click()
        Me.RaiseEvent_XApp_button1_click()

    End Sub
End Class

