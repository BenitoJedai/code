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

            var temp = from k in new PerformanceResourceTimingData2.ApplicationResourcePerformance()
                       where k.duration == 45
                       select k;

            var q = from k in temp

                    join u in new PerformanceResourceTimingData2.ApplicationResourcePerformance() on k.duration equals u.duration

                    //join uu in new PerformanceResourceTimingData2.ApplicationResourcePerformance() on k.duration equals uu.duration

                    join uu in new PerformanceResourceTimingData2.ApplicationResourcePerformance() on u.duration equals uu.duration
                    join uuu in new PerformanceResourceTimingData2.ApplicationResourcePerformance() on uu.duration equals uuu.duration
                    join uuuu in new PerformanceResourceTimingData2.ApplicationResourcePerformance() on uuu.duration equals uuuu.duration
                    join uuuuu in new PerformanceResourceTimingData2.ApplicationResourcePerformance() on uuuu.duration equals uuuuu.duration

                    // does not yet work does it.

                    group k by k.duration into g

                    select new
                    {
                        g.Key,


                        path = g.Last().path
                    };

            var dt = q.AsDataTable();


            Debugger.Break();
        }

    }
}
