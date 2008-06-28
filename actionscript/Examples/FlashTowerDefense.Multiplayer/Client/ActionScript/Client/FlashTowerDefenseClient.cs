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
using FlashTowerDefense.ActionScript.Assets;

using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.Shared.Lambda;

namespace FlashTowerDefense.ActionScript.Client
{


    /// <summary>
    /// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
    /// </summary>
    [Script, ScriptApplicationEntryPoint(Width = Width, Height = Height)]
    [SWF(width = Width, height = Height)]
    public class FlashTowerDefenseClient : Sprite
    {
        public const int Width = FlashTowerDefense.Width + 240;
        public const int Height = FlashTowerDefense.Height;

        /// <summary>
        /// Default constructor
        /// </summary>
        public FlashTowerDefenseClient()
        {
            if (stage == null)
                this.addedToStage +=
                    delegate
                    {
                        Initialize();
                    };
            else
                Initialize();
        }

        public Action<string> ShowMessage = delegate { };

        private void Initialize()
        {
            var c = NonobaAPI.MakeMultiplayer(stage
                , "192.168.3.102"
                );

            c.Init +=
                delegate
                {
                    var Players = new List<PlayerWarrior>();

                    var Map = new FlashTowerDefense();

                    // we need synced enemies
                    Map.CanAutoSpawnEnemies = false;
                    // silence music for testing
                    Map.ToggleMusic();
                    Map.TeleportEgoNearTurret();

                    Map.AttachTo(this);
                    Map.ShowMessage("Running in multiplayer mode!");



                    c.Disconnect +=
                        delegate
                        {
                            Map.ShowMessage("Disconnected!");

                            foreach (var v in Players.ToArray())
                            {
                                v.RemoveFrom(Players).AddDamage(v.health);
                            }
                        };



                    // compiler bug: extension method as instance method, the first param is lost

                    //Func<SharedClass1.Messages, ParamsAction<object>> f = (SharedClass1.Messages j) => j.FixParam((SharedClass1.Messages i, object[] args) => c.SendMessage(i, args));

                    var NetworkMessages = new SharedClass1.RemoteMessages();

                    NetworkMessages.Send = e => c.SendMessage(e.i, e.args);

                    var NetworkEvents = InitializeEvents(Players, Map);

                    // with data
                    Action SendTeleportTo = () => NetworkMessages.TeleportTo(Map.Ego.x.ToInt32(), Map.Ego.y.ToInt32());

                    Map.EgoEnteredMachineGun +=
                        () => NetworkMessages.EnterMachineGun();

                    Map.EgoExitedMachineGun +=
                        delegate
                        {
                            NetworkMessages.ExitMachineGun();
                            SendTeleportTo();
                        };

                    Map.EgoMovedSlowTimer.timer +=
                        delegate
                        {
                            NetworkMessages.WalkTo(Map.Ego.x.ToInt32(), Map.Ego.y.ToInt32());
                        };

                    Map.PrebuiltTurret.AnimationEnabledChanged +=
                        delegate
                        {
                            if (!Map.EgoIsOnTheField())
                            {
                                if (Map.PrebuiltTurret.AnimationEnabled)
                                    NetworkMessages.StartMachineGun();
                                else
                                    NetworkMessages.StopMachineGun();
                            }
                        };

                    Map.EgoFiredShotgun += () => NetworkMessages.FiredShotgun();
                    Map.GameInterlevelBegin += () => NetworkMessages.CancelServerRandomNumbers();
                    Map.GameInterlevelEnd += () => NetworkMessages.ReadyForServerRandomNumbers();

                    Map.NetworkAddDamageFromDirection +=
                        (Id, Damage, Arc) =>
                        {
                            c.SendMessage(SharedClass1.Messages.AddDamageFromDirection,
                                Id,
                                Damage,
                                Arc
                            );
                        };

                    Map.NetworkTakeCrate += Id => NetworkMessages.TakeBox(Id);
                    Map.NetworkShowBulletsFlying +=
                        (X, Y, Degrees, WeaponId) =>
                        {
                            // fixme: compiler bug: 
                            // 1: delegate to an extension method
                            // 2: this block is not surrounded by parentheses
                            // - need to look at the il to figure it out

                            c.SendMessage(SharedClass1.Messages.ShowBulletsFlying,
                               X, Y, Degrees, WeaponId
                            );
                        };

                    500.AtInterval(
                        t => c.SendMessage(SharedClass1.Messages.Ping)
                    );

                    Func<Message, bool> Dispatch =
                        e =>
                        {
                            var type = (SharedClass1.Messages)int.Parse(e.Type);

                            if (NetworkEvents.Dispatch(type,
                                  new SharedClass1.RemoteEvents.DispatchHelper(i => e.length)
                                  {
                                      GetInt32 = e.GetInt,
                                      GetDouble = e.GetNumber,
                                      GetString = e.GetString,
                                  }
                              ))
                                return true;

                            return false;
                        };

                    #region message
                    c.Message +=
                        e =>
                        {
                            //try
                            //{

                            var Dispatched = false;

                            try
                            {
                                Dispatched = Dispatch(e.message);
                            }
                            catch (Exception ex)
                            {
                                System.Console.WriteLine("error at dispatch " + e.message.Type);

                                throw ex;
                            }

                            if (Dispatched)
                                return;

                            System.Console.WriteLine("not on dispatch: " + e.message.Type);

                            var type = (SharedClass1.Messages)int.Parse(e.message.Type);



                            if (type == SharedClass1.Messages.UserJoined)
                            {
                                var n = new PlayerWarrior
                                {
                                    CanMakeFootsteps = false,
                                    RunAnimation = false,
                                    NetworkName = e.message.GetString(0),
                                    NetworkId = e.message.GetInt(1),
                                    x = 100 + 100.Random(),
                                    y = 100 + 100.Random()
                                }.AddTo(Players).AttachTo(Map.GetWarzone());

                                Map.ShowMessage("Player joined: " + n.NetworkName);

                                c.SendMessage(SharedClass1.Messages.ToUserJoinedReply, n.NetworkId);
                                SendTeleportTo();
                            }
                            else if (type == SharedClass1.Messages.UserJoinedReply)
                            {
                                var id = e.message.GetInt(1);

                                if (!Players.Any(n => n.NetworkId == id))
                                {
                                    var n = new PlayerWarrior
                                    {
                                        CanMakeFootsteps = false,
                                        RunAnimation = false,
                                        NetworkName = e.message.GetString(0),
                                        NetworkId = e.message.GetInt(1),
                                        x = 100 + 100.Random(),
                                        y = 100 + 100.Random()
                                    }.AddTo(Players).AttachTo(Map.GetWarzone());

                                    Map.ShowMessage("Player is here: " + n.NetworkName);
                                }
                            }
                            else if (type == SharedClass1.Messages.UserLeft)
                            {
                                var id = e.message.GetInt(1);

                                Map.ShowMessage("Player left: " + e.message.GetString(0));

                                foreach (var v in Players.Where(i => i.NetworkId == id))
                                {
                                    v.RemoveFrom(Players).AddDamage(v.health);
                                }
                            }
                            else if (type == SharedClass1.Messages.UserAddDamageFromDirection)
                            {
                                var uid = e.message.GetInt(0);
                                var id = e.message.GetInt(1);
                                var damage = e.message.GetInt(2);
                                var arc = e.message.GetInt(3).DegreesToRadians();


                                int cc = 0;
                                foreach (var v in Map.BadGuys)
                                {
                                    if (v.NetworkId == id)
                                    {
                                        cc++;

                                        v.AddDamageFromDirection(damage, arc);
                                    }
                                }
                                //m.ShowMessage("damage: " + id + " by " + damage + " : " + cc);
                            }
                            else if (type == SharedClass1.Messages.UserShowBulletsFlying)
                            {
                                var Weapon = WeaponInfo.PredefinedWeapones.SingleOrDefault(i => i.NetworkId == e.message.GetInt(3));

                                Map.ShowBulletsFlying(
                                    new Point
                                    {
                                        x = e.message.GetInt(0),
                                        y = e.message.GetInt(1),
                                    },
                                        e.message.GetInt(2).DegreesToRadians(),
                                        Weapon
                                );



                            }
                            //}
                            //catch
                            //{
                            //    m.ShowMessage("bad message: " + e.message.Type);
                            //}
                        };
                    #endregion

                    SendTeleportTo();

                    c.SendMessage(SharedClass1.Messages.ReadyForServerRandomNumbers);
                };

        }

