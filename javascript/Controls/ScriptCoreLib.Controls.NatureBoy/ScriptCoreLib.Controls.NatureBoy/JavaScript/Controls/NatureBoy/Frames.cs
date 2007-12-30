using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLib.JavaScript.Controls.NatureBoy
{
    [Script]
    public static class Frames
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
                               Source = Assets.dude5 + "/" + i + ".png",
                               Weight = d8
                           }
                       ).ToArray();
            }
        }


        public static FrameInfo[][] WolfSoldier_Walk
        {
            get
            {
                return 4.Range(
                    j =>
                        8.Range(i => (122 + j * 8) + (i + 6) % 8)
                        .Select(i =>
                            new FrameInfo
                            {
                                Source = Assets.dude5 + "/" + i + ".png",
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
                               Source = Assets.dude6 + "/" + i + ".png",
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
                                Source = Assets.dude6 + "/" + i + ".png",
                                Weight = d8
                            }
                        ).ToArray()
                ).ToArray();
            }
        }


    }

}
