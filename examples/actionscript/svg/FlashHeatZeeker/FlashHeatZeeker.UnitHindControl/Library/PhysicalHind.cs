using Box2D.Collision.Shapes;
using Box2D.Common.Math;
using Box2D.Dynamics;
using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.StarlingSetup.Library;
using FlashHeatZeeker.UnitHind.Library;
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
    public class PhysicalHind : IPhysicalUnit
    {
        public double CameraRotation { get; set; }

        public DriverSeat driverseat { get; set; }


        public double speed = 40;

        public double VerticalVelocity;

        public double AngularVelocity;
        public double LinearVelocityX;
        public double LinearVelocityY;

        public VisualHind visual;

        public b2Body ground_current = null;
        public b2Body air_current = null;


        public b2Body body
        {
            get
            {
                if (visual.Altitude > 0)
                    return air_current;

                return ground_current;
            }
        }

        public b2Body current_slave1
        {
            get
            {
                if (visual.Altitude > 0)
                    return ground_current;

                return air_current;
            }
        }

        public bool AutomaticTakeoff;

        public void SetVelocityFromInput(object[] __keyDown)
        {
            this.AngularVelocity = 0;
            this.LinearVelocityX = 0;
            this.LinearVelocityY = 0;

            if (__keyDown == null)
                return;


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

            if (this.LinearVelocityX == 0)
                if (this.LinearVelocityY == 0)
                    if (this.AngularVelocity == 0)
                        return;


            if (this.visual.Altitude == 0)
                if (AutomaticTakeoff)
                {
                    this.VerticalVelocity = 1.0;

                    // reset

                    this.AngularVelocity = 0;
                    this.LinearVelocityX = 0;
                    this.LinearVelocityY = 0;
                }
        }


        long ApplyVelocityElapsed;
        public void ApplyVelocity()
        {
            var dx = Context.gametime.ElapsedMilliseconds - ApplyVelocityElapsed;
            ApplyVelocityElapsed = Context.gametime.ElapsedMilliseconds;

            var current = this.body;

            {
                var v = this.AngularVelocity * 10;
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


            this.visual.Altitude =
                (this.visual.Altitude + 0.005 * dx * this.VerticalVelocity).Max(0).Min(1);
        }

        public void ShowPositionAndAngle()
        {
            if (body != null)
                body.SetActive(true);

            if (current_slave1 != null)
            {
                current_slave1.SetActive(false);

                // sync up
                if (body != null)
                    current_slave1.SetPositionAndAngle(
                        body.GetPosition(),
                        body.GetAngle()
                    );
            }

            this.visual.Animate(Context.gametime);
            // where are we now
            this.visual.SetPositionAndAngle(
                this.body.GetPosition().x * 16,
                this.body.GetPosition().y * 16,
                this.body.GetAngle()
            );
        }

        StarlingGameSpriteWithPhysics Context;

        public PhysicalHind(StarlingGameSpriteWithHindTextures textures, StarlingGameSpriteWithPhysics Context)
        {
            this.driverseat = new DriverSeat();

            this.Context = Context;

            visual = new VisualHind(textures, Context.Content, Context.airzoom);






            #region ground_b2world ground_current


            {
                var ground_bodyDef = new b2BodyDef();

                ground_bodyDef.type = Box2D.Dynamics.b2Body.b2_dynamicBody;

                // stop moving if legs stop walking!
                ground_bodyDef.linearDamping = 10.0;
                ground_bodyDef.angularDamping = 0;
                //bodyDef.angle = 1.57079633;
                ground_bodyDef.fixedRotation = true;

                var ground_body = Context.ground_b2world.CreateBody(ground_bodyDef);
                ground_current = ground_body;


                var ground_fixDef = new Box2D.Dynamics.b2FixtureDef();
                ground_fixDef.density = 0.1;
                ground_fixDef.friction = 0.01;
                ground_fixDef.restitution = 0;

                var ground_fixdef_shape = new b2PolygonShape();

                ground_fixDef.shape = ground_fixdef_shape;

                // physics unit is looking to right
                ground_fixdef_shape.SetAsBox(2, 0.5);



                var ground_fix = ground_body.CreateFixture(ground_fixDef);
            }



            #endregion


            #region air_b2world air_current




            {
                var air_bodyDef = new b2BodyDef();

                air_bodyDef.type = Box2D.Dynamics.b2Body.b2_dynamicBody;

                // stop moving if legs stop walking!
                air_bodyDef.linearDamping = 10.0;
                air_bodyDef.angularDamping = 0;
                //bodyDef.angle = 1.57079633;
                air_bodyDef.fixedRotation = true;
                air_bodyDef.active = false;

                var air_body = Context.air_b2world.CreateBody(air_bodyDef);
                air_current = air_body;


                var air_fixDef = new Box2D.Dynamics.b2FixtureDef();
                air_fixDef.density = 0.1;
                air_fixDef.friction = 0.01;
                air_fixDef.restitution = 0;

                var air_fixdef_shape = new b2PolygonShape();

                air_fixDef.shape = air_fixdef_shape;

                // physics unit is looking to right
                air_fixdef_shape.SetAsBox(2, 0.5);



                var air_fix = air_body.CreateFixture(air_fixDef);
            }


            #endregion



            ApplyVelocityElapsed = Context.gametime.ElapsedMilliseconds;


            Context.internalunits.Add(this);
        }
    }

}
