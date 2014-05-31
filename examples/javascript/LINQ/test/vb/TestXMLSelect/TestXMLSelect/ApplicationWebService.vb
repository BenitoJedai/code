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
Imports TestXMLSelect.Data

''' <summary>
''' Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
''' </summary>
Public Class ApplicationWebService
    Inherits PerformanceResourceTimingData2XMLEntitiesRow

    ' where is the bootstrap taking a ref to page?
    ' X:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToJavaScriptDocument.InjectJavaScriptBootstrap.cs
    ' 233
    ' at this point we should almost be able to do the override?
    ' we already have a special call of
    ' ScriptCoreLib.JavaScript.Extensions.INodeExtensionsWithXLinq.InternalReplaceAll
    ' X:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToJavaScriptDocument.WebServiceForJavaScript.cs

    ' where is the .ApplicationWebService loading its fields?

    Sub New()
        Me.data = <div id="data">dynamic override in the application.ctor bootsrap</div>
    End Sub

    ' !!! if this field shares a name in the IApp htm
    ' then instead of sending it as a cookie
    ' we could send it within HTML itself!
    Public content As XElement = <div>content</div>

    'Public Async Function WebMethod2() As Task(Of XElement)
    Public Async Function WebMethod2() As Task


        Dim x As New PerformanceResourceTimingData2.XMLEntities

        '     at System.Data.QueryStrategyOfTRowExtensions.AsGenericEnumerable[TSource](IQueryStrategy`1 source)
        'at System.Data.QueryStrategyOfTRowExtensions.FirstOrDefault[TElement](IQueryStrategy`1 source)
        'at TestXMLSelect.ApplicationWebService.VB$StateMachine_0_WebMethod2.MoveNext()


        x.Insert(
            <div style='color:red;'>boo1! count: <%= x.Count() %></div>,
            <div style='color:red;'>boo2! count: <%= x.Count() %></div>,
            <div style='color:red;'>boo3! count: <%= x.Count() %></div>
        )

        'x.Insert(New PerformanceResourceTimingData2XMLEntitiesRow With {.data = <div>hello</div>})
        'x.Insert(New PerformanceResourceTimingData2XMLEntitiesRow With {.data = <div>world</div>})
        'x.Insert(<div>goo!</div>)


        'Return x.FirstOrDefault().data
        Dim q = From z In New PerformanceResourceTimingData2.XMLEntities()
                Order By z.Key Descending
                Select z.data



        Dim ge = q.AsGenericEnumerable()


        'Dim dt = q.AsEnumerable()


        ' {1, PGRpdj5oZWxsbzwvZGl2Pg==, , 5/29/2014 2:39:43 PM}
        Dim f = q.FirstOrDefault()


        'Return f

        ' update current selection
        Me.data = f


        content.Add(f)

        'x.AsEnumerable().FirstOrDefault().data
    End Function


End Class

