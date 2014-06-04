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

namespace TestGroupByMultipleFields
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
            // X:\jsc.svn\examples\javascript\LINQ\MashableVelocityGraph\MashableVelocityGraph\ApplicationWebService.cs

            var x = new Data.PerformanceResourceTimingData2.ApplicationResourcePerformance();

            // += ?

            x.Insert(
                new Data.PerformanceResourceTimingData2ApplicationResourcePerformanceRow { duration = 47, path = " /zfoo/BAR/ " }
            );

            x.Insert(
                new Data.PerformanceResourceTimingData2ApplicationResourcePerformanceRow { duration = 46, path = " /zfoo/BAR/ " }
            );

            x.Insert(
                new Data.PerformanceResourceTimingData2ApplicationResourcePerformanceRow { duration = 46, path = " /zfoo/BAR/X " }
            );

            x.Insert(
                new Data.PerformanceResourceTimingData2ApplicationResourcePerformanceRow { duration = 46, path = " /zfoo/BAR/ " }
            );


            // Error	1	'ScriptCoreLib.Shared.Data.Diagnostics.IQueryStrategy<<anonymous type: <anonymous type: long duration, string path> Key, long Count>>' does not contain a definition for 'AsEnumerable' and the best extension method overload 'ScriptCoreLib.Extensions.DataExtensions.AsEnumerable(System.Data.DataRowCollection)' requires a receiver of type 'System.Data.DataRowCollection'	X:\jsc.svn\examples\javascript\linq\test\TestGroupByMultipleFields\TestGroupByMultipleFields\ApplicationWebService.cs	61	22	TestGroupByMultipleFields


            //            select-- diagnostics,
            //         __h__TransparentIdentifier0.`du` as `du`,
            //         __h__TransparentIdentifier0.`Key` as `Key`,
            //         g. `Count` as `Count`
            //from(select-- diagnostics,
            //                 g.Key as `Key`,
            //                 g.`y_duration` as `du`
            //        from(select g.`Grouping.Key`,
            //                         g.`y_duration`
            //                from(
            //                        select s.`path` as `Grouping.Key`,
            //                                 s.`y_duration`
            //                         from `PerformanceResourceTimingData2.ApplicationResourcePerformance` as s
            //                         group by `Grouping.Key`
            //                ) as g
            //                ) as g
            //        ) as __h__TransparentIdentifier0


            var zz = from y in new Data.PerformanceResourceTimingData2.ApplicationResourcePerformance()
                         //.AsGenericEnumerable()
                         //group new { y } by new { y.duration, y.path, y.connectStart } into gg
                         //group new { y } by new { y.duration, y.path } into gg
                     //group new { y } by new { y.path } into gg
                     group new { y } by y.path into gg


                     //let du = gg.Last().duration
                     //let xCount = gg.Count()
                     select new
                     {
                         //du = gg.Last().duration,

                         // keep the keys?
                         //du = gg.Last().y.duration,
                         //pa = gg.Last().y.path,

                         //du,

                         gg.Key,
                         //.duration, g.Key.path,
                         xCount = gg.Count()
                         //xCount
                     };

            var r = zz.ToArray();

            var zz0 = zz.AsGenericEnumerable();

            //var z = from y in new Data.PerformanceResourceTimingData2.ApplicationResourcePerformance()
            //            //.AsGenericEnumerable()
            //            //group new { y } by new { y.duration, y.path } into g
            //        group new { y } by y.path into gg

            //        let du = gg.Last().y.duration

            //        select new
            //        {
            //            du,

            //            gg.Key,
            //            //.duration, g.Key.path,
            //            Count = gg.Count()
            //        };

            //var z0 = z.AsGenericEnumerable();
            //var zz = z.AsEnumerable();

            Debugger.Break();

        }


    }
}
