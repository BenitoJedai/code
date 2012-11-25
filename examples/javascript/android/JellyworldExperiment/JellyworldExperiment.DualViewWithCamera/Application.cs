using JellyworldExperiment.DualViewWithCamera.Design;
using JellyworldExperiment.DualViewWithCamera.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace JellyworldExperiment.DualViewWithCamera
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public Application(IApp page)
        {
            var sprite = new ApplicationSprite();

            sprite.AutoSizeSpriteTo(page.ContentSize);
            sprite.AttachSpriteTo(page.Content);

            sprite.InitializeContent();

            sprite.AfterInitializeContent();
        }

    }

    public static class ApplicationContent
    {
        public static void AfterInitializeContent(this IApplicationSprite sprite)
        {
            var a = new JellyworldExperiment.DualView.HTML.Pages.App();

            a.Container.AttachToDocument();
            a.Container.style.position = IStyle.PositionEnum.absolute;
            a.Container.style.left = "0px";
            //a.Container.style.top = "0px";
            a.Container.style.right = "0px";
            a.Container.style.bottom = "0px";

            a.range_s.value = "70";

            new JellyworldExperiment.DualView.Application(a).With(
                app =>
                {

                    sprite.AverageChanged +=
                        (Left, Top, Width, Height) =>
                        {
                            app.FaceDetectedAt(
                                int.Parse(Left),
                                int.Parse(Top),
                                int.Parse(Width),
                                int.Parse(Height)
                            );
                        };
                }
            );

        }
    }

}
