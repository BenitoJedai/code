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
using TestSelect.Data;
using System.Diagnostics;

namespace TestSelect
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

            var x = new PerformanceResourceTimingData2.ApplicationResourcePerformance();

            x.Insert(
                new PerformanceResourceTimingData2ApplicationResourcePerformanceRow { duration = 44, path = " /zfoo/BAR/ " }
            );


            var uc = from k in new PerformanceResourceTimingData2.ApplicationResourcePerformance()
                     select k;

            // +		that.selector	{k => k}	System.Linq.Expressions.Expression<System.Func<TestSelect.Data.PerformanceResourceTimingData2ApplicationResourcePerformanceRow,TestSelect.Data.PerformanceResourceTimingData2ApplicationResourcePerformanceRow>>
            // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Shared\Data\Diagnostics\QueryStrategyOfTRowExtensions.Select.cs


            //error:
            //    {
            //        Message = Object reference not set to an instance of an object., ex = System.NullReferenceException: Object reference not set to an instance of an object.
            // at System.Data.QueryStrategyOfTRowExtensions.<> c__DisplayClass0`2.< Select > b__8(CommandBuilderState state) in X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Shared\Data\Diagnostics\QueryStrategyOfTRowExtensions.Select.cs:line 732

            var dt = uc.AsDataTable();

            //FillColumns { i = 13, columName = Timestamp }
            //Fill { n = Key, ft = System.Int32, value = 1, valueType = System.Int32 }

            Debugger.Break();
        }

    }
}
