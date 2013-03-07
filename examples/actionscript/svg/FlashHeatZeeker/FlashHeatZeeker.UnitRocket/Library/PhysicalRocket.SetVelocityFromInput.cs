using Box2D.Dynamics;
using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.UnitJeepControl.Library;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.Shared.BCLImplementation.GLSL;
using starling.display;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FlashHeatZeeker.UnitRocket.Library
{
    public partial class PhysicalRocket : IPhysicalUnit
    {
        public KeySample CurrentInput { get; set; }
        public void SetVelocityFromInput(KeySample __keyDown)
        {
            CurrentInput = __keyDown;






            ExtractVelocityFromInput(__keyDown, velocity);



            if (velocity.LinearVelocityX == 0)
                if (velocity.LinearVelocityY == 0)
                    if (velocity.AngularVelocity == 0)
                        return;


            // we are moving. stop laying on the ground mode


        }

    }
}
