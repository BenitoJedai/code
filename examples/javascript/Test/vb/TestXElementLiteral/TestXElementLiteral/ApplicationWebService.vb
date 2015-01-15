Imports ScriptCoreLib
Imports ScriptCoreLib.Delegates
Imports ScriptCoreLib.Extensions
Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Diagnostics
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports System.Text
Imports System.Threading.Tasks
Imports System.Xml.Linq

''' <summary>
''' Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
''' </summary>
Public Class ApplicationWebService
    ''' <summary>
    ''' The static content defined in the HTML file will be update to the dynamic content once application is running.
    ''' </summary>
    Public Header As New XElement("h1", "JSC - The .NET crosscompiler for web platforms. ready.")

    ''' <summary>
    ''' This Method is a javascript callable method.
    ''' </summary>
    ''' <param name="e">A parameter from javascript.</param>
    ''' <param name="y">A callback to javascript.</param>
    Public Sub WebMethod2(e As String, y As Action(Of String))
        ' Send it back to the caller.
        y(e)
    End Sub


End Class

