using ScriptCoreLib.Query.Experimental;
using System;
using System.Data.SQLite;
using System.Linq.Expressions;
using System.Xml.Linq;
using TestSQLiteCLRInsert;

class Program
{

    static void Main(string[] args)
    {
        // X:\jsc.svn\examples\javascript\LINQ\ClickCounter\ClickCounter\Application.cs
        // X:\jsc.svn\examples\javascript\LINQ\LINQWebCamAvatars\LINQWebCamAvatars\Application.cs

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
            EventTime = DateTime.Now.AddDays(-0),

            z = new XElement("goo", "foo")
        }
        );
        // { LastInsertRowId = 2 }


        var c = new PerformanceResourceTimingData2ApplicationPerformance().Count();

        Console.WriteLine(new { c, rid });
    }
}
