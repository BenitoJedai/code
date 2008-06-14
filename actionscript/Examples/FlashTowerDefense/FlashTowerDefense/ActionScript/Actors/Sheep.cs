using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.mx.core;

namespace FlashTowerDefense.ActionScript.Actors
{
    [Script]
    class Sheep : Actor
    {
        static Bitmap[] frames
        {
            get
            {
                return new BitmapAsset[]
                {
                    Assets.sheep1,
                    Assets.sheep2,
                    Assets.sheep3,
                    Assets.sheep4
                };
            }
        }

        public Sheep()
            : base(frames, Assets.sheep_corpse, Assets.sheep_blood, Assets.snd_sheep)
        {
            ActorName = "Sheep";
        }
    }
}
