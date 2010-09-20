Imports ScriptCoreLib
Imports ScriptCoreLib.Delegates
Imports ScriptCoreLib.Extensions
Imports System
Imports System.Linq
Imports System.Xml.Linq

''' <summary>
''' Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
''' </summary>
Public NotInheritable Class ApplicationWebService
	''' <summary>
	''' This Method is a javascript callable method.
	''' </summary>
	''' <param name="e">A parameter from javascript. JSC supports string data type for all platforms.</param>
	''' <param name="y">A callback to javascript. In the future all platforms will allow Action&lt;XElementConvertable&gt; delegates.</param>
	Public Sub WebMethod2(e As String, y As StringAction)
		' Send it back to the caller.
		y(e)
	End Sub 


End Class

