using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using Abstractatech.JavaScript.NatureBoy.HTML.Pages;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.Controls.NatureBoy
{
    public static class Frames
    {
        const double d8 = 1d / 8;


        // http://www.wolf3d.co.uk/enemies.html



        public static FrameInfo[] WolfSoldier
        {
            get
            {
                IHTMLImage[] __Dude5Images = new Dude5Images().Images;
                return

                      8.Range(i => 114 + (i + 6) % 8)
                       .Select(i =>
                           new FrameInfo
                           {
                               //Source = Assets.dude5 + "/" + i + ".png",
                               Source = __Dude5Images.ElementAt(i - 114).src,

                               Weight = d8
                           }
                       ).ToArray();
            }
        }


        public static FrameInfo[][] WolfSoldier_Walk
        {
            get
            {
                IHTMLImage[] __Dude5Images = new Dude5Images().Images;
                return 4.Range(
                    j =>
                        8.Range(i => (122 + j * 8) + (i + 6) % 8)
                        .Select(i =>
                            new FrameInfo
                            {
                                //Source = Assets.dude5 + "/" + i + ".png",
                                Source = __Dude5Images.ElementAt(i - 114).src,
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
                IHTMLImage[] __Dude6Images = new Dude6Images().Images;
                return

                       8.Range(i => 244 + (i + 6) % 8)
                       .Select(i =>
                           new FrameInfo
                           {
                               //Source = Assets.dude6 + "/" + i + ".png",
                               Source = __Dude6Images.ElementAt(i - 244).src,
                               Weight = d8
                           }
                       ).ToArray();
            }
        }


        public static FrameInfo[][] DoomImp_Walk
        {
            get
            {
                IHTMLImage[] __Dude6Images = new Dude6Images().Images;
                return 4.Range(
                    j =>
                        8.Range(i => (252 + j * 8) + (i + 6) % 8)
                        .Select(i =>
                            new FrameInfo
                            {
                                //Source = Assets.dude6 + "/" + i + ".png",
                                Source = __Dude6Images.ElementAt(i - 244).src,
                                Weight = d8
                            }
                        ).ToArray()
                ).ToArray();
            }
        }


    }

}
