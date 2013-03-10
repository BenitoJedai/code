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
    public partial class PhysicalTank : IPhysicalUnit
    {
        public double Altitude { get; set; }

        public RemoteGame RemoteGameReference { get; set; }

        public string Identity { get; set; }

        public double CameraRotation { get; set; }

        public DriverSeat driverseat { get; set; }

        public double speed = 20;



        public b2Body damagebody { set; get; }
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
            this.damagebody.SetPositionAndAngle(
            new b2Vec2(x, y), a
          );
        }

        public KeySample CurrentInput { get; set; }

        public PhysicalTank(StarlingGameSpriteWithTankTextures textures, StarlingGameSpriteWithPhysics Context)
        {
            this.CurrentInput = new KeySample();

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
                bodyDef.angularDamping = 8;
                //bodyDef.angle = 1.57079633;
                //bodyDef.fixedRotation = true;

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

                var fix_data = new Action<double>(
                    jeep_forceA =>
                    {
                        if (jeep_forceA < 1)
                            return;

                        Context.oncollision(this, jeep_forceA);
                    }
                );
                fix.SetUserData(fix_data);
            }


            {
                var bodyDef = new b2BodyDef();

                bodyDef.type = Box2D.Dynamics.b2Body.b2_dynamicBody;

                // stop moving if legs stop walking!
                bodyDef.linearDamping = 1.0;
                bodyDef.angularDamping = 8;
                //bodyDef.angle = 1.57079633;
                //bodyDef.fixedRotation = true;

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

            #region b2world



            {
                var bodyDef = new b2BodyDef();

                bodyDef.type = Box2D.Dynamics.b2Body.b2_dynamicBody;

                // stop moving if legs stop walking!
                bodyDef.linearDamping = 1.0;
                bodyDef.angularDamping = 8;
                //bodyDef.angle = 1.57079633;
                //bodyDef.fixedRotation = true;

                damagebody = Context.damage_b2world.CreateBody(bodyDef);
                //current = body;


                var fixDef = new Box2D.Dynamics.b2FixtureDef();
                fixDef.density = 0.1;
                fixDef.friction = 0.01;
                fixDef.restitution = 0;

                var fixdef_shape = new b2PolygonShape();

                fixDef.shape = fixdef_shape;

                // physics unit is looking to right
                fixdef_shape.SetAsBox(3, 2);



                var fix = damagebody.CreateFixture(fixDef);

                var fix_data = new Action<double>(
                    jeep_forceA =>
                    {
                        if (jeep_forceA < 1)
                            return;

                        Context.oncollision(this, jeep_forceA);
                    }
                );
                fix.SetUserData(fix_data);
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



        public class Velocity
        {
            public double AngularVelocity;
            public double LinearVelocityX;
            public double LinearVelocityY;
        }

        public double AngularVelocity;
        public double LinearVelocityX;
        public double LinearVelocityY;

        public void SetVelocityFromInput(KeySample __keyDown)
        {
            CurrentInput = __keyDown;

            var velocity = new Velocity();

            ExtractVelocityFromInput(__keyDown, velocity);

            this.AngularVelocity = velocity.AngularVelocity;
            this.LinearVelocityX = velocity.LinearVelocityX;
            this.LinearVelocityY = velocity.LinearVelocityY;
        }





    }
}
