using ScriptCoreLib.Query.Experimental;
using System;
using System.Data;
using System.Data.MySQL;
using System.Data.SQLite;
using System.Linq.Expressions;
using System.Reflection;
using ScriptCoreLib.Extensions;
using TestXMySQL;

class Program
{
    #region example generated data layer
    public class xApplicationPerformance : QueryExpressionBuilder.xSelect<PerformanceResourceTimingData2ApplicationPerformanceRow>
    {
        public xApplicationPerformance()
        {
            Expression<Func<PerformanceResourceTimingData2ApplicationPerformanceRow, PerformanceResourceTimingData2ApplicationPerformanceRow>> selector =
                (xApplicationPerformance) => new PerformanceResourceTimingData2ApplicationPerformanceRow
                {
                    Key = xApplicationPerformance.Key,

                    connectEnd = xApplicationPerformance.connectEnd,
                    connectStart = xApplicationPerformance.connectStart,
                    domComplete = xApplicationPerformance.domComplete,
                    domLoading = xApplicationPerformance.domLoading,
                    EventTime = xApplicationPerformance.EventTime,
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


    //public enum xPerformanceResourceTimingData2ApplicationPerformanceKey : long { }

    //public class xPerformanceResourceTimingData2ApplicationPerformanceRow
    //{
    //    public long connectEnd;
    //    public long connectStart;
    //    public long domComplete;
    //    public long domLoading;
    //    public DateTime EventTime;
    //    public xPerformanceResourceTimingData2ApplicationPerformanceKey Key;
    //    public long loadEventEnd;
    //    public long loadEventStart;
    //    public long requestStart;
    //    public long responseEnd;
    //    public long responseStart;
    //    public string Tag;
    //    public DateTime Timestamp;

    //}
    #endregion

    static void Main(string[] args)
    {
        // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectGroupByAndConstant\TestSelectGroupByAndConstant\ApplicationWebService.cs

        // the idea is to test MySQL as we have LINQ to SQL also running in chrome now
        var mysqld = @"C:\util\xampp-win32-1.8.0-VC9\xampp\mysql\bin\mysqld.exe";



        #region MySQLConnection
        // the safe way to hint we need to talk PHP dialect
        QueryExpressionBuilder.Dialect = QueryExpressionBuilderDialect.PHP;
        QueryExpressionBuilder.WithConnection =
            y =>
            {
                var DataSource = "file:xApplicationPerformance.xlsx.sqlite";
                var cc0 = new MySQLConnection(

                    new System.Data.MySQL.MySQLConnectionStringBuilder
                    {


                        UserID = "root",
                        Server = "127.0.0.1",

                    }.ToString()
                    //new MySQLConnectionStringBuilder { DataSource = "file:PerformanceResourceTimingData2.xlsx.sqlite" }.ToString()
                );





                // Additional information: Authentication to host '' for user '' using method 'mysql_native_password' failed with message: Access denied for user ''@'asus7' (using password: NO)
                // Additional information: Unable to connect to any of the specified MySQL hosts.
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

                y(cc0);

                cc0.Dispose();
            };
        #endregion



        //'TestXMySQL.PerformanceResourceTimingData2ApplicationPerformanceRow' cannot be used for delegate parameter of type 'System.Object'


        // ThreadLocal SynchronizationContext aware ConnectionPool?
        var n = new PerformanceResourceTimingData2ApplicationPerformance();

        n.Create();

        // wont return? jsc broke xMySQL async? no there was an sql error
        //var count = n.CountAsync(cc0).Result;
        var count = n.Count();


        // ScriptCoreLib.Async
        n.Insert(
           new PerformanceResourceTimingData2ApplicationPerformanceRow
           {
               connectStart = 5,
               connectEnd = 13,
               EventTime = DateTime.Now.AddDays(-0)
           }

           // if you do not wait you wont get the id damn it
       );


        // should be based on QueryExpressionBuilder.Dialect, and wait for the last async?
        //var id = cc0.GetLastInsertRowId();


        // http://stackoverflow.com/questions/5440168/c-sharp-mysql-there-is-already-an-open-datareader-associated-with-this-connectio

        var q = from x in new PerformanceResourceTimingData2ApplicationPerformance()
                orderby x.Timestamp descending
                select new
                {
                    x.Key,
                    x.connectStart,
                    x.connectEnd,
                    x.Timestamp
                };

        //var f = q.FirstOrDefaultAsync().Result;
        var f = q.FirstOrDefault();

        Console.WriteLine(new { f });

        //new PerformanceResourceTimingData2ApplicationPerformance().Where(x => x.Key == f.Key).Delete();
        //new PerformanceResourceTimingData2ApplicationPerformance().Delete(x => x.Key == f.Key);
        new PerformanceResourceTimingData2ApplicationPerformance().Delete(f.Key);



    }
}
