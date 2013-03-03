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
using FlashHeatZeeker.TestDriversWithAudio.Library;

namespace FlashHeatZeeker.PlayerIOIntegrationBeta2.Library
{
    class StarlingGameSpriteBeta2 : StarlingGameSpriteWithTestDriversWithAudio
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

            //var textures_map = new StarlingGameSpriteWithMapTextures(new_tex_crop);

            //var textures_ped = new StarlingGameSpriteWithPedTextures(this.new_tex_crop);
            //var textures_jeep = new StarlingGameSpriteWithJeepTextures(this.new_tex_crop);

            //var textures_hind = new StarlingGameSpriteWithHindTextures(this.new_tex_crop);
            //var textures_tank = new StarlingGameSpriteWithTankTextures(this.new_tex_crop);
            //var textures_cannon = new StarlingGameSpriteWithCannonTextures(this.new_tex_crop);
            //var textures_bunker = new StarlingGameSpriteWithBunkerTextures(this.new_tex_crop);

            this.disablephysicsdiagnostics = true;
            this.disable_enter_and_space = true;

            this.onbeforefirstframe += (stage, s) =>
             {

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





                 #region :ego
                 var ego = this.current as PhysicalPed;

                 ego.Identity = sessionid + ":ego";

                 ego.SetPositionAndAngle(
                     random.NextDouble() * -8,
                     random.NextDouble() * -8,
                     random.NextDouble() * Math.PI
                 );

                 current = ego;
                 #endregion




                 this.units.WithEachIndex(
                     (u, i) =>
                     {
                         if (u.Identity == null)
                             u.Identity = "#" + i;
                     }
                 );





                 #region beta
                 var beta = new Image(textures_beta()).AttachTo(this);

                 onframe +=
                      delegate
                      {

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


                 bool entermode_changepending = false;
                 bool mode_changepending = false;
                 onsyncframe += delegate
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

                                         __raise_enterorexit(
                                              "" + this.sessionid,
                                              candidatedriver.Identity,
                                              x.candidatevehicle.Identity
                                          );

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

                                         __raise_enterorexit(
                                             "" + this.sessionid,
                                             current.Identity,
                                             driver.Identity
                                         );

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
