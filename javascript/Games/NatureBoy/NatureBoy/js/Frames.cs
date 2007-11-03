using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.Shared.Drawing;

namespace NatureBoy.js
{
    [Script]
    static class Frames
    {
        #region duke
        const string DukeTile = "assets/NatureBoy/dude2/stand/";

        public static FrameInfo[] Duke = new[] {
                new FrameInfo { Source = DukeTile + "TILE1406h.png",  Weight = 1d / 6 , OffsetX = -5, OffsetY = 1} ,
                new FrameInfo { Source = DukeTile + "TILE1405.png",  Weight = 1d / 6 , OffsetX = -1, OffsetY = 1 } ,
                new FrameInfo { Source = DukeTile + "TILE1406.png",  Weight = 1d / 6, OffsetX = 5, OffsetY = 1 } ,
                new FrameInfo { Source = DukeTile + "TILE1407.png",  Weight = 1d / 10 , OffsetX = 3, OffsetY = 1} ,
                new FrameInfo { Source = DukeTile + "TILE1408.png",  Weight = 1d / 10 , OffsetX = -1, OffsetY = 1} ,
                new FrameInfo { Source = DukeTile + "TILE1409.png",  Weight = 1d / 10 , OffsetX = 1, OffsetY = 1} ,
                new FrameInfo { Source = DukeTile + "TILE1408h.png",  Weight = 1d / 10 , OffsetX = 1, OffsetY = 1} ,
                new FrameInfo { Source = DukeTile + "TILE1407h.png",  Weight = 1d / 10 , OffsetX = -3, OffsetY = 1} 
            };

        const string DukeTile_Walk1 = "assets/NatureBoy/dude2/walk1/";

        public static FrameInfo[] Duke_Walk1 = new[] {
                new FrameInfo { Source = DukeTile_Walk1 + "TILE1426h.png",  Weight = 1d / 6 , OffsetX = -5, OffsetY = 1} ,
                new FrameInfo { Source = DukeTile_Walk1 + "TILE1425.png",  Weight = 1d / 6 , OffsetX = -1, OffsetY = 1 } ,
                new FrameInfo { Source = DukeTile_Walk1 + "TILE1426.png",  Weight = 1d / 6, OffsetX = 5, OffsetY = 1 } ,
                new FrameInfo { Source = DukeTile_Walk1 + "TILE1427.png",  Weight = 1d / 10 , OffsetX = 3, OffsetY = 1} ,
                new FrameInfo { Source = DukeTile_Walk1 + "TILE1428.png",  Weight = 1d / 10 , OffsetX = -1, OffsetY = 1} ,
                new FrameInfo { Source = DukeTile_Walk1 + "TILE1429.png",  Weight = 1d / 10 , OffsetX = 1, OffsetY = 1} ,
                new FrameInfo { Source = DukeTile_Walk1 + "TILE1428h.png",  Weight = 1d / 10 , OffsetX = 1, OffsetY = 1} ,
                new FrameInfo { Source = DukeTile_Walk1 + "TILE1427h.png",  Weight = 1d / 10 , OffsetX = -3, OffsetY = 1} 
            };

        const string DukeTile_Walk2 = "assets/NatureBoy/dude2/walk2/";

        public static FrameInfo[] Duke_Walk2 = new[] {
                new FrameInfo { Source = DukeTile_Walk2 + "TILE1431h.png",  Weight = 1d / 6 , OffsetX = -5, OffsetY = 1} ,
                new FrameInfo { Source = DukeTile_Walk2 + "TILE1430.png",  Weight = 1d / 6 , OffsetX = -1, OffsetY = 1 } ,
                new FrameInfo { Source = DukeTile_Walk2 + "TILE1431.png",  Weight = 1d / 6, OffsetX = 5, OffsetY = 1 } ,
                new FrameInfo { Source = DukeTile_Walk2 + "TILE1432.png",  Weight = 1d / 10 , OffsetX = 3, OffsetY = 1} ,
                new FrameInfo { Source = DukeTile_Walk2 + "TILE1433.png",  Weight = 1d / 10 , OffsetX = -1, OffsetY = 1} ,
                new FrameInfo { Source = DukeTile_Walk2 + "TILE1434.png",  Weight = 1d / 10 , OffsetX = 1, OffsetY = 1} ,
                new FrameInfo { Source = DukeTile_Walk2 + "TILE1433h.png",  Weight = 1d / 10 , OffsetX = 1, OffsetY = 1} ,
                new FrameInfo { Source = DukeTile_Walk2 + "TILE1432h.png",  Weight = 1d / 10 , OffsetX = -3, OffsetY = 1} 
            };


