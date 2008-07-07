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
    public class BrickWall : Actor
    {
        static Bitmap[] frames
        {
            get
            {
                return new Bitmap[]
                {
                    Images.bricks
                };
            }
        }

        public Weapon Material;

        public BrickWall()
            : base(frames, null, null, null)
        {
            
            CanMakeFootsteps = false;
            MaxHealth = 3000;
            Health = 3000;

            ActorName = "BrickWall";


        }
    }
}
