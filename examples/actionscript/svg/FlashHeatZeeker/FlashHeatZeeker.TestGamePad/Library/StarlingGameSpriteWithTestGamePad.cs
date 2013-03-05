using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.UnitPed.Library;
using FlashHeatZeeker.UnitPedControl.Library;
using FlashHeatZeeker.UnitTank.Library;
using FlashHeatZeeker.UnitTankControl.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlashHeatZeeker.TestGamePad.Library
{
    class StarlingGameSpriteWithTestGamePad : StarlingGameSpriteWithPhysics
    {
        // X:\jsc.svn\examples\actionscript\MultitouchExample\MultitouchFingerTools.FlashLAN\Program.cs
        // X:\jsc.community\zproxygames\AvalonSki\AvalonSkiAcceleratedEgo\ApplicationSprite.cs


        public static KeySample __keyDown = new KeySample();


        public StarlingGameSpriteWithTestGamePad()
        {
            var textures_tank = new StarlingGameSpriteWithTankTextures(new_tex_crop);
            var textures_ped = new StarlingGameSpriteWithPedTextures(new_tex_crop);

            disablephysicsdiagnostics = true;
            disable_movezoom_and_altitude_for_scale = true;

            internalscale = 1.5;
            internal_center_y = 0.6;

            this.onbeforefirstframe += (stage, s) =>
            {

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


                var tank1 = new PhysicalTank(textures_tank, this);
                tank1.AngularVelocityMultiplier = 0.5;
                tank1.SetPositionAndAngle(100, 100);

                current = tank1;


                var physical0 = new PhysicalPed(textures_ped, this);
                physical0.SetPositionAndAngle(100, 100);


                onsyncframe += delegate
                {

                    current.SetVelocityFromInput(__keyDown);


                    while (Content_layer0_tracks.numChildren > 5)
                    {
                        Content_layer0_tracks.removeChildAt(0);
                    }

                };
            };
        }
    }
}
