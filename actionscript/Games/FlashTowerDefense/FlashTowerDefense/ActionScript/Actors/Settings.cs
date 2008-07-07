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
        public Settings()
        {
            KnownActors =
                new Actor[]
                {
                    new Sheep(),
                    new Warrior(),
                    new NuclearWarrior(),
                    new BossWarrior(),
                    new BossSheep()
                };
        }
        public readonly Actor[] KnownActors;
    }
}
