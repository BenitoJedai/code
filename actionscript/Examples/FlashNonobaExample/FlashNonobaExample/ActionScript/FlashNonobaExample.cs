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
            }.AttachTo(this);


            c.Message +=
                u =>
                {


                    p.text = "message2: " + u.message.Type + " " + u.message.length;
                };


            c.Send("hello, powered by jsc");
        }
    }
}