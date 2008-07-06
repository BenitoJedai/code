using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.media;
using System;
using System.Linq;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.mx.core;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.Shared.Lambda;
using System.Collections.Generic;
using ScriptCoreLib.ActionScript.flash.ui;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.geom;
using FlashTowerDefense.ActionScript.Actors;
using FlashTowerDefense.ActionScript.Assets;


namespace FlashTowerDefense.ActionScript
{

    /// <summary>
    /// testing...
    /// </summary>

    partial class FlashTowerDefense
    {


        private void AddNewActorsToMap(Action UpdateScoreBoard, Func<double> GetEntryPointY, Func<Actor, Actor> AttachRules)
        {
            Action<Actor> ReduceSpeedToHalf = i => i.speed /= 2;

            if (0.3.ByChance())
            {
                var Minnions = new List<Actor>();

                if (0.5.ByChance())
                {

                    #region create boss
                    var boss = AttachRules(
                           new BossWarrior
                           {
                               x = -OffscreenMargin,
                               y = GetEntryPointY(),
                               speed = 1 + 2.0.FixedRandom(),
                           }
                       );



                    // make the minions slower when boss dies
                    boss.Die += () => Minnions.ForEach(ReduceSpeedToHalf);

                    #region create minnions
                    Func<double, Actor> CreateMinionWarriorByArc =
                                     arc =>
                                       new Warrior
                                       {
                                           x = boss.x + Math.Cos(arc) * 96,
                                           y = boss.y + Math.Sin(arc) * 96 / 2,
                                           speed = boss.speed
                                       };

                    Func<double, Actor> CreateMinionByArc =
                        arc =>
                          new Sheep
                          {
                              x = boss.x + Math.Cos(arc) * 64,
                              y = boss.y + Math.Sin(arc) * 64 / 2,
                              speed = boss.speed
                          };


                    if (0.3.ByChance())
                    {
                        // boss with 2 minions
                        AttachRules(CreateMinionByArc((Math.PI * 2) * 0.15)).AddTo(Minnions);
                        AttachRules(CreateMinionByArc((Math.PI * 2) * 0.85)).AddTo(Minnions);
                    }
                    else if (0.3.ByChance())
                    {
                        // boss with 3 minions
                        AttachRules(CreateMinionByArc((Math.PI * 2) * 0.20)).AddTo(Minnions);
                        AttachRules(CreateMinionByArc((Math.PI * 2) * 0)).AddTo(Minnions);
                        AttachRules(CreateMinionByArc((Math.PI * 2) * 0.80)).AddTo(Minnions);
                    }
                    else if (0.3.ByChance())
                    {
                        AttachRules(CreateMinionByArc((Math.PI * 2) * 0.15)).AddTo(Minnions);
                        AttachRules(CreateMinionByArc((Math.PI * 2) * 0.25)).AddTo(Minnions);
                        AttachRules(CreateMinionByArc((Math.PI * 2) * 0.85)).AddTo(Minnions);
                        AttachRules(CreateMinionByArc((Math.PI * 2) * 0.75)).AddTo(Minnions);
                    }
                    else
                    {
                        AttachRules(CreateMinionWarriorByArc((Math.PI * 2) * 0.3)).AddTo(Minnions);
                        AttachRules(CreateMinionByArc((Math.PI * 2) * 0.15)).AddTo(Minnions);
                        AttachRules(CreateMinionWarriorByArc((Math.PI * 2) * 0.0)).AddTo(Minnions);
                        AttachRules(CreateMinionByArc((Math.PI * 2) * 0.85)).AddTo(Minnions);
                        AttachRules(CreateMinionWarriorByArc((Math.PI * 2) * 0.7)).AddTo(Minnions);
                    }

                    #endregion

                    // respawn the boss
                    boss.CorpseGone +=
                        delegate
                        {


                            var newboss = AttachRules(
                                new BossWarrior
                                {
                                    x = boss.x,
                                    y = boss.y,
                                    speed = boss.speed / 2,
                                    filters = boss.filters,
                                    IsBleeding = true,
                                    NetworkId = boss.NetworkId + 4
                                }
                            );

                            // remove the glow from the old boss cuz we respawned
                            boss.filters = null;



                            // if the respawned boss dies remove the glow
                            newboss.Die +=
                                delegate
                                {
                                    newboss.filters = null;
                                };
                        };
                    #endregion

                }
                else
                {
                    var boss = AttachRules(
                         new BossSheep
                         {
                             x = -OffscreenMargin,
                             y = GetEntryPointY(),
                             speed = 0.5 + 2.0.FixedRandom()
                         }
                    );

                    Func<double, Actor> CreateMinionByIndex =
                        i =>
                            new Sheep
                            {
                                x = boss.x + Math.Abs(i) * -24,
                                y = boss.y + i * 32,
                                speed = boss.speed
                            };

                    Enumerable.Range(1, (2.0.FixedRandom() + 1).ToInt32()).ForEach(
                        i =>
                        {
                            AttachRules(CreateMinionByIndex(-i)).AddTo(Minnions);
                            AttachRules(CreateMinionByIndex(i)).AddTo(Minnions);
                        }
                    );


                    // make the minions slower when boss dies
                    boss.Die += () => Minnions.ForEach(ReduceSpeedToHalf);


                    // respawn the boss
                    boss.CorpseGone +=
                        delegate
                        {


                            var newboss = AttachRules(
                                new BossSheep
                                {
                                    x = boss.x,
                                    y = boss.y,
                                    speed = boss.speed / 2,
                                    filters = boss.filters,
                                    IsBleeding = true,
                                    NetworkId = boss.NetworkId + 4
                                }
                            );

                            // remove the glow from the old boss cuz we respawned
                            boss.filters = null;

                            // ?? fixme: sync/race condition in multiplayer at the moment

                            //Action<Sheep> AddMinnion = i => AttachRules(i).AddTo(Minnions);

                            //AddMinnion.ToForEach()(
                            //    from i in Minnions
                            //    where i.IsCorpseGone
                            //    where !i.IsCorpseAndBloodGone
                            //    select new Sheep
                            //    {
                            //        x = i.x,
                            //        y = i.y,
                            //        speed = newboss.speed,
                            //        IsBleeding = true,
                            //        NetworkId = i.NetworkId + 4
                            //    }
                            //);



                            // if the respawned boss dies remove the glow
                            newboss.Die +=
                                delegate
                                {
                                    newboss.filters = null;
                                };
                        };
                }
            }
            else
            {
                if (0.1.ByChance())
                {
                    var n = AttachRules(
                      new NuclearWarrior
                      {
                          x = -OffscreenMargin,
                          y = GetEntryPointY(),
                          speed = 1 + 2.0.FixedRandom()
                      }
                    );


                    n.Die +=
                        () => CreateExplosion(WeaponInfo.ExplosivesBarrel, n.ToPoint(), NetworkMode.Remote);
                }


                if (0.1.ByChance())
                {
                    var n = AttachRules(
                      new NuclearSheep
                      {
                          x = -OffscreenMargin,
                          y = GetEntryPointY(),
                          speed = 1 + 2.0.FixedRandom()
                      }
                    );


                    n.Die +=
                        () => CreateExplosion(WeaponInfo.ExplosivesBarrel, n.ToPoint(), NetworkMode.Remote);
                }

                if (0.1.ByChance())
                {
                    var n = AttachRules(
                      new NuclearPig
                      {
                          x = -OffscreenMargin,
                          y = GetEntryPointY(),
                          speed = 1 + 2.0.FixedRandom()
                      }
                    );


                    n.Die +=
                        () => CreateExplosion(WeaponInfo.ExplosivesBarrel, n.ToPoint(), NetworkMode.Remote);
                }

                if (0.3.ByChance())
                {


                    AttachRules(
                        new Warrior
                        {
                            x = -OffscreenMargin,
                            y = GetEntryPointY(),
                            speed = 1 + 2.0.FixedRandom()
                        }
                    );


                }
                else if (0.5.ByChance())
                {
                    AttachRules(
                        new Sheep
                        {
                            x = -OffscreenMargin,
                            y = GetEntryPointY(),
                            speed = 0.5 + 2.0.FixedRandom()
                        }
                    );

                }
                else
                {
                    AttachRules(
                         new Pig
                         {
                             x = -OffscreenMargin,
                             y = GetEntryPointY(),
                             speed = 0.5 + 2.0.FixedRandom()
                         }
                     );
                }
            }

            UpdateScoreBoard();
        }

    }












}
