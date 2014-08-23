using System;
using System.Data.SQLite;
using System.Xml.Linq;
using ScriptCoreLib.Query.Experimental;
using TestSQLiteCLRSumLong;

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

        // ThreadLocal SynchronizationContext aware ConnectionPool?
        var n = new PerformanceResourceTimingData2ApplicationPerformance();

        var rid = n.Insert(
            new PerformanceResourceTimingData2ApplicationPerformanceRow
        {
            connectStart = 5,
            connectEnd = 13,
            //EventTime = DateTime.Now.AddDays(-0),

            z = new XElement("goo", "foo")
        }
        );
        // { LastInsertRowId = 2 }

        // sumLong = 0
        var sumLong = (
            from x in new PerformanceResourceTimingData2ApplicationPerformance()
            where x.connectEnd == 13
            select x.connectStart
            ).Sum();

        Console.WriteLine(new { sumLong });
    }
}
