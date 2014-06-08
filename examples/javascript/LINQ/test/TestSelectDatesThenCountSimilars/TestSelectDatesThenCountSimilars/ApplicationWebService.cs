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

namespace TestSelectDatesThenCountSimilars
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
                // timestamp is special and insert will override it!
                // shall jsc signify that with [Obsolete attribute?]

                // will timestamp also include signature, hash or ID signature? or is it good
                // enough if we sign previous data row?
                // does security metadata need to live in a different table? a generic security table?
                //DateTime.Now.AddDays(-1)
                new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { EventTime = DateTime.Now.AddDays(-0) },

                new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { EventTime = DateTime.Now.AddDays(-1) },
                new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { EventTime = DateTime.Now.AddDays(-1) },

                new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { EventTime = DateTime.Now.AddDays(-2) },
                new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { EventTime = DateTime.Now.AddDays(-2) },
                new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { EventTime = DateTime.Now.AddDays(-2) },

                new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { EventTime = DateTime.Now.AddDays(-3) },
                new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { EventTime = DateTime.Now.AddDays(-3) },
                new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { EventTime = DateTime.Now.AddDays(-3) },
                new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { EventTime = DateTime.Now.AddDays(-3) },


                new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { EventTime = DateTime.Now.AddDays(-5) }

            );











            // X:\jsc.svn\examples\javascript\LINQ\MashableVelocityGraph\MashableVelocityGraph\ApplicationWebService.cs

            // http://www.sqlite.org/lang_select.html#fromclause
            // http://stackoverflow.com/questions/774475/what-joins-does-sqlite-support

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201406/20140608

            var q = from x in new Data.PerformanceResourceTimingData2.ApplicationPerformance()




                    select new
                    {
                        xEventTime = x.EventTime,
                        xEventTimeDate = x.EventTime.Date,


                        byDate = (from xx in new Data.PerformanceResourceTimingData2.ApplicationPerformance()
                                  where xx.EventTime.Date == x.EventTime.Date

                                  // make sure the .Select is there
                                  select xx.Key
                                ).Count(),

                        // where shall we generate this data?
                        //yy = from u in Enumerable.Range(0, 3)
                        //     select new { z = x.EventTime.Date.AddDays(-u) },

                        xx = new[]
                        {
                            new { z = x.EventTime.Date.AddDays(1) },
                            new { z = x.EventTime.Date.AddDays(0) },
                            new { z = x.EventTime.Date.AddDays(-1) }
                        }
                    };



            var f = q.FirstOrDefault();

            // 26cc:0001 AsDataTable { ElapsedMilliseconds = 177, IsAttached = True, caller = AsDataTable<TElement> at offset 59 in file:line:column X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\QueryStrategyOfTRowExtensions.AsDataTable.cs:159:13
            var dt = q.AsDataTable();

            //var qa = q.ToArray();

            Debugger.Break();

        }


    }
}
