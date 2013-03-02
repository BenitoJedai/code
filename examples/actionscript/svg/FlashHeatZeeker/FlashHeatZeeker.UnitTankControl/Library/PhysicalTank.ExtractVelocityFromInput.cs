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
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlashHeatZeeker.UnitTankControl.Library
{
    public partial class PhysicalTank : IPhysicalUnit
    {
        public void ExtractVelocityFromInput(KeySample __keyDown, Velocity value)
        {


            value.AngularVelocity = 0;
            value.LinearVelocityX = 0;
            value.LinearVelocityY = 0;



            if (__keyDown != null)
            {

                if (__keyDown[Keys.Up])
                {
                    // we have reasone to keep walking

                    value.LinearVelocityY = 1;
                }

                if (__keyDown[Keys.Down])
                {
                    // we have reasone to keep walking
                    // go slow backwards
                    value.LinearVelocityY = -0.5;

                }


                if (__keyDown[Keys.Left])
                {
                    // we have reasone to keep walking

                    value.AngularVelocity = -1;

                }

                if (__keyDown[Keys.Right])
                {
                    // we have reasone to keep walking

                    value.AngularVelocity = 1;

                }

            }


        }



    }
}
