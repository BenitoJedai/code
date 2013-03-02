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

namespace FlashHeatZeeker.TestDriversSync.Library
{
    class StarlingGameSpriteWithTestDriversSync : StarlingGameSpriteWithPhysics
    {
        public static Action<string> __raise_sync = delegate { };
        public static Action<string> __at_sync = delegate { };



        public static SetVelocityFromInputAction __raise_SetVelocityFromInput = delegate { };
        public static SetVelocityFromInputAction __at_SetVelocityFromInput = delegate { };



        public List<RemoteGame> others = new List<RemoteGame>();


        public StarlingGameSpriteWithTestDriversSync()
        {
            var textures_ped = new StarlingGameSpriteWithPedTextures(this.new_tex_crop);
            var textures_jeep = new StarlingGameSpriteWithJeepTextures(this.new_tex_crop);

            this.onbeforefirstframe += (stage, s) =>
            {
                #region :ego
                var ego = new PhysicalPed(textures_ped, this)
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

                new PhysicalJeep(textures_jeep, this) { Identity = ":1" }.SetPositionAndAngle(-8, -12);
                new PhysicalJeep(textures_jeep, this) { Identity = ":2" }.SetPositionAndAngle(0, -12);
                new PhysicalJeep(textures_jeep, this) { Identity = ":3" }.SetPositionAndAngle(8, -12);




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

                                        //current.loc.visible = false;
                                        current.body.SetActive(false);


                                        x.candidatevehicle.driverseat.driver = candidatedriver;
                                        candidatedriver.seatedvehicle = x.candidatevehicle;

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

                                        current.driverseat.driver = null;
                                        driver.seatedvehicle = null;
                                        current.SetVelocityFromInput(new KeySample());

                                        // crashland?
                                        //(current as PhysicalHind).With(
                                        //    hind => hind.VerticalVelocity = -1
                                        //);


                                        current = driver;
                                        current.body.SetActive(true);
                                        //hud.texture = textures_ped.hud_look();
                                        move_zoom = 1;
                                    }
                                );
                            }

                        }
                    }
                    #endregion


                    current.SetVelocityFromInput(__keyDown);





                };


            };
        }
    }
}
