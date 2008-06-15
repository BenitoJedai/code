﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.filters;

namespace FlashTowerDefense.ActionScript.Actors
{
    [Script]
    public class NuclearWarrior : Warrior
    {
        public NuclearWarrior()
        {
            ActorName = "NuclearWarrior";
            ScoreValue = 3;
            
            var t = 200.AtIntervalOnRandom(
                            delegate
                            {
                                this.filters = new[] { new GlowFilter((uint)0xffffff.Random()) };
                            }
                        );

            this.Die += t.stop;
        }
    }
}
