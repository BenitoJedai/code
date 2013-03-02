using FlashHeatZeeker.StarlingSetup.Library;
using ScriptCoreLib.ActionScript.flash.geom;
using starling.display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlashHeatZeeker.UnitTank.Library
{
    public class VisualTank
    {

        public void Animate(double dy)
        {


            Action<Image, double> offsetTexCoords =
                (img, offset) =>
                {
                    img.setTexCoords(0, new Point(0, offset));
                    img.setTexCoords(1, new Point(1, offset));
                    img.setTexCoords(2, new Point(0, offset + 1));
                    img.setTexCoords(3, new Point(1, offset + 1));
                };

            if (dy > 0)
            {
                var offset = 1.0 - (Context.gametime.ElapsedMilliseconds % 1000) / 1000.0;
                offsetTexCoords(tanktrackpattern0, offset);
                offsetTexCoords(tanktrackpattern1, offset - 1);
            }
            else
            {
                var offset = (Context.gametime.ElapsedMilliseconds % 1000) / 1000.0;
                offsetTexCoords(tanktrackpattern0, offset);
                offsetTexCoords(tanktrackpattern1, offset - 1);
            }
        }

        public void SetPositionAndAngle(double x, double y, double angle)
        {

            {
                var cm = new Matrix();


                cm.translate(-64, -64);

                // shadow with tracks!
                cm.scale(1.2, 1.0);
                //cm.rotate(rot);
                //cm.translate(i * 128, yi * 128);



                cm.rotate(angle + Math.PI / 2);
                cm.translate(
                    x,
                    y
                );


                cm.translate(8, 8);

                currentshadow.transformationMatrix = cm;
            }

            {
                var cm = new Matrix();

                cm.rotate(angle + Math.PI / 2);
                cm.translate(
                    x,
                    y
                );

                currentvisual.transformationMatrix = cm;
            }
        }

        Image currentshadow,
            tanktrackpattern1, tanktrackpattern0;

        public  Sprite currentvisual;

        StarlingGameSpriteBase Context;

        public VisualTank(StarlingGameSpriteWithTankTextures textures, StarlingGameSpriteBase Context)
        {

            this.Context = Context;




            #region visual
            currentshadow = new Image(
                   textures.greentank_shadow()
                   )
           {
           }.AttachTo(
                    Context.Content
                );

            currentvisual = new Sprite().AttachTo(Context.Content);



            tanktrackpattern1 = new Image(
                textures.tanktrackpattern()
                )
           {
           }.AttachTo(
                 currentvisual
             );


            tanktrackpattern0 = new Image(
                textures.tanktrackpattern()
                )
           {
           }.AttachTo(
                 currentvisual
             );


            var imgstand = new Image(
               textures.greentank()
               )
            {
            }.AttachTo(
                currentvisual
            );

            var guntower = new Image(
                textures.greentank_guntower()
                )
            {
            }.AttachTo(
                 currentvisual
             );



            {
                var cm = new Matrix();

                cm.translate(-64, -64);
                cm.scale(0.55, 0.75);

                tanktrackpattern0.transformationMatrix = cm;
                tanktrackpattern1.transformationMatrix = cm;
            }

            // rpeate not suppported!
            // http://forum.starling-framework.org/topic/problem-with-repeat-and-textureatlas





            {
                var cm = new Matrix();

                cm.translate(-64, -64);

                imgstand.transformationMatrix = cm;
            }

            {
                var cm = new Matrix();

                cm.translate(-64, -64);

                guntower.transformationMatrix = cm;
            }

            //{
            //    var cm = new Matrix();

            //    cm.translate(-64, -64);

            //    // shadow with tracks!
            //    cm.scale(1.2, 1.0);
            //    //cm.rotate(rot);
            //    //cm.translate(i * 128, yi * 128);

            //    cm.translate(8, 8);

            //    currentshadow.transformationMatrix = cm;
            //}

            //{
            //    var cm = new Matrix();

            //    //cm.rotate(rot);
            //    //cm.translate(i * 128, yi * 128);


            //    currentvisual.transformationMatrix = cm;
            //}

            #endregion



        }
    }
}
