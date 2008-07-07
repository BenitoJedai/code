using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.filters;

namespace FlashTowerDefense.ActionScript.Actors
{
    [Script]
    public class NuclearSheep : Sheep
    {
        public NuclearSheep()
        {
            ActorName = "NuclearSheep";
            ScoreValue = 3;

            MaxHealth = 200;
            Health = 200;

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
