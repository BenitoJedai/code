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

            //this.internalscale = 1.0;
            this.disablephysicsdiagnostics = true;

            this.onbeforefirstframe += (stage, s) =>
            {
                // hind is looking right


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
