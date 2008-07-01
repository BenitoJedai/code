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
    [Script, ScriptApplicationEntryPoint(Width = DefaultWidth, Height = DefaultHeight)]
    [SWF(width = DefaultWidth, height = DefaultHeight, backgroundColor = ColorWhite)]
    public partial class FlashTowerDefense : Sprite
    {
        public const int DefaultWidth = 560;
        public const int DefaultHeight = 480;

        public const uint ColorGreen = 0x00ff00;
        public const uint ColorRed = 0xff0000;
        public const uint ColorBlack = 0x000000;
        public const uint ColorWhite = 0xffffff;
        public const uint ColorBlue = 0x0000ff;
        public const uint ColorBlueDark = 0x000080;
        public const uint ColorBlueLight = 0x9090ff;

        const int OffscreenMargin = 32;

        public Animation PrebuiltTurret;

        public Timer PrebuiltTurretBlinkTimer;

        public readonly Func<DisplayObjectContainer> GetWarzone;
        public readonly Action<TextField, bool> BlurWarzoneOnHover;

        public bool CanFire = true;

        public readonly Shape Aim;

        public SoundChannel IngameMusic;
        public double IngameMusicVolume = 0.3;

        Func<double> GetRandomHitDamage = () => (8 + 12.Random()) * 1.5;

        public FlashTowerDefense()
        {

            var bg = new Sprite { x = 0, y = 0 };

            //bg.graphics.beginFill(0xffffff);
            //bg.graphics.beginFill(0x808080);
            //bg.graphics.drawRect(0, 0, Width / 2, Height);



            var warzone = new Sprite { x = 0, y = 0 };

            warzone.graphics.beginFill(ColorWhite);
            warzone.graphics.drawRect(-OffscreenMargin, -OffscreenMargin, DefaultWidth + 2 * OffscreenMargin, DefaultHeight + 2 * OffscreenMargin);

            warzone.mouseChildren = false;


            GetWarzone = () => warzone;

            bg.AttachTo(GetWarzone());
            warzone.AttachTo(this);

            #region create aim
            this.Aim = new Shape();

            Aim.graphics.lineStyle(4, 0, 1);

            Aim.graphics.moveTo(-8, 0);
            Aim.graphics.lineTo(8, 0);

            Aim.graphics.moveTo(0, -8);
            Aim.graphics.lineTo(0, 8);

            Aim.filters = new[] { new DropShadowFilter() };
            Aim.AttachTo(GetWarzone());
            #endregion


            #region BlurWarzoneOnHover
            this.BlurWarzoneOnHover =
                (txt, HideAim) =>
                {


                    txt.mouseOver +=

                        delegate
                        {
                            //warzone.alpha = 0.8;



                            //powered_by_jsc.htmlText = "<u><a href='http://jsc.sf.net'>powered by <b>jsc</b></a></u>";

                            txt.textColor = ColorBlue;
                            txt.filters = null;

                            if (CanFire)
                            {
                                warzone.filters = new[] { new BlurFilter() };

                                if (HideAim)
                                    Aim.visible = false;
                            }
                        };

                    txt.mouseOut +=
                        delegate
                        {
                            txt.filters = new[] { new BlurFilter() };
                            txt.textColor = ColorBlack;

                            if (CanFire)
                            {
                                //powered_by_jsc.htmlText = "<a href='http://jsc.sf.net'>powered by <b>jsc</b></a>";

                                //warzone.alpha = 1;

                                warzone.filters = null;


                                if (HideAim)
                                    Aim.visible = true;
                            }
                        };
                };
            #endregion


            #region ScoreBoard
            var ScoreBoard = new TextField
            {
                x = 24,
                y = 24,

                defaultTextFormat = new TextFormat
                {
                    size = 12
                },
                autoSize = TextFieldAutoSize.LEFT,
                text = "Defend yourself by shooting those mad sheep.",
                filters = new[] { new BlurFilter() },
                selectable = false,
            };

            //ScoreBoard.AttachTo(this);


            BlurWarzoneOnHover(ScoreBoard, true);
            #endregion



            Action<double, Action> Times =
                (m, h) => (DefaultWidth * DefaultHeight * m).Times(h);

            Action<double, Func<BitmapAsset>> AddDoodads =
                (m, GetImage) => Times(m, () => GetImage().AttachTo(bg).SetCenteredPosition(DefaultWidth.Random(), DefaultHeight.Random()));

            AddDoodads(0.0001, () => Images.grass1.ToBitmapAsset());
            AddDoodads(0.00005, () => Images.bump2.ToBitmapAsset());

            Action StartIngameMusic =
                delegate
                {
                    if (this.IngameMusic != null)
                        this.IngameMusic.stop();

                    this.IngameMusic = Sounds.snd_world.ToSoundAsset().play(0, 999, new SoundTransform(IngameMusicVolume));
                };

            StartIngameMusic();

            Func<Animation> AddCactus = () =>
                new Animation(null, Images.img_cactus)
                {
                    FrameRate = 1000 / 7,
                    AnimationEnabled = true
                };




            Action<double> AddCactusAt = y =>
                {
                    var x = DefaultWidth.Random();

                    AddCactus().AttachTo(GetWarzone()).MoveTo(
                        x, y + Math.Cos(x + y) * DefaultHeight * 0.03);
                };

            (3 + 3.Random()).Times(AddCactusAt.FixParam(DefaultHeight * 0.06));
            (3 + 3.Random()).Times(AddCactusAt.FixParam(DefaultHeight * 0.94));

            PrebuiltTurret = new Animation(Images.img_turret1_gunfire_180, Images.img_turret1_gunfire_180_frames);

            PrebuiltTurret.x = (DefaultWidth - PrebuiltTurret.width) * 0.9;
            PrebuiltTurret.y = (DefaultHeight - PrebuiltTurret.height) / 2;

            PrebuiltTurret.AttachTo(GetWarzone());

            #region Messages
            var ActiveMessages = new List<TextField>();
            var ShowMessageNow = default(Action<string, Action>);

            ShowMessageNow =
                (MessageText, Done) =>
                {

                    var p = new TextField
                    {
                        textColor = ColorWhite,
                        background = true,
                        backgroundColor = ColorBlack,
                        filters = new[] { new GlowFilter(ColorBlack) },
                        autoSize = TextFieldAutoSize.LEFT,
                        text = MessageText,
                        mouseEnabled = false
                    };

                    var y = DefaultHeight - p.height - 32;

                    p.AddTo(ActiveMessages).AttachTo(this).MoveTo((DefaultWidth - p.width) / 2, DefaultHeight);

                    Sounds.snd_message.ToSoundAsset().play();

                    var MessagesToBeMoved = (from TheMessage in ActiveMessages select new { TheMessage, y = TheMessage.y - TheMessage.height }).ToArray();



                    (1000 / 24).AtInterval(
                        t =>
                        {
                            foreach (var i in MessagesToBeMoved)
                            {
                                if (i.TheMessage.y > i.y)
                                    i.TheMessage.y -= 4;

                            }

                            p.y -= 4;

                            if (p.y < y)
                            {
                                t.stop();

                                if (Done != null)
                                    Done();

                                9000.AtDelayDo(
                                    () => p.RemoveFrom(ActiveMessages).FadeOutAndOrphanize(1000 / 24, 0.21)
                                );
                            }
                        }
                    );
                };


            var QueuedMessages = new Queue<string>();

            this.ShowMessage =
                Text =>
                {
                    if (QueuedMessages.Count > 0)
                    {
                        QueuedMessages.Enqueue(Text);
                        return;
                    }

                    // not busy
                    QueuedMessages.Enqueue(Text);

                    var NextQueuedMessages = default(Action);

                    NextQueuedMessages =
                        () => ShowMessageNow(QueuedMessages.Peek(),
                            delegate
                            {
                                QueuedMessages.Dequeue();

                                if (QueuedMessages.Count > 0)
                                    NextQueuedMessages();
                            }
                        );

                    NextQueuedMessages();
                };
            #endregion

            var StatusBar = new Sprite
            {
                mouseEnabled = false,
                mouseChildren = false,
            }.MoveTo(DefaultWidth - 96, DefaultHeight - 64).AttachTo(this);



            #region  WeaponBar
            var WeaponBar = new Sprite
            {
            }.AttachTo(StatusBar);


            var AmmoAvatar = new Sprite().MoveTo(38, 24).AttachTo(WeaponBar);

            Images.Avatars.avatars_ammo.ToBitmapAsset().MoveToCenter().AttachTo(AmmoAvatar);

            var AmmoText = new TextField
            {
                defaultTextFormat = new TextFormat
                {
                    bold = true,
                    size = 20,
                    font = "_sans"
                },
                text = "200000",
                filters = new[] { new GlowFilter(ColorBlack, 0.5) },
                autoSize = TextFieldAutoSize.RIGHT,
                x = 0,
                width = 0
            }.AttachTo(AmmoAvatar);




            var WeaponAvatar = new Sprite().AttachTo(WeaponBar);
            #endregion

            var CurrentTarget = default(MouseEvent);
            var CurrentTargetTimer = default(Timer);

            #region Ego
            Ego = new PlayerWarrior
                {
                    filters = new[] { new GlowFilter(ColorGreen) }
                };

            EgoIsOnTheField = () => Ego.parent != null;
            EgoIsAlive = () => Ego.IsAlive;

            Func<bool> EgoCanManTurret = () => !PrebuiltTurretInUse; // look up if there is somebody else in it
            Func<bool> EgoIsCloseToTurret = () => new Point { x = Ego.x - PrebuiltTurret.x, y = Ego.y - PrebuiltTurret.y }.length < 32;

            var EgoAimDistance = 48;
            var EgoAimMoveSpeed = 0.1;
            var EgoMoveSpeed = 3.0;
            var EgoMoveToMouseTarget = false;

            UpdateEgoAim =
                delegate
                {
                    Aim.MoveTo(Ego.x + Math.Cos(EgoAimDirection) * EgoAimDistance, Ego.y + Math.Sin(EgoAimDirection) * EgoAimDistance);
                };


            EgoMovedSlowTimer = new Timer(200, 1);

            var EgoMoveUpTimer = (1000 / 30).AtInterval(
                delegate
                {
                    if (EgoMoveToMouseTarget)
                    {
                        var p = new Point { x = CurrentTarget.stageX - Ego.x, y = CurrentTarget.stageY - Ego.y };

                        if (p.length <= EgoMoveSpeed)
                        {
                            // run one the pointer
                            return;
                        }

                        Ego.MoveToArc(p.GetRotation(), EgoMoveSpeed);
                    }
                    else
                        Ego.MoveToArc(EgoAimDirection, EgoMoveSpeed);

                    Ego.x = Ego.x.Max(0).Min(DefaultWidth);
                    Ego.y = Ego.y.Max(0).Min(DefaultHeight);

                    UpdateEgoAim();

                    EgoMovedSlowTimer.start();

                    // we moved now let's check for boxes
                    foreach (var BoxToTake in
                                     from ss in Boxes
                                     where new Point { x = Ego.x - ss.x, y = Ego.y - ss.y }.length < 32
                                     select ss)
                    {
                        BoxToTake.RemoveFrom(Boxes).Orphanize();

                        Sounds.sound20.ToSoundAsset().play();

                        if (BoxToTake.WeaponInside == null)
                        {
                            // add stuff, so the player doesn't get bored:D
                            // maybe implment them too? see Diablo.
                            var PowerUps = new[] { "Defense", "Mana", "Dexerity", "Experience", "Stamina", "Strength", "Life", "Replenish Life" };


                            ShowMessage("+1 " + PowerUps.Random());
                        }
                        else
                        {
                            var WeaponToAddAmmoTo = Ego.Weapons.FirstOrDefault(i => i.NetworkId == BoxToTake.WeaponInside.NetworkId);

                            Action<Weapon> ChangeWeaponIfNeeded =
                                w =>
                                {
                                    if (Ego.CurrentWeapon.SelectMode != w.SelectMode)
                                        return;

                                    if (Ego.CurrentWeapon.Ammo > 0)
                                        return;

                                    Ego.CurrentWeapon = w;
                                };

                            if (WeaponToAddAmmoTo == null)
                            {
                                Ego.Weapons.Add(BoxToTake.WeaponInside);

                                ChangeWeaponIfNeeded(BoxToTake.WeaponInside);

                                ShowMessage("You found " + BoxToTake.WeaponInside.Name);
                            }
                            else
                            {
                                ShowMessage("Got ammo for " + BoxToTake.WeaponInside.Name);


                                WeaponToAddAmmoTo.Ammo += BoxToTake.WeaponInside.Ammo;

                                ChangeWeaponIfNeeded(WeaponToAddAmmoTo);

                            }


                        }

                        if (NetworkTakeCrate != null)
                            NetworkTakeCrate(BoxToTake.NetworkId);
                    }
                });

            EgoMoveUpTimer.stop();

            var EgoAimMoveTimer = (1000 / 30).AtInterval(
                 delegate
                 {
                     EgoAimDirection += EgoAimMoveSpeed * EgoMoveSpeed;

                     if (EgoAimDirection < 0)
                         EgoAimDirection += Math.PI * 4;

                     EgoAimDirection %= Math.PI * 2;

                     UpdateEgoAim();
                 });
            EgoAimMoveTimer.stop();


            #endregion


            Ego.CurrentWeaponChanged +=
                delegate
                {
                    Sounds.SelectWeapon.ToSoundAsset().play();

                    if (WeaponAvatar.numChildren > 0)
                        WeaponAvatar.getChildAt(0).Orphanize();

                    Ego.CurrentWeapon.Type.Avatar.MoveToCenter().AttachTo(WeaponAvatar);
                    WeaponBar.filters = new[] { new GlowFilter(Ego.CurrentWeapon.Color) };
                };

            Ego.CurrentWeaponAmmoChanged += () => AmmoText.text = "" + Ego.CurrentWeapon.Ammo;
            Ego.CurrentWeapon = Ego.Weapons.FirstOrDefault(i => i.SelectMode == Weapon.SelectModeEnum.Turret);

            Ego.Die +=
                () =>
                {
                    ShowMessage("Meeeediiic!");
                    ShowMessage("You died a painful death");

                    PlayerWarrior.AutoResurrectDelay.AtDelayDo(
                        delegate
                        {
                            Ego.Revive();

                            if (EgoResurrect != null)
                                EgoResurrect();
                        }
                    );
                };

            #region HealthBar
            var HealthBar = new Sprite
            {
                filters = new[] { new GlowFilter(ColorRed) }
            }.MoveTo(-20, 0).AttachTo(StatusBar);

            var Hearts = new[] 
            {
                Images.Avatars.avatars_heart.ToBitmapAsset().MoveToArc(20.DegreesToRadians(), 90).AttachTo(HealthBar),
                Images.Avatars.avatars_heart.ToBitmapAsset().MoveToArc(7.DegreesToRadians(), 90).AttachTo(HealthBar),
                Images.Avatars.avatars_heart.ToBitmapAsset().MoveToArc(353.DegreesToRadians(), 90).AttachTo(HealthBar),
                Images.Avatars.avatars_heart.ToBitmapAsset().MoveToArc(340.DegreesToRadians(), 90).AttachTo(HealthBar),
            };

            var EgoHeartBeat = default(SoundChannel);

            Ego.HealthChanged +=
                delegate
                {
                    if (EgoHeartBeat == null)
                    {
                        EgoHeartBeat = Sounds.heartbeat3.ToSoundAsset().play();
                        EgoHeartBeat.soundComplete +=
                            e =>
                            {
                                EgoHeartBeat = null;
                            };
                    }

                    if (Ego.Health > 0)
                    {

                        var z = (Ego.Health / Ego.MaxHealth) * Hearts.Length;
                        var f = Math.Floor(z).ToInt32();
                        var a = z % 1;

                        for (int i = 0; i < f; i++)
                        {
                            Hearts[i].alpha = 1;
                        }

                        if (f < Hearts.Length)
                        {
                            Hearts[f].alpha = a;

                            for (int i = f + 1; i < Hearts.Length; i++)
                            {
                                Hearts[i].alpha = 0;
                            }

                        }
                    }
                    else
                    {
                        for (int i = 0; i < Hearts.Length; i++)
                        {
                            Hearts[i].alpha = 0;
                        }
                    }

                    //ShowMessage("h: " + f + " " + a);
                };

            #endregion

            var PrebuiltTurretSound = default(SoundChannel);

            var runaways = 0;
            var score = 0;

            Action UpdateScoreBoard =
                delegate
                {
                    if (!CanFire) return;

                    ScoreBoard.text =
                        new
                        {
                            runaways,
                            gore = (100 * (double)BadGuys.Count(i => !i.IsAlive) / (double)BadGuys.Count()).Round() + "%",
                            score,
                            level = CurrentLevel
                        }.ToString();
                };

            Action<MouseEvent> DoMachineGunFire =
                e =>
                {
                    if (Ego.CurrentWeapon.Ammo <= 0)
                        return;


                    Ego.CurrentWeapon.Ammo--;



                    var DamagePointOfOrigin = new Point { x = PrebuiltTurret.x, y = PrebuiltTurret.y };
                    var DamageDirection = new Point { x = e.stageX - PrebuiltTurret.x, y = e.stageY - PrebuiltTurret.y }.GetRotation();

                    DoSomeDamage(DamagePointOfOrigin, DamageDirection, WeaponInfo.Machinegun);

                    if (Ego.CurrentWeapon.Ammo <= 0)
                    {
                        Sounds.OutOfAmmo.ToSoundAsset().play();
                        PrebuiltTurret.AnimationEnabled = false;
                        return;
                    }
                };



            PrebuiltTurret.AnimationEnabledChanged +=
                delegate
                {

                    if (PrebuiltTurret.AnimationEnabled)
                    {
                        if (PrebuiltTurretSound == null)
                            PrebuiltTurretSound = Sounds.gunfire.ToSoundAsset().play(0, 999);

                        PrebuiltTurret.filters = new[] { new GlowFilter() };
                    }
                    else
                    {
                        if (PrebuiltTurretSound != null)
                            PrebuiltTurretSound.stop();

                        PrebuiltTurretSound = null;
                        PrebuiltTurret.filters = null;
                    }

                };

            GetWarzone().mouseDown +=
                e =>
                {
                    if (!CanFire) return;

                    // the turret cannot fire without a man behind it
                    if (EgoIsOnTheField())
                        return;

                    if (Ego.CurrentWeapon.Ammo <= 0)
                    {
                        Sounds.OutOfAmmo.ToSoundAsset().play();
                        return;
                    }


                    PrebuiltTurret.AnimationEnabled = true;

                    CurrentTarget = e;
                    CurrentTargetTimer =
                        (1000 / 10).AtInterval(
                            delegate
                            {
                                if (!PrebuiltTurret.AnimationEnabled)
                                {
                                    CurrentTargetTimer.stop();

                                    return;
                                }

                                DoMachineGunFire(CurrentTarget);
                            }
                        );

                };

            GetWarzone().mouseUp +=
                 e =>
                 {
                     PrebuiltTurret.AnimationEnabled = false;
                 };


            GetWarzone().mouseMove +=
                e =>
                {
                    if (!CanFire)
                        return;

                    CurrentTarget = e;

                    if (EgoIsOnTheField())
                    {
                        Mouse.show();
                        return;
                    }


                    Mouse.hide();

                    Aim.x = CurrentTarget.stageX;
                    Aim.y = CurrentTarget.stageY;
                };





            Func<double> GetEntryPointY = () => (DefaultHeight * 0.8).FixedRandom() + DefaultHeight * 0.1;



            #region AttachRules
            Func<Actor, Actor> AttachRules =
                a =>
                {
                    if (a == null)
                        throw new Exception("AttachRules");

                    if (a.NetworkId == 0)
                    {
                        a.NetworkId = int.MaxValue.FixedRandom();

                        if (0.5.ByChance())
                        {
                            a.Crate = new Crate
                                {
                                    NetworkId = int.MaxValue.FixedRandom()
                                }.AddTo(Boxes);

                            if (0.7.ByChance())
                            {
                                // add random weapon, note that this is
                                // not a fixed random, so each player
                                // gets different weapon
                                a.Crate.WeaponInside = Weapon.PredefinedWeapons.Random().Clone();
                            }
                        }
                    }

                    a.CorpseAndBloodGone += () => BadGuys.Remove(a);
                    a.Moved +=
                        delegate
                        {
                            if (a.x > (DefaultWidth + OffscreenMargin))
                            {
                                a.Crate = null;

                                a.RemoveFrom(BadGuys).Orphanize();

                                if (CanFire)
                                {
                                    WaveEndCountdown--;
                                    runaways++;

                                    ScoreBoard.textColor = ColorRed;
                                    UpdateScoreBoard();
                                }

                                a.IsAlive = false;
                                // this one was able to run away
                            }
                        };





                    a.Die +=
                        delegate
                        {
                            WaveEndCountdown--;

                            score += a.ScoreValue;
                            UpdateScoreBoard();


                            if (a.Crate != null)
                            {
                                a.Crate.MoveTo(a.x, a.y).AttachTo(GetWarzone());
                            }
                        };

                    a.AttachTo(GetWarzone()).AddTo(BadGuys);

                    if (a.PlayHelloSound != null)
                        a.PlayHelloSound();

                    return a;
                };
            #endregion

            Action EgoTakeNextWeapon = () => Ego.CurrentWeapon = Ego.OtherWeaponsLikeCurrent.Next(i => i == Ego.CurrentWeapon);
            Action EgoTakePreviousWeapon = () => Ego.CurrentWeapon = Ego.OtherWeaponsLikeCurrent.Previous(i => i == Ego.CurrentWeapon);






            GetWarzone().mouseWheel +=
                e =>
                {
                    if (e.delta > 0)
                        EgoTakeNextWeapon();
                    else
                        EgoTakePreviousWeapon();
                };

            var EgoMoveToMouseTargetAntiDoubleClick = default(Timer);

            GetWarzone().mouseDown +=
                e =>
                {
                    if (!EgoIsOnTheField())
                        return;

                    if (!EgoIsAlive())
                        return;

                    var p = new Point { x = e.stageX - Ego.x, y = e.stageY - Ego.y };

                    EgoAimDirection = p.GetRotation();

                    CurrentTarget = e;

                    UpdateEgoAim();

                    EgoMoveToMouseTarget = true;

                    EgoMoveToMouseTargetAntiDoubleClick =
                        200.AtDelayDo(
                        delegate
                        {
                            if (!EgoMoveToMouseTarget)
                                return;

                            Ego.RunAnimation = true;
                            EgoMoveSpeed = 2.5;
                            EgoMoveUpTimer.start();
                        }
                    );
                };


            GetWarzone().mouseMove +=
                e =>
                {
                    if (!EgoMoveToMouseTarget)
                        return;

                    if (!EgoIsOnTheField())
                        return;

                    if (!EgoIsAlive())
                        return;

                    var p = new Point { x = e.stageX - Ego.x, y = e.stageY - Ego.y };

                    EgoAimDirection = p.GetRotation();

                    CurrentTarget = e;

                    UpdateEgoAim();
                };

            GetWarzone().mouseUp +=
                e =>
                {
                    if (!EgoMoveToMouseTarget)
                        return;

                    if (!EgoIsAlive())
                        return;

                    if (EgoMoveToMouseTargetAntiDoubleClick != null)
                    {
                        EgoMoveToMouseTargetAntiDoubleClick.stop();
                        EgoMoveToMouseTargetAntiDoubleClick = null;
                    }

                    EgoMoveUpTimer.stop();
                    Ego.RunAnimation = false;
                    EgoMoveToMouseTarget = false;
                };

            GetWarzone().doubleClickEnabled = true;
            GetWarzone().doubleClick +=
              e =>
              {
                  if (!EgoIsOnTheField())
                      return;

                  if (!EgoIsAlive())
                      return;

                  EgoAimDirection = new Point { x = e.stageX - Ego.x, y = e.stageY - Ego.y }.GetRotation();

                  UpdateEgoAim();

                  EgoDoFireWeapon();
              };

            Action StageIsReady =
                delegate
                {

                    //stage.scaleMode = StageScaleMode.NO_BORDER;

                    // multiplay ?

                    
                    var WeaponShortcutButtons = new[]
                    {
                        Keyboard.NUMBER_1,
                        Keyboard.NUMBER_2,
                        Keyboard.NUMBER_3,
                        Keyboard.NUMBER_4,
                        Keyboard.NUMBER_5,
                        Keyboard.NUMBER_6,
                        Keyboard.NUMBER_7,
                        Keyboard.NUMBER_8,
                        Keyboard.NUMBER_9,
                        Keyboard.NUMBER_0,
                    }.Select(
                        (Button, Index) =>
                            new KeyboardButton(stage)
                            {
                                Buttons = new[] { Button },
                                Filter = EgoIsAlive,
                                Up = () => Ego.CurrentWeapon = Ego.OtherWeaponsLikeCurrent.AtOrDefault(Index, Ego.CurrentWeapon)
                            }
                    ).ToArray();

                    var KeySpace = new KeyboardButton(stage)
                     {
                         Buttons = new[] { Keyboard.SPACE },
                         Filter = EgoIsOnTheField.And(EgoIsAlive),
                         Up =
                             delegate
                             {
                                 // car brakes, open door, take item

                                 foreach (var BarrelToTake in this.Barrels.Where(i => (i.ToPoint() - Ego.ToPoint()).length < 32))
                                 {

                                     var w = Ego.Weapons.SingleOrDefault(i => i.NetworkId == BarrelToTake.ExplosiveMaterialType.NetworkId);

                                     if (w != null)
                                     {
                                         BarrelToTake.RemoveFrom(Barrels).Orphanize();

                                         w.Ammo += BarrelToTake.ExplosiveMaterialType.Ammo;

                                         Sounds.sound20.ToSoundAsset().play();
                                     }
                                 }
                             }
                     };


                    var KeyLeft = new KeyboardButton(stage)
                    {
                        Buttons = new[] { Keyboard.LEFT, Keyboard.A },
                        Filter = EgoIsOnTheField.And(EgoIsAlive),
                        Down =
                            delegate
                            {
                                EgoAimMoveSpeed = -0.1;
                                EgoAimMoveTimer.start();
                            },
                        Up = EgoAimMoveTimer.stop,
                    };

                    var KeyRight = new KeyboardButton(stage)
                    {
                        Buttons = new[] { Keyboard.RIGHT, Keyboard.D },
                        Filter = EgoIsOnTheField.And(EgoIsAlive),
                        Down =
                            delegate
                            {
                                EgoAimMoveSpeed = +0.1;
                                EgoAimMoveTimer.start();
                            },
                        Up = EgoAimMoveTimer.stop,
                    };

                    var KeyUp = new KeyboardButton(stage)
                    {
                        Buttons = new[] { Keyboard.UP, Keyboard.W },
                        Filter = EgoIsOnTheField.And(EgoIsAlive),
                        Down =
                            delegate
                            {
                                if (!Ego.IsAlive)
                                    return;

                                Ego.RunAnimation = true;
                                EgoMoveSpeed = 2.5;
                                EgoMoveUpTimer.start();
                            },
                        Up =
                            delegate
                            {
                                EgoMoveUpTimer.stop();
                                Ego.RunAnimation = false;
                            }
                    };

                    var KeyDown = new KeyboardButton(stage)
                    {
                        Buttons = new[] { Keyboard.DOWN, Keyboard.S },
                        Filter = EgoIsOnTheField.And(EgoIsAlive),
                        Down =
                            delegate
                            {
                                if (!Ego.IsAlive)
                                    return;

                                Ego.RunAnimation = true;
                                EgoMoveSpeed = -1.5;
                                EgoMoveUpTimer.start();
                            },
                        Up =
                            delegate
                            {
                                EgoMoveSpeed = 1;
                                EgoMoveUpTimer.stop();
                                Ego.RunAnimation = false;
                            }
                    };

                    var KeyWeaponNext = new KeyboardButton(stage)
                    {
                        Buttons = new[] { Keyboard.X, Keyboard.PAGE_UP },
                        Up = EgoTakeNextWeapon,
                    };

                    var KeyWeaponPrevious = new KeyboardButton(stage)
                    {
                        Buttons = new[] { Keyboard.Z, Keyboard.PAGE_DOWN },
                        Up = EgoTakePreviousWeapon
                    };

                    var KeyMusic = new KeyboardButton(stage)
                    {
                        Buttons = new[] { Keyboard.M },
                        Up = () => ToggleMusic()
                    };


                    var KeyControl = new KeyboardButton(stage)
                    {
                        Buttons = new[] { Keyboard.CONTROL },
                        Filter = EgoIsOnTheField.And(EgoIsAlive),
                        Up = EgoDoFireWeapon
                    };


                    var KeyEnter = new KeyboardButton(stage)
                    {
                        Buttons = new[] { Keyboard.ENTER, Keyboard.F },
                        Filter = EgoIsAlive,
                        Up =
                            delegate
                            {
                                if (EgoIsOnTheField())
                                {
                                    if (EgoIsCloseToTurret())
                                    {
                                        if (EgoCanManTurret())
                                        {


                                            Ego.Orphanize();
                                            Ego.RunAnimation = false;
                                            EgoMoveUpTimer.stop();
                                            EgoAimMoveTimer.stop();
                                            PrebuiltTurretBlinkTimer.stop();
                                            PrebuiltTurret.alpha = 1;

                                            Ego.CurrentWeapon = Ego.Weapons.FirstOrDefault(i => i.SelectMode == Weapon.SelectModeEnum.Turret);

                                            ShowMessage("Machinegun manned!");
                                            Mouse.hide();

                                            if (CurrentTarget != null)
                                            {
                                                Aim.x = CurrentTarget.stageX;
                                                Aim.y = CurrentTarget.stageY;
                                            }

                                            Sounds.door_open.ToSoundAsset().play();

                                            if (EgoEnteredMachineGun != null)
                                                EgoEnteredMachineGun();
                                        }
                                        else
                                        {
                                            ShowMessage("Cannot man the machinegun!");
                                        }
                                    }
                                    else
                                    {
                                        ShowMessage("The machinegun is too far! Get closer!");
                                    }
                                }
                                else
                                {
                                    Sounds.door_open.ToSoundAsset().play();


                                    ShowMessage("Machinegun unmanned!");

                                    TeleportEgoNearTurret();
                                    PrebuiltTurret.AnimationEnabled = false;

                                    PrebuiltTurret.alpha = 0.5;
                                    Mouse.show();
                                    PrebuiltTurretBlinkTimer.start();



                                    if (EgoExitedMachineGun != null)
                                        EgoExitedMachineGun();
                                }
                            }
                    };



                };

            PrebuiltTurretBlinkTimer = 500.AtInterval(t => PrebuiltTurret.alpha = ((t.currentCount % 2) + 1) / 2);
            PrebuiltTurretBlinkTimer.stop();

            #region readiness
            if (stage == null)
            {
                this.addedToStage +=
                    delegate
                    {
                        StageIsReady();
                    };
            }
            else
            {
                StageIsReady();
            }
            #endregion

            //Mouse.hide();



            ShowMessage("Aim at the enemy unit and hold down the mouse!");
            ShowMessage("Press 'Enter' to exit the machinegun");
            //ShowMessage("Day " + CurrentLevel);

            GameEvent =
                delegate
                {
                    // do not add if the day is over...
                    //if (WaveEndCountdown <= 0)
                    //    return;

                    AddNewActorsToMap(UpdateScoreBoard, GetEntryPointY, AttachRules);

                };




            ReportDays =
                delegate
                {
                    if (WaveEndCountdown < 0)
                    {
                        if (InterlevelMusic == null)
                        {
                            InterlevelMusic = Sounds.snd_birds.ToSoundAsset().play(0, 999);
                            ShowMessage("Day " + CurrentLevel + " is ending...");

                            if (GameInterlevelBegin != null)
                                GameInterlevelBegin();
                        }

                        if (WaveEndsWhenAllBadGuysAreDead)
                        {
                            // wait for all actors get off stage
                            if (BadGuys.Where(i => i.IsAlive).Any())
                                return;
                        }


                        // show "level END"
                        ShowMessage("Day " + CurrentLevel + " Survived!");

                        ReportDaysTimer.stop();



                        InterlevelTimeout.AtDelayDo(
                            delegate
                            {
                                if (GameInterlevelEnd != null)
                                    GameInterlevelEnd();


                                // maybe higher levels will have more enemies?
                                WaveEndCountdown = 15;
                                CurrentLevel++;

                                // show "level START"
                                ShowMessage("Day " + CurrentLevel);
                                ReportDaysTimer.start();

                                var InterlevelMusicStopping = InterlevelMusic;
                                InterlevelMusic = null;
                                2000.AtDelayDoOnRandom(InterlevelMusicStopping.stop);
                            }
                        );


                        UpdateScoreBoard();

                        return;
                    }


                    if (CanAutoSpawnEnemies)
                    {
                        // new actors if we got less 10 
                        if (InterlevelMusic != null)
                            return;

                        if (BadGuys.Where(i => i.IsAlive).Count() < 8)
                        {

                            GameEvent();
                        }
                    }
                };

            ReportDaysTimer = 1500.AtIntervalDo(ReportDays);


            (1000 / 15).AtInterval(
                delegate
                {


                    Aim.rotation += 1;

                    foreach (var s in
                           from ss in BadGuys
                           where ss.IsAlive
                           select ss)
                        s.x += s.speed;
                }
            );

            #region powered_by_jsc
            var powered_by_jsc = new TextField
            {

                x = 32,

                defaultTextFormat = new TextFormat
                {
                    size = 24
                },
                autoSize = TextFieldAutoSize.LEFT,

                // how to make a link
                // http://www.actionscript.com/Article/tabid/54/ArticleID/actionscript-quick-tips-and-gotchas/Default.aspx
                htmlText = "<a href='http://jsc.sf.net' target='_blank'>powered by <b>jsc</b></a>",
                selectable = false,
                filters = new[] { new BlurFilter() },
                textColor = ColorBlack
            }.AttachTo(this);

            powered_by_jsc.y = DefaultHeight - powered_by_jsc.height - 32;

            // make it fade/show in time
            200.AtInterval(t => powered_by_jsc.alpha = (Math.Sin(t.currentCount * 0.05) + 1) * 0.5);

            #endregion

            ScoreBoard.AttachTo(this);

            BlurWarzoneOnHover(powered_by_jsc, true);

            40000.AtIntervalOnRandom(
                delegate
                {
                    Sounds.snd_bird2.ToSoundAsset().play();
                }
            );


            #region music on off
            var MusicButton = new Sprite
            {
                x = DefaultWidth - 32,
                y = 32,
                filters = new[] { new GlowFilter(ColorBlueLight) },

            };

            var MusicOn = Images.music_on.ToBitmapAsset().MoveToCenter();
            var MusicOff = Images.music_off.ToBitmapAsset().MoveToCenter();

            MusicButton.mouseOver +=
                delegate
                {
                    Mouse.show();
                    Aim.visible = false;
                };

            MusicButton.mouseOut +=
              delegate
              {
                  Mouse.hide();
                  Aim.visible = true;
              };

            ToggleMusic =
                delegate
                {
                    if (MusicOn.parent == MusicButton)
                    {
                        MusicOn.Orphanize();
                        MusicOff.AttachTo(MusicButton);
                        ShowMessage("Music silenced");
                        IngameMusic.soundTransform = new SoundTransform(0);
                    }
                    else
                    {
                        IngameMusic.soundTransform = new SoundTransform(IngameMusicVolume);
                        ShowMessage("Music activated");

                        MusicOff.Orphanize();
                        MusicOn.AttachTo(MusicButton);
                    }
                };

            MusicButton.click +=
                delegate
                {
                    ToggleMusic();
                };

            MusicOn.AttachTo(MusicButton);

            MusicButton.AttachTo(this);
            #endregion

            Action<InteractiveObject, InteractiveObject> OnMouseDownDisableMouseOnTarget =
                (subject, target) =>
                {
                    subject.mouseDown += delegate { target.mouseEnabled = false; };
                    subject.mouseUp += delegate { target.mouseEnabled = true; };
                };

            OnMouseDownDisableMouseOnTarget(GetWarzone(), MusicButton);
            OnMouseDownDisableMouseOnTarget(GetWarzone(), ScoreBoard);
            OnMouseDownDisableMouseOnTarget(GetWarzone(), powered_by_jsc);
        }

        private void DoSomeDamage(Point DamagePointOfOrigin, double DamageDirection, WeaponInfo Weapon)
        {

            ShowBulletsFlying(DamagePointOfOrigin, DamageDirection, Weapon);

            if (NetworkShowBulletsFlying != null)
                NetworkShowBulletsFlying(
                    DamagePointOfOrigin.x.ToInt32(),
                    DamagePointOfOrigin.y.ToInt32(),
                    DamageDirection.RadiansToDegrees(),
                    Weapon.NetworkId
                    );

            var AllMortals = this.AllMortals.ToArray();

            //Console.WriteLine("mortals in warzone: " + AllMortals.Length);

            foreach (var DeadManWalking in AllMortals)
            {
                var DamageThisDude = true;

                // why bother shooting itself?
                if (DeadManWalking == Ego)
                    if (!Weapon.CanHurtEgo)
                        DamageThisDude = false;

                if (!DeadManWalking.IsAlive)
                    DamageThisDude = false;

                if (DamageThisDude)
                {
                    var Location = new Point { x = DamagePointOfOrigin.x, y = DamagePointOfOrigin.y }.MoveToArc(DamageDirection, -32);
                    var Offset = new Point { y = DeadManWalking.y - Location.y, x = DeadManWalking.x - Location.x };
                    var Arc = Offset.GetRotation();
                    var Distance = Offset.length;

                    Func<double, double> Warp = i => i % (Math.PI * 2);

                    var LessThan = Arc < (Warp(DamageDirection) + Weapon.ArcRange);
                    var MoreThan = Arc > (Warp(DamageDirection) - Weapon.ArcRange);

                    var Hit = LessThan && MoreThan;

                    if (Distance < Weapon.Range)
                        if (Hit)
                        {
                            // calculate damage variation here based on hit approximity

                            DeadManWalking.AddDamageFromDirection(Weapon.Damage, Arc);

                            if (NetworkAddDamageFromDirection != null)
                                NetworkAddDamageFromDirection(
                                    DeadManWalking.NetworkId,
                                    Weapon.Damage,
                                    Arc.RadiansToDegrees()
                                );
                        }
                }

            }
        }

        public void ShowBulletsFlying(Point DamagePointOfOrigin, double DamageDirection, WeaponInfo Weapon)
        {
            if (Weapon == null)
                return;

            (1 + ((Weapon.VisibleBulletLines - 1).Random())).Times(
                delegate
                {
                    var VisibleBullet = new Shape().AttachTo(GetWarzone());

                    var RandomGray = (0x40 + 0x80.Random()).ToInt32();

                    VisibleBullet.graphics.lineStyle(1, RandomGray.ToGrayColor(), 1);


                    var BulletDirection = DamageDirection + (2.0.Random() - 1.0) * (Weapon.ArcRange * 0.75);
                    var BulletDropFromRange = 32 + (Weapon.Range / 2 - 32).Random();
                    var BulletDropFrom = DamagePointOfOrigin.MoveToArc(BulletDirection, BulletDropFromRange);

                    VisibleBullet.graphics.moveTo(BulletDropFrom.x, BulletDropFrom.y);

                    var BulletDropTo = DamagePointOfOrigin.MoveToArc(BulletDirection, BulletDropFromRange + (Weapon.Range.Random() / 2));

                    VisibleBullet.graphics.lineTo(BulletDropTo.x, BulletDropTo.y);

                    50.AtDelayDo(() => VisibleBullet.Orphanize());
                }
            );
        }

        public void TeleportEgoNearTurret()
        {
            Ego.CurrentWeapon = Ego.Weapons.FirstOrDefault(i => i.SelectMode == Weapon.SelectModeEnum.Outside);


            Ego.RunAnimation = false;
            Ego.CanMakeFootsteps = false;

            var Outside = new Point { x = PrebuiltTurret.x, y = PrebuiltTurret.y }.MoveToArc((Math.PI * 2).Random(), 64);

            Ego.MoveTo(Outside.x, Outside.y).AttachTo(GetWarzone());

            UpdateEgoAim();
        }


        public readonly List<Crate> Boxes = new List<Crate>();

        public readonly Settings Settings = new Settings();

        public readonly Action<string> ShowMessage;

        public event Action EgoEnteredMachineGun;
        public event Action EgoExitedMachineGun;

        public bool CanAutoSpawnEnemies = true;

        public readonly Action ToggleMusic;

        public readonly Func<bool> EgoIsOnTheField;
        public readonly Func<bool> EgoIsAlive;

        public bool PrebuiltTurretInUse;

        public readonly PlayerWarrior Ego;
        public readonly Action UpdateEgoAim;
        public readonly Timer EgoMovedSlowTimer;

        public event Action<Weapon> EgoFiredWeapon;
        public event Action EgoResurrect;

        public readonly Action GameEvent;


        public event Action GameInterlevelBegin;
        public event Action GameInterlevelEnd;

        public SoundChannel InterlevelMusic;

        public event Action<int /* x */, int /* y */, int /* arc */, int /* weapon */> NetworkShowBulletsFlying;
        public event Action<int, int, int> NetworkAddDamageFromDirection;
        public event Action<int> NetworkTakeCrate;


        // Between levels the player upgrades, collects items, etc...
        public int CurrentLevel = 1;

        // If this gets negative, we end this level and pause... maybe send a big boss, too?
        public int WaveEndCountdown = 15;
        public bool WaveEndsWhenAllBadGuysAreDead = true;

        public const int InterlevelTimeoutDefault = 8000;
        public int InterlevelTimeout = InterlevelTimeoutDefault;

        public readonly List<Actor> BadGuys = new List<Actor>();
        public readonly List<ExplosiveBarrel> Barrels = new List<ExplosiveBarrel>();
        public readonly List<Func<IEnumerable<Actor>>> GoodGuys = new List<Func<IEnumerable<Actor>>>();

        public IEnumerable<Actor> AllMortals
        {
            get
            {
                var a = new List<Actor>();

                a.AddRange(BadGuys);

                foreach (var v in Barrels)
                {
                    a.Add(v);
                }

                a.Add(Ego);

                foreach (var v in GoodGuys)
                    a.AddRange(v());

                return a;
            }
        }

        public Action ReportDays;

        public Timer ReportDaysTimer;

        double EgoAimDirection = 3.14; // look down

        bool EgoIsReloadingHisWeapon = false;

        public void EgoDoFireWeapon()
        {
            if (EgoIsReloadingHisWeapon)
                return;

            if (Ego.CurrentWeapon.Ammo <= 0)
            {
                Sounds.OutOfAmmo.ToSoundAsset().play();
                // need ammo
                return;
            }

            EgoIsReloadingHisWeapon = true;

            500.AtDelayDo(() => EgoIsReloadingHisWeapon = false);

            Ego.CurrentWeapon.Type.SoundFire.ToSoundAsset().play();

            if (EgoFiredWeapon != null)
                EgoFiredWeapon(Ego.CurrentWeapon);


            Ego.CurrentWeapon.Ammo--;


            if (Ego.CurrentWeapon.Usage == Weapon.UsageEnum.FireBullets)
            {
                //ShowMessage("Bang!");

                var DamagePointOfOrigin = new Point { x = Ego.x, y = Ego.y };
                var DamageDirection = EgoAimDirection;

                DoSomeDamage(DamagePointOfOrigin, DamageDirection, Ego.CurrentWeapon.Type);
            }
            else
            {
                //ShowMessage("Deploy!");

                if (Ego.CurrentWeapon.Usage == Weapon.UsageEnum.DeployBarrel)
                {
                    //  ShowMessage("Barrel deployed");

                    var barrel = new ExplosiveBarrel
                        {
                            ExplosiveMaterialType = Ego.CurrentWeapon.Clone(),
                            
                        };

                    barrel.ExplosiveMaterialType.Ammo = 1;

                    barrel.HealthChangedToWorse +=
                        delegate
                        {
                            if (!barrel.IsAlive)
                                return;

                            barrel.filters = new[] { new GlowFilter(ColorRed, 0.3) };

                            500.AtDelayDo(
                                delegate
                                {
                                    if (!barrel.IsAlive)
                                        return;

                                    barrel.AddDamage(barrel.Health);
                                    // notify network?
                                }
                            );
                        };

                    barrel.MoveTo(Ego).AttachTo(GetWarzone()).AddTo(Barrels);

                    barrel.Die +=
                        delegate
                        {
                            barrel.RemoveFrom(Barrels).Orphanize();

                            var hole = new Animation(Images.hole_1).MoveTo(barrel).AttachTo(GetWarzone());

                            (10000 + 10000.Random().ToInt32()).AtDelayDo(
                                 delegate
                                 {
                                     hole.Orphanize();

                                     hole = new Animation(Images.hole_2).MoveTo(barrel).AttachTo(GetWarzone());
                                 },
                                 delegate
                                 {
                                     hole.Orphanize();
                                 }
                            );

                            // damage objects around the barrel

                            var Range = barrel.ExplosiveMaterialType.Type.Range;

                            foreach (var v in from i in AllMortals
                                              let p = i.ToPoint() - barrel.ToPoint()
                                              where p.length < Range
                                              select new { i, p })
                            {
                                var Damage = barrel.ExplosiveMaterialType.Type.Damage * ((Range - v.p.length) / Range);

                                v.i.AddDamageFromDirection(Damage, v.p.GetRotation());
                            }

                            var a = new Animation(null, Images.Explosions.ani6);

                            a.LastFrame +=
                                delegate
                                {
                                    a.AnimationEnabled = false;
                                    a.Orphanize();
                                };


                            a.MoveTo(barrel).AttachTo(GetWarzone());
                            a.AnimationEnabled = true;
                        };
                }
                else
                    ShowMessage("Unknown weapon usage");
            }
        }
    }












}
