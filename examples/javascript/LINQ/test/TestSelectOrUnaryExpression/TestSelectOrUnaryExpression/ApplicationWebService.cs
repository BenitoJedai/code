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
using TestSelectOrUnaryExpression.Data;
using System.Diagnostics;

namespace TestSelectOrUnaryExpression
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
        public void WebMethod2(long test = 23, PerformanceResourceTimingData2ApplicationResourcePerformanceKey ke = (PerformanceResourceTimingData2ApplicationResourcePerformanceKey) 6)
        {
            var x = new PerformanceResourceTimingData2.ApplicationResourcePerformance();


            x.Insert(
                new PerformanceResourceTimingData2ApplicationResourcePerformanceRow { duration = 45, startTime = 1 }
            );
            x.Insert(
               new PerformanceResourceTimingData2ApplicationResourcePerformanceRow { duration = 20, startTime = 23 }
           );


            var uc = from k in new PerformanceResourceTimingData2.ApplicationResourcePerformance()
                     where k.Key == ke || k.startTime == test
                     select k;

            var dt = uc.AsDataTable();


            Debugger.Break();
        }

    }
}
