using System;
using System.Collections.Generic;

using System.Text;
using ScriptCoreLib;
using java.applet;
using java.lang;
using java.awt;

namespace RayCaster1.source.java
{

    partial class RayCaster1Base
    {
        /// <summary>
        /// painted wall
        /// </summary>
        protected static readonly sbyte M = 2;


        /// <summary>
        /// wall
        /// </summary>
        protected static readonly sbyte W = 1;

        /// <summary>
        /// opening
        /// </summary>
        protected static readonly sbyte O = 0;

        // size of tile (wall height)
        public static readonly int TILE_SIZE = 32;
        public static readonly int WALL_HEIGHT = 64;

        protected static readonly int MAP_WIDTH = 14;
        protected static readonly int MAP_HEIGHT = 14;

        // 2 dimensional map
        Array2DSByte myMap;

        // player's attributes
        protected int fPlayerX = (int)(TILE_SIZE * 1.5);
        protected int fPlayerY = (int)(TILE_SIZE * 1.5);



        private void CreateMap()
        {

            myMap = new Array2DSByte(MAP_WIDTH, MAP_HEIGHT,
                W,W,W,W,W,W,W,W,W,W,W,W,W,W,
                W,O,O,O,O,O,O,O,O,O,W,O,O,W,
                W,O,O,O,O,O,O,O,O,O,W,O,O,W,
                W,O,O,O,O,O,O,O,W,O,W,W,O,W,
                W,O,O,W,O,M,O,O,W,O,O,O,O,W,
                W,O,O,W,O,W,W,O,W,O,W,W,O,W,
                W,M,W,W,O,O,W,O,W,O,W,W,O,W,
                W,W,W,W,W,O,W,O,W,O,W,O,O,W,
                W,O,W,O,O,O,W,O,W,O,W,O,O,W,
                W,O,W,O,W,W,W,O,W,O,W,W,O,W,
                W,O,O,O,O,O,W,O,O,O,W,O,O,W,
                W,W,W,W,W,O,W,O,O,O,W,O,O,W,
                W,O,O,O,O,O,O,O,O,O,W,O,O,W,
                W,W,W,W,W,W,W,W,W,W,W,W,W,W
            );


        }




    }
}
