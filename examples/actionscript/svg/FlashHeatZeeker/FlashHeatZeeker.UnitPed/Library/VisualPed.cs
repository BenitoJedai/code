using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.StarlingSetup.Library;
using ScriptCoreLib.ActionScript.flash.geom;
using starling.display;
using starling.textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlashHeatZeeker.UnitPed.Library
{
    public class VisualPed
    {
        public Image shadow;
        public Image currentvisual;


        public void SetPositionAndAngle(double x, double y, double angle)
        {
            #region transformationMatrix, phisics updated, now update visual


            {
                var cm = new Matrix();

                cm.translate(-32, -32);
                // how big shall the shadow be?
                cm.scale(2.0, 2.0);

                // shadow does NOT move!
                //cm.rotate(current.GetAngle());
                //cm.translate(i * 128, yi * 128);

                cm.translate(
                     x, y
                 );

                this.shadow.transformationMatrix = cm;
            }

            {
                var cm = new Matrix();

                cm.translate(-32, -32);
                //cm.scale(2.0, 2.0);

                // physics 0 looks right
                cm.rotate(angle + Math.PI / 2);
                cm.translate(
                    x, y
                );

                this.currentvisual.transformationMatrix = cm;
            }
            #endregion
        }

        public StarlingGameSpriteBase Context;

        int AnimateSeed;

        Texture[] walk_ani;
        Texture[] texframes;

        // 50 FPS
        // public Action<bool> Animate;
        // jsc could do some magic with single use delegates to make the faster for flash
        // 60 FPS
        public void Animate(double dx, double dy)
        {
            if (dy == 0 && dx == 0)
            {
                currentvisual.texture = texframes[0];

                return;
            }


            var ii = ((AnimateSeed + Context.gametime.ElapsedMilliseconds) / 150) % walk_ani.Length;

            if (dy < 0)
                ii = walk_ani.Length - 1 - ii;

            currentvisual.texture = walk_ani[ii];


        }

        public VisualPed(StarlingGameSpriteWithPedTextures textures, StarlingGameSpriteBase Context)
        {
            this.Context = Context;

            walk_ani = new[] {
                    textures.ped_walk3_leftclose(), 
                    textures.ped_walk3x_rightclose(),
                    textures.ped_walk1x_rightfar(),
                    textures.ped_walk2x_rightmid(),
                    textures.ped_walk3x_rightclose(),
                    textures.ped_walk3_leftclose(), 
                    textures.ped_walk1_leftfar(), 
                    textures.ped_walk2_leftmid(), 

                };

            texframes = new[] {

                    textures.ped_stand(),
                };

            // 781
            // 15 FPS
            // 60 FPS
            // 31  FPS

            // 41
            //var q = new Sprite().AttachTo(Content);

            // Error: Error #3691: Resource limit for this resource type exceeded.
            shadow = new Image(
               textures.ped_shadow()
               )
            {
                // fkn expensive!!
                //alpha = 0.5
            }.AttachTo(Context.Content);

            //peds.Add(imgstand);


            currentvisual = new Image(
               texframes[0]) { }.AttachTo(
               Context.Content
           );



            AnimateSeed = Context.random.Next() % 3000;


            #region Animate

            // FPS 50

            #endregion


            //#region SetPositionAndAngle
            //SetPositionAndAngle =
            //    (x, y, angle) =>
            //    {

            //    };
            //#endregion


        }
    }

}
