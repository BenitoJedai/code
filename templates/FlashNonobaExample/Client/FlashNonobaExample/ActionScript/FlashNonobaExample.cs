using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Nonoba.api;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript;
using System;

namespace FlashNonobaExample.ActionScript
{


    /// <summary>
    /// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    public class FlashNonobaExample : Sprite
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public FlashNonobaExample()
        {
            var c = NonobaAPI.MakeMultiplayer(stage);

            var p = new TextField
            {
                border = true,
                width = 200,
                height = 200
            }.AttachTo(this);

            c.Message +=
                e =>
                {
                    p.text += "message2: " + e.message.Type + " " + e.message.length + "\n";
                };


            c.Send("hello, powered by jsc");
        }
    }
}