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


        // sheep
        [Embed(source = Path + "/sheep-walk_1.png")]
        public static Class sheep1;

        [Embed(source = Path + "/sheep-walk_2.png")]
        public static Class sheep2;

        [Embed(source = Path + "/sheep-walk_3.png")]
        public static Class sheep3;

        [Embed(source = Path + "/sheep-walk_4.png")]
        public static Class sheep4;


        [Embed(source = Path + "/sheep-corpse.png")]
        public static Class sheep_corpse;

        [Embed(source = Path + "/sheep-blood.png")]
        public static Class sheep_blood;

        [Embed(source = Path + "/sheep.mp3")]
        public static Class snd_sheep;

        // man

        [Embed(source = Path + "/man2-horns_1.png")]
        public static Class man2_horns1;

        [Embed(source = Path + "/man2-horns_2.png")]
        public static Class man2_horns2;

        [Embed(source = Path + "/man2-horns_3.png")]
        public static Class man2_horns3;

        [Embed(source = Path + "/man2-horns_4.png")]
        public static Class man2_horns4;

        [Embed(source = Path + "/man2-horns_5.png")]
        public static Class man2_horns5;

        [Embed(source = Path + "/man2-horns_6.png")]
        public static Class man2_horns6;

        [Embed(source = Path + "/man2-horns_7.png")]
        public static Class man2_horns7;

        [Embed(source = Path + "/man2-horns_8.png")]
        public static Class man2_horns8;

        [Embed(source = Path + "/man2-horns_9.png")]
        public static Class man2_horns9;

    }
}
