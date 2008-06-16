using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Nonoba.api;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript;
using System;
using System.Linq;

using FlashTowerDefense.ActionScript;

using FlashTowerDefense.Shared;
using FlashTowerDefense.ActionScript.Actors;
using System.Collections.Generic;

namespace FlashTowerDefense.ActionScript.Client
{


    /// <summary>
    /// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
    /// </summary>
    [Script, ScriptApplicationEntryPoint(Width = Width, Height = Height)]
    [SWF(width = Width, height = Height)]
    public class FlashTowerDefenseClient : Sprite
    {
        public const int Width = FlashTowerDefense.Width + 200;
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
                    var m = new FlashTowerDefense();

                    // we need synced enemies
                    m.CanAutoSpawnEnemies = false;
                    // silence music for testing
                    m.ToggleMusic();
                    m.TeleportEgoNearTurret();

                    m.AttachTo(this);
                    m.ShowMessage("Running in multiplayer mode!");

                    c.Disconnect +=
                        delegate
                        {
                            m.ShowMessage("Disconnected!");
                        };

                    m.EgoEnteredMachineGun +=
                        () => c.SendMessage(SharedClass1.Messages.EnterMachineGun);

                    Action SendTeleportTo =
                        delegate
                        {
                            c.SendMessage(SharedClass1.Messages.TeleportTo, Convert.ToInt32(m.Ego.x), Convert.ToInt32(m.Ego.y));
                        };

                    m.EgoExitedMachineGun +=
                        delegate
                        {
                            c.SendMessage(SharedClass1.Messages.ExitMachineGun);
                            SendTeleportTo();
                        };

                    m.EgoMovedSlowTimer.timer +=
                        delegate
                        {
                            SendTeleportTo();
                        };

                    m.PrebuiltTurret.AnimationEnabledChanged +=
                        delegate
                        {
                            if (!m.EgoIsOnTheField())
                            {
                                if (m.PrebuiltTurret.AnimationEnabled)
                                    c.SendMessage(SharedClass1.Messages.StartMachineGun);
                                else
                                    c.SendMessage(SharedClass1.Messages.StopMachineGun);
                            }
                        };

                    var Players = new List<Warrior>();

                    #region message
                    c.Message +=
                        e =>
                        {
                            try
                            {
                                var type = (SharedClass1.Messages)int.Parse(e.message.Type);

                                if (type == SharedClass1.Messages.UserJoined)
                                {
                                    var n = new Warrior
                                    {
                                        RunAnimation = false,
                                        NetworkName = e.message.GetString(0),
                                        NetworkId = e.message.GetInt(1),
                                        x = 100 + 100.Random(),
                                        y = 100 + 100.Random()
                                    }.AddTo(Players).AttachTo(m.GetWarzone());

                                    m.ShowMessage("Player joined: " + n.NetworkName);

                                    c.SendMessage(SharedClass1.Messages.ToUserJoinedReply, n.NetworkId);
                                    SendTeleportTo();
                                }
                                else if (type == SharedClass1.Messages.UserJoinedReply)
                                {
                                    var id = e.message.GetInt(1);

                                    if (!Players.Any(n => n.NetworkId == id))
                                    {
                                        var n = new Warrior
                                        {
                                            RunAnimation = false,
                                            NetworkName = e.message.GetString(0),
                                            NetworkId = e.message.GetInt(1),
                                            x = 100 + 100.Random(),
                                            y = 100 + 100.Random()
                                        }.AddTo(Players).AttachTo(m.GetWarzone());

                                        m.ShowMessage("Player is here: " + n.NetworkName);
                                    }
                                }
                                else if (type == SharedClass1.Messages.UserLeft)
                                {
                                    var id = e.message.GetInt(1);

                                    m.ShowMessage("Player left: " + e.message.GetString(0));

                                    foreach (var v in Players.Where(i => i.NetworkId == id))
                                    {
                                        v.RemoveFrom(Players).AddDamage(v.health);
                                    }
                                }
                                else if (type == SharedClass1.Messages.UserEnterMachineGun)
                                {
                                    var id = e.message.GetInt(0);

                                    foreach (var v in Players.Where(i => i.NetworkId == id))
                                    {
                                        v.Orphanize();
                                    }

                                    m.PrebuiltTurretBlinkTimer.stop();
                                    m.PrebuiltTurret.alpha = 1;
                                    m.PrebuiltTurretInUse = true;
                                }
                                else if (type == SharedClass1.Messages.UserExitMachineGun)
                                {


                                    if (m.EgoIsOnTheField())
                                    {
                                        m.PrebuiltTurretBlinkTimer.start();
                                        m.PrebuiltTurretInUse = false;
                                    }
                                }
                                else if (type == SharedClass1.Messages.UserStartMachineGun)
                                {
                                    if (m.EgoIsOnTheField())
                                    {
                                        m.PrebuiltTurret.AnimationEnabled = true;

                                        // late info? :)
                                        m.PrebuiltTurretInUse = true;
                                    }
                                }
                                else if (type == SharedClass1.Messages.UserStopMachineGun)
                                {
                                    if (m.EgoIsOnTheField())
                                        m.PrebuiltTurret.AnimationEnabled = false;
                                }
                                else if (type == SharedClass1.Messages.UserTeleportTo)
                                {

                                    var id = e.message.GetInt(0);

                                    foreach (var v in Players.Where(i => i.NetworkId == id))
                                    {
                                        m.ShowMessage(v.NetworkName + " has been teleported");

                                        v.MoveTo(
                                            e.message.GetInt(1),
                                            e.message.GetInt(2)
                                        ).AttachTo(m.GetWarzone());
                                    }

                                }

                            }
                            catch
                            {
                                m.ShowMessage("bad message: " + e.message.Type);
                            }
                        };
                    #endregion

                    SendTeleportTo();
                };




        }
    }
}