using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;

[assembly: ScriptResources(FlashTowerDefense.ActionScript.Assets.Images.Avatars.Path)]

namespace FlashTowerDefense.ActionScript.Assets
{
    
    partial class Images
    {
        [Script]
        public static class Avatars
        {
            public const string Path = "/assets/FlashTowerDefense.Images.Avatars";

            [Embed(Path + "/avatars_barrel.png")]
            public static Class avatars_barrel;


            [Embed(Path + "/avatars_heart.png")]
            public static Class avatars_heart;


            [Embed(Path + "/avatars_ammo.png")]
            public static Class avatars_ammo;

            [Embed(Path + "/avatars_colt45.png")]
            public static Class avatars_colt45;

            [Embed(Path + "/avatars_m249.png")]
            public static Class avatars_m249;

            [Embed(Path + "/avatars_shotgun.png")]
            public static Class avatars_shotgun;

            [Embed(Path + "/avatars_shotgun2.png")]
            public static Class avatars_shotgun2;

        }

       

    }
}
