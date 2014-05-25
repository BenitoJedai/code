using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Shared.Lambda;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using NatureBoyTestPadExperiment.Design;
using NatureBoyTestPadExperiment.HTML.Pages;
using NatureBoyTestPadExperiment.HTML.Audio.FromAssets;
using ScriptCoreLib.JavaScript.Controls.NatureBoy;
using NatureBoyTestPad.js;
using System.Collections.Generic;

namespace NatureBoyTestPadExperiment
{
    static class X
    {


        public static Action<Action> ContinueAfterImagesReady(this IEnumerable<FrameInfo[][]> e, Action<int> update)
        {
            var c = 0;

            return e.ForEach(
                (FrameInfo[][] k, Action next1) =>
                {

                    k.ForEach(
                         (FrameInfo[] kk, Action next2) =>
                         {

                             kk.ForEach(
                                (FrameInfo kkk, Action next3) =>
                                {

                                    c++;

                                    update(c);

                                    kkk.Image.InvokeOnComplete(
                                        delegate { next3(); }, 1
                                    );

                                }
                            )(
                                next2
                            );
                         }
                     )(
                         next1
                     );
                }
            );
        }
    }

    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // not roslyn friendly?

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // where does the tcp server put the memory?

            #region ChromeTCPServer
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                chrome.Notification.DefaultIconUrl = new HTML.Images.FromAssets.Preview().src;
                chrome.Notification.DefaultTitle = "NatureBoyTestPadExperiment";


                ChromeTCPServer.TheServerWithStyledForm.Invoke(
                    AppSource.Text
                );

                return;
            }
            #endregion

            global::DiagnosticsConsole.ApplicationContent.BindKeyboardToDiagnosticsConsole();


            #region music
            var music = default(world);

            Action loop = null;

            loop = delegate
            {
                music = new world { volume = 0.1 }.AttachToDocument();

                music.onended +=
                    delegate
                    {
                        Console.WriteLine(" music.onended ");
                        music.Orphanize();

                        loop();
                    };

                music.play();

            };

            loop();
            #endregion

            //music.setAttribute("loop", "loop");

            new[]
            {
                new [] {
                Frames.WolfSoldier,
                Frames.DoomImp,
                MyFrames.NPC3.Frames_Stand,
                MyFrames.ManWithHorns.Frames_Stand,
                MyFrames.ThePig.Frames_Stand,
                MyFrames.TheSheep.Frames_Stand,
                },
                Frames.WolfSoldier_Walk,
                Frames.DoomImp_Walk,
                MyFrames.NPC3.Frames_Walk,
                MyFrames.ManWithHorns.Frames_Walk
            }.ContinueAfterImagesReady(

                    // why cannot we just set progress and skip innerText, should work, a bug?
                c => page.progress.innerText = c + " images loaded"
            )(
                 NatureBoyTestPad.js.NatureBoyTestPad.InitializeContent
            );

        }

    }
}
