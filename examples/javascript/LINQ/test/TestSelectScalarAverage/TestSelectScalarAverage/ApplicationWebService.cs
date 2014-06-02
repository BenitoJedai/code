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
using System.Diagnostics;

namespace TestSelectScalarAverage
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
            // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectOfSelect\TestSelectOfSelect\ApplicationWebService.cs
            // x:\jsc.svn\examples\javascript\linq\test\testselectandsubselect\testselectandsubselect\applicationwebservice.cs
            // x:\jsc.svn\examples\javascript\linq\test\testselectscalaraverage\testselectscalaraverage\applicationwebservice.cs

            {
                var k = new Data.PerformanceResourceTimingData2.ApplicationPerformance().Insert(
                    new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { Tag = " hi ", connectStart = 1, connectEnd = 2 }
                );

                new Data.PerformanceResourceTimingData2.ApplicationResourcePerformance().Insert(
                    new Data.PerformanceResourceTimingData2ApplicationResourcePerformanceRow { ApplicationPerformance = k, name = "!first", connectStart = 6 },
                    new Data.PerformanceResourceTimingData2ApplicationResourcePerformanceRow { ApplicationPerformance = k, name = "!last", connectStart = 140 }
                );
            }

            var kall = new Data.PerformanceResourceTimingData2.ApplicationPerformance().AsDataTable();
            var all = new Data.PerformanceResourceTimingData2.ApplicationResourcePerformance().AsDataTable();


            var uc = from k in new Data.PerformanceResourceTimingData2.ApplicationPerformance()

                     let avg = (
                            from kk in new Data.PerformanceResourceTimingData2.ApplicationResourcePerformance()
                            where kk.ApplicationPerformance == k.Key
                            select kk.connectStart
                         ).Average()
                     // selec string bufffer?


                     let count = (
                             from kk in new Data.PerformanceResourceTimingData2.ApplicationResourcePerformance()
                             where kk.ApplicationPerformance == k.Key
                             select kk.connectStart
                          ).Count()


                     let c = new { count }

                     where count > 1
                     //where count > 2

                     select new
                     {
                         k.Key,

                         //w = new StringBuilder(
                         k.Tag,

                         //avg,

                         c,

                         count,

                         //count = (
                         //   from kk in new Data.PerformanceResourceTimingData2.ApplicationResourcePerformance()
                         //   where kk.ApplicationPerformance == k.Key
                         //   select kk.connectStart
                         //).Count(),

                         avg = (
                            from kk in new Data.PerformanceResourceTimingData2.ApplicationResourcePerformance()
                            where kk.ApplicationPerformance == k.Key
                            select kk.connectStart
                         ).Average(),

                         first = (
                            from kk in new Data.PerformanceResourceTimingData2.ApplicationResourcePerformance()
                            where kk.ApplicationPerformance == k.Key
                            select kk.name
                         ).FirstOrDefault(),

                         last = (
                            from kk in new Data.PerformanceResourceTimingData2.ApplicationResourcePerformance()
                            where kk.ApplicationPerformance == k.Key
                            orderby kk.Key descending
                            select kk.name
                         ).FirstOrDefault()

                     } into g
                     orderby g.avg descending
                     select g;



            var dt = uc.AsDataTable();


            var ff = uc.FirstOrDefault();

            Debugger.Break();
        }

    }
}
