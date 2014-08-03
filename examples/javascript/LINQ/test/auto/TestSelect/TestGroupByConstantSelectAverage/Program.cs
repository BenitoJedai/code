using System;
using System.Data.SQLite;
using System.Diagnostics;
using System.Xml.Linq;
using ScriptCoreLib.Query.Experimental;
using TestGroupByConstantSelectAverage;

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
            connectEnd = 666,

            Tag = "first insert"
        },

             new PerformanceResourceTimingData2ApplicationPerformanceRow
        {
            connectStart = 5,
            connectEnd = 9999,
            Tag = "Last insert, selected by group by"
        }
         );



        var f = (
            from x in new PerformanceResourceTimingData2ApplicationPerformance()

                //group x by x.connectStart into gg
            group x by 2 into gg

            select new
            {

                //count1 = gg.Count(),
                average1 = gg.Average(x => x.connectEnd),

                //gg.Key,

                gg.Last().Tag
            }

        ).FirstOrDefault();

        // { f = { count1 = 1, Tag = first insert } }
        // { f = { count1 = 22, Tag = Last insert, selected by group by } }
        System.Console.WriteLine(
            new { f }
            );

        Debugger.Break();


    }
}
