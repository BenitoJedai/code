using ScriptCoreLib;
using System.Collections.Generic;
using System;
using System.Windows.Forms;
using System.Drawing;




namespace RayCaster4.ActionScript
{


    //[ScriptImportsType("flash.utils.getTimer")]
    [Script]
    public class RayCaster4base //: Sprite
    {
        // http://livedocs.adobe.com/flex/2/langref/flash/utils/package.html#getTimer()
        internal static int getTimer()
        {
            return (int)DateTime.Now.Ticks;
        }

        public Label txtMain;
        //protected TextFormat tfNormal;
        //protected Loader bitmapLoader;
        //protected BitmapData image;
        //protected Sprite imageCont;
        protected string[] textureFiles;
        protected int textureLoadNum;
        protected uint[][][] textures;
        //protected double[] floorVals;

        protected double posX;
        protected double posY;  //x and y start position
        protected double dirX;
        protected double dirY; //initial direction vector
        protected double planeX;
        protected double planeY; //the 2d raycaster version of camera plane
        protected int w;
        protected int h;

        protected double moveSpeed;
        protected double rotSpeed;
        protected int texWidth;
        protected int texHeight;
        public Bitmap screen;
        //protected Bitmap screenImage;
        //protected Sprite[] sprites; // ?
        protected double[] ZBuffer;
        protected int time;
        protected int counter;

        protected int[][] worldMap;

        protected int activeKey;

        [Script(NoDecoration = true)]
        protected void setWorldMap()
        {
            worldMap =
                new[]
                {
			new [] {8,8,8,8,8,8,8,8,8,8,8,4,4,6,4,4,6,4,6,4,4,4,6,4},
			new [] {8,0,0,0,0,0,0,0,0,0,8,4,0,0,0,0,0,0,0,0,0,0,0,4},
			new [] {8,0,3,3,0,0,0,0,0,8,8,4,0,0,0,0,0,0,0,0,0,0,0,6},
			new [] {8,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6},
			new [] {8,0,3,3,0,0,0,0,0,8,8,4,0,0,0,0,0,0,0,0,0,0,0,4},
			new [] {8,0,0,0,0,0,0,0,0,0,8,4,0,0,0,0,0,6,6,6,0,6,4,6},
			new [] {8,8,8,8,0,8,8,8,8,8,8,4,4,4,4,4,4,6,0,0,0,0,0,6},
			new [] {7,7,7,7,0,7,7,7,7,0,8,0,8,0,8,0,8,4,0,4,0,6,0,6},
			new [] {7,7,0,0,0,0,0,0,7,8,0,8,0,8,0,8,8,6,0,0,0,0,0,6},
			new [] {7,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,8,6,0,0,0,0,0,4},
			new [] {7,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,8,6,0,6,0,6,0,6},
			new [] {7,7,0,0,0,0,0,0,7,8,0,8,0,8,0,8,8,6,4,6,0,6,6,6},
			new [] {7,7,7,7,0,7,7,7,7,8,8,4,0,6,8,4,8,3,3,3,0,3,3,3},
			new [] {2,2,2,2,0,2,2,2,2,4,6,4,0,0,6,0,6,3,0,0,0,0,0,3},
			new [] {2,2,0,0,0,0,0,2,2,4,0,0,0,0,0,0,4,3,0,0,0,0,0,3},
			new [] {2,0,0,0,0,0,0,0,2,4,0,0,0,0,0,0,4,3,0,0,0,0,0,3},
			new [] {1,0,0,0,0,0,0,0,1,4,4,4,4,4,6,0,6,3,3,0,0,0,3,3},
			new [] {2,0,0,0,0,0,0,0,2,2,2,1,2,2,2,6,6,0,0,5,0,5,0,5},
			new [] {2,2,0,0,0,0,0,2,2,2,0,0,0,2,2,0,5,0,5,0,0,0,5,5},
			new [] {2,0,0,0,0,0,0,0,2,0,0,0,0,0,2,5,0,5,0,5,0,5,0,5},
			new [] {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5},
			new [] {2,0,0,0,0,0,0,0,2,0,0,0,0,0,2,5,0,5,0,5,0,5,0,5},
			new [] {2,2,0,0,0,0,0,2,2,2,0,0,0,2,2,0,5,0,5,0,0,0,5,5},
			new [] {2,2,2,2,1,2,2,2,2,2,2,1,2,2,2,5,5,5,5,5,5,5,5,5}
                };
        }



