using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;

[assembly: ScriptResources(FlashTowerDefense.ActionScript.Assets.Images.Explosions.Path)]

namespace FlashTowerDefense.ActionScript.Assets
{

    partial class Images
    {
        [Script]
        public static class Explosions
        {
            public const string Path = "/assets/FlashTowerDefense.Images.Explosions";

            [Embed(Path + "/ani6_01.png")]
            public static Class ani6_01;
            [Embed(Path + "/ani6_02.png")]
            public static Class ani6_02;
            [Embed(Path + "/ani6_03.png")]
            public static Class ani6_03;
            [Embed(Path + "/ani6_04.png")]
            public static Class ani6_04;
            [Embed(Path + "/ani6_05.png")]
            public static Class ani6_05;
            [Embed(Path + "/ani6_06.png")]
            public static Class ani6_06;
            [Embed(Path + "/ani6_07.png")]
            public static Class ani6_07;
            [Embed(Path + "/ani6_08.png")]
            public static Class ani6_08;
            [Embed(Path + "/ani6_09.png")]
            public static Class ani6_09;
            [Embed(Path + "/ani6_10.png")]
            public static Class ani6_10;
            [Embed(Path + "/ani6_11.png")]
            public static Class ani6_11;
            [Embed(Path + "/ani6_12.png")]
            public static Class ani6_12;
            [Embed(Path + "/ani6_13.png")]
            public static Class ani6_13;
            [Embed(Path + "/ani6_14.png")]
            public static Class ani6_14;
            [Embed(Path + "/ani6_15.png")]
            public static Class ani6_15;
            [Embed(Path + "/ani6_16.png")]
            public static Class ani6_16;


            public static Class[] ani6 = new[]
            {
                ani6_01,

                ani6_02,

                ani6_03,

                ani6_04,

                ani6_05,

                ani6_06,

                ani6_07,

                ani6_08,

                ani6_09,

                ani6_10,

                ani6_11,

                ani6_12,

                ani6_13,

                ani6_14,

                ani6_15,

                ani6_16,
                    };


        }



    }
}
