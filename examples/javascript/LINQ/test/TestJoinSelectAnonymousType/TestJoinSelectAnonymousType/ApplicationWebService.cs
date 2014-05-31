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

            /*
            select 0 as foo,
         s.`u0_kkey` as `kkey`,
         s.`x` as `x`,
         s.`u0_kpath` as `kpath`,
         s.`u1_upath` as `upath`,
         s.`u2_uupath` as `uupath`,
         s.`u3_u3path` as `u3path`,
         s.`u4_u4path` as `u4path`,
         s.`u5_u5path` as `u5path`
from(select 0 as foo,
                 <> h__TransparentIdentifier4 as `<> h__TransparentIdentifier4`,
                 @arg0 as `x`
        from(select 0 as foo,
                         <> h__TransparentIdentifier3 as `<> h__TransparentIdentifier3`,
                         u5 as `u5`
                from(select 0 as foo,
                                 <> h__TransparentIdentifier2 as `<> h__TransparentIdentifier2`,
                                 u4 as `u4`
                        from(select 0 as foo,
                                         <> h__TransparentIdentifier1 as `<> h__TransparentIdentifier1`,
                                         u3 as `u3`
                                from(select 0 as foo,
                                                 <> h__TransparentIdentifier0 as `<> h__TransparentIdentifier0`,
                                                 u2 as `u2`
                                        from(select 0 as foo,
                                                         u0 as `u0`,
                                                         u1 as `u1`
                                                from(select `Key`, `name`, `path`, `entryType`, `duration`, `startTime`, `connectStart`, `connectEnd`, `requestStart`, `responseStart`, `responseEnd`, `ApplicationPerformance`, `Tag`, `Timestamp`
                                                        from `PerformanceResourceTimingData2.ApplicationResourcePerformance`
                                                         where `requestStart` > @where0
                                                        ) as u0 inner join `PerformanceResourceTimingData2.ApplicationPerformance` as u1
                                                on u0.`requestStart` = u1.`requestStart`
                                                ) as __h__TransparentIdentifier0 inner join `PerformanceResourceTimingData2.ApplicationPerformance` as u2
                                        on __h__TransparentIdentifier0.`u1_requestStart` = u2.`requestStart`
                                        ) as __h__TransparentIdentifier1 inner join `PerformanceResourceTimingData2.ApplicationPerformance` as u3
                                on __h__TransparentIdentifier1.`u2_requestStart` = u3.`requestStart`
                                ) as __h__TransparentIdentifier2 inner join `PerformanceResourceTimingData2.ApplicationPerformance` as u4
                        on __h__TransparentIdentifier2.`u3_requestStart` = u4.`requestStart`
                        ) as __h__TransparentIdentifier3 inner join `PerformanceResourceTimingData2.ApplicationPerformance` as u5
                on __h__TransparentIdentifier3.`u4_requestStart` = u5.`requestStart`
                ) as s
        ) as s
        */

            var q = from u0 in new PerformanceResourceTimingData2.ApplicationResourcePerformance()
                        //where k.requestStart == 5
                    where u0.requestStart > 4

                    join u1 in new PerformanceResourceTimingData2.ApplicationPerformance() on u0.requestStart equals u1.requestStart
                    join u2 in new PerformanceResourceTimingData2.ApplicationPerformance() on u1.requestStart equals u2.requestStart
                    join u3 in new PerformanceResourceTimingData2.ApplicationPerformance() on u2.requestStart equals u3.requestStart
                    join u4 in new PerformanceResourceTimingData2.ApplicationPerformance() on u3.requestStart equals u4.requestStart
                    join u5 in new PerformanceResourceTimingData2.ApplicationPerformance() on u4.requestStart equals u5.requestStart

                    let x = 7

                    //select k.path;

                    select new
                    {
                        kkey = u0.Key,

                        x,

                        kpath = u0.path,
                        upath = u1.requestStart,
                        uupath = u2.requestStart,
                        u3path = u3.requestStart,
                        u4path = u4.requestStart,
                        u5path = u5.requestStart,
                    };


            var dt = q.AsDataTable();

            Debugger.Break();
        }

    }
}
