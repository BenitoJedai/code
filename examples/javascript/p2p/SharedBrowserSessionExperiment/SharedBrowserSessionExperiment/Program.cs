using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using SharedBrowserSessionExperiment.DataLayer.Data;
using System;
using System.Diagnostics;
using ScriptCoreLib.Extensions;

namespace SharedBrowserSessionExperiment
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
#if !RELEASE
            //Additional information: ActiveX control '8856f961-340a-11d0-a96b-00c04fd705a2' cannot be instantiated because the current thread is not in a single-threaded apartment.

            if (Debugger.IsAttached)
            {
                //new TheBrowserTab().ShowDialog();
                new PositionsWatchdog().ShowDialog();
                return;
            }
#endif

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
