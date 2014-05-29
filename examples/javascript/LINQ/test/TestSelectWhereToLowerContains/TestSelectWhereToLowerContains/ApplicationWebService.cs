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
using TestSelectWhereToLowerContains.Data;
using System.Diagnostics;

namespace TestSelectWhereToLowerContains
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
        public  void WebMethod2(string ff = "bar")
        {
            // X:\jsc.svn\examples\javascript\svg\SVGNavigationTiming\SVGNavigationTiming\ApplicationWebService.cs
            // X:\jsc.svn\examples\javascript\LINQ\MinMaxAverageExperiment\MinMaxAverageExperiment\ApplicationWebService.cs
            // X:\jsc.svn\examples\javascript\LINQ\test\TestSelect\TestSelect\ApplicationWebService.cs
            // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectIntoMemberInitExpression\TestSelectIntoMemberInitExpression\ApplicationWebService.cs
            // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectIntoNewExpression\TestSelectIntoNewExpression\ApplicationWebService.cs
            // X:\jsc.svn\examples\javascript\LINQ\test\TestWhereSelect\TestWhereSelect\ApplicationWebService.cs
            // x:\jsc.svn\examples\javascript\linq\test\testselectwheretolowercontains\testselectwheretolowercontains\applicationwebservice.cs

            var x = new PerformanceResourceTimingData2.ApplicationResourcePerformance();

            // += ?

            x.Insert(
                new PerformanceResourceTimingData2ApplicationResourcePerformanceRow { duration = 45, path = " /zfoo/BAR/ " }
            );



            //var ff = "bar";
            //var z = new { ff };


            var uc = from k in new PerformanceResourceTimingData2.ApplicationResourcePerformance()
                     where k.path.ToLower().Contains(
                         //z.ff
                         ff
                         //"bar"
                         )
                     //where k.path.Contains("BAR")
                     select k;

            var dt = uc.AsDataTable();

            Debugger.Break();
        }

    }
}
