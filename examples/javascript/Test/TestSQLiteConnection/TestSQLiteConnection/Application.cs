using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestSQLiteConnection;
using TestSQLiteConnection.Design;
using TestSQLiteConnection.HTML.Pages;
using ScriptCoreLib.Query.Experimental;
using System.Linq.Expressions;
using System.Data.SQLite;

namespace TestSQLiteConnection
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // X:\jsc.svn\examples\javascript\Test\TestWebSQLDatabase\TestWebSQLDatabase\Application.cs



            Foo.Invoke();

        }

    }

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


    class Foo
    {
        public static async Task Invoke()
        {
            #region SQLiteConnection
            var cc0 = new SQLiteConnection(
                new SQLiteConnectionStringBuilder
            {
                DataSource = "file:PerformanceResourceTimingData2.xlsx.sqlite"
            }.ToString()
            );

            cc0.Open();

            //cc0.BeginTransaction(System.Data.IsolationLevel.

            {
                var c1 = cc0.CreateCommand();
                c1.CommandText = "CREATE TABLE IF NOT EXISTS Employee_Table (xid, Name, Location)";
                c1.ExecuteNonQuery();

                Console.WriteLine("table created");
            }

            {
                var c1 = cc0.CreateCommand();
                c1.CommandText = "insert into Employee_Table(xid, Name, Location) values(0, 'foo', 'bar')";
                // allow the callback
                await c1.ExecuteNonQueryAsync();

                // 0:1348ms {{ LastInsertRowId = 1 }}
                Console.WriteLine(new { cc0.LastInsertRowId });
            }

            {
                var c1 = cc0.CreateCommand();
                c1.CommandText = "SELECT xid, Name, Location FROM Employee_Table";
                // allow the callback
                var r = await c1.ExecuteReaderAsync();

                Console.WriteLine(new { r });

                while (r.Read())
                {
                    Console.WriteLine("xRow " + new
                    {
                        xid = r.GetInt32(r.GetOrdinal("xid")),
                        Name = r.GetString(r.GetOrdinal("Name")),
                        Location = r["Location"]
                    }
                        );
                }

                // 0:1348ms {{ LastInsertRowId = 1 }}
            }

            cc0.Close();
            #endregion

            var q = from x in new xApplicationPerformance()

                        //let gap1 = 1

                    orderby x.Timestamp descending

                    //let gap2 = 1
                    //let gap3 = 1
                    //select x;
                    select new
                    {
                        x.connectStart,
                        x.connectEnd,
                        x.Timestamp
                        //,  gap2, gap3 
                    };

            var f = q.FirstOrDefault();

            Console.WriteLine(new { f });

        }
    }
}
