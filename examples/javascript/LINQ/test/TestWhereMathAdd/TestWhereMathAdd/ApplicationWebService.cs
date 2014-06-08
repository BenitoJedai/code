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

namespace TestWhereMathAdd
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
                new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { EventTime = DateTime.Now.AddDays(-1), domComplete = 5 },
                new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { EventTime = DateTime.Now.AddDays(-1), domComplete = 6 }
            );














            //            select  /* QueryStrategyOfTRowExtensions.SelectMany.cs:105 */  -- ,
            // /* QueryStrategyOfTRowExtensions.SelectMany.cs:476 */  x.`Key` as `Key`,
            // /* QueryStrategyOfTRowExtensions.SelectMany.cs:476 */  x.`EventTime` as `EventTime`,
            // /* QueryStrategyOfTRowExtensions.SelectMany.cs:476 */  x.`domComplete` as `domComplete`,
            // /* QueryStrategyOfTRowExtensions.SelectMany.cs:1183 */          y.`y` as `y`
            //from(select `Key`, `connectStart`, `connectEnd`, `requestStart`, `responseStart`, `responseEnd`, `domLoading`, `domComplete`, `loadEventStart`, `loadEventEnd`, `EventTime`, `Tag`, `Timestamp`
            //        from `PerformanceResourceTimingData2.ApplicationPerformance`
            //         where `domComplete` = @where0
            //        ) as x,
            // /* QueryStrategyOfTRowExtensions.SelectMany.cs:1582 */  (
            //    select(@argRangeFrom1 + x0.x * 10 + x1.x) as y from
            //   (select 0 as x union select 1 union select 2 union select 3 union select 4 union select 5 union select 6 union select 7 union select 8 union select 9) as x0,
            //    (select 0 as x union select 1 union select 2 union select 3 union select 4 union select 5 union select 6 union select 7 union select 8 union select 9) as x1
            //    limit @argRangeCount1
            //) as y





            // X:\jsc.svn\examples\javascript\LINQ\MashableVelocityGraph\MashableVelocityGraph\ApplicationWebService.cs

            // http://www.sqlite.org/lang_select.html#fromclause
            // http://stackoverflow.com/questions/774475/what-joins-does-sqlite-support
            var q = from x in new Data.PerformanceResourceTimingData2.ApplicationPerformance()
                    where x.domComplete + 5 == 10
                    select new
                    {
                        c1 = "hi",

                        z = x.domComplete + 5,

                        u = (
                            from xx in new Data.PerformanceResourceTimingData2.ApplicationPerformance()
                            // next lets try to select from a group
                            where xx.domComplete - 1 == x.domComplete
                            select xx.Key
                        ).Count()
                    };


            // 26cc:0001 AsDataTable { ElapsedMilliseconds = 177, IsAttached = True, caller = AsDataTable<TElement> at offset 59 in file:line:column X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\QueryStrategyOfTRowExtensions.AsDataTable.cs:159:13
            var dt = q.AsDataTable();

            //var qa = q.ToArray();

            Debugger.Break();

        }


    }
}
