using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.utils;
using System;
using ScriptCoreLib.ActionScript.flash.ui;

namespace RayCaster2.ActionScript
{
    partial class RayCaster2
    {
        // !! this is a port from the RayCaster 1 java applet project


        // implement: create a dungeon
        // http://www.aarg.net/~minam/dungeon.cgi
        // http://www.astrolog.org/labyrnth/algrithm.htm

        public int PlayerRadiusMargin = 10;

        //*******************************************************************//
        //* Running thread
        //*******************************************************************//
        public void run()
        {
            //fOffscreenGraphics.clear();

            // rotate left
            if (fKeyLeft)
            {
                if ((fPlayerArc -= ANGLE10) < ANGLE0)
                    fPlayerArc += ANGLE360;
            }
            // rotate right
            else if (fKeyRight)
            {
                if ((fPlayerArc += ANGLE10) >= ANGLE360)
                    fPlayerArc -= ANGLE360;
            }

            //  _____     _
            // |\ arc     |
            // |  \       y
            // |    \     |
            //            -
            // |--x--|  
            //
            //  sin(arc)=y/diagonal
            //  cos(arc)=x/diagonal   where diagonal=speed
            double playerXDir = fCosTable[fPlayerArc];
            double playerYDir = fSinTable[fPlayerArc];

            // move forward
            if (fKeyUp)
            {
                fPlayerX += (int)(playerXDir * fPlayerSpeed);
                fPlayerY += (int)(playerYDir * fPlayerSpeed);
            }
            // move backward
            else if (fKeyDown)
            {
                fPlayerX -= (int)(playerXDir * fPlayerSpeed);
                fPlayerY -= (int)(playerYDir * fPlayerSpeed);
            }


            // get walls

            ClipPlayer();
            //showStatus(CurrentMapPosition.ToString());









            render2();
        }



        public bool IsInsideMapAndIsOpening(PointInt32 p)
        {
            if (myMap.ToRectInt32().IsOutSide(p))
                return false;

            return myMap[p] == O;
        }

        public sbyte CurrentMapTile
        {
            get
            {
                var p = CurrentMapPosition;

                return myMap[p.X, p.Y];
            }
        }

        public PointInt32 CurrentMapPosition
        {
            get
            {
                return new PointInt32
                {
                    X = (int)(fPlayerX / TILE_SIZE),
                    Y = (int)(fPlayerY / TILE_SIZE)
                };
            }
        }

        // movement flag
        protected KeyboardButton fKeyUp = new uint[] { Keyboard.UP, 'i', 'I', 'w', 'W' };
        protected KeyboardButton fKeyDown = new uint[] { Keyboard.DOWN, 'k', 'K', 's', 'S' };
        protected KeyboardButton fKeyLeft = new uint[] { Keyboard.LEFT, 'j', 'J', 'a', 'A' };
        protected KeyboardButton fKeyRight = new uint[] { Keyboard.RIGHT, 'l', 'L', 'd', 'D' };





    }
}