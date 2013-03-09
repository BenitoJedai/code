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
        public RemoteGame RemoteGameReference { get; set; }

        public Queue<KeySample> KarmaInput0 = new Queue<KeySample>();
        public double speed = 20;

        Stopwatch ApplyVelocityElapse = new Stopwatch();
        public bool issmoke;
        public double smokerandom;
        public double smokescale = 2.0;
        public long smoketime;
        public double smoketimelength = 4500.0;

        public void ApplyVelocity()
        {
            // this is now

            {
                var current = this.body;
                //var v = velocity.AngularVelocity * 10;         current.SetAngularVelocity(v);




                var vx = Math.Cos(current.GetAngle()) * velocity.LinearVelocityY * this.speed * this.CurrentInput.forcey
                    + Math.Cos(current.GetAngle() + Math.PI / 2) * velocity.LinearVelocityX * this.speed;
                var vy = Math.Sin(current.GetAngle()) * velocity.LinearVelocityY * this.speed * this.CurrentInput.forcey
                        + Math.Sin(current.GetAngle() + Math.PI / 2) * velocity.LinearVelocityX * this.speed;


                //this.visual.currentvisual.alpha = 1.0;



                current.SetLinearVelocity(
                    new b2Vec2(
                     vx, vy
                    )
                );



                current.ApplyAngularImpulse(
                    velocity.AngularVelocity
                      * this.CurrentInput.forcex
                    * ApplyVelocityElapse.ElapsedMilliseconds
                    * 0.01
                    * ((this.body.GetLinearVelocity().Length() / this.speed).Min(0.9) * 0.5)
                );


            }


            ApplyVelocityElapse.Restart();
        }


    }
}
