using System;
using System.Data.SQLite;
using System.Diagnostics;
using ScriptCoreLib.Query.Experimental;
using TestSelectScalarCount;

class Program
{
    static void Main(string[] args)
    {
        // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\Experimental\QueryExpressionBuilder.cs
        // 410


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

        new PerformanceResourceTimingData2ApplicationPerformance().Insert(
            new PerformanceResourceTimingData2ApplicationPerformanceRow
        {
            connectStart = 5,
            Tag = "first insert"
        }
        );

        new PerformanceResourceTimingData2ApplicationResourcePerformance().Insert(
             new PerformanceResourceTimingData2ApplicationResourcePerformanceRow
        {
            connectStart = 5,
            Tag = "first insert"
        }
         );

        new PerformanceResourceTimingData2ApplicationResourcePerformance().Insert(
             new PerformanceResourceTimingData2ApplicationResourcePerformanceRow
        {
            connectStart = 5,
            Tag = "last insert"
        }
         );


        // Additional information: no such column: x.connectStart


        var f = (
            from x in new PerformanceResourceTimingData2ApplicationPerformance()

            let y =
                from z in new PerformanceResourceTimingData2ApplicationResourcePerformance()
                where z.connectStart == x.connectStart
                select z.Key

            let c = y.Count()


            select c

        ).FirstOrDefault();

        Console.WriteLine(new { f });

        Debugger.Break();

    }
}
