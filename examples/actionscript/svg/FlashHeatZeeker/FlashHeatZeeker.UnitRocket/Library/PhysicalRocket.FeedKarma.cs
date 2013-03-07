using Box2D.Common.Math;
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
        public void FeedKarma()
        {
            if (this.KarmaInput0.Count > 0)
            {
                var k = new KeySample
                {
                    value = CurrentInput.value,

                    BodyIsActive = this.body.IsActive(),

                    fixup = true,

                    angle = this.body.GetAngle(),
                    x = this.body.GetPosition().x,
                    y = this.body.GetPosition().y,
                };

                if (CurrentInput.fixup)
                {
                    k.x = CurrentInput.x;
                    k.y = CurrentInput.y;
                    k.angle = CurrentInput.angle;
                }



                this.KarmaInput0.Enqueue(k);
                this.KarmaInput0.Dequeue();
            }
        }


    }
}
