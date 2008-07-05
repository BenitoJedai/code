using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;

[assembly: ScriptResources(FlashMinesweeper.ActionScript.Client.Assets.Sounds.Path)]


namespace FlashMinesweeper.ActionScript.Client.Assets
{
    [Script]
    public static class Sounds
    {
        public const string Path = "/assets/FlashMinesweeper.Client.Sounds";

        [Embed(source = Path + "/scroll12.mp3")]
        public static Class snd_message;
    }
}
