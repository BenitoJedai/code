using Box2D.Collision.Shapes;
using Box2D.Common.Math;
using Box2D.Dynamics;
using FlashHeatZeeker.Core.Library;
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
            groundkarma_b2world,
            air_b2world;

        public IPhysicalUnit
            current;


        public ScriptCoreLib.ActionScript.flash.display.Sprite
            ground_dd,
            groundkarma_dd,
            air_dd;

        public Stopwatch physicstime = new Stopwatch();
        public double airzoom = 0.35;

        public List<IPhysicalUnit> internalunits = new List<IPhysicalUnit>();

        public IEnumerable<IPhysicalUnit> units
        {
            get { return internalunits; }
        }

        public StarlingGameSpriteWithPhysics()
        {


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


            #region ground_b2world
            // first frame  ... set up our physccs
            // zombies!!
            groundkarma_b2world = new b2World(new b2Vec2(0, 0), false);

            var groundkarma_b2debugDraw = new b2DebugDraw();

            groundkarma_dd = new ScriptCoreLib.ActionScript.flash.display.Sprite();
            groundkarma_dd.transform.colorTransform = new ColorTransform(0.0, 1.0, 0.0);





            groundkarma_b2debugDraw.SetSprite(groundkarma_dd);
            // textures are 512 pixels, while our svgs are 400px
            // so how big is a meter in our game world? :)
            groundkarma_b2debugDraw.SetDrawScale(16);
            groundkarma_b2debugDraw.SetFillAlpha(0.1);
            groundkarma_b2debugDraw.SetLineThickness(1.0);
            groundkarma_b2debugDraw.SetFlags(b2DebugDraw.e_shapeBit);

            groundkarma_b2world.SetDebugDraw(groundkarma_b2debugDraw);





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


                fixDef.shape = new Box2D.Collision.Shapes.b2CircleShape(4);


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


                fixDef.shape = new Box2D.Collision.Shapes.b2CircleShape(4);


                var fix = body.CreateFixture(fixDef);


                body.SetPosition(
                    new b2Vec2(8, -8)
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

                var body = groundkarma_b2world.CreateBody(bodyDef);
                body.SetPosition(new b2Vec2(10, 10));

                var fixDef = new Box2D.Dynamics.b2FixtureDef();
                fixDef.density = 0.1;
                fixDef.friction = 0.01;
                fixDef.restitution = 0;


                fixDef.shape = new Box2D.Collision.Shapes.b2CircleShape(4);


                var fix = body.CreateFixture(fixDef);


                body.SetPosition(
                    new b2Vec2(8, 8)
                );
            }
            #endregion


            physicstime.Start();

            this.onbeforefirstframe += (stage, s) =>
            {
                s.nativeOverlay.addChild(ground_dd);
                s.nativeOverlay.addChild(groundkarma_dd);
                s.nativeOverlay.addChild(air_dd);

                // 1000 / 15
                var syncframeinterval = 1000 / 15;
                var syncframesince = 0L;
                var syncframeextra = 0L;

                onframe +=
                    delegate
                    {
                        // drop frames
                        //if (frameid % 2 == 0)
                        //    return;

                        var physicstime_elapsed = physicstime.ElapsedMilliseconds + syncframeextra;
                        syncframeextra = 0;

                        // physicstime_elapsed needs to be split!

                        var physicstime_elapsed_PRE = physicstime_elapsed;




                        physicstime.Restart();

                        // add up the time we are spending
                        syncframesince += physicstime_elapsed;

                        #region raise_onsyncframe
                        var raise_onsyncframe = false;
                        // time for sync frame yet?
                        if (syncframesince >= syncframeinterval)
                        {
                            // time for syncframe!
                            raise_onsyncframe = true;

                            // does it actually help us? disabled for now
                            var dx = syncframesince - syncframeinterval;
                            syncframesince = 0;

                            // Error: raise_onsyncframe: { physicstime_elapsed_PRE = 275, dx = 75, physicstime_elapsed_POST = 0 }

                            physicstime_elapsed_PRE -= dx;
                            syncframeextra += dx;
                        }
                        #endregion


                        var iterations = 10;

                        syncframetime += physicstime_elapsed_PRE;

                        #region PRE Step

                        //update physics world
                        ground_b2world.Step(physicstime_elapsed_PRE / 1000.0, iterations, iterations);
                        groundkarma_b2world.Step(physicstime_elapsed_PRE / 1000.0, iterations, iterations);
                        air_b2world.Step(physicstime_elapsed_PRE / 1000.0, iterations, iterations);
                        #endregion

                        ground_b2world.ClearForces();
                        groundkarma_b2world.ClearForces();
                        air_b2world.ClearForces();


                        #region DrawDebugData ClearForces
                        ground_b2world.DrawDebugData();
                        groundkarma_b2world.DrawDebugData();
                        air_b2world.DrawDebugData();
                        #endregion

                        // syncframe
                        if (raise_onsyncframe)
                            this.onsyncframe(stage, s);


                        #region air_dd vs ground_dd
                        if (current != null)
                            if (current.body.GetWorld() == air_b2world)
                            {
                                air_dd.alpha = 0.6;
                                ground_dd.alpha = 0.1;
                            }
                            else
                            {
                                air_dd.alpha = 0.1;
                                ground_dd.alpha = 0.6;
                            }
                        #endregion

                        #region DisableDefaultContentDransformation
                        DisableDefaultContentDransformation = true;
                        {
                            var cm = new Matrix();

                            if (current != null)
                            {
                                cm.translate(
                                    -(current.body.GetPosition().x * 16),
                                    -(current.body.GetPosition().y * 16)
                                );



                                cm.rotate(-current.body.GetAngle() - Math.PI / 2 + current.CameraRotation);
                            }
                            //cm.rotate(-current.GetAngle());


                            stagescale = internalscale * (stage.stageWidth) / (800.0);


                            cm.scale(stagescale, stagescale);


                            cm.translate(
                                (stage.stageWidth * 0.5),
                                (stage.stageHeight * 0.8)
                            );


                            Content.transformationMatrix = cm;

                            ground_dd.transform.matrix = cm;
                            groundkarma_dd.transform.matrix = cm;
                            air_dd.transform.matrix = cm;
                        }
                        #endregion



                        foreach (var item in units)
                        {
                            item.ShowPositionAndAngle();

                            #region driverseat
                            if (item.driverseat != null)
                                if (item.driverseat.driver != null)
                                {
                                    var driver = item.driverseat.driver;

                                    driver.body.SetPositionAndAngle(

                                        new b2Vec2(

                                            item.body.GetPosition().x + Math.Cos(item.body.GetAngle() - Math.PI * 0.5 - item.CameraRotation) * 0.2,
                                            item.body.GetPosition().y + Math.Sin(item.body.GetAngle() - Math.PI * 0.5 - item.CameraRotation) * 0.2
                                        ),
                                        item.body.GetAngle() - item.CameraRotation
                                    );

                                    driver.ShowPositionAndAngle();
                                }
                            #endregion

                            item.ApplyVelocity();
                        }





                    };
            };


        }

        public long syncframetime;
    }
}
