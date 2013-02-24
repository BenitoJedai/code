using Box2D.Common.Math;
using Box2D.Dynamics;
using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.StarlingSetup.Library;
using FlashHeatZeeker.UnitPed.Library;
using ScriptCoreLib.ActionScript.flash.geom;
using starling.display;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlashHeatZeeker.UnitPedControl.Library
{
    public class PhysicalPed : IPhysicalUnit
    {
        public DriverSeat driverseat { get; set; }

        public b2Body body { get; set; }

        public double speed = 20;

        public double AngularVelocity;
        public double LinearVelocityX;
        public double LinearVelocityY;

        public void SetVelocityFromInput(object[] __keyDown)
        {
            this.AngularVelocity = 0;
            this.LinearVelocityX = 0;
            this.LinearVelocityY = 0;

            if (__keyDown[(int)Keys.Up] != null)
            {
                // we have reasone to keep walking

                this.LinearVelocityY = 1;
            }

            if (__keyDown[(int)Keys.Down] != null)
            {
                // we have reasone to keep walking
                // go slow backwards
                this.LinearVelocityY = -0.5;

            }

            if (__keyDown[(int)Keys.Alt] == null)
            {
                if (__keyDown[(int)Keys.Left] != null)
                {
                    // we have reasone to keep walking

                    this.AngularVelocity = -1;

                }

                if (__keyDown[(int)Keys.Right] != null)
                {
                    // we have reasone to keep walking

                    this.AngularVelocity = 1;

                }
            }
            else
            {
                if (__keyDown[(int)Keys.Left] != null)
                {
                    // we have reasone to keep walking

                    this.LinearVelocityX = -1;

                }

                if (__keyDown[(int)Keys.Right] != null)
                {
                    // we have reasone to keep walking

                    this.LinearVelocityX = 1;

                }
            }
        }

        public void ApplyVelocity()
        {
            var current = this.body;

            {
                var v = this.AngularVelocity * 10;
                //angular damping does not work under low fps
                //if (v != 0)
                current.SetAngularVelocity(v);
            }

            {
                var vx = Math.Cos(current.GetAngle()) * this.LinearVelocityY * this.speed
                        + Math.Cos(current.GetAngle() + Math.PI / 2) * this.LinearVelocityX * this.speed;
                var vy = Math.Sin(current.GetAngle()) * this.LinearVelocityY * this.speed
                        + Math.Sin(current.GetAngle() + Math.PI / 2) * this.LinearVelocityX * this.speed;

                if (vx == 0 && vy == 0)
                {

                }
                else
                {
                    this.body.SetLinearVelocity(
                        new b2Vec2(
                         vx, vy


                        )
                    );
                }
            }
        }

        public void ShowPositionAndAngle()
        {
            if (body != null)
                body.SetActive(true);


            var iswalking = this.LinearVelocityX != 0 || this.LinearVelocityY != 0;
            this.visual.Animate(this.LinearVelocityX, this.LinearVelocityY);
            // where are we now
            this.visual.SetPositionAndAngle(
                this.body.GetPosition().x * 16,
                this.body.GetPosition().y * 16,
                this.body.GetAngle()
            );
        }

        public VisualPed visual;

        public PhysicalPed(StarlingGameSpriteWithPedTextures textures, StarlingGameSpriteWithPhysics Context)
        {
            visual = new VisualPed(textures, Context);


            #region b2world




            {
                var bodyDef = new b2BodyDef();

                bodyDef.type = Box2D.Dynamics.b2Body.b2_dynamicBody;

                // stop moving if legs stop walking!
                bodyDef.linearDamping = 10.0;
                bodyDef.angularDamping = 0;
                //bodyDef.angle = 1.57079633;
                bodyDef.fixedRotation = true;

                body = Context.ground_b2world.CreateBody(bodyDef);


                var fixDef = new Box2D.Dynamics.b2FixtureDef();
                fixDef.density = 0.1;
                fixDef.friction = 0.01;
                fixDef.restitution = 0;


                fixDef.shape = new Box2D.Collision.Shapes.b2CircleShape(1.0);


                var fix = body.CreateFixture(fixDef);
            }


            #endregion


            Context.internalunits.Add(this);

        }
    }

}
