using System;
using System.Data.SQLite;
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


        var f = (
            from x in new PerformanceResourceTimingData2ApplicationPerformance()
            join y in new PerformanceResourceTimingData2ApplicationResourcePerformance() on x.connectStart equals y.connectStart
            select new
            {
                x_connectStart = x.connectStart,

                // can we use it ? comma is not rendered. why?
                y_connectStart = y.connectStart,
                y_connectStart2 = y.connectStart,
            }

        ).FirstOrDefault();

        // why null?
        Console.WriteLine(new { f });

    }
}
