using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.filters;

namespace FlashTowerDefense.ActionScript.Actors
{
    [Script]
    class BossSheep : Sheep
    {
        public BossSheep()
        {
            ActorName = "BossSheep";
            ScoreValue = 8;
            Description = "Respawns";
            //PlayHelloSound += () => Assets.snd_sheep.ToSoundAsset().play();


            filters = new[] { new GlowFilter((uint)new Random().Next()) };
        }
    }
}
