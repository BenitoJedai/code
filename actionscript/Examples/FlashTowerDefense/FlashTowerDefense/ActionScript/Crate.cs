using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using FlashTowerDefense.ActionScript.Assets;

namespace FlashTowerDefense.ActionScript
{
    [Script]
    public class Crate : Animation
    {
        public Weapon WeaponInside;

        public Crate()
            : base(Images.box)
        {

        }
    }
}
