using Box2D.Common.Math;
using Box2D.Dynamics;
using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.StarlingSetup.Library;
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
                var units = new List<PhyscalPed>();

                var physical0 = new PhyscalPed(textures, this);
                units.Add(physical0);

                // 32x32 = 15FPS?
                // 24x24 35?

                for (int ix = 0; ix < 4; ix++)
                    for (int iy = 0; iy < 4; iy++)
                    {
                        var p = new PhyscalPed(textures, this);

                        p.current.SetPosition(
                            new b2Vec2(8 * ix, 8 * iy)
                        );

                        units.Add(p);

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


                var unitsa = units.ToArray();

                onframe += delegate
                {
                    // 15 FPS
                    // 20 FPS
                    foreach (var item in unitsa)
                    {
                        item.ShowPositionAndAngle();
                        item.ApplyVelocity();
                    }


                    current = physical0.current;
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




                };


            };



        }
    }
}