        const string DukeTile_Walk3 = "assets/NatureBoy/dude2/walk3/";

        public static FrameInfo[] Duke_Walk3 = new[] {
                new FrameInfo { Source = DukeTile_Walk3 + "TILE1436h.png",  Weight = 1d / 6 , OffsetX = -5, OffsetY = 1} ,
                new FrameInfo { Source = DukeTile_Walk3 + "TILE1435.png",  Weight = 1d / 6 , OffsetX = -1, OffsetY = 1 } ,
                new FrameInfo { Source = DukeTile_Walk3 + "TILE1436.png",  Weight = 1d / 6, OffsetX = 5, OffsetY = 1 } ,
                new FrameInfo { Source = DukeTile_Walk3 + "TILE1437.png",  Weight = 1d / 10 , OffsetX = 3, OffsetY = 1} ,
                new FrameInfo { Source = DukeTile_Walk3 + "TILE1438.png",  Weight = 1d / 10 , OffsetX = -1, OffsetY = 1} ,
                new FrameInfo { Source = DukeTile_Walk3 + "TILE1439.png",  Weight = 1d / 10 , OffsetX = 1, OffsetY = 1} ,
                new FrameInfo { Source = DukeTile_Walk3 + "TILE1438h.png",  Weight = 1d / 10 , OffsetX = 1, OffsetY = 1} ,
                new FrameInfo { Source = DukeTile_Walk3 + "TILE1437h.png",  Weight = 1d / 10 , OffsetX = -3, OffsetY = 1} 
            };

        public static FrameInfo[][] Duke_Walk = new[] { Duke_Walk2, Duke_Walk3, Duke_Walk1 };

        #endregion

        const string PigCopTile = "assets/NatureBoy/dude3/stand/";

        public static FrameInfo[] PigCop = new[] {
                new FrameInfo { Source = PigCopTile + "TILE2001h.png",  Weight = 1d / 6 , OffsetX = -9, OffsetY = -3} ,
                new FrameInfo { Source = PigCopTile + "TILE2000.png",  Weight = 1d / 6 , OffsetX = 1, OffsetY = -3 } ,
                new FrameInfo { Source = PigCopTile + "TILE2001.png",  Weight = 1d / 6, OffsetX = 9, OffsetY = -3 } ,
                new FrameInfo { Source = PigCopTile + "TILE2002.png",  Weight = 1d / 10 , OffsetX = 7, OffsetY = -3} ,
                new FrameInfo { Source = PigCopTile + "TILE2003.png",  Weight = 1d / 10 , OffsetX = -1, OffsetY = -3} ,
                new FrameInfo { Source = PigCopTile + "TILE2004.png",  Weight = 1d / 10 , OffsetX = -3, OffsetY = -3} ,
                new FrameInfo { Source = PigCopTile + "TILE2003h.png",  Weight = 1d / 10 , OffsetX = 1, OffsetY = -3} ,
                new FrameInfo { Source = PigCopTile + "TILE2002h.png",  Weight = 1d / 10 , OffsetX = -7, OffsetY = -3} 
            };

        const string TrooperTile = "assets/NatureBoy/dude4/stand/";

