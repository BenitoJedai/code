using System;
using System.Collections.Generic;
using System.Linq;
using FlashTowerDefense.ActionScript;
using FlashTowerDefense.ActionScript.Actors;
using FlashTowerDefense.ActionScript.Assets;
using FlashTowerDefense.Shared;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Nonoba.api;
using ScriptCoreLib.Shared.Lambda;

namespace FlashTowerDefense.ActionScript.Client
{


    partial class FlashTowerDefenseClient
    {

        [Script]
        public class Implementation
        {
            public Action<string> ShowMessage = delegate { };

            public List<PlayerWarrior> Players;

            Action BroadcastTeleportTo;

            public FlashTowerDefenseSized Map;

            public SharedClass1.IMessages NetworkMessages;
            public SharedClass1.IEvents NetworkEvents;

            public void InitializeMap()
            {
                
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

               
                Map.ShowMessage("Running in multiplayer mode!");


                // compiler bug: extension method as instance method, the first param is lost

                //Func<SharedClass1.Messages, ParamsAction<object>> f = (SharedClass1.Messages j) => j.FixParam((SharedClass1.Messages i, object[] args) => c.SendMessage(i, args));


                var ToOthers = /*(SharedClass1.IPairedMessagesWithoutUser)*/NetworkMessages;




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

                Map.NetworkAddKillScore += NetworkMessages.AddKillScore;

                500.AtIntervalDo(NetworkMessages.Ping);


                NetworkMessages.PlayerAdvertise(Map.Ego.NetworkId);

                BroadcastTeleportTo();

                300.AtDelayDo(NetworkMessages.ReadyForServerRandomNumbers);
            }

            public void InitializeEvents()
            {


                var FromUser = /*(SharedClass1.IPairedEventsWithUser)*/ NetworkEvents;

                #region Events
                // NetworkEvents // NetworkEvents // NetworkEvents // NetworkEvents // NetworkEvents 
                // NetworkEvents // NetworkEvents // NetworkEvents // NetworkEvents // NetworkEvents 
                // NetworkEvents // NetworkEvents // NetworkEvents // NetworkEvents // NetworkEvents 

                FromUser.UserFiredWeapon +=
                    e =>
                        WeaponInfo.PredefinedWeaponTypes.Single(i => i.NetworkId == e.weapon).SoundFire.ToSoundAsset().play();

                FromUser.UserDeployExplosiveBarrel +=
                    e =>
                    {
                        Map.CreateExplosiveBarrel(
                            Weapon.PredefinedWeapons.Single(i => i.NetworkId == e.weapon),
                            new Point { x = e.x, y = e.y },
                            e.barrel,
                            FlashTowerDefense.NetworkMode.Remote
                            );
                    };

                FromUser.UserUndeployExplosiveBarrel +=
                    e =>
                    {
                        foreach (var v in Map.Barrels.Where(i => i.NetworkId == e.barrel))
                        {
                            v.RemoveFrom(Map.Barrels).Orphanize();
                        }


                    };

                FromUser.UserTakeBox +=
                    e =>
                    {
                        foreach (var v in Map.Boxes.Where(i => i.NetworkId == e.box))
                            v.RemoveFrom(Map.Boxes).Orphanize();

                        Sounds.sound20.ToSoundAsset().play();
                    };

                FromUser.UserWalkTo +=
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

                FromUser.UserTeleportTo +=
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
                            //Map.InterlevelTimeout = FlashTowerDefense.InterlevelTimeoutDefault;
                            //Map.WaveEndsWhenAllBadGuysAreDead = true;
                            //Map.WaveEndCountdown = 15;
                            Map.GameEvent();
                        }
                    };

                FromUser.UserEnterMachineGun +=
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

                FromUser.UserExitMachineGun +=
                    e =>
                    {
                        if (Map.EgoIsOnTheField())
                        {
                            Map.PrebuiltTurretBlinkTimer.start();
                            Map.PrebuiltTurretInUse = false;
                        }
                    };

                FromUser.UserStartMachineGun +=
                    e =>
                    {
                        if (Map.EgoIsOnTheField())
                        {
                            Map.PrebuiltTurret.AnimationEnabled = true;

                            // late info? :)
                            Map.PrebuiltTurretInUse = true;
                        }
                    };

                FromUser.UserStopMachineGun +=
                    e =>
                    {
                        if (Map.EgoIsOnTheField())
                            Map.PrebuiltTurret.AnimationEnabled = false;
                    };


                FromUser.UserAddDamageFromDirection +=
                    e =>
                    {
                        foreach (var v in Map.AllMortals.Where(i => i.NetworkId == e.target))
                        {

                            v.AddDamageFromDirection(e.damage, e.arc.DegreesToRadians());
                        }
                    };

                FromUser.UserAddDamage +=
                    e =>
                    {
                        foreach (var v in Map.AllMortals.Where(i => i.NetworkId == e.target))
                        {
                            v.AddDamage(e.damage);
                        }
                    };

                FromUser.UserShowBulletsFlying +=
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

                FromUser.UserPlayerResurrect +=
                               e =>
                               {
                                   var p = Players.Single(n => n.NetworkId == e.user);

                                   p.Revive();

                                   Map.ShowMessage(p.NetworkName + " is born again");

                               };

                NetworkEvents.ServerMessage +=
                     e =>
                     {
                         if (Map != null)
                             Map.ShowMessage("Server: " + e.text);
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
                        //Map.WaveEndsWhenAllBadGuysAreDead = false;
                        //Map.WaveEndCountdown = 0;
                        //Map.InterlevelTimeout = 500;
                        //Map.ReportDays();

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



                #endregion

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
                    y = 100 + 100.Random(),
                    filters = new[] { new GlowFilter(0x8080ff) }
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
}