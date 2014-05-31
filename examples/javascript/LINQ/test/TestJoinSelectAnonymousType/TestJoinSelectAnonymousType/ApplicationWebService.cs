using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestJoinSelectAnonymousType.Data;
using System.Diagnostics;

namespace TestJoinSelectAnonymousType
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2()
        {
            //X:\jsc.svn\examples\javascript\LINQ\test\TestWhereJoinTTGroupBySelectLast\TestWhereJoinTTGroupBySelectLast\ApplicationWebService.cs


            new PerformanceResourceTimingData2.ApplicationResourcePerformance().Insert(
                new PerformanceResourceTimingData2ApplicationResourcePerformanceRow
            {
                requestStart = 5,
                path = " /zfoo/BAR/ "
            }
            );


            new PerformanceResourceTimingData2.ApplicationPerformance().Insert(
                new PerformanceResourceTimingData2ApplicationPerformanceRow
            {
                requestStart = 5
            }
            );



            //var temp = from k in new PerformanceResourceTimingData2.ApplicationResourcePerformance()
            //           where k.requestStart == 5
            //           select k;



            //            select 0 as foo,
            //         uu.`requestStart` as `uupath`
            //from(select 0 as foo,
            //                 k as `k`,
            //                 u as `u`
            //        from(select `Key`, `name`, `path`, `entryType`, `duration`, `startTime`, `connectStart`, `connectEnd`, `requestStart`, `responseStart`, `responseEnd`, `ApplicationPerformance`, `Tag`, `Timestamp`
            //                from `PerformanceResourceTimingData2.ApplicationResourcePerformance`
            //                 where `requestStart` > @where0
            //                ) as k inner join `PerformanceResourceTimingData2.ApplicationPerformance` as u
            //        on k.`requestStart` = u.`requestStart`
            //        ) as __h__TransparentIdentifier0 inner join `PerformanceResourceTimingData2.ApplicationPerformance` as uu
            //on __h__TransparentIdentifier0.`u_requestStart` = uu.`requestStart`

            var q = from u0 in new PerformanceResourceTimingData2.ApplicationResourcePerformance()
                        //where k.requestStart == 5
                    where u0.requestStart > 4

                    join u1 in new PerformanceResourceTimingData2.ApplicationPerformance() on u0.requestStart equals u1.requestStart
                    join u2 in new PerformanceResourceTimingData2.ApplicationPerformance() on u1.requestStart equals u2.requestStart
                    join u3 in new PerformanceResourceTimingData2.ApplicationPerformance() on u2.requestStart equals u3.requestStart
                    join u4 in new PerformanceResourceTimingData2.ApplicationPerformance() on u3.requestStart equals u4.requestStart
                    join u5 in new PerformanceResourceTimingData2.ApplicationPerformance() on u4.requestStart equals u5.requestStart

                    //select k.path;

                    select new
                    {
                        kkey = k.Key,

                        kpath = k.path,
                        upath = u.requestStart,
                        uupath = uu.requestStart,
                        u3path = u3.requestStart,
                        u4path = u4.requestStart,
                    };


            var dt = q.AsDataTable();

            Debugger.Break();
        }

    }
}
