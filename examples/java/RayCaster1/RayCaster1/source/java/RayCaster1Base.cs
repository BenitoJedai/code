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
        // http://www.permadi.com/java/rayc/Rayc.java

        // this is Java's stuff 
        protected Thread fThread;


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




        protected int fPlayerArc = ANGLE0;
        protected int fPlayerDistanceToTheProjectionPlane = 277;
        protected int fPlayerHeight = 32;
        protected int fPlayerSpeed = 8;
        protected int fProjectionPlaneYCenter = PROJECTIONPLANEHEIGHT / 2;
        // the following variables are used to keep the player coordinate in the overhead map
        protected int fPlayerMapX, fPlayerMapY, fMinimapWidth;




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

            //var Log =
            //     "Sin:\n" + fSinTable.ToDebugString() +
            //     "ISin:\n" + fISinTable.ToDebugString() +
            //     "Cos:\n" + fCosTable.ToDebugString() +
            //     "ICos:\n" + fICosTable.ToDebugString() +
            //     "Tan:\n" + fTanTable.ToDebugString() +
            //     "ITan:\n" + fITanTable.ToDebugString() +
            //     "Fish:\n" + fFishTable.ToDebugString() +
            //     "XStep:\n" + fXStepTable.ToDebugString() +
            //     "YStep:\n" + fYStepTable.ToDebugString();

            //Console.WriteLine(Log);

            CreateMap();

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
            fMinimapWidth = 5;
            for (int u = 0; u < MAP_WIDTH; u++)
            {
                for (int v = 0; v < MAP_HEIGHT; v++)
                {
                    if (myMap[u, v] != O)
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








        private void BlitToScreen()
        {
            paint(this.getGraphics());
        }



    }
}