        public static FrameInfo[] Trooper = new[] {
                new FrameInfo { Source = TrooperTile + "TILE1681h.png",  Weight = 1d / 6 , OffsetX = -2, OffsetY = -3} ,
                new FrameInfo { Source = TrooperTile + "TILE1680.png",  Weight = 1d / 6 , OffsetX = 1, OffsetY = -3 } ,
                new FrameInfo { Source = TrooperTile + "TILE1681.png",  Weight = 1d / 6, OffsetX = 2, OffsetY = -3 } ,
                new FrameInfo { Source = TrooperTile + "TILE1682.png",  Weight = 1d / 10 , OffsetX = 1, OffsetY = -3} ,
                new FrameInfo { Source = TrooperTile + "TILE1683.png",  Weight = 1d / 10 , OffsetX = 3, OffsetY = -3} ,
                new FrameInfo { Source = TrooperTile + "TILE1684.png",  Weight = 1d / 10 , OffsetX = -3, OffsetY = -3} ,
                new FrameInfo { Source = TrooperTile + "TILE1683h.png",  Weight = 1d / 10 , OffsetX = -3, OffsetY = -3} ,
                new FrameInfo { Source = TrooperTile + "TILE1682h.png",  Weight = 1d / 10 , OffsetX = -1, OffsetY = -3} 
            };

        const double d8 = 1d / 8;

        #region wolf3d
        // http://www.wolf3d.co.uk/enemies.html

        public static FrameInfo[] WolfSoldier =
            8.Range(i => 114 + (i + 6) % 8)
            .Select(i =>
                new FrameInfo
                {
                    Source = "assets/NatureBoy/dude5/stand/" + i + ".png",
                    Weight = d8
                }
            ).ToArray();

        // compiler bug: casting wont work from IEnumerable<T[]> to T[]

        public static FrameInfo[][] WolfSoldier_Walk =
            4.Range(
                j =>
                    8.Range(i => (122 + j * 8) + (i + 6) % 8)
                    .Select(i =>
                        new FrameInfo
                        {
                            Source = "assets/NatureBoy/dude5/walk" + (j + 1) + "/" + i + ".png",
                            Weight = d8
                        }
                    ).ToArray()
            ).ToArray();

        // doom
        public static FrameInfo[] DoomImp =
           8.Range(i => 244 + (i + 6) % 8)
           .Select(i =>
               new FrameInfo
               {
                   Source = "assets/NatureBoy/dude6/" + i + ".png",
                   Weight = d8
               }
           ).ToArray();


        public static FrameInfo[][] DoomImp_Walk =
            4.Range(
                j =>
                    8.Range(i => (252 + j * 8) + (i + 6) % 8)
                    .Select(i =>
                        new FrameInfo
                        {
                            Source = "assets/NatureBoy/dude6/" + i + ".png",
                            Weight = d8
                        }
                    ).ToArray()
            ).ToArray();

        #endregion

        public static FrameInfo[] Shrek =
           8.Range(i => i + 1)
           .Select(i =>
               new FrameInfo
               {
                   Source = "assets/NatureBoy/dude7/" + i + ".png",
                   Weight = d8,
                   OffsetY = -20
               }
           ).ToArray();

        public static FrameInfo[] Dragon =
           8.Range(i => i + 1)
           .Select(i =>
               new FrameInfo
               {
                   Source = "assets/NatureBoy/dude8/" + i + ".png",
                   Weight = d8,
                   OffsetY = -80
               }
           ).ToArray();



        public static FrameInfo[] WhiteDog =
           8.Range(i => i + 1)
           .Select(i =>
               new FrameInfo
               {
                   Source = "assets/NatureBoy/dude9/" + i + ".png",
                   Weight = d8,
                   OffsetY = -10
               }
           ).ToArray();

        public static FrameInfo[][] WhiteDog_Walk =
            2.Range(
                j =>
                    8.Range(i => (9 + j * 8) + i)
                    .Select(i =>
                        new FrameInfo
                        {
                            Source = "assets/NatureBoy/dude9/" + i + ".png",
                            Weight = d8,
                            OffsetY = -10
                        }
                    ).ToArray()
            ).ToArray();

        public static FrameInfo[][] AllFrames = new[] 
        {
            Duke,
            Trooper,
            PigCop,
            WolfSoldier,
            DoomImp,
            Shrek,
            Dragon,
            WhiteDog
        }
        .Concat(Duke_Walk)
        .Concat(WolfSoldier_Walk)
        .Concat(DoomImp_Walk)
        .Concat(WhiteDog_Walk)
        .ToArray();
    }

}