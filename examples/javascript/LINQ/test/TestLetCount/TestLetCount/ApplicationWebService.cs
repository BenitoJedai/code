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

namespace TestLetCount
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
            {
                var k = new Data.PerformanceResourceTimingData2.ApplicationPerformance().Insert(
                    new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { Tag = " hi ", connectStart = 1, connectEnd = 2 }
                );

                new Data.PerformanceResourceTimingData2.ApplicationResourcePerformance().Insert(
                    new Data.PerformanceResourceTimingData2ApplicationResourcePerformanceRow { ApplicationPerformance = k, name = "!first", connectStart = 6 },
                    new Data.PerformanceResourceTimingData2ApplicationResourcePerformanceRow { ApplicationPerformance = k, name = "!last", connectStart = 140 }
                );
            }

            var q = from x in new Data.PerformanceResourceTimingData2.ApplicationPerformance()


                    let c = (
                        from xx in new Data.PerformanceResourceTimingData2.ApplicationPerformance()
                        select xx.Key
                    ).Count()

                    let cc = c + 1

                    select new
                    {
                        c
                    };


            // 26cc:0001 AsDataTable { ElapsedMilliseconds = 177, IsAttached = True, caller = AsDataTable<TElement> at offset 59 in file:line:column X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\QueryStrategyOfTRowExtensions.AsDataTable.cs:159:13
            var dt = q.AsDataTable();

            //var qa = q.ToArray();

            Debugger.Break();

        }


    }
}
