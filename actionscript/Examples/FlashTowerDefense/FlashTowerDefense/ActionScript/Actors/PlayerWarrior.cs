using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace FlashTowerDefense.ActionScript.Actors
{
    [Script]
    public class PlayerWarrior : Warrior
    {
        public int MaxHealth = 10000;

        public PlayerWarrior()
        {
            this.health = MaxHealth;
        }

        public WeaponInfo CurrentWeaponType = WeaponInfo.Shotgun;
    }
}
