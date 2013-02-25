using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.UnitTank.Library;
using FlashHeatZeeker.UnitTankControl.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlashHeatZeeker.CoreMap.Library
{
    class StarlingGameSpriteWithMap : StarlingGameSpriteWithPhysics
    {
        public StarlingGameSpriteWithMap()
        {
            var textures = new StarlingGameSpriteWithTankTextures(new_tex_crop);

            this.onbeforefirstframe += (stage, s) =>
            {

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




                var tank1 = new PhysicalTank(textures, this);
                current = tank1.body;

                onframe += delegate
                {

                    tank1.SetVelocityFromInput(__keyDown);




                };
            };
        }
    }
}
