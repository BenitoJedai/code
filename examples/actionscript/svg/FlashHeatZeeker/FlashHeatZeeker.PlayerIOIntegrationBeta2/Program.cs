using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;
using System.Threading;
using ScriptCoreLib.Extensions;

namespace FlashHeatZeeker.PlayerIOIntegrationBeta2
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            #region player.io dev server
            new Thread(
                delegate()
                {
                    Thread.Sleep(1000);

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
        }

    }
}
