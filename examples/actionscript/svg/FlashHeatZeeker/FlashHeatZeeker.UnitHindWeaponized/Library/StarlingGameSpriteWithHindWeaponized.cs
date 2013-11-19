using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.UnitHind.Library;
using FlashHeatZeeker.UnitHindControl.Library;
using FlashHeatZeeker.UnitRocket.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;
using FlashHeatZeeker.CoreAudio.Library;
using System.Windows.Forms;
using FlashHeatZeeker.UnitJeepControl.Library;
using FlashHeatZeeker.CoreMap.Library;
using starling.display;
using FlashHeatZeeker.UnitBunkerControl.Library;
using ScriptCoreLib.ActionScript.flash.geom;
using FlashHeatZeeker.UnitPedControl.Library;
using FlashHeatZeeker.UnitPed.Library;
using ScriptCoreLib.ActionScript.flash.display;

namespace FlashHeatZeeker.UnitHindWeaponized.Library
{
    class StarlingGameSpriteWithHindWeaponized : StarlingGameSpriteWithPhysics
    {
        public static KeySample __keyDown = new KeySample();
        public Soundboard sb = new Soundboard();



        public StarlingGameSpriteWithHindWeaponized()
        {
            var textures_ped = new StarlingGameSpriteWithPedTextures(
                this.new_tex_crop,
                this.new_texsprite_crop
                );


            var textures_hind = new StarlingGameSpriteWithHindTextures(this.new_tex_crop);
            var textures_rocket = new StarlingGameSpriteWithRocketTextures(this.new_tex_crop);
            var textures_map = new StarlingGameSpriteWithMapTextures(new_tex_crop);
            var textures_bunker = new StarlingGameSpriteWithBunkerTextures(new_tex_crop);
            var textures_explosions = new StarlingGameSpriteWithMapExplosionsTextures(new_tex96);

            //this.internalscale = 1.0;
            this.disablephysicsdiagnostics = true;

            this.onbeforefirstframe += (stage, s) =>
            {
                stage.color = 0x75C64F;

                // hind is looking right
                var explosins = new List<ExplosionInfo>();

                for (int i = -12; i < 12; i++)
                {
                    new Image(textures_map.road0()).AttachTo(Content).x = 256 * i;

                    var bunker0 = new PhysicalBunker(textures_bunker, this);
                    bunker0.SetPositionAndAngle(-12 * i, 24);

                    var z = new PhysicalPed(textures_ped, this);

                    z.SetPositionAndAngle(16 * i, 0);
                    z.BehaveLikeZombie();

                    //var exp = new Image(textures_explosions.explosions[0]()).AttachTo(Content);
                    //explosins.Add(exp);
                    //exp.scaleX = 2.0;
                    //exp.scaleY = 2.0;

                    //exp.x = 256 * i;
                }

                #region other units
                for (int i = 3; i < 9; i++)
                {
                    var hind2 = new PhysicalHindWeaponized(textures_hind, textures_rocket, this)
                    {
                        AutomaticTakeoff = true
                    };

                    hind2.SetPositionAndAngle(
                        i * 16, 8, random.NextDouble()
                    );





                }
                #endregion


                #region CreateExplosion
                this.CreateExplosion = (x, y) =>
                {
                    var size = 0.2 + 0.2 * random.NextDouble();

                    sb.snd_explosion.play(
                        sndTransform: new ScriptCoreLib.ActionScript.flash.media.SoundTransform(size)
                    );

                    var exp = new Image(textures_explosions.explosions[0]()).AttachTo(Content);
                    var cm = new Matrix();

                    cm.translate(-32, -32);
                    cm.scale(5 * size, 5 * size);
                    cm.translate(16 * x, 16 * y);

                    exp.transformationMatrix = cm;

                    explosins.Add(
                    new ExplosionInfo { visual = exp }
                    );

                };
                #endregion


                var hind0 = new PhysicalHindWeaponized(
                    textures_hind, textures_rocket, this
                    );


                current = hind0;


                #region __keyDown

                stage.keyDown +=
                   e =>
                   {
                       e.preventDefault();
                       // http://circlecube.com/2008/08/actionscript-key-listener-tutorial/
                       if (e.altKey)
                           __keyDown[System.Windows.Forms.Keys.Alt] = true;

                       __keyDown[(System.Windows.Forms.Keys)e.keyCode] = true;
                   };

                stage.keyUp +=
                 e =>
                 {
                     e.preventDefault();

                     if (!e.altKey)
                         __keyDown[System.Windows.Forms.Keys.Alt] = false;

                     __keyDown[(System.Windows.Forms.Keys)e.keyCode] = false;
                 };

                #endregion

                bool mode_changepending = false;

                var dx_stop = false;
                var dy_stop = false;
                var dx = 0.0;
                var dy = 0.0;

                //              Error: Error #3707: Property can not be set in non full screen mode.
                //at flash.display::Stage/set mouseLock()

                // http://inflagrantedelicto.memoryspiral.com/2012/07/as3-quickie-mouse-lock-and-relative-mouse-coordinates/
                stage.fullScreen +=
                    delegate
                    {
                        if (stage.displayState == StageDisplayState.NORMAL)
                            return;

                        // http://helpx.adobe.com/flash-player/release-note/fp_114_air_34_release_notes.html
                        // · Mouse Lock feature disabled after entering Full Screen Interactive mode(3174344)
                        stage.mouseLock = true;

                    };



                stage.mouseDown +=
                    e =>
                    {
                        if (stage.displayState == StageDisplayState.NORMAL)
                        {
                            return;
                        }

                        __keyDown[Keys.ControlKey] = true;
                    };

                stage.doubleClickEnabled = true;
                stage.doubleClick += delegate
                {
                    if (stage.displayState == StageDisplayState.NORMAL)
                    {
                        stage.displayState = ScriptCoreLib.ActionScript.flash.display.StageDisplayState.FULL_SCREEN;
                        stage.displayState = ScriptCoreLib.ActionScript.flash.display.StageDisplayState.FULL_SCREEN_INTERACTIVE;
                        return;
                    }
                };

                stage.mouseUp +=
                    e =>
                    {
                        if (stage.displayState == StageDisplayState.NORMAL)
                        {
                            return;
                        }

                        __keyDown[Keys.ControlKey] = false;
                    };

                stage.mouseMove +=
                     e =>
                     {
                         if (stage.displayState == StageDisplayState.NORMAL)
                         {
                             dx = 0;
                             dy = 0;
                             return;
                         }

                         //dynamic ee = e;

                         //// ReferenceError: Erroe.r #1069: Property movementX not found on flash.events.MouseEvent and there is no default value.
                         //double movementX = ee.me.ovementX;
                         //double movementY = ee.movementY;

                         // http://www.levelxgames.com/2012/11/how-to-overlay-abode-air-sdk-over-flex-sdk/
                         dx += e.movementX;
                         dy += e.movementY;
                     };

                onsyncframe +=
                   delegate
                   {
                       this.Text = new { dx }.ToString();

                       foreach (var item in explosins.ToArray())
                       {
                           item.index++;

                           if (item.index == textures_explosions.explosions.Length)
                           {
                               item.visual.Orphanize();
                               explosins.Remove(item);
                           }
                           else
                           {
                               item.visual.texture = textures_explosions.explosions[item.index]();
                           }
                       }

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
                                           hind1.VerticalVelocity = -0.4;

                                   }
                               );






                               mode_changepending = false;



                           }
                       }
                       #endregion

