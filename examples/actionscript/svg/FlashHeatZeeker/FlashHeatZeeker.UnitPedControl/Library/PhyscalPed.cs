using Box2D.Common.Math;
using Box2D.Dynamics;
using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.StarlingSetup.Library;
using FlashHeatZeeker.UnitJeepControl.Library;
using FlashHeatZeeker.UnitPed.Library;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.Shared.BCLImplementation.GLSL;
using starling.display;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlashHeatZeeker.UnitPedControl.Library
{
    public class PhysicalPed : IPhysicalUnit
    {
        public string Identity { get; set; }



        public double CameraRotation { get; set; }


        public IPhysicalUnit seatedvehicle { get; set; }
        public DriverSeat driverseat { get; set; }

        public b2Body body { get; set; }
        public b2Body karmabody { get; set; }

        public double speed = 20;



        public void SetPositionAndAngle(double x, double y, double a = 0)
        {
            this.body.SetPositionAndAngle(
                new b2Vec2(x, y), a
            );

            this.karmabody.SetPositionAndAngle(
              new b2Vec2(x, y), a
            );

        }



        Velocity velocity = new Velocity();

        // nop
        public KeySample CurrentInput = new KeySample();
        public void SetVelocityFromInput(KeySample __keyDown)
        {
            CurrentInput = __keyDown;






            ExtractVelocityFromInput(__keyDown, velocity);



            if (velocity.LinearVelocityX == 0)
                if (velocity.LinearVelocityY == 0)
                    if (velocity.AngularVelocity == 0)
                        return;


            // we are moving. stop laying on the ground mode
            this.visual.LayOnTheGround = false;


        }

        public void FeedKarma()
        {
            if (this.KarmaInput0.Count > 0)
            {
                var k = new KeySample
                {
                    value = CurrentInput.value,

                    BodyIsActive = this.body.IsActive(),

                    fixup = true,

                    angle = this.body.GetAngle(),
                    x = this.body.GetPosition().x,
                    y = this.body.GetPosition().y,
                };

                if (CurrentInput.fixup)
                {
                    k.x = CurrentInput.x;
                    k.y = CurrentInput.y;
                    k.angle = CurrentInput.angle;
                }

          

                this.KarmaInput0.Enqueue(k);
                this.KarmaInput0.Dequeue();
            }
        }



        // script: error JSC1000: ActionScript : Opcode not implemented: stind.r8 at FlashHeatZeeker.UnitPedControl.Library.PhysicalPed.ExtractVelocityFromInput

        public class Velocity
        {
            public double AngularVelocity;
            public double LinearVelocityX;
            public double LinearVelocityY;
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

                if (!__keyDown[Keys.Alt])
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
                else
                {
                    if (__keyDown[Keys.Left])
                    {
                        // we have reasone to keep walking

                        value.LinearVelocityX = -1;

                    }

                    if (__keyDown[Keys.Right])
                    {
                        // we have reasone to keep walking

                        value.LinearVelocityX = 1;

                    }
                }
            }


        }


        //[Description("Owned by a remote game!")]
        //public bool __network_fixup = false;

        public RemoteGame RemoteGameReference;

        //public double __network_fixup_x;
        //public double __network_fixup_y;
        //public double __network_fixup_angle;

        public bool ShouldDoKarmaFixup()
        {

            return false;
        }

        public void ApplyVelocity()
        {
            // this is now

            {
                var current = this.body;
                var v = velocity.AngularVelocity * 10;
                var vx = Math.Cos(current.GetAngle()) * velocity.LinearVelocityY * this.speed
                    + Math.Cos(current.GetAngle() + Math.PI / 2) * velocity.LinearVelocityX * this.speed;
                var vy = Math.Sin(current.GetAngle()) * velocity.LinearVelocityY * this.speed
                        + Math.Sin(current.GetAngle() + Math.PI / 2) * velocity.LinearVelocityX * this.speed;


                this.visual.currentvisual.alpha = 1.0;

                #region RemoteGameReference
                if (RemoteGameReference != null)
                    if (vx == 0)
                        if (vy == 0)
                            if (v == 0)
                            {
                                // not moving anymore in network mode
                                // far enough to be out of sync?

                                if (karmabody.GetLinearVelocity().Length() == 0)
                                    if (this.KarmaInput0.All(k => k.value == 0))
                                    {

                                        this.body.SetAngle(
                                             karmabody.GetAngle()
                                         );


                                        var gap = new __vec2(
                                            (float)this.karmabody.GetPosition().x - (float)this.body.GetPosition().x,
                                            (float)this.karmabody.GetPosition().y - (float)this.body.GetPosition().y
                                        );

                                        // tolerate lesser distance?
                                        if (gap.GetLength() > 0.5)
                                        {
                                            //this.body.SetPositionAndAngle(
                                            //    new b2Vec2(
                                            //        this.karmabody.GetPosition().x,
                                            //        this.karmabody.GetPosition().y
                                            //    ),
                                            //    this.karmabody.GetAngle()
                                            //);

                                            // show we are in the wrong place!
                                            this.visual.currentvisual.alpha = 0.3;


                                            // look at where we should be instead
                                            this.body.SetAngle(
                                                gap.GetRotation()
                                            );

                                            // and walk there!
                                            vx = Math.Cos(current.GetAngle()) * 0.5 * this.speed
                                               + Math.Cos(current.GetAngle() + Math.PI / 2) * 0 * this.speed;
                                            vy = Math.Sin(current.GetAngle()) * 0.5 * this.speed
                                                   + Math.Sin(current.GetAngle() + Math.PI / 2) * 0 * this.speed;
                                        }
                                    }

                            }
                #endregion

                current.SetAngularVelocity(v);
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


                current.SetActive(
                     _karma__keyDown.BodyIsActive
                 );

                current.SetLinearVelocity(
                    new b2Vec2(
                     vx, vy
                    )
                );

                if (_karma__keyDown.fixup)
                {
                    var fixupmultiplier = 0.90;

                    current.SetAngle(
                        // meab me in scotty,
                         _karma__keyDown.angle + (current.GetAngle() - _karma__keyDown.angle) * fixupmultiplier

                    );



                    var gap = new __vec2(
                        (float)this.karmabody.GetPosition().x - (float)_karma__keyDown.x,
                        (float)this.karmabody.GetPosition().y - (float)_karma__keyDown.y
                    );

                    if (gap.GetLength() > 0.1)
                    {
                        current.SetLinearVelocity(
                             new b2Vec2(
                              vx - gap.x * 2.0,
                              vy - gap.y * 2.0
                             )
                         );
                    }
                }
            }
        }

        bool prev = false;
        double prevx = 0.0;
        double prevy = 0.0;


        public void ShowPositionAndAngle()
        {
            //if (body != null)
            //    body.SetActive(true);

            // where are we now
            this.visual.SetPositionAndAngle(
                this.body.GetPosition().x * 16,
                this.body.GetPosition().y * 16,
                this.body.GetAngle()
            );

            if (this.seatedvehicle != null)
            {
                this.visual.shadow.visible = false;
                this.visual.currentvisual.visible = false;
                return;
            }

            //var iswalking = velocity.LinearVelocityX != 0 || velocity.LinearVelocityY != 0;
            var iswalking = this.body.GetLinearVelocity().Length() > 0;
            this.visual.Animate(velocity.LinearVelocityX, velocity.LinearVelocityY);



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

                if (distance > 1)
                {
                    var tracks0 = new Image(textures.ped_footprints()).AttachTo(Context.Content_layer0_tracks);

                    var cm = new Matrix();

                    cm.translate(-32, -32);
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

        public Queue<KeySample> KarmaInput4 = new Queue<KeySample>();
        public Queue<KeySample> KarmaInput0 = new Queue<KeySample>();

        public VisualPed visual;

        StarlingGameSpriteWithPedTextures textures;
        StarlingGameSpriteWithPhysics Context;

        public PhysicalPed(StarlingGameSpriteWithPedTextures textures, StarlingGameSpriteWithPhysics Context)
        {
            this.textures = textures;
            this.Context = Context;

            for (int i = 0; i < 7; i++)
            {
                this.KarmaInput0.Enqueue(
                    new KeySample()
                );
            }


            visual = new VisualPed(textures, Context,
                AnimateSeed:
                    Context.random.Next() % 3000
            );


            #region b2world




            {
                var bodyDef = new b2BodyDef();

                bodyDef.type = Box2D.Dynamics.b2Body.b2_dynamicBody;

                // stop moving if legs stop walking!
                bodyDef.linearDamping = 0;
                bodyDef.angularDamping = 0;
                //bodyDef.angle = 1.57079633;
                bodyDef.fixedRotation = true;

                body = Context.ground_b2world.CreateBody(bodyDef);


                var fixDef = new Box2D.Dynamics.b2FixtureDef();
                fixDef.density = 0.1;
                fixDef.friction = 0.0;
                fixDef.restitution = 0;


                fixDef.shape = new Box2D.Collision.Shapes.b2CircleShape(1.0);


                var fix = body.CreateFixture(fixDef);
            }


            #endregion


            {
                var bodyDef = new b2BodyDef();

                bodyDef.type = Box2D.Dynamics.b2Body.b2_dynamicBody;

                // stop moving if legs stop walking!
                bodyDef.linearDamping = 0;
                bodyDef.angularDamping = 0;
                //bodyDef.angle = 1.57079633;
                bodyDef.fixedRotation = true;

                karmabody = Context.groundkarma_b2world.CreateBody(bodyDef);


                var fixDef = new Box2D.Dynamics.b2FixtureDef();
                fixDef.density = 0.1;
                fixDef.friction = 0.0;
                fixDef.restitution = 0;


                fixDef.shape = new Box2D.Collision.Shapes.b2CircleShape(1.0);


                var fix = karmabody.CreateFixture(fixDef);
            }



            Context.internalunits.Add(this);

        }
    }

}
