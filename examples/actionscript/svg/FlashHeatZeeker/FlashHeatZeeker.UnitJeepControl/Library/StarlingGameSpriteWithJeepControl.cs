using Box2D.Common.Math;
using Box2D.Dynamics;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.UnitJeep.Library;
using FlashHeatZeekerWithStarlingB2.Library;
using ScriptCoreLib.ActionScript.flash.geom;
using starling.display;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;
using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.UnitBunkerControl.Library;

namespace FlashHeatZeeker.UnitJeepControl.Library
{
    public class StarlingGameSpriteWithJeepControl : StarlingGameSpriteWithPhysics
    {
        // hacky way, yet user probably ahs only one keyboard / set of hands anyhow
        public static KeySample __keyDown = new KeySample();

        public StarlingGameSpriteWithJeepControl()
        {
            this.autorotate = false;



            var textures_bunker = new StarlingGameSpriteWithBunkerTextures(new_tex_crop);
            var textures_jeep = new StarlingGameSpriteWithJeepTextures(new_tex_crop);

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



                var jeep = new PhysicalJeep(textures_jeep, this);


                current = jeep;

                new PhysicalJeep(textures_jeep, this);
                new PhysicalJeep(textures_jeep, this);


                new PhysicalBarrel(textures_bunker, this).SetPositionAndAngle(12, 0);
                new PhysicalBarrel(textures_bunker, this).SetPositionAndAngle(12, -4);
                new PhysicalBarrel(textures_bunker, this).SetPositionAndAngle(12, -8);


                onsyncframe += delegate
                {

                    current.SetVelocityFromInput(__keyDown);


                };
            };
        }
    }
}
