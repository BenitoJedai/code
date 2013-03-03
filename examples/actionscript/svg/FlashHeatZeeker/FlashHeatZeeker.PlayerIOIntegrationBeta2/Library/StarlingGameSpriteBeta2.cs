using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CoreMap.Library;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.UnitBunkerControl.Library;
using FlashHeatZeeker.UnitCannon.Library;
using FlashHeatZeeker.UnitHind.Library;
using FlashHeatZeeker.UnitJeep.Library;
using FlashHeatZeeker.UnitPed.Library;
using FlashHeatZeeker.UnitPedControl.Library;
using FlashHeatZeeker.UnitTank.Library;
using starling.display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Lambda;
using FlashHeatZeeker.UnitCannonControl.Library;
using FlashHeatZeeker.UnitTankControl.Library;
using FlashHeatZeeker.UnitJeepControl.Library;
using FlashHeatZeeker.UnitHindControl.Library;
using FlashHeatZeeker.CoreAudio.Library;
using ScriptCoreLib.ActionScript.flash.media;
using ScriptCoreLib.Shared.BCLImplementation.GLSL;
using ScriptCoreLib.ActionScript.flash.geom;
using FlashHeatZeeker.UnitPed.ActionScript.Images;

namespace FlashHeatZeeker.PlayerIOIntegrationBeta2.Library
{
    class StarlingGameSpriteBeta2 : StarlingGameSpriteWithPhysics
    {
        public static EnterOrExitAction __raise_enterorexit = delegate { };
        public static EnterOrExitAction __at_enterorexit = delegate { };


        public static Action<string> __raise_sync = delegate { };
        public static Action<string> __at_sync = delegate { };



        public static SetVelocityFromInputAction __raise_SetVelocityFromInput = delegate { };
        public static SetVelocityFromInputAction __at_SetVelocityFromInput = delegate { };

        public static SetVerticalVelocityAction __raise_SetVerticalVelocity = delegate { };
        public static SetVerticalVelocityAction __at_SetVerticalVelocity = delegate { };



        public List<RemoteGame> others = new List<RemoteGame>();


