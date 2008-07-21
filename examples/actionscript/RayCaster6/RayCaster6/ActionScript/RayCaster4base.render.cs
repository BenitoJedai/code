using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.filters;
using System;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.ActionScript.flash.ui;
using ScriptCoreLib.ActionScript.flash.geom;


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

            x = 0;
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

                while (y < drawEnd)
                {

                    var d = y * 256 - hT + lhT;  //256 and 128 factors to avoid floats
                    var texY = ((d * texHeight) / lineHeight) / 256;

                    var color = textures[texNum][(texX).Floor()][(texY).Floor()];

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
                #region draw flood
                y = drawEnd;
                double weight;
                double currentFloorX;
                double currentFloorY;
                int floorTexX;
                int floorTexY;

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
                        var color = textures[1][floorTexX][floorTexY];

                        screen.setPixel(x, y, color); //floor
                    }
                    catch
                    {
                        //trace("err");
                    }

                    try
                    {
                        var color = textures[2][floorTexX][floorTexY];

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

            if (getTimer() - 500 >= time)
            {
                txtMain.text = (counter * 2).ToString();
                counter = 0;
                time = getTimer();
            }

            const int isize = 3;

            var minimap = new BitmapData(isize * (worldMap.XLength + 2), isize * (worldMap.YLength + 2), true, 0x0);
            var minimap_bmp = new Bitmap(minimap);
            

            for (int ix = 0; ix < worldMap.XLength; ix++)
                for (int iy = 0; iy < worldMap.YLength; iy++)
                {
                    if (worldMap[ix, iy] > 0)
                        minimap.fillRect(new Rectangle((ix + 1) * isize, (iy + 1) * isize, isize, isize), 0x7f00ff00);

                }

            minimap.applyFilter(minimap, minimap.rect, new Point(), new GlowFilter(0x00ff00));
            minimap.fillRect(new Rectangle((posX + 1) * isize, (posY + 1) * isize , isize, isize), 0xffff0000);


            screen.draw(minimap);

            //screenImage.bitmapData = screen;
            screen.unlock();


        }





    }

}
