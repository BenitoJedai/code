using System;
using System.Data.SQLite;
using System.Diagnostics;
using ScriptCoreLib.Query.Experimental;
using TestJoinOnNewExpression;

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
            join y in new PerformanceResourceTimingData2ApplicationResourcePerformance() on new { x.connectStart } equals new { y.connectStart }
            select new
            {
                x_connectStart = x.connectStart,

                // can we use it ? comma is not rendered. why?
                y_connectStart = y.connectStart,
                y_connectStart2 = y.connectStart,
            }

        );

        // error
        //var c = q.Count();
        var f = q.FirstOrDefault();

        Console.WriteLine(new { f });

        Debugger.Break();
    }
}
