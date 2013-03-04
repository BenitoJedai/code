using Box2D.Collision.Shapes;
using Box2D.Common.Math;
using Box2D.Dynamics;
using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CorePhysics.Library;
using ScriptCoreLib.ActionScript.flash.geom;
using starling.display;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlashHeatZeeker.UnitBunkerControl.Library
{
    public class PhysicalWatertower : IPhysicalUnit
    {
        public double Altitude { get; set; }
        public RemoteGame RemoteGameReference { get; set; }

        public string Identity { get; set; }

        public double CameraRotation { get; set; }

        public DriverSeat driverseat { get; set; }


        public b2Body body { get; set; }
        public b2Body karmabody { get; set; }

        public void SetPositionAndAngle(double x, double y, double a = 0)
        {
            this.body.SetPositionAndAngle(
                new b2Vec2(x, y), 0
            );

            this.karmabody.SetPositionAndAngle(
              new b2Vec2(x, y), 0
            );

        }


        StarlingGameSpriteWithBunkerTextures textures;
        StarlingGameSpriteWithPhysics Context;

        Image
            visualshadow,
            visual
            ;

        public KeySample CurrentInput { get; set; }

        public PhysicalWatertower(StarlingGameSpriteWithBunkerTextures textures, StarlingGameSpriteWithPhysics Context)
        {
            this.CurrentInput = new KeySample();
            this.driverseat = new DriverSeat();

            this.textures = textures;
            this.Context = Context;


            for (int i = 0; i < 7; i++)
            {
                this.KarmaInput0.Enqueue(
                    new KeySample()
                );
            }

            visualshadow = new Image(
               textures.watertower0_shadow()
           ).AttachTo(Context.Content_layer2_shadows);


            visual = new Image(
               textures.watertower0()
           ).AttachTo(Context.Content_layer3_buildings);


            {
                //initialize body
                var bdef = new b2BodyDef();
                bdef.angle = 0;
                bdef.fixedRotation = true;
                this.body = Context.ground_b2world.CreateBody(bdef);

                //initialize shape
                var fixdef = new b2FixtureDef();

                var shape = new b2CircleShape(1.2);
                fixdef.shape = shape;


                fixdef.restitution = 0.4; //positively bouncy!



                var fix = this.body.CreateFixture(fixdef);

                var fix_data = new Action<double>(
                   force =>
                   {
                       if (force < 1)
                           return;

                       Context.oncollision(this, force);
                   }
              );

                fix.SetUserData(fix_data);
            }

            {
                //initialize body
                var bdef = new b2BodyDef();
                bdef.angle = 0;
                bdef.fixedRotation = true;
                this.karmabody = Context.groundkarma_b2world.CreateBody(bdef);

                //initialize shape
                var fixdef = new b2FixtureDef();

                var shape = new b2CircleShape(1.2);
                fixdef.shape = shape;


                fixdef.restitution = 0.4; //positively bouncy!



                this.karmabody.CreateFixture(fixdef);
            }
            Context.internalunits.Add(this);
        }


        public void ShowPositionAndAngle()
        {
            var x = this.body.GetPosition().x * 16;
            var y = this.body.GetPosition().y * 16;

            {
                var cm = new Matrix();
                cm.translate(-96, -96);
                cm.translate(
                    x,
                    y
                );


                visual.transformationMatrix = cm;
            }
            {
                var cm = new Matrix();
                cm.translate(-96, -96);
                cm.translate(
                    x,
                    y
                );
                //cm.translate(8, 8);

                visualshadow.transformationMatrix = cm;
            }
        }


        Stopwatch ApplyVelocityElapsed = new Stopwatch();
        public void ApplyVelocity()
        {
            {
                var a = this.CameraRotation;

                //angular damping does not work under low fps
                //if (v != 0)

                a -= this.velocity.AngularVelocity * (ApplyVelocityElapsed.ElapsedMilliseconds) * 0.01;

                this.CameraRotation = a;
            }
            ApplyVelocityElapsed.Restart();
        }

        Velocity velocity = new Velocity();
        public void SetVelocityFromInput(KeySample __keyDown)
        {
            this.CurrentInput = __keyDown;

            ExtractVelocityFromInput(__keyDown, velocity);
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
    }
}
