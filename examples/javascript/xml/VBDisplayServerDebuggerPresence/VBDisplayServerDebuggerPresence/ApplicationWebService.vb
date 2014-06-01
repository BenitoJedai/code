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
Imports System.Threading

''' <summary>
''' Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
''' </summary>
Public Class ApplicationWebService




    Public info As XElement =
        <div><%= New With {
                                  System.Diagnostics.Process.GetCurrentProcess().ProcessName,
                                  Thread.CurrentThread.ManagedThreadId,
                                  Environment.CommandLine,
                                  Environment.Version,
                                  Environment.UserName,
                                  Debugger.IsAttached
                                  } %></div>


End Class

