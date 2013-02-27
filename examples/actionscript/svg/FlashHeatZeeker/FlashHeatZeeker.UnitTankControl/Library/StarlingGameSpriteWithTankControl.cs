using Box2D.Collision.Shapes;
using Box2D.Common.Math;
using Box2D.Dynamics;
using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.UnitJeepControl.Library;
using FlashHeatZeeker.UnitTank.Library;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.Shared.BCLImplementation.GLSL;
using starling.display;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;


namespace FlashHeatZeeker.UnitTankControl.Library
{
    public class StarlingGameSpriteWithTankControl : StarlingGameSpriteWithPhysics
    {
        public StarlingGameSpriteWithTankControl()
        {
            var textures = new StarlingGameSpriteWithTankTextures(new_tex_crop);


            this.onbeforefirstframe += (stage, s) =>
            {

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


                var tank1 = new PhysicalTank(textures, this);

                current = tank1;

                var tank2 = new PhysicalTank(textures, this);



                onsyncframe += delegate
                {

                    tank1.SetVelocityFromInput(__keyDown);
                };
            };
        }
    }
}
