using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.StarlingSetup.Library;
using ScriptCoreLib.ActionScript.flash.geom;
using starling.display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlashHeatZeeker.UnitJeep.Library
{
    public class VisualJeep : IVisualUnit
    {
        public Image
            shadow,
            tire0,
            tire1,
            tire2,
            tire3;

        public Sprite
            currentvisual;

        public void SetPositionAndAngle(double x, double y, double angle)
        {
            #region transformationMatrix, phisics updated, now update visual


            {
                var cm = new Matrix();

                cm.translate(-32, -32);
                // how big shall the shadow be?
                //cm.scale(1.0, 1.0);

                // shadow does NOT move!
                cm.rotate(angle);
                //cm.translate(i * 128, yi * 128);

                cm.translate(
                     x,
                     y
                 );
                cm.translate(6, 6);

                this.shadow.transformationMatrix = cm;
            }

            {
                var cm = new Matrix();

          
                cm.rotate(angle);
                cm.translate(
                    x,
                    y
                );

                this.currentvisual.transformationMatrix = cm;
            }
            #endregion
        }

        public VisualJeep(StarlingGameSpriteWithJeepTextures textures, StarlingGameSpriteBase Context)
        {
            shadow = new Image(
                    textures.jeep_shadow()
                    )
           {
           }.AttachTo(
                    Context.Content_layer2_shadows
                //q
                );

            currentvisual = new Sprite().AttachTo(Context.Content);







            tire0 = new Image(
             textures.black4()
             )
           {
           }.AttachTo(
                //Content
               currentvisual
           );

            tire1 = new Image(textures.black4()).AttachTo(currentvisual);
             tire2 = new Image(textures.black4()).AttachTo(currentvisual);
             tire3 = new Image(textures.black4()).AttachTo(currentvisual);

            var imgstand = new Image(
              textures.jeep()
              )
            {
            }.AttachTo(
                //Content
                currentvisual
                  );






            {
                var cm = new Matrix();
                cm.translate(-2, -2);
                cm.scale(2, 4);
                cm.rotate(Math.PI * 0.1);

                cm.translate(-18, -20);

                tire0.transformationMatrix = cm;
            }

            {
                var cm = new Matrix();
                cm.translate(-2, -2);
                cm.scale(2, 4);
                cm.rotate(Math.PI * 0.1);

                cm.translate(18, -20);

                tire1.transformationMatrix = cm;
            }

            {
                var cm = new Matrix();
                cm.translate(-2, -2);
                cm.scale(2, 4);

                cm.translate(-18, 20);

                tire2.transformationMatrix = cm;
            }


            {
                var cm = new Matrix();
                cm.translate(-2, -2);
                cm.scale(2, 4);

                cm.translate(18, 20);

                tire3.transformationMatrix = cm;
            }


            {
                var cm = new Matrix();
                cm.translate(-32, -32);
                imgstand.transformationMatrix = cm;
            }

     
        }
    }
}