        public RayCaster4base()
        {
            //stage.keyDown +=
            //    e =>
            //    {
            //        var key = e.keyCode;

            //        fKeyUp.ProcessKeyDown(key);
            //        fKeyDown.ProcessKeyDown(key);
            //        fKeyLeft.ProcessKeyDown(key);
            //        fKeyRight.ProcessKeyDown(key);
            //    };

            //stage.keyUp +=
            //    e =>
            //    {
            //        var key = e.keyCode;

            //        fKeyUp.ProcessKeyUp(key);
            //        fKeyDown.ProcessKeyUp(key);
            //        fKeyLeft.ProcessKeyUp(key);
            //        fKeyRight.ProcessKeyUp(key);
            //    };

            textureFiles = new[] { "wall.jpg", "tech2.jpg", "roof.jpg" };

            textures = new uint[textureFiles.Length][][];
            textureLoadNum = 0;
            //bitmapLoader = new Loader();
            //bitmapLoader.load(new URLRequest("flashsrc/textures/" + textureFiles[textureLoadNum]));

            onBitmapLoaded(Image.FromFile("wall.jpg"));
            onBitmapLoaded(Image.FromFile("tech2.jpg"));
            onBitmapLoaded(Image.FromFile("roof.jpg"));

           
        }

        private void onBitmapLoaded(Image bd)
        {
            //var bd = (Bitmap)(bitmapLoader.getChildAt(0));
            var bdata = new Bitmap(bd);

            textures[textureLoadNum] = new uint[256][];
            for (var j = 0; j < 256; j++)
            {
                textures[textureLoadNum][j] = new uint[256];
                for (var k = 0; k < 256; k++)
                {
                    textures[textureLoadNum][j][k] = bdata.GetPixel(j, k).ToArgb().ToUInt32() & 0xffffff;
                }
            }

            textureLoadNum++;

        }

        [Script(NoDecoration = true)]
        protected uint RGBToInt(int[] rgb)
        {
            var r = ((rgb[0]) << 16).ToUInt32();
            var g = ((rgb[1]) << 8).ToUInt32();
            var b = ((rgb[2])).ToUInt32();
            return r | g | b;
        }

        [Script(NoDecoration = true)]
        protected uint[] intToRGB(uint number)
        {
            var b = number & 255; number >>= 8;
            var g = number & 255; number >>= 8;
            var r = number;
            return new[] { r, g, b };
        }

        // movement flag
        public KeyboardButton fKeyUp = new int[] { (int)Keys.Up, 'i', 'I', 'w', 'W' };
        public KeyboardButton fKeyDown = new int[] { (int)Keys.Down, 'k', 'K', 's', 'S' };
        public KeyboardButton fKeyLeft = new int[] { (int)Keys.Left, 'j', 'J', 'a', 'A' };
        public KeyboardButton fKeyRight = new int[] { (int)Keys.Right, 'l', 'L', 'd', 'D' };



