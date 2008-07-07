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
    public class Pig : Actor
    {
        static Bitmap[] frames
        {
            get
            {
                return new BitmapAsset[]
                {
                    Images.pig1,
                    Images.pig2,
                    Images.pig3,
                    Images.pig4
                };
            }
        }

        public Pig()
            : base(frames, Images.pig_corpse, Images.pig_blood, Sounds.snd_pig)
        {
            ActorName = "Pig";
        }
    }
}
