using System;
using System.Data.SQLite;
using System.Xml.Linq;
using ScriptCoreLib.Query.Experimental;
using TestSelectMin;

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




        var n = new PerformanceResourceTimingData2ApplicationPerformance();

        n.Delete();


        n.Insert(
            new PerformanceResourceTimingData2ApplicationPerformanceRow
        {
            connectStart = 5,
            connectEnd = 13,
            EventTime = DateTime.Now.AddDays(-0),

            z = new XElement("goo", "foo")
        }
        );


        var ee =n.AsEnumerable();


        var q = from x in new PerformanceResourceTimingData2ApplicationPerformance()
                    //orderby x.Timestamp descending
                select new
                {
                    x.z,

                    x.Key,
                    x.connectStart,
                    x.connectEnd,
                    x.Timestamp
                } into g

                select g;

        //select g.connectStart;

        //var f = q.FirstOrDefault();

        // X:\jsc.svn\examples\javascript\forms\test\TestMinSelector\TestMinSelector\ApplicationControl.cs

        var f = q.Min(k => k.connectStart);
        //var f = q.Min();

        Console.WriteLine(new { f });


    }
}
