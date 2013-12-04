using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.UnitBunkerControl.Library;
using FlashHeatZeeker.UnitPed.Library;
using FlashHeatZeeker.UnitPedControl.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Lambda;
using FlashHeatZeeker.UnitJeepControl.Library;
using FlashHeatZeeker.Core.Library;
using ScriptCoreLib.Shared.BCLImplementation.GLSL;
using FlashHeatZeeker.CoreAudio.Library;
using ScriptCoreLib.ActionScript.flash.media;
using starling.display;
using ScriptCoreLib.ActionScript.flash.geom;

namespace FlashHeatZeeker.Shop.Library
{
    class StarlingGameSpriteWithShop : StarlingGameSpriteWithPhysics
    {
        public static KeySample __keyDown = new KeySample();

        public Soundboard sb = new Soundboard();


        public static event Action<IPhysicalUnit> ShopEnter;
        public static event Action ShopExit;

        public StarlingGameSpriteWithShop()
        {
            var textures_bunker = new StarlingGameSpriteWithBunkerTextures(this.new_tex_crop);
            var textures_ped = new StarlingGameSpriteWithPedTextures(this.new_tex_crop, this.new_texsprite_crop);

            this.disablephysicsdiagnostics = true;

            this.onbeforefirstframe += (stage, s) =>
            {
                s.stage.color = 0xB27D51;
                current = new PhysicalPed(textures_ped, this)
                {

                    AttractZombies = true
                };

                current.SetPositionAndAngle(
                    16.Random(),
                    16.Random(),
                    360.Random().DegreesToRadians()
                );


                #region others
                for (int ix = 0; ix < 2; ix++)
                    for (int iy = 0; iy < 2; iy++)
                    {
                        var p = new PhysicalPed(textures_ped, this);

                        p.SetPositionAndAngle(
                            8 * ix, 8 * iy, random.NextDouble()
                        );

                        p.BehaveLikeZombie();
                    }
                #endregion

                var myshop = new PhysicalBunker(textures_bunker, this, IsShop: true);

                myshop.SetPositionAndAngle(
                          -8, -8
                      );



                #region __keyDown

                stage.keyDown +=
                   e =>
                   {
                       __keyDown.forcex = 1.0;
                       __keyDown.forcey = 1.0;

                       // http://circlecube.com/2008/08/actionscript-key-listener-tutorial/
                       if (e.altKey)
                           __keyDown[System.Windows.Forms.Keys.Alt] = true;

                       __keyDown[(System.Windows.Forms.Keys)e.keyCode] = true;
                   };

                stage.keyUp +=
                 e =>
                 {
                     if (!e.altKey)
                         __keyDown[System.Windows.Forms.Keys.Alt] = false;

                     __keyDown[(System.Windows.Forms.Keys)e.keyCode] = false;
                 };

                #endregion



                bool entermode_changepending = false;


                onsyncframe +=
                    delegate
                    {
                        current.SetVelocityFromInput(__keyDown);

                        #region Enter
                        if (!__keyDown[System.Windows.Forms.Keys.Enter])
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

                                            //if (current is PhysicalJeep)
                                            //{
                                            //    sb.snd_jeepengine_start.play();
                                            //}

                                            //else 
                                            if (current is PhysicalBunker)
                                            {
                                                if ((current as PhysicalBunker).visual_shopoverlay.visible)
                                                {
                                                    sb.snd_its_a_shop.play();
                                                    if (ShopEnter != null)
                                                        ShopEnter(candidatedriver);
                                                }
                                                else
                                                {
                                                    if (random.NextDouble() > 0.5)
                                                    {
                                                        sb.snd_itsempty.play();
                                                    }
                                                    else
                                                    {
                                                        sb.snd_nothinghere.play();
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                sb.snd_dooropen.play(
                                                  sndTransform: new SoundTransform(
                                                     0.3
                                                  )
                                               );
                                            }

                                            //if (current is PhysicalHind)
                                            //{
                                            //    nightvision_on();
                                            //}

                                            //hud_update();


                                            //switchto(x.x);
                                            move_zoom = 1;

                                            // fast start
                                            //(current as PhysicalHind).With(
                                            //    hind => hind.VerticalVelocity = 1
                                            //);
                                        }
                                    );
                                }
                                else
                                {
                                    (current.driverseat.driver as PhysicalPed).With(
                                        driver =>
                                        {
                                            // get out of the lift..

                                            //nightvision_off();

                                            current.driverseat.driver = null;
                                            driver.seatedvehicle = null;
                                            current.SetVelocityFromInput(new KeySample());

                                            if (current is PhysicalBunker)
                                            {
                                                if ((current as PhysicalBunker).visual_shopoverlay.visible)
                                                {
                                                    if (ShopExit != null)
                                                        ShopExit();
                                                }
                                            }
                                            // crashland?
                                            //(current as PhysicalHind).With(
                                            //    hind =>
                                            //    {
                                            //        if (hind.visual.Altitude > 0)
                                            //        {
                                            //            hind.VerticalVelocity = -1;
                                            //            sb.snd_touchdown.play();
                                            //        }
                                            //    }

                                            //);

                                            sb.snd_dooropen.play(
                                              sndTransform: new SoundTransform(
                                                 0.3
                                              )
                                           );
                                            //if (current.body.GetType() != Box2D.Dynamics.b2Body.b2_dynamicBody)
                                            //{
                                            //    sb.snd_letsgo.play();
                                            //}

                                            current = driver;
                                            driver.body.SetActive(true);
                                            driver.body.SetAngularVelocity(-11);

                                            //hud_update();
                                            move_zoom = 1;
                                        }
                                    );
                                }

                            }
                        }
                        #endregion

                    };
            };

        }
    }
}
