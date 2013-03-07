using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.UnitJeepControl.Library;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.Extensions;
using starling.display;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FlashHeatZeeker.UnitHind.Library
{
    public class VisualHind : IVisualUnit
    {
        public Sprite visualbody_shadow;
        public Image visualnowings_shadow;

        public Sprite visualwings_shadow;

        public Sprite visualbody;
        public Image visualnowings;


        public Sprite visualwings;
        public Sprite visualwings2;
        public Sprite visualwings3;


        public void SetPositionAndAngle(double x, double y, double angle)
        {
            #region transformationMatrix, phisics updated, now update visual



            #region visualbody_shadow
            {
                var cm = new Matrix();


                //cm.translate(-160, -160);


                // shadow with tracks!
                cm.scale(0.5, 0.5);

                // shadow should be smaller!
                cm.scale(1.2 - 0.3 * this.Altitude, 1.2 - 0.3 * this.Altitude);
                //cm.rotate(rot);
                //cm.translate(i * 128, yi * 128);



                cm.rotate(angle + Math.PI / 2);
                cm.translate(
                    x,
                    y
                );


                cm.translate(8, 8);

                cm.translate(96 * airzoom * Altitude, 96 * airzoom * Altitude);

                visualbody_shadow.transformationMatrix = cm;
            }
            #endregion

            {
                var cm = new Matrix();

                cm.scale(0.5, 0.5);
                cm.rotate(angle + Math.PI / 2);

                cm.scale(1 + airzoom * Altitude, 1 + airzoom * Altitude);

                cm.translate(
                    x,
                    y
                );

                visualbody.transformationMatrix = cm;
            }
            #endregion
        }

        Matrix AnimateMatrix = new Matrix();
        Stopwatch AnimateElapsed = new Stopwatch();
        public void Animate(Stopwatch gametime, double BaseSpeed = 1.0, double SpeedWhenInAir = 4.0)
        {
            var speedup = (BaseSpeed + SpeedWhenInAir * Math.Sign(Altitude));

            AnimateMatrix.rotate(AnimateElapsed.ElapsedMilliseconds * 0.001 * speedup);
            visualwings.transformationMatrix = AnimateMatrix;
            visualwings_shadow.transformationMatrix = AnimateMatrix;

            var g = AnimateMatrix.clone();
            g.rotate(-1.DegreesToRadians() * speedup);
            visualwings2.transformationMatrix = g;

            g.rotate(-1.DegreesToRadians() * speedup);
            visualwings3.transformationMatrix = g;

            AnimateElapsed.Restart();
        }

        public double Altitude;
        public double airzoom;

        public VisualHind(
            StarlingGameSpriteWithHindTextures textures,
            DisplayObjectContainer Content,
            double airzoom)
        {
            this.airzoom = airzoom;
            visualbody_shadow = new Sprite().AttachTo(Content);
            visualnowings_shadow = new Image(textures.hind0_shadow()).AttachTo(visualbody_shadow);

            visualbody = new Sprite().AttachTo(Content);
            visualnowings = new Image(textures.hind0_nowings()).AttachTo(visualbody);


            visualwings_shadow = new Sprite().AttachTo(visualbody_shadow);
            Enumerable.Range(0, 5).Select(
                wingindex =>
                    new Image(textures.hind0_wing1shadow()).AttachTo(visualwings_shadow).With(
                      img =>
                      {
                          var cm = new Matrix();

                          cm.translate(-160, -160);
                          cm.rotate(Math.PI * 2 * wingindex / 5);


                          img.transformationMatrix = cm;

                      }
                    )
            ).ToArray();

            visualwings = new Sprite().AttachTo(visualbody);
            Enumerable.Range(0, 5).Select(
                wingindex =>
                    new Image(textures.hind0_wing1()).AttachTo(visualwings).With(
                      img =>
                      {
                          var cm = new Matrix();

                          cm.translate(-160, -160);
                          cm.rotate(Math.PI * 2 * wingindex / 5);


                          img.transformationMatrix = cm;

                      }
                    )
            ).ToArray();

            visualwings2 = new Sprite().AttachTo(visualbody);
            Enumerable.Range(0, 5).Select(
                wingindex =>
                    new Image(textures.hind0_wing2()).AttachTo(visualwings2).With(
                      img =>
                      {
                          var cm = new Matrix();

                          cm.translate(-160, -160);
                          cm.rotate(Math.PI * 2 * wingindex / 5);


                          img.transformationMatrix = cm;

                      }
                    )
            ).ToArray();

            visualwings3 = new Sprite().AttachTo(visualbody);
            Enumerable.Range(0, 5).Select(
                wingindex =>
                    new Image(textures.hind0_wing3()).AttachTo(visualwings3).With(
                      img =>
                      {
                          var cm = new Matrix();

                          cm.translate(-160, -160);
                          cm.rotate(Math.PI * 2 * wingindex / 5);


                          img.transformationMatrix = cm;

                      }
                    )
            ).ToArray();

            {
                var cm = new Matrix();
                cm.translate(-160, -160);
                visualnowings.transformationMatrix = cm;
            }


            {
                var cm = new Matrix();
                cm.translate(-160, -160);
                visualnowings_shadow.transformationMatrix = cm;
            }




        }
    }

}
