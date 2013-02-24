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
    public class PhyscalPed : IPhysicalUnit
    {
        public b2Body current;

        public double speed = 40;

        public double AngularVelocity;
        public double LinearVelocityX;
        public double LinearVelocityY;

        // slower than actual methods?
        //public Action<object[]> SetVelocityFromInput
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

        //public Action ApplyVelocity;
        public void ApplyVelocity()
        {
            var current = this.current;

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
                    this.current.SetLinearVelocity(
                        new b2Vec2(
                         vx, vy


                        )
                    );
                }
            }
        }

        //public Action ShowPositionAndAngle;
        public void ShowPositionAndAngle()
        {
            if (current != null)
                current.SetActive(true);


            var iswalking = this.LinearVelocityX != 0 || this.LinearVelocityY != 0;
            this.visual.Animate(iswalking);
            // where are we now
            this.visual.SetPositionAndAngle(
                this.current.GetPosition().x * 16,
                this.current.GetPosition().y * 16,
                this.current.GetAngle()
            );
        }

        public VisualPed visual;

        public PhyscalPed(StarlingGameSpriteWithPedTextures textures, StarlingGameSpriteWithPhysics Context)
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

                var body = Context.ground_b2world.CreateBody(bodyDef);
                current = body;


                var fixDef = new Box2D.Dynamics.b2FixtureDef();
                fixDef.density = 0.1;
                fixDef.friction = 0.01;
                fixDef.restitution = 0;


                fixDef.shape = new Box2D.Collision.Shapes.b2CircleShape(2.0);


                var fix = body.CreateFixture(fixDef);
            }


            #endregion



        }
    }

}
