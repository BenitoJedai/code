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
using ScriptCoreLib.Shared.Data.Diagnostics;

namespace MashableVelocityGraph
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
        public async Task<IEnumerable<Data.VisualizationzDateToCountRow>> WebMethod2()
        {
            // http://mashable.com/2014/06/02/apple-metal-wwdc/#:eyJzIjoidCIsImkiOiJfdzdwcHFzNng4M3g1bWY5ciJ9
            // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Extensions\IDbConnectionExtensions.cs

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
            new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { EventTime = DateTime.Now.AddDays(-3) }
            );

            // get counts by day for last 5 days?

            var t0 = DateTime.Now.AddDays(0);
            var t24 = DateTime.Now.AddDays(-1);
            var t48 = DateTime.Now.AddDays(-2);
            var t72 = DateTime.Now.AddDays(-3);

            //var qq = new Data.PerformanceResourceTimingData2.ApplicationPerformance().AsGenericEnumerable();

            // how do we get an histogram?



            var qtotal = (
                from x in new Data.PerformanceResourceTimingData2.ApplicationPerformance()
                //where x.EventTime <= t48
                //where x.EventTime >= t24
                select x
             ).Count();

            // qtotal_last24 = 2
            var qtotal_last24 = (
                from x in new Data.PerformanceResourceTimingData2.ApplicationPerformance()
                //where x.EventTime <= t48
                where x.EventTime >= t24
                select x
                ).Count();

            // qtotal_last72 = 28
            var qtotal_last72 = (
                from x in new Data.PerformanceResourceTimingData2.ApplicationPerformance()
                //where x.EventTime <= t48
                where x.EventTime >= t72
                select x
                ).Count();

            // qtotal_last24to72 = 27
            var qtotal_last24to72 = (
                from x in new Data.PerformanceResourceTimingData2.ApplicationPerformance()
                where x.EventTime <= t24
                where x.EventTime >= t72
                select x
                ).Count();

            {
                Func<DateTime, DateTime, IQueryStrategy> f =
                    (x0, x24) =>
                        //(IQueryStrategy<Data.PerformanceResourceTimingData2ApplicationPerformanceRow>)
                    from x in new Data.PerformanceResourceTimingData2.ApplicationPerformance()
                    where x.EventTime <= x0
                    where x.EventTime > x24
                    select x;

                var xqtotal_last0to24 = f(t0, t24).Count();
                var xqtotal_last24to48 = f(t24, t48).Count();
                var xqtotal_last48to72 = f(t48, t72).Count();

                // this makes multiple requests in the db
                var a = from offset in Enumerable.Range(0, 4)
                        let x0 = DateTime.Now.AddDays(-offset)
                        let x24 = DateTime.Now.AddDays(-offset - 1)
                        let x = f(x0, x24).Count()
                        select x;

                var aa = a.ToArray();
            }

            {

                // http://blog.sqlauthority.com/2010/07/20/sql-server-select-from-dual-dual-equivalent/
                // do we need to select index on server?
                //var ctx =
                //    from index new Data.PerformanceResourceTimingData2<long>(Enumerable.Range(0, 4))
                //    select index;


                var a = from offset in Enumerable.Range(0, 4) // Data.PerformanceResourceTimingData2.Range

                        let x0 = DateTime.Now.AddDays(-offset)
                        let x24 = DateTime.Now.AddDays(-offset - 1)

                        let countable =
                            from x in new Data.PerformanceResourceTimingData2.ApplicationPerformance()
                            where x.EventTime <= x0
                            where x.EventTime > x24
                            select x

                        // can we do a count for all at once?
                        // http://dev.mysql.com/doc/refman/5.0/en/union.html
                        // either by union, concat our we somehow group?

                        //select y.Count();
                        select new Data.VisualizationzDateToCountRow
                        {
                            Count = countable.Count(),
                            Date = new { offset }.ToString()
                        };


                //a.First().AsGenericEnumerable

                //var m = a.Select(x => x.Count()).ToArray();
                return a;




                //var aa = a.ToArray();

                //Debugger.Break();
            }

            //Debugger.Break();
        }

    }
}
