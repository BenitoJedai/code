using Box2D.Collision.Shapes;
using Box2D.Common.Math;
using Box2D.Dynamics;
using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.StarlingSetup.Library;
using FlashHeatZeeker.UnitHind.Library;
using FlashHeatZeeker.UnitJeepControl.Library;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.Extensions;
using starling.display;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace FlashHeatZeeker.UnitHindControl.Library
{
    public partial class PhysicalHind : IPhysicalUnit
    {


        public Queue<KeySample> KarmaInput0 = new Queue<KeySample>();

        public void FeedKarma()
        {
            if (this.KarmaInput0.Count > 0)
            {
                var k = new KeySample
                {
                    value = CurrentInput.value,
                    angle = this.body.GetAngle(),
                    BodyIsActive = this.ground_body.IsActive(),

                    fixup = true,
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
