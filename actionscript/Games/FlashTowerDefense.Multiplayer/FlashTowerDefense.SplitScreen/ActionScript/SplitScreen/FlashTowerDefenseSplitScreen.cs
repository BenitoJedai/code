﻿using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Extensions;
using System.Collections.Generic;
using FlashTowerDefense.ActionScript;
using ScriptCoreLib.ActionScript;
using System;
using FlashTowerDefense.Shared;
using FlashTowerDefense.ActionScript.Client;

namespace FlashTowerDefense.ActionScript.SplitScreen
{
    /// <summary>
    /// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    [SWF(width = DefaultWidth, height = DefaultHeight, backgroundColor = 0)]
    public class FlashTowerDefenseSplitScreen : Sprite
    {
        public const int ReallyNarrowWidth = 420;


        public const int Padding = 2;

        public const int DefaultWidth = ReallyNarrowWidth * 2 + Padding;
        public const int DefaultHeight = FlashTowerDefense.DefaultHeight;

        /// <summary>
        /// Default constructor
        /// </summary>
        public FlashTowerDefenseSplitScreen()
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

        private void Initialize()
        {

            #region CreateView
            Func<FlashTowerDefenseSized> CreateView =
                delegate
                {

                    var v = new FlashTowerDefenseSized(ReallyNarrowWidth, FlashTowerDefense.DefaultHeight);

                    var v_mask = new Shape();
                    v_mask.graphics.beginFill(0xffffff);
                    v_mask.graphics.drawRect(0, 0, ReallyNarrowWidth, FlashTowerDefense.DefaultHeight);
                    v_mask.graphics.endFill();

                    v.mask = v_mask;
                    v_mask.AttachTo(v);

                    return v;
                };
            #endregion


            // now we need a bridge and a server

            var server = new Game
            {
                AtInterval = (a, i) => i.AtIntervalDo(a).stop,
                AtDelay = (a, i) => i.AtDelayDo(a).stop,
            };

            var left_to_server = new global::FlashTowerDefense.Shared.SharedClass1.Bridge();
            var server_to_left = new global::FlashTowerDefense.Shared.SharedClass1.Bridge();

            var right_to_server = new global::FlashTowerDefense.Shared.SharedClass1.Bridge();
            var server_to_right = new global::FlashTowerDefense.Shared.SharedClass1.Bridge();

            var player_left = new Player
            {
                FromPlayer = left_to_server,
                ToPlayer = server_to_left,
                ToOthers = server_to_right,
                UserId = 0,
                Username = "Lefty",
                AddScore = delegate { }
            };


            Action<SharedClass1.IEvents, SharedClass1.RemoteEvents.WithUserArgumentsRouter_Broadcast> AttachRouter =
                (from, _Router) =>
                {
                    from.TeleportTo += _Router.UserTeleportTo;
                    from.WalkTo += _Router.UserWalkTo;
                    from.TakeBox += _Router.UserTakeBox;
                    from.FiredWeapon += _Router.UserFiredWeapon;
                    from.EnterMachineGun += _Router.UserEnterMachineGun;
                    from.ExitMachineGun += _Router.UserExitMachineGun;
                    from.StartMachineGun += _Router.UserStartMachineGun;
                    from.StopMachineGun += _Router.UserStopMachineGun;
                    from.AddDamage += _Router.UserAddDamage;
                    from.AddDamageFromDirection += _Router.UserAddDamageFromDirection;
                    from.ShowBulletsFlying += _Router.UserShowBulletsFlying;
                    from.PlayerResurrect += _Router.UserPlayerResurrect;
                    from.UndeployExplosiveBarrel += _Router.UserUndeployExplosiveBarrel;
                    from.DeployExplosiveBarrel += _Router.UserDeployExplosiveBarrel;
                };

            AttachRouter(
                left_to_server,
                new SharedClass1.RemoteEvents.WithUserArgumentsRouter_Broadcast
                {
                    user = player_left.UserId,
                    Target = player_left.ToOthers
                }
            );

            var player_right = new Player
            {
                FromPlayer = right_to_server,
                ToPlayer = server_to_right,
                ToOthers = server_to_left,
                UserId = 1,
                Username = "Righty",
                AddScore = delegate { }
            };

            AttachRouter(
                  right_to_server,
                  new SharedClass1.RemoteEvents.WithUserArgumentsRouter_Broadcast
                  {
                      user = player_right.UserId,
                      Target = player_right.ToOthers
                  }
              );

            var left = CreateView();
            left.MovementArrows.Enabled = false;
            //left.ToggleMusic();

            var left_client = new FlashTowerDefenseClient.Implementation
            {
                Map = left,
                NetworkEvents = server_to_left,
                NetworkMessages = left_to_server,


            };

            left_client.InitializeEvents();
            left_client.InitializeMap();

            left.MoveTo(0, 0).AttachTo(this);


            var right = CreateView();
            right.MovementWASD.Enabled = false;
            var right_client = new FlashTowerDefenseClient.Implementation
            {
                Map = right,
                NetworkEvents = server_to_right,
                NetworkMessages = right_to_server,
            };


            right_client.InitializeEvents();
            right_client.InitializeMap();

            right.MoveTo(Padding + ReallyNarrowWidth, 0).AttachTo(this);

            //server_to_left.ServerPlayerHello += e => Console.WriteLine(e.ToString());
            //server_to_right.ServerPlayerHello += e => Console.WriteLine(e.ToString());

            //right_to_server.WalkTo += e => Console.WriteLine("server sees this: " + e.ToString());
            //server_to_left.WalkTo += e => Console.WriteLine("left sees this: " + e.ToString());

            server.GameStarted();

            100.AtDelayDo(
                delegate
                {
                    server.Users.Add(player_left);
                    server.UserJoined(player_left);
                }
            );

            300.AtDelayDo(
                delegate
                {
                    server.Users.Add(player_right);
                    server.UserJoined(player_right);
                }
            );
        }
    }
}