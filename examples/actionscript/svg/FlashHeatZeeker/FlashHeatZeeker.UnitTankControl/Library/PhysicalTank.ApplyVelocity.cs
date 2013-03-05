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

        public double AngularVelocityMultiplier = 1;

        bool ApplyVelocityMoveToLocation;
        public void ApplyVelocity()
        {

            {
                var current = this.body;
                //var v = this.AngularVelocity * 10;
                //current.SetAngularVelocity(v);

                current.ApplyAngularImpulse(
                   this.AngularVelocity * AngularVelocityMultiplier
                   * ApplyVelocityElapse.ElapsedMilliseconds
                   * 1.0
                   * (1 - (this.body.GetLinearVelocity().Length() / this.speed).Min(0.9) * 0.5)
               );


                var vx = Math.Cos(current.GetAngle()) * this.LinearVelocityY * this.speed
                        + Math.Cos(current.GetAngle() + Math.PI / 2) * this.LinearVelocityX * this.speed;
                var vy = Math.Sin(current.GetAngle()) * this.LinearVelocityY * this.speed
                        + Math.Sin(current.GetAngle() + Math.PI / 2) * this.LinearVelocityX * this.speed;





                this.visual.currentvisual.alpha = 1.0;

                #region RemoteGameReference
                if (RemoteGameReference != null)
                    if (vx == 0)
                        if (vy == 0)
                        //if (v == 0)
                        {
                            // not moving anymore in network mode
                            // far enough to be out of sync?

                            //if (karmabody.GetAngularVelocity() == 0)
                                if (karmabody.GetLinearVelocity().Length() == 0)
                                    if (this.KarmaInput0.All(k => k.value == 0))
                                    {




                                        var gap = new __vec2(
                                            (float)this.karmabody.GetPosition().x - (float)this.body.GetPosition().x,
                                            (float)this.karmabody.GetPosition().y - (float)this.body.GetPosition().y
                                        );

                                        // tolerate lesser distance?

                                        var CloseEnough = gap.GetLength() < 1.2;
                                        var TooFar = gap.GetLength() > 5;

                                        if (CloseEnough)
                                        {
                                            ApplyVelocityMoveToLocation = false;
                                        }

                                        if (TooFar || ApplyVelocityMoveToLocation)
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

                                            ApplyVelocityMoveToLocation = true;
                                        }
                                        else
                                        {
                                            var da0 = this.karmabody.GetAngle() - this.body.GetAngle();
                                            var da360 = (this.karmabody.GetAngle() + 360.DegreesToRadians()) - this.body.GetAngle();

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

                                            if (CloseEnough)
                                            { 
                                            
                                            }
                                            else
                                            {
                                                this.body.SetPosition(
                                                    new b2Vec2(
                                                        this.body.GetPosition().x + gap.x * 0.3,
                                                        this.body.GetPosition().y + gap.y * 0.3
                                                    )
                                                );
                                            }
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



            ApplyVelocityElapse.Restart();
        }




    }
}
