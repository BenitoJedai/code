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
using ScriptCoreLib.Shared.BCLImplementation.GLSL;
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
        long ApplyVelocityElapsed;
        double AngularVelocitySign;


        public void ApplyVelocity()
        {
            if (this.visual.Altitude != 0)
                if (AutomaticTouchdown)
                    if (this.driverseat.driver == null)
                    {
                        this.VerticalVelocity = -0.4;

                        // reset

                        this.AngularVelocity = 0;
                        this.LinearVelocityX = 0;
                        this.LinearVelocityY = 0;
                    }

            var dx = Context.gametime.ElapsedMilliseconds - ApplyVelocityElapsed;
            ApplyVelocityElapsed = Context.gametime.ElapsedMilliseconds;


            {
                var current = this.body;
                //var v = this.AngularVelocity * 10;
                //current.SetAngularVelocity(v);



                current.ApplyAngularImpulse(
                    this.AngularVelocity
                     * this.CurrentInput.forcex
                    * 0.6 
                    * (1 - (this.body.GetLinearVelocity().Length() / this.speed).Min(0.9) * 0.5)
                );


                var vx = Math.Cos(current.GetAngle()) * this.LinearVelocityY * this.speed * this.CurrentInput.forcey
                        + Math.Cos(current.GetAngle() + Math.PI / 2) * this.LinearVelocityX * this.speed;
                var vy = Math.Sin(current.GetAngle()) * this.LinearVelocityY * this.speed * this.CurrentInput.forcey
                        + Math.Sin(current.GetAngle() + Math.PI / 2) * this.LinearVelocityX * this.speed;




                #region RemoteGameReference
                if (RemoteGameReference != null)
                    if (vx == 0)
                        if (vy == 0)
                        //if (v == 0)
                        {
                            // not moving anymore in network mode
                            // far enough to be out of sync?

                            //if (karmabody.GetAngularVelocity() == 0)
                            if (body.GetLinearVelocity().Length() == 0)
                                if (this.KarmaInput0.All(k => k.value == 0))
                                {




                                    var gap = new __vec2(
                                        (float)this.groundkarma_body.GetPosition().x - (float)this.body.GetPosition().x,
                                        (float)this.groundkarma_body.GetPosition().y - (float)this.body.GetPosition().y
                                    );

                                    // tolerate lesser distance?

                                    var da0 = this.groundkarma_body.GetAngle() - this.body.GetAngle();
                                    var da360 = (this.groundkarma_body.GetAngle() + 360.DegreesToRadians()) - this.body.GetAngle();

                                    if (da0 < da360)
                                    {
                                        this.body.SetAngle(
                                               this.body.GetAngle() + da0 * 0.2
                                        );
                                    }
                                    else
                                    {
                                        this.body.SetAngle(
                                               this.body.GetAngle() + da360 * 0.2
                                        );
                                    }


                                    this.body.SetPosition(
                                        new b2Vec2(
                                            this.body.GetPosition().x + gap.x * 0.1,
                                            this.body.GetPosition().y + gap.y * 0.1
                                        )
                                    );
                                }

                        }
                #endregion







                this.body.SetLinearVelocity(
                    new b2Vec2(
                     vx, vy


                    )
                );

                // attempt to course correct if near 90 degree angles!

                //if (this.AngularVelocity == 0)
                //{
                //    var fixupmultiplier = 0.90;

                //    var fixai = (current.GetAngle().RadiansToDegrees() / 4.0);

                //    if (AngularVelocitySign > 0)
                //        fixai = Math.Ceiling(fixai);
                //    else
                //        fixai = Math.Floor(fixai);

                //    var fixaf = (fixai * 16).DegreesToRadians();


                //    // like a magnet
                //    current.SetAngle(
                //        // meab me in scotty
                //            fixaf + (current.GetAngle() - fixaf) * fixupmultiplier

                //    );
                //}
                //else
                //{
                //    AngularVelocitySign = Math.Sign(this.AngularVelocity);
                //}
            }


            this.visual.Altitude =
                (this.visual.Altitude + 0.005 * dx * this.VerticalVelocity).Max(0).Min(1);


            // what about our karma body?
            if (this.KarmaInput0.Count > 0)
            {
                var _karma__keyDown = this.KarmaInput0.Peek();

                var _karma_velocity = new Velocity();


                ExtractVelocityFromInput(_karma__keyDown, _karma_velocity);

                var current = this.groundkarma_body;
                //var v = _karma_velocity.AngularVelocity * 10;
                //current.SetAngularVelocity(v);


                current.ApplyAngularImpulse(
                    this.AngularVelocity * 0.6 * (1 - (current.GetLinearVelocity().Length() / this.speed).Min(0.9) * 0.5)
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
        }

    }

}
