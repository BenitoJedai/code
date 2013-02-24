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
        public DriverSeat driverseat { get; set; }

        public double AngularVelocity;

        public b2Body body { get; set; }
        public VisualCannon visual;

        public StarlingGameSpriteWithPhysics Context;

        public PhysicalCannon(StarlingGameSpriteWithCannonTextures textures, StarlingGameSpriteWithPhysics Context)
        {
            this.driverseat = new DriverSeat();
            this.Context = Context;

            visual = new VisualCannon(textures, Context);

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

        Stopwatch ApplyVelocityElapsed = new Stopwatch();

        public void ApplyVelocity()
        {
            var current = this.body;

            {
                var a = current.GetAngle();

                //angular damping does not work under low fps
                //if (v != 0)

                a += this.AngularVelocity * (ApplyVelocityElapsed.ElapsedMilliseconds) * 0.01;

                current.SetAngle(a);
            }

            ApplyVelocityElapsed.Restart();
        }

        public void SetVelocityFromInput(object[] __keyDown)
        {

            this.AngularVelocity = 0;



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
