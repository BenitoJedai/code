using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.UnitHind.Library;
using FlashHeatZeeker.UnitHindControl.Library;
using FlashHeatZeeker.UnitRocket.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;
using FlashHeatZeeker.CoreAudio.Library;
using System.Windows.Forms;
using FlashHeatZeeker.UnitJeepControl.Library;
using FlashHeatZeeker.CoreMap.Library;
using starling.display;

namespace FlashHeatZeeker.UnitHindWeaponized.Library
{
    class StarlingGameSpriteWithHindWeaponized : StarlingGameSpriteWithPhysics
    {
        public static KeySample __keyDown = new KeySample();
        public Soundboard sb = new Soundboard();

        public StarlingGameSpriteWithHindWeaponized()
        {
            var textures_hind = new StarlingGameSpriteWithHindTextures(this.new_tex_crop);
            var textures_rocket = new StarlingGameSpriteWithRocketTextures(this.new_tex_crop);
            var textures_map = new StarlingGameSpriteWithMapTextures(new_tex_crop);

            //this.internalscale = 1.0;
            //this.disablephysicsdiagnostics = true;

            this.onbeforefirstframe += (stage, s) =>
            {
                stage.color = 0x75C64F;

                // hind is looking right

                for (int i = -12; i < 12; i++)
                {
                    new Image(textures_map.road0()).AttachTo(Content).x = 256 * i;
                }

                #region other units
                for (int i = 3; i < 9; i++)
                {
                    var hind2 = new PhysicalHindWeaponized(textures_hind, textures_rocket, this)
                    {
                        AutomaticTakeoff = true
                    };

                    hind2.SetPositionAndAngle(
                        i * 16, 8, random.NextDouble()
                    );





                }
                #endregion



                var hind0 = new PhysicalHindWeaponized(
                    textures_hind, textures_rocket, this);


                current = hind0;


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

                bool mode_changepending = false;

                onsyncframe +=
                   delegate
                   {
                       #region mode
                       if (!__keyDown[System.Windows.Forms.Keys.Space])
                       {
                           // space is not down.
                           mode_changepending = true;
                       }
                       else
                       {
                           if (mode_changepending)
                           {
                               (current as PhysicalHind).With(
                                   hind1 =>
                                   {
                                       if (hind1.visual.Altitude == 0)
                                           hind1.VerticalVelocity = 1.0;
                                       else
                                           hind1.VerticalVelocity = -0.4;

                                   }
                               );






                               mode_changepending = false;



                           }
                       }
                       #endregion


                       current.SetVelocityFromInput(__keyDown);



                       #region simulate a weapone!
                       if (__keyDown[System.Windows.Forms.Keys.ControlKey])
                           if (syncframeid % 3 == 0)
                           {
                               sb.snd_missleLaunch.play();
                               hind0.FireRocket();
                           }
                       #endregion
                   };
            };

        }
    }
}
