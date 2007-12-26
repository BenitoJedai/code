using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ThreeDStuff.js.NatureBoy
{
    [Script]
    static class Frames
    {
        const double d8 = 1d / 8;


        // http://www.wolf3d.co.uk/enemies.html

        public static FrameInfo[] WolfSoldier
        {
            get
            {
                return
            8.Range(i => 114 + (i + 6) % 8)
            .Select(i =>
                new FrameInfo
                {
                    Source = "assets/NatureBoy/dude5/stand/" + i + ".png",
                    Weight = d8
                }
            ).ToArray();
            }
        }

        // compiler bug: casting wont work from IEnumerable<T[]> to T[]

        public static FrameInfo[][] WolfSoldier_Walk
        {
            get
            {
                return
            4.Range(
                j =>
                    8.Range(i => (122 + j * 8) + (i + 6) % 8)
                    .Select(i =>
                        new FrameInfo
                        {
                            Source = "assets/NatureBoy/dude5/walk" + (j + 1) + "/" + i + ".png",
                            Weight = d8
                        }
                    ).ToArray()
            ).ToArray();
            }
        }


        public static FrameInfo[] DoomImp
        {
            get
            {
                return

                       8.Range(i => 244 + (i + 6) % 8)
                       .Select(i =>
                           new FrameInfo
                           {
                               Source = "assets/NatureBoy/dude6/" + i + ".png",
                               Weight = d8
                           }
                       ).ToArray();
            }
        }


        public static FrameInfo[][] DoomImp_Walk
        {
            get
            {
                return 4.Range(
                    j =>
                        8.Range(i => (252 + j * 8) + (i + 6) % 8)
                        .Select(i =>
                            new FrameInfo
                            {
                                Source = "assets/NatureBoy/dude6/" + i + ".png",
                                Weight = d8
                            }
                        ).ToArray()
                ).ToArray();
            }
        }

        public static FrameInfo[] Vehicle1
        {
            get
            {
                return

                       32.Range(i => 308 + i)
                       .Select(i =>
                           new FrameInfo
                           {
                               Source = "assets/Vehicle1/" + i + ".png",
                               Weight = 1 / 32,
                               OffsetY =  -8
                           }
                       ).ToArray();
            }
        }
    }

}
