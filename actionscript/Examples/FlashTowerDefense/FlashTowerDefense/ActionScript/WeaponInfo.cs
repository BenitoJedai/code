using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.ActionScript.mx.core;
using FlashTowerDefense.ActionScript.Assets;
using ScriptCoreLib.ActionScript;

namespace FlashTowerDefense.ActionScript
{
    [Script]
    public class WeaponInfo
    {
        public bool CanHurtEgo = false;

        public double Range;
        public double ArcRange;

        public int Damage;
        public int NetworkId;

        public int VisibleBulletLines;

        public BitmapAsset Avatar;

        public Class SoundFire;

        public readonly static WeaponInfo Shotgun =
            new WeaponInfo
            {
                Range = 200,
                ArcRange = 0.4,
                Damage = 30,
                VisibleBulletLines = 6,
                SoundFire = Sounds.shotgun2,
                Avatar = Images.avatars_shotgun,

            };

        public readonly static WeaponInfo Shotgun2 =
        new WeaponInfo
        {
            Range = 300,
            ArcRange = 0.2,
            Damage = 60,
            VisibleBulletLines = 3,
            SoundFire = Sounds.shotgun2,
            Avatar = Images.avatars_shotgun2,

        };


        public readonly static WeaponInfo Machinegun =
            new WeaponInfo
            {
                Range = 400,
                ArcRange = 0.1,
                Damage = 40,
                VisibleBulletLines = 2,

                Avatar = Images.avatars_m249,

            };

        public readonly static WeaponInfo Colt45 =
             new WeaponInfo
             {
                 Range = 200,
                 ArcRange = 0.1,
                 Damage = 100,
                 VisibleBulletLines = 1,
                 SoundFire = Sounds.colt45,
                 Avatar = Images.avatars_colt45,

             };

        public static WeaponInfo[] PredefinedWeaponTypes =
            new[]
            {
                Shotgun,
                Shotgun2,
                Colt45,
                Machinegun

            }.ForEach((w, i) => w.NetworkId = i).ToArray();

        

    }
}
