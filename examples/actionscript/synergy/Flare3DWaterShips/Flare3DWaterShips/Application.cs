using chrome;
using Flare3DWaterShips.Design;
using Flare3DWaterShips.HTML.Pages;
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

namespace Flare3DWaterShips
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {



        public Application(IApp page)
        {
 
            ApplicationSprite sprite = new ApplicationSprite();

            // Initialize ApplicationSprite
            sprite.AttachSpriteToDocument();

            //var TellServerWhichFilesFlashWantsToLoad = new[] {
            //    //"assets/Flare3DWaterShips/ship.zf3d",
            //    "assets/Flare3DWaterShips/skybox.png",
            //    "assets/Flare3DWaterShips/highlights.png",
            //    "assets/Flare3DWaterShips/normalmap.jpg"
            //};
        }

    }
}
