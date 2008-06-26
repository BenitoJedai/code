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
    [Script, ScriptApplicationEntryPoint(Width = Width, Height = Height)]
    [SWF(width = Width, height = Height, backgroundColor = ColorWhite)]
    public partial class FlashTowerDefense : Sprite
    {
        public const int Width = 560;
        public const int Height = 480;

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
            warzone.graphics.drawRect(-OffscreenMargin, -OffscreenMargin, Width + 2 * OffscreenMargin, Height + 2 * OffscreenMargin);

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
                (m, h) => (Width * Height * m).Times(h);

            Action<double, Func<BitmapAsset>> AddDoodads =
                (m, GetImage) => Times(m, () => GetImage().AttachTo(bg).SetCenteredPosition(Width.Random(), Height.Random()));

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
                    var x = Width.Random();

                    AddCactus().AttachTo(GetWarzone()).MoveTo(
                        x, y + Math.Cos(x + y) * Height * 0.03);
                };

            (3 + 3.Random()).Times(AddCactusAt.FixParam(Height * 0.06));
            (3 + 3.Random()).Times(AddCactusAt.FixParam(Height * 0.94));

            PrebuiltTurret = new Animation(Images.img_turret1_gunfire_180, Images.img_turret1_gunfire_180_frames);

            PrebuiltTurret.x = (Width - PrebuiltTurret.width) * 0.9;
            PrebuiltTurret.y = (Height - PrebuiltTurret.height) / 2;

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

                    var y = Height - p.height - 32;

                    p.AddTo(ActiveMessages).AttachTo(this).MoveTo((Width - p.width) / 2, Height);

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



            #region Ego
            Ego = new Warrior();

            EgoIsOnTheField = () => Ego.parent != null;
            Func<bool> EgoCanManTurret = () => !PrebuiltTurretInUse; // look up if there is somebody else in it
            Func<bool> EgoIsCloseToTurret = () => new Point { x = Ego.x - PrebuiltTurret.x, y = Ego.y - PrebuiltTurret.y }.length < 32;

            var EgoAimDirection = 3.14; // look down
            var EgoAimDistance = 48;
            var EgoAimMoveSpeed = 0.1;
            var EgoMoveSpeed = 3.0;

            UpdateEgoAim =
                delegate
                {
                    Aim.MoveTo(Ego.x + Math.Cos(EgoAimDirection) * EgoAimDistance, Ego.y + Math.Sin(EgoAimDirection) * EgoAimDistance);
                };


            EgoMovedSlowTimer = new Timer(300, 1);

            var EgoMoveUpTimer = (1000 / 30).AtInterval(
                delegate
                {
                    Ego.MoveToArc(EgoAimDirection, EgoMoveSpeed);
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


                        // add stuff, so the player doesn't get bored:D
                        // maybe implment them too? see Diablo.
                        var PowerUps = new[] { "Defense", "Mana", "Dexerity", "Experience", "Stamina", "Strength", "Life", "Replenish Life" };


                        ShowMessage("+1 " + PowerUps.Random());

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

            var PrebuiltTurretSound = default(SoundChannel);






            BadGuys = new List<Actor>();

            var BulletsFired_MachineGun = 0;
            var BulletsFired_Shotgun = 0;

            var runaways = 0;
            var score = 0;

            // Between levels the player upgrades, collects items, etc...
            var CurrentLevel = 1;

            // If this gets negative, we end this level and pause... maybe send a big boss, too?
            var WaveEndCountdown = 15;


            var InterlevelTimeout = 12000;

            Action UpdateScoreBoard =
                delegate
                {
                    if (!CanFire) return;

                    ScoreBoard.text =
                        new
                        {
                            bullets = BulletsFired_MachineGun,
                            runaways,
                            gore = (100 * (double)BadGuys.Count(i => !i.IsAlive) / (double)BadGuys.Count()).Round() + "%",
                            score,
                            level = CurrentLevel
                        }.ToString();
                };

            Action<MouseEvent> DoGunFire =
                e =>
                {
                    BulletsFired_MachineGun--;

                    UpdateScoreBoard();

                    var DamagePointOfOrigin = new Point { x = PrebuiltTurret.x, y = PrebuiltTurret.y };
                    var DamageDirection = new Point { x = e.stageX - PrebuiltTurret.x, y = e.stageY - PrebuiltTurret.y }.GetRotation();

                    DoSomeDamage(DamagePointOfOrigin, DamageDirection, WeaponInfo.Machinegun);


                };

            var CurrentTarget = default(MouseEvent);
            var CurrentTargetTimer = default(Timer);

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

                                DoGunFire(CurrentTarget);
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





            Func<double> GetEntryPointY = () => (Height * 0.8).FixedRandom() + Height * 0.1;



            #region AttachRules
            Func<Actor, Actor> AttachRules =
                a =>
                {
                    if (a == null)
                        throw new Exception("AttachRules");

                    if (a.NetworkId == 0)
                    {
                        a.NetworkId = int.MaxValue.FixedRandom();

                        if (0.3.ByChance())
                        {
                            a.Crate = new Animation(Images.box).AddTo(Boxes);
                            a.Crate.NetworkId = int.MaxValue.FixedRandom();
                        }
                    }

                    a.CorpseAndBloodGone += () => BadGuys.Remove(a);
                    a.Moved +=
                        delegate
                        {
                            if (a.x > (Width + OffscreenMargin))
                            {
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



            Action StageIsReady =
                delegate
                {

                    //stage.scaleMode = StageScaleMode.NO_BORDER;

                    // multiplay ?

                    var KeyLeft = new KeyboardButton(stage)
                    {
                        Buttons = new[] { Keyboard.LEFT, Keyboard.A },
                        Filter = EgoIsOnTheField,
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
                        Filter = EgoIsOnTheField,
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
                        Filter = EgoIsOnTheField,
                        Down =
                            delegate
                            {
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
                        Filter = EgoIsOnTheField,
                        Down =
                            delegate
                            {
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

                    var KeyMusic = new KeyboardButton(stage)
                    {
                        Buttons = new[] { Keyboard.M },
                        Up = () => ToggleMusic()
                    };

                    var ShotgunReloading = false;

                    var KeyControl = new KeyboardButton(stage)
                    {
                        Buttons = new[] { Keyboard.CONTROL },
                        Filter = EgoIsOnTheField,
                        Up =
                            delegate
                            {
                                if (ShotgunReloading)
                                    return;

                                ShotgunReloading = true;

                                500.AtDelayDo(() => ShotgunReloading = false);

                                Sounds.shotgun2.ToSoundAsset().play();

                                if (EgoFiredShotgun != null)
                                    EgoFiredShotgun();

                                BulletsFired_Shotgun++;

                                var DamagePointOfOrigin = new Point { x = Ego.x, y = Ego.y };
                                var DamageDirection = EgoAimDirection;

                                DoSomeDamage(DamagePointOfOrigin, DamageDirection, WeaponInfo.Shotgun);
                            }
                    };


                    var KeyEnter = new KeyboardButton(stage)
                    {
                        Buttons = new[] { Keyboard.ENTER, Keyboard.F },
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
                                    PrebuiltTurret.alpha = 0.5;
                                    Mouse.show();
                                    PrebuiltTurretBlinkTimer.start();

                                    TeleportEgoNearTurret();

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


                    AddNewActorsToMap(UpdateScoreBoard, GetEntryPointY, AttachRules);

                };


            (1500).AtInterval(
                t =>
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

                        // wait for all actors get off stage
                        if (BadGuys.Where(i => i.IsAlive).Any())
                            return;


                        // show "level END"
                        ShowMessage("Day " + CurrentLevel + " Survived!");

                        t.stop();



                        InterlevelTimeout.AtDelayDo(
                            delegate
                            {
                                if (GameInterlevelEnd != null)
                                    GameInterlevelEnd();


                                // show "level START"
                                ShowMessage("Day " + CurrentLevel);
                                t.start();

                                var InterlevelMusicStopping = InterlevelMusic;
                                InterlevelMusic = null;
                                10000.AtDelayDoOnRandom(InterlevelMusicStopping.stop);
                            }
                        );

                        // maybe higher levels will have more enemies?
                        WaveEndCountdown = 15;
                        CurrentLevel++;
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
                }
            );


            (1000 / 24).AtInterval(
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

            powered_by_jsc.y = Height - powered_by_jsc.height - 32;
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
                x = Width - 32,
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

            foreach (var DeadManWalking in BadGuys.ToArray())
            {
                if (DeadManWalking.IsAlive)
                {
                    var Location = new Point { x = DamagePointOfOrigin.x, y = DamagePointOfOrigin.y }.MoveToArc(DamageDirection, -16);
                    var Offset = new Point { y = DeadManWalking.y - Location.y, x = DeadManWalking.x - Location.x };
                    var Arc = Offset.GetRotation();
                    var Distance = Offset.length;
                    var LessThan = Arc < (((DamageDirection + Weapon.ArcRange) % (Math.PI * 2)));
                    var MoreThan = Arc > (((DamageDirection - Weapon.ArcRange) % (Math.PI * 2)));
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

            (1 + (Weapon.VisibleBulletLines - 1.Random())).Times(
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
            Ego.RunAnimation = false;
            Ego.CanMakeFootsteps = false;

            var Outside = new Point { x = PrebuiltTurret.x, y = PrebuiltTurret.y }.MoveToArc((Math.PI * 2).Random(), 64);

            Ego.MoveTo(Outside.x, Outside.y).AttachTo(GetWarzone());

            UpdateEgoAim();
        }


        public readonly List<Animation> Boxes = new List<Animation>();

        public readonly Settings Settings = new Settings();

        public readonly Action<string> ShowMessage;

        public event Action EgoEnteredMachineGun;
        public event Action EgoExitedMachineGun;

        public bool CanAutoSpawnEnemies = true;

        public readonly Action ToggleMusic;

        public readonly Func<bool> EgoIsOnTheField;

        public bool PrebuiltTurretInUse;

        public readonly Warrior Ego;
        public readonly Action UpdateEgoAim;
        public readonly Timer EgoMovedSlowTimer;

        public event Action EgoFiredShotgun;

        public readonly Action GameEvent;


        public event Action GameInterlevelBegin;
        public event Action GameInterlevelEnd;

        public SoundChannel InterlevelMusic;

        public event Action<int /* x */, int /* y */, int /* arc */, int /* weapon */> NetworkShowBulletsFlying;
        public event Action<int, int, int> NetworkAddDamageFromDirection;
        public event Action<int> NetworkTakeCrate;


        public readonly List<Actor> BadGuys;
    }












}
