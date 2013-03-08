using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.StarlingSetup.Library;
using starling.display;
using starling.textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlashHeatZeeker.UnitRocket.Library
{
    public class StarlingGameSpriteWithRocketTextures
    {
        public Func<Texture>
            smoke1,
            rocket1,
            rocket1_burn1,
            rocket1_burn2;

        public StarlingGameSpriteWithRocketTextures(Texture64Constructor new_tex_crop)
        {
            //DRW 3

            smoke1 = new_tex_crop("assets/FlashHeatZeeker.UnitRocket/smoke1.svg", innersize: 128, alpha: 0.3);

            rocket1 = new_tex_crop("assets/FlashHeatZeeker.UnitRocket/rocket1.svg", innersize: 128);
            rocket1_burn1 = new_tex_crop("assets/FlashHeatZeeker.UnitRocket/rocket1_burn1.svg", innersize: 128);
            rocket1_burn2 = new_tex_crop("assets/FlashHeatZeeker.UnitRocket/rocket1_burn2.svg", innersize: 128);
        }

    }

    class StarlingGameSpriteWithRocket : StarlingGameSpriteWithPhysics
    {
        public static KeySample __keyDown = new KeySample();

        public StarlingGameSpriteWithRocket()
        {
            var textures_rocket = new StarlingGameSpriteWithRocketTextures(this.new_tex_crop);

            this.onbeforefirstframe += (stage, s) =>
            {

                var cl = new PhysicalRocket(textures_rocket, this);

                cl.issmoke = true;

                current = new PhysicalRocket(textures_rocket, this);


                #region __keyDown

                stage.keyDown +=
                   e =>
                   {
                       // http://circlecube.com/2008/08/actionscript-key-listener-tutorial/
                       if (e.altKey)
                           __keyDown[Keys.Alt] = true;

                       __keyDown[(Keys)e.keyCode] = true;
                   };

                stage.keyUp +=
                 e =>
                 {
                     if (!e.altKey)
                         __keyDown[Keys.Alt] = false;

                     __keyDown[(Keys)e.keyCode] = false;
                 };

                #endregion

                onsyncframe += delegate
                {
                    current.SetVelocityFromInput(__keyDown);
                };
            };
        }
    }
}
