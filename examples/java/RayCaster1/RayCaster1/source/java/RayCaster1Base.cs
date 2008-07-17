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
    [Script]
    public partial class RayCaster1Base : Applet, Runnable
    {
        // this is Java's stuff 
        protected Thread fThread;

        // size of tile (wall height)
        public static readonly int TILE_SIZE = 64;
        public static readonly int WALL_HEIGHT = 64;
        public static readonly int PROJECTIONPLANEWIDTH = RayCaster1.Settings.DefaultWidth - 100;
        public static readonly int PROJECTIONPLANEHEIGHT = RayCaster1.Settings.DefaultHeight;
        public static readonly int ANGLE60 = PROJECTIONPLANEWIDTH;
        public static readonly int ANGLE30 = (ANGLE60 / 2);
        public static readonly int ANGLE15 = (ANGLE30 / 2);
        public static readonly int ANGLE45 = ANGLE15 * 3;
        public static readonly int ANGLE90 = (ANGLE30 * 3);
        
        public static readonly int ANGLE180 = (ANGLE90 * 2);
        public static readonly int ANGLE270 = (ANGLE90 * 3);
        public static readonly int ANGLE360 = (ANGLE60 * 6);
        public static readonly int ANGLE0 = 0;
        public static readonly int ANGLE5 = (ANGLE30 / 6);
        public static readonly int ANGLE10 = (ANGLE5 * 2);

        // offscreen buffer
        protected Image fOffscreenImage;
        protected Graphics fOffscreenGraphics;



        // trigonometric tables
        protected double[] fSinTable;
        protected double[] fISinTable;
        protected double[] fCosTable;
        protected double[] fICosTable;
        protected double[] fTanTable;
        protected double[] fITanTable;
        protected double[] fFishTable;
        protected double[] fXStepTable;
        protected double[] fYStepTable;



        // player's attributes
        protected int fPlayerX = 100;
        protected int fPlayerY = 160;
        protected int fPlayerArc = ANGLE0;
        protected int fPlayerDistanceToTheProjectionPlane = 277;
        protected int fPlayerHeight = 32;
        protected int fPlayerSpeed = 8;
        protected int fProjectionPlaneYCenter = PROJECTIONPLANEHEIGHT / 2;
        // the following variables are used to keep the player coordinate in the overhead map
        protected int fPlayerMapX, fPlayerMapY, fMinimapWidth;


        /// <summary>
        /// wall
        /// </summary>
        protected static readonly sbyte W = 1;

        /// <summary>
        /// opening
        /// </summary>
        protected static readonly sbyte O = 0;

        protected static readonly int MAP_WIDTH = 14;
        protected static readonly int MAP_HEIGHT = 12;

        // 2 dimensional map
        Array2DSByte myMap;


        //*******************************************************************//
        //* Create tigonometric values to make the program runs faster.
        //*******************************************************************//
        public void createTables()
        {
            int i;
            double radian;
            fSinTable = new double[ANGLE360 + 1];
            fISinTable = new double[ANGLE360 + 1];
            fCosTable = new double[ANGLE360 + 1];
            fICosTable = new double[ANGLE360 + 1];
            fTanTable = new double[ANGLE360 + 1];
            fITanTable = new double[ANGLE360 + 1];
            fFishTable = new double[ANGLE60 + 1];
            fXStepTable = new double[ANGLE360 + 1];
            fYStepTable = new double[ANGLE360 + 1];

            for (i = 0; i <= ANGLE360; i++)
            {
                // get the radian value (the last addition is to avoid division by 0, try removing
                // that and you'll see a hole in the wall when a ray is at 0, 90, 180, or 270 degree)
                radian = arcToRad(i) + (double)(0.0001);
                fSinTable[i] = (double)Math.Sin(radian);
                fISinTable[i] = (1.0F / (fSinTable[i]));
                fCosTable[i] = (double)Math.Cos(radian);
                fICosTable[i] = (1.0F / (fCosTable[i]));
                fTanTable[i] = (double)Math.Tan(radian);
                fITanTable[i] = (1.0F / fTanTable[i]);

                //  you can see that the distance between xi is the same
                //  if we know the angle
                //  _____|_/next xi______________
                //       |
                //  ____/|next xi_________   slope = tan = height / dist between xi's
                //     / |
                //  __/__|_________  dist between xi = height/tan where height=tile size
                // old xi|
                //                  distance between xi = x_step[view_angle];
                //
                //
                // facine left
                // facing left
                if (i.IsBetweenIncluding(ANGLE90, ANGLE270))
                {
                    fXStepTable[i] = (double)(TILE_SIZE / fTanTable[i]);
                    if (fXStepTable[i] > 0)
                        fXStepTable[i] = -fXStepTable[i];
                }
                // facing right
                else
                {
                    fXStepTable[i] = (double)(TILE_SIZE / fTanTable[i]);
                    if (fXStepTable[i] < 0)
                        fXStepTable[i] = -fXStepTable[i];
                }

                // FACING DOWN
                if (i.IsBetweenIncluding(ANGLE0, ANGLE180))
                {
                    fYStepTable[i] = (double)(TILE_SIZE * fTanTable[i]);
                    if (fYStepTable[i] < 0)
                        fYStepTable[i] = -fYStepTable[i];
                }
                // FACING UP
                else
                {
                    fYStepTable[i] = (double)(TILE_SIZE * fTanTable[i]);
                    if (fYStepTable[i] > 0)
                        fYStepTable[i] = -fYStepTable[i];
                }
            }

            for (i = -ANGLE30; i <= ANGLE30; i++)
            {
                radian = arcToRad(i);
                // we don't have negative angle, so make it start at 0
                // this will give range 0 to 320
                fFishTable[i + ANGLE30] = (double)(1.0F / Math.Cos(radian));
            }



            myMap = new Array2DSByte(MAP_WIDTH, MAP_HEIGHT,
                W, W, W, W, W, W, W, W, W, W, W, W, W, W,
                W, O, O, O, O, O, O, O, O, O, W, O, O, W,
                W, O, O, O, O, O, O, O, O, O, W, O, O, W,
                W, O, O, O, O, O, O, O, W, O, W, W, O, W,
                W, O, O, W, O, W, O, O, W, O, O, O, O, W,
                W, O, O, W, O, W, W, O, W, O, W, W, O, W,
                W, O, O, W, O, O, W, O, W, O, W, W, O, W,
                W, O, O, O, W, O, W, O, W, O, W, O, O, W,
                W, O, O, O, W, O, W, O, W, O, W, O, O, W,
                W, O, O, O, W, W, W, O, W, O, W, W, O, W,
                W, O, O, O, O, O, O, O, O, O, O, O, O, W,
                W, W, W, W, W, W, W, W, W, W, W, W, W, W
            );

        }





        //*******************************************************************//
        //* Called when program starts
        //*******************************************************************//
        public void start()
        {
            createTables();
            fThread = new Thread(this);
            fThread.start();
        }

        //*******************************************************************//
        //* Called when leaving the page.
        //*******************************************************************//
        public void stop()
        {
            if (fThread == null)
                return;

            if (!fThread.isAlive())
                return;

            fThread = null;

            try
            {
                fThread.join();
            }
            catch
            {

            }

            // fThread.stop();
            
        }



        //*******************************************************************//
        //* Convert arc to radian
        //*******************************************************************//
        protected double arcToRad(double arcAngle)
        {
            return ((double)(arcAngle * System.Math.PI) / (double)ANGLE180);
        }

        //*******************************************************************//
        //* Called everytime applet need painting or whenever repaint is
        //*   called.
        //*******************************************************************//
        public override void paint(Graphics g)
        {
            if (fOffscreenImage != null)
                g.drawImage(fOffscreenImage, 0, 0, this);
        }


        //*******************************************************************//
        //* Draw background image
        //*******************************************************************//
        public void drawBackground()
        {
            // sky
            int c = 25;
            int r;
            for (r = 0; r < PROJECTIONPLANEHEIGHT / 2; r += 10)
            {
                var red = (int)(255.0 * r / PROJECTIONPLANEHEIGHT);

                fOffscreenGraphics.setColor(new Color(red, 125, 225));
                fOffscreenGraphics.fillRect(0, r, PROJECTIONPLANEWIDTH, 10);
                c += 20;
            }
            // ground
            c = 22;
            for (; r < PROJECTIONPLANEHEIGHT; r += 15)
            {
                var red2 = (int)(128.0 * r / PROJECTIONPLANEHEIGHT);

                fOffscreenGraphics.setColor(new Color(20, red2, 20));
                fOffscreenGraphics.fillRect(0, r, PROJECTIONPLANEWIDTH, 15);
                c += 15;
            }
        }



        //*******************************************************************//
        //* Draw map on the right side
        //*******************************************************************//
        public void drawOverheadMap()
        {
            fMinimapWidth = 8;
            for (int u = 0; u < MAP_WIDTH; u++)
            {
                for (int v = 0; v < MAP_HEIGHT; v++)
                {
                    if (myMap[u, v] == W)
                    {
                        fOffscreenGraphics.setColor(new Color(0x00ff00));
                    }
                    else
                    {
                        fOffscreenGraphics.setColor(new Color(0x002000));
                    }
                    fOffscreenGraphics.fillRect(PROJECTIONPLANEWIDTH + (u * fMinimapWidth),
                                (v * fMinimapWidth), fMinimapWidth, fMinimapWidth);
                }
            }
            fPlayerMapX = PROJECTIONPLANEWIDTH + (int)(((double)fPlayerX / (double)TILE_SIZE) * fMinimapWidth);
            fPlayerMapY = (int)(((double)fPlayerY / (double)TILE_SIZE) * fMinimapWidth);
        }


        //*******************************************************************//
        //* Draw ray on the overhead map (for illustartion purpose)
        //* This is not part of the ray-casting process
        //*******************************************************************//
        public void drawRayOnOverheadMap(double x, double y)
        {
            fOffscreenGraphics.setColor(new Color(0xffff00));
            // draw line from the player position to the position where the ray
            // intersect with wall
            fOffscreenGraphics.drawLine(fPlayerMapX, fPlayerMapY,
                    (int)(PROJECTIONPLANEWIDTH + ((double)(x * fMinimapWidth) / (double)TILE_SIZE)),
                    (int)(((double)(y * fMinimapWidth) / (double)TILE_SIZE)));
            // draw a red line indication the player's direction
            fOffscreenGraphics.setColor(new Color(0xff0000));
            fOffscreenGraphics.drawLine(fPlayerMapX, fPlayerMapY,
              (int)(fPlayerMapX + fCosTable[fPlayerArc] * 10),
                  (int)(fPlayerMapY + fSinTable[fPlayerArc] * 10));
        }








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
                    fOffscreenGraphics.setColor(new Color(0xa0a0a0));
                }
                // else, we use xray instead (meaning the vertical wall is closer than
                //   the horizontal wall)
                else
                {
                    // the next function call (drawRayOnMap()) is not a part of raycating rendering part, 
                    // it just draws the ray on the overhead map to illustrate the raycasting process
                    drawRayOnOverheadMap(verticalGrid, yIntersection);
                    dist = distToVerticalGridBeingHit;
                    fOffscreenGraphics.setColor(new Color(0x707070));
                }

                // correct distance (compensate for the fishbown effect)
                dist /= fFishTable[castColumn];
                // projected_wall_height/wall_height = fPlayerDistToProjectionPlane/dist;
                int projectedWallHeight = (int)(WALL_HEIGHT * (float)fPlayerDistanceToTheProjectionPlane / dist);
                bottomOfWall = fProjectionPlaneYCenter + (int)(projectedWallHeight * 0.5F);
                topOfWall = PROJECTIONPLANEHEIGHT - bottomOfWall;
                if (bottomOfWall >= PROJECTIONPLANEHEIGHT)
                    bottomOfWall = PROJECTIONPLANEHEIGHT - 1;
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

        private void BlitToScreen()
        {
            paint(this.getGraphics());
        }



    }
}
