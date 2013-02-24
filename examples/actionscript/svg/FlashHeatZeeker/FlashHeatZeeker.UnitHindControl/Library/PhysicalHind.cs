using Box2D.Collision.Shapes;
using Box2D.Common.Math;
using Box2D.Dynamics;
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
    public class PhysicalHind
    {
        public double speed = 40;

        public double VerticalVelocity;

        public double AngularVelocity;
        public double LinearVelocityX;
        public double LinearVelocityY;

        public VisualHind visual;

        public b2Body ground_current = null;
        public b2Body air_current = null;


        public b2Body current
        {
            get
            {
                if (visual.Altitude > 0)
                    return air_current;

                return ground_current;
            }
        }

        public b2Body current_slave1
        {
            get
            {
                if (visual.Altitude > 0)
                    return ground_current;

                return air_current;
            }
        }


        public Action<object[]> SetVelocityFromInput;
        public Action ApplyVelocity;
        public Action ShowPositionAndAngle;

        public PhysicalHind(StarlingGameSpriteWithHindTextures textures, StarlingGameSpriteWithPhysics Context)
        {
            visual = new VisualHind(textures, Context.Content, Context.airzoom);






            #region ground_b2world ground_current


            {
                var ground_bodyDef = new b2BodyDef();

                ground_bodyDef.type = Box2D.Dynamics.b2Body.b2_dynamicBody;

                // stop moving if legs stop walking!
                ground_bodyDef.linearDamping = 10.0;
                ground_bodyDef.angularDamping = 20.0;
                //bodyDef.angle = 1.57079633;
                ground_bodyDef.fixedRotation = true;

                var ground_body = Context.ground_b2world.CreateBody(ground_bodyDef);
                ground_current = ground_body;


                var ground_fixDef = new Box2D.Dynamics.b2FixtureDef();
                ground_fixDef.density = 0.1;
                ground_fixDef.friction = 0.01;
                ground_fixDef.restitution = 0;

                var ground_fixdef_shape = new b2PolygonShape();

                ground_fixDef.shape = ground_fixdef_shape;

                // physics unit is looking to right
                ground_fixdef_shape.SetAsBox(4, 1);



                var ground_fix = ground_body.CreateFixture(ground_fixDef);
            }



            #endregion


            #region air_b2world air_current




            {
                var air_bodyDef = new b2BodyDef();

                air_bodyDef.type = Box2D.Dynamics.b2Body.b2_dynamicBody;

                // stop moving if legs stop walking!
                air_bodyDef.linearDamping = 10.0;
                air_bodyDef.angularDamping = 20.0;
                //bodyDef.angle = 1.57079633;
                air_bodyDef.fixedRotation = true;
                air_bodyDef.active = false;

                var air_body = Context.air_b2world.CreateBody(air_bodyDef);
                air_current = air_body;


                var air_fixDef = new Box2D.Dynamics.b2FixtureDef();
                air_fixDef.density = 0.1;
                air_fixDef.friction = 0.01;
                air_fixDef.restitution = 0;

                var air_fixdef_shape = new b2PolygonShape();

                air_fixDef.shape = air_fixdef_shape;

                // physics unit is looking to right
                air_fixdef_shape.SetAsBox(4, 1);



                var air_fix = air_body.CreateFixture(air_fixDef);
            }


            #endregion


            #region SetVelocityFromInput
            this.SetVelocityFromInput =
                __keyDown =>
                {
                    this.AngularVelocity = 0;
                    this.LinearVelocityX = 0;
                    this.LinearVelocityY = 0;

                    if (__keyDown[(int)Keys.Up] != null)
                    {
                        // we have reasone to keep walking

                        this.LinearVelocityY = 1;
                    }

                    if (__keyDown[(int)Keys.Down] != null)
                    {
                        // we have reasone to keep walking
                        // go slow backwards
                        this.LinearVelocityY = -0.5;

                    }

                    if (__keyDown[(int)Keys.Alt] == null)
                    {
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
                    else
                    {
                        if (__keyDown[(int)Keys.Left] != null)
                        {
                            // we have reasone to keep walking

                            this.LinearVelocityX = -1;

                        }

                        if (__keyDown[(int)Keys.Right] != null)
                        {
                            // we have reasone to keep walking

                            this.LinearVelocityX = 1;

                        }
                    }
                };
            #endregion

            var ApplyVelocityElapsed = Context.gametime.ElapsedMilliseconds;
            #region ApplyVelocity
            this.ApplyVelocity = delegate
            {
                var dx = Context.gametime.ElapsedMilliseconds - ApplyVelocityElapsed;
                ApplyVelocityElapsed = Context.gametime.ElapsedMilliseconds;

                var current = this.current;

                {
                    var v = this.AngularVelocity * 10;
                    if (v != 0)
                        current.SetAngularVelocity(v);
                }

                {
                    var vx = Math.Cos(current.GetAngle()) * this.LinearVelocityY * this.speed
                            + Math.Cos(current.GetAngle() + Math.PI / 2) * this.LinearVelocityX * this.speed;
                    var vy = Math.Sin(current.GetAngle()) * this.LinearVelocityY * this.speed
                            + Math.Sin(current.GetAngle() + Math.PI / 2) * this.LinearVelocityX * this.speed;

                    if (vx == 0 && vy == 0)
                    {

                    }
                    else
                    {
                        this.current.SetLinearVelocity(
                            new b2Vec2(
                             vx, vy


                            )
                        );
                    }
                }


                this.visual.Altitude =
                    (this.visual.Altitude + 0.005 * dx * this.VerticalVelocity).Max(0).Min(1);
            };
            #endregion

            #region ShowPositionAndAngle
            this.ShowPositionAndAngle = delegate
            {
                if (current != null)
                    current.SetActive(true);

                if (current_slave1 != null)
                {
                    current_slave1.SetActive(false);

                    // sync up
                    if (current != null)
                        current_slave1.SetPositionAndAngle(
                            current.GetPosition(),
                            current.GetAngle()
                        );
                }

                this.visual.Animate(Context.gametime);
                // where are we now
                this.visual.SetPositionAndAngle(
                    this.current.GetPosition().x * 16,
                    this.current.GetPosition().y * 16,
                    this.current.GetAngle()
                );
            };
            #endregion

        }
    }

}
