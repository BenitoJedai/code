using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.flash.display;

namespace FlashTowerDefense.ActionScript.Actors
{
    [Script]
    class Warrior : Actor
    {
        static Bitmap[] frames
        {
            get
            {
                return new Bitmap[]
                {
                    Assets.man2_horns1,
                    Assets.man2_horns2,
                    Assets.man2_horns3,
                    Assets.man2_horns4,
                    Assets.man2_horns5,
                    Assets.man2_horns6,
                    Assets.man2_horns7,
                    Assets.man2_horns8,
                    Assets.man2_horns9,
                };
            }
        }
        public Warrior()
            : base(frames, Assets.man2_horns_dead1, Assets.man2_horns_dead2, Assets.snd_man2)
        {
            ActorName = "Warrior";
        }
    }
}
