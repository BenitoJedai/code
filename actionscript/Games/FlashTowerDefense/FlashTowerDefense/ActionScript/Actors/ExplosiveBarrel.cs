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

        public Weapon ExplosiveMaterialType;

        public ExplosiveBarrel()
            : base(frames, null, null, null)
        {
            
            CanMakeFootsteps = false;
            MaxHealth = 300;
            Health = 300;

            ActorName = "ExplosiveBarrel";


        }
    }
}
