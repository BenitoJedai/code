using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace com.abstractatech.scholar
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var u = new Uri("about:blank");
            var __u = new ScriptCoreLib.Shared.BCLImplementation.System.__Uri("about:blank");

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
