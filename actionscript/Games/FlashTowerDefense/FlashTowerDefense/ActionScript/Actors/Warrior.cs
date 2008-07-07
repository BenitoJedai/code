using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.flash.display;
using FlashTowerDefense.ActionScript.Assets;

namespace FlashTowerDefense.ActionScript.Actors
{
    [Script]
    public class Warrior : Actor
    {
        static Bitmap[] frames
        {
            get
            {
                return new Bitmap[]
                {
                    Images.man2_horns1,
                    Images.man2_horns2,
                    Images.man2_horns3,
                    Images.man2_horns4,
                    Images.man2_horns5,
                    Images.man2_horns6,
                    Images.man2_horns7,
                    Images.man2_horns8,
                    Images.man2_horns9,
                };
            }
        }
        public Warrior()
            : base(frames, Images.man2_horns_dead1, Images.man2_horns_dead2, Sounds.snd_man2)
        {
            ActorName = "Warrior";
        }


    }
}
