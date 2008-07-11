using System;
using System.Collections.Generic;
using System.Text;
using Nonoba.GameLibrary;
using ScriptCoreLib;

namespace LightsOut.Server.Generic
{
    [Script]
    public abstract class VirtualServerPlayerBase<TVirtualPlayer> : NonobaGameUser
    {
        public TVirtualPlayer Virtual { get; set; }

        public override Dictionary<string, string> GetDebugValues()
        {
            return new Dictionary<string, string> { };
        }
    }
}
