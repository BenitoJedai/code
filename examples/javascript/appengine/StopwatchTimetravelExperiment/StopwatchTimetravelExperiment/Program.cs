using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace StopwatchTimetravelExperiment
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var z=System.Diagnostics.StopwatchExtensions.CreateStopwatchAtElapsed(
                TimeSpan.FromMilliseconds(212)
            );


            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
