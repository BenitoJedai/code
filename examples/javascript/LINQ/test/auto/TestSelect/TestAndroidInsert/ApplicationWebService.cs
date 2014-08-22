using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ScriptCoreLib.Query.Experimental;
using System.Data.SQLite;

namespace TestAndroidInsert
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        //        D/Cursor( 4155):
        //D/Cursor( 4155): Database path: /data/data/TestAndroidInsert.Activities/databases/PerformanceResourceTimingData2.xlsx.sqlite
        //D/Cursor( 4155):
        //D/Cursor( 4155): Table name   : null
        //D/Cursor( 4155):
        //D/Cursor( 4155): SQL          : SQLiteQuery: SELECT last_insert_rowid()
        //I/dalvikvm( 4155): Uncaught exception thrown by finalizer(will be discarded):
        //I/dalvikvm( 4155): Ljava/lang/IllegalStateException;: Finalizing cursor android.database.sqlite.SQLiteCursor@405ffad0 on null that has not been deactivated or closed
        //I/dalvikvm( 4155):      at android.database.sqlite.SQLiteCursor.finalize(SQLiteCursor.java:620)
        //I/dalvikvm( 4155):      at dalvik.system.NativeStart.run(Native Method)

        static ApplicationWebService()
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
        }


        //public async Task<string> WebMethod2()
        public void WebMethod2(Action<string> yield)
        {
            // ThreadLocal SynchronizationContext aware ConnectionPool?
            var n = new PerformanceResourceTimingData2ApplicationPerformance();

            var rid = n.Insert(
                new PerformanceResourceTimingData2ApplicationPerformanceRow
            {
                connectStart = 5,
                connectEnd = 13,

                // conversion done in AddParameter
                // .stack rewriter needs to store struct. can we create new byref struct parameters?
                //EventTime = DateTime.Now.AddDays(-0),

                // conversion done in Insert?
                z = new XElement("goo", "foo")
            }
            );

            // { LastInsertRowId = 2 }
            Console.WriteLine("after insert " + new { rid });


            var c = new PerformanceResourceTimingData2ApplicationPerformance().Count();

            Console.WriteLine(new { c, rid });

            // I/System.Console( 7320): {{ c = 18, rid = 18 }}
            //return new { c, rid }.ToString();
            yield(new { c, rid }.ToString());

        }

    }
}
