using AndroidFlashTreasureHunt.IsometricView.Design;
using AndroidFlashTreasureHunt.IsometricView.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.Controls.NatureBoy;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace AndroidFlashTreasureHunt.IsometricView
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            ApplicationContent.InitializeContent();
        }

    }

    public static class ApplicationContent
    {
        public static void InitializeContent(Action yield = null)
        {
            var tt = new InteractiveTransformA.HTML.Pages.TexturesImages();
            tt.floor.InvokeOnComplete(
                delegate
                {
                    var content = new ApplicationCanvas();

                    content.Width = 400;
                    content.Height = 300;

                    NatureBoyTestPad.js.NatureBoyTestPad.FilterToImpAndSoldier = true;
                    NatureBoyTestPad.js.NatureBoyTestPad.DefaultActiorZoom = 0.5;
                    NatureBoyTestPad.js.NatureBoyTestPad.Title = "AndroidFlashTreasureHunt prototype";

                    NatureBoyTestPad.js.NatureBoyTestPad.BeforeAddingDebris =
                        Canvas =>
                        {
                            content.AttachToContainer(Canvas);

                        };
                    new[]
            {
                new [] {
                Frames.WolfSoldier,
                Frames.DoomImp,
        
                },
                Frames.WolfSoldier_Walk,
                Frames.DoomImp_Walk,
               
            }.ContinueAfterImagesReady(

                            // why cannot we just set progress and skip innerText, should work, a bug?
                        c => Native.Document.title = c + " images loaded"
                    )(
                        delegate
                        {
                            NatureBoyTestPad.js.NatureBoyTestPad.InitializeContent();
                            if (yield != null)
                                yield();

                        }
                    );
                }
            );

        }
    }

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
}
