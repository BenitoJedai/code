using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.MochiLibrary;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;

namespace LightsOut.ActionScript.Client.Monetized
{
    [Script,
    ScriptApplicationEntryPoint(
        Width = TeamPlay.DefaultControlWidth + TeamPlay.NonobaChatWidth, Height = TeamPlay.DefaultControlHeight)]
    [SWF(width = TeamPlay.DefaultControlWidth + TeamPlay.NonobaChatWidth, height = TeamPlay.DefaultControlHeight)]
    public class MochiPreloader : MochiAdPreloaderBase
    {
        public MochiPreloader()
        {
            _mochiads_game_id = "b339f85a1ce0627f";

            showPreGameAd(
                delegate
                {
                    new TeamPlay().AttachTo(stage);
                }
            );
        }
    }
}
