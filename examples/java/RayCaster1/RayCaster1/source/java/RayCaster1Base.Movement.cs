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

        public int PlayerRadiusMargin = 10;

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

         
                // get walls

                ClipPlayer();
                showStatus(CurrentMapPosition.ToString());









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
            // stay inside map
            fPlayerX = fPlayerX.Max(0).Min(MAP_WIDTH * TILE_SIZE);
            fPlayerY = fPlayerY.Max(0).Min(MAP_HEIGHT * TILE_SIZE);


            var PositionInWall =
                new PointInt32
                {
                    X = fPlayerX % TILE_SIZE,
                    Y = fPlayerY % TILE_SIZE
                };

            var TileTop = myMap[CurrentMapPosition.X, CurrentMapPosition.Y - 1];
            var TileLeft = myMap[CurrentMapPosition.X - 1, CurrentMapPosition.Y];
            var TileRight = myMap[CurrentMapPosition.X + 1, CurrentMapPosition.Y];
            var TileBottom = myMap[CurrentMapPosition.X, CurrentMapPosition.Y + 1];

            var c = CurrentMapPosition;

            if (CurrentMapTile != O)
            {
                // we are inside a wall
                // push us out of there

                #region Dia
                var A = PositionInWall.X > PositionInWall.Y;
                var B = PositionInWall.X > (TILE_SIZE - PositionInWall.Y);

                var DiaClipLeft = !A && !B;
                var DiaClipRight = A && B;
                var DiaClipTop = A && !B;
                var DiaClipBottom = !A && B;
                #endregion

                #region Alt
                var C = PositionInWall.X > (TILE_SIZE / 2);
                var D = PositionInWall.Y > (TILE_SIZE / 2);

                var AltClipTopLeft = !C && !D;
                var AltClipBottomLeft = !C && D;
                var AltClipTopRight = C && !D;
                var AltClipBottomRight = C && D;
                #endregion


                var ClipLeft = DiaClipLeft;

                if (TileTop != O) ClipLeft |= AltClipTopLeft;
                if (TileBottom != O) ClipLeft |= AltClipBottomLeft;

                var ClipRight = DiaClipRight;

                if (TileTop != O) ClipRight |= AltClipTopRight;
                if (TileBottom != O) ClipRight |= AltClipBottomRight;

                var ClipTop = DiaClipTop;

                if (TileLeft != O) ClipTop |= AltClipTopLeft;
                if (TileRight != O) ClipTop |= AltClipTopRight;

                var ClipBottom = DiaClipBottom;

                if (TileLeft != O) ClipBottom |= AltClipBottomLeft;
                if (TileRight != O) ClipBottom |= AltClipBottomRight;

                

                if (ClipLeft)
                    fPlayerX = fPlayerX.Min(c.X * TILE_SIZE - PlayerRadiusMargin);
                else if (ClipRight)
                    fPlayerX = fPlayerX.Max((c.X + 1) * TILE_SIZE + PlayerRadiusMargin);

                if (ClipTop)
                    fPlayerY = fPlayerY.Min(c.Y * TILE_SIZE - PlayerRadiusMargin);
                else if (ClipRight)
                    fPlayerY = fPlayerY.Max((c.Y + 1) * TILE_SIZE + PlayerRadiusMargin);
            }
            else
            {
                // fix corners
                //
                //   *        L
                //    F      *

                if (TileLeft != O)
                    fPlayerX = fPlayerX.Max(c.X * TILE_SIZE + PlayerRadiusMargin);
                if (TileRight != O)
                    fPlayerX = fPlayerX.Min((c.X + 1) * TILE_SIZE - PlayerRadiusMargin);
                
                if (TileTop != O)
                    fPlayerY = fPlayerY.Max(c.Y * TILE_SIZE + PlayerRadiusMargin);
                if (TileBottom != O)
                    fPlayerY = fPlayerY.Min((c.Y + 1) * TILE_SIZE - PlayerRadiusMargin);

            }


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

        [Script]
        public class KeyboardButton
        {
            public int[] Buttons;

            public bool IsPressed;

            public static implicit operator bool(KeyboardButton b)
            {
                return b.IsPressed;
            }

            public static implicit operator KeyboardButton(int[] b)
            {
                return new KeyboardButton { Buttons = b };
            }

            public bool ProcessKeyDown(int key)
            {
                if (Buttons.Contains(key))
                {
                    this.IsPressed = true;
                    return true;
                }

                return false;
            }

            public bool ProcessKeyUp(int key)
            {
                if (Buttons.Contains(key))
                {
                    this.IsPressed = false;
                    return true;
                }

                return false;
            }
        }

        // movement flag
        protected KeyboardButton fKeyUp = new int[] { Event.UP, 'i', 'I', 'w', 'W' };
        protected KeyboardButton fKeyDown = new int[] { Event.DOWN, 'k', 'K', 's', 'S' };
        protected KeyboardButton fKeyLeft = new int[] { Event.LEFT, 'j', 'J', 'a', 'A' };
        protected KeyboardButton fKeyRight = new int[] { Event.RIGHT, 'l', 'L', 'd', 'D' };


        //readonly int[] KeysUP = new int[] { Event.UP, 'i', 'I', 'w', 'W' };
        //readonly int[] KeysDOWN = new int[] { Event.DOWN, 'k', 'K', 's', 'S' };
        //readonly int[] KeysLEFT = new int[] { Event.LEFT, 'j', 'J', 'a', 'A' };
        //readonly int[] KeysRIGHT = new int [] { Event.RIGHT, 'l', 'L', 'd', 'D' };

        //*******************************************************************//
        //* Detect keypress
        //*******************************************************************//
        public override bool keyDown(Event evt, int key)
        {
            fKeyUp.ProcessKeyDown(key);
            fKeyDown.ProcessKeyDown(key);
            fKeyLeft.ProcessKeyDown(key);
            fKeyRight.ProcessKeyDown(key);

            return true;
        }

        //*******************************************************************//
        //* Detect key release
        //*******************************************************************//
        public override bool keyUp(Event evt, int key)
        {
            fKeyUp.ProcessKeyUp(key);
            fKeyDown.ProcessKeyUp(key);
            fKeyLeft.ProcessKeyUp(key);
            fKeyRight.ProcessKeyUp(key);

            return true;
        }




    }
}
