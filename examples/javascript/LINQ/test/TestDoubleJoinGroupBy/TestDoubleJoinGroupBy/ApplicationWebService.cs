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

namespace TestDoubleJoinGroupBy
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
            //error:
            //    {
            //        Message = Object reference not set to an instance of an object., ex = System.NullReferenceException: Object reference not set to an instance of an object.
            // at System.Data.QueryStrategyOfTRowExtensions.JoinQueryStrategy`4.<> c__DisplayClass6.< Invoke > b__21(IJoinQueryStrategy yy) in X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\QueryStrategyOfTRowExtensions.Join.cs:line 689

            var x = new Data.PerformanceResourceTimingData2.ApplicationResourcePerformance();

            // += ?

            x.Insert(
                new Data.PerformanceResourceTimingData2ApplicationResourcePerformanceRow { duration = 46, path = " /zfoo/BAR/ " }
            );

            new Data.PerformanceResourceTimingData2.ApplicationPerformance().Insert(
                new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { domComplete = 46 }
            );

            var q =
                from u in new Data.PerformanceResourceTimingData2.ApplicationResourcePerformance()
                    //join uu in new Data.PerformanceResourceTimingData2.ApplicationResourcePerformance() on u.startTime equals uu.startTime
                    //join u2 in new Data.PerformanceResourceTimingData2.ApplicationPerformance() on u.duration equals u2.domComplete
                join u2 in new Data.PerformanceResourceTimingData2.ApplicationPerformance() on u.duration equals u2.domComplete
                group new
                {
                    u,
                    //uu,

                    u2
                } by u.duration into nt
                select new Data.PerformanceResourceTimingData2ApplicationResourcePerformanceRow
                {
                    path = nt.Last().u.path,
                    //duration = (long)u.Key
                    duration = (long)nt.Last().u2.Key,

                    Tag = nt.Last().u2.Tag
                };

            //var s = from i in q
            //        //group u by u.path into ggg
            //        group i by i.path into ggg
            //        //let u = g.Last()
            //        select new Data.PerformanceResourceTimingData2ApplicationResourcePerformanceRow
            //        {
            //            //path = ggg.Last().path.ToLower()
            //            path = ggg.Last().path,
            //            duration = (long)ggg.Last().duration
            //        };

            // f = {0, ,  /zfoo/BAR/ , , 1, 0, 0, 0, 0, 0, 0, 0, , 6/5/2014 5:19:55 PM}
            var f = q.FirstOrDefault();


            Debugger.Break();

        }

    }
}
