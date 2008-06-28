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
                Weapon.TurretMachinegun.Clone(),
                Weapon.Shotgun.Clone(),
                Weapon.Colt45.Clone()
            };

        #region CurrentWeapon
        public event Action CurrentWeaponAmmoChanged;

        public event Action CurrentWeaponChanged;

        Weapon _CurrentWeapon;

        void RaiseCurrentWeaponAmmoChanged()
        {
            if (CurrentWeaponAmmoChanged != null)
                CurrentWeaponAmmoChanged();
        }

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

                if (_CurrentWeapon != null)
                {
                    _CurrentWeapon.AmmoChanged -= RaiseCurrentWeaponAmmoChanged;

                    if (!Weapons.Any(i => i.NetworkId == value.NetworkId))
                        Weapons.Add(value); 
                }

                _CurrentWeapon = value;

                

                if (CurrentWeaponChanged != null)
                    CurrentWeaponChanged();

                if (_CurrentWeapon != null)
                    _CurrentWeapon.AmmoChanged += RaiseCurrentWeaponAmmoChanged;

                RaiseCurrentWeaponAmmoChanged();
            }
        }
        #endregion

    }
}
