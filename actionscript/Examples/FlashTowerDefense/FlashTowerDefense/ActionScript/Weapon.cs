using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace FlashTowerDefense.ActionScript
{
    [Script]
    public class Weapon
    {
        public WeaponInfo Type;

        public int Ammo;

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
    }
}
