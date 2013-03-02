using Box2D.Collision.Shapes;
using Box2D.Common.Math;
using Box2D.Dynamics;
using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.UnitCannon.Library;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlashHeatZeeker.UnitCannonControl.Library
{
    public class PhysicalCannon : IPhysicalUnit
    {
        public RemoteGame RemoteGameReference { get; set; }


        public string Identity { get; set; }

        public double CameraRotation { get; set; }

        public DriverSeat driverseat { get; set; }


        public b2Body body { get; set; }
        public b2Body karmabody { get; set; }

        public void SetPositionAndAngle(double x, double y, double a)
        {
            this.body.SetPositionAndAngle(
                new b2Vec2(x, y), a
            );

            this.karmabody.SetPositionAndAngle(
              new b2Vec2(x, y), a
            );

        }

        public VisualCannon visual;

        public StarlingGameSpriteWithPhysics Context;

        public KeySample CurrentInput { get; set; }

        public PhysicalCannon(StarlingGameSpriteWithCannonTextures textures, StarlingGameSpriteWithPhysics Context)
        {
            this.CurrentInput = new KeySample();
            this.driverseat = new DriverSeat();
            this.Context = Context;

            visual = new VisualCannon(textures, Context);

            for (int i = 0; i < 7; i++)
            {
                this.KarmaInput0.Enqueue(
                    new KeySample()
                );
            }

            {
                //initialize body
                var bdef = new b2BodyDef();
                bdef.angle = 0;
                bdef.fixedRotation = true;
                this.body = Context.ground_b2world.CreateBody(bdef);

                //initialize shape
                var fixdef = new b2FixtureDef();

                var shape = new b2PolygonShape();
                fixdef.shape = shape;

                shape.SetAsBox(2, 2);

                fixdef.restitution = 0.4; //positively bouncy!



                this.body.CreateFixture(fixdef);
            }

            {
                //initialize body
                var bdef = new b2BodyDef();
                bdef.angle = 0;
                bdef.fixedRotation = true;
                this.karmabody = Context.groundkarma_b2world.CreateBody(bdef);

                //initialize shape
                var fixdef = new b2FixtureDef();

                var shape = new b2PolygonShape();
                fixdef.shape = shape;

                shape.SetAsBox(2, 2);

                fixdef.restitution = 0.4; //positively bouncy!



                this.karmabody.CreateFixture(fixdef);
            }

            Context.internalunits.Add(this);
        }

        public void ShowPositionAndAngle()
        {
            this.visual.SetPositionAndAngle(
              this.body.GetPosition().x * 16,
              this.body.GetPosition().y * 16,
              this.body.GetAngle()
          );
        }

        public Queue<KeySample> KarmaInput0 = new Queue<KeySample>();

        public void FeedKarma()
        {
            if (this.KarmaInput0.Count > 0)
            {
                this.KarmaInput0.Enqueue(new KeySample
                {
                    value = CurrentInput.value,

                    fixup = true,
                    angle = this.body.GetAngle(),

                });
                this.KarmaInput0.Dequeue();
            }
        }

        Stopwatch ApplyVelocityElapsed = new Stopwatch();

        public void ApplyVelocity()
        {

            {
                var current = this.body;
                var a = current.GetAngle();

                //angular damping does not work under low fps
                //if (v != 0)

                a += this.velocity.AngularVelocity * (ApplyVelocityElapsed.ElapsedMilliseconds) * 0.01;

                current.SetAngle(a);
            }

            if (this.KarmaInput0.Count > 0)
            {
                var _karma__keyDown = this.KarmaInput0.Peek();

                var _karma_velocity = new Velocity();


                ExtractVelocityFromInput(_karma__keyDown, _karma_velocity);


                //var a = karmabody.GetAngle();
                //a += _karma_velocity.AngularVelocity * (ApplyVelocityElapsed.ElapsedMilliseconds) * 0.01;
                karmabody.SetAngle(

                    // meab me in scotty,
                     _karma__keyDown.angle
                 );
            }
            ApplyVelocityElapsed.Restart();
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

            if (__keyDown != null)
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
        }


        Velocity velocity = new Velocity();

        public void SetVelocityFromInput(KeySample __keyDown)
        {
            this.CurrentInput = __keyDown;

            ExtractVelocityFromInput(__keyDown, velocity);
        }


    }
}
