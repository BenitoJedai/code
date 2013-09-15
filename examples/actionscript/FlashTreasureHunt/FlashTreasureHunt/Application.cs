using FlashTreasureHunt.Design;
using FlashTreasureHunt.HTML.Pages;
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

namespace FlashTreasureHunt
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                ChromeTCPServer.TheServer.Invoke(
                    AppSource.Text
                );


                return;
            }


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
