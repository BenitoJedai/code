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

        public b2Body ground_body = null;
        public b2Body groundkarma_body = null;
        public b2Body air_body = null;


        public b2Body body
        {
            get
            {
                if (visual.Altitude > 0)
                    return air_body;

                return ground_body;
            }
        }

        public b2Body current_slave1
        {
            get
            {
                if (visual.Altitude > 0)
                    return ground_body;

                return air_body;
            }
        }

        public void SetPositionAndAngle(double x, double y, double a)
        {
            this.ground_body.SetPositionAndAngle(
                new b2Vec2(x, y), a
            );

            this.groundkarma_body.SetPositionAndAngle(
              new b2Vec2(x, y), a
            );
            this.air_body.SetPositionAndAngle(
             new b2Vec2(x, y), a
           );
        }

        public bool AutomaticTakeoff;

        KeySample CurrentInput = new KeySample();
        public void SetVelocityFromInput(KeySample __keyDown)
        {
            CurrentInput = __keyDown;

            var velocity = new Velocity();

            ExtractVelocityFromInput(__keyDown, velocity);

            this.AngularVelocity = velocity.AngularVelocity;
            this.LinearVelocityX = velocity.LinearVelocityX;
            this.LinearVelocityY = velocity.LinearVelocityY;



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

        public Queue<KeySample> KarmaInput0 = new Queue<KeySample>();

        public void FeedKarma()
        {
            if (this.KarmaInput0.Count > 0)
            {
                this.KarmaInput0.Enqueue(new KeySample
                {
                    value = CurrentInput.value,

                    angle = this.body.GetAngle(),

                    BodyIsActive = this.ground_body.IsActive(),


                    fixup = true,

                    x = this.body.GetPosition().x,
                    y = this.body.GetPosition().y,
                });
                this.KarmaInput0.Dequeue();
            }
        }

        public class Velocity
        {
            public double AngularVelocity;
            public double LinearVelocityX;
            public double LinearVelocityY;
        }

        public void ExtractVelocityFromInput(KeySample __keyDown, Velocity value)
        {


            value.AngularVelocity = 0;
            value.LinearVelocityX = 0;
            value.LinearVelocityY = 0;



            if (__keyDown != null)
            {

                if (__keyDown[Keys.Up])
                {
                    // we have reasone to keep walking

                    value.LinearVelocityY = 1;
                }

                if (__keyDown[Keys.Down])
                {
                    // we have reasone to keep walking
                    // go slow backwards
                    value.LinearVelocityY = -0.5;

                }

                if (!__keyDown[Keys.Alt])
                {
                    if (__keyDown[Keys.Left])
                    {
                        // we have reasone to keep walking

                        value.AngularVelocity = -1;

                    }

                    if (__keyDown[Keys.Right])
                    {
                        // we have reasone to keep walking

                        value.AngularVelocity = 1;

                    }
                }
                else
                {
                    if (__keyDown[Keys.Left])
                    {
                        // we have reasone to keep walking

                        value.LinearVelocityX = -1;

                    }

                    if (__keyDown[Keys.Right])
                    {
                        // we have reasone to keep walking

                        value.LinearVelocityX = 1;

                    }
                }
            }


        }

   


        long ApplyVelocityElapsed;
        public void ApplyVelocity()
        {
            var dx = Context.gametime.ElapsedMilliseconds - ApplyVelocityElapsed;
            ApplyVelocityElapsed = Context.gametime.ElapsedMilliseconds;


            {
                var current = this.body;
                var v = this.AngularVelocity * 10;
                current.SetAngularVelocity(v);

                var vx = Math.Cos(current.GetAngle()) * this.LinearVelocityY * this.speed
                        + Math.Cos(current.GetAngle() + Math.PI / 2) * this.LinearVelocityX * this.speed;
                var vy = Math.Sin(current.GetAngle()) * this.LinearVelocityY * this.speed
                        + Math.Sin(current.GetAngle() + Math.PI / 2) * this.LinearVelocityX * this.speed;


                this.body.SetLinearVelocity(
                    new b2Vec2(
                     vx, vy


                    )
                );
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
                var v = _karma_velocity.AngularVelocity * 10;
                current.SetAngularVelocity(v);

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

            for (int i = 0; i < 7; i++)
            {
                this.KarmaInput0.Enqueue(
                    new KeySample()
                );
            }




            #region ground_b2world ground_current


            {
                var ground_bodyDef = new b2BodyDef();

                ground_bodyDef.type = Box2D.Dynamics.b2Body.b2_dynamicBody;

                // stop moving if legs stop walking!
                ground_bodyDef.linearDamping = 10.0;
                ground_bodyDef.angularDamping = 0;
                //bodyDef.angle = 1.57079633;
                ground_bodyDef.fixedRotation = true;

                ground_body = Context.ground_b2world.CreateBody(ground_bodyDef);


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


            #region groundkarma_body


            {
                var ground_bodyDef = new b2BodyDef();

                ground_bodyDef.type = Box2D.Dynamics.b2Body.b2_dynamicBody;

                // stop moving if legs stop walking!
                ground_bodyDef.linearDamping = 10.0;
                ground_bodyDef.angularDamping = 0;
                //bodyDef.angle = 1.57079633;
                ground_bodyDef.fixedRotation = true;

                groundkarma_body = Context.groundkarma_b2world.CreateBody(ground_bodyDef);


                var ground_fixDef = new Box2D.Dynamics.b2FixtureDef();
                ground_fixDef.density = 0.1;
                ground_fixDef.friction = 0.01;
                ground_fixDef.restitution = 0;

                var ground_fixdef_shape = new b2PolygonShape();

                ground_fixDef.shape = ground_fixdef_shape;

                // physics unit is looking to right
                ground_fixdef_shape.SetAsBox(2, 0.5);



                var ground_fix = groundkarma_body.CreateFixture(ground_fixDef);
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

                air_body = Context.air_b2world.CreateBody(air_bodyDef);


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
