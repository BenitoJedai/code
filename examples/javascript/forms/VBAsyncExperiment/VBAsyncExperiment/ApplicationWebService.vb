Imports ScriptCoreLib
Imports ScriptCoreLib.Delegates
Imports ScriptCoreLib.Extensions
Imports System
Imports System.ComponentModel
Imports System.Linq
Imports System.Xml.Linq

''' <summary>
''' Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
''' </summary>
Partial Public NotInheritable Class ApplicationWebService
	Inherits Component
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

