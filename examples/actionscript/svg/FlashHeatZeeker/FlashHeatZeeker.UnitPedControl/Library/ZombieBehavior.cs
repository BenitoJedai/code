using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.UnitJeepControl.Library;
using ScriptCoreLib.Shared.BCLImplementation.GLSL;
using starling.filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlashHeatZeeker.UnitPedControl.Library
{
    public static class ZombieBehavior
    {
        public static void BehaveLikeZombie(this PhysicalPed physical0)
        {
            physical0.speed = 4;


            physical0.visual.WalkLikeZombie = true;

            //var f = new ColorMatrixFilter();
            //f.adjustSaturation(-1);

            ////f.concat(
            ////         new double[] {

            ////              0, 0,  0,  0, 0,
            ////              0, 1,  0,  0, 0,
            ////              0, 0, 0,  0, 0,
            ////              0,  0,  0,  1,   0

            ////                                        }
            ////     );
            ////f.adjustSaturation(-0.8);

            //physical0.visual.currentvisual.filter = f;

            var seed = physical0.Context.random.Next(30);

            physical0.Context.onsyncframe +=
                delegate
                {
                    var frame = (seed + physical0.Context.syncframeid) % 20;


                    if (physical0.visual.LayOnTheGround)
                    {
                        if (frame == 0)
                            if (physical0.Context.random.NextDouble() < 0.1)
                            {
                                // review!
                                physical0.body.SetActive(true);
                                physical0.damagebody.SetActive(true);
                                physical0.visual.LayOnTheGround = false;
                            }
                        return;
                    }


                    Func<PhysicalPed, double, double> GetMotivation =
                        (candidateped, distance) =>
                        {
                            if (candidateped.AttractZombies)
                                return distance;


                            return 16 + distance;
                        };

                    var target =
                        from candidate in physical0.Context.units

                        let candidateped = candidate as PhysicalPed
                        where candidateped != null

                        // zombies wont attract zombies
                        where !candidateped.visual.WalkLikeZombie

                        let gap = new __vec2(
                            (float)(candidate.body.GetPosition().x - physical0.body.GetPosition().x),
                            (float)(candidate.body.GetPosition().y - physical0.body.GetPosition().y)
                        )

                        let distance = gap.GetLength()

                        let CloseEnoughToAttract = distance < 16
                        let PreventWanderingOff = distance > 48
                        where CloseEnoughToAttract || PreventWanderingOff

                        orderby GetMotivation(candidateped, distance) ascending

                        select new { candidateped, distance, gap };

                    // this costs 10% Total time
                    var firsttarget = target.FirstOrDefault();

                    if (firsttarget != null)
                    {


                        // stare at victim
                        var up = new KeySample();

                        if (firsttarget.distance > 3)
                        {
                            up[Keys.Up] = true;
                            up.forcey = firsttarget.distance.Min(8) / 4.0;
                        }
                        else
                        {
                            // attack isntead!
                        }

                        physical0.SetVelocityFromInput(up);


                        physical0.SetPositionAndAngle(
                            physical0.body.GetPosition().x,
                            physical0.body.GetPosition().y,
                            firsttarget.gap.GetRotation()
                        );

                        return;
                    }


                    if (frame == 0)
                    {
                        // where to?
                        if (physical0.Context.random.NextDouble() < 0.5)
                        {
                            var up = new KeySample();
                            up[Keys.Left] = true;
                            up.forcex = physical0.Context.random.NextDouble();
                            physical0.SetVelocityFromInput(up);
                        }
                        else
                        {
                            var up = new KeySample();
                            up[Keys.Right] = true;
                            up.forcex = physical0.Context.random.NextDouble();
                            physical0.SetVelocityFromInput(up);
                        }

                    }


                    if (frame == 3)
                    {
                        var up = new KeySample();
                        up[Keys.Up] = true;
                        physical0.SetVelocityFromInput(up);
                    }

                    if (frame == 17)
                    {
                        var up = new KeySample();
                        physical0.SetVelocityFromInput(up);
                    }
                };
        }
    }
}
