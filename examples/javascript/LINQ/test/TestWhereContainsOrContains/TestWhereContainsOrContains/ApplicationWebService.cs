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

namespace TestWhereContainsOrContains
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        /// <summary>
        /// The static content defined in the HTML file will be update to the dynamic content once application is running.
        /// </summary>
        public XElement Header = new XElement(@"h1", @"JSC - The .NET crosscompiler for web platforms. ready.");

        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2()
        {
            //new Data.PerformanceResourceTimingData2.ApplicationPerformance().Clear();
            new Data.PerformanceResourceTimingData2.ApplicationResourcePerformance().Insert(
                         new Data.PerformanceResourceTimingData2ApplicationResourcePerformanceRow { path = "abC" }
                     );





            // X:\jsc.svn\examples\javascript\LINQ\MashableVelocityGraph\MashableVelocityGraph\ApplicationWebService.cs

            // http://www.sqlite.org/lang_select.html#fromclause
            // http://stackoverflow.com/questions/774475/what-joins-does-sqlite-support

            //            select `Key`, `name`, `path`, `entryType`, `duration`, `startTime`, `connectStart`, `connectEnd`, `requestStart`, `responseStart`, `responseEnd`, `ApplicationPerformance`, `Tag`, `Timestamp`
            //from `PerformanceResourceTimingData2.ApplicationResourcePerformance`
            // where not(`path` is null or length(`path`) = 0)

            // x:\jsc.svn\examples\javascript\linq\test\testselectwheretolowercontains\testselectwheretolowercontains\applicationwebservice.cs


            var z = new { u = new { n = "C" } };

            var q = from x in new Data.PerformanceResourceTimingData2.ApplicationResourcePerformance()
                    where x.path.ToLower().Contains(z.u.n.ToLower()) || (x.path.Contains("a") && x.path.Contains("b"))
                    //where !x.path.ToUpper().Contains("C")
                    //select !x.path.ToUpper().Contains("C");
                    //select !x.path.ToLower().Contains(z.u.n.ToLower());
                    select x;

            var dt = q.AsDataTable();

            // f = true
            // f = false
            var f = q.FirstOrDefault();

            // 26cc:0001 AsDataTable { ElapsedMilliseconds = 177, IsAttached = True, caller = AsDataTable<TElement> at offset 59 in file:line:column X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\QueryStrategyOfTRowExtensions.AsDataTable.cs:159:13

            //var qa = q.ToArray();

            Debugger.Break();

        }


    }
}
