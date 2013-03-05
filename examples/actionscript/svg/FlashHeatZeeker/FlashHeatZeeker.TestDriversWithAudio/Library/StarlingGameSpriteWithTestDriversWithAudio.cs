using Box2D.Common.Math;
using Box2D.Dynamics;
using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CoreAudio.Library;
using FlashHeatZeeker.CoreMap.Library;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.StarlingSetup.Library;
using FlashHeatZeeker.UnitBunkerControl.Library;
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
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.media;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.BCLImplementation.GLSL;
using ScriptCoreLib.Shared.Lambda;
using starling.display;
using starling.filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;


namespace FlashHeatZeeker.TestDriversWithAudio.Library
{
    public class StarlingGameSpriteWithTestDriversWithAudio : StarlingGameSpriteWithPhysics
    {
        public static KeySample __keyDown = new KeySample();

        public static int HudPadding = 0;

        public Soundboard sb = new Soundboard();

        public Action hud_update;
        public Action nightvision_on;
        public Action nightvision_off;

        public bool disable_enter_and_space;

        public StarlingGameSpriteWithPedTextures
            textures_ped;

        public StarlingGameSpriteWithTestDriversWithAudio()
        {
            // http://www.mochigames.com/game/gunship_v838523/

            textures_ped = new StarlingGameSpriteWithPedTextures(this.new_tex_crop);


            var textures_map = new StarlingGameSpriteWithMapTextures(new_tex_crop);

            var textures_jeep = new StarlingGameSpriteWithJeepTextures(this.new_tex_crop);

            var textures_hind = new StarlingGameSpriteWithHindTextures(this.new_tex_crop);
            var textures_tank = new StarlingGameSpriteWithTankTextures(this.new_tex_crop);
            var textures_cannon = new StarlingGameSpriteWithCannonTextures(this.new_tex_crop);
            var textures_bunker = new StarlingGameSpriteWithBunkerTextures(this.new_tex_crop);

            this.disablephysicsdiagnostics = true;

            this.onbeforefirstframe += (stage, s) =>
            {
                s.stage.color = 0xB27D51;

                // error JSC1000: ActionScript : failure at starling.display.Stage.add_keyUp : Object reference not set to an instance of an object.

                #region FULL_SCREEN_INTERACTIVE
                stage.keyUp +=
                     e =>
                     {
                         if (e.keyCode == (uint)System.Windows.Forms.Keys.F2)
                         {
                             this.Content_layer2_shadows.visible =
                                !this.Content_layer2_shadows.visible;
                         }

                     };
                #endregion

                #region FULL_SCREEN_INTERACTIVE
                stage.keyUp +=
                     e =>
                     {
                         if (e.keyCode == (uint)System.Windows.Forms.Keys.F1)
                         {
                             if (this.internalscale == 0.3)
                                 this.internalscale = 0.05;
                             else
                                 this.internalscale = 0.3;
                         }

                     };
                #endregion

                var hud = new Image(textures_ped.hud_look()).AttachTo(this);

                #region hill1
                for (int i = 0; i < 32; i++)
                {
                    new Image(textures_map.hill1()).AttachTo(Content_layer0_tracks).With(
                        hill =>
                        {
                            hill.x = 2048.Random();
                            hill.y = 2048.Random() - 1024;
                        }
                    );

                    new Image(textures_map.hole1()).AttachTo(Content_layer0_tracks).With(
                        hill =>
                        {
                            hill.x = 2048.Random();
                            hill.y = 2048.Random() - 1024;
                        }
                    );

                    new Image(textures_map.grass1()).AttachTo(Content_layer0_tracks).With(
                        hill =>
                        {
                            hill.x = 2048.Random();

                            var y = -2048.Random() - 512 - 256;
                            hill.y = y;
                        }
                    );
                }
                #endregion

                for (int i = -12; i < 12; i++)
                {
                    new Image(textures_map.road0()).AttachTo(Content_layer0_tracks).x = 256 * i;
                }

                #region other units
                for (int i = 3; i < 7; i++)
                {
                    {
                        new PhysicalCannon(textures_cannon, this).SetPositionAndAngle(
                            i * 16, -20, -random.NextDouble()
                        );

                        new PhysicalCannon(textures_cannon, this).SetPositionAndAngle(
                            i * 16, 36, random.NextDouble()
                        );
                    }


                    if (i % 3 == 0)
                    {
                        new PhysicalBunker(textures_bunker, this).SetPositionAndAngle(
                            i * 16, -8, random.NextDouble()
                        );

                        new PhysicalBunker(textures_bunker, this).SetPositionAndAngle(
                            i * 16, 24, random.NextDouble()
                        );


                        var ibunker = new PhysicalBunker(textures_bunker, this);

                        ibunker.SetPositionAndAngle(
                            i * 16, 64, random.NextDouble()
                        );

                        ibunker.visualshadow.Orphanize(); //.AttachTo(this.Content_layer10_hiddenforgoggles);
                        ibunker.visual.Orphanize().AttachTo(this.Content_layer10_hiddenforgoggles);

                    }
                    else if (i % 3 == 1)
                    {


                        new PhysicalBarrel(textures_bunker, this).SetPositionAndAngle(i * 16, -4 - 3);
                        new PhysicalBarrel(textures_bunker, this).SetPositionAndAngle(i * 16 + 2, -4);
                        new PhysicalBarrel(textures_bunker, this).SetPositionAndAngle(i * 16, -4 + 3);

                        new PhysicalWatertower(textures_bunker, this).SetPositionAndAngle(
                            i * 16, 16, random.NextDouble()
                        );

                        new PhysicalWatertower(textures_bunker, this).SetPositionAndAngle(
                            i * 16 - 4, 16 + 4, random.NextDouble()
                        );

                        new PhysicalWatertower(textures_bunker, this).SetPositionAndAngle(
                            i * 16 + 4, 16 + 4, random.NextDouble()
                        );
                    }
                    else
                    {
                        new PhysicalBarrel(textures_bunker, this).SetPositionAndAngle(i * 16, 24 - 3);
                        new PhysicalBarrel(textures_bunker, this).SetPositionAndAngle(i * 16 + 2, 24);
                        new PhysicalBarrel(textures_bunker, this).SetPositionAndAngle(i * 16, 24 + 3);

                        new PhysicalSilo(textures_bunker, this).SetPositionAndAngle(
                            i * 16, -4, random.NextDouble()
                        );
                    }

                }
                #endregion



                new PhysicalBarrel(textures_bunker, this).SetPositionAndAngle(12, 0);
                new PhysicalBarrel(textures_bunker, this).SetPositionAndAngle(12, -4);
                new PhysicalBarrel(textures_bunker, this).SetPositionAndAngle(12, -8);



                new Image(textures_map.touchdown()).AttachTo(Content).MoveTo(256, -256);
                new Image(textures_map.touchdown()).AttachTo(Content).y = 256;

                new PhysicalTank(textures_tank, this).SetPositionAndAngle(128 / 16, 128 * 3 / 16);

                new Image(textures_map.tree0_shadow()).AttachTo(Content).y = 128 + 16;
                new Image(textures_map.tree0()).AttachTo(Content).y = 128;

                // can I have 
                // new ped, hind, jeep, tank
                current = new PhysicalPed(textures_ped, this);
                current.SetPositionAndAngle(
                    16.Random(),
                    16.Random(),

                    360.Random().DegreesToRadians()
                );

                var jeep2 = new PhysicalJeep(textures_jeep, this);

                jeep2.SetPositionAndAngle(
                    16, 16, random.NextDouble()
                );

                var jeep3 = new PhysicalJeep(textures_jeep, this);


                jeep3.visual0.shadow.Orphanize(); //.AttachTo(this.Content_layer10_hiddenforgoggles);
                jeep3.visual0.currentvisual.Orphanize().AttachTo(this.Content_layer10_hiddenforgoggles);

                jeep3.SetPositionAndAngle(
                    -16, 16, random.NextDouble()
                );



                #region tree0
                for (int i = 0; i < 128; i++)
                {

                    {
                        var x = 2048.Random();
                        var y = -2048.Random() - 512 - 256;

                        new Image(textures_map.tree0_shadow()).AttachTo(Content_layer2_shadows).MoveTo(x + 16, y + 16);
                        new Image(textures_map.tree0()).AttachTo(Content).MoveTo(x, y);
                    }

                    {
                        var x = 2048.Random();
                        var y = 2048.Random() + 512 + 128;

                        new Image(textures_map.tree0_shadow()).AttachTo(Content_layer2_shadows).MoveTo(x + 16, y + 16);
                        new Image(textures_map.tree0()).AttachTo(Content).MoveTo(x, y);
                    }
                }
                #endregion

                // 12 = 34FPS

                new PhysicalHind(textures_hind, this)
                {
                    AutomaticTakeoff = true
                }.SetPositionAndAngle((128 + 256) / 16, -128 / 16);


                #region __keyDown

                stage.keyDown +=
                   e =>
                   {
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

                bool nightvision_changepending = false;
                bool entermode_changepending = false;
                bool mode_changepending = false;

                onframe +=
                    delegate
                    {
                        //var v = current.body.GetLinearVelocity();

                        //Text = new { move_zoom, v.x, v.y }.ToString();

                        #region hud
                        {
                            var cm = new Matrix();

                            cm.scale(0.5, 0.5);
                            cm.translate(
                                16 + HudPadding,
                                stage.stageHeight - 64 - 24);

                            hud.transformationMatrix = cm;
                        }
                        #endregion
                    };

                // ego + local environment
                #region Soundboard
                // http://www.nasa.gov/vision/universe/features/halloween_sounds.html

                sb.loopsand_run.MasterVolume = 0;
                sb.loopsand_run.Sound.play();

                sb.loophelicopter1.MasterVolume = 0;
                sb.loophelicopter1.Sound.play();

                sb.loopjeepengine.MasterVolume = 0;
                sb.loopjeepengine.Sound.play();

                sb.loopdiesel2.MasterVolume = 0;
                sb.loopdiesel2.Sound.play();

                sb.loopcrickets.MasterVolume = 0;
                sb.loopcrickets.Sound.play();

                sb.loopstrange1.MasterVolume = 0;
                sb.loopstrange1.Sound.play();


                var jeep_forceA = 0.0;
                var ped_forceA = 0.0;
                var barrel_forceA = 0.0;

                var hardmetal_forceA = 0.0;

                this.oncollision +=
                    (u, force) =>
                    {
                        if (u is PhysicalTank)
                            hardmetal_forceA = hardmetal_forceA.Max(force);
                        if (u is PhysicalSilo)
                            hardmetal_forceA = hardmetal_forceA.Max(force);
                        if (u is PhysicalBunker)
                            hardmetal_forceA = hardmetal_forceA.Max(force);
                        if (u is PhysicalWatertower)
                            hardmetal_forceA = hardmetal_forceA.Max(force);
                        if (u is PhysicalCannon)
                            hardmetal_forceA = hardmetal_forceA.Max(force);
                    };

                PhysicalJeep.oncollision +=
                    (u, value) =>
                    {
                        jeep_forceA = jeep_forceA.Max(value);

                    };

                PhysicalPed.oncollision +=
                     (u, value) =>
                     {
                         ped_forceA = ped_forceA.Max(value);
                     };

                PhysicalBarrel.oncollision +=
                  (u, value) =>
                  {
                      barrel_forceA = barrel_forceA.Max(value);
                  };
                #endregion

                var nightvision_filter = new ColorMatrixFilter();
                var nightvision_filter_age = new Stopwatch();
                nightvision_filter_age.Start();
                Action nighvision_handler = null;

                bool nightvision_mode = false;

                #region hud_update
                hud_update = delegate
               {
                   if (nightvision_mode)
                   {

                       hud.texture = textures_ped.hud_look_goggles();
                       return;
                   }

                   if (current is PhysicalPed)
                   {
                       hud.texture = textures_ped.hud_look();
                   }
                   else if (current == jeep3)
                   {
                       hud.texture = textures_ped.hud_look_onlygoggles();
                   }
                   else
                   {
                       if (current.body.GetType() == Box2D.Dynamics.b2Body.b2_dynamicBody)
                       {
                           hud.texture = textures_ped.hud_look_goggles();
                       }
                       else
                       {
                           hud.texture = textures_ped.hud_look_building();
                       }
                   }
               };
                #endregion



                #region nightvision_mode
                #region nightvision_on
                nightvision_on = delegate
               {
                   if (nightvision_mode)
                       return;

                   nightvision_mode = true;
                   hud_update();
                   nightvision_filter_age.Restart();
                   this.Content_layer10_hiddenforgoggles.visible = true;

                   sb.snd_nightvision.play(
                      sndTransform: new SoundTransform(
                         0.5
                      )
                   );


                   nighvision_handler = delegate
                   {
                       // http://doc.starling-framework.org/core/starling/filters/ColorMatrixFilter.html
                       // create an inverted filter with 50% saturation and 180° hue rotation
                       nightvision_filter = new ColorMatrixFilter();
                       nightvision_filter.adjustSaturation(-1.0);
                       nightvision_filter.invert();
                       //nightvision_filter.adjustContrast(1.0);

                       var a = (nightvision_filter_age.ElapsedMilliseconds / 1100.0).Min(1);

                       nightvision_filter.adjustContrast(

                           16 - 14 * a
                       );


                       nightvision_filter.concat(
                           new double[] {
                                                    
                          0, 0,  0,  0, 0,
                          0, 1,  0,  0, 0,
                          0, 0, 0,  0, 0,
                          0,  0,  0,  1,   0

                                                    }
                       );
                       this.filter = nightvision_filter;
                       this.stage.color = 0x006E00;

                       if (a == 1)
                           nighvision_handler = null;
                   };


               };
                #endregion

                #region nightvision_off
                nightvision_off = delegate
               {
                   if (!nightvision_mode)
                       return;

                   nightvision_mode = false;
                   hud_update();

                   this.Content_layer10_hiddenforgoggles.visible = false;

                   sb.snd_SelectWeapon.play(
                      sndTransform: new SoundTransform(
                         0.3
                      )
                   );

                   nightvision_filter_age.Restart();

                   nighvision_handler = delegate
                   {
                       nightvision_filter = new ColorMatrixFilter();



                       // nightvision_filter.adjustBrightness(

                       //    1 - (nightvision_filter_age.ElapsedMilliseconds / 200.0).Min(1)
                       //);

                       var a = (nightvision_filter_age.ElapsedMilliseconds / 500.0).Min(1);

                       nightvision_filter.adjustBrightness(

                            0.5 - 0.5 * a
                        );


                       this.filter = nightvision_filter;
                       this.stage.color = 0xB27D51;

                       if (a == 1)
                           nighvision_handler = null;
                   };

               };
                #endregion


                onframe +=
                    delegate
                    {
                        if (nighvision_handler != null)
                            nighvision_handler();



                    };
                #endregion


                onsyncframe +=
                    delegate
                    {
                        #region Soundboard
                        if (barrel_forceA > 0)
                        {
                            sb.snd_woodsmash.play(
                               sndTransform: new SoundTransform(
                                   Math.Min(1.0, barrel_forceA / 30.0) * (0.15 + 0.15 * random.NextDouble())
                               )
                           );

                            barrel_forceA = 0;

                        }
                        else if (hardmetal_forceA > 0)
                        {
                            sb.snd_hardmetalsmash.play(
                               sndTransform: new SoundTransform(
                                   Math.Min(1.0, hardmetal_forceA / 30.0) * (0.2 + 0.2 * random.NextDouble())
                               )
                           );

                            hardmetal_forceA = 0;
                        }
                        else if (jeep_forceA > 0)
                        {
                            sb.snd_metalsmash.play(
                               sndTransform: new SoundTransform(
                                   Math.Min(1.0, jeep_forceA / 30.0) * (0.2 + 0.2 * random.NextDouble())
                               )
                           );

                            jeep_forceA = 0;
                        }
                        else if (ped_forceA > 0)
                        {
                            sb.snd_ped_hit.play(
                               sndTransform: new SoundTransform(
                                   Math.Min(1.0, ped_forceA / 30.0) * (0.15 + 0.15 * random.NextDouble())
                               )
                           );

                            ped_forceA = 0;
                        }

                        if (this.syncframeid == 200)
                            sb.snd_whatsthatsound.play();

                        if (this.syncframeid == 400)
                            sb.snd_needweapon.play();

                        if (this.syncframeid == 800)
                            sb.snd_didyouhearthat.play();


                        if (this.syncframeid == 1200)
                            sb.snd_whatsthatsound.play();

                        sb.loopcrickets.MasterVolume = (1 - move_zoom) * 0.08;

                        sb.loopstrange1.MasterVolume = (1 - move_zoom) * 0.04;

                        sb.loophelicopter1.MasterVolume = 0.0;
                        sb.loopjeepengine.MasterVolume = 0.0;
                        sb.loopdiesel2.MasterVolume = 0.0;
                        sb.loopsand_run.MasterVolume = 0.0;


                        if (current is PhysicalPed)
                        {
                            sb.loopsand_run.MasterVolume = 0.5;
                            sb.loopsand_run.LeftVolume = 0.0 + move_zoom * 0.9;
                            sb.loopsand_run.RightVolume = 0.0 + move_zoom * 0.9;
                            sb.loopsand_run.Rate = 0.9 + move_zoom * 0.1;
                        }
                        else if (current is PhysicalHind)
                        {
                            sb.loopcrickets.MasterVolume = 0;

                            sb.loophelicopter1.MasterVolume = 0.3 + (current as PhysicalHind).visual.Altitude * 0.2;
                            sb.loophelicopter1.LeftVolume = 0.7 + move_zoom * 0.1;
                            sb.loophelicopter1.RightVolume = 0.8;
                            sb.loophelicopter1.Rate = 0.7 + (current as PhysicalHind).visual.Altitude * 0.25 + move_zoom * 0.05;
                        }
                        else if (current is PhysicalJeep)
                        {
                            sb.loopjeepengine.MasterVolume = 0.5;
                            sb.loopjeepengine.LeftVolume = 0.4 + move_zoom * 0.7;
                            sb.loopjeepengine.RightVolume = 1;
                            sb.loopjeepengine.Rate = 0.9 + move_zoom * 0.5;
                        }
                        else if (current is PhysicalTank)
                        {
                            sb.loopcrickets.MasterVolume = 0;

                            sb.loopdiesel2.MasterVolume = 0.5;
                            sb.loopdiesel2.LeftVolume = 0.3 + move_zoom * 0.7;
                            sb.loopdiesel2.RightVolume = 1;
                            sb.loopdiesel2.Rate = 0.9 + move_zoom;
                        }
                        else
                        {
                            sb.loopcrickets.MasterVolume = 0.2;


                        }

                        // stereoeffect // siren
                        sb.loopstrange1.LeftVolume = 0.1 * (1 + Math.Cos(this.gametime.ElapsedMilliseconds * 0.00001)) / 2.0;
                        sb.loopstrange1.RightVolume = 0.2 * (1 + Math.Cos(this.gametime.ElapsedMilliseconds * 0.00001)) / 2.0;

                        sb.loopcrickets.LeftVolume = (1 + Math.Sin(
                            this.gametime.ElapsedMilliseconds * 0.0001
                            + this.current.CameraRotation
                            + this.current.body.GetAngle()
                            )) / 2.0;
                        sb.loopcrickets.RightVolume = (3 + Math.Cos(this.gametime.ElapsedMilliseconds * 0.001
                            + this.current.CameraRotation
                            + this.current.body.GetAngle()
                            )) / 4.0;
                        #endregion

                        if (disable_enter_and_space)
                        {
                            // implemented elsewhere
                        }
                        else
                        {
                            #region entermode_changepending
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

                                                if (current is PhysicalJeep)
                                                {
                                                    sb.snd_jeepengine_start.play();
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

                                                hud_update();


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

                                                // crashland?
                                                (current as PhysicalHind).With(
                                                    hind =>
                                                    {
                                                        if (hind.visual.Altitude > 0)
                                                        {
                                                            hind.VerticalVelocity = -1;
                                                            sb.snd_touchdown.play();
                                                        }
                                                    }

                                                );

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

                                                hud_update();
                                                move_zoom = 1;
                                            }
                                        );
                                    }

                                }
                            }
                            #endregion


                            #region Space
                            if (!__keyDown[System.Windows.Forms.Keys.Space])
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
                                            {
                                                nightvision_on();
                                                hind1.VerticalVelocity = 1.0;
                                            }
                                            else
                                            {
                                                nightvision_off();

                                                hind1.VerticalVelocity = -0.4;

                                                sb.snd_touchdown.play();
                                            }

                                        }
                                    );

