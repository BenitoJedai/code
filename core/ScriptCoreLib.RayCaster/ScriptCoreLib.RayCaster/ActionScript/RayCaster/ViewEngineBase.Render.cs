using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.RayCaster.Extensions;
using ScriptCoreLib.ActionScript.Extensions;

namespace ScriptCoreLib.ActionScript.RayCaster
{
	[ScriptImportsType("flash.utils.getTimer")]
    partial class ViewEngineBase
    {
        protected double[] _ZBuffer;

        /// <summary>
        /// Renders a solid color ceiling and floor
        /// </summary>
        public void RenderHorizon()
        {
            buffer.fillRect(
                new Rectangle(0, 0, _ViewWidth, _ViewHeight / 2), 0xa0a0a0
                );

            buffer.fillRect(
                            new Rectangle(0, _ViewHeight / 2, _ViewWidth, _ViewHeight / 2), 0x808080
                            );
        }

        /// <summary>
        /// Enable RenderLowQualityWalls to render walls in lower quality
        /// </summary>
        public bool RenderLowQualityWalls;

        /// <summary>
        /// Enable SpritesVisible to render sprites
        /// </summary>
        public bool SpritesVisible = true;

        /// <summary>
        /// Enable FloorAndCeilingVisible to see texturized floor and ceiling
        /// </summary>
        public bool FloorAndCeilingVisible;


        /// <summary>
        /// renders a single sprite on display honoring zbuffer
        /// </summary>
        /// <param name="s"></param>
        /// <param name="Sprite_x"></param>
        public void RenderSingleSprite(SpriteInfoFromPOV s, int Sprite_x)
        {
            var depth = s.RelativePosition.length;

            // scale down enemies to eye line
            var z = (_ViewHeight / depth).Floor();

            if (z < 0.1)
                return;

            //var zmaxed = z.Max(_ViewHeight / 2).Floor();
            var zhalf = z / 2;

            // we are in a mirror? theres definetly a bug somewhere

            var clip = new Rectangle(Sprite_x - zhalf, 0, 0, ViewHeight);

            var min = clip.left.Floor().Max(0);
            var max = (clip.left + z).Floor().Min(_ViewWidth);

            if (min < max)
                for (int i = min; i < max; i++)
                {
                    if (_ZBuffer[i] > depth)
                    {
                        clip.left = i;

                        for (; i < max; i++)
                        {
                            if (_ZBuffer[i] > depth)
                            {
                                //buffer.setPixel32(i, _ViewHeight / 2 + 2, 0xffff00);
                            }
                            else
                            {
                                break;
                            }
                        }


                        clip.width = i - clip.left;

                        for (; i < max; i++)
                        {
                            //buffer.setPixel32(i, _ViewHeight / 2 + 1, 0xff8f0000);
                        }

                        break;
                    }
                    else
                    {
                        //buffer.setPixel32(i, _ViewHeight / 2, 0xffff0000);
                    }
                }

            if (clip.width > 0)
            {
                var texture = s.Sprite.Frames[GetFrameForPOV(s)];

                var matrix = new Matrix();
                var scale = (double)z / (double)texWidth;

                matrix.scale(scale, scale);
                matrix.translate(-zhalf + Sprite_x, -zhalf + _ViewHeight / 2);

                buffer.draw(texture.Bitmap, matrix, null, null, clip, true);
            }



            //for (int ix = 0; ix < z; ix++)
            //{
            //    var cx = Sprite_x + ix - zhalf;
            //    var cxt = ix * texWidth / z;

            //    if (_ZBuffer[cx] > depth)
            //    {
            //        if (texture == null)
            //            texture = s.Sprite.Frames[GetFrameForPOV(s)];

            //        for (int iy = 0; iy < zmaxed; iy += blocksize)
            //        {
            //            var cyt = iy * texture.Size / z;

            //            var color = texture[cxt, cyt];

            //            var color_a = (color >> 24) & 0xff;
            //            //var color_r = (color >> 16) & 0xff;
            //            //var color_g = (color >> 8) & 0xff;
            //            //var color_b = color & 0xff;

            //            if (color_a == 0xff)
            //                buffer.fillRect(
            //                    //new Rectangle(
            //                        cx, (_ViewHeight / 2) + iy - zhalf, 1, blocksize
            //                    //)
            //                        , color);


            //        }
            //    }
            //}
            //}
            //else
            //{
            //    for (int ix = 0; ix < z; ix++)
            //    {
            //        var cx = Sprite_x + ix - zhalf;
            //        var cxt = ix * texWidth / z;

            //        if (_ZBuffer[cx] > depth)
            //        {
            //            if (texture == null)
            //                texture = s.Sprite.Frames[GetFrameForPOV(s)];

            //            for (int iy = 0; iy < z; iy++)
            //            {
            //                var cyt = iy * texture.Size / z;

            //                var color = texture[cxt, cyt];

            //                var color_a = (color >> 24) & 0xff;
            //                //var color_r = (color >> 16) & 0xff;
            //                //var color_g = (color >> 8) & 0xff;
            //                //var color_b = color & 0xff;

            //                if (color_a == 0xff)
            //                    buffer.setPixel(cx, (_ViewHeight / 2) + iy - zhalf, color);


            //            }
            //        }
            //    }
            //}
        }

