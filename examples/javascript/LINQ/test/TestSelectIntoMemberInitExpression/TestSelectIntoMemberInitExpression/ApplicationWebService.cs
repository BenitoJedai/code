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
using TestSelectIntoMemberInitExpression.Data;
using System.Diagnostics;

namespace TestSelectIntoMemberInitExpression
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
            // X:\jsc.svn\examples\javascript\svg\SVGNavigationTiming\SVGNavigationTiming\ApplicationWebService.cs
            // X:\jsc.svn\examples\javascript\LINQ\MinMaxAverageExperiment\MinMaxAverageExperiment\ApplicationWebService.cs
            // X:\jsc.svn\examples\javascript\LINQ\test\TestSelect\TestSelect\ApplicationWebService.cs
            // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectIntoMemberInitExpression\TestSelectIntoMemberInitExpression\ApplicationWebService.cs

            var x = new PerformanceResourceTimingData2.ApplicationResourcePerformance();

            // += ?

            x.Insert(
                new PerformanceResourceTimingData2ApplicationResourcePerformanceRow { duration = 44, path = " /zfoo/BAR/ " }
            );


            var uc = from k in new PerformanceResourceTimingData2.ApplicationResourcePerformance()
                     select new PerformanceResourceTimingData2ApplicationResourcePerformanceRow
                     {
                         duration = k.duration,
                         path = k.path
                     };

            // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Shared\Data\Diagnostics\QueryStrategyOfTRowExtensions.Select.cs



            var dt = uc.AsDataTable();

            //FillColumns { i = 13, columName = Timestamp }
            //Fill { n = Key, ft = System.Int32, value = 1, valueType = System.Int32 }

            //            select 0 as Key,
            //         '' as name,
            //         '' as entryType,
            //         0 as startTime,
            //         0 as connectStart,
            //         0 as connectEnd,
            //         0 as requestStart,
            //         0 as responseStart,
            //         0 as responseEnd,
            //         0 as ApplicationPerformance,
            //         '' as Tag,
            //         0 as Timestamp,
            //         k.duration as duration,
            //         k.path as path
            //from `PerformanceResourceTimingData2.ApplicationResourcePerformance` as k


            Debugger.Break();
        }

    }
}
