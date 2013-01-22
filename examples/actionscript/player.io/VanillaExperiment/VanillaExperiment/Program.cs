using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using ScriptCoreLib.Desktop.Extensions;
using ScriptCoreLib.Extensions;
using System;
using System.Threading;

namespace VanillaExperiment
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

            #region player.io dev server
            new Thread(
                delegate()
                {
                    PlayerIO.DevelopmentServer.Server.StartWithDebugging();
                }
            )
            {
                Name = "player.io dev server",
                ApartmentState = ApartmentState.STA
            }.With(
                t =>
                {
                    Console.WriteLine("will start threads for jsc server and also " + t.Name + "...");

                    t.Start();
                }
            );
            #endregion

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
#endif
        }

    }
}
