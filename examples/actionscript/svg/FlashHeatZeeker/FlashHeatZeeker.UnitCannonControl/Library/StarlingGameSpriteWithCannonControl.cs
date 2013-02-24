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
                var __keyDown = new object[0xffffff];

                stage.keyDown +=
                   e =>
                   {
                       if (__keyDown[e.keyCode] != null)
                           return;

                       // http://circlecube.com/2008/08/actionscript-key-listener-tutorial/
                       if (e.altKey)
                           __keyDown[(int)Keys.Alt] = new object();

                       __keyDown[e.keyCode] = new object();
                   };

                stage.keyUp +=
                 e =>
                 {
                     if (!e.altKey)
                         __keyDown[(int)Keys.Alt] = null;

                     __keyDown[e.keyCode] = null;
                 };

                #endregion


                onframe += delegate
                  {


                      current = cannon1.body;
                      cannon1.SetVelocityFromInput(__keyDown);
                  };

            };
        }
    }
}
