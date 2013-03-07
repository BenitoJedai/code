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
using System.Windows.Forms;

namespace FlashHeatZeeker.UnitRocket.Library
{
    public partial class PhysicalRocket : IPhysicalUnit
    {
        public b2Body body { get; set; }
        public b2Body karmabody { get; set; }


        public void SetPositionAndAngle(double x, double y, double a = 0)
        {
            this.body.SetPositionAndAngle(
                new b2Vec2(x, y), a
            );

            this.karmabody.SetPositionAndAngle(
              new b2Vec2(x, y), a
            );

        }


    }
}
