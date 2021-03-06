﻿using System.Data.SQLite;
using System.Diagnostics;
using ScriptCoreLib.Query.Experimental;
using TestGroupByConstant;

class Program
{
    static void Main(string[] args)
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/20140705/20140721

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
            Tag = "first insert, ff"
        },

            new PerformanceResourceTimingData2ApplicationPerformanceRow
        {
            connectStart = 5,
            Tag = "Last insert, selected by group by"
        }
        );



        var f = (
            from x in new PerformanceResourceTimingData2ApplicationPerformance()

                //orderby x.Key

                //group x by x.connectStart into gg
            group x by 1 into gg


            // groupby constant returns the first item by default, since it does not need to see all?
            select new { gg.Last().Tag }

        ).FirstOrDefault();

        System.Console.WriteLine(
            new { f.Tag }
            );

        Debugger.Break();
    }
}
