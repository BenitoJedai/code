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
        public double Range;
        public double ArcRange;

        public int Damage;

        public readonly static WeaponInfo Shotgun =
            new WeaponInfo
            {
                Range = 200,
                ArcRange = 0.4,
                Damage = 30,
                VisibleBulletLines = 5
            };


        public readonly static WeaponInfo Machinegun =
            new WeaponInfo
            {
                Range = 400,
                ArcRange = 0.1,
                Damage = 40,
                VisibleBulletLines = 2,
            };

        public int VisibleBulletLines;
    }
}
