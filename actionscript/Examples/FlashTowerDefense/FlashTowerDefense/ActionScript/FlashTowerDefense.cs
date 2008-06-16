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
using ScriptCoreLib.ActionScript.Lambda;
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
        public const int Width = 800;
        public const int Height = 600;

        public const uint ColorRed = 0xff0000;
        public const uint ColorBlack = 0x000000;
        public const uint ColorWhite = 0xffffff;
        public const uint ColorBlue = 0x0000ff;
        public const uint ColorBlueDark = 0x000080;
        public const uint ColorBlueLight = 0x9090ff;

        const int OffscreenMargin = 32;

        Animation PrebuiltTurret;
        Timer PrebuiltTurretBlinkTimer;

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
            var Ego = new Warrior();
            Func<bool> EgoIsOnTheField = () => Ego.parent != null;
            Func<bool> EgoCanManTurret = () => true; // look up if there is somebody else in it
            Func<bool> EgoIsCloseToTurret = () => new Point { x = Ego.x - PrebuiltTurret.x, y = Ego.y - PrebuiltTurret.y }.length < 32;

            var EgoAimDirection = 3.14; // look down
            var EgoAimDistance = 48;
            var EgoAimMoveSpeed = 0.1;
            var EgoMoveSpeed = 3;

            Action UpdateEgoAim =
                delegate
                {
                    Aim.MoveTo(Ego.x + Math.Cos(EgoAimDirection) * EgoAimDistance, Ego.y + Math.Sin(EgoAimDirection) * EgoAimDistance);
                };

            var EgoMoveUpTimer = (1000 / 30).AtInterval(
                delegate
                {
                    Ego.MoveToArc(EgoAimDirection, EgoMoveSpeed);
                    UpdateEgoAim();

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

            var f = new[] { new GlowFilter() };




            var list = new List<Actor>();

            var BulletsFired_MachineGun = 0;
            var BulletsFired_Shotgun = 0;

            var runaways = 0;
            var score = 0;

            // Between levels the player upgrades, collects items, etc...
            var CurrentLevel = 1;

            // If this gets negative, we end this level and pause... maybe send a big boss, too?
            var WaveEndCountdown = 30;

            var InterlevelMusic = default(SoundChannel);
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
                            gore = (100 * (double)list.Count(i => !i.IsAlive) / (double)list.Count()).Round() + "%",
                            score,
                            level = CurrentLevel
                        }.ToString();
                };

            Action<MouseEvent> DoGunFire =
                e =>
                {
                    BulletsFired_MachineGun--;

                    UpdateScoreBoard();


                    foreach (var s in
                   from ss in list
                   where ss.IsAlive
                   where new Point { x = ss.x - e.stageX, y = ss.y - e.stageY }.length < 32
                   select ss)
                        s.AddDamageFromDirection(GetRandomHitDamage(), new Point { x = s.x - PrebuiltTurret.x, y = s.y - PrebuiltTurret.y }.GetRotation());
                };

            var CurrentTarget = default(MouseEvent);
            var CurrentTargetTimer = default(Timer);


            GetWarzone().mouseDown +=
                e =>
                {
                    if (!CanFire) return;

                    // the turret cannot fire without a man behind it
                    if (EgoIsOnTheField())
                        return;

                    if (PrebuiltTurretSound != null)
                        PrebuiltTurretSound.stop();

                    PrebuiltTurretSound = Sounds.gunfire.ToSoundAsset().play(0, 999);

                    PrebuiltTurret.filters = f;

                    PrebuiltTurret.AnimationEnabled = true;



                    CurrentTarget = e;
                    CurrentTargetTimer =
                        (1000 / 10).AtInterval(
                            delegate
                            {
                                if (!PrebuiltTurret.AnimationEnabled)
                                {
                                    CurrentTargetTimer.stop();
                                    PrebuiltTurretSound.stop();
                                    PrebuiltTurret.filters = null;
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
                    CurrentTarget = e;

                    if (EgoIsOnTheField())
                    {
                        return;
                    }

                    Aim.x = CurrentTarget.stageX;
                    Aim.y = CurrentTarget.stageY;

                };





            Func<double> GetEntryPointY = () => (Height * 0.8).Random() + Height * 0.1;



            #region AttachRules
            Func<Actor, Actor> AttachRules =
                a =>
                {
                    if (a == null)
                        throw new Exception("AttachRules");

                    a.CorpseAndBloodGone += () => list.Remove(a);
                    a.Moved +=
                        delegate
                        {
                            if (a.x > (Width + OffscreenMargin))
                            {
                                a.RemoveFrom(list).Orphanize();

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


                            if (0.3.ByChance())
                            {
                                new Animation(Images.box).AddTo(Boxes).MoveTo(a.x, a.y).AttachTo(GetWarzone());
                            }
                        };

                    a.AttachTo(GetWarzone()).AddTo(list);

                    if (a.PlayHelloSound != null)
                        a.PlayHelloSound();

                    return a;
                };
            #endregion



            Action StageIsReady =
                delegate
                {
                    stage.scaleMode = StageScaleMode.NO_BORDER;

                    #region keyboard
                    stage.keyDown +=
                        e =>
                        {
                            if (EgoIsOnTheField())
                            {
                                if (e.keyCode == Keyboard.LEFT)
                                {
                                    EgoAimMoveSpeed = -0.1;
                                    EgoAimMoveTimer.start();
                                }
                                else if (e.keyCode == Keyboard.RIGHT)
                                {
                                    EgoAimMoveSpeed = 0.1;
                                    EgoAimMoveTimer.start();
                                }
                                else if (e.keyCode == Keyboard.UP)
                                {
                                    Ego.RunAnimation = true;
                                    EgoMoveSpeed = 2;
                                    EgoMoveUpTimer.start();
                                }
                                else if (e.keyCode == Keyboard.DOWN)
                                {
                                    Ego.RunAnimation = true;
                                    EgoMoveSpeed = -1;
                                    EgoMoveUpTimer.start();
                                }
                                else if (e.keyCode == Keyboard.CONTROL)
                                {



                                }
                            }
                        };

                    stage.keyUp +=
                      e =>
                      {
                          if (!CanFire)
                              return;

                          if (EgoIsOnTheField())
                          {
                              if (e.keyCode == Keyboard.LEFT)
                              {
                                  EgoAimMoveTimer.stop();
                              }
                              else if (e.keyCode == Keyboard.RIGHT)
                              {
                                  EgoAimMoveTimer.stop();
                              }
                              else if (e.keyCode == Keyboard.UP)
                              {
                                  Ego.RunAnimation = false;
                                  EgoMoveUpTimer.stop();
                              }
                              else if (e.keyCode == Keyboard.DOWN)
                              {
                                  Ego.RunAnimation = false;
                                  EgoMoveUpTimer.stop();
                              }
                              else if (e.keyCode == Keyboard.CONTROL)
                              {

                                  if (EgoIsOnTheField())
                                  {
                                      Sounds.shotgun2.ToSoundAsset().play();

                                      BulletsFired_Shotgun++;

                                      foreach (var DeadManWalking in list.ToArray())
                                      {
                                          if (DeadManWalking.IsAlive)
                                          {
                                              var Location = new Point { x = Ego.x, y = Ego.y }.MoveToArc(EgoAimDirection, -16);
                                              var Offset = new Point { y = DeadManWalking.y - Location.y, x = DeadManWalking.x - Location.x };
                                              var Arc = Offset.GetRotation();
                                              var Distance = Offset.length;
                                              var LessThan = Arc < ((EgoAimDirection % (Math.PI * 2)) + 0.4);
                                              var MoreThan = Arc > ((EgoAimDirection % (Math.PI * 2)) - 0.4);
                                              var Hit = LessThan && MoreThan;
                                              var Max = 180;

                                              if (Distance < Max)
                                                  if (Hit)
                                                  {
                                                      var Damage = 60.Random() + 40;


                                                      DeadManWalking.AddDamageFromDirection(Damage, Arc);


                                                  }
                                          }

                                      }
                                  }
                              }
                          }

                          if (e.keyCode == Keyboard.ENTER)
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
                                          Aim.x = CurrentTarget.stageX;
                                          Aim.y = CurrentTarget.stageY;
                                          Sounds.door_open.ToSoundAsset().play();
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
                                  PrebuiltTurretBlinkTimer = 500.AtInterval(t => PrebuiltTurret.alpha = ((t.currentCount % 2) + 1) / 2);

                                  Ego.RunAnimation = false;
                                  Ego.CanMakeFootsteps = false;

                                  Ego.MoveTo(PrebuiltTurret.x + 48, PrebuiltTurret.y).AttachTo(GetWarzone());

                                  UpdateEgoAim();
                              }
                          }
                          //else
                          //{
                          //    ShowMessage(new { e.charCode, e.keyCode, e.keyLocation }.ToString());
                          //}
                      };
                    #endregion

                };

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

            Mouse.hide();



            ShowMessage("Aim at the enemy unit and hold down the mouse!");
            ShowMessage("Press 'Enter' to exit the machinegun");
            //ShowMessage("Day " + CurrentLevel);



            (1500).AtInterval(
                t =>
                {



                    if (WaveEndCountdown < 0)
                    {
                        if (InterlevelMusic == null)
                        {
                            InterlevelMusic = Sounds.snd_birds.ToSoundAsset().play(0, 999);
                            ShowMessage("Day " + CurrentLevel + " is ending...");

                        }

                        // wait for all actors get off stage
                        if (list.Where(i => i.IsAlive).Any())
                            return;


                        // show "level END"
                        ShowMessage("Day " + CurrentLevel + " Survived!");

                        t.stop();



                        InterlevelTimeout.AtDelayDo(
                            delegate
                            {
                                // show "level START"
                                ShowMessage("Day " + CurrentLevel);
                                t.start();

                                var InterlevelMusicStopping = InterlevelMusic;
                                InterlevelMusic = null;
                                10000.AtDelayDoOnRandom(InterlevelMusicStopping.stop);
                            }
                        );

                        // maybe higher levels will have more enemies?
                        WaveEndCountdown = 30;
                        CurrentLevel++;
                        UpdateScoreBoard();

                        return;
                    }


                    // new actors if we got less 10 
                    if (list.Where(i => i.IsAlive).Count() < 8)
                    {

                        AddNewActorsToMap(UpdateScoreBoard, GetEntryPointY, AttachRules);
                    }
                }
            );


            (1000 / 24).AtInterval(
                delegate
                {
                    //if (EgoIsOnTheField())
                    //{
                    //    foreach (var DeadManWalking in list)
                    //    {
                    //        var Offset = new Point { y = DeadManWalking.y - Ego.y, x = DeadManWalking.x - Ego.x };
                    //        var Arc = Offset.GetRotation();
                    //        var Distance = Offset.length;
                    //        var LessThan = Arc < (EgoAimDirection + 0.3);
                    //        var MoreThan = Arc > (EgoAimDirection - 0.3);
                    //        var Hit = LessThan && MoreThan;

                    //        if (Hit)
                    //            DeadManWalking.filters = new[] { new GlowFilter() };
                    //        else
                    //            DeadManWalking.filters = null;
                    //    }
                    //}

                    Aim.rotation += 1;

                    foreach (var s in
                           from ss in list
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

            MusicButton.click +=
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
                               speed = 1 + 2.Random(),
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
                                    IsBleeding = true
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
                             speed = 0.5 + 2.Random()
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

                    Enumerable.Range(1, (2.Random() + 1).ToInt32()).ForEach(
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
                                    IsBleeding = true
                                }
                            );

                            // remove the glow from the old boss cuz we respawned
                            boss.filters = null;

                            Action<Sheep> AddMinnion = i => AttachRules(i).AddTo(Minnions);

                            AddMinnion.ToForEach()(
                                from i in Minnions
                                where i.IsCorpseGone
                                where !i.IsCorpseAndBloodGone
                                select new Sheep
                                {
                                    x = i.x,
                                    y = i.y,
                                    speed = newboss.speed,
                                    IsBleeding = true
                                }
                            );



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
                    AttachRules(
                      new NuclearWarrior
                      {
                          x = -OffscreenMargin,
                          y = GetEntryPointY(),
                          speed = 1 + 2.Random()
                      }
                  );
                }
                else if (0.3.ByChance())
                {


                    AttachRules(
                        new Warrior
                        {
                            x = -OffscreenMargin,
                            y = GetEntryPointY(),
                            speed = 1 + 2.Random()
                        }
                    );


                }
                else
                {
                    AttachRules(
                        new Sheep
                        {
                            x = -OffscreenMargin,
                            y = GetEntryPointY(),
                            speed = 0.5 + 2.Random()
                        }
                    );

                }
            }

            UpdateScoreBoard();
        }

        public readonly List<Animation> Boxes = new List<Animation>();

        public readonly Settings Settings = new Settings();

        public readonly Action<string> ShowMessage;
    }












}
