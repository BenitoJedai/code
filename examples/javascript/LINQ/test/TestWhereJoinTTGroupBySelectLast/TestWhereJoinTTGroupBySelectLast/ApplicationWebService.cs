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
using TestWhereJoinTTGroupBySelectLast.Data;

namespace TestWhereJoinTTGroupBySelectLast
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

            var x = new PerformanceResourceTimingData2.ApplicationResourcePerformance();

            // += ?

            x.Insert(
                new PerformanceResourceTimingData2ApplicationResourcePerformanceRow { duration = 45, path = " /zfoo/BAR/ " }
            );



            var q = from u0 in new PerformanceResourceTimingData2.ApplicationResourcePerformance()

                    join u1 in new PerformanceResourceTimingData2.ApplicationResourcePerformance() on u0.duration equals u1.duration
                    join u2 in new PerformanceResourceTimingData2.ApplicationResourcePerformance() on u1.duration equals u2.duration
                    join u3 in new PerformanceResourceTimingData2.ApplicationResourcePerformance() on u2.duration equals u3.duration
                    join u4 in new PerformanceResourceTimingData2.ApplicationResourcePerformance() on u3.duration equals u4.duration
                    join u5 in new PerformanceResourceTimingData2.ApplicationResourcePerformance() on u4.duration equals u5.duration

                    // whats not selected into new group is lost?
                    // are we propagating the group by key selector yet?
                    group new { u0, u1, u5 } by u5.duration into g

                    select new
                    {
                        g.Key,


                        //last_u0_path = g.Last().u0.path,
                        //last_u1_path = g.Last().u1.path.ToUpper(),
                        //last_u1_path = g.Last().u1.path.ToLower(),
                        last_u0_path = g.Last().u0.path.ToLower(),

                        //firstpath = g.First().u.path
                    };

            var dt = q.Take(5).AsDataTable();


            Debugger.Break();
        }

    }
}
