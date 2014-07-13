using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using ScriptCoreLib.Desktop.Extensions;
using System;

namespace com.abstractatech.gomoku
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
#if DEBUG
			DesktopAvalonExtensions.Launch(
				() => new ApplicationCanvas()
			);
#else
            // The process cannot access the file 'C:\Users\Arvo\mm.cfg' because it is being used by another process.
            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
#endif
        }

    }
}
