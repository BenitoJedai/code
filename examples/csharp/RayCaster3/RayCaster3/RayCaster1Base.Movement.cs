using System;
using System.Collections.Generic;

using System.Text;
using ScriptCoreLib;
using System.Windows.Forms;

namespace RayCaster1.source.java
{

    partial class RayCaster1Base
    {
        // implement: create a dungeon
        // http://www.aarg.net/~minam/dungeon.cgi
        // http://www.astrolog.org/labyrnth/algrithm.htm

        public int PlayerRadiusMargin = 10;

        public RayCaster1Base()
        {
            createTables();
        }

        public void runonce()
        {
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
                showStatus(CurrentMapPosition.ToString());









                //render();
       
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
        public KeyboardButton fKeyUp = new int[] { (int)Keys.Up, 'i', 'I', 'w', 'W' };
        public KeyboardButton fKeyDown = new int[] { (int)Keys.Down, 'k', 'K', 's', 'S' };
        public KeyboardButton fKeyLeft = new int[] { (int)Keys.Left, 'j', 'J', 'a', 'A' };
        public KeyboardButton fKeyRight = new int[] { (int)Keys.Right, 'l', 'L', 'd', 'D' };

        ////*******************************************************************//
        ////* Detect keypress
        ////*******************************************************************//
        //public override bool keyDown(Event evt, int key)
        //{
        //    fKeyUp.ProcessKeyDown(key);
        //    fKeyDown.ProcessKeyDown(key);
        //    fKeyLeft.ProcessKeyDown(key);
        //    fKeyRight.ProcessKeyDown(key);

        //    return true;
        //}

        ////*******************************************************************//
        ////* Detect key release
        ////*******************************************************************//
        //public override bool keyUp(Event evt, int key)
        //{
        //    fKeyUp.ProcessKeyUp(key);
        //    fKeyDown.ProcessKeyUp(key);
        //    fKeyLeft.ProcessKeyUp(key);
        //    fKeyRight.ProcessKeyUp(key);

        //    return true;
        //}




    }
}