        public StarlingGameSpriteBeta2()
        {
            var textures_beta = new_tex96(new BetaBanner());

            var textures_map = new StarlingGameSpriteWithMapTextures(new_tex_crop);

            var textures_ped = new StarlingGameSpriteWithPedTextures(this.new_tex_crop);
            var textures_jeep = new StarlingGameSpriteWithJeepTextures(this.new_tex_crop);

            var textures_hind = new StarlingGameSpriteWithHindTextures(this.new_tex_crop);
            var textures_tank = new StarlingGameSpriteWithTankTextures(this.new_tex_crop);
            var textures_cannon = new StarlingGameSpriteWithCannonTextures(this.new_tex_crop);
            var textures_bunker = new StarlingGameSpriteWithBunkerTextures(this.new_tex_crop);

            this.disablephysicsdiagnostics = true;

            this.onbeforefirstframe += (stage, s) =>
             {
                 #region hill1
                 for (int i = 0; i < 32; i++)
                 {
                     new Image(textures_map.hill1()).AttachTo(Content).With(
                         hill =>
                         {
                             hill.x = 2048.Random();
                             hill.y = 2048.Random() - 1024;
                         }
                     );

                     new Image(textures_map.hole1()).AttachTo(Content).With(
                         hill =>
                         {
                             hill.x = 2048.Random();
                             hill.y = 2048.Random() - 1024;
                         }
                     );

                     new Image(textures_map.grass1()).AttachTo(Content).With(
                         hill =>
                         {
                             hill.x = 2048.Random();

                             var y = -2048.Random() - 512 - 256;
                             hill.y = y;
                         }
                     );
                 }
                 #endregion


                 #region other units
                 for (int i = 3; i < 7; i++)
                 {
                     {
                         new PhysicalCannon(textures_cannon, this).SetPositionAndAngle(
                             i * 16, -20, -random.NextDouble()
                         );

                         new PhysicalCannon(textures_cannon, this).SetPositionAndAngle(
                             i * 16, 42, random.NextDouble()
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
                     }
                     else if (i % 3 == 1)
                     {
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
                         new PhysicalSilo(textures_bunker, this).SetPositionAndAngle(
                             i * 16, -4, random.NextDouble()
                         );
                     }

                 }
                 #endregion


                 for (int i = -12; i < 12; i++)
                 {
                     new Image(textures_map.road0()).AttachTo(Content).x = 256 * i;
                 }

                 new Image(textures_map.touchdown()).AttachTo(Content).MoveTo(256, -256);
                 new Image(textures_map.touchdown()).AttachTo(Content).y = 256;

                 new PhysicalTank(textures_tank, this).SetPositionAndAngle(128 / 16, 128 * 3 / 16);

                 new Image(textures_map.tree0_shadow()).AttachTo(Content).y = 128 + 16;
                 new Image(textures_map.tree0()).AttachTo(Content).y = 128;

                 // can I have 
                 // new ped, hind, jeep, tank

                 #region :ego
                 var ego = new PhysicalPed(textures_ped, this)
                 {
                     Identity = sessionid + ":ego"
                 };

                 ego.SetPositionAndAngle(
                     random.NextDouble() * -8,
                     random.NextDouble() * -8,
                     random.NextDouble() * Math.PI
                 );

                 current = ego;
                 #endregion



                 var jeep2 = new PhysicalJeep(textures_jeep, this);


                 jeep2.SetPositionAndAngle(
                     16, 16, random.NextDouble()
                 );


                 #region tree0
                 for (int i = 0; i < 128; i++)
                 {


                     var x = 2048.Random();
                     var y = -2048.Random() - 512 - 256;

                     new Image(textures_map.tree0_shadow()).AttachTo(Content).MoveTo(x + 16, y + 16);
                     new Image(textures_map.tree0()).AttachTo(Content).MoveTo(x, y);
                 }
                 #endregion

                 // 12 = 34FPS

                 new PhysicalHind(textures_hind, this)
                 {
                     AutomaticTakeoff = true
                 }.SetPositionAndAngle((128 + 256) / 16, -128 / 16);


                 this.units.WithEachIndex(
                     (u, i) =>
                     {
                         if (u.Identity == null)
                             u.Identity = "#" + i;
                     }
                 );


                 #region KeySample
                 var __keyDown = new KeySample();

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

                 #region __at_SetVerticalVelocity
                 __at_SetVerticalVelocity +=
                     (string __sessionid, string identity, string value) =>
                     {
                         var o = other(__sessionid);

                         var u = this.units.FirstOrDefault(k => k.Identity == identity);

                         (u as PhysicalHind).With(hind1 => hind1.VerticalVelocity = double.Parse(value));

                         (u as PhysicalPed).With(
                               physical0 =>
                               {
                                   //                                  BCL needs another method, please define it.
                                   //Cannot call type without script attribute :
                                   //System.Convert for Boolean ToBoolean(Double) used at

                                   var LayOnTheGround = double.Parse(value);

                                   if (LayOnTheGround == 1)
                                       physical0.visual.LayOnTheGround = true;
                                   else
                                       physical0.visual.LayOnTheGround = false;




                               }
                           );

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

                         var u = this.units.FirstOrDefault(k => k.Identity == __identity);

                         if (u == null)
                             if (o.ego == null)
                             {
                                 // the only object we can be creating implicitly is
                                 // the remote ego

                                 u = new PhysicalPed(textures_ped, this)
                                 {
                                     Identity = __identity,
                                     RemoteGameReference = o
                                 };

                                 u.SetPositionAndAngle(
                                     double.Parse(__fixup_x),
                                     double.Parse(__fixup_y),
                                     double.Parse(__fixup_angle)
                                 );

                                 o.ego = u;
                             }


                         // set the input!


                         u.SetVelocityFromInput(
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


                 #region __at_enterorexit
                 __at_enterorexit += (__egoid, __from, __to) =>
                 {
                     var o = other(__egoid);

                     var ufrom = this.units.FirstOrDefault(k => k.Identity == __from);
                     var uto = this.units.FirstOrDefault(k => k.Identity == __to);

                     (ufrom as PhysicalPed).With(
                         candidatedriver =>
                         {
                             if (uto != null)
                                 if (uto.driverseat != null)
                                     if (uto.driverseat.driver == null)
                                     {
                                         // and the devil enters
                                         uto.RemoteGameReference = o;

                                         candidatedriver.body.SetActive(false);

                                         uto.driverseat.driver = candidatedriver;
                                         candidatedriver.seatedvehicle = uto;
                                     }
                         }
                     );

                     (uto as PhysicalPed).With(
                         driver =>
                         {
                             if (ufrom != null)
                                 if (ufrom.driverseat != null)
                                     if (ufrom.driverseat.driver == driver)
                                     {
                                         // relinguish that vehicle. no longer posessed :)
                                         ufrom.RemoteGameReference = null;

                                         // stop the vehicle
                                         ufrom.SetVelocityFromInput(new KeySample());

                                         // get out of the lift..
                                         ufrom.driverseat.driver = null;

                                         driver.seatedvehicle = null;
                                         driver.body.SetActive(true);
                                     }
                         }
                     );

                 };
                 #endregion




                 #region hud
                 var hud = new Image(textures_ped.hud_look()).AttachTo(this);
                 var beta = new Image(textures_beta()).AttachTo(this);

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
                                  16 + 0,
                                  stage.stageHeight - 64 - 24);

                              hud.transformationMatrix = cm;
                          }
                          #endregion

                          #region beta
                          {
                              var cm = new Matrix();

                              cm.translate(
                                 stage.stageWidth - 96,
                                  0);

                              beta.transformationMatrix = cm;
                          }
                          #endregion
                      };
                 #endregion


                 #region Soundboard
                 // http://www.nasa.gov/vision/universe/features/halloween_sounds.html
                 Soundboard sb = new Soundboard();

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

                 PhysicalJeep.oncollision +=
                     (u, jeep_forceA) =>
                     {
                         sb.snd_metalsmash.play(
                            sndTransform: new SoundTransform(
                                Math.Min(1.0, jeep_forceA / 30.0) * 0.4
                            )
                        );
                     };
                 #endregion

                 bool entermode_changepending = false;
                 bool mode_changepending = false;
                 onsyncframe += delegate
                 {
                     #region Soundboard
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


                     #region mode
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
                                         hind1.VerticalVelocity = 1.0;
                                     else
                                     {
                                         hind1.VerticalVelocity = -0.4;
                                         sb.snd_touchdown.play();

                                     }

                                     __raise_SetVerticalVelocity(
                                         "" + this.sessionid,
                                         hind1.Identity,
                                         "" + hind1.VerticalVelocity
                                     );
                                 }
                             );

                             (current as PhysicalPed).With(
                                  physical0 =>
                                  {
                                      if (physical0.visual.LayOnTheGround)
                                          physical0.visual.LayOnTheGround = false;
                                      else
                                          physical0.visual.LayOnTheGround = true;


                                      //                                     BCL needs another method, please define it.
                                      //Cannot call type without script attribute :
                                      //System.Convert for Double ToDouble(Boolean) used at

                                      var value = 0;
                                      if (physical0.visual.LayOnTheGround)
                                          value = 1;

                                      __raise_SetVerticalVelocity(
                                          "" + this.sessionid,
                                          physical0.Identity,
                                          "" + value
                                      );

                                  }
                              );




                             mode_changepending = false;



                         }
                     }
                     #endregion



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

