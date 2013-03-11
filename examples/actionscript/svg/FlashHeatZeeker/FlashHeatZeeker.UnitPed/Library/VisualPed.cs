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
    public class DriversSeatUser
    {
        public IPhysicalUnit vehicle;
    }

    public class VisualPed
    {
        public DriversSeatUser driverseatuser = new DriversSeatUser();

        public Image shadow;
        public Image currentvisual;


        public void SetPositionAndAngle(double x, double y, double angle)
        {
            #region transformationMatrix, phisics updated, now update visual


            {
                var cm = new Matrix();

                cm.translate(-48, -48);
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

                cm.translate(-48, -48);
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

        public void Orphanize()
        {
            this.currentvisual.Orphanize();
            this.shadow.Orphanize();
        }

        Texture[] walk_ani;
        Texture[] walkzombie_ani;
        Texture[] texframes;




        public bool LayOnTheGround;

        // 50 FPS
        // public Action<bool> Animate;
        // jsc could do some magic with single use delegates to make the faster for flash
        // 60 FPS
        public void Animate(double dx, double dy)
        {
            // are we in a vehicle?
            this.shadow.visible = true;
            this.currentvisual.visible = true;

            if (dy == 0 && dx == 0)
            {
                if (LayOnTheGround)
                {
                    this.shadow.visible = false;
                    if (WalkLikeZombie)
                        currentvisual.texture = texframes[3];
                    else
                        currentvisual.texture = texframes[1];
                }
                else
                {
                    if (WalkLikeZombie)
                        currentvisual.texture = texframes[2];
                    else
                        currentvisual.texture = texframes[0];
                }

                return;
            }


            var ii = ((AnimateSeed + Context.gametime.ElapsedMilliseconds) / 120) % walk_ani.Length;

            if (dy < 0)
                ii = walk_ani.Length - 1 - ii;

            if (WalkLikeZombie)
                currentvisual.texture = walkzombie_ani[ii];
            else
                currentvisual.texture = walk_ani[ii];


        }

        public bool WalkLikeZombie;

        /// <summary>
        /// If everybody walks the same time, look different
        /// 
        /// </summary>
        public int AnimateSeed;
        public VisualPed(StarlingGameSpriteWithPedTextures textures, StarlingGameSpriteBase Context, int AnimateSeed = 0)
        {
            this.AnimateSeed = AnimateSeed;
            this.Context = Context;

            walk_ani = new[] {
                    textures.ped_walk.ped_walk3_leftclose(), 
                    textures.ped_walk.ped_walk3x_rightclose(),
                    textures.ped_walk.ped_walk1x_rightfar(),
                    textures.ped_walk.ped_walk2x_rightmid(),
                    textures.ped_walk.ped_walk3x_rightclose(),
                    textures.ped_walk.ped_walk3_leftclose(), 
                    textures.ped_walk.ped_walk1_leftfar(), 
                    textures.ped_walk.ped_walk2_leftmid(), 

                };

            walkzombie_ani = new[] {
                    textures.ped_walkzombie.ped_walk3_leftclose(), 
                    textures.ped_walkzombie.ped_walk3x_rightclose(),
                    textures.ped_walkzombie.ped_walk1x_rightfar(),
                    textures.ped_walkzombie.ped_walk2x_rightmid(),
                    textures.ped_walkzombie.ped_walk3x_rightclose(),
                    textures.ped_walkzombie.ped_walk3_leftclose(), 
                    textures.ped_walkzombie.ped_walk1_leftfar(), 
                    textures.ped_walkzombie.ped_walk2_leftmid(), 
                };

            texframes = new[] {
                textures.ped_walk.ped_stand(),
                textures.ped_walk.ped_down(),
                textures.ped_walkzombie.ped_stand(),
                textures.ped_walkzombie.ped_down(),
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
            }.AttachTo(Context.Content_layer2_shadows);



            currentvisual = new Image(
               texframes[0]) { }.AttachTo(
               Context.Content
           );






        }
    }

}
