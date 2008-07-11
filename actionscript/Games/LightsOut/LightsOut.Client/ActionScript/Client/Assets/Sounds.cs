using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;

[assembly: ScriptResources(LightsOut.ActionScript.Client.Assets.Sounds.Path)]

namespace LightsOut.ActionScript.Client.Assets
{
    [Script]
    public static class Sounds
    {
        public const string Path = "/assets/LightsOut.Client.Sounds";

        [Embed(source = Path + "/scroll12.mp3")]
        public static Class snd_message;
    }
}
