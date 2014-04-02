using chrome;
using FlashTreasureHunt.Design;
using FlashTreasureHunt.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FlashTreasureHunt
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
    



            ApplicationSprite sprite = new ApplicationSprite();


            // Initialize ApplicationSprite
            sprite.AttachSpriteToDocument();

            page.GoFullscreenFromFlash.onclick +=
                delegate
                {
                    sprite.GoFullscreen();
                };

            page.GoFullscreenFromDOM.onclick +=
             delegate
             {
                 SpriteExtensions.ToHTMLElement(sprite).requestFullscreen();
             };

            @"FlashTreasureHunt".ToDocumentTitle();

        }

    }
}
