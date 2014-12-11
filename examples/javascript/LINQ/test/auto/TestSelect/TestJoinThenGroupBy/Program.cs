using System;
using System.Data.SQLite;
using System.Diagnostics;
using ScriptCoreLib.Query.Experimental;
using TestJoinThenGroupBy;

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
            APconnectStart = 5,
            Tag = "first insert 1"
        }
        );

        new PerformanceResourceTimingData2ApplicationResourcePerformance().Insert(
             new PerformanceResourceTimingData2ApplicationResourcePerformanceRow
        {
            ARPconnectStart = 5,
            Tag = "first insert 2"
        }
         );


        var q = (
            from x in new PerformanceResourceTimingData2ApplicationPerformance()
            join y in new PerformanceResourceTimingData2ApplicationResourcePerformance() on x.APconnectStart equals y.ARPconnectStart

            group new { x, y } by x.APconnectStart into gg

            select new
            {
                field1 = gg.Last().x.Tag,
                field2 = gg.Last().y.Tag,
                field3 = gg.Last().x.APconnectStart,
            }

        );

        var f = q.FirstOrDefault();

        Console.WriteLine(new { f });
        // { f = { field1 = 5, field2 = 5, field3 = 5 } }

        Debugger.Break();
    }
}
