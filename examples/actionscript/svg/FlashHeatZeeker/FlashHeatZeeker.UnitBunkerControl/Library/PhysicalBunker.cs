using Box2D.Collision.Shapes;
using Box2D.Common.Math;
using Box2D.Dynamics;
using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.UnitJeepControl.Library;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.Shared.BCLImplementation.GLSL;
using starling.core;
using starling.display;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlashHeatZeeker.UnitBunkerControl.Library
{
    public class PhysicalBunker : IPhysicalUnit
    {
        public double Altitude { get; set; }
        public RemoteGame RemoteGameReference { get; set; }

        public string Identity { get; set; }

        public double CameraRotation { get; set; }

        public DriverSeat driverseat { get; set; }


        public b2Body damagebody { get; set; }
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

            this.damagebody.SetPositionAndAngle(
               new b2Vec2(x, y), 0
             );
        }


        StarlingGameSpriteWithBunkerTextures textures;
        StarlingGameSpriteWithPhysics Context;

        public Sprite visual;


        public Image
            visualshadow,

            visual_body,
            visual_shopoverlay,
            visual_shopoverlay_arrow
            ;

        public KeySample CurrentInput { get; set; }


        public PhysicalBunker(StarlingGameSpriteWithBunkerTextures textures, StarlingGameSpriteWithPhysics Context, bool IsShop = false)
        {
            var textures_bunker = textures;

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
               textures.bunker2_shadow()
           ).AttachTo(Context.Content_layer2_shadows);

            visual = new Sprite().AttachTo(Context.Content_layer3_buildings);


            visual_body = new Image(
               textures.bunker2()
           ).AttachTo(visual);

            visual_shopoverlay = new Image(
                 textures.bunker2_shopoverlay()
             ).AttachTo(visual);

            visual_shopoverlay_arrow = new Image(
                 textures.bunker2_shopoverlay_arrow()
             ).AttachTo(visual);


            if (IsShop)
            {

                #region hud_arrow
                var hud_arrow = new Image(
                      textures_bunker.bunker2_shopoverlay_arrow()
                  ).AttachTo(Context);


                StarlingGameSpriteWithPhysics.onframe +=
                    (ScriptCoreLib.ActionScript.flash.display.Stage stage, Starling starling) =>
                    {
                        var gap = new __vec2(
                             (float)(this.body.GetPosition().x - Context.current.body.GetPosition().x),
                             (float)(this.body.GetPosition().y - Context.current.body.GetPosition().y)
                         );

                        var distance = gap.GetLength();

                        if (distance < 40)
                        {
                            visual_shopoverlay_arrow.visible = true;
                            hud_arrow.visible = false;
                            return;
                        }


                        //if (distance < 50)
                        //{
                        //    visual_shopoverlay_arrow.visible = false;
                        //    hud_arrow.visible = false;
                        //    return;
                        //}

                        visual_shopoverlay_arrow.visible = false;
                        hud_arrow.visible = true;

                        var cm = new Matrix();

                        // 
                        var yy = 8 * Math.Sin(this.Context.gametime.ElapsedMilliseconds * 0.002);



                        cm.translate(-128, -128 - 64 + yy);


                        cm.scale(Context.stagescale, Context.stagescale);


                        cm.rotate(gap.GetRotation() - Context.current.body.GetAngle() + Context.current.CameraRotation);

                        cm.translate(
                            (stage.stageWidth * 0.5),
                            (stage.stageHeight * Context.internal_center_y)
                        );


                        hud_arrow.transformationMatrix = cm;
                    };
                #endregion

            }

            this.IsShop = IsShop;

            #region damage_b2world
            {
                //initialize body
                var bdef = new b2BodyDef();
                bdef.angle = 0;
                bdef.fixedRotation = true;
                this.damagebody = Context.damage_b2world.CreateBody(bdef);

                //initialize shape
                var fixdef = new b2FixtureDef();

                var shape = new b2PolygonShape();
                fixdef.shape = shape;

                shape.SetAsBox(4.5, 4.5);

                fixdef.restitution = 0.4; //positively bouncy!



                var fix = this.damagebody.CreateFixture(fixdef);

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
            #endregion


            #region ground_b2world
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

                shape.SetAsBox(4.5, 4.5);

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
            #endregion

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


                shape.SetAsBox(4.5, 4.5);
                fixdef.restitution = 0.4; //positively bouncy!



                this.karmabody.CreateFixture(fixdef);
            }
            Context.internalunits.Add(this);
        }

        public bool IsShop
        {
            get { return this.visual_shopoverlay.visible; }
            set
            {
                this.visual_shopoverlay.visible = value;
                this.visual_shopoverlay_arrow.visible = value;
            }
        }

        public void ShowPositionAndAngle()
        {
            var x = this.body.GetPosition().x * 16;
            var y = this.body.GetPosition().y * 16;

            {
                var cm = new Matrix();
                cm.translate(-128, -128);
                cm.translate(
                    x,
                    y
                );


                visual.transformationMatrix = cm;
            }
            {
                var cm = new Matrix();
                cm.translate(-128, -128);
                cm.translate(
                    x,
                    y
                );
                //cm.translate(8, 8);

                visualshadow.transformationMatrix = cm;
            }

            if (visual_shopoverlay_arrow.visible)
                visual_shopoverlay_arrow.y = 8 * Math.Sin(this.Context.gametime.ElapsedMilliseconds * 0.002) + 96 - 4;

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
