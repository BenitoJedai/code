using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Nonoba.api;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript;
using System;

using FlashTowerDefense.ActionScript;
using FlashTowerDefense.Shared;

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
          
            c.Init +=
                delegate
                {
                    var m = new Menu();

                    m.AttachTo(this);
                    m.ShowMessage("Running in multiplayer mode!");

                    c.Message +=
                        e =>
                        {
                            try
                            {
                                var type = (SharedClass1.Messages)int.Parse(e.message.Type);

                                if (type == SharedClass1.Messages.UserJoined)
                                    m.ShowMessage("Player joined: " + e.message.GetString(0));
                                else if (type == SharedClass1.Messages.UserLeft)
                                    m.ShowMessage("Player left: " + e.message.GetString(0));
                            }
                            catch
                            {
                                m.ShowMessage("bad message: " + e.message.Type);
                            }
                        };


                };

            c.Send("hello, powered by jsc");



        }
    }
}