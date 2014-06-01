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

            {
                var k = new Data.PerformanceResourceTimingData2.ApplicationPerformance().Insert(
                    new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { Tag = " hi ", connectStart = 1, connectEnd = 2 }
                );

                new Data.PerformanceResourceTimingData2.ApplicationResourcePerformance().Insert(
                    new Data.PerformanceResourceTimingData2ApplicationResourcePerformanceRow { ApplicationPerformance = k, name = "!first" },
                    new Data.PerformanceResourceTimingData2ApplicationResourcePerformanceRow { ApplicationPerformance = k, name = "!last" }
                );
            }


            var uc = from k in new Data.PerformanceResourceTimingData2.ApplicationPerformance()
                     select new
                     {
                         k.Tag,

                         first = (
                            from kk in new Data.PerformanceResourceTimingData2.ApplicationResourcePerformance()
                            where kk.ApplicationPerformance == k.Key
                            select kk.path
                         ).FirstOrDefault(),

                         last = (
                            from kk in new Data.PerformanceResourceTimingData2.ApplicationResourcePerformance()
                            where kk.ApplicationPerformance == k.Key
                            orderby kk.Key descending
                            select kk.path
                         ).FirstOrDefault()

                     };

            var dt = uc.AsDataTable();


            var ff = uc.FirstOrDefault();

            Debugger.Break();
        }

    }
}
