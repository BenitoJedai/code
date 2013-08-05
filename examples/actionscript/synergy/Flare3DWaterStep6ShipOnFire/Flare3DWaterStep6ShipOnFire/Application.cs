using Flare3DWaterStep6ShipOnFire.Design;
using Flare3DWaterStep6ShipOnFire.HTML.Pages;
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

namespace Flare3DWaterStep6ShipOnFire
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

            sprite.AttachSpriteToDocument().With(
                   embed =>
                   {
                       embed.style.SetLocation(0, 0);
                       embed.style.SetSize(Native.window.Width, Native.window.Height);

                       Native.window.onresize +=
                           delegate
                           {
                               embed.style.SetSize(Native.window.Width, Native.window.Height);
                           };
                   }
               );

            var TellServerWhichFilesFlashWantsToLoad = new[] {
                "assets/Flare3DWaterStep6ShipOnFire/ship.zf3d",
                "assets/Flare3DWaterStep6ShipOnFire/skybox.png",
                "assets/Flare3DWaterStep6ShipOnFire/highlights.png",
                "assets/Flare3DWaterStep6ShipOnFire/normalmap.jpg"
            };
        }

    }
}
