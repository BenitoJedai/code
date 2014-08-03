using ScriptCoreLib.Query.Experimental;
using System;
using System.Data.SQLite;
using System.Linq.Expressions;
using System.Xml.Linq;
using TestSelectAverage;
using System.Diagnostics;
using ScriptCoreLib.Query.Experimental;
using System.Linq;

class Program
{

    static void Main(string[] args)
    {
        // X:\jsc.svn\examples\javascript\LINQ\ClickCounter\ClickCounter\Application.cs
        // X:\jsc.svn\examples\javascript\LINQ\LINQWebCamAvatars\LINQWebCamAvatars\Application.cs

        #region QueryExpressionBuilder.WithConnection
        QueryExpressionBuilder.WithConnection =
            y =>
            {
                var cc = new SQLiteConnection(
                    new SQLiteConnectionStringBuilder { DataSource = "file:PerformanceResourceTimingData2.xlsx.sqlite" }.ToString()
                );

                cc.Open();
                y(cc);
                cc.Dispose();
            };
        #endregion


        //new[] { 5 }.Average();

        // ThreadLocal SynchronizationContext aware ConnectionPool?
        var n = new PerformanceResourceTimingData2ApplicationPerformance();

        n.Insert(
            new PerformanceResourceTimingData2ApplicationPerformanceRow { connectStart = 5 },
            new PerformanceResourceTimingData2ApplicationPerformanceRow { connectStart = 55 },
            new PerformanceResourceTimingData2ApplicationPerformanceRow { connectStart = 555 }
        );

        //var c = new PerformanceResourceTimingData2ApplicationPerformance().Count();
        var c = (from x in new PerformanceResourceTimingData2ApplicationPerformance()
                 select x.connectStart
                 ).Average();


        // { c = 205 }
        Console.WriteLine(new { c });

        Debugger.Break();
    }
}
