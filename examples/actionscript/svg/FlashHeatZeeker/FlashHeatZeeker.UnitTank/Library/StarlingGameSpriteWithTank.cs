using FlashHeatZeeker.StarlingSetup.Library;
using ScriptCoreLib.ActionScript.flash.geom;
using starling.display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlashHeatZeeker.UnitTank.Library
{
    public sealed class StarlingGameSpriteWithTank : StarlingGameSpriteBase
    {
        public StarlingGameSpriteWithTank()
        {
            var textures_tanktrackpattern = new_tex_crop("assets/FlashHeatZeeker.UnitTank/tanktrackpattern.svg", innersize: 128);
            var textures_greentank_guntower = new_tex_crop("assets/FlashHeatZeeker.UnitTank/greentank_guntower.svg", innersize: 128);
            var textures_greentank_shadow = new_tex_crop("assets/FlashHeatZeeker.UnitTank/greentank_shadow.svg", innersize: 128, alpha: 0.3);
            var textures_greentank = new_tex_crop("assets/FlashHeatZeeker.UnitTank/greentank.svg", innersize: 128);


            this.autorotate = true;
            this.internalscale = 2.0;

            this.onbeforefirstframe += delegate
             {

                 for (int i = 0; i < 12; i++)
                     for (int yi = 0; yi < 12; yi++)
                     {
                         var rot = random.NextDouble() * Math.PI;

                         var shadow = new Image(
                            textures_greentank_shadow()
                            )
                             {
                             }.AttachTo(
                             Content
                         );

                         var q = new Sprite().AttachTo(Content);



                         var tanktrackpattern1 = new Image(
                              textures_tanktrackpattern()
                              )
                                 {
                                 }.AttachTo(
                               q
                           );


                         var tanktrackpattern0 = new Image(
                              textures_tanktrackpattern()
                              )
                         {
                         }.AttachTo(
                               q
                           );


                         var imgstand = new Image(
                            textures_greentank()
                            )
                         {
                         }.AttachTo(
                             q
                         );

                         var guntower = new Image(
                             textures_greentank_guntower()
                             )
                             {
                             }.AttachTo(
                              q
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


                         Action<Image, double> offsetTexCoords =
                             (img, offset) =>
                             {
                                 img.setTexCoords(0, new Point(0, offset));
                                 img.setTexCoords(1, new Point(1, offset));
                                 img.setTexCoords(2, new Point(0, offset + 1));
                                 img.setTexCoords(3, new Point(1, offset + 1));
                             };



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

                         {
                             var cm = new Matrix();

                             cm.translate(-64, -64);

                             // shadow with tracks!
                             cm.scale(1.2, 1.0);
                             cm.rotate(rot);
                             cm.translate(i * 128, yi * 128);

                             cm.translate(8, 8);

                             shadow.transformationMatrix = cm;
                         }

                         {
                             var cm = new Matrix();

                             cm.rotate(rot);
                             cm.translate(i * 128, yi * 128);


                             q.transformationMatrix = cm;
                         }



                         onframe += delegate
                         {
                             var offset = 1.0 - (gametime.ElapsedMilliseconds % 2000) / 2000.0;
                             offsetTexCoords(tanktrackpattern0, offset);
                             offsetTexCoords(tanktrackpattern1, offset - 1);
                         };
                     }

             };
        }
    }
}
