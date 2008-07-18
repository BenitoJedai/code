using ScriptCoreLib;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;




namespace RayCaster4.ActionScript
{



    partial class RayCaster4base
    {

        static bool render_DebugTrace_Assign_Active = true;

#if DebugTrace_Assign
        private static void render_DebugTrace_Assign(string e)
        {
            if (render_DebugTrace_Assign_Active)
                Console.WriteLine(e);
        }
#endif

        [Script(NoDecoration = true)]
        public void render(object e)
        {

            //screen.floodFill(0, 0, 0x0);
            var screenData = screen.@lock();

            int x;
            int y;
            DoMovement();

            foreach (var s in Sprites)
            {
                s.ScanComplete = false;
                s.ScanValid = false;

            }

            double rayDirXLeft = dirX - planeX;
            double rayDirYLeft = dirY - planeY;
            var rayDirLeft = new PointDouble { X = rayDirXLeft, Y = rayDirYLeft }.GetRotation();

            double rayDirXRight = dirX + planeX;
            double rayDirYRight = dirY + planeY;
            var rayDirRight = new PointDouble { X = rayDirXRight, Y = rayDirYRight }.GetRotation();

            var DelayDrawSprites = new List<SpriteDrawRequest>();


            x = 0;
            while (x < w)
            {

                //calculate ray position and direction
                double cameraX = 2.0 * (double)x / (double)w - 1.0; //x-coordinate in camera space
                double rayPosX = posX;
                double rayPosY = posY;
                double rayDirX = dirX + planeX * cameraX;
                double rayDirY = dirY + planeY * cameraX;

                var rayDir = new PointDouble { X = rayDirX, Y = rayDirY }.GetRotation();

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

                //if (texNum != 3)
                texNum = 3;

                //texNum = 0;

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
                var texX = Math.Abs((wallX * texWidth).Floor());
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
                    var texY = Math.Abs(((d * texHeight) / lineHeight) / 256);


                    var color = textures[texNum][texX][texY];

                    //var xxx = perpWallDist;
                    #region apply fog
                    var color_r = (color >> 16) & 0xff;
                    var color_g = (color >> 8) & 0xff;
                    var color_b = color & 0xff;

                    var fog = 1 - (perpWallDist / 10).Min();

                    if (side == 0)
                        fog = (fog * 2).Min();

                    color_r = (uint)(color_r * fog);
                    color_g = (uint)(color_g * fog);
                    color_b = (uint)(color_b * fog);


                    color = (color_r << 16) + (color_g << 8) + color_b;
                    #endregion
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

                    floorTexX = Math.Abs((currentFloorX * texWidth).Floor() % texWidth);
                    floorTexY = Math.Abs((currentFloorY * texHeight).Floor() % texHeight);



                    #region floor
                    try
                    {
                        var color = textures[1][floorTexX][floorTexY];

                        #region apply fog
                        var color_r = (color >> 16) & 0xff;
                        var color_g = (color >> 8) & 0xff;
                        var color_b = color & 0xff;

                        var fog = 1 - (currentDist / 10).Min();

                        fog = (fog * 2).Min();

                        color_r = (uint)(color_r * fog);
                        color_g = (uint)(color_g * fog);
                        color_b = (uint)(color_b * fog);


                        color = (color_r << 16) + (color_g << 8) + color_b;
                        #endregion

                        screen.setPixel(x, y, color); //floor
                    }
                    catch
                    {
                        //trace("err");
                    }
                    #endregion


                    #region draw ceiling
                    try
                    {
                        var color = textures[2][floorTexX][floorTexY];

                        #region apply fog
                        var color_r = (color >> 16) & 0xff;
                        var color_g = (color >> 8) & 0xff;
                        var color_b = color & 0xff;

                        var fog = 1 - (currentDist / 10).Min();

                        fog = (fog * 2).Min();

                        color_r = (uint)(color_r * fog);
                        color_g = (uint)(color_g * fog);
                        color_b = (uint)(color_b * fog);


                        color = (color_r << 16) + (color_g << 8) + color_b;
                        #endregion

                        screen.setPixel(x, h - y - 1, color); //ceiling (symmetrical!)
                    }
                    catch
                    {
                        //trace("err");
                    }
                    #endregion

                    // draw sprites here

                    y++;
                }
                #endregion

               
                foreach (var s in Sprites)
                {
                    var DeltaToSprite =
                        new PointDouble
                        {
                            X = s.X - posX,
                            Y = s.Y - posY,
                        };


                    var DirectionToSprite = Math.Abs(DeltaToSprite.GetRotation() - rayDir);

                    if (DirectionToSprite > (rayDirLeft % (Math.PI * 4)))
                    {
                        continue;
                    }
                

                    if (s.ScanComplete)
                    {
                        // painted
                    }
                    else
                    {
                        //var ScanDirDelta = DirectionToSprite - rayDir;

                        if (x == 0)
                            s.ScanDir = DirectionToSprite;
                        else
                        {
                            if (s.ScanDir < DirectionToSprite)
                            {
                                DelayDrawSprites.Add(
                                    new SpriteDrawRequest
                                    {
                                        Sprite = s,
                                        z = (1 / DeltaToSprite.GetDistance()) * 4,
                                        x = x,
                                    }
                                );

                          

                                //for (int _y = 0; _y < (120 * z).ToInt32(); _y++)
                                //{

                                //    screen.setPixel(x + 8, 120 + _y, 0xffffff); //ceili
                                //    screen.setPixel(x, 120 + _y, 0xffffff); //ceili
                                //    screen.setPixel(x, 120 - _y, 0xffffff); //ceili
                                //    screen.setPixel(x + 8, 120 - _y, 0xffffff); //ceili
                                //}
                                s.ScanComplete = true;
                            }
                            else
                            {
                                s.ScanDir = DirectionToSprite;

                            }
                        }
                    }

                 
                }

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
            screen.UnlockBits(screenData);



            using (var g = Graphics.FromImage(screen))
            using (var green = new SolidBrush(Color.FromArgb(0x7f, Color.Green)))
            using (var yellow = new SolidBrush(Color.FromArgb(0x7f, Color.Yellow)))
            using (var blue = new SolidBrush(Color.FromArgb(0x7f, Color.Blue)))
            using (var purple = new SolidBrush(Color.FromArgb(0x7f, Color.Purple)))
            using (var cyan = new SolidBrush(Color.FromArgb(0x7f, Color.Cyan)))
            {
                foreach (var r in DelayDrawSprites.OrderBy(i => i.z))
                {
                     g.DrawImage(r.Sprite.Image,
                                    r.x - r.Sprite.Image.Width / 2 * r.z,
                                    120 - r.Sprite.Image.Height / 2 * r.z,
                                    (r.Sprite.Image.Width * r.z),
                                    (r.Sprite.Image.Width * r.z));
                }

                var colors = new[] { green, yellow, blue, purple, cyan };

                g.DrawImage(PistolImage, (w - PistolImage.Width) / 2, h - PistolImage.Height - HudImage.Height);
                g.DrawImage(HudImage, 0, h - HudImage.Height);
                g.ScaleTransform(4, 4);
                // i want a minimap!

                for (int iy = 0; iy < worldMap.Length; iy++)
                    for (int ix = 0; ix < worldMap[iy].Length; ix++)
                    {
                        var cell = worldMap[iy][ix];

                        if (cell == 0)
                        {

                        }
                        else
                        {
                            g.FillRectangle(colors[cell % colors.Length], iy, ix, 1, 1);

                        }
                    }

                g.DrawLine(Pens.Red, posX, posY, posX + dirX * 1, posY + dirY * 1);


                foreach (var s in Sprites)
                {

                    g.DrawLine(Pens.Yellow, s.X, s.Y, s.X + 1, s.Y);
                }


            }

        }



        Image PistolImage = Image.FromFile("pistol.png");
        Image HudImage = Image.FromFile("hud.png");

        static Image Sprite1 = Image.FromFile("116.png");
        static Image Sprite2 = Image.FromFile("lamp.png");
        
        public class SpriteDrawRequest
        {
            public Sprite Sprite;

            public int x;

            public double z;
        }

        public class Sprite : PointDouble
        {
            public bool ScanComplete;
            public bool ScanValid;
            public double ScanDir;

            public override string ToString()
            {
                return new { ScanDir }.ToString();
            }

            public Image Image;
        }

        public Sprite[] Sprites =
            new[] {
                new Sprite { X = 20.5, Y = 12.5, Image = Sprite1 },
                new Sprite { X = 18.9, Y = 11.5, Image = Sprite2 },
                new Sprite { X = 18.5, Y = 12.5, Image = Sprite1 },
            };

    }

}