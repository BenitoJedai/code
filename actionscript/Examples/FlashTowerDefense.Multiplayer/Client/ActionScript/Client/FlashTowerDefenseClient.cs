using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Nonoba.api;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript;
using System;

using FlashTowerDefense.ActionScript;

namespace FlashTowerDefense.ActionScript.Client
{


    /// <summary>
    /// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
    /// </summary>
    [Script, ScriptApplicationEntryPoint(Width = Width, Height = Height)]
    [SWF(width = Width, height = Height)]
    public class FlashTowerDefenseClient : Sprite
    {
        public const int Width = 1000;
        public const int Height = 600;

        /// <summary>
        /// Default constructor
        /// </summary>
        public FlashTowerDefenseClient()
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

            c.Init +=
                delegate
                {
                    var m = new Menu();

                    m.AttachTo(this);
                };

            c.Send("hello, powered by jsc");

           

        }
    }
}