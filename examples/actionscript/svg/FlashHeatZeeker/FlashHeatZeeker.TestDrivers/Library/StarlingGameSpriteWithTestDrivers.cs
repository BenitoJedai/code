﻿using Box2D.Common.Math;
using Box2D.Dynamics;
using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.StarlingSetup.Library;
using FlashHeatZeeker.UnitCannon.Library;
using FlashHeatZeeker.UnitCannonControl.Library;
using FlashHeatZeeker.UnitHind.Library;
using FlashHeatZeeker.UnitHindControl.Library;
using FlashHeatZeeker.UnitJeep.Library;
using FlashHeatZeeker.UnitJeepControl.Library;
using FlashHeatZeeker.UnitPed.Library;
using FlashHeatZeeker.UnitPedControl.Library;
using FlashHeatZeeker.UnitTank.Library;
using FlashHeatZeeker.UnitTankControl.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Shared.BCLImplementation.GLSL;
using FlashHeatZeeker.UnitBunkerControl.Library;
using FlashHeatZeeker.CoreMap.Library;
using starling.display;

namespace FlashHeatZeeker.TestDrivers.Library
{
    public class StarlingGameSpriteWithTestDrivers : StarlingGameSpriteWithPhysics
    {
        public static object[] __keyDown = new object[0xffffff];


