Imports ScriptCoreLib
Imports ScriptCoreLib.Delegates
Imports ScriptCoreLib.Extensions
Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports System.Text
Imports System.Threading.Tasks
Imports System.Xml.Linq
Imports System.Diagnostics

''' <summary>
''' Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
''' </summary>
Public Class ApplicationWebService
    Inherits XApp


    Public index As Int32 = 3



    ' how could we auto bind to xelements?
    '  like Handles Me.XApp_button1_click
    Private Async Sub ApplicationWebService_button1_click() Handles Me.XApp_button1_click


        'Me.title.Value = "you clicked me"


        index += 1


        content =
            <div>dynamic content: 
                <span style='color: blue;'> new content after click (<%= index %>)</span>
            </div>

        Console.WriteLine("ApplicationWebService_button1_click")

        ' could we await and then change the title again?
        ' this would require us to have a stream to the client!


    End Sub

    Protected Event XApp_button1_click As Action

    ' task forces the fields to merge on client again
    Public Async Function RaiseEvent_XApp_button1_click() As Task
        RaiseEvent XApp_button1_click()
    End Function

End Class

' generated from html
Public Class XApp

    Public title As XElement = <title>dynamic title</title>
    Public content As XElement = <div>dynamic content</div>

    Public button1 As XElement = <button>dynamic button</button>




End Class

