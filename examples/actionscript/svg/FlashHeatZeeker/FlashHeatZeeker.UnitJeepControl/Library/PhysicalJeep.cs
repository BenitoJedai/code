using Box2D.Common.Math;
using Box2D.Dynamics;
using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.UnitJeep.Library;
using FlashHeatZeekerWithStarlingB2.Library;
using ScriptCoreLib.ActionScript.flash.geom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlashHeatZeeker.UnitJeepControl.Library
{
    public class PhysicalJeep : IPhysicalUnit
    {
        public DriverSeat driverseat { get; set; }


        StarlingGameSpriteWithPhysics Context;
        VisualJeep visual0;

        public PhysicalJeep(StarlingGameSpriteWithJeepTextures textures, StarlingGameSpriteWithPhysics Context)
        {
            this.driverseat = new DriverSeat();
            this.Context = Context;

            visual0 = new VisualJeep(textures, Context);


            #region b2world

            // add ghost obstacles for diagnostics

            {
                var bodyDef = new b2BodyDef();

                bodyDef.type = Box2D.Dynamics.b2Body.b2_dynamicBody;

                // stop moving if legs stop walking!
                bodyDef.linearDamping = 10.0;
                bodyDef.angularDamping = 0.3;
                //bodyDef.angle = 1.57079633;
                bodyDef.fixedRotation = true;

                var body = Context.ground_b2world.CreateBody(bodyDef);
                body.SetPosition(new b2Vec2(10, 10));

                var fixDef = new Box2D.Dynamics.b2FixtureDef();
                fixDef.density = 0.1;
                fixDef.friction = 0.01;
                fixDef.restitution = 0;


                fixDef.shape = new Box2D.Collision.Shapes.b2CircleShape(1.0);


                var fix = body.CreateFixture(fixDef);

                //body.SetPosition(
                //    new b2Vec2(0, -100 * 16)
                //);
            }





            #endregion







            var xwheels = new[] { 
                        //top left
                        new Wheel { b2world = Context.ground_b2world, x = -1.1, y = -1.2, width = 0.4, length = 0.8, revolving = true, powered = true },

                        //top right
                        new Wheel{b2world= Context.ground_b2world, x =1.1,  y =-1.2,  width =0.4,  length =0.8,  revolving =true,  powered =true},


                        //back left
                        new Wheel{b2world= Context.ground_b2world, x =-1.1,  y =1.2,  width =0.4,  length =0.8,  revolving =false,  powered =false},

                        //back right
                        new Wheel{b2world= Context.ground_b2world, x =1.1,  y =1.2,  width =0.4,  length =0.8,  revolving =false,  powered =false},
                    };

            xwheels[0].setAngle += a =>
            {
                var cm = new Matrix();
                cm.translate(-2, -2);
                cm.scale(2, 4);
                cm.rotate(a.DegreesToRadians());

                cm.translate(-18, -20);

                visual0.tire0.transformationMatrix = cm;
            };

            xwheels[1].setAngle += a =>
            {

                var cm = new Matrix();
                cm.translate(-2, -2);
                cm.scale(2, 4);
                cm.rotate(a.DegreesToRadians());

                cm.translate(18, -20);

                visual0.tire1.transformationMatrix = cm;
            };
            Func<double, double, double[]> ff = (a, b) => { return new double[] { a, b }; };

            unit4_physics = new Car(
                b2world: Context.ground_b2world,
                width: 2,
                length: 4,
                position: ff(0, 0),
                angle: 180,
                power: 60,

                max_steer_angle: 20,
                //max_steer_angle: 40,

                max_speed: 60,
                wheels: xwheels
        );


            Context.internalunits.Add(this);

        }

        public b2Body body
        {
            get { return unit4_physics.body; }
        }

        public Car unit4_physics;

        public void ShowPositionAndAngle()
        {
            this.visual0.SetPositionAndAngle(
              this.unit4_physics.body.GetPosition().x * 16,
              this.unit4_physics.body.GetPosition().y * 16,
              this.unit4_physics.body.GetAngle()
          );
        }

        long xgt;
        public void ApplyVelocity()
        {
            unit4_physics.update(Context.gametime.ElapsedMilliseconds - xgt);
            xgt = Context.gametime.ElapsedMilliseconds;
        }

        public void SetVelocityFromInput(object[] __keyDown)
        {
            //var rot = 0;
            //var dx = 0.0;
            //var dy = 0.0;

            unit4_physics.accelerate = Car.ACC_NONE;
            unit4_physics.steer_left = Car.STEER_NONE;
            unit4_physics.steer_right = Car.STEER_NONE;

            if (__keyDown[(int)Keys.Up] != null)
            {
                // we have reasone to keep walking

                unit4_physics.accelerate = Car.ACC_ACCELERATE;
                //dy = 1;
            }

            if (__keyDown[(int)Keys.Down] != null)
            {
                // we have reasone to keep walking
                // go slow backwards
                //dy = -0.5;
                unit4_physics.accelerate = Car.ACC_BRAKE;

            }


            if (__keyDown[(int)Keys.Left] != null)
            {
                // we have reasone to keep walking

                unit4_physics.steer_left = Car.STEER_LEFT;

            }

            if (__keyDown[(int)Keys.Right] != null)
            {
                // we have reasone to keep walking

                unit4_physics.steer_right = Car.STEER_RIGHT;

            }
        }
    }
}