        private static int GetFrameForPOV(SpriteInfoFromPOV s)
        {
            var r = 360.DegreesToRadians();

            var len = s.Sprite.Frames.Length;

            #region direction translation magic
            var dir = s.Direction;

            dir -= (r / (len)) / 2;

            dir = r - (dir % r);
            dir += s.Sprite.Direction;

            dir += 270.DegreesToRadians();
            #endregion

            // we want to see it from behind...
            //dir += Math.PI / 2;

            var grad = ((dir * len) / r).Floor() % len;
            return grad;
        }

        protected SpriteInfoFromPOV[] _SpritesFromPOV;

		public SpriteInfoFromPOV[] SpritesFromPointOfView
		{
			get
			{
				return _SpritesFromPOV;
			}
		}

        protected void UpdatePOV()
        {
            if (_SpritesFromPOV == null || _SpritesFromPOV.Length != Sprites.Count)
                _SpritesFromPOV = Sprites.Select(i => new SpriteInfoFromPOV(i)).ToArray();


            //UpdatePOVCounter++;

            var fuzzy = 0.000001;

            foreach (var v in _SpritesFromPOV)
            {
                v.Update(this.posX + fuzzy, this.posY + fuzzy, this.rayDirLeft, this.rayDirRight);

                if (v.Distance < 0.1)
                    v.ViewInfo.IsInView = false;
            }

            //if (UpdatePOVCounter % 4 == 0)

            // whats up with the orderby? not working all the time..
            _SpritesFromPOV = _SpritesFromPOV.OrderBy(i => (i.Distance * -texWidth).Floor()).ToArray();

        }

        /// <summary>
        /// Renders all visible sprites
        /// </summary>
        protected void RenderSprites()
        {
            if (!SpritesVisible)
                return;

            foreach (var s in _SpritesFromPOV)
            {
                if (s.ViewInfo.IsInView)
                {
                    var Total = (s.ViewInfo.Right - s.ViewInfo.Left);

                    var LeftTarget = s.ViewInfo.Target - s.ViewInfo.Left;
                    //var RightTarget = s.ViewInfo.Right - s.ViewInfo.Target;

                    RenderSingleSprite(s, (LeftTarget * _ViewWidth / Total).Floor());

                }
            }
        }

		public double ViewDirectionLeftBorder
		{
			get
			{
				return rayDirLeft;
			}
		}

		public double ViewDirectionRightBorder
		{
			get
			{
				return rayDirRight;
			}
		}


        protected double rayDirLeft;
        protected double rayDirRight;


		// http://livedocs.adobe.com/flex/2/langref/flash/utils/package.html#getTimer()
		[Script(OptimizedCode = "return flash.utils.getTimer();")]
		internal static int getTimer()
		{
			return default(int);
		}





		int time;
		int counter;



