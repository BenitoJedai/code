using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;

[assembly: ScriptResources(FlashTowerDefense.ActionScript.Assets.Path)]

namespace FlashTowerDefense.ActionScript
{
    [Script]
    internal static class Assets
    {
        public const string Path = "/assets/FlashTowerDefense";

        [Embed(source = Path + "/turret1-default.png")]
        public static Class turret1_default;


        [Embed(source = Path + "/grass1.png")]
        public static Class grass1;


        [Embed(source = Path + "/bump2.png")]
        public static Class bump2;


        [Embed(source = Path + "/FNCL.mp3")]
        public static Class gunfire;

        [Embed(source = Path + "/world.mp3")]
        public static Class world;

    }
}
