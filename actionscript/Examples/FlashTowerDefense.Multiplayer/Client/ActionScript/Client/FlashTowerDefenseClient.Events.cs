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


    partial class FlashTowerDefenseClient 
    {
 
        private SharedClass1.RemoteEvents InitializeEvents()
        {
            var NetworkEvents = new SharedClass1.RemoteEvents();

            #region Events
            // NetworkEvents // NetworkEvents // NetworkEvents // NetworkEvents // NetworkEvents 
            // NetworkEvents // NetworkEvents // NetworkEvents // NetworkEvents // NetworkEvents 
            // NetworkEvents // NetworkEvents // NetworkEvents // NetworkEvents // NetworkEvents 

            NetworkEvents.UserFiredWeapon +=
                e =>
                    WeaponInfo.PredefinedWeaponTypes.Single(i => i.NetworkId == e.weapon).SoundFire.ToSoundAsset().play();

            

            NetworkEvents.UserTakeBox +=
                e =>
                {
                    foreach (var v in Map.Boxes.Where(i => i.NetworkId == e.box))
                        v.RemoveFrom(Map.Boxes).Orphanize();

                    Sounds.sound20.ToSoundAsset().play();
                };

            NetworkEvents.UserWalkTo +=
                e =>
                {
                    foreach (var v in Players.Where(i => i.NetworkId == e.user))
                    {
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


                    foreach (var i in e.e)
                    {
                        MyExtensions.ByChance_RandomNumbers.Enqueue(i);
                    }

                    // active warzone
                    if (Map.InterlevelMusic == null)
                    {
                        Map.InterlevelTimeout = FlashTowerDefense.InterlevelTimeoutDefault;
                        Map.WaveEndsWhenAllBadGuysAreDead = true;
                        Map.GameEvent();
                    }
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
                    if (Map != null)
                        Map.ShowMessage("Server: " + e.text);
                };

            NetworkEvents.UserAddDamageFromDirection +=
                e =>
                {
                    foreach (var v in Map.AllMortals.Where(i => i.NetworkId == e.target))
                    {
                        
                        v.AddDamageFromDirection(e.damage, e.arc.DegreesToRadians());
                    }
                };

            NetworkEvents.UserShowBulletsFlying +=
                e =>
                {
                    var Weapon = WeaponInfo.PredefinedWeaponTypes.SingleOrDefault(i => i.NetworkId == e.weaponType);

                    Map.ShowBulletsFlying(
                        new Point
                        {
                            x = e.x,
                            y = e.y
                        },
                        e.arc.DegreesToRadians(),
                        Weapon
                    );

                };

            NetworkEvents.ServerPlayerHello +=
                e =>
                {
                    //Map.ShowMessage("Howdy, " + e.name + "! #" + e.user);


                    if (Map != null)
                        Map.Ego.NetworkId = e.user;

                    EgoId = e.user;
                };

            NetworkEvents.ServerPlayerJoined +=
                e =>
                {
                    // when a player joins end this day sooner
                    Map.WaveEndsWhenAllBadGuysAreDead = false;
                    Map.WaveEndCountdown = 0;
                    Map.InterlevelTimeout = 500;
                    Map.ReportDays();

                    var name = e.name;
                    var id = e.user;

                    var n = AddNewPlayer(name, id);

                    Map.ShowMessage("Player joined: " + n.NetworkName + " #" + n.NetworkId);

                    this.NetworkMessages.PlayerAdvertise(Map.Ego.NetworkId);
                    
                    BroadcastTeleportTo();
                };

            NetworkEvents.ServerPlayerAdvertise +=
                e =>
                {
                    
                    if (!Players.Any(n => n.NetworkId == e.user))
                    {
                        var name = e.name;
                        var id = e.user;

                        var n = AddNewPlayer(name, id);

                        Map.ShowMessage("Player is here: " + n.NetworkName + " #" + n.NetworkId);
                    }
                };

            NetworkEvents.ServerPlayerLeft +=
                e =>
                {
                    Map.ShowMessage("Player left: " + e.name);

                    foreach (var v in Players.Where(i => i.NetworkId == e.user))
                    {
                        v.RemoveFrom(Players).AddDamage(v.Health);
                    }
                };

            NetworkEvents.UserPlayerResurrect +=
                e =>
                {
                    var p = Players.Single(n => n.NetworkId == e.user);

                    p.Revive();

                    Map.ShowMessage(p.NetworkName + " is born again");

                };

            #endregion
            return NetworkEvents;
        }

        private PlayerWarrior AddNewPlayer(string name, int id)
        {
            var n = new PlayerWarrior
            {
                CanMakeFootsteps = false,
                RunAnimation = false,
                NetworkName = name,
                NetworkId = id,
                x = 100 + 100.Random(),
                y = 100 + 100.Random()
            }.AddTo(Players).AttachTo(Map.GetWarzone());

            n.Die +=
                delegate
                {
                    if (Players.Contains(n))
                    {
                        Map.ShowMessage("One of us has died!");
                    }
                };

            return n;
        }

        public int EgoId;
    }
}