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
    public partial class FlashTowerDefenseClient : Sprite
    {
        public const int Width = FlashTowerDefense.DefaultWidth + 240;
        public const int Height = FlashTowerDefense.DefaultHeight;

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

        List<PlayerWarrior> Players;
        FlashTowerDefense Map;
        Action BroadcastTeleportTo;
        SharedClass1.RemoteMessages NetworkMessages;
        SharedClass1.RemoteEvents NetworkEvents;

        private void Initialize()
        {

            var c = NonobaAPI.MakeMultiplayer(stage
                //, "192.168.3.102"
                    //, "192.168.1.119"
                );

            NetworkEvents = InitializeEvents();

            NetworkMessages = new SharedClass1.RemoteMessages
            {
                Send = e => c.SendMessage(e.i, e.args)
            };

          


            Func<Message, bool> Dispatch =
                e =>
                {
                    var type = (SharedClass1.Messages)int.Parse(e.Type);

                    if (NetworkEvents.Dispatch(type,
                          new SharedClass1.RemoteEvents.DispatchHelper
                          {
                              GetLength = i => e.length,
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

                };
            #endregion

            c.Disconnect +=
                 delegate
                 {
                     if (Map == null)
                         throw new Exception("Cannot connect to server at the moment");

                     Map.ShowMessage("Disconnected!");
                     Map.CanAutoSpawnEnemies = true;

                     foreach (var v in Players.ToArray())
                     {
                         v.RemoveFrom(Players).AddDamage(v.Health);
                     }
                 };



            c.Init +=
                delegate
                {
                    Map = new FlashTowerDefense();
                    Map.Ego.NetworkId = this.EgoId;

                    Players = new List<PlayerWarrior>();


                    Map.GoodGuys.Add(
                        delegate
                        {
                            return Players.Select(i => (Actor)i);
                        });




                    // we need synced enemies
                    Map.CanAutoSpawnEnemies = false;
                    Map.ReportDaysTimer.stop();

                    // silence music for testing
                    Map.ToggleMusic();
                    Map.TeleportEgoNearTurret();

                    Map.AttachTo(this);
                    Map.ShowMessage("Running in multiplayer mode!");


                    // compiler bug: extension method as instance method, the first param is lost

                    //Func<SharedClass1.Messages, ParamsAction<object>> f = (SharedClass1.Messages j) => j.FixParam((SharedClass1.Messages i, object[] args) => c.SendMessage(i, args));


                    var ToOthers = (SharedClass1.IPairedMessagesWithoutUser) NetworkMessages;


                    

                    // with data
                    BroadcastTeleportTo = () => ToOthers.TeleportTo(Map.Ego.x.ToInt32(), Map.Ego.y.ToInt32());

                    Map.EgoEnteredMachineGun +=
                        () => ToOthers.EnterMachineGun();

                    Map.EgoExitedMachineGun +=
                        delegate
                        {
                            ToOthers.ExitMachineGun();
                            BroadcastTeleportTo();
                        };

                    Map.EgoMovedSlowTimer.timer +=
                        delegate
                        {
                            ToOthers.WalkTo(Map.Ego.x.ToInt32(), Map.Ego.y.ToInt32());
                        };

                    Map.PrebuiltTurret.AnimationEnabledChanged +=
                        delegate
                        {
                            if (!Map.EgoIsOnTheField())
                            {
                                if (Map.PrebuiltTurret.AnimationEnabled)
                                    ToOthers.StartMachineGun();
                                else
                                    ToOthers.StopMachineGun();
                            }
                        };

                    Map.EgoFiredWeapon += w => ToOthers.FiredWeapon(w.Type.NetworkId);
                    Map.EgoResurrect += ToOthers.PlayerResurrect;

                    Map.GameInterlevelBegin += () => NetworkMessages.CancelServerRandomNumbers();
                    Map.GameInterlevelEnd += () => NetworkMessages.ReadyForServerRandomNumbers();

                    Map.NetworkAddDamageFromDirection += ToOthers.AddDamageFromDirection;
                    Map.NetworkAddDamage += ToOthers.AddDamage;

                    Map.NetworkTakeCrate += Id => ToOthers.TakeBox(Id);
                    Map.NetworkShowBulletsFlying += ToOthers.ShowBulletsFlying;

                    Map.NetworkDeployExplosiveBarrel += ToOthers.DeployExplosiveBarrel;
                    Map.NetworkUndeployExplosiveBarrel += ToOthers.UndeployExplosiveBarrel;


                    500.AtIntervalDo(NetworkMessages.Ping);


                    NetworkMessages.PlayerAdvertise(Map.Ego.NetworkId);

                    BroadcastTeleportTo();

                    300.AtDelayDo(NetworkMessages.ReadyForServerRandomNumbers);
                };

        }

    }
}