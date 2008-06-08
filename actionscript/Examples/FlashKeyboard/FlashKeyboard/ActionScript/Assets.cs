using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;

//[assembly: ScriptResources(FlashKeyboard.ActionScript.Assets.Path)]

namespace FlashKeyboard.ActionScript
{
    [Script]
    internal static class Assets
    {
        public const string Path = "/assets/FlashKeyboard";


        [Embed(Path + "/default.png")]
        public static Class img_default;


        [Embed(Path + "/alt.png")]
        public static Class img_alt;


        [Embed(Path + "/shift.png")]
        public static Class img_shift;





    }
}
