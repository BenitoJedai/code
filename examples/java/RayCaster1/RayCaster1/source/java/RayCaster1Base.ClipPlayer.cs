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





    }
}
