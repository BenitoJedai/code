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
using TestWhereSelect.Data;
using System.Diagnostics;

namespace TestWhereSelect
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
            // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectIntoNewExpression\TestSelectIntoNewExpression\ApplicationWebService.cs
            // X:\jsc.svn\examples\javascript\LINQ\test\TestWhereSelect\TestWhereSelect\ApplicationWebService.cs





            var x = new PerformanceResourceTimingData2.ApplicationResourcePerformance();

            // += ?

            x.Insert(
                new PerformanceResourceTimingData2ApplicationResourcePerformanceRow { duration = 45, path = " /zfoo/BAR/ " }
            );


            var uc = from k in new PerformanceResourceTimingData2.ApplicationResourcePerformance()
                     where k.duration == 45
                     orderby k.path
                     select k;

            //            select `Key`, `name`, `path`, `entryType`, `duration`, `startTime`, `connectStart`, `connectEnd`, `requestStart`, `responseStart`, `responseEnd`, `ApplicationPerformance`, `Tag`, `Timestamp`
            //from `PerformanceResourceTimingData2.ApplicationResourcePerformance`
            // where `duration` = @where0


            //select new
            //{
            //    duration = k.duration,
            //    path = k.path
            //};

            // Error	251	Could not find an implementation of the query pattern for source type 'ScriptCoreLib.Shared.Data.Diagnostics.IQueryStrategy<<anonymous type: long duration, string path>>'.  'SelectMany' not found.	X:\jsc.svn\examples\javascript\LINQ\test\TestSelectIntoNewExpression\TestSelectIntoNewExpression\ApplicationWebService.cs	48	32	TestSelectIntoNewExpression
            //into g join z in  new PerformanceResourceTimingData2.ApplicationResourcePerformance() on g.path equals z.path
            //select new { z, g }

            //from kk in new PerformanceResourceTimingData2.ApplicationResourcePerformance()
            //select kk


            ;

            // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Shared\Data\Diagnostics\QueryStrategyOfTRowExtensions.Select.cs



            var dt = uc.AsDataTable();

            //FillColumns { i = 13, columName = Timestamp }
            //Fill { n = Key, ft = System.Int32, value = 1, valueType = System.Int32 }

            //            select 0 as foo,
            //         s.`duration` as `duration`,
            //         s.`path` as `path`
            //from `PerformanceResourceTimingData2.ApplicationResourcePerformance` as s


            Debugger.Break();
        }

    }
}
