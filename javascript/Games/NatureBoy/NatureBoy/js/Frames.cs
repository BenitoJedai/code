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
                new FrameInfo { Image = DukeTile + "TILE1406h.png",  Weight = 1d / 6 , OffsetX = -5, OffsetY = 1} ,
                new FrameInfo { Image = DukeTile + "TILE1405.png",  Weight = 1d / 6 , OffsetX = -1, OffsetY = 1 } ,
                new FrameInfo { Image = DukeTile + "TILE1406.png",  Weight = 1d / 6, OffsetX = 5, OffsetY = 1 } ,
                new FrameInfo { Image = DukeTile + "TILE1407.png",  Weight = 1d / 10 , OffsetX = 3, OffsetY = 1} ,
                new FrameInfo { Image = DukeTile + "TILE1408.png",  Weight = 1d / 10 , OffsetX = -1, OffsetY = 1} ,
                new FrameInfo { Image = DukeTile + "TILE1409.png",  Weight = 1d / 10 , OffsetX = 1, OffsetY = 1} ,
                new FrameInfo { Image = DukeTile + "TILE1408h.png",  Weight = 1d / 10 , OffsetX = 1, OffsetY = 1} ,
                new FrameInfo { Image = DukeTile + "TILE1407h.png",  Weight = 1d / 10 , OffsetX = -3, OffsetY = 1} 
            };
        #endregion

        const string PigCopTile = "assets/NatureBoy/dude3/stand/";

        public static FrameInfo[] PigCop = new[] {
                new FrameInfo { Image = PigCopTile + "TILE2001h.png",  Weight = 1d / 6 , OffsetX = -9, OffsetY = -3} ,
                new FrameInfo { Image = PigCopTile + "TILE2000.png",  Weight = 1d / 6 , OffsetX = 1, OffsetY = -3 } ,
                new FrameInfo { Image = PigCopTile + "TILE2001.png",  Weight = 1d / 6, OffsetX = 9, OffsetY = -3 } ,
                new FrameInfo { Image = PigCopTile + "TILE2002.png",  Weight = 1d / 10 , OffsetX = 7, OffsetY = -3} ,
                new FrameInfo { Image = PigCopTile + "TILE2003.png",  Weight = 1d / 10 , OffsetX = -1, OffsetY = -3} ,
                new FrameInfo { Image = PigCopTile + "TILE2004.png",  Weight = 1d / 10 , OffsetX = -3, OffsetY = -3} ,
                new FrameInfo { Image = PigCopTile + "TILE2003h.png",  Weight = 1d / 10 , OffsetX = 1, OffsetY = -3} ,
                new FrameInfo { Image = PigCopTile + "TILE2002h.png",  Weight = 1d / 10 , OffsetX = -7, OffsetY = -3} 
            };

        const string TrooperTile = "assets/NatureBoy/dude4/stand/";

        public static FrameInfo[] Trooper = new[] {
                new FrameInfo { Image = TrooperTile + "TILE1681h.png",  Weight = 1d / 6 , OffsetX = -2, OffsetY = -3} ,
                new FrameInfo { Image = TrooperTile + "TILE1680.png",  Weight = 1d / 6 , OffsetX = 1, OffsetY = -3 } ,
                new FrameInfo { Image = TrooperTile + "TILE1681.png",  Weight = 1d / 6, OffsetX = 2, OffsetY = -3 } ,
                new FrameInfo { Image = TrooperTile + "TILE1682.png",  Weight = 1d / 10 , OffsetX = 1, OffsetY = -3} ,
                new FrameInfo { Image = TrooperTile + "TILE1683.png",  Weight = 1d / 10 , OffsetX = 3, OffsetY = -3} ,
                new FrameInfo { Image = TrooperTile + "TILE1684.png",  Weight = 1d / 10 , OffsetX = -3, OffsetY = -3} ,
                new FrameInfo { Image = TrooperTile + "TILE1683h.png",  Weight = 1d / 10 , OffsetX = -3, OffsetY = -3} ,
                new FrameInfo { Image = TrooperTile + "TILE1682h.png",  Weight = 1d / 10 , OffsetX = -1, OffsetY = -3} 
            };
    }

}