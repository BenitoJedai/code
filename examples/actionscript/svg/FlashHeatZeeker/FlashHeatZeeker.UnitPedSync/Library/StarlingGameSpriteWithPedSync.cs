using Box2D.Common.Math;
using Box2D.Dynamics;
using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.UnitPed.Library;
using FlashHeatZeeker.UnitPedControl.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;

namespace FlashHeatZeeker.UnitPedSync.Library
{




    class StarlingGameSpriteWithPedSync : StarlingGameSpriteWithPhysics
    {
        // one game per flash vm, for now.. :)
        public static Action<string> __raise_sync = delegate { };
        public static Action<string> __at_sync = delegate { };



        public static SetVelocityFromInputAction __raise_SetVelocityFromInput = delegate { };
        public static SetVelocityFromInputAction __at_SetVelocityFromInput = delegate { };



        public List<RemoteGame> others = new List<RemoteGame>();


        public StarlingGameSpriteWithPedSync()
        {
            var textures = new StarlingGameSpriteWithPedTextures(new_tex_crop);


            this.onbeforefirstframe += (stage, s) =>
            {
                #region :ego
                var ego = new PhysicalPed(textures, this)
                {
                    Identity = egoid + ":ego"
                };

                ego.SetPositionAndAngle(
                    random.NextDouble() * -8,
                    random.NextDouble() * -8,
                    random.NextDouble() * Math.PI
                );

                current = ego;
                #endregion

                // 32x32 = 15FPS?
                // 24x24 35?

                #region others
                for (int ix = 2; ix < 4; ix++)
                    for (int iy = 2; iy < 4; iy++)
                    {
                        var p = new PhysicalPed(textures, this);

                        p.SetPositionAndAngle(
                            8 * ix, 8 * iy
                        );


                    }
                #endregion


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

                #region other
                Func<string, RemoteGame> other = __egoid =>
                {
                    // that other game has sent us a sync frame!

                    var already_known_other = others.FirstOrDefault(k => k.__egoid == __egoid);

                    if (already_known_other == null)
                    {
                        already_known_other = new RemoteGame
                        {
                            __egoid = __egoid,

                            // this
                            __syncframeid = this.syncframeid
                        };

                        others.Add(already_known_other);
                    }


                    return already_known_other;
                };
                #endregion


                #region __at_sync
                __at_sync += __egoid =>
                {
                    // that other game has sent us a sync frame!

                    var o = other(__egoid);

                    o.__syncframeid++;
                    // move on!
                };
                #endregion


                #region __at_SetVelocityFromInput
                __at_SetVelocityFromInput +=
                    (
                        string __egoid,
                        string __identity,
                        string __KeySample,
                        string __fixup_x,
                        string __fixup_y,
                        string __fixup_angle

                        ) =>
                    {
                        var o = other(__egoid);



                        if (o.ego == null)
                        {
                            o.ego = new PhysicalPed(textures, this)
                            {
                                Identity = __identity,
                                RemoteGameReference = o
                            };

                            o.ego.SetPositionAndAngle(
                                double.Parse(__fixup_x),
                                double.Parse(__fixup_y),
                                double.Parse(__fixup_angle)
                            );
                        }


                        // set the input!


                        o.ego.SetVelocityFromInput(
                            new KeySample
                            {
                                value = int.Parse(__KeySample),

                                fixup = true,

                                x = double.Parse(__fixup_x),
                                y = double.Parse(__fixup_y),
                                angle = double.Parse(__fixup_angle)

                            }
                        );

                    };
                #endregion


                bool mode_changepending = false;
                onsyncframe += delegate
                {
                    // sync me!

                    // tell others in the session about our game
                    // a beacon


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

                            var body = ground_b2world.CreateBody(bodyDef);
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
                            if (ego.visual.LayOnTheGround)
                                ego.visual.LayOnTheGround = false;
                            else
                                ego.visual.LayOnTheGround = true;

                            mode_changepending = false;



                        }
                    }
                    #endregion



                    ego.SetVelocityFromInput(__keyDown);

                    __raise_SetVelocityFromInput(
                        "" + egoid,
                        ego.Identity,
                        "" + ego.CurrentInput.value,
                        "" + ego.body.GetPosition().x,
                        "" + ego.body.GetPosition().y,
                        "" + ego.body.GetAngle()

                    );


                    // tell others this sync frame ended for us

                    //Console.WriteLine("before __raise_sync");
                    __raise_sync("" + egoid);
                    //Console.WriteLine("after __raise_sync");

                };





            };



        }
    }
}
