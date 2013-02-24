using Box2D.Common.Math;
using Box2D.Dynamics;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.UnitPed.Library;
using ScriptCoreLib.ActionScript.flash.geom;
using starling.display;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlashHeatZeeker.UnitPedControl.Library
{
    class StarlingGameSpriteWithPedControl : StarlingGameSpriteWithPhysics
    {

        public StarlingGameSpriteWithPedControl()
        {
            var textures = new StarlingGameSpriteWithPedTextures(new_tex_crop);


            this.onbeforefirstframe += (stage, s) =>
            {
              


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

                    var body = ground_b2world.CreateBody(bodyDef);


                    var fixDef = new Box2D.Dynamics.b2FixtureDef();
                    fixDef.density = 0.1;
                    fixDef.friction = 0.01;
                    fixDef.restitution = 0;


                    fixDef.shape = new Box2D.Collision.Shapes.b2CircleShape(2.0);


                    var fix = body.CreateFixture(fixDef);

                    //body.SetPosition(
                    //    new b2Vec2(0, -100 * 16)
                    //);
                }

                {
                    var bodyDef = new b2BodyDef();

                    bodyDef.type = Box2D.Dynamics.b2Body.b2_dynamicBody;

                    // stop moving if legs stop walking!
                    bodyDef.linearDamping = 10.0;
                    bodyDef.angularDamping = 0.3;
                    //bodyDef.angle = 1.57079633;
                    bodyDef.fixedRotation = true;

                    var body = ground_b2world.CreateBody(bodyDef);
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


                {
                    var bodyDef = new b2BodyDef();

                    bodyDef.type = Box2D.Dynamics.b2Body.b2_dynamicBody;

                    // stop moving if legs stop walking!
                    bodyDef.linearDamping = 10.0;
                    bodyDef.angularDamping = 0.3;
                    //bodyDef.angle = 1.57079633;
                    bodyDef.fixedRotation = true;

                    var body = ground_b2world.CreateBody(bodyDef);
                    current = body;


                    var fixDef = new Box2D.Dynamics.b2FixtureDef();
                    fixDef.density = 0.1;
                    fixDef.friction = 0.01;
                    fixDef.restitution = 0;


                    fixDef.shape = new Box2D.Collision.Shapes.b2CircleShape(2.0);


                    var fix = body.CreateFixture(fixDef);
                }


                #endregion





                var walk_ani = new[] {
                    textures.ped_walk3_leftclose(), 
                    textures.ped_walk3x_rightclose(),
                    textures.ped_walk1x_rightfar(),
                    textures.ped_walk2x_rightmid(),
                    textures.ped_walk3x_rightclose(),
                    textures.ped_walk3_leftclose(), 
                    textures.ped_walk1_leftfar(), 
                    textures.ped_walk2_leftmid(), 

                };

                var texframes = new[] {

                    textures.ped_stand(),
                };

                // 781
                // 15 FPS
                // 60 FPS
                // 31  FPS
                var rr = random.Next() % 1024;

                // 41
                //var q = new Sprite().AttachTo(Content);

                // Error: Error #3691: Resource limit for this resource type exceeded.
                var shadow = new Image(
                    textures.ped_shadow()
                    )
                    {
                        // fkn expensive!!
                        //alpha = 0.5
                    }.AttachTo(
                        Content
                    //q
                        );

                //peds.Add(imgstand);

                {
                    var cm = new Matrix();

                    cm.translate(-32, -32);
                    // how big shall the shadow be?
                    cm.scale(4.0, 4.0);

                    //cm.rotate(random.NextDouble() * Math.PI);
                    //cm.translate(i * 128, yi * 128);
                    shadow.transformationMatrix = cm;
                }
                var currentvisual = new Image(
                    texframes[
                    //0
                    random.Next() % texframes.Length
                        ]) { }.AttachTo(
                        Content
                    //q
                        );


                {
                    var cm = new Matrix();

                    cm.translate(-32, -32);
                    cm.scale(2.0, 2.0);

                    //cm.rotate(random.NextDouble() * Math.PI);
                    //cm.translate(i * 128, yi * 128);
                    currentvisual.transformationMatrix = cm;
                }



                // 54FPS without
                // 43FPS

                var current_speed = 40.0;

                onframe += delegate
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

                    current.SetAngularVelocity(rot * 10);
                    current.SetLinearVelocity(
                        new b2Vec2(
                            Math.Cos(current.GetAngle()) * dy * current_speed
                            + Math.Cos(current.GetAngle() + Math.PI / 2) * dx * current_speed,
                            Math.Sin(current.GetAngle()) * dy * current_speed
                            + Math.Sin(current.GetAngle() + Math.PI / 2) * dx * current_speed

                        )
                    );


                    #endregion

              


                    #region  animate

                    var ii = ((frameid + 0) / (8)) % walk_ani.Length;




                    // 40fps
                    if (dx == 0 && dy == 0)
                        currentvisual.texture = texframes[0];
                    else
                        currentvisual.texture = walk_ani[ii];
                    #endregion


                    #region transformationMatrix, phisics updated, now update visual


                    {
                        var cm = new Matrix();

                        cm.translate(-32, -32);
                        // how big shall the shadow be?
                        cm.scale(4.0, 4.0);

                        // shadow does NOT move!
                        //cm.rotate(current.GetAngle());
                        //cm.translate(i * 128, yi * 128);

                        cm.translate(
                             current.GetPosition().x * 16,
                             current.GetPosition().y * 16
                         );

                        shadow.transformationMatrix = cm;
                    }

                    {
                        var cm = new Matrix();

                        cm.translate(-32, -32);
                        cm.scale(2.0, 2.0);

                        // physics 0 looks right
                        cm.rotate(current.GetAngle() + Math.PI / 2);
                        cm.translate(
                            current.GetPosition().x * 16,
                            current.GetPosition().y * 16
                        );

                        currentvisual.transformationMatrix = cm;
                    }
                    #endregion

                    #region simulate a weapone!
                    if (__keyDown[(int)Keys.ControlKey] != null)
                        if (frameid % 20 == 0)
                        {
                            var bodyDef = new b2BodyDef();

                            bodyDef.type = Box2D.Dynamics.b2Body.b2_dynamicBody;

                            // stop moving if legs stop walking!
                            bodyDef.linearDamping = 0;
                            bodyDef.angularDamping = 0;
                            //bodyDef.angle = 1.57079633;
                            bodyDef.fixedRotation = true;

                            var body = ground_b2world.CreateBody(bodyDef);
                            body.SetPosition(
                                new b2Vec2(
                                    current.GetPosition().x + 2,
                                    current.GetPosition().y + 2
                                )
                            );

                            body.SetLinearVelocity(
                                   new b2Vec2(
                                     100,
                                    100
                                )
                            );

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


                    //#region DisableDefaultContentDransformation
                    //DisableDefaultContentDransformation = true;
                    //{
                    //    var cm = new Matrix();


                    //    cm.translate(
                    //        -(current.GetPosition().x * 16),
                    //        -(current.GetPosition().y * 16)
                    //    );

                    //    cm.rotate(-current.GetAngle() - Math.PI / 2);


                    //    stagex = stage.stageWidth * 0.5;
                    //    stagey = stage.stageHeight * 0.8;
                    //    stagescale = internalscale * (stage.stageWidth) / (800.0);


                    //    cm.scale(stagescale, stagescale);

                    //    cm.translate(
                    //        (stage.stageWidth * 0.5),
                    //        (stage.stageHeight * 0.8)
                    //    );


                    //    Content.transformationMatrix = cm;
                    //    dd.transform.matrix = cm;
                    //}
                    //#endregion

                };

         
            };



        }
    }
}
