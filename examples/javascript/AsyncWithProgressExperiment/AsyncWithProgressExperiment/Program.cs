using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace AsyncWithProgressExperiment
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Action<object> OnReportAction = default(__IProgress<object>).Report;
            var OnReportMethod = OnReportAction.Method;

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
