using ScriptCoreLib.Query.Experimental;
using System;
using System.Data.SQLite;
using System.Linq.Expressions;
using System.Xml.Linq;
using TestSelectScalarArrayOfXElementField;

class Program
{

    static void Main(string[] args)
    {
        // X:\jsc.svn\examples\javascript\LINQ\ClickCounter\ClickCounter\Application.cs
        // X:\jsc.svn\examples\javascript\LINQ\LINQWebCamAvatars\LINQWebCamAvatars\Application.cs

        Console.WriteLine("i am a zombie");

        // string DataSource = "file:PerformanceResourceTimingData2.xlsx.sqlite"


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

        n.Create();

        n.Insert(
            new PerformanceResourceTimingData2ApplicationPerformanceRow
            {
                connectStart = 5,
                connectEnd = 13,
                EventTime = DateTime.Now.AddDays(-0),

                z = new XElement("goo", "foo")
            }
        );

        //var c = new PerformanceResourceTimingData2ApplicationPerformance().Count();

        var q = from x in new PerformanceResourceTimingData2ApplicationPerformance()
                orderby x.Timestamp descending
                select new[]{x.z};


        var f = q.FirstOrDefault();

        Console.WriteLine(new { f });

        //new xApplicationPerformance().Where(x => x.Key == f.Key).Delete();
        //new PerformanceResourceTimingData2ApplicationPerformance().Delete(x => x.Key == f.Key);



    }
}
