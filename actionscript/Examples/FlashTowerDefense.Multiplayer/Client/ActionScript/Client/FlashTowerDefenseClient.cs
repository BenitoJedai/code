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

        List<PlayerWarrior> Players;
        FlashTowerDefense Map;
        Action BroadcastTeleportTo;
        SharedClass1.RemoteMessages NetworkMessages;
        SharedClass1.RemoteEvents NetworkEvents;

        private void Initialize()
        {

            var c = NonobaAPI.MakeMultiplayer(stage
                , "192.168.3.102"
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
                     Map.ShowMessage("Disconnected!");
                     Map.CanAutoSpawnEnemies = true;

                     foreach (var v in Players.ToArray())
                     {
                         v.RemoveFrom(Players).AddDamage(v.health);
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
                    // silence music for testing
                    Map.ToggleMusic();
                    Map.TeleportEgoNearTurret();

                    Map.AttachTo(this);
                    Map.ShowMessage("Running in multiplayer mode!");


                    // compiler bug: extension method as instance method, the first param is lost

                    //Func<SharedClass1.Messages, ParamsAction<object>> f = (SharedClass1.Messages j) => j.FixParam((SharedClass1.Messages i, object[] args) => c.SendMessage(i, args));

              

                    

                    // with data
                    BroadcastTeleportTo = () => NetworkMessages.TeleportTo(Map.Ego.x.ToInt32(), Map.Ego.y.ToInt32());

                    Map.EgoEnteredMachineGun +=
                        () => NetworkMessages.EnterMachineGun();

                    Map.EgoExitedMachineGun +=
                        delegate
                        {
                            NetworkMessages.ExitMachineGun();
                            BroadcastTeleportTo();
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

                    Map.EgoFiredWeapon += w => NetworkMessages.FiredWeapon(w.Type.NetworkId);
                    Map.GameInterlevelBegin += () => NetworkMessages.CancelServerRandomNumbers();
                    Map.GameInterlevelEnd += () => NetworkMessages.ReadyForServerRandomNumbers();

                    Map.NetworkAddDamageFromDirection += NetworkMessages.AddDamageFromDirection;

                    Map.NetworkTakeCrate += Id => NetworkMessages.TakeBox(Id);
                    Map.NetworkShowBulletsFlying += NetworkMessages.ShowBulletsFlying;

                    500.AtIntervalDo(NetworkMessages.Ping);


                    NetworkMessages.PlayerAdvertise(Map.Ego.NetworkId);
                    BroadcastTeleportTo();
                    NetworkMessages.ReadyForServerRandomNumbers();
                };

        }

    }
}