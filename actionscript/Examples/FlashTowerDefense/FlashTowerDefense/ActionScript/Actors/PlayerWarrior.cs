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

        public IEnumerable<Weapon> OtherWeaponsLikeCurrent
        {
            get
            {
                return Weapons.Where(i => i.SelectMode == CurrentWeapon.SelectMode);
            }
        }

        public readonly List<Weapon> Weapons = new List<Weapon>
            {
                new Weapon
                {
                    Ammo = 200,
                    Type = WeaponInfo.Machinegun,
                    SelectMode = Weapon.SelectModeEnum.Turret
                },
                new Weapon
                {
                    Ammo = 100,
                    Type = WeaponInfo.Shotgun,
                    SelectMode = Weapon.SelectModeEnum.Outside
                },
                new Weapon
                {
                    Ammo = 100,
                    Type = WeaponInfo.Shotgun2,
                    SelectMode = Weapon.SelectModeEnum.Outside
                }
            };


        public event Action CurrentWeaponChanged;

        Weapon _CurrentWeapon;

        public Weapon CurrentWeapon
        {
            get
            {
                return _CurrentWeapon;
            }
            set
            {
                if (value == _CurrentWeapon)
                    return;

                _CurrentWeapon = value;
                CurrentWeaponChanged();
            }
        }
    }
}
