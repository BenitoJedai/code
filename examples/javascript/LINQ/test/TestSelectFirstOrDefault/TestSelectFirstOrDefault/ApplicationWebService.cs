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

namespace TestSelectFirstOrDefault
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
            var x = new Data.PerformanceResourceTimingData2.ApplicationResourcePerformance();

            // += ?

            x.Insert(
                new Data.PerformanceResourceTimingData2ApplicationResourcePerformanceRow { duration = 45, path = " /zfoo/BAR/ " }
            );

            // getting this to work was the first step into enabling reflection baking
            // we will be figting jvm type erasure, enum erasure and js type erasure,
            var f = x.FirstOrDefault();



            Debugger.Break();
        }

    }
}
