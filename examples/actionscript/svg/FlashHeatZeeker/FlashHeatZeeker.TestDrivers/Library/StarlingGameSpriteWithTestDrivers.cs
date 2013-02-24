using Box2D.Common.Math;
using Box2D.Dynamics;
using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.StarlingSetup.Library;
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

            this.onbeforefirstframe += (stage, s) =>
            {

                // can I have 
                // new ped, hind, jeep, tank
                var ped = new PhysicalPed(textures_ped, this);

                var hind1 = new PhysicalHind(textures_hind, this)
                {
                    AutomaticTakeoff = true
                };

                var jeep1 = new PhysicalJeep(textures_jeep, this)
                {
                };

                var tank1 = new PhysicalTank(textures_tank, this)
                {
                };

                // 12 = 34FPS
                for (int i = 0; i < 4; i++)
                {
                    var hind2 = new PhysicalHind(textures_hind, this);

                    hind2.current.SetPositionAndAngle(
                        new b2Vec2(i * 16, 8), random.NextDouble()
                    );


                    var jeep2 = new PhysicalJeep(textures_jeep, this);

                    jeep2.unit4_physics.body.SetPositionAndAngle(
                        new b2Vec2(i * 16, 16), random.NextDouble()
                    );



                    var tank2 = new PhysicalTank(textures_tank, this);

                    tank2.body.SetPositionAndAngle(
                        new b2Vec2(i * 16, 24), random.NextDouble()
                    );


                    var ped2 = new PhysicalPed(textures_ped, this);

                    ped2.body.SetPositionAndAngle(
                        new b2Vec2(i * 16, 32), random.NextDouble()
                    );


                }

                IPhysicalUnit currentunit = ped;


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

                bool entermode_changepending = false;
                bool flightmode_changepending = false;


                onframe +=
                    delegate
                    {


                        #region entermode_changepending
                        if (__keyDown[(int)Keys.Enter] == null)
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

                                if (currentunit == ped)
                                {
                                    currentunit = hind1;
                                }
                                else if (currentunit == hind1)
                                {
                                    currentunit = jeep1;
                                }
                                else if (currentunit == jeep1)
                                {
                                    currentunit = tank1;
                                }
                                else
                                {
                                    currentunit = ped;
                                }
                            }
                        }
                        #endregion

                        if (currentunit == ped)
                        {
                            current = ped.body;
                        }
                        else if (currentunit == jeep1)
                        {
                            current = jeep1.unit4_physics.body;
                        }
                        else if (currentunit == tank1)
                        {
                            current = tank1.body;
                        }
                        else
                        {
                            current = hind1.current;
                        }
                        #region flightmode
                        if (__keyDown[(int)Keys.Space] == null)
                        {
                            // space is not down.
                            flightmode_changepending = true;
                        }
                        else
                        {
                            if (flightmode_changepending)
                            {
                                if (hind1.visual.Altitude == 0)
                                    hind1.VerticalVelocity = 1.0;
                                else
                                    hind1.VerticalVelocity = -0.4;

                                flightmode_changepending = false;



                            }
                        }
                        #endregion


                        currentunit.SetVelocityFromInput(__keyDown);




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

                                var body = current.GetWorld().CreateBody(bodyDef);
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
                    };
            };
        }
    }
}
