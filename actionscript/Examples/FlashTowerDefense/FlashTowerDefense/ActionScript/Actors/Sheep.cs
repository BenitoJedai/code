using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.mx.core;
using FlashTowerDefense.ActionScript.Assets;

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
                    Images.sheep1,
                    Images.sheep2,
                    Images.sheep3,
                    Images.sheep4
                };
            }
        }

        public Sheep()
            : base(frames, Images.sheep_corpse, Images.sheep_blood, Sounds.snd_sheep)
        {
            ActorName = "Sheep";
        }
    }
}
