using Box2D.Collision.Shapes;
using Box2D.Common.Math;
using Box2D.Dynamics;
using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.StarlingSetup.Library;
using FlashHeatZeeker.UnitHind.Library;
using FlashHeatZeeker.UnitJeepControl.Library;
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
    public partial class PhysicalHind : IPhysicalUnit
    {
        public RemoteGame RemoteGameReference { get; set; }
        public string Identity { get; set; }

        public double CameraRotation { get; set; }

        public DriverSeat driverseat { get; set; }


        public double speed = 50;

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

        public void SetPositionAndAngle(double x, double y, double a = 0)
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
        public bool AutomaticTouchdown;

        public double Altitude
        {
            get
            {
                return this.visual.Altitude;
            }
            set
            {
                this.visual.Altitude = value;
            }
        }

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
                    // slow takeoff?
                    this.VerticalVelocity = 0.4;

                    // reset

                    this.AngularVelocity = 0;
                    this.LinearVelocityX = 0;
                    this.LinearVelocityY = 0;
                }
        }



 



        public Action ShowPositionAndAngleForSlaves;

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

            if (ShowPositionAndAngleForSlaves != null)
                ShowPositionAndAngleForSlaves();


            if (this.driverseat.driver == null)
                this.visual.Animate(Context.gametime);
            else
                this.visual.Animate(Context.gametime, 4, 3);

            // where are we now
            this.visual.SetPositionAndAngle(
                this.body.GetPosition().x * 16,
                this.body.GetPosition().y * 16,
                this.body.GetAngle()
            );
        }

        StarlingGameSpriteWithPhysics Context;

        public KeySample CurrentInput { get; set; }
        public PhysicalHind(StarlingGameSpriteWithHindTextures textures, StarlingGameSpriteWithPhysics Context)
        {
            this.CurrentInput = new KeySample();
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
                ground_bodyDef.angularDamping = 4;
                //bodyDef.angle = 1.57079633;
                //ground_bodyDef.fixedRotation = true;

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
                ground_bodyDef.angularDamping = 4;
                //bodyDef.angle = 1.57079633;
                //ground_bodyDef.fixedRotation = true;

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
                air_bodyDef.angularDamping = 4;
                //bodyDef.angle = 1.57079633;
                //air_bodyDef.fixedRotation = true;
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
