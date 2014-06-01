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
using TestSelectOfSelect.Data;
using System.Diagnostics;

namespace TestSelectOfSelect
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
            // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectOfSelect\TestSelectOfSelect\ApplicationWebService.cs

            var x = new PerformanceResourceTimingData2.ApplicationResourcePerformance();

            // += ?

            x.Insert(
                new PerformanceResourceTimingData2ApplicationResourcePerformanceRow { duration = 45, connectStart = 49, path = " /zfoo/BAR/ " }
            );

            x.Insert(
                new PerformanceResourceTimingData2ApplicationResourcePerformanceRow { duration = 49, path = " /zfoo/BAR/ selected by scalar selector first " }
            );

            x.Insert(
              new PerformanceResourceTimingData2ApplicationResourcePerformanceRow { duration = 49, path = " /zfoo/BAR/ selected by scalar selector last " }
          );


            // x:\jsc.svn\examples\javascript\linq\test\testselectandsubselect\testselectandsubselect\applicationwebservice.cs
            // http://blog.tanelpoder.com/2013/08/22/scalar-subqueries-in-oracle-sql-where-clauses-and-a-little-bit-of-exadata-stuff-too/


            //  from k in new PerformanceResourceTimingData2 
            // could allow to query on meta and stats?
            // what about access control and logging?
            // what about saving stacktrace?
            // on every acccess?
            // with full debugging details?
            // intellitrace way?
            // with locals visible?

            var uc = from k in new PerformanceResourceTimingData2.ApplicationResourcePerformance()
                     select new
                     {
                         k.path,

                         // wont show up for AsDataTable as more queries need to be made?
                         other = (
                            // Error	1	An expression tree may not contain a call or invocation that uses optional arguments	X:\jsc.svn\examples\javascript\LINQ\test\TestSelectOfSelect\TestSelectOfSelect\ApplicationWebService.cs	58	40	TestSelectOfSelect

                            //from kk in new PerformanceResourceTimingData2.ApplicationResourcePerformance("file:PerformanceResourceTimingData2.xlsx.sqlite")
                            from kk in new PerformanceResourceTimingData2.ApplicationResourcePerformance()
                                //where kk.duration == 47
                            where kk.duration == k.connectStart
                            //orderby kk.Key descending
                            select kk.path

                            // either first or last?
                         ).FirstOrDefault()

                     };

            var dt = uc.AsDataTable();


            var ff = uc.FirstOrDefault();

            Debugger.Break();
        }

    }
}
