using flare.basic;
using flare.core;
using Flare3DWaterShipComponent;
using FlashFlare3DEmbedingColladaFile;
using FlashFlare3DEmbedingColladaFile.Design;
using FlashFlare3DEmbedingColladaFile.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
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

namespace FlashFlare3DEmbedingColladaFile
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {

        public readonly ApplicationSprite sprite = new ApplicationSprite();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // Initialize ApplicationSprite
            sprite.AttachSpriteTo(page.Content);

        }

    }
}
