using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using ScriptCoreLib.Desktop.Extensions;
using System;

namespace FlashHeatZeeker.Core
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
//#if DEBUG
//#if X

//            DesktopAvalonExtensions.Launch(
//                () => new ApplicationCanvas()
//            );
//#endif
//#else
            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
//#endif
        }

    }
}