        [Script(NoDecoration = true)]
        public void prepare()
        {
            //stage.align = StageAlign.TOP_LEFT;
            //stage.quality 	= StageQuality.LOW;

            //txtMain = new TextField();
            //tfNormal = new TextFormat();
            //tfNormal.font = "Verdana";
            //tfNormal.align = TextFormatAlign.LEFT;
            //tfNormal.size = 10;
            //tfNormal.color = 0xffffff;
            //txtMain.defaultTextFormat = tfNormal;
            //txtMain.autoSize = "left";
            //txtMain.appendText("0");

            moveSpeed = 0.2;
            rotSpeed = 0.12;
            texWidth = 256;
            texHeight = 256;
            posX = 22;
            posY = 11.5;
            dirX = -1;
            dirY = 0;
            planeX = 0;
            planeY = 0.66;
            w = 320;
            h = 240;
            time = getTimer();
            setWorldMap();

            //floorVals = new[] {
            //    80,40,26.6666666666667,20,16,13.3333333333333,11.4285714285714,10,8.88888888888889,8,7.27272727272727,6.66666666666667,6.15384615384615,5.71428571428571,5.33333333333333,5,4.70588235294118,4.44444444444444,4.21052631578947,4,3.80952380952381,3.63636363636364,3.47826086956522,3.33333333333333,3.2,3.07692307692308,2.96296296296296,2.85714285714286,2.75862068965517,2.66666666666667,2.58064516129032,2.5,2.42424242424242,2.35294117647059,2.28571428571429,2.22222222222222,2.16216216216216,2.10526315789474,2.05128205128205,2,
            //1.95121951219512,1.9047619047619,1.86046511627907,1.81818181818182,1.77777777777778,1.73913043478261,1.70212765957447,1.66666666666667,1.63265306122449,1.6,1.56862745098039,1.53846153846154,1.50943396226415,1.48148148148148,1.45454545454545,1.42857142857143,1.40350877192982,1.37931034482759,1.35593220338983,1.33333333333333,1.31147540983607,1.29032258064516,1.26984126984127,1.25,1.23076923076923,1.21212121212121,1.19402985074627,1.17647058823529,1.15942028985507,1.14285714285714,1.12676056338028,1.11111111111111,1.0958904109589,1.08108108108108,1.06666666666667,1.05263157894737,1.03896103896104,1.02564102564103,1.0126582278481 };

            time = getTimer();
            counter = 0;

            ZBuffer = new double[320];

            screen = new Bitmap(w, h);
            //screenImage = new Image();
            //screenImage.bitmapData = screen;

            //addChild(screenImage);
            //addChild(txtMain);

            //this.enterFrame += render;

            //addEventListener(Event.ENTER_FRAME, render);

            render(null);
        }

        static bool render_DebugTrace_Assign_Active = true;

        #if DebugTrace_Assign
        private static void render_DebugTrace_Assign(string e)
        {
            if (render_DebugTrace_Assign_Active)
                Console.WriteLine(e);
        }
        #endif

