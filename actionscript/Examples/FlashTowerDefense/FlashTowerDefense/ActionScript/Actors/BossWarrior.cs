using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.filters;
using FlashTowerDefense.ActionScript.Assets;

namespace FlashTowerDefense.ActionScript.Actors
{
    [Script]
    class BossWarrior : Warrior
    {

        public BossWarrior()
        {
            ActorName = "BossWarrior";
            ScoreValue = 4;
            Description = "Respawns once";

            filters = new[] { new GlowFilter((uint)new Random().Next()) };

            PlayHelloSound = () => Sounds.snd_ghoullaugh.ToSoundAsset().play();
        }

    }
}
