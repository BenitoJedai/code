using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using SVGNavigationTiming.Design;
using System;
using System.Linq;

namespace SVGNavigationTiming
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            //java.lang.NumberFormatException: null
            //        at java.lang.Long.parseLong(Unknown Source)
            //        at java.lang.Long.parseLong(Unknown Source)
            //        at ScriptCoreLib.Shared.BCLImplementation.System.__Convert.ToInt64(__Convert.java:144)


            new PerformanceResourceTimingData2.ApplicationPerformance().Insert(
                new PerformanceResourceTimingData2ApplicationPerformanceRow { }
            );

            var x = new PerformanceResourceTimingData2.ApplicationPerformance().SelectAllAsDataTable();
            var y = new PerformanceResourceTimingData2.ApplicationPerformance().SelectAllAsEnumerable();
            var yy = y.ToArray();

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
