using Box2D.Common.Math;
using Box2D.Dynamics;
using FlashHeatZeeker.UnitJeep.Library;
using FlashHeatZeekerWithStarlingB2.Library;
using ScriptCoreLib.ActionScript.flash.geom;
using starling.display;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlashHeatZeeker.UnitJeepControl.Library
{
    public class StarlingGameSpriteWithJeepControl : StarlingGameSpriteWithJeepTextures
    {
        // hacky way, yet user probably ahs only one keyboard / set of hands anyhow
        public static object[] __keyDown = new object[0xffffff];

        public StarlingGameSpriteWithJeepControl()
        {
            this.autorotate = false;

            this.onbeforefirstframe += (stage, s) =>
            {
                var b2world = default(b2World);


                var physicstime = new Stopwatch();

                physicstime.Start();


                #region __keyDown

                stage.keyDown +=
                   e =>
                   {
                       if (__keyDown[e.keyCode] != null)
                           return;

                       // http://circlecube.com/2008/08/actionscript-key-listener-tutorial/
                       if (e.altKey)
                           __keyDown[(int)Keys.Alt] = new object();

                       __keyDown[e.keyCode] = new object();
                   };

                stage.keyUp +=
                 e =>
                 {
                     if (!e.altKey)
                         __keyDown[(int)Keys.Alt] = null;

                     __keyDown[e.keyCode] = null;
                 };

                #endregion


                b2Body current = null;



                #region b2world
                // first frame  ... set up our physccs
                // zombies!!
                b2world = new b2World(new b2Vec2(0, 0), false);

                var b2debugDraw = new b2DebugDraw();

                var dd = new ScriptCoreLib.ActionScript.flash.display.Sprite();

                s.nativeOverlay.addChild(dd);

                var stagex = 200.0;
                var stagey = 200.0;
                var internalscale = 0.3;
                var stagescale = internalscale;



                b2debugDraw.SetSprite(dd);
                // textures are 512 pixels, while our svgs are 400px
                // so how big is a meter in our game world? :)
                b2debugDraw.SetDrawScale(16);
                b2debugDraw.SetFillAlpha(0.1);
                b2debugDraw.SetLineThickness(1.0);
                b2debugDraw.SetFlags(b2DebugDraw.e_shapeBit);

                b2world.SetDebugDraw(b2debugDraw);


                // add ghost obstacles for diagnostics

                {
                    var bodyDef = new b2BodyDef();

                    bodyDef.type = Box2D.Dynamics.b2Body.b2_dynamicBody;

                    // stop moving if legs stop walking!
                    bodyDef.linearDamping = 10.0;
                    bodyDef.angularDamping = 0.3;
                    //bodyDef.angle = 1.57079633;
                    bodyDef.fixedRotation = true;

                    var body = b2world.CreateBody(bodyDef);
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


                var shadow = new Image(
                            textures_jeep_shadow()
                            )
                {
                }.AttachTo(
                            Content
                    //q
                        );

                var currentvisual = new Sprite().AttachTo(Content);







                var tire0 = new Image(
                  textures_black4()
                  )
                {
                }.AttachTo(
                    //Content
                    currentvisual
                );

                var tire1 = new Image(textures_black4()).AttachTo(currentvisual);
                var tire2 = new Image(textures_black4()).AttachTo(currentvisual);
                var tire3 = new Image(textures_black4()).AttachTo(currentvisual);

                var imgstand = new Image(
                  textures_jeep()
                  )
                {
                }.AttachTo(
                    //Content
                    currentvisual
                      );



                {
                    var cm = new Matrix();

                    cm.translate(-32, -32);

                    cm.scale(1.0, 1.0);

                    //cm.translate(i * 512, yi * 512);

                    // how high his the unit?
                    cm.translate(32, 32);

                    shadow.transformationMatrix = cm;
                }




                {
                    var cm = new Matrix();
                    cm.translate(-2, -2);
                    cm.scale(2, 4);
                    cm.rotate(Math.PI * 0.1);

                    cm.translate(-18, -20);

                    tire0.transformationMatrix = cm;
                }

                {
                    var cm = new Matrix();
                    cm.translate(-2, -2);
                    cm.scale(2, 4);
                    cm.rotate(Math.PI * 0.1);

                    cm.translate(18, -20);

                    tire1.transformationMatrix = cm;
                }

                {
                    var cm = new Matrix();
                    cm.translate(-2, -2);
                    cm.scale(2, 4);

                    cm.translate(-18, 20);

                    tire2.transformationMatrix = cm;
                }


                {
                    var cm = new Matrix();
                    cm.translate(-2, -2);
                    cm.scale(2, 4);

                    cm.translate(18, 20);

                    tire3.transformationMatrix = cm;
                }


                {
                    var cm = new Matrix();
                    cm.translate(-32, -32);
                    imgstand.transformationMatrix = cm;
                }

                {
                    var cm = new Matrix();

                    // how big shall the shadow be?
                    cm.scale(1.0, 1.0);

                    //cm.translate(i * 512, yi * 512);
                    currentvisual.transformationMatrix = cm;
                }




                var xwheels = new[] { 
                        //top left
                        new Wheel { b2world = b2world, x = -1.1, y = -1.2, width = 0.4, length = 0.8, revolving = true, powered = true },

                        //top right
                        new Wheel{b2world= b2world, x =1.1,  y =-1.2,  width =0.4,  length =0.8,  revolving =true,  powered =true},


                        //back left
                        new Wheel{b2world= b2world, x =-1.1,  y =1.2,  width =0.4,  length =0.8,  revolving =false,  powered =false},

                        //back right
                        new Wheel{b2world= b2world, x =1.1,  y =1.2,  width =0.4,  length =0.8,  revolving =false,  powered =false},
                    };

                xwheels[0].setAngle += a =>
                {
                    var cm = new Matrix();
                    cm.translate(-2, -2);
                    cm.scale(2, 4);
                    cm.rotate(a.DegreesToRadians());

                    cm.translate(-18, -20);

                    tire0.transformationMatrix = cm;
                };

                xwheels[1].setAngle += a =>
                {

                    var cm = new Matrix();
                    cm.translate(-2, -2);
                    cm.scale(2, 4);
                    cm.rotate(a.DegreesToRadians());

                    cm.translate(18, -20);

                    tire1.transformationMatrix = cm;
                };
                Func<double, double, double[]> ff = (a, b) => { return new double[] { a, b }; };

                var unit4_physics = new Car(
                 b2world: b2world,
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

                current = unit4_physics.body;

                var current_speed = 40.0;

                onframe += delegate
                {
                    #region SetLinearVelocity

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



                    //current.SetAngularVelocity(rot * 10);

                    var physicstime_elapsed = physicstime.ElapsedMilliseconds;

                    unit4_physics.update(physicstime_elapsed);


                    ////current.SetLinearVelocity(
                    ////    new b2Vec2(
                    ////        Math.Cos(current.GetAngle()) * dy * current_speed
                    ////        + Math.Cos(current.GetAngle() + Math.PI / 2) * dx * current_speed,
                    ////        Math.Sin(current.GetAngle()) * dy * current_speed
                    ////        + Math.Sin(current.GetAngle() + Math.PI / 2) * dx * current_speed

                    ////    )
                    ////);


                    #endregion

                    #region Step
                    physicstime.Restart();
                    //update physics world
                    b2world.Step(physicstime_elapsed / 1000.0, 10, 8);
                    b2world.DrawDebugData();

                    //

                    //clear applied forces, so they don't stack from each update
                    b2world.ClearForces();
                    #endregion

                    #region transformationMatrix, phisics updated, now update visual


                    {
                        var cm = new Matrix();

                        cm.translate(-32, -32);
                        // how big shall the shadow be?
                        //cm.scale(1.0, 1.0);

                        // shadow does NOT move!
                        cm.rotate(current.GetAngle());
                        //cm.translate(i * 128, yi * 128);

                        cm.translate(
                             current.GetPosition().x * 16,
                             current.GetPosition().y * 16
                         );
                        cm.translate(6, 6);

                        shadow.transformationMatrix = cm;
                    }

                    {
                        var cm = new Matrix();

                        //cm.translate(-32, -32);
                        //cm.scale(1.0, 1.0);

                        // physics 0 looks right
                        cm.rotate(current.GetAngle());
                        cm.translate(
                            current.GetPosition().x * 16,
                            current.GetPosition().y * 16
                        );

                        currentvisual.transformationMatrix = cm;
                    }
                    #endregion


                    #region DisableDefaultContentDransformation
                    DisableDefaultContentDransformation = true;
                    {
                        var cm = new Matrix();


                        cm.translate(
                            -(current.GetPosition().x * 16),
                            -(current.GetPosition().y * 16)
                        );

                        //cm.rotate(-current.GetAngle() - Math.PI / 2);
                        cm.rotate(-current.GetAngle());


                        stagex = stage.stageWidth * 0.5;
                        stagey = stage.stageHeight * 0.8;
                        stagescale = internalscale * 3.0 * (stage.stageWidth) / (800.0);


                        cm.scale(stagescale, stagescale);

                        cm.translate(
                            (stage.stageWidth * 0.5),
                            (stage.stageHeight * 0.8)
                        );


                        Content.transformationMatrix = cm;
                        dd.transform.matrix = cm;
                    }
                    #endregion
                };
            };
        }
    }
}
