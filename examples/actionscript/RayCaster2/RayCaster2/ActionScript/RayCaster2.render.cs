using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.utils;
using System;
using System.Diagnostics;

namespace RayCaster2.ActionScript
{
    partial class RayCaster2
    {
        // !! this is a port from the RayCaster 1 java applet project

        static bool render2_DebugTrace_Assign_Active = true;

#if DebugTrace_Assign
        private static void render2_DebugTrace_Assign(string e)
        {
            if (render2_DebugTrace_Assign_Active)
                Console.WriteLine(e);
        }
#endif

        //*******************************************************************//
        //* Renderer
        //*******************************************************************//
        public void render2()
        {
            drawBackground();
            drawOverheadMap();

            //if (CurrentMapTile == W)
            //{
            //    // render nothing inside wall
            //    BlitToScreen();
            //    return;
            //}

            int verticalGrid;        // horizotal or vertical coordinate of intersection
            int horizontalGrid;      // theoritically, this will be multiple of TILE_SIZE
            // , but some trick did here might cause
            // the values off by 1
            int distToNextVerticalGrid; // how far to the next bound (this is multiple of
            int distToNextHorizontalGrid; // tile size)
            double xIntersection;  // x and y intersections
            double yIntersection;
            double distToNextXIntersection;
            double distToNextYIntersection;

            int xGridIndex;        // the current cell that the ray is in
            int yGridIndex;

            double distToVerticalGridBeingHit = 0;      // the distance of the x and y ray intersections from
            double distToHorizontalGridBeingHit = 0;      // the viewpoint

            sbyte VerticalGridBeingHit = 0;
            sbyte HorizontalGridBeingHit = 0;

            int castArc, castColumn;

            castArc = fPlayerArc;
            // field of view is 60 degree with the point of view (player's direction in the middle)
            // 30  30
            //    ^
            //  \ | /
            //   \|/
            //    v
            // we will trace the rays starting from the leftmost ray
            castArc -= ANGLE30;
            // wrap around if necessary
            if (castArc < 0)
            {
                castArc = ANGLE360 + castArc;
            }

            for (castColumn = 0; castColumn < PROJECTIONPLANEWIDTH; castColumn += 5)
            {
                // ray is between 0 to 180 degree (1st and 2nd quadrant)
                // ray is facing down
                //if (castArc > ANGLE0 && castArc < ANGLE180)
                if (castArc.IsBetween(ANGLE0, ANGLE180))
                {
                    // truncuate then add to get the coordinate of the FIRST grid (horizontal
                    // wall) that is in front of the player (this is in pixel unit)
                    // ROUND DOWN
                    horizontalGrid = (fPlayerY / TILE_SIZE).ToInt32() * TILE_SIZE + TILE_SIZE;

                    // compute distance to the next horizontal wall
                    distToNextHorizontalGrid = TILE_SIZE;

                    double xtemp = fITanTable[castArc] * (horizontalGrid - fPlayerY);
                    // we can get the vertical distance to that wall by
                    // (horizontalGrid-GLplayerY)
                    // we can get the horizontal distance to that wall by
                    // 1/tan(arc)*verticalDistance
                    // find the x interception to that wall
                    xIntersection = xtemp + fPlayerX;
                }
                // else, the ray is facing up
                else
                {
                    horizontalGrid = (fPlayerY / TILE_SIZE).ToInt32() * TILE_SIZE;
                    distToNextHorizontalGrid = -TILE_SIZE;

                    double xtemp = fITanTable[castArc] * (horizontalGrid - fPlayerY);
                    xIntersection = xtemp + fPlayerX;

                    horizontalGrid--;
                }
                // LOOK FOR HORIZONTAL WALL
                if (castArc.IsExact(ANGLE0, ANGLE180))
                {
                    distToHorizontalGridBeingHit = 9999999F;//Float.MAX_VALUE;
                }
                // else, move the ray until it hits a horizontal wall
                else
                {
                    distToNextXIntersection = fXStepTable[castArc];

                    #region loop1
                    var loop1 = true;
                    while (loop1)
                    {
                        xGridIndex = (int)(xIntersection / TILE_SIZE).Floor();
                        // in the picture, yGridIndex will be 1
                        yGridIndex = (horizontalGrid / TILE_SIZE).ToInt32();

                        if (myMap.ToRectInt32().IsOutSide(
                                new PointInt32
                                {
                                    X = xGridIndex,
                                    Y = yGridIndex
                                }
                            )
                            //  (xGridIndex >= MAP_WIDTH) ||
                            //(yGridIndex >= MAP_HEIGHT) ||
                            //xGridIndex < 0 || yGridIndex < 0
                            )
                        {
                            distToHorizontalGridBeingHit = float.MaxValue;
                            loop1 = false;
                        }
                        else if ((myMap[xGridIndex, yGridIndex]) != O)
                        {
                            distToHorizontalGridBeingHit = (xIntersection - fPlayerX) * fICosTable[castArc];
                            HorizontalGridBeingHit = myMap[xGridIndex, yGridIndex];
                            loop1 = false;
                        }
                        // else, the ray is not blocked, extend to the next block
                        else
                        {
                            xIntersection += distToNextXIntersection;
                            horizontalGrid += distToNextHorizontalGrid;
                        }
                    }
                    #endregion

                }


                // FOLLOW X RAY

                if (!castArc.IsBetween(ANGLE90, ANGLE270))
                //if (castArc < ANGLE90 || castArc > ANGLE270)
                {
                    verticalGrid = TILE_SIZE + (fPlayerX / TILE_SIZE).ToInt32() * TILE_SIZE;
                    distToNextVerticalGrid = TILE_SIZE;

                    double ytemp = fTanTable[castArc] * (verticalGrid - fPlayerX);
                    yIntersection = ytemp + fPlayerY;
                }
                // RAY FACING LEFT
                else
                {
                    verticalGrid = (fPlayerX / TILE_SIZE).ToInt32() * TILE_SIZE;
                    distToNextVerticalGrid = -TILE_SIZE;

                    double ytemp = fTanTable[castArc] * (verticalGrid - fPlayerX);
                    yIntersection = ytemp + fPlayerY;

                    verticalGrid--;
                }
                // LOOK FOR VERTICAL WALL
                if (castArc.IsExact(ANGLE90, ANGLE270))
                {
                    distToVerticalGridBeingHit = 9999999;//Float.MAX_VALUE;
                }
                else
                {
                    distToNextYIntersection = fYStepTable[castArc];
                    #region loop2
                    var loop2 = true;
                    while (loop2)
                    {
                        // compute current map position to inspect
                        xGridIndex = (verticalGrid / TILE_SIZE).ToInt32();
                        yGridIndex = (int)(yIntersection / TILE_SIZE).Floor();

                        if (myMap.ToRectInt32().IsOutSide(
                                new PointInt32
                                {
                                    X = xGridIndex,
                                    Y = yGridIndex
                                }
                            )

                          //  (xGridIndex >= MAP_WIDTH) ||
                            //(yGridIndex >= MAP_HEIGHT) ||
                            //xGridIndex < 0 || yGridIndex < 0
                            )
                        {
                            distToVerticalGridBeingHit = float.MaxValue;
                            loop2 = false;
                        }
                        else if ((myMap[xGridIndex, yGridIndex]) != O)
                        {
                            distToVerticalGridBeingHit = (yIntersection - fPlayerY) * fISinTable[castArc];
                            VerticalGridBeingHit = myMap[xGridIndex, yGridIndex];
                            loop2 = false;
                        }
                        else
                        {
                            yIntersection += distToNextYIntersection;
                            verticalGrid += distToNextVerticalGrid;
                        }
                    }
                    #endregion

                }

                // DRAW THE WALL SLICE
                double scaleFactor;
                double dist;
                int topOfWall;   // used to compute the top and bottom of the sliver that
                int bottomOfWall;   // will be the staring point of floor and ceiling
                // determine which ray strikes a closer wall.
                // if yray distance to the wall is closer, the yDistance will be shorter than
                // the xDistance
                if (distToHorizontalGridBeingHit < distToVerticalGridBeingHit)
                {
                    // the next function call (drawRayOnMap()) is not a part of raycating rendering part, 
                    // it just draws the ray on the overhead map to illustrate the raycasting process
                    drawRayOnOverheadMap(xIntersection, horizontalGrid);
                    dist = distToHorizontalGridBeingHit;
                }
                // else, we use xray instead (meaning the vertical wall is closer than
                //   the horizontal wall)
                else
                {
                    // the next function call (drawRayOnMap()) is not a part of raycating rendering part, 
                    // it just draws the ray on the overhead map to illustrate the raycasting process
                    drawRayOnOverheadMap(verticalGrid, yIntersection);
                    dist = distToVerticalGridBeingHit;
                }

                // correct distance (compensate for the fishbown effect)
                dist_eq_32_00(dist.FuzzyEquals(32.00, 0.01),
                    new
                    {
                        dist,
                        castColumn,
                        fFishTable = fFishTable[castColumn],
                        xIntersection,
                        horizontalGrid,
                        distToHorizontalGridBeingHit,
                        verticalGrid,
                        yIntersection,
                        distToVerticalGridBeingHit
                    }.ToString());
                dist /= fFishTable[castColumn];
                // projected_wall_height/wall_height = fPlayerDistToProjectionPlane/dist;
                var projectedWallHeightPercentage = (double)fPlayerDistanceToTheProjectionPlane / dist;
                projectedWallHeightPercentage_eq_9_99(projectedWallHeightPercentage.FuzzyEquals(9.99, 0.01), new { fPlayerDistanceToTheProjectionPlane, dist, projectedWallHeightPercentage }.ToString());

                int projectedWallHeight = (int)(WALL_HEIGHT * projectedWallHeightPercentage);
                projectedWallHeight_eq_639(projectedWallHeight == 639, new { WALL_HEIGHT, projectedWallHeightPercentage, projectedWallHeight }.ToString());


                bottomOfWall = fProjectionPlaneYCenter + (int)(projectedWallHeight * 0.5F);
                bottomOfWall_eq_559(bottomOfWall == 559, new { fProjectionPlaneYCenter, projectedWallHeight, bottomOfWall }.ToString());

                topOfWall = PROJECTIONPLANEHEIGHT - bottomOfWall;
                topOfWall_eq_79(topOfWall == 79, new { PROJECTIONPLANEHEIGHT, bottomOfWall, topOfWall }.ToString());


                if (bottomOfWall >= PROJECTIONPLANEHEIGHT)
                    bottomOfWall = PROJECTIONPLANEHEIGHT - 1;

                #region get color
                if (distToHorizontalGridBeingHit < distToVerticalGridBeingHit)
                {
                    if (HorizontalGridBeingHit != W)
                    {
                        fOffscreenGraphics.beginFill(GetWallColor(HorizontalGridBeingHit, true));
                    }
                    else
                    {
                        GrayColor g = 0xa0 * projectedWallHeightPercentage.Min(1);

                        fOffscreenGraphics.beginFill(g);
                    }
                }
                else
                {
                    if (VerticalGridBeingHit != W)
                    {
                        fOffscreenGraphics.beginFill(GetWallColor(VerticalGridBeingHit, false));
                    }
                    else
                    {
                        GrayColor g = 0x70 * projectedWallHeightPercentage.Min(1);

                        fOffscreenGraphics.beginFill(g);
                    }
                }
                #endregion

                //fOffscreenGraphics.drawLine(castColumn, topOfWall, castColumn, bottomOfWall);
                fOffscreenGraphics.lineStyle(0, 0, 0);
                fOffscreenGraphics.drawRect(castColumn, topOfWall, 5, projectedWallHeight);



                // y = 79;
                // h = 639;

                // TRACE THE NEXT RAY
                castArc += 5;
                if (castArc >= ANGLE360)
                    castArc -= ANGLE360;
            }


            // blit to screen

            // freeze
            //fThread.stop();

            render2_DebugTrace_Assign_Active = false;
        }

        Action<bool, string> dist_eq_32_00 = new AssertOnce { Message = "dist_eq_32_00" };
        Action<bool, string> projectedWallHeightPercentage_eq_9_99 = new AssertOnce { Message = "projectedWallHeightPercentage_eq_9_99" };
        Action<bool, string> projectedWallHeight_eq_639 = new AssertOnce { Message = "projectedWallHeight_eq_639" };
        Action<bool, string> bottomOfWall_eq_559 = new AssertOnce { Message = "bottomOfWall_eq_559" };
        Action<bool, string> topOfWall_eq_79 = new AssertOnce { Message = "topOfWall_eq_79" };

        [Script]
        class AssertOnce
        {
            bool Once;

            public string Message;

            public void Assert(bool v, string message)
            {
                if (Once)
                    return;
                Once = true;

                if (v)
                    return;


                Console.WriteLine("Assert: " + Message + " " + message);


                //throw new Exception(Message + " " + message);
            }

            public static implicit operator Action<bool, string>(AssertOnce e)
            {
                return e.Assert;
            }
        }
    }
}