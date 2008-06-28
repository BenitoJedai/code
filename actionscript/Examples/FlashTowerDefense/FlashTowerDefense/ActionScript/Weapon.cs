using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;

namespace FlashTowerDefense.ActionScript
{
    [Script]
    public class Weapon
    {
        public string Name;

        public int NetworkId;

        public WeaponInfo Type;

        public int MaxAmmo;

        int _Ammo;


        public Weapon Clone()
        {
            return new Weapon
            {
                Name = Name,
                NetworkId = NetworkId,
                Type = Type,
                MaxAmmo = MaxAmmo,
                _Ammo = _Ammo,
                SelectMode = SelectMode
            };
        }

        public int Ammo
        {
            get
            {
                return _Ammo;
            }
            set
            {
                if (_Ammo == value)
                    return;

                _Ammo = value;

                if (MaxAmmo > 0)
                {
                    if (_Ammo > MaxAmmo)
                        _Ammo = MaxAmmo;
                }

                if (AmmoChanged != null)
                    AmmoChanged();
            }
        }

        public event Action AmmoChanged;

        [Script]
        public enum SelectModeEnum
        {
            Unknown,
            Outside,
            Turret
        }

        public SelectModeEnum SelectMode;

        public uint Color
        {
            get
            {
                if (SelectMode == SelectModeEnum.Turret)
                    return 0xf36d21;

                return 0x00ff00;
            }
        }

        
        public static Weapon TurretMachinegun =
            new Weapon
                {
                    MaxAmmo = 200,

                    Ammo = 50,
                    Type = WeaponInfo.Machinegun,
                    SelectMode = Weapon.SelectModeEnum.Turret,
                    Name = "Turret Machinegun"
                };

        public static Weapon Shotgun =
                new Weapon
                {
                    MaxAmmo = 14,

                    Ammo = 7,
                    Type = WeaponInfo.Shotgun,
                    SelectMode = Weapon.SelectModeEnum.Outside,
                    Name = "Shotgun"
                };

        public static Weapon Shotgun2 =
           new Weapon
           {
               MaxAmmo = 14,
               Ammo = 7,
               Type = WeaponInfo.Shotgun2,
               SelectMode = Weapon.SelectModeEnum.Outside,
               Name = "Long Shotgun"
           };

        public static Weapon Colt45 =
                new Weapon
                {
                    MaxAmmo = 15,
                    Ammo = 6,
                    Type = WeaponInfo.Colt45,
                    SelectMode = Weapon.SelectModeEnum.Outside,
                    Name = "Colt45"

                };

        public static Weapon[] PredefinedWeapons =
             new[]
            {
                Shotgun,
                Shotgun2,
                Colt45,
                TurretMachinegun
            }.ForEach((w, i) => w.NetworkId = i).ToArray();

        
    }
}
