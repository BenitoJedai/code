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
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlashHeatZeeker.UnitTankControl.Library
{
    public class PhysicalTank : IPhysicalUnit
    {
        public string Identity { get; set; }

        public double CameraRotation { get; set; }

        public DriverSeat driverseat { get; set; }

        public double speed = 20;



        public b2Body body { set; get; }
        public b2Body karmabody { get; set; }

        public VisualTank visual;

        public StarlingGameSpriteWithTankTextures textures;
        public StarlingGameSpriteWithPhysics Context;

        public void SetPositionAndAngle(double x, double y, double a = 0)
        {
            this.body.SetPositionAndAngle(
                new b2Vec2(x, y), a
            );

            this.karmabody.SetPositionAndAngle(
              new b2Vec2(x, y), a
            );

        }

        public PhysicalTank(StarlingGameSpriteWithTankTextures textures, StarlingGameSpriteWithPhysics Context)
        {
            this.textures = textures;
            this.Context = Context;
            this.driverseat = new DriverSeat();
            this.visual = new VisualTank(textures, Context);

            for (int i = 0; i < 7; i++)
            {
                this.KarmaInput0.Enqueue(
                    new KeySample()
                );
            }


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


            {
                var bodyDef = new b2BodyDef();

                bodyDef.type = Box2D.Dynamics.b2Body.b2_dynamicBody;

                // stop moving if legs stop walking!
                bodyDef.linearDamping = 1.0;
                bodyDef.angularDamping = 0;
                //bodyDef.angle = 1.57079633;
                bodyDef.fixedRotation = true;

                karmabody = Context.groundkarma_b2world.CreateBody(bodyDef);
                //current = body;


                var fixDef = new Box2D.Dynamics.b2FixtureDef();
                fixDef.density = 0.1;
                fixDef.friction = 0.01;
                fixDef.restitution = 0;

                var fixdef_shape = new b2PolygonShape();

                fixDef.shape = fixdef_shape;

                // physics unit is looking to right
                fixdef_shape.SetAsBox(3, 2);



                var fix = karmabody.CreateFixture(fixDef);
            }


            #endregion



            Context.internalunits.Add(this);


        }

        bool prev = false;
        double prevx = 0.0;
        double prevy = 0.0;

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


            #region Content_layer0_tracks
            if (!prev)
            {
                prev = true;
                prevx = this.body.GetPosition().x;
                prevy = this.body.GetPosition().y;
            }
            else
            {
                var p = this.body.GetPosition();


                var distance = X.GetLength(
                    new __vec2(
                    (float)(p.x - prevx),
                    (float)(p.y - prevy)
                    )
                );

                if (distance > 2)
                {
                    var tracks0 = new Image(textures.tracks0()).AttachTo(Context.Content_layer0_tracks);

                    var cm = new Matrix();

                    cm.translate(-64, -64);
                    cm.rotate(this.body.GetAngle() - Math.PI / 2);
                    cm.translate(
                        p.x * 16.0,
                        p.y * 16.0
                    );

                    tracks0.transformationMatrix = cm;

                    prevx = p.x;
                    prevy = p.y;
                }
            }
            #endregion

        }

        public void ApplyVelocity()
        {

            {
                var current = this.body;
                var v = this.AngularVelocity * 10;
                current.SetAngularVelocity(v);

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
                var v = _karma_velocity.AngularVelocity * 10;
                current.SetAngularVelocity(v);

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
        }


        public class Velocity
        {
            public double AngularVelocity;
            public double LinearVelocityX;
            public double LinearVelocityY;
        }

        public double AngularVelocity;
        public double LinearVelocityX;
        public double LinearVelocityY;

        KeySample CurrentInput = new KeySample();
        public void SetVelocityFromInput(KeySample __keyDown)
        {
            CurrentInput = __keyDown;

            var velocity = new Velocity();

            ExtractVelocityFromInput(__keyDown, velocity);

            this.AngularVelocity = velocity.AngularVelocity;
            this.LinearVelocityX = velocity.LinearVelocityX;
            this.LinearVelocityY = velocity.LinearVelocityY;
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

                    fixup = true,
                    x = this.body.GetPosition().x,
                    y = this.body.GetPosition().y,
                });
                this.KarmaInput0.Dequeue();
            }
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


        }


    }
}
