using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;

//[assembly: ScriptResources(LightsOut.ActionScript.Assets.Path)]

namespace LightsOut.ActionScript
{
    [Script]
    internal static class Assets
    {
        //public const string Path = "assets/LightsOut";

        const string Path = "/assets/LightsOut";

        [Embed(source = Path + "/background.png")]
        public static Class background;

        [Embed(source = Path + "/vistaLogoOn.png")]
        public static Class vistaLogoOn;

        [Embed(source = Path + "/vistaLogoOff.png")]
        public static Class vistaLogoOff;

        [Embed(source = Path + "/click.mp3")]
        public static Class click;
    }
}
