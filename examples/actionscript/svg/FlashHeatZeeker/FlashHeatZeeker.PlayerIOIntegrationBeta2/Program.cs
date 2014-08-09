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
            //Method not found: 'Void starling.core.Starling..ctor(ScriptCoreLib.ActionScript.Class, ScriptCoreLib.ActionScript.flash.display.Stage, ScriptCoreLib.ActionScript.flash.geom.Rectangle, ScriptCoreLib.ActionScript.flash.display.Stage3D, System.String, System.String)'.

#if FPLAYERIO
            #region player.io dev server
#if DEBUG
            new Thread(
                delegate()
                {
                    Thread.Sleep(1000);

                    if (System.Windows.Forms.MessageBox.Show("Start player.io dev server?", "", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
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
#endif

            #endregion
#endif


            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
