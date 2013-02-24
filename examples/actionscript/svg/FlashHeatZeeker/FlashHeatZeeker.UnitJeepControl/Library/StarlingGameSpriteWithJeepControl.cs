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

namespace FlashHeatZeeker.UnitJeepControl.Library
{
    public class StarlingGameSpriteWithJeepControl : StarlingGameSpriteWithPhysics
    {
        // hacky way, yet user probably ahs only one keyboard / set of hands anyhow
        public static object[] __keyDown = new object[0xffffff];

        public StarlingGameSpriteWithJeepControl()
        {
            this.autorotate = false;

            current_rotation_extra = 0;


            var textures = new StarlingGameSpriteWithJeepTextures(new_tex_crop);

            this.onbeforefirstframe += (stage, s) =>
            {

                #region __keyDown

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


                var jeep = new PhysicalJeep(textures, this);


                current = jeep.unit4_physics.body;



                onframe += delegate
                {

                    jeep.SetVelocityFromInput(__keyDown);

                };
            };
        }
    }
}
