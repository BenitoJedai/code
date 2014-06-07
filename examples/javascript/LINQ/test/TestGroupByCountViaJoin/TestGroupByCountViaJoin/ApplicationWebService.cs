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

namespace TestGroupByCountViaJoin
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
                new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { EventTime = DateTime.Now.AddDays(-1), domComplete = 5, connectStart = 5 }
            );






            var qElementsInGroup = from x in new Data.PerformanceResourceTimingData2.ApplicationPerformance()

                                   group x by new { x.domComplete } into g
                                   //group x by new { x.offset } into g
                                   select new
                                   {
                                       g.Key,

                                       ElementsInGroup = g.Count(),

                                       // special?
                                       //ZElementsInGroup = g.Count(y => y.connectStart > 1),

                                       LastKey = g.Last().Key
                                   };


            var qZElementsInGroup = from x in new Data.PerformanceResourceTimingData2.ApplicationPerformance()

                                        // this came from a group then select scalar count?
                                    where x.connectStart > 1

                                    group x by new { x.domComplete } into g
                                    //group x by new { x.offset } into g
                                    select new
                                    {
                                        g.Key,


                                        //ElementsInGroup = g.Count(),

                                        // special?
                                        ZElementsInGroup = g.Count(),

                                        //LastKey = g.Last().Key
                                    };

            var qElementsInGroup0 = qElementsInGroup.AsDataTable();
            var qZElementsInGroup0 = qElementsInGroup.AsDataTable();

            // X:\jsc.svn\examples\javascript\linq\test\TestGroupByCount\TestGroupByCount\ApplicationWebService.cs


            var q = from xx in qElementsInGroup
                        // what if there are empty groups?
                        // we wont be seeing them will we.
                    join y in qZElementsInGroup on xx.Key equals y.Key
                    select new
                    {
                        xx.Key,

                        y.ZElementsInGroup,

                        xx.ElementsInGroup,
                        xx.LastKey,
                    };



            // 26cc:0001 AsDataTable { ElapsedMilliseconds = 177, IsAttached = True, caller = AsDataTable<TElement> at offset 59 in file:line:column X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\QueryStrategyOfTRowExtensions.AsDataTable.cs:159:13
            var dt = q.AsDataTable();

            //var qa = q.ToArray();

            Debugger.Break();

        }


    }
}
