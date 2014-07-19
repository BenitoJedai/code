using System;
using System.Data.SQLite;
using System.Xml.Linq;
using ScriptCoreLib.Query.Experimental;
using TestSelectScalarMin;

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


        new PerformanceResourceTimingData2ApplicationPerformance().Insert(
            new PerformanceResourceTimingData2ApplicationPerformanceRow
        {
            connectStart = 5,
            connectEnd = 13,
            EventTime = DateTime.Now.AddDays(-0),

            z = new XElement("goo", "foo")
        }
        );

        new PerformanceResourceTimingData2ApplicationResourcePerformance().Insert(
            new PerformanceResourceTimingData2ApplicationResourcePerformanceRow
            {
                // lookup
                connectStart = 5,

                duration = 77
            },

               new PerformanceResourceTimingData2ApplicationResourcePerformanceRow
            {
                // lookup
                connectStart = 5,

                duration = 33
            }
        );



        var q = from x in new PerformanceResourceTimingData2ApplicationPerformance()
                

                let rminq = from y in new PerformanceResourceTimingData2ApplicationResourcePerformance()
                            where y.connectStart == x.connectStart
                            select y.duration

                let rmin = rminq.Min()

                select new { rmin };




        var f = q.FirstOrDefault();

        //var f = q.Min();

        Console.WriteLine(new { f });


    }
}
