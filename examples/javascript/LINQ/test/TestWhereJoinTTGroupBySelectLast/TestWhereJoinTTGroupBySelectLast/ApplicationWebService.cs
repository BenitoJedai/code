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


        }

    }
}
