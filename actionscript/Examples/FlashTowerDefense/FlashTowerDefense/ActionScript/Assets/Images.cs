using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;

[assembly: ScriptResources(FlashTowerDefense.ActionScript.Assets.Images.Path)]

namespace FlashTowerDefense.ActionScript.Assets
{
    [Script]
    public static partial class Images
    {
        public const string Path = "/assets/FlashTowerDefense.Images";

        [Embed(source = Path + "/hole_2.png")]
        public static Class hole_2;

        [Embed(source = Path + "/hole_1.png")]
        public static Class hole_1;


        [Embed(source = Path + "/barrel.png")]
        public static Class barrel;


        [Embed(source = Path + "/box.png")]
        public static Class box;


        [Embed(source = Path + "/music_on.png")]
        public static Class music_on;

        [Embed(source = Path + "/music_off.png")]
        public static Class music_off;

        [Embed(source = Path + "/footsteps.png")]
        public static Class footsteps;


        [Embed(source = Path + "/turret1-default.png")]
        public static Class turret1_default;


        [Embed(source = Path + "/grass1.png")]
        public static Class grass1;


        [Embed(source = Path + "/bump2.png")]
        public static Class bump2;



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


        [Embed(source = Path + "/man2-horns_dead_1.png")]
        public static Class man2_horns_dead1;

        [Embed(source = Path + "/man2-horns_dead_2.png")]
        public static Class man2_horns_dead2;


        // gunfire animation

        [Embed(Path + "/turret1-gunfire-180.png")]
        public static Class img_turret1_gunfire_180;

        [Embed(Path + "/turret1-gunfire-180-1.png")]
        static Class img_turret1_gunfire_180_1;

        [Embed(Path + "/turret1-gunfire-180-2.png")]
        static Class img_turret1_gunfire_180_2;

        [Embed(Path + "/turret1-gunfire-180-3.png")]
        static Class img_turret1_gunfire_180_3;

        public static Class[] img_turret1_gunfire_180_frames = new[] {
            img_turret1_gunfire_180_1,
            img_turret1_gunfire_180_2,
            img_turret1_gunfire_180_3
        };

        // boss hello


        // cactus

        [Embed(Path + "/img_cactus_1.png")]
        static Class img_cactus_1;

        [Embed(Path + "/img_cactus_2.png")]
        static Class img_cactus_2;

        [Embed(Path + "/img_cactus_3.png")]
        static Class img_cactus_3;

        public static Class[] img_cactus = new[] {
            img_cactus_1,
            img_cactus_2,
            img_cactus_3
        };

    }
}
