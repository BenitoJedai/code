using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.filters;
using System;
using System.Linq;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.ActionScript.flash.ui;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.utils;


namespace RayCaster6.ActionScript
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

        int interleave_x_step = 1;
        int interleave_counter = 0;

        double rayDirLeft;
        double rayDirRight;

        [Script(NoDecoration = true)]
        private new void render(Event e)
        {
            /* 			try {
                            screen.dispose();
                            screen = new BitmapData( w, h, false, 0x0 );
                            screen.lock();
                        } catch(e:Error) {
                            trace("err");
                        } */

            screen.floodFill(0, 0, 0x0);
            screen.@lock();


            int x;
            int y;
            DoMovement();

            // interleaving?
            interleave_counter++;
            x = interleave_counter % interleave_x_step;

            double rayDirXLeft = dirX + planeX;
            double rayDirYLeft = dirY + planeY;
            rayDirLeft = new Point { x = rayDirXLeft, y = rayDirYLeft }.GetRotation();

            double rayDirXRight = dirX - planeX;
            double rayDirYRight = dirY - planeY;
            rayDirRight = new Point { x = rayDirXRight, y = rayDirYRight }.GetRotation();

            // update for current frame
            UpdatePOV();


            while (x < w)
            {

                //calculate ray position and direction
                var cameraX = 2.0 * (double)x / (double)w - 1.0; //x-coordinate in camera space

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
                    if (worldMap[mapX, mapY] > 0)
                    {
                        hit = 1; //Check if ray has hit a wall   
                    }
                }

                //Calculate distance projected on camera direction (oblique distance will give fisheye effect!)
                double perpWallDist;
                if (side == 0)
                {
                    perpWallDist = Math.Abs((mapX - rayPosX + (1 - stepX) / 2) / rayDirX);
                }
                else
                {
                    perpWallDist = Math.Abs((mapY - rayPosY + (1 - stepY) / 2) / rayDirY);
                }

                //Calculate height of line to draw on screen
                var lineHeight = Math.Abs((h / perpWallDist).Floor());

                //calculate lowest and highest pixel to fill in current stripe
                var drawStart = (-lineHeight / 2 + h / 2).Floor();
                if (drawStart < 0) drawStart = 0;
                var drawEnd = (lineHeight / 2 + h / 2).Floor();
                if (drawEnd >= h) drawEnd = h;

                var texNum = worldMap[mapX, mapY] - 1; //1 subtracted from it so that texture 0 can be used!
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

                var texture = textures[texNum];

                while (y < drawEnd)
                {
                    var d = y * 256 - hT + lhT;  //256 and 128 factors to avoid floats
                    var texY = ((d * texHeight) / lineHeight) / 256;

                    var color = texture[texX, texY];

                    if (side == 1) color = (color >> 1) & 0x7F7F7F;


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
                #region draw floor
                y = drawEnd;
                double weight;
                double currentFloorX;
                double currentFloorY;
                int floorTexX;
                int floorTexY;

                var textures_floor = textures[1];
                var textures_ceiling = textures[2];

                while (y < h)
                {

                    currentDist = h / (2 * y - h); //you could make a small lookup table for this instead
                    //currentDist = floorVals[int(y-80)];

                    weight = (currentDist - distPlayer) / (distWall - distPlayer);

                    currentFloorX = weight * floorXWall + (1.0 - weight) * posX;
                    currentFloorY = weight * floorYWall + (1.0 - weight) * posY;

                    floorTexX = (currentFloorX * texWidth).Floor() % texWidth;
                    floorTexY = (currentFloorY * texHeight).Floor() % texHeight;

                    try
                    {
                        var color = textures_floor[floorTexX, floorTexY];

                        screen.setPixel(x, y, color); //floor
                    }
                    catch
                    {
                        //trace("err");
                    }

                    try
                    {
                        var color = textures_ceiling[floorTexX, floorTexY];


                        screen.setPixel(x, h - y - 1, color); //ceiling (symmetrical!)
                    }
                    catch
                    {
                        //trace("err");
                    }


                    y++;
                }
                #endregion

                x += interleave_x_step;

                if (x > 4)
                    render_DebugTrace_Assign_Active = false;
            }

            RenderSprites();

            counter++;

            if (getTimer() - 500 >= time)
            {
                txtMain.text = (counter * 2).ToString() + "fps " + global::ScriptCoreLib.ActionScript.flash.system.System.totalMemory + "bytes";
                counter = 0;
                time = getTimer();
            }

            const int isize = 2;

            DrawMinimap(isize);

            //screenImage.bitmapData = screen;
            screen.unlock();


        }

        private void RenderSprites()
        {

            foreach (var s in SpritesFromPOV)
            {
                if (s.ViewInfo.IsInView)
                {
                    var Total = (s.ViewInfo.Right - s.ViewInfo.Left);

                    //var LeftTarget = s.ViewInfo.Target - s.ViewInfo.Left;
                    var RightTarget = s.ViewInfo.Right - s.ViewInfo.Target;

                    RenderSingleSprite(s, (RightTarget * w / Total).Floor());

                }
            }
        }

        private void RenderSingleSprite(SpriteInfoFromPOV s, int Sprite_x)
        {
            var depth = s.RelativePosition.length;



            // 14

            // scale down enemies to eye line
            var z = (h / depth).Floor();
            var zhalf = z / 2;


            // screen.setPixel(Sprite_x, 120, 0xffffff);

            var texture = s.Sprite.CurrentFrame;

            if (z > texture.Size)
            {
                var blocksize = (z / texture.Size).Floor().Max(1);

                for (int ix = 0; ix < z; ix++)
                {
                    var cx = Sprite_x + ix - zhalf;
                    var cxt = ix * texture.Size / z;

                    if (ZBuffer[cx] > depth)
                        for (int iy = 0; iy < z; iy += blocksize)
                        {
                            var cyt = iy * texture.Size / z;

                            var color = texture[cxt, cyt];

                            var color_a = (color >> 24) & 0xff;
                            var color_r = (color >> 16) & 0xff;
                            var color_g = (color >> 8) & 0xff;
                            var color_b = color & 0xff;

                            if (color_a == 0xff)
                                screen.fillRect(
                                    new Rectangle(cx, (h / 2) + iy - zhalf, 1, blocksize), color);


                        }
                }
            }
            else
            {
                for (int ix = 0; ix < z; ix++)
                {
                    var cx = Sprite_x + ix - zhalf;
                    var cxt = ix * texture.Size / z;

                    if (ZBuffer[cx] > depth)
                        for (int iy = 0; iy < z; iy++)
                        {
                            var cyt = iy * texture.Size / z;

                            var color = texture[cxt, cyt];

                            var color_a = (color >> 24) & 0xff;
                            var color_r = (color >> 16) & 0xff;
                            var color_g = (color >> 8) & 0xff;
                            var color_b = color & 0xff;

                            if (color_a == 0xff)
                                screen.setPixel(cx, (h / 2) + iy - zhalf , color);


                        }
                }
            }
        }

        private void UpdatePOV()
        {
            if (SpritesFromPOV == null || SpritesFromPOV.Length != Sprites.Count)
                SpritesFromPOV = Sprites.Select(i => new SpriteInfoFromPOV(i)).ToArray();


            //UpdatePOVCounter++;

            foreach (var v in SpritesFromPOV)
            {
                v.Update(this.posX, this.posY, this.rayDirLeft, this.rayDirRight);
            }

            //if (UpdatePOVCounter % 4 == 0)

            // whats up with the orderby? not working all the time..
            SpritesFromPOV = SpritesFromPOV.OrderBy(i => (i.Distance * -texWidth).Floor()).ToArray();

            //System.Array.Reverse(SpritesFromPOV);
        }

        private void DrawMinimap(int isize)
        {
            var minimap = new BitmapData(isize * (worldMap.XLength + 2), isize * (worldMap.YLength + 2), true, 0x0);
            var minimap_bmp = new Bitmap(minimap);


            for (int ix = 0; ix < worldMap.XLength; ix++)
                for (int iy = 0; iy < worldMap.YLength; iy++)
                {
                    if (worldMap[ix, iy] > 0)
                        minimap.fillRect(new Rectangle((ix + 1) * isize, (iy + 1) * isize, isize, isize), 0x7f00ff00);

                }

            minimap.applyFilter(minimap, minimap.rect, new Point(), new GlowFilter(0x00ff00));

            minimap.fillRect(new Rectangle((posX + 0.5) * isize, (posY + 0.5) * isize, isize, isize), 0xffff0000);

            minimap.drawLine(0xffffffff,
                (posX + 1) * isize,
                (posY + 1) * isize,
                (posX + 1 + Math.Cos(rayDirLeft) * 8) * isize,
                (posY + 1 + Math.Sin(rayDirLeft) * 8) * isize
                );

            minimap.drawLine(0xffffffff,
              (posX + 1) * isize,
              (posY + 1) * isize,
              (posX + 1 + Math.Cos(rayDirRight) * 8) * isize,
              (posY + 1 + Math.Sin(rayDirRight) * 8) * isize
              );

            //Console.WriteLine("left: " + rayDirLeft);
            //Console.WriteLine("right: " + rayDirLeft);

            foreach (var ss in SpritesFromPOV)
            {
                uint color = 0xff00ffff;



                if (!ss.ViewInfo.IsInView)
                    color = 0xff000000;


                minimap.fillRect(new Rectangle(
                    (ss.Sprite.Position.x + 1) * isize,
                    (ss.Sprite.Position.y + 1) * isize,
                    isize,
                    isize), color);
            }



            screen.draw(minimap);
        }

        SpriteInfoFromPOV[] SpritesFromPOV;



        [Script]
        public class SpriteInfoFromPOV
        {
            public Point RelativePosition;

            public SpriteInfo Sprite;

            public double Direction;

            public readonly ViewInfo ViewInfo = new ViewInfo();

            public double Distance;

            public SpriteInfoFromPOV(SpriteInfo s)
            {
                Sprite = s;




            }

            public void Update(double x, double y, double left, double right)
            {
                RelativePosition = new Point
                {
                    x = Sprite.Position.x - x,
                    y = Sprite.Position.y - y
                };

                Direction = RelativePosition.GetRotation();
                Distance = RelativePosition.length;

                ViewInfo.Left = left;
                ViewInfo.Right = right;
                ViewInfo.Target = Direction;

                ViewInfo.Update();

            }
        }

        [Script]
        public class SpriteInfo
        {
            public Point Position = new Point();

            public Texture64 CurrentFrame;

            public double Zoom = 0.8;
        }

        public readonly List<SpriteInfo> Sprites = new List<SpriteInfo>();
    }

}
