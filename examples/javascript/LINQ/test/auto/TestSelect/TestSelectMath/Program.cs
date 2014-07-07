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
        var n = new xApplicationPerformance();

        n.Create();

        n.Insert(
            new xPerformanceResourceTimingData2ApplicationPerformanceRow
            {
                connectStart = 5,
                connectEnd = 13,
                EventTime = DateTime.Now.AddDays(-0)
            }
        );

        var q = from x in new xApplicationPerformance()
                orderby x.Timestamp descending
                select new
                {
                    x.Key,
                    x.connectStart,
                    x.connectEnd,
                    x.Timestamp
                };

        var f = q.FirstOrDefault();

        Console.WriteLine(new { f });

        new xApplicationPerformance().Where(x => x.Key == f.Key).Delete();



    }
}
