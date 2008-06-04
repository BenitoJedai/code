using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;

namespace FlashTowerDefense.ActionScript.Monetized
{
    [Script, ScriptApplicationEntryPoint(Width = FlashTowerDefense.Width, Height = FlashTowerDefense.Height)]
    [SWF(width = FlashTowerDefense.Width, height = FlashTowerDefense.Height, backgroundColor = FlashTowerDefense.ColorWhite)]
    class NewgroundsPreloader : MochiPreloader
    {

        public NewgroundsPreloader()
        {
            NewgroundsAPI.linkAPI(this);
            NewgroundsAPI.connectMovie(1253);
        }
    }
}
