using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using FlashTowerDefense.ActionScript.Assets;

namespace FlashTowerDefense.ActionScript.Actors
{
    [Script]
    public class ExplosiveBarrel : Actor
    {
        static Bitmap[] frames
        {
            get
            {
                return new Bitmap[]
                {
                    Images.barrel
                };
            }
        }

        public WeaponInfo ExplosiveMaterialType;

        public ExplosiveBarrel()
            : base(frames, null, null, Sounds.explosion)
        {

            CanMakeFootsteps = false;
            Health = 50;

            ActorName = "ExplosiveBarrel";


        }
    }
}
