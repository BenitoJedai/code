using System;
using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using ScriptCoreLib.Desktop.Extensions;

namespace BrowserAvalonApplicationWithAdobeFlash2
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
            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));

#endif
        }

    }
}
