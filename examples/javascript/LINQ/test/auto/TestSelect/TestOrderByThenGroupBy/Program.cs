using System.Data.SQLite;
using System.Diagnostics;
using ScriptCoreLib.Query.Experimental;
using TestOrderByThenGroupBy;

class Program
{
    static void Main(string[] args)
    {
        // does the jsc agent notice when we add xlsx?
        // if it oes then it should autorebuild, and make sure XSqlite is nugeted..

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

        new PerformanceResourceTimingData2ApplicationPerformance().Delete();

        new PerformanceResourceTimingData2ApplicationPerformance().Insert(
            new PerformanceResourceTimingData2ApplicationPerformanceRow
        {
            connectEnd = 9,
            connectStart = 5,
            Tag = "first insert"
        },

            new PerformanceResourceTimingData2ApplicationPerformanceRow
        {
            connectStart = 5,
            connectEnd = 111,
            Tag = "middle insert"
        },

            new PerformanceResourceTimingData2ApplicationPerformanceRow
        {
            connectStart = 5,
            connectEnd = 11,
            Tag = "Last insert, selected by group by"
        }
        );



        var f = (
            from x in new PerformanceResourceTimingData2ApplicationPerformance()
            // MYSQL and SQLITE seem to behave differently? in reverse actually!
            //orderby x.Key ascending
            orderby x.connectEnd ascending
            // { f = { Tag = middle insert } }
            group x by x.connectStart into gg
            //select new
            //{
            //    //c = gg.Count(),
            //    gg.Last().Tag
            //}
                       select gg.Last().Tag

        ).FirstOrDefault();

        System.Console.WriteLine(
            new { f }
            );

        Debugger.Break();
    }
}
