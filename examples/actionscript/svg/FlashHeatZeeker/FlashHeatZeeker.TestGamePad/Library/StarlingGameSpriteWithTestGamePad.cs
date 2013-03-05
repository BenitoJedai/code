using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.UnitBunkerControl.Library;
using FlashHeatZeeker.UnitCannon.Library;
using FlashHeatZeeker.UnitCannonControl.Library;
using FlashHeatZeeker.UnitHind.Library;
using FlashHeatZeeker.UnitHindControl.Library;
using FlashHeatZeeker.UnitJeep.Library;
using FlashHeatZeeker.UnitJeepControl.Library;
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

        public static Action<string> __switchto = delegate { };

        public StarlingGameSpriteWithTestGamePad()
        {
            var textures_ped = new StarlingGameSpriteWithPedTextures(new_tex_crop);
            var textures_tank = new StarlingGameSpriteWithTankTextures(new_tex_crop);
            var textures_hind = new StarlingGameSpriteWithHindTextures(this.new_tex_crop);
            var textures_jeep = new StarlingGameSpriteWithJeepTextures(new_tex_crop);
            var textures_cannon = new StarlingGameSpriteWithCannonTextures(new_tex_crop);
            var textures_bunker = new StarlingGameSpriteWithBunkerTextures(new_tex_crop);

            disablephysicsdiagnostics = true;
            disable_movezoom_and_altitude_for_scale = true;

            internalscale = 1.7;
            internal_center_y = 0.5;

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
                tank1.SetPositionAndAngle(random.NextDouble() * 4000, 100);

                // half speed!
                //tank1.AngularVelocityMultiplier = 0.5;
                //tank1.speed = 10;

                //tank1.SetPositionAndAngle(100, 100);



                var cannon1 = new PhysicalCannon(textures_cannon, this);
                cannon1.SetPositionAndAngle(random.NextDouble() * 4000, 100);

                var physical0 = new PhysicalPed(textures_ped, this);
                physical0.SetPositionAndAngle(random.NextDouble() * 4000, 100);

                current = physical0;

                var hind0 = new PhysicalHind(textures_hind, this);
                hind0.SetPositionAndAngle(random.NextDouble() * 4000, 100);

                var jeep = new PhysicalJeep(textures_jeep, this);
                jeep.SetPositionAndAngle(random.NextDouble() * 4000, 100);

                var bunker0 = new PhysicalBunker(textures_bunker, this);
                bunker0.SetPositionAndAngle(random.NextDouble() * 4000, 100);

                var silo0 = new PhysicalSilo(textures_bunker, this);

                __switchto +=
                    type =>
                    {
                        if (type == "ped")
                            current = physical0;

                        if (type == "tank")
                            current = tank1;

                        if (type == "hind")
                            current = hind0;

                        if (type == "jeep")
                            current = jeep;

                        if (type == "cannon")
                            current = cannon1;

                        if (type == "bunker")
                            current = bunker0;

                        if (type == "silo")
                            current = silo0;
                    };

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
