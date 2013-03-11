using FlashHeatZeeker.Core.Library;
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
                    if (physical0.visual.LayOnTheGround)
                        return;

                    var frame = (seed + physical0.Context.syncframeid) % 20;

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
