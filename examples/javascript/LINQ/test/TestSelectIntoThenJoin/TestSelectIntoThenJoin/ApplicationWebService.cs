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
using TestSelectIntoThenJoin.Data;
using System.Diagnostics;

namespace TestSelectIntoThenJoin
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
            // x:\jsc.svn\examples\javascript\linq\test\testselectwheretolowercontains\testselectwheretolowercontains\applicationwebservice.cs




            var x = new PerformanceResourceTimingData2.ApplicationResourcePerformance();

            // += ?

            x.Insert(
                new PerformanceResourceTimingData2ApplicationResourcePerformanceRow { duration = 45, path = " /zfoo/BAR/ " }
            );


            var uc = from k in new PerformanceResourceTimingData2.ApplicationResourcePerformance()
                     select new { k.Key, k.duration, k.path };


            var ucLower = from k in new PerformanceResourceTimingData2.ApplicationResourcePerformance()
                          select new { k.Key, k.duration, path = k.path.ToLower() };

            var ucUpper = from k in new PerformanceResourceTimingData2.ApplicationResourcePerformance()
                          select new { k.Key, k.duration, path = k.path.ToUpper() };


            var ucJoin =
                    from k in uc
                        //.AsGenericEnumerable()
                    join kk in ucLower
                    //.AsGenericEnumerable() 
                    on k.Key equals kk.Key
                    //join kkk in ucUpper.AsGenericEnumerable() on k.Key equals kkk.Key

                    select new
                    {
                        k.Key,
                        k.path,
                        pathToLower = kk.path
                        //, pathToUpper = kkk.path
                    };


            var dt = ucJoin.AsDataTable();

            //var a = ucJoin.ToArray();


            Debugger.Break();
        }

    }
}
