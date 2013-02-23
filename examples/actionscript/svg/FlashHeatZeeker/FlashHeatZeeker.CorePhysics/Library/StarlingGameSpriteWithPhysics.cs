using Box2D.Collision.Shapes;
using Box2D.Common.Math;
using Box2D.Dynamics;
using FlashHeatZeeker.StarlingSetup.Library;
using ScriptCoreLib.ActionScript.flash.geom;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FlashHeatZeeker.CorePhysics.Library
{
    public class StarlingGameSpriteWithPhysics : StarlingGameSpriteBase
    {
        public b2World
            ground_b2world,
            air_b2world;

        public b2Body
            current,
            current_slave1;

        public ScriptCoreLib.ActionScript.flash.display.Sprite
            ground_dd,
            air_dd;

        public StarlingGameSpriteWithPhysics()
        {
            var airzoom = 1.5;


            //b2Body ground_current = null;
            //b2Body air_current = null;




            #region ground_b2world
            // first frame  ... set up our physccs
            // zombies!!
            ground_b2world = new b2World(new b2Vec2(0, 0), false);

            var ground_b2debugDraw = new b2DebugDraw();

            ground_dd = new ScriptCoreLib.ActionScript.flash.display.Sprite();
            ground_dd.transform.colorTransform = new ColorTransform(0.0, 0, 1.0);





            ground_b2debugDraw.SetSprite(ground_dd);
            // textures are 512 pixels, while our svgs are 400px
            // so how big is a meter in our game world? :)
            ground_b2debugDraw.SetDrawScale(16);
            ground_b2debugDraw.SetFillAlpha(0.1);
            ground_b2debugDraw.SetLineThickness(1.0);
            ground_b2debugDraw.SetFlags(b2DebugDraw.e_shapeBit);

            ground_b2world.SetDebugDraw(ground_b2debugDraw);





            #endregion


            #region air_b2world
            // first frame  ... set up our physccs
            // zombies!!
            air_b2world = new b2World(new b2Vec2(0, 0), false);

            var air_b2debugDraw = new b2DebugDraw();

            air_dd = new ScriptCoreLib.ActionScript.flash.display.Sprite();

            // make it red!
            air_dd.transform.colorTransform = new ColorTransform(1.0, 0, 0);
            // make it slave
            air_dd.alpha = 0.3;





            air_b2debugDraw.SetSprite(air_dd);
            // textures are 512 pixels, while our svgs are 400px
            // so how big is a meter in our game world? :)
            air_b2debugDraw.SetDrawScale(16);
            air_b2debugDraw.SetFillAlpha(0.1);
            air_b2debugDraw.SetLineThickness(1.0);
            air_b2debugDraw.SetFlags(b2DebugDraw.e_shapeBit);

            air_b2world.SetDebugDraw(air_b2debugDraw);








            #endregion


            #region obstacles
            {
                var bodyDef = new b2BodyDef();

                bodyDef.type = Box2D.Dynamics.b2Body.b2_dynamicBody;

                // stop moving if legs stop walking!
                bodyDef.linearDamping = 10.0;
                bodyDef.angularDamping = 0.3;
                //bodyDef.angle = 1.57079633;
                bodyDef.fixedRotation = true;

                var body = air_b2world.CreateBody(bodyDef);
                body.SetPosition(new b2Vec2(10, 10));

                var fixDef = new Box2D.Dynamics.b2FixtureDef();
                fixDef.density = 0.1;
                fixDef.friction = 0.01;
                fixDef.restitution = 0;


                fixDef.shape = new Box2D.Collision.Shapes.b2CircleShape(10.0);


                var fix = body.CreateFixture(fixDef);

                body.SetPosition(
                    new b2Vec2(-8, 0)
                );
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


                fixDef.shape = new Box2D.Collision.Shapes.b2CircleShape(10.0);


                var fix = body.CreateFixture(fixDef);


                body.SetPosition(
                    new b2Vec2(8, 0)
                );
            }
            #endregion


            var physicstime = new Stopwatch();
            physicstime.Start();

            this.onbeforefirstframe += (stage, s) =>
            {
                s.nativeOverlay.addChild(ground_dd);
                s.nativeOverlay.addChild(air_dd);

                onframe +=
                    delegate
                    {
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

                        #region DisableDefaultContentDransformation
                        DisableDefaultContentDransformation = true;
                        {
                            var cm = new Matrix();

                            if (current != null)
                            {
                                cm.translate(
                                    -(current.GetPosition().x * 16),
                                    -(current.GetPosition().y * 16)
                                );

                                cm.rotate(-current.GetAngle() - Math.PI / 2);
                            }
                            //cm.rotate(-current.GetAngle());


                            stagescale = internalscale * (stage.stageWidth) / (800.0);


                            cm.scale(stagescale, stagescale);

                            var cm_air = cm.clone();

                            cm_air.scale(airzoom, airzoom);

                            cm_air.translate(
                                (stage.stageWidth * 0.5),
                                (stage.stageHeight * 0.8)
                            );

                            cm.translate(
                                (stage.stageWidth * 0.5),
                                (stage.stageHeight * 0.8)
                            );


                            Content.transformationMatrix = cm;

                            ground_dd.transform.matrix = cm;

                            air_dd.transform.matrix = cm_air;
                        }
                        #endregion
                    };
            };


        }
    }
}
