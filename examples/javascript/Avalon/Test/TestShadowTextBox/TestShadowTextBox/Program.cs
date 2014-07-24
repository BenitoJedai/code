using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using ScriptCoreLib.Desktop.Extensions;
using System;
using System.Diagnostics;

namespace TestShadowTextBox
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
                DesktopAvalonExtensions.Launch(
                    () => new ApplicationCanvas()
                );
                return;
            }
#endif
            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
