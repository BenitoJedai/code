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

namespace FlashHeatZeeker.UnitTankControl.Library
{
    public partial class PhysicalTank : IPhysicalUnit
    {
        Stopwatch ApplyVelocityElapse = new Stopwatch();

        public void ApplyVelocity()
        {

            {
                var current = this.body;
                //var v = this.AngularVelocity * 10;
                //current.SetAngularVelocity(v);

                current.ApplyAngularImpulse(
                   this.AngularVelocity
                   * ApplyVelocityElapse.ElapsedMilliseconds
                   * 1.0
                   * (1 - (this.body.GetLinearVelocity().Length() / this.speed).Min(0.9) * 0.5)
               );


                var vx = Math.Cos(current.GetAngle()) * this.LinearVelocityY * this.speed
                        + Math.Cos(current.GetAngle() + Math.PI / 2) * this.LinearVelocityX * this.speed;
                var vy = Math.Sin(current.GetAngle()) * this.LinearVelocityY * this.speed
                        + Math.Sin(current.GetAngle() + Math.PI / 2) * this.LinearVelocityX * this.speed;



                current.SetLinearVelocity(
                    new b2Vec2(
                     vx, vy


                    )
                );
            }


            // what about our karma body?
            if (this.KarmaInput0.Count > 0)
            {
                var _karma__keyDown = this.KarmaInput0.Peek();

                var _karma_velocity = new Velocity();


                ExtractVelocityFromInput(_karma__keyDown, _karma_velocity);

                var current = this.karmabody;
                //var v = _karma_velocity.AngularVelocity * 10;
                //current.SetAngularVelocity(v);

                this.karmabody.ApplyAngularImpulse(
                    _karma_velocity.AngularVelocity
                    * ApplyVelocityElapse.ElapsedMilliseconds
                    * 0.5
                    * (1 - (this.karmabody.GetLinearVelocity().Length() / this.speed).Min(0.9) * 0.25)
                );


                var vx = Math.Cos(current.GetAngle()) * _karma_velocity.LinearVelocityY * this.speed
                                   + Math.Cos(current.GetAngle() + Math.PI / 2) * _karma_velocity.LinearVelocityX * this.speed;
                var vy = Math.Sin(current.GetAngle()) * _karma_velocity.LinearVelocityY * this.speed
                        + Math.Sin(current.GetAngle() + Math.PI / 2) * _karma_velocity.LinearVelocityX * this.speed;

                current.SetLinearVelocity(
                    new b2Vec2(
                     vx, vy
                    )
                );

                if (_karma__keyDown.fixup)
                {
                    var fixupmultiplier = 0.95;
                    // like a magnet
                    current.SetPositionAndAngle(
                        new b2Vec2(
                            _karma__keyDown.x + (current.GetPosition().x - _karma__keyDown.x) * fixupmultiplier,
                            _karma__keyDown.y + (current.GetPosition().y - _karma__keyDown.y) * fixupmultiplier
                        ),
                        // meab me in scotty
                            _karma__keyDown.angle + (current.GetAngle() - _karma__keyDown.angle) * fixupmultiplier

                    );
                }
            }



            ApplyVelocityElapse.Restart();
        }




    }
}
