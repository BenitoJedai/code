using FlashHeatZeeker.StarlingSetup.Library;
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

            var airzoom = 1.5;

            this.onbeforefirstframe += delegate
              {

                  for (int i = 0; i < 12; i++)
                      for (int yi = 0; yi < 12; yi++)
                      {
                          var rot = random.NextDouble() * Math.PI;

                          var visual1 = new VisualHind(textures, Content, airzoom);


                          visual1.SetPositionAndAngle(
                             i * 400, yi * 400,
                             rot
                         );

                          onframe +=
                              delegate
                              {
                                  visual1.Animate(gametime);

                              };
                      }
              };
        }
    }
}
