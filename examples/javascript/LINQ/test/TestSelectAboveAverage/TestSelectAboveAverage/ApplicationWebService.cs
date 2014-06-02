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

namespace TestSelectAboveAverage
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
            // !!! what about securestring?

            // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectOfSelect\TestSelectOfSelect\ApplicationWebService.cs
            // x:\jsc.svn\examples\javascript\linq\test\testselectandsubselect\testselectandsubselect\applicationwebservice.cs
            // x:\jsc.svn\examples\javascript\linq\test\testselectscalaraverage\testselectscalaraverage\applicationwebservice.cs

            {
                var k = new Data.PerformanceResourceTimingData2.ApplicationPerformance().Insert(
                    new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { Tag = " hi ", connectStart = 1, connectEnd = 2 }
                );

                new Data.PerformanceResourceTimingData2.ApplicationResourcePerformance().Insert(
                    new Data.PerformanceResourceTimingData2ApplicationResourcePerformanceRow { ApplicationPerformance = k, name = "!first", connectStart = 6 },
                    new Data.PerformanceResourceTimingData2ApplicationResourcePerformanceRow { ApplicationPerformance = k, name = "!first1", connectStart = 3 },
                    new Data.PerformanceResourceTimingData2ApplicationResourcePerformanceRow { ApplicationPerformance = k, name = "!first2", connectStart = 133 },
                    new Data.PerformanceResourceTimingData2ApplicationResourcePerformanceRow { ApplicationPerformance = k, name = "!last", connectStart = 140 }
                );
            }


            var uc = from k in new Data.PerformanceResourceTimingData2.ApplicationResourcePerformance()

                     let avg = (
                            from kk in new Data.PerformanceResourceTimingData2.ApplicationResourcePerformance()
                            select kk.connectStart
                         ).Average()

                     where k.connectStart >= avg

                     select new
                     {

                         avg,

                         k = new { k.name, k.connectStart }


                         // cannot just yet select all?
                         //k
                     };


            var data = uc.AsDataTable();
            var first = uc.FirstOrDefault();

            Debugger.Break();
        }

    }
}
