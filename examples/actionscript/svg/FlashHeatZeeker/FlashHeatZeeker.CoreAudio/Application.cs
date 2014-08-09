using FlashHeatZeeker.CoreAudio.Design;
using FlashHeatZeeker.CoreAudio.HTML.Pages;
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

namespace FlashHeatZeeker.CoreAudio
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        public readonly ApplicationSprite sprite = new ApplicationSprite();

        // verified 20140809

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {


            // Initialize ApplicationSprite
            sprite.AttachSpriteToDocument();

            sprite.Visualize();

            // http://renaun.com/blog/2012/12/enable-advanced-telemetry-on-flex-or-old-swfs-with-swf-scount-enabler/
            new IHTMLButton { innerText = "need more" }.AttachToDocument().WhenClicked(
                btn => sprite.Visualize()
            );
        }

    }
}