		public void RenderScene()
		{
			if (_textures == null)
				return;

			if (_textures.Length == 0)
				return;

			if (_ZBuffer == null)
				_ZBuffer = new double[_ViewWidth];
			else if (_ZBuffer.Length != _ViewWidth)
				_ZBuffer = new double[_ViewWidth];

			RenderHorizon();

			buffer.@lock();


			int y;

			//// interleaving?
			//interleave_counter++;
			//x = interleave_counter % interleave_x_step;

			double rayDirXLeft = dirX + planeX;
			double rayDirYLeft = dirY + planeY;
			rayDirLeft = new Point { x = rayDirXLeft, y = rayDirYLeft }.GetRotation();

			double rayDirXRight = dirX - planeX;
			double rayDirYRight = dirY - planeY;
			rayDirRight = new Point { x = rayDirXRight, y = rayDirYRight }.GetRotation();

			// update for current frame
			UpdatePOV();

			var x = 0;
			var clip = new Rectangle(0, 0, 1, _ViewHeight);

			while (x < _ViewWidth)
			{
				var x_mirror = _ViewWidth - (x) - 1;
				var x_mirror_1 = -1;

				if (RenderLowQualityWalls)
					x_mirror_1 = _ViewWidth - (x + 1) - 1;

				clip.x = x_mirror;

				//calculate ray position and direction
				var cameraX = 2.0 * (double)x / (double)_ViewWidth - 1.0; //x-coordinate in camera space

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
					if (_WallMap[mapX, mapY] > 0)
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
				var lineHeight = Math.Abs((_ViewHeight / perpWallDist).Floor());

				//calculate lowest and highest pixel to fill in current stripe
				var drawStart = (-lineHeight / 2 + _ViewHeight / 2);
				if (drawStart < 0) drawStart = 0;
				var drawEnd = (lineHeight / 2 + _ViewHeight / 2);
				if (drawEnd >= _ViewHeight) drawEnd = _ViewHeight;

				var texNum = _WallMap[mapX, mapY] - 1; //1 subtracted from it so that texture 0 can be used!
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
				var texX = (wallX * texWidth).Floor();
				if (side == 0)
					if (rayDirX > 0) texX = texWidth - texX - 1;
				if (side == 1)
					if (rayDirY < 0) texX = texWidth - texX - 1;

				var hT = _ViewHeight * 128;
				var lhT = lineHeight * 128;

				// 22 fps - old
				// 34 fps - without walls

				y = drawStart;


				var texture = _textures[texNum % _textures.Length];



				//y += drawStart % 2;


				while (y < drawEnd)
				{
					var d = y * 256 - hT + lhT;  //256 and 128 factors to avoid floats
					var texY = ((d * texHeight) / lineHeight) / 256;

					var color = texture[texX, texY];

					if (side == 1) color = (color >> 1) & 0x7F7F7F;


					if (RenderLowQualityWalls)
					{
						buffer.fillRect(
							//new Rectangle(
										x_mirror_1, y, 2, 2
							//)
										, color);
						y += 2;
					}
					else
					{
						buffer.setPixel(x_mirror, y, color);
						y += 1;
					}
				}

				//SET THE ZBUFFER FOR THE SPRITE CASTING
				//perpendicular distance is used
				_ZBuffer[x_mirror] = perpWallDist;

				if (RenderLowQualityWalls)
					_ZBuffer[x_mirror_1] = perpWallDist;

				if (FloorAndCeilingVisible)
				{
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

					if (drawEnd < 0) drawEnd = _ViewHeight; //becomes < 0 when the integer overflows

					//draw the floor from drawEnd to the bottom of the screen
					#region draw floor
					y = drawEnd;
					double weight;
					double currentFloorX;
					double currentFloorY;
					int floorTexX;
					int floorTexY;

					var textures_floor = this.FloorTexture;
					var textures_ceiling = this.CeilingTexture;

					while (y < _ViewHeight)
					{

						currentDist = _ViewHeight / (2 * y - _ViewHeight); //you could make a small lookup table for this instead
						//currentDist = floorVals[int(y-80)];

						var pen_x = x_mirror;
						var pen_width = 1;
						var pen_height = 1;

						if (RenderLowQualityWalls)
						{
							pen_x = x_mirror_1;
							pen_width = 2;
						}

						//pen_width = currentDist.Floor().Max(2).Min(4);




						weight = (currentDist - distPlayer) / (distWall - distPlayer);

						currentFloorX = weight * floorXWall + (1.0 - weight) * posX;
						currentFloorY = weight * floorYWall + (1.0 - weight) * posY;

						floorTexX = (currentFloorX * texWidth).Floor() % texWidth;
						floorTexY = (currentFloorY * texHeight).Floor() % texHeight;

						try
						{
							var color = textures_floor[floorTexX, floorTexY];

							if (pen_width > 1)
								buffer.fillRect(
									//new Rectangle(
												pen_x, y, pen_width, pen_height
									//)
												, color);
							else
								buffer.setPixel(x_mirror, y, color); //floor
						}
						catch
						{
							//trace("err");
						}

						try
						{
							var color = textures_ceiling[floorTexX, floorTexY];

							if (pen_width > 1)
								buffer.fillRect(
									//new Rectangle(
												pen_x, _ViewHeight - y - pen_width + 1, pen_width, pen_height
									//)
												, color);
							else
								buffer.setPixel(pen_x, _ViewHeight - y - 1, color); //ceiling (symmetrical!)
						}
						catch
						{
							//trace("err");
						}

						y += pen_height;
					}
					#endregion

				}


				//x += 1;

				if (RenderLowQualityWalls)
					x += 2;
				else
					x += 1;

			}

			var RenderSpritesTimeA = getTimer();
			RenderSprites();
			//RenderSpritesTimeB = (RenderSpritesTimeB + getTimer() - RenderSpritesTimeA) / 2;

			counter++;

			if (getTimer() - 500 >= time)
			{
				FramesPerSecond = counter * 2;

				if (FramesPerSecondChanged != null)
					FramesPerSecondChanged();

				// txtMain.text = (counter * 2).ToString() + "fps " + global::ScriptCoreLib.ActionScript.flash.system.System.totalMemory + "bytes";
				//txtMain.text = (counter * 2).ToString() + "fps @" + dir.RadiansToDegrees();
				//txtMain.text = (counter * 2).ToString() + "fps @" + _WallMap[posX.Floor(), posY.Floor()]
				//        + " sprites: " + this.SpritesFromPOV.Length
				//        + " spriterender: " + RenderSpritesTimeB
				//        + " spriterender/sprite: " + (RenderSpritesTimeB / this.SpritesFromPOV.Length)
				//        ;
				counter = 0;
				time = getTimer();
			}

			if (RenderOverlay != null)
				RenderOverlay();

			//screenImage.bitmapData = screen;
			buffer.unlock();


		}

		public int FramesPerSecond;
		public event Action FramesPerSecondChanged;

		public event Action RenderOverlay;



    }
}
