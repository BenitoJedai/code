using Flare3DWaterStep5TheShip.Design;
using Flare3DWaterStep5TheShip.HTML.Pages;
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

namespace Flare3DWaterStep5TheShip
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
            // Initialize ApplicationSprite
            sprite.AttachSpriteTo(page.Content);
            var TellServerWhichFilesFlashWantsToLoad = new[] {
            "assets/Flare3DWaterStep5TheShip/ship.zf3d",
            "assets/Flare3DWaterStep5TheShip/skybox.png",
            "assets/Flare3DWaterStep5TheShip/highlights.png",
            "assets/Flare3DWaterStep5TheShip/normalmap.jpg"
        };
        }

 
    }
}