        public StarlingGameSpriteWithTestDrivers()
        {
            var textures_ped = new StarlingGameSpriteWithPedTextures(this.new_tex_crop);
            var textures_hind = new StarlingGameSpriteWithHindTextures(this.new_tex_crop);
            var textures_jeep = new StarlingGameSpriteWithJeepTextures(this.new_tex_crop);
            var textures_tank = new StarlingGameSpriteWithTankTextures(this.new_tex_crop);
            var textures_cannon = new StarlingGameSpriteWithCannonTextures(this.new_tex_crop);
            var textures_bunker = new StarlingGameSpriteWithBunkerTextures(this.new_tex_crop);
            var textures_map = new StarlingGameSpriteWithMapTextures(new_tex_crop);

            this.disablephysicsdiagnostics = true;

            this.onbeforefirstframe += (stage, s) =>
            {
                for (int i = 0; i < 64; i++)
                    new Image(textures_map.hill1()).AttachTo(Content).With(
                        hill =>
                        {
                            hill.x = 2048.Random();
                            hill.y = 2048.Random();
                        }
                    );

                for (int i = -8; i < 8; i++)
                {
                    new Image(textures_map.road0()).AttachTo(Content).x = 256 * i;
                }

                new Image(textures_map.touchdown()).AttachTo(Content).y = 256;

                new Image(textures_map.tree0_shadow()).AttachTo(Content).y = 128 + 16;
                new Image(textures_map.tree0()).AttachTo(Content).y = 128;

                // can I have 
                // new ped, hind, jeep, tank
                var ped = new PhysicalPed(textures_ped, this);


                // 12 = 34FPS
                for (int i = 3; i < 9; i++)
                {
                    var cannon2 = new PhysicalCannon(textures_cannon, this);

                    cannon2.SetPositionAndAngle(
                        i * 16, -32, random.NextDouble()
                    );



                    if (i % 3 == 0)
                    {
                        new PhysicalBunker(textures_bunker, this).SetPositionAndAngle(
                            i * 16, -16, random.NextDouble()
                        );
                    }
                    else if (i % 3 == 1)
                    {
                        new PhysicalWatertower(textures_bunker, this).SetPositionAndAngle(
                            i * 16, -16, random.NextDouble()
                        );
                    }
                    else
                    {
                        new PhysicalSilo(textures_bunker, this).SetPositionAndAngle(
                            i * 16, -16, random.NextDouble()
                        );
                    }

                    var hind2 = new PhysicalHind(textures_hind, this)
                    {
                        AutomaticTakeoff = true
                    };

                    hind2.SetPositionAndAngle(
                        i * 16, 8, random.NextDouble()
                    );


                    var jeep2 = new PhysicalJeep(textures_jeep, this);


                    jeep2.SetPositionAndAngle(
                        i * 16, 16, random.NextDouble()
                    );



                    var tank2 = new PhysicalTank(textures_tank, this);

                    tank2.SetPositionAndAngle(
                        i * 16, 24, random.NextDouble()
                    );


                    var ped2 = new PhysicalPed(textures_ped, this);

                    ped2.SetPositionAndAngle(
                        i * 16, 32, random.NextDouble()
                    );


                }

                current = ped;


                #region __keyDown
                var __keyDown = new KeySample();

                stage.keyDown +=
                   e =>
                   {
                       // http://circlecube.com/2008/08/actionscript-key-listener-tutorial/
                       if (e.altKey)
                           __keyDown[Keys.Alt] = true;

                       __keyDown[(Keys)e.keyCode] = true;
                   };

                stage.keyUp +=
                 e =>
                 {
                     if (!e.altKey)
                         __keyDown[Keys.Alt] = false;

                     __keyDown[(Keys)e.keyCode] = false;
                 };

                #endregion
                bool entermode_changepending = false;
                bool mode_changepending = false;


                onsyncframe +=
                    delegate
                    {


                        #region entermode_changepending
                        if (!__keyDown[Keys.Enter])
                        {
                            // space is not down.
                            entermode_changepending = true;
                        }
                        else
                        {
                            if (entermode_changepending)
                            {
                                entermode_changepending = false;

                                // enter another vehicle?

                                var candidatedriver = current as PhysicalPed;
                                if (candidatedriver != null)
                                {
                                    var target =
                                         from candidatevehicle in units
                                         where candidatevehicle.driverseat != null

                                         // can enter if the seat is full.
                                         // unless we kick them out before ofcourse
                                         where candidatevehicle.driverseat.driver == null

                                         let distance = new __vec2(
                                             (float)(candidatedriver.body.GetPosition().x - candidatevehicle.body.GetPosition().x),
                                             (float)(candidatedriver.body.GetPosition().y - candidatevehicle.body.GetPosition().y)
                                         ).GetLength()

                                         where distance < 6

                                         orderby distance ascending
                                         select new { candidatevehicle, distance };

                                    target.FirstOrDefault().With(
                                        x =>
                                        {
                                            Console.WriteLine(new { x.distance });

                                            //current.loc.visible = false;
                                            current.body.SetActive(false);


                                            x.candidatevehicle.driverseat.driver = candidatedriver;
                                            candidatedriver.seatedvehicle = x.candidatevehicle;

                                            current = x.candidatevehicle;
                                            //switchto(x.x);
                                        }
                                    );
                                }
                                else
                                {
                                    (current.driverseat.driver as PhysicalPed).With(
                                        driver =>
                                        {
                                            current.driverseat.driver = null;
                                            driver.seatedvehicle = null;
                                            current.SetVelocityFromInput(new KeySample());

                                            // crashland?
                                            (current as PhysicalHind).With(
                                                hind => hind.VerticalVelocity = -1
                                            );



                                            current = driver;
                                            current.body.SetActive(true);
                                        }
                                    );
                                }

                            }
                        }
                        #endregion



                        #region mode
                        if (!__keyDown[Keys.Space])
                        {
                            // space is not down.
                            mode_changepending = true;
                        }
                        else
                        {
                            if (mode_changepending)
                            {
                                (current as PhysicalHind).With(
                                    hind1 =>
                                    {
                                        if (hind1.visual.Altitude == 0)
                                            hind1.VerticalVelocity = 1.0;
                                        else
                                            hind1.VerticalVelocity = -0.4;

                                    }
                                );

                                (current as PhysicalPed).With(
                                 physical0 =>
                                 {
                                     if (physical0.visual.LayOnTheGround)
                                         physical0.visual.LayOnTheGround = false;
                                     else
                                         physical0.visual.LayOnTheGround = true;

                                 }
                             );




                                mode_changepending = false;



                            }
                        }
                        #endregion


                        current.SetVelocityFromInput(__keyDown);




                        #region simulate a weapone!
                        if (__keyDown[Keys.ControlKey])
                            if (frameid % 20 == 0)
                            {
                                var bodyDef = new b2BodyDef();

                                bodyDef.type = Box2D.Dynamics.b2Body.b2_dynamicBody;

                                // stop moving if legs stop walking!
                                bodyDef.linearDamping = 0;
                                bodyDef.angularDamping = 0;
                                //bodyDef.angle = 1.57079633;
                                bodyDef.fixedRotation = true;

                                var body = current.body.GetWorld().CreateBody(bodyDef);
                                body.SetPosition(
                                    new b2Vec2(
                                        current.body.GetPosition().x + 2,
                                        current.body.GetPosition().y + 2
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
                    };
            };
        }
    }
}
