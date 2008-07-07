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
                SelectMode = SelectMode,
                Usage = Usage
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

        public const uint ColorForTurretWeapons = 0xf36d21;
        public const uint ColorForOutsideWeapons = 0x00ff00;
        public const uint ColorForDeployableWeapons = 0x66ffff;

        public enum UsageEnum
        {
            FireBullets,
            DeployBarrel
        }

        public UsageEnum Usage = UsageEnum.FireBullets;

        public uint Color
        {
            get
            {
               
                if (SelectMode == SelectModeEnum.Turret)
                    return ColorForTurretWeapons;

                if (Usage == UsageEnum.FireBullets)
                    return ColorForOutsideWeapons;

                return ColorForDeployableWeapons;
            }
        }


        public static Weapon Dagger =
            new Weapon
            {
                MaxAmmo = 200,
                Ammo = 20,
                Type = WeaponInfo.Dagger,
                SelectMode = Weapon.SelectModeEnum.Outside,
                Name = "Dagger",
                Usage = UsageEnum.FireBullets
            };

        public static Weapon MedivalAxe =
            new Weapon
            {
                MaxAmmo = 400,
                Ammo = 40,
                Type = WeaponInfo.MedivalAxe,
                SelectMode = Weapon.SelectModeEnum.Outside,
                Name = "MedivalAxe",
                Usage = UsageEnum.FireBullets
            };


        public static Weapon ExplosivesBarrel =
            new Weapon
            {
                MaxAmmo = 20,
                Ammo = 7,
                Type = WeaponInfo.ExplosivesBarrel,
                SelectMode = Weapon.SelectModeEnum.Outside,
                Name = "ExplosivesBarrel",
                Usage = UsageEnum.DeployBarrel
            };


        public static Weapon BrickWall =
            new Weapon
            {
                MaxAmmo = 20,
                Ammo = 7,
                Type = WeaponInfo.BrickWall,
                SelectMode = Weapon.SelectModeEnum.Outside,
                Name = "BrickWall",
                Usage = UsageEnum.DeployBarrel
            };


        public static Weapon TurretMachinegun =
            new Weapon
                {
                    MaxAmmo = 400,

                    Ammo = 100,
                    Type = WeaponInfo.Machinegun,
                    SelectMode = Weapon.SelectModeEnum.Turret,
                    Name = "Turret Machinegun",
                };

        public static Weapon Shotgun =
                new Weapon
                {
                    MaxAmmo = 30,

                    Ammo = 12,
                    Type = WeaponInfo.Shotgun,
                    SelectMode = Weapon.SelectModeEnum.Outside,
                    Name = "Shotgun"
                };

        public static Weapon Shotgun2 =
           new Weapon
           {
               MaxAmmo = 35,
               Ammo = 14,
               Type = WeaponInfo.Shotgun2,
               SelectMode = Weapon.SelectModeEnum.Outside,
               Name = "Long Shotgun"
           };

        public static Weapon Colt45 =
                new Weapon
                {
                    MaxAmmo = 30,
                    Ammo = 12,
                    Type = WeaponInfo.Colt45,
                    SelectMode = Weapon.SelectModeEnum.Outside,
                    Name = "Colt45"

                };

        public static Weapon[] PredefinedWeapons =
             new[]
            {

                ExplosivesBarrel,
                BrickWall,
                Shotgun,
                Shotgun2,
                Colt45,
                TurretMachinegun,
                MedivalAxe,
                Dagger


            }.ForEach((w, i) => w.NetworkId = i).ToArray();


    }
}