        [Script(NoDecoration = true)]
        public new void render(object e)
        {
            /* 			try {
                            screen.dispose();
                            screen = new BitmapData( w, h, false, 0x0 );
                            screen.lock();
                        } catch(e:Error) {
                            trace("err");
                        } */

            //screen.floodFill(0, 0, 0x0);
            //var screenData = screen.@lock();
            
            int x;
            int y;
            DoMovement();

            x = 0;
            while (x < w)
            {

                //calculate ray position and direction
                double cameraX = 2.0 * (double)x / (double)w - 1.0; //x-coordinate in camera space
                double rayPosX = posX;
                double rayPosY = posY;
                double rayDirX = dirX + planeX * cameraX;
                double rayDirY = dirY + planeY * cameraX;

                //which box of the map we're in
                var mapX = (rayPosX).Floor();
                var mapY = (rayPosY).Floor();

                //length of ray from current position to next x or y-side
                double sideDistX;
                double sideDistY;

                //length of ray from one x or y-side to next x or y-side
                var deltaDistX = Math.Sqrt(1 + (rayDirY * rayDirY) / (rayDirX * rayDirX));
                var deltaDistY = Math.Sqrt(1 + (rayDirX * rayDirX) / (rayDirY * rayDirY));

                //what direction to step in x or y-direction (either +1 or -1)
                int stepX;
                int stepY;

                //calculate step and initial sideDist
                if (rayDirX < 0)
                {
                    stepX = -1;
                    sideDistX = (rayPosX - mapX) * deltaDistX;
                }
                else
                {
                    stepX = 1;
                    sideDistX = (mapX + 1.0 - rayPosX) * deltaDistX;
                }
                if (rayDirY < 0)
                {
                    stepY = -1;
                    sideDistY = (rayPosY - mapY) * deltaDistY;
                }
                else
                {
                    stepY = 1;
                    sideDistY = (mapY + 1.0 - rayPosY) * deltaDistY;
                }

                double hit = 0; //was there a wall hit?
                var side = default(int); //was a NS or a EW wall hit?

                while (hit == 0)
                { //perform DDA                 
                    //jump to next map square, OR in x-direction, OR in y-direction
                    if (sideDistX < sideDistY)
                    {
                        sideDistX += deltaDistX;
                        mapX += stepX;
                        side = 0;
                    }
                    else
                    {
                        sideDistY += deltaDistY;
                        mapY += stepY;
                        side = 1;
                    }
                    if (worldMap[mapX][mapY] > 0)
                    {
                        hit = 1; //Check if ray has hit a wall   
                    }
                }

                //Calculate distance projected on camera direction (oblique distance will give fisheye effect!)
                double perpWallDist;
                if (side == 0)
                {
                    perpWallDist = Math.Abs((mapX - rayPosX + (1 - stepX) / 2.0) / rayDirX);
                }
                else
                {
                    perpWallDist = Math.Abs((mapY - rayPosY + (1 - stepY) / 2.0) / rayDirY);
                }

                //Calculate height of line to draw on screen
                var lineHeight = Math.Abs((h / perpWallDist).Floor());

                //calculate lowest and highest pixel to fill in current stripe
                var drawStart = (-lineHeight / 2 + h / 2).Floor();
                if (drawStart < 0) drawStart = 0;
                var drawEnd = (lineHeight / 2 + h / 2).Floor();
                if (drawEnd >= h) drawEnd = h;

                var texNum = worldMap[mapX][mapY] - 1; //1 subtracted from it so that texture 0 can be used!
                texNum = 0;

                //calculate value of wallX
                double wallX; //where exactly the wall was hit
                if (side == 1)
                {
                    wallX = rayPosX + ((mapY - rayPosY + (1 - stepY) / 2) / rayDirY) * rayDirX;
                }
                else
                {
                    wallX = rayPosY + ((mapX - rayPosX + (1 - stepX) / 2) / rayDirX) * rayDirY;
                }
                wallX -= Math.Floor((wallX));

                //x coordinate on the texture
                var texX = (wallX * texWidth).Floor();
                if (side == 0)
                    if (rayDirX > 0) texX = texWidth - texX - 1;
                if (side == 1)
                    if (rayDirY < 0) texX = texWidth - texX - 1;

                var hT = h * 128;
                var lhT = lineHeight * 128;

                y = drawStart;

                while (y < drawEnd)
                {

                    var d = y * 256 - hT + lhT;  //256 and 128 factors to avoid floats
                    var texY = ((d * texHeight) / lineHeight) / 256;

                    var color = textures[texNum][(texX).Floor()][(texY).Floor()];

                    if (side == 1) color = (color >> 1) & 0x7F7F7F;

                    //color = 0xff0000;

                    screen.setPixel(x, y, color);

                    y++;
                }

                //SET THE ZBUFFER FOR THE SPRITE CASTING
                ZBuffer[x] = perpWallDist; //perpendicular distance is used

                //floor casting    
                double floorXWall;
                double floorYWall; //x, y position of the floor texel at the bottom of the wall

                //4 different wall directions possible
                if (side == 0)
                {
                    if (rayDirX > 0)
                    {
                        floorXWall = mapX;
                        floorYWall = mapY + wallX;
                    }
                    else
                    {
                        floorXWall = mapX + 1.0;
                        floorYWall = mapY + wallX;
                    }
                }
                else
                {
                    if (rayDirY > 0)
                    {
                        floorXWall = mapX + wallX;
                        floorYWall = mapY;
                    }
                    else
                    {
                        floorXWall = mapX + wallX;
                        floorYWall = mapY + 1.0;
                    }
                }


                var distWall = perpWallDist;
                var distPlayer = 0.0;
                var currentDist = 0.0;

                if (drawEnd < 0) drawEnd = h; //becomes < 0 when the integer overflows

                //draw the floor from drawEnd to the bottom of the screen
                #region draw flood
                y = drawEnd;
                double weight;
                double currentFloorX;
                double currentFloorY;
                int floorTexX;
                int floorTexY;

                while (y < h)
                {

                    currentDist = (double)h / ((double)2 * (double)y - (double)h); //you could make a small lookup table for this instead
                    //currentDist = floorVals[int(y-80)];

                    weight = (currentDist - distPlayer) / (distWall - distPlayer);

                    currentFloorX = weight * floorXWall + (1.0 - weight) * posX;
                    currentFloorY = weight * floorYWall + (1.0 - weight) * posY;

                    floorTexX = (currentFloorX * texWidth).Floor() % texWidth;
                    floorTexY = (currentFloorY * texHeight).Floor() % texHeight;

                    try
                    {
                        var color = textures[1][floorTexX][floorTexY];

                        //color = 0xff;

                        screen.setPixel(x, y, color); //floor
                    }
                    catch
                    {
                        //trace("err");
                    }

                    try
                    {
                        var color = textures[2][floorTexX][floorTexY];

                        //color = 0xff00;
                        //color = 0xff0000;

                        screen.setPixel(x, h - y - 1, color); //ceiling (symmetrical!)
                    }
                    catch
                    {
                        //trace("err");
                    }

                    y++;
                }
                #endregion

                x++;

                if (x > 4)
                    render_DebugTrace_Assign_Active = false;
            }

            counter++;

            if (getTimer() - 1000 >= time)
            {
                txtMain.Text = counter.ToString();
                counter = 0;
                time = getTimer();
            }

            //screenImage.bitmapData = screen;
            //screen.UnlockBits(screenData);

        }

