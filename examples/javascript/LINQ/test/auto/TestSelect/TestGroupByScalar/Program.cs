using System.Data.SQLite;
using ScriptCoreLib.Query.Experimental;
using TestGroupByScalar;

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
            },

            new PerformanceResourceTimingData2ApplicationPerformanceRow
            {
                connectStart = 5,
                Tag = "Last insert, selected by group by"
            }
        );



        var f = (
            from x in new PerformanceResourceTimingData2ApplicationPerformance()

            group x by x.connectStart into gg
            //group x by 2 into gg

            select new { gg.Last().Tag }

        ).FirstOrDefault();

        System.Console.WriteLine(
            new { f.Tag }
            );

    }
}