                                    (current as PhysicalPed).With(
                                     physical0 =>
                                     {
                                         if (physical0.visual.LayOnTheGround)
                                         {
                                             physical0.visual.LayOnTheGround = false;

                                             sb.snd_letsgo.play(
                                                 sndTransform: new SoundTransform(
                                                     0.3 * (0.15 + 0.15 * random.NextDouble())
                                                 )
                                             );
                                         }
                                         else
                                         {
                                             physical0.visual.LayOnTheGround = true;

                                             sb.snd_ped_hit.play(
                                                  sndTransform: new SoundTransform(
                                                      0.3 * (0.15 + 0.15 * random.NextDouble())
                                                  )
                                              );
                                         }

                                     }
                                 );




                                    mode_changepending = false;



                                }
                            }
                            #endregion

                        }


                        #region N
                        if (!__keyDown[System.Windows.Forms.Keys.N])
                        {
                            // space is not down.
                            nightvision_changepending = true;
                        }
                        else
                        {
                            if (nightvision_changepending)
                            {
                                if (nightvision_mode)
                                    nightvision_off();
                                else
                                    nightvision_on();




                                nightvision_changepending = false;



                            }
                        }
                        #endregion



                        current.SetVelocityFromInput(__keyDown);




                        #region simulate a weapone!
                        if (__keyDown[System.Windows.Forms.Keys.ControlKey])
                            if (syncframeid % 4 == 0)
                            {
                                sb.snd_missleLaunch.play();

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
