using Box2D.Common.Math;
using Box2D.Dynamics;
using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.UnitJeepControl.Library;
using FlashHeatZeeker.UnitPed.Library;
using FlashHeatZeeker.UnitPedControl.Library;
using starling.display;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;

namespace FlashHeatZeeker.UnitPedControlTimetravel.Library
{


    class StarlingGameSpriteWithPedControlTimetravel : StarlingGameSpriteWithPhysics
    {

        public StarlingGameSpriteWithPedControlTimetravel()
        {
            var textures = new StarlingGameSpriteWithPedTextures(new_tex_crop);



            this.onbeforefirstframe += (stage, s) =>
            {


                // 1000 / 15
                // this means 15 samples per second
                var ilag = 7;

                #region man_with_lag
                var man_with_lag = new PhysicalPed(textures, this);

                //karmaman.body.SetActive(false);

                for (int i = 0; i < ilag; i++)
                {
                    man_with_lag.KarmaInput4.Enqueue(
                        new KeySample()
                    );
                }

                man_with_lag.body.SetPosition(
                     new b2Vec2(-8, 8)
                 );

                man_with_lag.karmabody.SetPosition(
                     new b2Vec2(-8, 8)
                 );
                #endregion


                #region man_with_karma
                var man_with_karma = new PhysicalPed(textures, this);

                man_with_karma.TeleportTo(-8, -8);



                #endregion



                var physical0 = new PhysicalPed(textures, this);
                current = physical0;

                // 32x32 = 15FPS?
                // 24x24 35?

                #region the others
                for (int ix = 1; ix < 4; ix++)
                    for (int iy = 1; iy < 4; iy++)
                    {
                        var p = new PhysicalPed(textures, this);

                        p.TeleportTo(
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

                bool mode_changepending = false;

                var man_with_karma_karmavisuals = new Queue<VisualPed>();
                var physical0_karmavisuals = new Queue<VisualPed>();
                var karmasw = new Stopwatch();
                karmasw.Start();



                //var inputfilter = new Stopwatch();
                //inputfilter.Start();

                onsyncframe += delegate
                {
                    physical0.SetVelocityFromInput(__keyDown);

                    man_with_karma.SetVelocityFromInput(__keyDown);

                    man_with_lag.KarmaInput4.Enqueue(new KeySample { value = __keyDown.value });
                    var physical2_karmastream_keydown = man_with_lag.KarmaInput4.Dequeue();

                    // this one lives in the past?
                    man_with_lag.SetVelocityFromInput(physical2_karmastream_keydown);


                    #region karmavisuals
                    if (karmasw.ElapsedMilliseconds > (1000 / 15))
                    {
                        karmasw.Restart();

                        {
                            physical0_karmavisuals.Enqueue(physical0.visual);

                            if (physical0_karmavisuals.Count > ilag)
                            {
                                physical0_karmavisuals.Dequeue().Orphanize();
                            }

                            physical0.visual.shadow.visible = false;
                            physical0.visual.currentvisual.alpha = 0.2;

                            physical0.visual = new VisualPed(textures, this, physical0.visual.AnimateSeed)
                            {
                                LayOnTheGround = physical0.visual.LayOnTheGround
                            };

                            physical0.ShowPositionAndAngle();
                        }


                        {
                            man_with_karma_karmavisuals.Enqueue(man_with_karma.visual);

                            if (man_with_karma_karmavisuals.Count > 4)
                            {
                                man_with_karma_karmavisuals.Dequeue().Orphanize();
                            }

                            man_with_karma.visual.shadow.visible = false;
                            man_with_karma.visual.currentvisual.alpha = 0.2;

                            man_with_karma.visual = new VisualPed(textures, this, man_with_karma.visual.AnimateSeed)
                            {
                                LayOnTheGround = man_with_karma.visual.LayOnTheGround
                            };

                            man_with_karma.ShowPositionAndAngle();
                        }
                    }
                    #endregion


                    foreach (var item in units)
                    {
                        (item as PhysicalPed).With(ped => ped.FeedKarma());

                    }
                };

                onframe += delegate
                {
                    this.Text = new
                    {
                        da = (man_with_lag.body.GetAngle() - physical0.body.GetAngle()).RadiansToDegrees(),
                        dx = man_with_lag.body.GetPosition().x - physical0.body.GetPosition().x,
                        dy = man_with_lag.body.GetPosition().y - physical0.body.GetPosition().y
                    }.ToString();




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
                    if (__keyDown[Keys.Space])
                    {
                        // space is not down.
                        mode_changepending = true;
                    }
                    else
                    {
                        if (mode_changepending)
                        {
                            if (physical0.visual.LayOnTheGround)
                                physical0.visual.LayOnTheGround = false;
                            else
                                physical0.visual.LayOnTheGround = true;

                            mode_changepending = false;



                        }
                    }
                    #endregion



                };


            };



        }
    }
}
