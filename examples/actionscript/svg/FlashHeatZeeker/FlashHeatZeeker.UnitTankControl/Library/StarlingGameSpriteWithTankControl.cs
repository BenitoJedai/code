using Box2D.Collision.Shapes;
using Box2D.Common.Math;
using Box2D.Dynamics;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.UnitTank.Library;
using ScriptCoreLib.ActionScript.flash.geom;
using starling.display;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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

                var tank2 = new PhysicalTank(textures, this);
                onframe += delegate
                {

                    tank1.SetVelocityFromInput(__keyDown);




                };
            };
        }
    }
}