        private static SharedClass1.RemoteEvents InitializeEvents(List<PlayerWarrior> Players, FlashTowerDefense Map)
        {
            var NetworkEvents = new SharedClass1.RemoteEvents();

            #region Events
            // NetworkEvents // NetworkEvents // NetworkEvents // NetworkEvents // NetworkEvents 
            // NetworkEvents // NetworkEvents // NetworkEvents // NetworkEvents // NetworkEvents 
            // NetworkEvents // NetworkEvents // NetworkEvents // NetworkEvents // NetworkEvents 

            NetworkEvents.UserFiredShotgun += e => Sounds.shotgun2.ToSoundAsset().play();

            NetworkEvents.UserTakeBox +=
                e =>
                {
                    foreach (var v in Map.Boxes)
                    {
                        if (v.NetworkId == e.box)
                        {
                            v.RemoveFrom(Map.Boxes).Orphanize();

                            Sounds.sound20.ToSoundAsset().play();
                        }
                    }
                };

            NetworkEvents.UserWalkTo +=
                e =>
                {
                    foreach (var v in Players.Where(i => i.NetworkId == e.user))
                    {
                        Map.ShowMessage(v.NetworkName + " has been teleported");

                        v.WalkTo(
                            e.x,
                            e.y
                        );
                    }

                };

            NetworkEvents.UserTeleportTo +=
                e =>
                {
                    foreach (var v in Players.Where(i => i.NetworkId == e.user))
                    {
                        Map.ShowMessage(v.NetworkName + " has been teleported");

                        v.MoveTo(
                            e.x,
                            e.y
                        ).AttachTo(Map.GetWarzone());
                    }
                };

            NetworkEvents.ServerRandomNumbers +=
                e =>
                {

                    if (MyExtensions.ByChance_RandomNumbers == null)
                        MyExtensions.ByChance_RandomNumbers = new Queue<double>();
                    else
                        MyExtensions.ByChance_RandomNumbers.Clear();

                    System.Console.WriteLine("Randoms: " + e.e.Length);

                    foreach (var i in e.e)
                    {
                        MyExtensions.ByChance_RandomNumbers.Enqueue(i);
                    }

                    // active warzone
                    if (Map.InterlevelMusic == null)
                        Map.GameEvent();
                };

            NetworkEvents.UserEnterMachineGun +=
                e =>
                {
                    foreach (var v in Players.Where(i => i.NetworkId == e.user))
                    {
                        v.Orphanize();
                    }

                    Map.PrebuiltTurretBlinkTimer.stop();
                    Map.PrebuiltTurret.alpha = 1;
                    Map.PrebuiltTurretInUse = true;
                };

            NetworkEvents.UserExitMachineGun +=
                e =>
                {
                    if (Map.EgoIsOnTheField())
                    {
                        Map.PrebuiltTurretBlinkTimer.start();
                        Map.PrebuiltTurretInUse = false;
                    }
                };

            NetworkEvents.UserStartMachineGun +=
                e =>
                {
                    if (Map.EgoIsOnTheField())
                    {
                        Map.PrebuiltTurret.AnimationEnabled = true;

                        // late info? :)
                        Map.PrebuiltTurretInUse = true;
                    }
                };

            NetworkEvents.UserStopMachineGun +=
                e =>
                {
                    if (Map.EgoIsOnTheField())
                        Map.PrebuiltTurret.AnimationEnabled = false;
                };

            NetworkEvents.ServerMessage +=
                e =>
                {
                    Map.ShowMessage("Server: " + e.text);
                };
            #endregion
            return NetworkEvents;
        }
    }
}