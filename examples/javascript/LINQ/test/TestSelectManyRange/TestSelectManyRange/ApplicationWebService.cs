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

namespace TestSelectManyRange
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        /// <summary>
        /// The static content defined in the HTML file will be update to the dynamic content once application is running.
        /// </summary>
        public XElement Header = new XElement(@"h1", @"JSC - The .NET crosscompiler for web platforms. ready.");

        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2()
        {
            //new Data.PerformanceResourceTimingData2.ApplicationPerformance().Clear();

            new Data.PerformanceResourceTimingData2.ApplicationPerformance().Insert(
                new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { EventTime = DateTime.Now.AddDays(-0) },
                new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { EventTime = DateTime.Now.AddDays(-1), domComplete = 5 }
            );


            // http://www.sqlite.org/lang_select.html#fromclause
            // http://stackoverflow.com/questions/774475/what-joins-does-sqlite-support
            var q = from x in new Data.PerformanceResourceTimingData2.ApplicationPerformance()
                    where x.domComplete == 5
                        // .AsGenericEnumerable()
                    from y in Enumerable.Range(0, 3)
                    select new { x, y };

            //var z = new Data.PerformanceResourceTimingData2.ApplicationPerformance().AsGenericEnumerable().SelectMany(
            //     x => Enumerable.Range(0, 3),
            //     (x, y) => new { x, y }
            //);

            var dt = q.AsDataTable();

            //var qa = q.ToArray();

            Debugger.Break();

        }


    }
}
