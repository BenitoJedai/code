using Box2D.Collision.Shapes;
using Box2D.Common.Math;
using Box2D.Dynamics;
using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.UnitTank.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlashHeatZeeker.UnitTankControl.Library
{
    public class PhysicalTank : IPhysicalUnit
    {
        public DriverSeat driverseat { get; set; }

        public double speed = 30;

        public double AngularVelocity;
        public double LinearVelocityX;
        public double LinearVelocityY;

        public b2Body body { set; get; }
        public VisualTank visual;

        public PhysicalTank(StarlingGameSpriteWithTankTextures textures, StarlingGameSpriteWithPhysics Context)
        {
            this.driverseat = new DriverSeat();
            this.visual = new VisualTank(textures, Context);


            #region b2world



            {
                var bodyDef = new b2BodyDef();

                bodyDef.type = Box2D.Dynamics.b2Body.b2_dynamicBody;

                // stop moving if legs stop walking!
                bodyDef.linearDamping = 1.0;
                bodyDef.angularDamping = 0;
                //bodyDef.angle = 1.57079633;
                bodyDef.fixedRotation = true;

                body = Context.ground_b2world.CreateBody(bodyDef);
                //current = body;


                var fixDef = new Box2D.Dynamics.b2FixtureDef();
                fixDef.density = 0.1;
                fixDef.friction = 0.01;
                fixDef.restitution = 0;

                var fixdef_shape = new b2PolygonShape();

                fixDef.shape = fixdef_shape;

                // physics unit is looking to right
                fixdef_shape.SetAsBox(3, 2);



                var fix = body.CreateFixture(fixDef);
            }


            #endregion



            Context.internalunits.Add(this);


        }

        public void ShowPositionAndAngle()
        {


            #region animate offsetTexCoords
            if (AngularVelocity == 0 && LinearVelocityY == 0)
            {
            }
            else
            {
                visual.Animate(LinearVelocityY);
            }
            #endregion


            visual.SetPositionAndAngle(
                        body.GetPosition().x * 16,
                    body.GetPosition().y * 16,

                    body.GetAngle()
            );

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

                //if (vx == 0 && vy == 0)
                //{

                //}
                //else
                //{

                current.SetLinearVelocity(
                    new b2Vec2(
                     vx, vy


                    )
                );
                //}
            }
        }

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
    }
}
