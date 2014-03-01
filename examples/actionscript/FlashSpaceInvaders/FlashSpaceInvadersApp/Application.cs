using chrome;
using FlashSpaceInvadersApp.Design;
using FlashSpaceInvadersApp.HTML.Pages;
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

namespace FlashSpaceInvadersApp
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        // Reason: Mochi received a DMCA take down warning from the original creators of "Space Invaders."
        // https://www.mochimedia.com/community/forum/topic/all-space-invaders-games-pulled-from-mochi-catal

        public readonly ApplicationWebService service = new ApplicationWebService();


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {

            var sprite = new ApplicationSprite();

            // why transparent?
            // why is the sprite bg white?
            sprite.ToTransparentSprite();

            // Initialize ApplicationSprite
            sprite.AttachSpriteToDocument();


        }

    }
}
