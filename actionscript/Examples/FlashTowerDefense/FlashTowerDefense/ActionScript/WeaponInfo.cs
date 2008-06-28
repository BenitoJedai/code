using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

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

        public readonly static WeaponInfo Shotgun =
            new WeaponInfo
            {
                Range = 200,
                ArcRange = 0.4,
                Damage = 30,
                VisibleBulletLines = 5,

                NetworkId = 1,
            };


        public readonly static WeaponInfo Machinegun =
            new WeaponInfo
            {
                Range = 400,
                ArcRange = 0.1,
                Damage = 40,
                VisibleBulletLines = 2,

                NetworkId = 2,
            };

        public static WeaponInfo[] PredefinedWeapones =
            new[]
            {
                Shotgun,
                Machinegun
            };

    }
}
