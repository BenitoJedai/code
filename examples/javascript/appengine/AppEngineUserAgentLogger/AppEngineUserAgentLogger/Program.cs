using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace AppEngineUserAgentLogger
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // AppEngineUserAgentLogger.Schema.FirstTableExtensions::ExecuteNonQuery] Method does not exist.

            var w = new ApplicationWebService();

            w.SetScreenSize(1, 1);

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
