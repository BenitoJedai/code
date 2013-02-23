﻿using FlashHeatZeeker.StarlingSetup.Library;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.Extensions;
using starling.display;
using starling.textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlashHeatZeeker.UnitHind.Library
{
    public class StarlingGameSpriteWithHindTextures 
    {
        public Func<Texture>
          hind0_nowings,
          hind0_shadow,
          hind0_wing1;

        public StarlingGameSpriteWithHindTextures(Texture64Constructor new_tex_crop)
        {

             hind0_nowings = new_tex_crop("assets/FlashHeatZeeker.UnitHind/hind0_nowings.svg", innersize: 320);
             hind0_shadow = new_tex_crop("assets/FlashHeatZeeker.UnitHind/hind0_shadow.svg", innersize: 320, alpha: 0.3);
             hind0_wing1 = new_tex_crop("assets/FlashHeatZeeker.UnitHind/hind0_wing1.svg", innersize: 320);

        }
    }

    public class StarlingGameSpriteWithHind : StarlingGameSpriteBase
    {

        public StarlingGameSpriteWithHind()
        {
            this.autorotate = true;

            var textures = new StarlingGameSpriteWithHindTextures(this.new_tex_crop);


            this.onbeforefirstframe += delegate
              {

                  for (int i = 0; i < 12; i++)
                      for (int yi = 0; yi < 12; yi++)
                      {
                          var rot = random.NextDouble() * Math.PI;


                          var shadow = new Image(
                            textures.hind0_shadow()
                            )
                              {
                              }.AttachTo(
                             Content
                         );

                          var visual = new Sprite().AttachTo(Content);

                          var nowings = new Image(
                            textures.hind0_nowings()
                            )
                              {
                              }.AttachTo(visual);

                          var wings = new Sprite().AttachTo(visual);

                          Enumerable.Range(0, 5).Select(
                              wingindex =>
                                  new Image(textures.hind0_wing1()).AttachTo(wings).With(
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


                              nowings.transformationMatrix = cm;
                          }


                          {
                              var cm = new Matrix();

                              cm.translate(-160, -160);

                              // shadow with tracks!
                              cm.rotate(rot);
                              cm.translate(i * 400, yi * 400);

                              cm.translate(8, 8);

                              shadow.transformationMatrix = cm;
                          }

                          {
                              var cm = new Matrix();


                              // shadow with tracks!
                              cm.rotate(rot);
                              cm.translate(i * 400, yi * 400);


                              visual.transformationMatrix = cm;
                          }

                          onframe +=
                              delegate
                              {
                                  {
                                      var cm = new Matrix();

                                      cm.rotate(this.gametime.ElapsedMilliseconds * 0.001);


                                      wings.transformationMatrix = cm;
                                  }
                              };
                      }
              };
        }
    }
}
