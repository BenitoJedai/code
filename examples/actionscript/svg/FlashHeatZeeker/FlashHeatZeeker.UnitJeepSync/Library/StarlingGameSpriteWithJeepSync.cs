using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.UnitJeep.Library;
using FlashHeatZeeker.UnitJeepControl.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlashHeatZeeker.UnitJeepSync.Library
{
    class StarlingGameSpriteWithJeepSync : StarlingGameSpriteWithPhysics
    {
        // hacky way, yet user probably ahs only one keyboard / set of hands anyhow
        public static KeySample __keyDown = new KeySample();

        public StarlingGameSpriteWithJeepSync()
        {
            this.autorotate = false;



            var textures = new StarlingGameSpriteWithJeepTextures(new_tex_crop);

            this.onbeforefirstframe += (stage, s) =>
            {
                #region __keyDown

                stage.keyDown +=
                   e =>
                   {
                       // http://circlecube.com/2008/08/actionscript-key-listener-tutorial/
                       if (e.altKey)
                           __keyDown[System.Windows.Forms.Keys.Alt] = true;

                       __keyDown[(System.Windows.Forms.Keys)e.keyCode] = true;
                   };

                stage.keyUp +=
                 e =>
                 {
                     if (!e.altKey)
                         __keyDown[System.Windows.Forms.Keys.Alt] = false;

                     __keyDown[(System.Windows.Forms.Keys)e.keyCode] = false;
                 };

                #endregion



                var ego = new PhysicalJeep(textures, this)
                {
                    Identity = egoid + ":ego"
                };

                ego.SetPositionAndAngle(
                   random.NextDouble() * -8 - 4,
                   random.NextDouble() * -8 - 4,
                   random.NextDouble() * Math.PI
               );


                current = ego;

                new PhysicalJeep(textures, this);
                new PhysicalJeep(textures, this);


                onsyncframe += delegate
                {

                    current.SetVelocityFromInput(__keyDown);


                };
            };
        }
    }
}
