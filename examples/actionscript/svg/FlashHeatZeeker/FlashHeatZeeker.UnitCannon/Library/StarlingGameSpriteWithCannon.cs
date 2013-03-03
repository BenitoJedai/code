using FlashHeatZeeker.StarlingSetup.Library;
using starling.textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlashHeatZeeker.UnitCannon.Library
{
    public class StarlingGameSpriteWithCannonTextures
    {
        public Func<Texture>
            tracergun_guntower,
            tracergun_guntower_shadow,
            tracergun_mount;

        public StarlingGameSpriteWithCannonTextures(Texture64Constructor new_tex_crop)
        {
            tracergun_guntower = new_tex_crop("assets/FlashHeatZeeker.UnitCannon/tracergun_guntower.svg", innersize: 128);
            tracergun_guntower_shadow = new_tex_crop("assets/FlashHeatZeeker.UnitCannon/tracergun_guntower_shadow.svg", innersize: 128, alpha: 0.15);
            tracergun_mount = new_tex_crop("assets/FlashHeatZeeker.UnitCannon/tracergun_mount.svg", innersize: 128);

        }
    }

    class StarlingGameSpriteWithCannon : StarlingGameSpriteBase
    {
        public StarlingGameSpriteWithCannon()
        {
            autorotate = true;

            var textures = new StarlingGameSpriteWithCannonTextures(new_tex_crop);

            onbeforefirstframe += (stage, s) =>
            {
                for (int i = 0; i < 12; i++)
                    for (int yi = 0; yi < 12; yi++)
                    {
                        var rot = random.NextDouble() * Math.PI;


                        var cannon1 = new VisualCannon(textures, this);

                        cannon1.SetPositionAndAngle(
                            i * 128, yi * 128, rot);

                    }
            };

        }
    }
}
