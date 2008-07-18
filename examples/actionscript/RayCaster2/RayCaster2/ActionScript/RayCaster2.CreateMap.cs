using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.utils;
using System;

namespace RayCaster2.ActionScript
{
    partial class RayCaster2
    {
        // !! this is a port from the RayCaster 1 java applet project



        protected static readonly sbyte B = 4;
        protected static readonly sbyte G = 3;
        protected static readonly sbyte R = 2;


        /// <summary>
        /// wall
        /// </summary>
        protected static readonly sbyte W = 1;

        /// <summary>
        /// opening
        /// </summary>
        protected static readonly sbyte O = 0;

        // size of tile (wall height)
        public static readonly int TILE_SIZE = 64;
        public static readonly int WALL_HEIGHT = 64;

        protected static readonly int MAP_WIDTH = 14;
        protected static readonly int MAP_HEIGHT = 14;

        // 2 dimensional map
        Array2DSByte myMap;

        // player's attributes
        protected int fPlayerX = (int)(TILE_SIZE * 1.5);
        protected int fPlayerY = (int)(TILE_SIZE * 1.5);

        public uint GetWallColor(sbyte w, bool alt)
        {
            if (alt)
            {
                if (w == R) return (uint)(0xaf0000);
                if (w == G) return (uint)(0x00af00);
                if (w == B) return (uint)(0x0000af);
            }

            if (w == R) return (uint)(0xbf0000);
            if (w == G) return (uint)(0x00bf00);
            if (w == B) return (uint)(0x0000bf);

            return (uint)(0);
        }

        private void CreateMap()
        {

            myMap = new Array2DSByte(MAP_WIDTH, MAP_HEIGHT,
                W, W, W, W, W, W, W, W, W, W, W, W, W, W,
                W, O, O, O, O, O, O, O, O, O, B, O, O, W,
                W, O, O, O, O, O, O, O, O, O, G, O, O, W,
                W, O, O, O, O, O, O, O, W, O, G, G, O, W,
                W, O, O, W, O, R, O, O, W, O, O, O, O, O,
                W, O, O, W, O, W, W, O, W, O, W, W, O, W,
                W, R, W, W, O, O, W, O, W, O, W, W, O, W,
                W, W, W, W, W, O, W, O, W, O, W, O, O, W,
                W, O, W, O, O, O, W, O, W, O, W, O, O, W,
                W, O, W, O, W, W, W, O, W, O, W, W, O, W,
                W, O, O, O, O, O, W, O, O, O, W, O, O, W,
                W, W, W, W, W, O, W, O, O, O, W, O, O, W,
                W, O, O, O, O, O, O, O, O, O, W, O, O, W,
                W, W, W, W, W, W, W, W, W, W, W, W, W, W
            );


        }



    }
}