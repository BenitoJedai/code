using System;
using System.Collections.Generic;
using System.Text;
using Nonoba.GameLibrary;

namespace FlashTowerDefense.Server.Generic
{
    public abstract class VirtualServerPlayerBase<TVirtualPlayer> : NonobaGameUser
    {
        public TVirtualPlayer Virtual { get; set; }

        public override Dictionary<string, string> GetDebugValues()
        {
            return new Dictionary<string, string> { };
        }
    }
}
