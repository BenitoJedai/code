using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.UnitJeep.Library;
using FlashHeatZeeker.UnitJeepControl.Library;
using FlashHeatZeeker.UnitPed.Library;
using FlashHeatZeeker.UnitPedControl.Library;
using ScriptCoreLib.Shared.BCLImplementation.GLSL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;
using starling.display;
using FlashHeatZeeker.CoreMap.Library;
using FlashHeatZeeker.UnitBunkerControl.Library;
using FlashHeatZeeker.UnitTank.Library;
using FlashHeatZeeker.UnitTankControl.Library;

namespace FlashHeatZeeker.TestDriversSync.Library
{
    public delegate void EnterOrExitAction(string egoid, string from, string to);

    class StarlingGameSpriteWithTestDriversSync : StarlingGameSpriteWithPhysics
    {
        public static EnterOrExitAction __raise_enterorexit = delegate { };
        public static EnterOrExitAction __at_enterorexit = delegate { };


        public static Action<string> __raise_sync = delegate { };
        public static Action<string> __at_sync = delegate { };



        public static SetVelocityFromInputAction __raise_SetVelocityFromInput = delegate { };
        public static SetVelocityFromInputAction __at_SetVelocityFromInput = delegate { };



        public List<RemoteGame> others = new List<RemoteGame>();


        public StarlingGameSpriteWithTestDriversSync()
        {
            var textures_map = new StarlingGameSpriteWithMapTextures(new_tex_crop);

            var textures_ped = new StarlingGameSpriteWithPedTextures(this.new_tex_crop);
            var textures_jeep = new StarlingGameSpriteWithJeepTextures(this.new_tex_crop);
            var textures_tank = new StarlingGameSpriteWithTankTextures(new_tex_crop);

            var textures_bunker = new StarlingGameSpriteWithBunkerTextures(this.new_tex_crop);

            this.onbeforefirstframe += (stage, s) =>
            {
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

                new PhysicalBunker(textures_bunker, this) { Identity = "bunker:0" }.SetPositionAndAngle(0, -24);

                new PhysicalJeep(textures_jeep, this) { Identity = ":1" }.SetPositionAndAngle(-8, -12);

                new Image(textures_map.touchdown()).AttachTo(Content).y = 256;

                new PhysicalJeep(textures_jeep, this) { Identity = ":2" }.SetPositionAndAngle(0, -12);
                new PhysicalTank(textures_tank, this) { Identity = ":3" }.SetPositionAndAngle(8, -12);





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



                bool entermode_changepending = false;

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

                                        move_zoom = 1;
                                        current = x.candidatevehicle;





                                        //if (current.body.GetType() == Box2D.Dynamics.b2Body.b2_dynamicBody)
                                        //{
                                        //    hud.texture = textures_ped.hud_look_goggles();
                                        //}
                                        //else
                                        //{
                                        //    hud.texture = textures_ped.hud_look_building();
                                        //}

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
                                        //(current as PhysicalHind).With(
                                        //    hind => hind.VerticalVelocity = -1
                                        //);

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
