using System;
using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using ScriptCoreLib.Desktop.Extensions;

namespace BrowserAvalonApplication1
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    public static class Program
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