                       if (dx < 0)
                       {
                           __keyDown[Keys.Left] = true;
                           __keyDown[Keys.Right] = false;
                           __keyDown.forcex = (Math.Abs(dx) / 100.0).Min(1.0);
                           dx = 0;
                           dx_stop = true;
                       }
                       else if (dx > 0)
                       {
                           __keyDown[Keys.Right] = true;
                           __keyDown[Keys.Left] = false;
                           __keyDown.forcex = (Math.Abs(dx) / 100.0).Min(1.0);
                           dx = 0;
                           dx_stop = true;
                       }
                       else
                       {
                           if (dx_stop)
                           {
                               __keyDown[Keys.Left] = false;
                               __keyDown[Keys.Right] = false;
                           }
                           dx_stop = false;

                           __keyDown.forcex = 1.0;
                       }

                       if (dy < 0)
                       {
                           __keyDown[Keys.Up] = true;
                           __keyDown[Keys.Down] = false;
                           __keyDown.forcey = (Math.Abs(dy) / 200.0).Min(1.0);
                           dy_stop = true;
                       }
                       else if (dy > 0)
                       {
                           __keyDown[Keys.Up] = false;
                           __keyDown[Keys.Down] = true;
                           __keyDown.forcey = (Math.Abs(dy) / 200.0).Min(1.0);
                           dy_stop = true;
                       }
                       else
                       {
                           if (dy_stop)
                           {
                               __keyDown[Keys.Up] = false;
                               __keyDown[Keys.Down] = false;
                           }
                           dy_stop = false;

                           __keyDown.forcey = 1.0;
                       }

                       current.SetVelocityFromInput(__keyDown);



                       #region simulate a weapone!
                       if (__keyDown[System.Windows.Forms.Keys.ControlKey])
                           if (syncframeid % 3 == 0)
                           {
                               sb.snd_missleLaunch.play();
                               hind0.FireRocket();
                           }
                       #endregion
                   };
            };

        }
    }
}
