﻿using FlashHeatZeeker.Core.Library;
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
        public Image visualshadow;
        public Sprite visualbody;
        public Image visualnowings;
        public Sprite visualwings;


        public Action<double, double, double> SetPositionAndAngle { get; set; }
        public Action<Stopwatch> Animate;

        public double Altitude;

        public VisualHind(
            StarlingGameSpriteWithHindTextures textures,
            DisplayObjectContainer Content,
            double airzoom)
        {
            #region currentvisual
            visualshadow = new Image(textures.hind0_shadow()).AttachTo(Content);
            visualbody = new Sprite().AttachTo(Content);

            visualnowings = new Image(textures.hind0_nowings()).AttachTo(visualbody);
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


            {
                var cm = new Matrix();

                cm.translate(-160, -160);


                visualnowings.transformationMatrix = cm;
            }
            #endregion


            #region SetPositionAndAngle
            SetPositionAndAngle =
                (x, y, angle) =>
                {
                    #region transformationMatrix, phisics updated, now update visual




                    {
                        var cm = new Matrix();


                        cm.translate(-160, -160);


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

                        cm.translate(96 * airzoom * Altitude, 96 * airzoom * Altitude);

                        visualshadow.transformationMatrix = cm;
                    }

                    {
                        var cm = new Matrix();

                        cm.rotate(angle + Math.PI / 2);

                        cm.scale(1 + airzoom * Altitude, 1 + airzoom * Altitude);

                        cm.translate(
                            x,
                            y
                        );

                        visualbody.transformationMatrix = cm;
                    }
                    #endregion
                };
            #endregion

            #region Animate
            var AnimateElapsed = new Stopwatch();
            var AnimateMatrix = new Matrix();
            Animate =
                (gametime) =>
                {
                    AnimateMatrix.rotate(AnimateElapsed.ElapsedMilliseconds * 0.001 * (1 + 4 * Math.Sign(Altitude)));
                    visualwings.transformationMatrix = AnimateMatrix;
                    AnimateElapsed.Restart();
                };
            #endregion

        }
    }

}
