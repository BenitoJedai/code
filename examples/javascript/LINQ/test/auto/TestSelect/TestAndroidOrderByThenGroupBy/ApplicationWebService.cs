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

namespace TestAndroidOrderByThenGroupBy
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        /// <summary>
        /// The static content defined in the HTML file will be update to the dynamic content once application is running.
        /// </summary>
        //public XElement Header = new XElement(@"h1", @"JSC - The .NET crosscompiler for web platforms. ready.");

        // android cookie garbles, or turnacates data?
        // I/System.Console( 1762): XDocument Parse error: { text = <h1 id="Header">JSC - The .NET crosscompiler for web platforms. ready.</h1∩┐╜∩┐╜ }
        //        I/System.Console( 1762): #4 POST /xml/WebMethod2 HTTP/1.1 error:
        //I/System.Console( 1762): #4 java.lang.RuntimeException: expected: '>' actual: '∩┐┐' (position:END_TAG </h1∩┐╜∩┐╜>@1:77 in java.io.StringReader@40669738)
        //I/System.Console( 1762): #4 java.lang.RuntimeException: expected: '>' actual: '∩┐┐' (position:END_TAG </h1∩┐╜∩┐╜>@1:77 in java.io.StringReader@40669738)

        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Activator.cs

        // wtf.

        //        I/System.Console( 4441): Caused by: java.lang.StringIndexOutOfBoundsException
        //I/System.Console( 4441):        at java.lang.String.substring(String.java:1651)
        //I/System.Console( 4441):        at ScriptCoreLibJava.BCLImplementation.System.__String.Substring(__String.java:123)
        //I/System.Console( 4441):        at ScriptCoreLib.Android.BCLImplementation.System.Data.SQLite.__SQLiteConnection.get_InternalDatabaseName(__SQLiteConnection.java:46)
        //I/System.Console( 4441):        at ScriptCoreLib.Android.BCLImplementation.System.Data.SQLite.__SQLiteConnection.<init>(__SQLiteConnection.java:33)
        //I/System.Console( 4441):        at TestAndroidOrderByThenGroupBy.ApplicationWebService.__cctor_b__0(ApplicationWebService.java:152)
        //I/System.Console( 4441):        ... 31 more



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



        public void WebMethod2(Action<string> yield)
        //public async Task<string> WebMethod2()
        {
            Console.WriteLine("enter WebMethod2");

            //            I / System.Console(4251): #8 POST /xml/WebMethod2 HTTP/1.1 error:
            //I / System.Console(4251): #8 java.lang.RuntimeException: System.Diagnostics.Debugger.Break
            //I / System.Console(4251): #8 java.lang.RuntimeException: System.Diagnostics.Debugger.Break
            //I / System.Console(4251):        at ScriptCoreLibJava.BCLImplementation.System.Diagnostics.__Debugger.Break(__Debugger.java:32)
            //I / System.Console(4251):        at ScriptCoreLibJava.BCLImplementation.System.Runtime.CompilerServices.__AsyncTaskMethodBuilder_1.SetException(__AsyncTaskMethodBuilder_1.java:46)

            new PerformanceResourceTimingData2ApplicationPerformance().Delete();

            new PerformanceResourceTimingData2ApplicationPerformance().Insert(
                new PerformanceResourceTimingData2ApplicationPerformanceRow
            {
                connectEnd = 9,
                connectStart = 5,
                Tag = "first insert"
            },

                new PerformanceResourceTimingData2ApplicationPerformanceRow
            {
                connectStart = 5,
                connectEnd = 111,
                Tag = "middle insert"
            },

                new PerformanceResourceTimingData2ApplicationPerformanceRow
            {
                connectStart = 5,
                connectEnd = 11,
                Tag = "Last insert, selected by group by"
            }
            );



            var f = (
                from x in new PerformanceResourceTimingData2ApplicationPerformance()
                    // MYSQL and SQLITE seem to behave differently? in reverse actually!
                    //orderby x.Key ascending
                orderby x.connectEnd descending
                // { f = { Tag = middle insert } }
                group x by x.connectStart into gg
                //select new
                //{
                //    //c = gg.Count(),
                //    gg.Last().Tag
                //}
                select gg.Last().Tag

            ).FirstOrDefault();

            System.Console.WriteLine(
                new { f }
                );

            //Debugger.Break();

            //return new { message = "ok" }.ToString();
            yield(new { message = "ok" }.ToString());
            //return new { message = "ok" }.ToString();
        }

    }
}
