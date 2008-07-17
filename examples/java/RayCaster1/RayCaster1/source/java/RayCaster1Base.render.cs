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

        //*******************************************************************//
        //* Renderer
        //*******************************************************************//
        public void render()
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
                    horizontalGrid = (fPlayerY / TILE_SIZE) * TILE_SIZE + TILE_SIZE;

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
                    horizontalGrid = (fPlayerY / TILE_SIZE) * TILE_SIZE;
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
                        xGridIndex = (int)(xIntersection / TILE_SIZE);
                        // in the picture, yGridIndex will be 1
                        yGridIndex = (horizontalGrid / TILE_SIZE);

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
                    verticalGrid = TILE_SIZE + (fPlayerX / TILE_SIZE) * TILE_SIZE;
                    distToNextVerticalGrid = TILE_SIZE;

                    double ytemp = fTanTable[castArc] * (verticalGrid - fPlayerX);
                    yIntersection = ytemp + fPlayerY;
                }
                // RAY FACING LEFT
                else
                {
                    verticalGrid = (fPlayerX / TILE_SIZE) * TILE_SIZE;
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
                        xGridIndex = (verticalGrid / TILE_SIZE);
                        yGridIndex = (int)(yIntersection / TILE_SIZE);

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
                dist /= fFishTable[castColumn];
                // projected_wall_height/wall_height = fPlayerDistToProjectionPlane/dist;
                var projectedWallHeightPercentage = (double)fPlayerDistanceToTheProjectionPlane / dist;

                int projectedWallHeight = (int)(WALL_HEIGHT * projectedWallHeightPercentage);
                bottomOfWall = fProjectionPlaneYCenter + (int)(projectedWallHeight * 0.5F);
                topOfWall = PROJECTIONPLANEHEIGHT - bottomOfWall;
                if (bottomOfWall >= PROJECTIONPLANEHEIGHT)
                    bottomOfWall = PROJECTIONPLANEHEIGHT - 1;

                if (distToHorizontalGridBeingHit < distToVerticalGridBeingHit)
                {
                    if (HorizontalGridBeingHit != W)
                    {
                        fOffscreenGraphics.setColor(GetWallColor(HorizontalGridBeingHit, true));
                    }
                    else
                    {
                        GrayColor g = 0xa0 * projectedWallHeightPercentage.Min(1);

                        fOffscreenGraphics.setColor(g);
                    }
                }
                else
                {
                    if (VerticalGridBeingHit != W)
                    {
                        fOffscreenGraphics.setColor(GetWallColor(VerticalGridBeingHit, false));
                    }
                    else
                    {
                        GrayColor g = 0x70 * projectedWallHeightPercentage.Min(1);

                        fOffscreenGraphics.setColor(g);
                    }
                }

                //fOffscreenGraphics.drawLine(castColumn, topOfWall, castColumn, bottomOfWall);
                fOffscreenGraphics.fillRect(castColumn, topOfWall, 5, projectedWallHeight);

                // TRACE THE NEXT RAY
                castArc += 5;
                if (castArc >= ANGLE360)
                    castArc -= ANGLE360;
            }

            // blit to screen
            BlitToScreen();
        }






    }
}
