using ScriptCoreLib.Query.Experimental;
using System;
using System.Data;
using System.Data.MySQL;
using System.Data.SQLite;
using System.Linq.Expressions;
using System.Reflection;
using ScriptCoreLib.Extensions;

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
        // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectGroupByAndConstant\TestSelectGroupByAndConstant\ApplicationWebService.cs

        // the idea is to test MySQL as we have LINQ to SQL also running in chrome now
        var mysqld = @"C:\util\xampp-win32-1.8.0-VC9\xampp\mysql\bin\mysqld.exe";



        #region MySQLConnection
        // the safe way to hint we need to talk PHP dialect
        QueryExpressionBuilder.Dialect = QueryExpressionBuilderDialect.PHP;

        var DataSource = "file:xApplicationPerformance.xlsx.sqlite";
        var cc0 = new MySQLConnection(

            new System.Data.MySQL.MySQLConnectionStringBuilder
            {
                
                
                UserID = "root",
                Server = "127.0.0.1",
                
            }.ToString()
            //new MySQLConnectionStringBuilder { DataSource = "file:PerformanceResourceTimingData2.xlsx.sqlite" }.ToString()
        );
        #endregion





        // Additional information: Authentication to host '' for user '' using method 'mysql_native_password' failed with message: Access denied for user ''@'asus7' (using password: NO)
        cc0.Open();

        #region use db
        {
            var a = Assembly.GetExecutingAssembly().GetName();


            // SkipUntilIfAny ???
            var QDataSource = a.Name + ":" + DataSource.SkipUntilIfAny("file:").TakeUntilIfAny(".xlsx.sqlite");

            // QDataSource.Length = 76
            var QLengthb = QDataSource.Length;

            // Database	64
            cc0.CreateCommand("CREATE DATABASE IF NOT EXISTS `" + QDataSource + "`").ExecuteScalar();
            cc0.CreateCommand("use `" + QDataSource + "`").ExecuteScalar();
        }
        #endregion

        // ThreadLocal SynchronizationContext aware ConnectionPool?
        var n = new xApplicationPerformance();

        n.Create(cc0);

        // wont return? jsc broke xMySQL async? no there was an sql error
        var count = n.CountAsync(cc0).Result;
        //var count = n.Count(cc0);


        // ScriptCoreLib.Async
         n.InsertAsync(cc0,
            new xPerformanceResourceTimingData2ApplicationPerformanceRow
            {
                connectStart = 5,
                connectEnd = 13,
                EventTime = DateTime.Now.AddDays(-0)
            }

            // if you do not wait you wont get the id damn it
        ).Wait();


        // should be based on QueryExpressionBuilder.Dialect, and wait for the last async?
        var id = cc0.GetLastInsertRowId();


        // http://stackoverflow.com/questions/5440168/c-sharp-mysql-there-is-already-an-open-datareader-associated-with-this-connectio

        var q = from x in new xApplicationPerformance()
                orderby x.Timestamp descending
                select new
                {
                    x.Key,
                    x.connectStart,
                    x.connectEnd,
                    x.Timestamp
                };

        var f = q.FirstOrDefault(cc0);

        Console.WriteLine(new { f });

        new xApplicationPerformance().Where(x => x.Key == f.Key).Delete(cc0);

        cc0.Close();


    }
}
