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

namespace TestSelectDateGroups
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
            new Data.PerformanceResourceTimingData2.ApplicationPerformance().Insert(
                // timestamp is special and insert will override it!
                // shall jsc signify that with [Obsolete attribute?]

                // will timestamp also include signature, hash or ID signature? or is it good
                // enough if we sign previous data row?
                // does security metadata need to live in a different table? a generic security table?
                //DateTime.Now.AddDays(-1)
                new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { EventTime = DateTime.Now.AddDays(-0) },

               new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { EventTime = DateTime.Now.AddDays(-1) },
               new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { EventTime = DateTime.Now.AddDays(-1) },

               new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { EventTime = DateTime.Now.AddDays(-2) },
               new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { EventTime = DateTime.Now.AddDays(-2) },
               new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { EventTime = DateTime.Now.AddDays(-2) },

               new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { EventTime = DateTime.Now.AddDays(-3) },
               new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { EventTime = DateTime.Now.AddDays(-3) },
               new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { EventTime = DateTime.Now.AddDays(-3) },
               new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { EventTime = DateTime.Now.AddDays(-3) },


               new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { EventTime = DateTime.Now.AddDays(-5) }

           );


            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201406/20140607

            var z_continious =
                 from x in new Data.PerformanceResourceTimingData2.ApplicationPerformance() //.AsGenericEnumerable()
                 from offset in Enumerable.Range(0, 7)
                 group x by new { offset } into g
                 select new
                 {
                     //Count = g.Count(y => y.EventTime.Date == DateTime.Now.Date.AddDays(-g.Key.offset)),

                     // any group select sub filters need to be translated to joins !!!
                     Count = g.Count(y => true),
                     g.Key.offset
                 };

            // f = { Count = 484, offset = 0 }
            // f = { Count = 11, offset = 0 }

            // how can sql give us count of g 7. we have 7 groups
            // ah we need a count within each group!

            // f = { Count = 7, offset = 0 }
            var f = z_continious.FirstOrDefault();
            //var dt = z_continious.AsDataTable();

            Debugger.Break();

        }

    }
}
