using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using ScriptCoreLib.Desktop.Extensions;
using ScriptCoreLib.CSharp.Avalon.Extensions;
using System;

namespace AvalonWindowlessWindowDrawer
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
                () => new ApplicationCanvas(),

                w =>
                {
                    //w.Content.c.r.Visibility = System.Windows.Visibility.Hidden;
                    //w.Window.ExplicitWithGlass();
                }
            );
#else
            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
#endif
        }

    }
}
