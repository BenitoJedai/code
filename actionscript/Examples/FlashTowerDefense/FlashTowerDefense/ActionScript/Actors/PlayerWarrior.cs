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
        public const int AutoResurrectDelay = 6000;


        public PlayerWarrior()
        {
            this.MaxHealth = 1000;
            this.Health = MaxHealth;

            this.Weapons = new List<Weapon>
            {
                Weapon.TurretMachinegun.Clone(),
                Weapon.Shotgun.Clone(),
                Weapon.Colt45.Clone(),
                Weapon.ExplosivesBarrel.Clone(),
            };

        }

        public IEnumerable<Weapon> OtherWeaponsLikeCurrent
        {
            get
            {
                return Weapons.Where(i => i.SelectMode == CurrentWeapon.SelectMode);
            }
        }

        public readonly List<Weapon> Weapons;


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
