using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using java.applet;
using java.lang;
using java.awt;

namespace RayCaster1.source.java
{

    partial class RayCaster1Base
    {

        public int PlayerRadiusMargin = 4;

        //*******************************************************************//
        //* Running thread
        //*******************************************************************//
        public void run()
        {
            requestFocus();
            // create offscreen buffer
            fOffscreenImage = createImage(getSize().width, getSize().height);
            fOffscreenGraphics = fOffscreenImage.getGraphics();

            while (fThread != null)
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

                // stay inside map
                fPlayerX = fPlayerX.Max(0).Min(MAP_WIDTH * TILE_SIZE);
                fPlayerY = fPlayerY.Max(0).Min(MAP_HEIGHT * TILE_SIZE);

                // get walls

                if (CurrentMapTile != O)
                {
                    ClipPlayer();
                }
                else
                {
                    showStatus(CurrentMapPosition.ToString());
                }









                render();
                try
                {
                    Thread.sleep(50);
                }
                catch //(System.Exception sleepProblem)
                {
                    showStatus("Sleep problem");
                }
            }
        }

        private void ClipPlayer()
        {
            var PositionInWall =
                new PointInt32
                {
                    X = fPlayerX % TILE_SIZE,
                    Y = fPlayerY % TILE_SIZE
                };

            var A = PositionInWall.X > PositionInWall.Y;
            var B = PositionInWall.X > (TILE_SIZE - PositionInWall.Y);


            var ClipLeft = !A && !B;
            var ClipRight = A && B;
            if (ClipLeft)
                fPlayerX = fPlayerX.Min(CurrentMapPosition.X * TILE_SIZE - PlayerRadiusMargin);
            else if (ClipRight)
                fPlayerX = fPlayerX.Max((CurrentMapPosition.X + 1) * TILE_SIZE + PlayerRadiusMargin);

            //var ClipLeft = !A && !B;
            //var ClipRight = A && B;
            //var ClipBottom = !A && B;
            //var ClipTop = !A && B;

            //if (ClipLeft) showStatus("ClipLeft");
            //if (ClipTop) showStatus("ClipTop");
            //if (ClipBottom) showStatus("ClipBottom");


            showStatus("A " + A + " B " + B);
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
        protected bool fKeyUp = false;
        protected bool fKeyDown = false;
        protected bool fKeyLeft = false;
        protected bool fKeyRight = false;


        readonly int[] KeysUP = new[] { Event.UP, 'i', 'I', 'w', 'W' };
        readonly int[] KeysDOWN = new[] { Event.DOWN, 'k', 'K', 's', 'S' };
        readonly int[] KeysLEFT = new[] { Event.LEFT, 'j', 'J', 'a', 'A' };
        readonly int[] KeysRIGHT = new[] { Event.RIGHT, 'l', 'L', 'd', 'D' };

        //*******************************************************************//
        //* Detect keypress
        //*******************************************************************//
        public override bool keyDown(Event evt, int key)
        {
            var v = true;

            if (KeysUP.Contains(key)) fKeyUp = v;
            if (KeysDOWN.Contains(key)) fKeyDown = v;
            if (KeysLEFT.Contains(key)) fKeyLeft = v;
            if (KeysRIGHT.Contains(key)) fKeyRight = v;

            return true;
        }

        //*******************************************************************//
        //* Detect key release
        //*******************************************************************//
        public override bool keyUp(Event evt, int key)
        {
            var v = false;

            if (KeysUP.Contains(key)) fKeyUp = v;
            if (KeysDOWN.Contains(key)) fKeyDown = v;
            if (KeysLEFT.Contains(key)) fKeyLeft = v;
            if (KeysRIGHT.Contains(key)) fKeyRight = v;

            return true;
        }




    }
}
