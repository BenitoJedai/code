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


        public async Task<string> WebMethod2()
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


            var c = new PerformanceResourceTimingData2ApplicationPerformance().Count();

            Console.WriteLine(new { c, rid });

            return new { c, rid }.ToString();

        }

    }
}
