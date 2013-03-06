using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.StarlingSetup.Library;
using starling.display;
using starling.textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlashHeatZeeker.UnitRocket.Library
{
    public class StarlingGameSpriteWithRocketTextures
    {
        public Func<Texture>
            rocket1;

        public StarlingGameSpriteWithRocketTextures(Texture64Constructor new_tex_crop)
        {
            //DRW 3

            rocket1 = new_tex_crop("assets/FlashHeatZeeker.UnitRocket/rocket1.svg", innersize: 128);
        }

    }

    class StarlingGameSpriteWithRocket : StarlingGameSpriteWithPhysics
    {
        public StarlingGameSpriteWithRocket()
        {
            var textures_rocket = new StarlingGameSpriteWithRocketTextures(this.new_tex_crop);

            this.onbeforefirstframe += (stage, s) =>
            {
                var n = new Image(textures_rocket.rocket1());


                n.AttachTo(this);

            };
        }
    }
}
