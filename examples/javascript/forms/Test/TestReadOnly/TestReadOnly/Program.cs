using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using ScriptCoreLib.Desktop.Forms.Extensions;
using System;
using System.Diagnostics;

namespace TestReadOnly
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
#if DEBUG
            if (Debugger.IsAttached)
            {
                DesktopFormsExtensions.Launch(
                    () => new ApplicationControl()
                );
                return;
            }
#endif
            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
