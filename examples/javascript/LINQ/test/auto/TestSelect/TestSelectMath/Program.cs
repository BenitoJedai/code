using ScriptCoreLib.Query.Experimental;
using System;
using System.Data.SQLite;
using System.Linq.Expressions;

class Program
{
    #region example generated data layer
    public class xApplicationPerformance : QueryExpressionBuilder.xSelect<xPerformanceResourceTimingData2ApplicationPerformanceRow>
    {
        public xApplicationPerformance()
        {
            Expression<Func<xPerformanceResourceTimingData2ApplicationPerformanceRow, xPerformanceResourceTimingData2ApplicationPerformanceRow>> selector =
                (xApplicationPerformance) => new xPerformanceResourceTimingData2ApplicationPerformanceRow
                {
                    // : Field 'connectEnd' defined on type 'Program+xPerformanceResourceTimingData2ApplicationPerformanceRow' is not a field on the target object 
                    // which is of type 'Program+xApplicationPerformance'.

                    connectEnd = xApplicationPerformance.connectEnd,
                    connectStart = xApplicationPerformance.connectStart,
                    domComplete = xApplicationPerformance.domComplete,
                    domLoading = xApplicationPerformance.domLoading,
                    EventTime = xApplicationPerformance.EventTime,
                    Key = xApplicationPerformance.Key,
                    loadEventEnd = xApplicationPerformance.loadEventEnd,
                    loadEventStart = xApplicationPerformance.loadEventStart,
                    requestStart = xApplicationPerformance.requestStart,
                    responseEnd = xApplicationPerformance.responseEnd,
                    responseStart = xApplicationPerformance.responseStart,
                    Tag = xApplicationPerformance.Tag,
                    Timestamp = xApplicationPerformance.Timestamp
                };

            this.selector = selector;
        }
    }


    public enum xPerformanceResourceTimingData2ApplicationPerformanceKey : long { }

    public class xPerformanceResourceTimingData2ApplicationPerformanceRow
    {
        public long connectEnd;
        public long connectStart;
        public long domComplete;
        public long domLoading;
        public DateTime EventTime;
        public xPerformanceResourceTimingData2ApplicationPerformanceKey Key;
        public long loadEventEnd;
        public long loadEventStart;
        public long requestStart;
        public long responseEnd;
        public long responseStart;
        public string Tag;
        public DateTime Timestamp;

    }
    #endregion

    static void Main(string[] args)
    {
        Console.WriteLine("i am a zombie");

        // string DataSource = "file:PerformanceResourceTimingData2.xlsx.sqlite"

        var cc0 = new SQLiteConnection(
            new SQLiteConnectionStringBuilder
            {
                DataSource = "file:PerformanceResourceTimingData2.xlsx.sqlite"
            }.ToString()
        );

        cc0.Open();


        // ThreadLocal SynchronizationContext aware ConnectionPool?
        var n = new xApplicationPerformance();

        n.Create(cc0);

        n.Insert(cc0,
            new xPerformanceResourceTimingData2ApplicationPerformanceRow
            {
                connectStart = 5,
                connectEnd = 13,
                EventTime = DateTime.Now.AddDays(-0)
            }
        );


        //n.Insert();
        //var nn = new TestSelectMath.PerformanceResourceTimingData2.ApplicationPerformance();
        //.Insert(
        //    new TestSelectMath.PerformanceResourceTimingData2ApplicationPerformanceRow { connectStart = 5, connectEnd = 13, EventTime = DateTime.Now.AddDays(-0) }
        //);




        //var q = from x in new TestSelectMath.PerformanceResourceTimingData2.ApplicationPerformance()
        var q = from x in new xApplicationPerformance()

                //let gap1 = 1

                orderby x.Timestamp descending

                //let gap2 = 1
                //let gap3 = 1
                //select x;
                select new { x.Key, x.connectStart, x.connectEnd, x.Timestamp
                    //,  gap2, gap3 
                };

        var f = q.FirstOrDefault(cc0);

        Console.WriteLine(new { f });

        new xApplicationPerformance().Where(x => x.Key == f.Key).Delete(cc0);
        //var zz = q.AsEnumerable(cc);

        //var z = q.AsDataTable(cc);

        cc0.Close();

        //        let add = x.connectStart + x.connectEnd
        //        let mul = add / 3

        //        where mul > 0

        //        select new
        //        {
        //            mul
        //        };

        //var f = q.FirstOrDefault();

        //var z = f.x.field1;

    }
}
