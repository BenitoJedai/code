using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

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


            var z = Convert.ToInt64(default(string));

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
