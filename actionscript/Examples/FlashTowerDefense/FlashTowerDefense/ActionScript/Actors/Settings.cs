using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace FlashTowerDefense.ActionScript.Actors
{
    [Script]
    public class Settings
    {
        public readonly Actor[] KnownActors =
            new Actor[]
            {
                new Sheep(),
                new Warrior(),
                new BossWarrior(),
                new BossSheep()
            };
    }
}
