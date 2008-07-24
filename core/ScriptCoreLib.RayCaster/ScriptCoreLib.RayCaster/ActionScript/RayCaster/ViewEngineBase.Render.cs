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

        protected SpriteInfoFromPOV[] SpritesFromPOV;


        protected void UpdatePOV()
        {
            if (SpritesFromPOV == null || SpritesFromPOV.Length != Sprites.Count)
                SpritesFromPOV = Sprites.Select(i => new SpriteInfoFromPOV(i)).ToArray();


            //UpdatePOVCounter++;

            var fuzzy = 0.000001;

            foreach (var v in SpritesFromPOV)
            {
                v.Update(this.posX + fuzzy, this.posY + fuzzy, this.rayDirLeft, this.rayDirRight);

                if (v.Distance < 0.1)
                    v.ViewInfo.IsInView = false;
            }

            //if (UpdatePOVCounter % 4 == 0)

            // whats up with the orderby? not working all the time..
            SpritesFromPOV = SpritesFromPOV.OrderBy(i => (i.Distance * -texWidth).Floor()).ToArray();

        }

        /// <summary>
        /// Renders all visible sprites
        /// </summary>
        protected void RenderSprites()
        {
            if (!SpritesVisible)
                return;

            foreach (var s in SpritesFromPOV)
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


        protected double rayDirLeft;
        protected double rayDirRight;
    }
}
