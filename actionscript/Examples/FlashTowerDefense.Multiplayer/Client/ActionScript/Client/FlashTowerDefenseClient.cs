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

                            foreach (var v in Players.ToArray())
                            {
                                v.RemoveFrom(Players).AddDamage(v.health);
                            }
                        };

                    m.EgoEnteredMachineGun +=
                        () => c.SendMessage(SharedClass1.Messages.EnterMachineGun);


                    // compiler bug: extension method as instance method, the first param is lost

                    //Func<SharedClass1.Messages, ParamsAction<object>> f = (SharedClass1.Messages j) => j.FixParam((SharedClass1.Messages i, object[] args) => c.SendMessage(i, args));

                    var NetworkMessages = new SharedClass1.RemoteMessages();

                    NetworkMessages.Send = e => c.SendMessage(e.i, e.args);

                    var NetworkEvents = new SharedClass1.RemoteEvents();

                    #region Events
                    NetworkEvents.UserFiredShotgun += e => Sounds.shotgun2.ToSoundAsset().play();

                    NetworkEvents.UserTakeBox +=
                        e =>
                        {
                            foreach (var v in m.Boxes)
                            {
                                if (v.NetworkId == e.box)
                                {
                                    v.RemoveFrom(m.Boxes).Orphanize();

                                    Sounds.sound20.ToSoundAsset().play();
                                }
                            }
                        };

                    NetworkEvents.UserTeleportTo +=
                        e =>
                        {
                            if (e == null)
                                throw new ArgumentNullException("e");

                            if (Players == null)
                                throw new ArgumentNullException("Players");

                            foreach (var v in Players.Where(i =>
                                {
                                    if (i == null)
                                        throw new ArgumentNullException("i");

                                    if (e == null)
                                        throw new ArgumentNullException("e");
                          

                                    return i.NetworkId == e.user; }))
                            {
                                m.ShowMessage(v.NetworkName + " has been teleported");

                                v.MoveTo(
                                    e.x,
                                    e.y
                                ).AttachTo(m.GetWarzone());
                            }
                        };
                    #endregion

                    // with data
                    Action SendTeleportTo = () => NetworkMessages.TeleportTo(m.Ego.x.ToInt32(), m.Ego.y.ToInt32());


                    m.EgoExitedMachineGun +=
                        delegate
                        {
                            c.SendMessage(SharedClass1.Messages.ExitMachineGun);
                            SendTeleportTo();
                        };

                    m.EgoMovedSlowTimer.timer +=
                        delegate
                        {
                            c.SendMessage(SharedClass1.Messages.WalkTo, Convert.ToInt32(m.Ego.x), Convert.ToInt32(m.Ego.y));
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

                    m.EgoFiredShotgun += () => NetworkMessages.FiredShotgun();
                    m.GameInterlevelBegin += () => NetworkMessages.CancelServerRandomNumbers();
                    m.GameInterlevelEnd += () => NetworkMessages.ReadyForServerRandomNumbers();

                    m.NetworkAddDamageFromDirection +=
                        (Id, Damage, Arc) =>
                        {
                            c.SendMessage(SharedClass1.Messages.AddDamageFromDirection,
                                Id,
                                Damage,
                                Arc
                            );
                        };

                    m.NetworkTakeCrate += Id => NetworkMessages.TakeBox(Id);
                    m.NetworkShowBulletsFlying +=
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
                                  new SharedClass1.RemoteEvents.DispatchHelper
                                  {
                                      GetInt32 = e.GetInt,
                                      GetDouble = e.GetNumber,
                                      GetString = e.GetString
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
                                    var n = new PlayerWarrior
                                    {
                                        CanMakeFootsteps = false,
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
                            //else if (type == SharedClass1.Messages.UserTeleportTo)
                            //{
                            //    var id = e.message.GetInt(0);

                            //    foreach (var v in Players.Where(i => i.NetworkId == id))
                            //    {
                            //        m.ShowMessage(v.NetworkName + " has been teleported");

                            //        v.MoveTo(
                            //            e.message.GetInt(1),
                            //            e.message.GetInt(2)
                            //        ).AttachTo(m.GetWarzone());
                            //    }

                            //}
                            else if (type == SharedClass1.Messages.UserWalkTo)
                            {

                                var id = e.message.GetInt(0);

                                foreach (var v in Players.Where(i => i.NetworkId == id))
                                {
                                    v.WalkTo(
                                         e.message.GetInt(1),
                                        e.message.GetInt(2)
                                    );

                                }

                            }
                            //else if (type == SharedClass1.Messages.UserFiredShotgun)
                            //{
                            //    Sounds.shotgun2.ToSoundAsset().play();
                            //}
                            else if (type == SharedClass1.Messages.ServerMessage)
                            {
                                m.ShowMessage("Server: " + e.message.GetString(0));
                            }
                            else if (type == SharedClass1.Messages.ServerRandomNumbers)
                            {
                                if (MyExtensions.ByChance_RandomNumbers == null)
                                    MyExtensions.ByChance_RandomNumbers = new Queue<double>();
                                else
                                    MyExtensions.ByChance_RandomNumbers.Clear();

                                for (uint i = 0; i < e.message.length; i++)
                                {
                                    MyExtensions.ByChance_RandomNumbers.Enqueue(e.message.GetNumber(i));
                                }

                                // active warzone
                                if (m.InterlevelMusic == null)
                                    m.GameEvent();
                            }
                            else if (type == SharedClass1.Messages.UserAddDamageFromDirection)
                            {
                                var uid = e.message.GetInt(0);
                                var id = e.message.GetInt(1);
                                var damage = e.message.GetInt(2);
                                var arc = e.message.GetInt(3).DegreesToRadians();


                                int cc = 0;
                                foreach (var v in m.BadGuys)
                                {
                                    if (v.NetworkId == id)
                                    {
                                        cc++;

                                        v.AddDamageFromDirection(damage, arc);
                                    }
                                }
                                //m.ShowMessage("damage: " + id + " by " + damage + " : " + cc);
                            }
                            //else if (type == SharedClass1.Messages.UserTakeBox)
                            //{
                            //    var uid = e.message.GetInt(0);
                            //    var id = e.message.GetInt(1);


                            //    int cc = 0;
                            //    foreach (var v in m.Boxes)
                            //    {
                            //        if (v.NetworkId == id)
                            //        {
                            //            cc++;

                            //            v.RemoveFrom(m.Boxes).Orphanize();

                            //            Sounds.sound20.ToSoundAsset().play();
                            //        }
                            //    }
                            //    //m.ShowMessage("damage: " + id + " by " + damage + " : " + cc);
                            //}
                            else if (type == SharedClass1.Messages.UserShowBulletsFlying)
                            {
                                var Weapon = WeaponInfo.PredefinedWeapones.SingleOrDefault(i => i.NetworkId == e.message.GetInt(3));

                                m.ShowBulletsFlying(
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
    }
}