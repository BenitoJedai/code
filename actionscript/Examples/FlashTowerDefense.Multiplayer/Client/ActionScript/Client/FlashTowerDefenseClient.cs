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
        public const int Width = FlashTowerDefense.Width  + 200;
        public const int Height = FlashTowerDefense.Height;

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

                    // we need synced enemies
                    m.CanAutoSpawnEnemies = false;

                    m.AttachTo(this);
                    m.ShowMessage("Running in multiplayer mode!");

                    m.EgoEnteredMachineGun +=
                        () => c.SendMessage(SharedClass1.Messages.EnterMachineGun);

                    m.EgoExitedMachineGun +=
                        () => c.SendMessage(SharedClass1.Messages.ExitMachineGun);


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
                                else if (type == SharedClass1.Messages.UserEnterMachineGun)
                                    m.ShowMessage("Player entered machinegun: " + e.message.GetString(0));
                                else if (type == SharedClass1.Messages.UserExitMachineGun)
                                    m.ShowMessage("Player exited machinegun: " + e.message.GetString(0));
                            }
                            catch
                            {
                                m.ShowMessage("bad message: " + e.message.Type);
                            }
                        };

                };




        }
    }
}