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

namespace TestJoinGroupSelectCastLong
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
            // xxx

            new Data.PerformanceResourceTimingData2.ApplicationPerformance().Insert(
                new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { EventTime = DateTime.Now.AddDays(-0) },
                new Data.PerformanceResourceTimingData2ApplicationPerformanceRow
            {
                EventTime = DateTime.Now.AddDays(-1),
                domComplete = 6
            }
            );


            new Data.PerformanceResourceTimingData2.ApplicationResourcePerformance().Insert(
                new Data.PerformanceResourceTimingData2ApplicationResourcePerformanceRow { requestStart = 6 }
            );


            var q = from x in new Data.PerformanceResourceTimingData2.ApplicationPerformance()

                    join y in new Data.PerformanceResourceTimingData2.ApplicationResourcePerformance() on x.domComplete equals y.requestStart

                    group new { x, y } by new
                    {
                        x.domComplete,
                        y.requestStart
                        //, xxx = "?"
                    } into gg

                    from offset in new[] {
                        //gg.Key.domComplete + 55,
                        12, 23 }
                    //select new { x.domComplete, offset };
                    select new
                    {
                        gg.Key,
                        offset,

                        lkey = (long)gg.Last().x.Key,




                        gg.Last().x.domComplete
                    };

            var f = q.FirstOrDefault();




            Debugger.Break();


        }

    }
}
