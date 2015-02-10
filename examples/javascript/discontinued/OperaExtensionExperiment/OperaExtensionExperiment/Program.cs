using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace OperaExtensionExperiment
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // The operation is not allowed on non-connected sockets.
            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
