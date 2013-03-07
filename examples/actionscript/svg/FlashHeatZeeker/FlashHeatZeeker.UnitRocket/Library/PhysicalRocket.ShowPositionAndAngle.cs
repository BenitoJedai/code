using Box2D.Dynamics;
using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.UnitJeepControl.Library;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.Shared.BCLImplementation.GLSL;
using starling.display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlashHeatZeeker.UnitRocket.Library
{
    public partial class PhysicalRocket : IPhysicalUnit
    {

        bool prev = false;
        double prevx = 0.0;
        double prevy = 0.0;


        public void ShowPositionAndAngle()
        {
            //if (body != null)
            //    body.SetActive(true);

            // where are we now


            {
                var x = this.body.GetPosition().x * 16;
                var y = this.body.GetPosition().y * 16;
                var a = this.body.GetAngle();

                var cm = new Matrix();

                cm.translate(-64, -64);
                // how big shall the shadow be?
                //cm.scale(2.0, 2.0);

                cm.rotate(a + Math.PI / 2);


                cm.translate(
                     x, y
                 );

                this.visual.transformationMatrix = cm;
            }

            //if (this.seatedvehicle != null)
            //{
            //    this.visual.shadow.visible = false;
            //    this.visual.currentvisual.visible = false;
            //    return;
            //}

            //var iswalking = velocity.LinearVelocityX != 0 || velocity.LinearVelocityY != 0;
            //var iswalking = this.body.GetLinearVelocity().Length() > 0;
            //this.visual.Animate(velocity.LinearVelocityX, velocity.LinearVelocityY);



            #region Content_layer0_tracks
            if (!prev)
            {
                prev = true;
                prevx = this.body.GetPosition().x;
                prevy = this.body.GetPosition().y;
            }
            else
            {
                var p = this.body.GetPosition();


                var distance = X.GetLength(
                    new __vec2(
                    (float)(p.x - prevx),
                    (float)(p.y - prevy)
                    )
                );

                if (distance > 1)
                {
                    // smoke!

                    //var tracks0 = new Image(textures.ped_footprints()).AttachTo(Context.Content_layer0_tracks);

                    //var cm = new Matrix();

                    //cm.translate(-32, -32);
                    //cm.rotate(this.body.GetAngle() - Math.PI / 2);
                    //cm.translate(
                    //    p.x * 16.0,
                    //    p.y * 16.0
                    //);

                    //tracks0.transformationMatrix = cm;

                    //prevx = p.x;
                    //prevy = p.y;
                }
            }
            #endregion
        }

    }
}
