using Box2D.Collision.Shapes;
using Box2D.Common.Math;
using Box2D.Dynamics;
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
    public class StarlingGameSpriteWithHindControl : StarlingGameSpriteWithHindTextures
    {
        public StarlingGameSpriteWithHindControl()
        {

            this.onbeforefirstframe += (stage, s) =>
            {
                b2Body ground_current = null;
                b2Body air_current = null;




                #region ground_b2world
                // first frame  ... set up our physccs
                // zombies!!
                var ground_b2world = new b2World(new b2Vec2(0, 0), false);

                var ground_b2debugDraw = new b2DebugDraw();

                var ground_dd = new ScriptCoreLib.ActionScript.flash.display.Sprite();

                s.nativeOverlay.addChild(ground_dd);

                var stagex = 200.0;
                var stagey = 200.0;
                var internalscale = 0.3;
                var stagescale = internalscale;



                ground_b2debugDraw.SetSprite(ground_dd);
                // textures are 512 pixels, while our svgs are 400px
                // so how big is a meter in our game world? :)
                ground_b2debugDraw.SetDrawScale(16);
                ground_b2debugDraw.SetFillAlpha(0.1);
                ground_b2debugDraw.SetLineThickness(1.0);
                ground_b2debugDraw.SetFlags(b2DebugDraw.e_shapeBit);

                ground_b2world.SetDebugDraw(ground_b2debugDraw);





                {
                    var ground_bodyDef = new b2BodyDef();

                    ground_bodyDef.type = Box2D.Dynamics.b2Body.b2_dynamicBody;

                    // stop moving if legs stop walking!
                    ground_bodyDef.linearDamping = 0.0;
                    ground_bodyDef.angularDamping = 20.0;
                    //bodyDef.angle = 1.57079633;
                    ground_bodyDef.fixedRotation = true;

                    var ground_body = ground_b2world.CreateBody(ground_bodyDef);
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


                #region air_b2world
                // first frame  ... set up our physccs
                // zombies!!
                var air_b2world = new b2World(new b2Vec2(0, 0), false);

                var air_b2debugDraw = new b2DebugDraw();

                var air_dd = new ScriptCoreLib.ActionScript.flash.display.Sprite();

                // make it red!
                air_dd.transform.colorTransform = new ColorTransform(1.0, 0, 0);
                // make it slave
                air_dd.alpha = 0.3;

                s.nativeOverlay.addChild(air_dd);




                air_b2debugDraw.SetSprite(air_dd);
                // textures are 512 pixels, while our svgs are 400px
                // so how big is a meter in our game world? :)
                air_b2debugDraw.SetDrawScale(16 * 2.0);
                air_b2debugDraw.SetFillAlpha(0.1);
                air_b2debugDraw.SetLineThickness(1.0);
                air_b2debugDraw.SetFlags(b2DebugDraw.e_shapeBit);

                air_b2world.SetDebugDraw(air_b2debugDraw);





                {
                    var air_bodyDef = new b2BodyDef();

                    air_bodyDef.type = Box2D.Dynamics.b2Body.b2_dynamicBody;

                    // stop moving if legs stop walking!
                    air_bodyDef.linearDamping = 0.0;
                    air_bodyDef.angularDamping = 20.0;
                    //bodyDef.angle = 1.57079633;
                    air_bodyDef.fixedRotation = true;

                    var air_body = air_b2world.CreateBody(air_bodyDef);
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



                {
                    var current_rot = random.NextDouble() * Math.PI;


                    var currentshadow = new Image(
                      textures_hind0_shadow()
                      )
                    {
                    }.AttachTo(
                       Content
                   );

                    var currentvisual = new Sprite().AttachTo(Content);

                    var nowings = new Image(
                      textures_hind0_nowings()
                      )
                    {
                    }.AttachTo(currentvisual);

                    var wings = new Sprite().AttachTo(currentvisual);

                    Enumerable.Range(0, 5).Select(
                        wingindex =>
                            new Image(textures_hind0_wing1()).AttachTo(wings).With(
                              img =>
                              {
                                  var cm = new Matrix();

                                  cm.translate(-160, -160);
                                  cm.rotate(Math.PI * 2 * wingindex / 5);


                                  img.transformationMatrix = cm;

                              }
                            )
                    ).ToArray();





                    {
                        var cm = new Matrix();

                        cm.translate(-160, -160);


                        nowings.transformationMatrix = cm;
                    }


                    {
                        var cm = new Matrix();

                        cm.translate(-160, -160);

                        // shadow with tracks!
                        cm.rotate(current_rot);

                        cm.translate(8, 8);

                        currentshadow.transformationMatrix = cm;
                    }

                    {
                        var cm = new Matrix();


                        // shadow with tracks!
                        cm.rotate(current_rot);


                        currentvisual.transformationMatrix = cm;
                    }


                    #region __keyDown
                    var __keyDown = new object[0xffffff];

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


                    var current_speed = 40.0;

                    var physicstime = new Stopwatch();
                    physicstime.Start();

                    onframe +=
                        delegate
                        {
                            #region SetLinearVelocity

                            var rot = 0;
                            var dx = 0.0;
                            var dy = 0.0;

                            if (__keyDown[(int)Keys.Up] != null)
                            {
                                // we have reasone to keep walking

                                dy = 1;
                            }

                            if (__keyDown[(int)Keys.Down] != null)
                            {
                                // we have reasone to keep walking
                                // go slow backwards
                                dy = -0.5;

                            }

                            if (__keyDown[(int)Keys.Alt] == null)
                            {
                                if (__keyDown[(int)Keys.Left] != null)
                                {
                                    // we have reasone to keep walking

                                    rot = -1;

                                }

                                if (__keyDown[(int)Keys.Right] != null)
                                {
                                    // we have reasone to keep walking

                                    rot = 1;

                                }
                            }
                            else
                            {
                                if (__keyDown[(int)Keys.Left] != null)
                                {
                                    // we have reasone to keep walking

                                    dx = -1;

                                }

                                if (__keyDown[(int)Keys.Right] != null)
                                {
                                    // we have reasone to keep walking

                                    dx = 1;

                                }
                            }

                            {
                                var v = rot * 10;
                                if (v != 0)
                                    ground_current.SetAngularVelocity(v);
                            }

                            {
                                var vx = Math.Cos(ground_current.GetAngle()) * dy * current_speed
                                        + Math.Cos(ground_current.GetAngle() + Math.PI / 2) * dx * current_speed;
                                var vy = Math.Sin(ground_current.GetAngle()) * dy * current_speed
                                        + Math.Sin(ground_current.GetAngle() + Math.PI / 2) * dx * current_speed;

                                ground_current.SetLinearVelocity(
                                    new b2Vec2(
                                     vx, vy


                                    )
                                );
                            }

                            #endregion

                            #region animate
                            {
                                var cm = new Matrix();

                                cm.rotate(this.gametime.ElapsedMilliseconds * 0.001);


                                wings.transformationMatrix = cm;

                            }
                            #endregion


                            var physicstime_elapsed = physicstime.ElapsedMilliseconds;
                            physicstime.Restart();

                            #region Step

                            //update physics world
                            ground_b2world.Step(physicstime_elapsed / 1000.0, 10, 8);
                            ground_b2world.DrawDebugData();
                            //clear applied forces, so they don't stack from each update
                            ground_b2world.ClearForces();

                            air_b2world.Step(physicstime_elapsed / 1000.0, 10, 8);
                            air_b2world.DrawDebugData();
                            //clear applied forces, so they don't stack from each update
                            air_b2world.ClearForces();
                            #endregion

                            #region transformationMatrix, phisics updated, now update visual


                            {
                                var cm = new Matrix();


                                cm.translate(-160, -160);


                                // shadow with tracks!
                                cm.scale(1.2, 1.0);
                                //cm.rotate(rot);
                                //cm.translate(i * 128, yi * 128);



                                cm.rotate(ground_current.GetAngle() + Math.PI / 2);
                                cm.translate(
                                    ground_current.GetPosition().x * 16,
                                    ground_current.GetPosition().y * 16
                                );


                                cm.translate(8, 8);

                                currentshadow.transformationMatrix = cm;
                            }

                            {
                                var cm = new Matrix();

                                cm.rotate(ground_current.GetAngle() + Math.PI / 2);
                                cm.translate(
                                    ground_current.GetPosition().x * 16,
                                    ground_current.GetPosition().y * 16
                                );

                                currentvisual.transformationMatrix = cm;
                            }
                            #endregion


                            #region DisableDefaultContentDransformation
                            DisableDefaultContentDransformation = true;
                            {
                                var cm = new Matrix();


                                cm.translate(
                                    -(ground_current.GetPosition().x * 16),
                                    -(ground_current.GetPosition().y * 16)
                                );

                                cm.rotate(-ground_current.GetAngle() - Math.PI / 2);
                                //cm.rotate(-current.GetAngle());


                                stagex = stage.stageWidth * 0.5;
                                stagey = stage.stageHeight * 0.8;
                                stagescale = internalscale * (stage.stageWidth) / (800.0);


                                cm.scale(stagescale, stagescale);

                                cm.translate(
                                    (stage.stageWidth * 0.5),
                                    (stage.stageHeight * 0.8)
                                );


                                Content.transformationMatrix = cm;

                                air_dd.transform.matrix = cm;
                                ground_dd.transform.matrix = cm;
                            }
                            #endregion
                        };
                }
            };
        }

    }
}
