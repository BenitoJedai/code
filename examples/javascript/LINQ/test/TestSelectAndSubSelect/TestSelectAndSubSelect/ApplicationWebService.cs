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
using ScriptCoreLib.Shared.Data.Diagnostics;

namespace TestSelectAndSubSelect
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
            // PerformanceResourceTimingData2ApplicationPerformance Context has key and table to insert to?
            var k = new Data.PerformanceResourceTimingData2.ApplicationPerformance().Insert(
                new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { Tag = " hi ", connectStart = 1, connectEnd = 2 }
            );

            var kk = new Data.PerformanceResourceTimingData2.ApplicationResourcePerformance().Insert(
                new Data.PerformanceResourceTimingData2ApplicationResourcePerformanceRow { ApplicationPerformance = k, name = "!" }
            );

            //            select 'Select' as diagnostics,
            //         __h__TransparentIdentifier0.`z` as `z`,
            //         __h__TransparentIdentifier0.`x_Tag` as `Tag`,
            //         __h__TransparentIdentifier0.`x_connectStart` as `connect.connectStart`,
            //         __h__TransparentIdentifier0.`x_connectEnd` as `connect.connectEnd`
            //from(select 'Select' as diagnostics,
            //                 @arg0 as `z`
            //        from `PerformanceResourceTimingData2.ApplicationPerformance` as x
            //        ) as __h__TransparentIdentifier0
            //limit @arg1




            // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectOfSelect\TestSelectOfSelect\ApplicationWebService.cs
            Func<Data.PerformanceResourceTimingData2ApplicationPerformanceKey , IQueryStrategy<Data.PerformanceResourceTimingData2ApplicationResourcePerformanceRow>> child1 =
                x =>
                    //from xx in new Data.PerformanceResourceTimingData2.ApplicationResourcePerformance("file:PerformanceResourceTimingData2.xlsx.sqlite")
                    from xx in new Data.PerformanceResourceTimingData2.ApplicationResourcePerformance()
                    where xx.ApplicationPerformance == x
                    select xx;



            var q = from x in new Data.PerformanceResourceTimingData2.ApplicationPerformance()
                    let zzz = "!"
                    let zz = "???"
                    //let qq = new { u = "!!!", x.Tag }

                    //let qq = new object()

                    // Error	285	An expression tree may not contain a call or invocation that uses optional arguments	X:\jsc.svn\examples\javascript\linq\test\TestSelectAndSubSelect\TestSelectAndSubSelect\ApplicationWebService.cs	54	30	TestSelectAndSubSelect
                    // Error	267	An expression tree may not contain a named argument specification	X:\jsc.svn\examples\javascript\linq\test\TestSelectAndSubSelect\TestSelectAndSubSelect\ApplicationWebService.cs	57	30	TestSelectAndSubSelect
                    // !!! LINQ makes us send the arg to sql server. shall we use securestring to mark it as non sendable?
                    //let qq = (from xx in new Data.PerformanceResourceTimingData2.ApplicationResourcePerformance("file:PerformanceResourceTimingData2.xlsx.sqlite")
                    //          select xx).FirstOrDefault()

                    // !!! by moveing the other query out of current stack we shall be able to call it
                    //let qq = child1(x).FirstOrDefault()
                    let qq = child1(x.Key).FirstOrDefault()

                    orderby x.Key descending

                    select new
                    {
                        qq,
                        x.Key,

                        zzz,
                        zz,


                        //  __h__TransparentIdentifier1.`zz` as `z.zz`,
                        fz = new { zz, x.Tag },

                        x.Tag,

                        //x.connectStart,
                        //x.connectEnd

                        // cant detect those members just yet
                        connect = new { x.connectStart, x.connectEnd }
                    };

            var f = q.FirstOrDefault();

            Debugger.Break();
        }

    }
}
