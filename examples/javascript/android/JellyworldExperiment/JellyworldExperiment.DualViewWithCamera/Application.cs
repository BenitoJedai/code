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
        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly ApplicationSprite sprite = new ApplicationSprite();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            sprite.ToTransparentSprite();

            sprite.AutoSizeSpriteTo(page.ContentSize);
            sprite.AttachSpriteTo(page.Content);

            sprite.InitializeContent();

            var a = new JellyworldExperiment.DualView.HTML.Pages.App();

            a.Container.AttachToDocument();
            a.Container.style.position = IStyle.PositionEnum.absolute;
            a.Container.style.left = "0px";
            //a.Container.style.top = "0px";
            a.Container.style.right = "0px";
            a.Container.style.bottom = "0px";

            new JellyworldExperiment.DualView.Application(a);

        }

    }
}
