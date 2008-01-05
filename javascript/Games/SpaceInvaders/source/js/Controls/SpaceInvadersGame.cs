using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScriptCoreLib;
using ScriptCoreLib.JavaScript.Extensions;

namespace SpaceInvaders.source.js.Controls
{
    [Script, Serializable]
    public sealed class GameSettings
    {
        public string Location;
    }

    [Script, ScriptApplicationEntryPoint(IsClickOnce = true)]
    public class SpaceInvadersGame
    {
        public static readonly GameSettings DefaultData =
            new GameSettings
                {
                    //Location = "http://jsc.sourceforge.net/examples/web/SpaceInvaders/"
                    Location = ""
                };

        public SpaceInvadersGame(GameSettings Data)
        {
            SpaceInvaders.SpawnSpaceInvaders(Data.Location);
        }

        static SpaceInvadersGame()
        {
            var KnownTypes = new object[] { 
                    new GameSettings() 
                };

            typeof(SpaceInvadersGame).SpawnTo<GameSettings>(KnownTypes, i => new SpaceInvadersGame(i));
        }
    }

}
