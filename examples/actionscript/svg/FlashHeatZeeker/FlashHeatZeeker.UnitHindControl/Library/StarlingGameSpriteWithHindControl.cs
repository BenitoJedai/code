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

    public class StarlingGameSpriteWithHindControl : StarlingGameSpriteWithPhysics
    {
        public static object[] __keyDown = new object[0xffffff];


        public StarlingGameSpriteWithHindControl()
        {
            // how much bigger are units in flight altidude?

            var textures = new StarlingGameSpriteWithHindTextures(this.new_tex_crop);


            this.onbeforefirstframe += (stage, s) =>
            {

                var units = new List<PhysicalHind>();

                var physical0 = new PhysicalHind(textures, this);
                units.Add(physical0);


                for (int ix = 0; ix < 32; ix++)
                {
                    var physical1 = new PhysicalHind(textures, this);
                    var physical2 = new PhysicalHind(textures, this);
                    var physical3 = new PhysicalHind(textures, this);

                    physical1.current.SetPosition(new b2Vec2(10 + 20 * ix, 20));
                    units.Add(physical1);

                    physical2.visual.Altitude = 1.0;
                    physical2.current.SetPosition(new b2Vec2(20 + 20 * ix, 40));
                    units.Add(physical2);

                    physical3.visual.Altitude = 0.5;
                    physical3.current.SetPosition(new b2Vec2(30 + 20 * ix, 60));
                    units.Add(physical3);
                }





                bool flightmode_changepending = false;
                //bool visual0_flightmode = false;

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


                onframe +=
                    delegate
                    {
                        units.WithEach(
                            unit =>
                            {
                                unit.ShowPositionAndAngle();
                                unit.ApplyVelocity();
                            }
                        );




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
                                if (physical0.visual.Altitude == 0)
                                    physical0.VerticalVelocity = 1.0;
                                else
                                    physical0.VerticalVelocity = -0.4;

                                flightmode_changepending = false;



                            }
                        }
                        #endregion

                        // for camera
                        this.current = physical0.current;

                        physical0.SetVelocityFromInput(__keyDown);




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

                                var body = physical0.current.GetWorld().CreateBody(bodyDef);
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
