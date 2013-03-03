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
    public class PhysicalBarrel : IPhysicalUnit
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

        public Image
            visualshadow,
            visual
            ;

        public KeySample CurrentInput { get; set; }

        public PhysicalBarrel(StarlingGameSpriteWithBunkerTextures textures, StarlingGameSpriteWithPhysics Context)
        {
            this.CurrentInput = new KeySample();

            // hide in a barrel?
            //this.driverseat = new DriverSeat();

            this.textures = textures;
            this.Context = Context;


            //for (int i = 0; i < 7; i++)
            //{
            //    this.KarmaInput0.Enqueue(
            //        new KeySample()
            //    );
            //}

            visualshadow = new Image(
               textures.barrel1_shadow()
           ).AttachTo(Context.Content);


            visual = new Image(
               textures.barrel1()
           ).AttachTo(Context.Content);


            {
                //initialize body
                var bdef = new b2BodyDef();
                bdef.type = Box2D.Dynamics.b2Body.b2_dynamicBody;
                bdef.linearDamping = 8.0;
                bdef.angularDamping = 8;

                bdef.angle = 0;
                this.body = Context.ground_b2world.CreateBody(bdef);

                //initialize shape
                var fixdef = new b2FixtureDef();
                fixdef.density = 1;
                fixdef.friction = 0.01;
                fixdef.restitution = 0.4; //positively bouncy!

                var shape = new b2PolygonShape();
                fixdef.shape = shape;

                shape.SetAsBox(1.6, 1);




                this.body.CreateFixture(fixdef);
            }

            {
                //initialize body
                var bdef = new b2BodyDef();
                bdef.type = Box2D.Dynamics.b2Body.b2_dynamicBody;
                bdef.linearDamping = 8.0;
                bdef.angularDamping = 8;

                bdef.angle = 0;
                this.karmabody = Context.groundkarma_b2world.CreateBody(bdef);

                //initialize shape
                var fixdef = new b2FixtureDef();
                fixdef.density = 1;
                fixdef.friction = 0.01;
                fixdef.restitution = 0.4; //positively bouncy!

                var shape = new b2PolygonShape();
                fixdef.shape = shape;


                shape.SetAsBox(1.6, 1);



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
                cm.rotate(this.body.GetAngle());
                cm.translate(
                    x,
                    y
                );


                visual.transformationMatrix = cm;
            }
            {
                var cm = new Matrix();
                cm.translate(-96, -96);
                cm.rotate(this.body.GetAngle());
                cm.translate(
                    x,
                    y
                );
                cm.translate(8, 8);

                visualshadow.transformationMatrix = cm;
            }
        }


        public void ApplyVelocity()
        {
            var fixupmultiplier = 0.95;

            // like a magnet
            karmabody.SetPositionAndAngle(
                new b2Vec2(
                    body.GetPosition().x + (karmabody.GetPosition().x - body.GetPosition().x) * fixupmultiplier,
                    body.GetPosition().y + (karmabody.GetPosition().y - body.GetPosition().y) * fixupmultiplier
                ),
                // meab me in scotty,
                    body.GetAngle() + (karmabody.GetAngle() - body.GetAngle()) * fixupmultiplier

            );
        }

        public void SetVelocityFromInput(KeySample __keyDown)
        {

        }



        public void FeedKarma()
        {

        }
    }
}
