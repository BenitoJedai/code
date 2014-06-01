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

            var q = from x in new Data.PerformanceResourceTimingData2.ApplicationPerformance()
                    let zzz = "!"
                    let zz = "???"
                    //let qq = new { u = "!!!", x.Tag }

                    let qq = new object()

                    select new
                    {
                        zzz,
                        zz,

                        qq,

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
