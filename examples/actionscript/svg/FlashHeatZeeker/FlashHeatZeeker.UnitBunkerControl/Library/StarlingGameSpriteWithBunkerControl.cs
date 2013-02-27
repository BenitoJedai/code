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

namespace FlashHeatZeeker.UnitBunkerControl.Library
{
    public class StarlingGameSpriteWithBunkerTextures
    {
        public Func<Texture>
            bunker2,
            bunker2_shadow;

        public StarlingGameSpriteWithBunkerTextures(Texture64Constructor new_tex_crop)
        {
            bunker2 = new_tex_crop("assets/FlashHeatZeeker.UnitBunkerControl/bunker2.svg", innersize: 192);
            bunker2_shadow = new_tex_crop("assets/FlashHeatZeeker.UnitBunkerControl/bunker2_shadow.svg", innersize: 192, alpha: 0.3);

        }
    }

    class StarlingGameSpriteWithBunkerControl : StarlingGameSpriteWithPhysics
    {
        public StarlingGameSpriteWithBunkerControl()
        {


            var textures = new StarlingGameSpriteWithBunkerTextures(new_tex_crop);

            onbeforefirstframe += (stage, s) =>
            {
                #region __keyDown
                var __keyDown = new KeySample();

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

                var bunker0 = new PhysicalBunker(textures, this);

                current = bunker0;
                onsyncframe += delegate
                {


                    bunker0.SetVelocityFromInput(__keyDown);
                };
            };

        }
    }
}
