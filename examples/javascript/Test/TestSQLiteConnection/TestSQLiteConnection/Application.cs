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
using System.IO;
using System.Diagnostics;

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
            // Application Cache NoUpdate event 

            // X:\jsc.svn\examples\javascript\Test\TestWebSQLDatabase\TestWebSQLDatabase\Application.cs

            //Console.SetOut(new xConsole());

            new IHTMLButton { "invoke" }.AttachToDocument().onclick +=
                async delegate
            {
                var sw = Stopwatch.StartNew();
                var f = await Foo.Invoke();

                //new IHTMLPre { f.connectStart, f.connectEnd, f.Tag }.AttachToDocument();
                new IHTMLPre { new { sw.ElapsedMilliseconds, f.connectStart, f.connectEnd, f.Tag } }.AttachToDocument();

                // {{ ElapsedMilliseconds = 877, connectStart = 5.0, connectEnd = 13.0, Tag = what about xml? }}
                // {{ ElapsedMilliseconds = 234, connectStart = 5.0, connectEnd = 13.0, Tag = what about xml? }}
                // {{ ElapsedMilliseconds = 233, connectStart = 5.0, connectEnd = 13.0, Tag = what about xml? }}
                // {{ ElapsedMilliseconds = 66, connectStart = 5.0, connectEnd = 13.0, Tag = what about xml? }}
                // {{ ElapsedMilliseconds = 61, connectStart = 5.0, connectEnd = 13.0, Tag = what about xml? }}
            };

            new IHTMLButton { "invoke worker" }.AttachToDocument().onclick += async delegate
            {
                // http://calvinmetcalf.com/post/55957954794/web-workers-are-slower-and-thats-ok
                // https://bugzilla.mozilla.org/show_bug.cgi?id=653967
                // http://social.msdn.microsoft.com/forums/ie/en-US/aa17a719-d2e6-4277-a2f7-5cb05dcc66b6/indexeddb-is-extremely-slow-in-web-worker
                // http://ejohn.org/blog/asmjs-javascript-compile-target/
                // http://mrale.ph/blog/2013/03/28/why-asmjs-bothers-me.html

                var sw = Stopwatch.StartNew();
                var f = await Task.Run(Foo.Invoke);

                new IHTMLPre { new { sw.ElapsedMilliseconds, f.connectStart, f.connectEnd, f.Tag } }.AttachToDocument();

                // {{ ElapsedMilliseconds = 1018, connectStart = 5.0, connectEnd = 13.0, Tag = what about xml? }}
                // {{ ElapsedMilliseconds = 914, connectStart = 5.0, connectEnd = 13.0, Tag = what about xml? }}
                // {{ ElapsedMilliseconds = 1036, connectStart = 5.0, connectEnd = 13.0, Tag = what about xml? }}
                // no console
                // {{ ElapsedMilliseconds = 460, connectStart = 5.0, connectEnd = 13.0, Tag = what about xml? }}
                // {{ ElapsedMilliseconds = 414, connectStart = 5.0, connectEnd = 13.0, Tag = what about xml? }}

                // no log, console open
                // {{ ElapsedMilliseconds = 585, connectStart = 5.0, connectEnd = 13.0, Tag = what about xml? }}
                // no log, no console
                // {{ ElapsedMilliseconds = 400, connectStart = 5.0, connectEnd = 13.0, Tag = what about xml? }}
                // {{ ElapsedMilliseconds = 392, connectStart = 5.0, connectEnd = 13.0, Tag = what about xml? }}


            };
            //Foo.Invoke2();

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
        public static async Task<xPerformanceResourceTimingData2ApplicationPerformanceRow> Invoke()
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

            #endregion


            // X:\jsc.svn\examples\javascript\linq\WebSQLXElement\WebSQLXElement\Application.cs
            var n = new xApplicationPerformance();

            Console.WriteLine("before create");
            n.Create(cc0);

            // X:\jsc.svn\core\ScriptCoreLib.Async\ScriptCoreLib.Async\Query\Experimental\QueryExpressionBuilderAsync.IDbConnection.Insert.cs
            Console.WriteLine("before insert");
            await n.InsertAsync(cc0,
                new xPerformanceResourceTimingData2ApplicationPerformanceRow
            {
                connectStart = 5,
                connectEnd = 13,
                EventTime = DateTime.Now.AddDays(-0),
                Tag = "what about xml?"
            }
            );


            //await Task.Delay(10);

            Console.WriteLine("after insert " + new { cc0.LastInsertRowId });

            var q = from x in new xApplicationPerformance()

                        //let gap1 = 1

                        //orderby x.Timestamp descending

                        //let gap2 = 1
                        //let gap3 = 1
                        //select x;
                    select x;

            //    new
            //{
            //    ///* 0000:0002 */  /* WriteProjection */
            //    ///* 0000:0003 */  /* let */ `x`.`connectStart` as _6wAABgsu8D2ea0LF3_aPlKg


            //    x.connectStart,
            //    x.connectEnd,
            //    x.Timestamp
            //    //,  gap2, gap3 
            //};

            // X:\jsc.svn\examples\javascript\test\TestMemberInitExpression\TestMemberInitExpression\Application.cs

            Console.WriteLine("before FirstOrDefaultAsync");

            //q.Count();

            var f = await q.FirstOrDefaultAsync(cc0);

            // 0:14956ms {{ f = [object Object] }} 

            Console.WriteLine(new { f.connectStart, f.connectEnd, f.Tag });

            cc0.Close();

            return f;
        }

    }


    #region xConsole
    //class xConsole : StringWriter
    [Obsolete("jsc:js does not allow to overrider an override? we need it for SpecialFieldInfo to work!")]
    class xConsole : TextWriter
    {
        // http://www.danielmiessler.com/study/encoding_encryption_hashing/
        [Obsolete("can we have encrypted encoding?")]
        public override Encoding Encoding
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override void Write(string value)
        {
            var p = new IHTMLCode { innerText = value }.AttachToDocument();
            var s = p.style;

            // jsc, enum tostring?
            if (Console.ForegroundColor == ConsoleColor.Red)
                s.color = "red";

            if (Console.ForegroundColor == ConsoleColor.Blue)
                s.color = "blue";

            if (Console.ForegroundColor == ConsoleColor.Gray)
                s.color = "gray";

            if (Console.ForegroundColor == ConsoleColor.Yellow)
                s.color = "yellow";

            if (Console.ForegroundColor == ConsoleColor.Magenta)
                s.color = "magneta";
        }

        public override void WriteLine(object value)
        {
            WriteLine("" + value);
        }

        public override void WriteLine(string value)
        {
            //Console.WriteLine(new { value });


            Write(value);

            new IHTMLBreak { }.AttachToDocument();
        }
    }
    #endregion

}