        private void DoMovement()
        {
            double oldDirX;
            double oldPlaneX;

            if (fKeyUp)
            {
                if (worldMap[(posX + dirX * moveSpeed).Floor()][(posY).Floor()] == 0) posX += dirX * moveSpeed;
                if (worldMap[(posX).Floor()][(posY + dirY * moveSpeed).Floor()] == 0) posY += dirY * moveSpeed;
            }
            if (fKeyDown)
            {
                if (worldMap[(posX - dirX * moveSpeed).Floor()][(posY).Floor()] == 0) posX -= dirX * moveSpeed;
                if (worldMap[(posX).Floor()][(posY - dirY * moveSpeed).Floor()] == 0) posY -= dirY * moveSpeed;
            }
            if (fKeyRight)
            { //both camera direction and camera plane must be rotated
                oldDirX = dirX;
                dirX = dirX * Math.Cos(-rotSpeed) - dirY * Math.Sin(-rotSpeed);
                dirY = oldDirX * Math.Sin(-rotSpeed) + dirY * Math.Cos(-rotSpeed);
                oldPlaneX = planeX;
                planeX = planeX * Math.Cos(-rotSpeed) - planeY * Math.Sin(-rotSpeed);
                planeY = oldPlaneX * Math.Sin(-rotSpeed) + planeY * Math.Cos(-rotSpeed);
            }
            if (fKeyLeft)
            { //both camera direction and camera plane must be rotated
                oldDirX = dirX;
                dirX = dirX * Math.Cos(rotSpeed) - dirY * Math.Sin(rotSpeed);
                dirY = oldDirX * Math.Sin(rotSpeed) + dirY * Math.Cos(rotSpeed);
                oldPlaneX = planeX;
                planeX = planeX * Math.Cos(rotSpeed) - planeY * Math.Sin(rotSpeed);
                planeY = oldPlaneX * Math.Sin(rotSpeed) + planeY * Math.Cos(rotSpeed);
            }
        }






    }

    /// <summary>
    /// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    public class RayCaster4 : RayCaster4base
    {
        // http://www.digital-ist-besser.de/
        // http://www.fredheintz.com/sitefred/main.html

        public RayCaster4()
        {

            //this.scaleX = 2;
            //this.scaleY = 2;
            //s.filters = new[] { new BlurFilter() };

        }

    }
}