                                         __raise_enterorexit(
                                            "" + this.sessionid,
                                            candidatedriver.Identity,
                                            x.candidatevehicle.Identity
                                        );

                                         //current.loc.visible = false;
                                         current.body.SetActive(false);

                                         x.candidatevehicle.driverseat.driver = candidatedriver;
                                         candidatedriver.seatedvehicle = x.candidatevehicle;

                                         move_zoom = 1;
                                         current = x.candidatevehicle;



                                         if (current is PhysicalJeep)
                                         {
                                             sb.snd_jeepengine_start.play();
                                         }

                                         if (current.body.GetType() == Box2D.Dynamics.b2Body.b2_dynamicBody)
                                         {
                                             hud.texture = textures_ped.hud_look_goggles();
                                         }
                                         else
                                         {
                                             hud.texture = textures_ped.hud_look_building();
                                         }
                                         //switchto(x.x);

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
                                         // stop the vehicle
                                         current.SetVelocityFromInput(new KeySample());

                                         // get out of the lift..
                                         current.driverseat.driver = null;

                                         driver.seatedvehicle = null;
                                         driver.body.SetActive(true);


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


                                         __raise_enterorexit(
                                              "" + this.sessionid,
                                              current.Identity,
                                              driver.Identity
                                          );

                                         current = driver;
                                         //hud.texture = textures_ped.hud_look();
                                         move_zoom = 1;
                                     }
                                 );
                             }

                         }
                     }
                     #endregion


                     current.SetVelocityFromInput(__keyDown);


                     __raise_SetVelocityFromInput(
                          "" + sessionid,
                          current.Identity,
                          "" + current.CurrentInput.value,
                          "" + current.body.GetPosition().x,
                          "" + current.body.GetPosition().y,
                          "" + current.body.GetAngle()

                      );


                     // tell others this sync frame ended for us
                     __raise_sync("" + sessionid);
                 };
             };

        }
    }
}
