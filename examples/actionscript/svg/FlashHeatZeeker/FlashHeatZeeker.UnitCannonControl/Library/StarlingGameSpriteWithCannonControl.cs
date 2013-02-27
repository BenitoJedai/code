using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.UnitCannon.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlashHeatZeeker.UnitCannonControl.Library
{
    class StarlingGameSpriteWithCannonControl : StarlingGameSpriteWithPhysics
    {
        public StarlingGameSpriteWithCannonControl()
        {
            var textures = new StarlingGameSpriteWithCannonTextures(new_tex_crop);

            onbeforefirstframe += (stage, s) =>
            {
                var cannon1 = new PhysicalCannon(textures, this);




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

                current = cannon1;
                onsyncframe += delegate
                  {


                      cannon1.SetVelocityFromInput(__keyDown);
                  };

            };
        }
    }
}
