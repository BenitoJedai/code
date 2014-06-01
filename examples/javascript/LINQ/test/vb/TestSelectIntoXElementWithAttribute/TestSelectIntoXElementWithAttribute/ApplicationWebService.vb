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

''' <summary>
''' Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
''' </summary>
Public Class ApplicationWebService

    Public body As XElement =
        <body>
            dynamic. coming full circle arent we. whats the c# razor doing. cshtml?
            <h2>header2</h2>
            <%= (From x In New Data.PerformanceResourceTimingData2.ApplicationPerformance
                 Select <div><%= x.domComplete %></div>
                ).AsGenericEnumerable()
            %>
        </body>

    Sub New()
        Console.WriteLine("init ApplicationWebService")

        Dim x As New Data.PerformanceResourceTimingData2.ApplicationPerformance
        x.Insert(New Data.PerformanceResourceTimingData2ApplicationPerformanceRow With {.domComplete = New Random().Next()})
    End Sub



End Class

