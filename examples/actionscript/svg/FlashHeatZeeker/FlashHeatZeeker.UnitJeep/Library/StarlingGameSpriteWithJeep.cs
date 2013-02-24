using FlashHeatZeeker.StarlingSetup.Library;
using ScriptCoreLib.ActionScript.flash.geom;
using starling.display;
using starling.textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlashHeatZeeker.UnitJeep.Library
{
    public class StarlingGameSpriteWithJeepTextures
    {
        public Func<Texture>
          black4,
          jeep,
          jeep_shadow;

        public StarlingGameSpriteWithJeepTextures(Texture64Constructor new_tex_crop)
        {
            // http://forum.starling-framework.org/topic/confirmation-on-optimum-quadbatch-use
            // hack, Quad should do the work, yet it drags performance!
            black4 = new_tex_crop("assets/FlashHeatZeeker.UnitJeep/jeep_shadow.svg", innersize: 4);


            jeep = new_tex_crop("assets/FlashHeatZeeker.UnitJeep/jeep.svg");
            jeep_shadow = new_tex_crop("assets/FlashHeatZeeker.UnitJeep/jeep_shadow.svg", alpha: 0.3);


        }
    }

    public sealed class StarlingGameSpriteWithJeep : StarlingGameSpriteBase
    {
        public StarlingGameSpriteWithJeep()
        {
            this.autorotate = true;

            var textures = new StarlingGameSpriteWithJeepTextures(new_tex_crop);

            this.onbeforefirstframe += delegate
            {


                //peds.Add(imgstand);

                //var count = 12;
                var count = 8;

                for (int i = 0; i < count; i++)
                    for (int yi = 0; yi < count; yi++)
                    {
                        var visual0 = new VisualJeep(textures, this);


                        visual0.SetPositionAndAngle(
                            i * 512, yi * 512,
                            random.NextDouble()
                        );




                    }

                //var t0 = new Quad(32, 32, 0).AttachTo(
                //                    Content
                //    //q
                //                );

            };

        }
    }
}
