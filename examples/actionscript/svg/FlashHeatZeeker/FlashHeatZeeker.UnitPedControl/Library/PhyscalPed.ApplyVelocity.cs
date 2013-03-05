using Box2D.Common.Math;
using Box2D.Dynamics;
using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.StarlingSetup.Library;
using FlashHeatZeeker.UnitJeepControl.Library;
using FlashHeatZeeker.UnitPed.Library;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.Shared.BCLImplementation.GLSL;
using starling.display;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlashHeatZeeker.UnitPedControl.Library
{
    public partial class PhysicalPed : IPhysicalUnit
    {
        Stopwatch ApplyVelocityElapse = new Stopwatch();
        public void ApplyVelocity()
        {
            // this is now

            {
                var current = this.body;
                //var v = velocity.AngularVelocity * 10;         current.SetAngularVelocity(v);


                current.ApplyAngularImpulse(
                    velocity.AngularVelocity
                      * this.CurrentInput.forcex
                    * ApplyVelocityElapse.ElapsedMilliseconds
                    * 0.01
                    * (1 - (this.body.GetLinearVelocity().Length() / this.speed).Min(0.9) * 0.5)
                );



                var vx = Math.Cos(current.GetAngle()) * velocity.LinearVelocityY * this.speed * this.CurrentInput.forcey
                    + Math.Cos(current.GetAngle() + Math.PI / 2) * velocity.LinearVelocityX * this.speed;
                var vy = Math.Sin(current.GetAngle()) * velocity.LinearVelocityY * this.speed * this.CurrentInput.forcey
                        + Math.Sin(current.GetAngle() + Math.PI / 2) * velocity.LinearVelocityX * this.speed;


                this.visual.currentvisual.alpha = 1.0;

                #region RemoteGameReference
                if (RemoteGameReference != null)
                    if (vx == 0)
                        if (vy == 0)
                            //if (v == 0)
                            {
                                // not moving anymore in network mode
                                // far enough to be out of sync?

                                if (karmabody.GetLinearVelocity().Length() == 0)
                                    if (this.KarmaInput0.All(k => k.value == 0))
                                    {

                                        this.body.SetAngle(
                                             karmabody.GetAngle()
                                         );


                                        var gap = new __vec2(
                                            (float)this.karmabody.GetPosition().x - (float)this.body.GetPosition().x,
                                            (float)this.karmabody.GetPosition().y - (float)this.body.GetPosition().y
                                        );

                                        // tolerate lesser distance?
                                        if (gap.GetLength() > 0.5)
                                        {
                                            //this.body.SetPositionAndAngle(
                                            //    new b2Vec2(
                                            //        this.karmabody.GetPosition().x,
                                            //        this.karmabody.GetPosition().y
                                            //    ),
                                            //    this.karmabody.GetAngle()
                                            //);

                                            // show we are in the wrong place!
                                            this.visual.currentvisual.alpha = 0.3;


                                            // look at where we should be instead
                                            this.body.SetAngle(
                                                gap.GetRotation()
                                            );

                                            // and walk there!
                                            vx = Math.Cos(current.GetAngle()) * 0.5 * this.speed
                                               + Math.Cos(current.GetAngle() + Math.PI / 2) * 0 * this.speed;
                                            vy = Math.Sin(current.GetAngle()) * 0.5 * this.speed
                                                   + Math.Sin(current.GetAngle() + Math.PI / 2) * 0 * this.speed;
                                        }
                                    }

                            }
                #endregion

       
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

                current.ApplyAngularImpulse(
                    _karma_velocity.AngularVelocity
                    * ApplyVelocityElapse.ElapsedMilliseconds
                   * 0.01
                    * (1 - (current.GetLinearVelocity().Length() / this.speed).Min(0.9) * 0.5)
                );


                var vx = Math.Cos(current.GetAngle()) * _karma_velocity.LinearVelocityY * this.speed
                                   + Math.Cos(current.GetAngle() + Math.PI / 2) * _karma_velocity.LinearVelocityX * this.speed;
                var vy = Math.Sin(current.GetAngle()) * _karma_velocity.LinearVelocityY * this.speed
                        + Math.Sin(current.GetAngle() + Math.PI / 2) * _karma_velocity.LinearVelocityX * this.speed;


                current.SetActive(
                     _karma__keyDown.BodyIsActive
                 );

                current.SetLinearVelocity(
                    new b2Vec2(
                     vx, vy
                    )
                );

                if (_karma__keyDown.fixup)
                {
                    var fixupmultiplier = 0.90;

                    current.SetAngle(
                        // meab me in scotty,
                         _karma__keyDown.angle + (current.GetAngle() - _karma__keyDown.angle) * fixupmultiplier

                    );



                    var gap = new __vec2(
                        (float)this.karmabody.GetPosition().x - (float)_karma__keyDown.x,
                        (float)this.karmabody.GetPosition().y - (float)_karma__keyDown.y
                    );

                    if (gap.GetLength() > 0.1)
                    {
                        current.SetLinearVelocity(
                             new b2Vec2(
                              vx - gap.x * 2.0,
                              vy - gap.y * 2.0
                             )
                         );
                    }
                }
            }
            ApplyVelocityElapse.Restart();
        }

    }

}
