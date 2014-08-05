using System;
using System.Data.SQLite;
using System.Diagnostics;
using ScriptCoreLib.Query.Experimental;
using TestJoin;

class Program
{
    static void Main(string[] args)
    {
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


        var q = (
            from x in new PerformanceResourceTimingData2ApplicationPerformance()
            join y in new PerformanceResourceTimingData2ApplicationResourcePerformance() on x.connectStart equals y.connectStart
            select new
            {
                field1 = x.connectStart,
                field2 = y.connectStart,
                field3 = y.connectStart,
            }

        );

        var f = q.FirstOrDefault();

        Console.WriteLine(new { f });
        // { f = { field1 = 5, field2 = 5, field3 = 5 } }

        Debugger.Break();
    }
}
