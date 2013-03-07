using Box2D.Dynamics;
using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CorePhysics.Library;
using starling.display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlashHeatZeeker.UnitRocket.Library
{
    public partial class PhysicalRocket : IPhysicalUnit
    {
        public double Altitude { get; set; }
        public string Identity { get; set; }
        public double CameraRotation { get; set; }
        public DriverSeat driverseat { get; set; }

        public Image visual;

        public PhysicalRocket(StarlingGameSpriteWithRocketTextures textures_rocket, StarlingGameSpriteWithPhysics Context)
        {
            visual = new Image(textures_rocket.rocket1());
            visual.AttachTo(Context.Content);


            //this.CameraRotation = Math.PI / 2;

            #region b2world




            {
                var bodyDef = new b2BodyDef();

                bodyDef.type = Box2D.Dynamics.b2Body.b2_dynamicBody;

                // stop moving if legs stop walking!
                bodyDef.linearDamping = 0;
                bodyDef.angularDamping = 6;
                //bodyDef.angle = 1.57079633;
                //bodyDef.fixedRotation = true;

                body = Context.ground_b2world.CreateBody(bodyDef);


                var fixDef = new Box2D.Dynamics.b2FixtureDef();
                fixDef.density = 0.1;
                fixDef.friction = 0.0;
                fixDef.restitution = 0;


                fixDef.shape = new Box2D.Collision.Shapes.b2CircleShape(1.0);


                var fix = body.CreateFixture(fixDef);

                var fix_data = new Action<double>(
                    jeep_forceA =>
                    {
                        if (jeep_forceA < 1)
                            return;

                        if (Context.oncollision != null)
                            Context.oncollision(this, jeep_forceA);
                    }
                );
                fix.SetUserData(fix_data);
            }


            #endregion

            #region groundkarma_b2world
            {
                var bodyDef = new b2BodyDef();

                bodyDef.type = Box2D.Dynamics.b2Body.b2_dynamicBody;

                // stop moving if legs stop walking!
                bodyDef.linearDamping = 0;
                bodyDef.angularDamping = 6;
                //bodyDef.angle = 1.57079633;
                //bodyDef.fixedRotation = true;

                karmabody = Context.groundkarma_b2world.CreateBody(bodyDef);


                var fixDef = new Box2D.Dynamics.b2FixtureDef();
                fixDef.density = 0.1;
                fixDef.friction = 0.0;
                fixDef.restitution = 0;


                fixDef.shape = new Box2D.Collision.Shapes.b2CircleShape(1.0);


                var fix = karmabody.CreateFixture(fixDef);
            }
            #endregion



            Context.internalunits.Add(this);
        }
    }
}
