﻿using ScriptCoreLib;
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


namespace FlashTowerDefense.ActionScript
{

    /// <summary>
    /// testing...
    /// </summary>
    [Script, ScriptApplicationEntryPoint(Width = Width, Height = Height)]
    [SWF(width = Width, height = Height, backgroundColor = ColorWhite)]
    public class FlashTowerDefense : Sprite
    {
        public const int Width = 640;
        public const int Height = 480;

        public const uint ColorRed = 0xff0000;
        public const uint ColorBlack = 0x000000;
        public const uint ColorWhite = 0xffffff;
        public const uint ColorBlue = 0x0000ff;
        public const uint ColorBlueDark = 0x000080;
        public const uint ColorBlueLight = 0x9090ff;

        const int OffscreenMargin = 32;

        Animation turret;

        public readonly Func<DisplayObjectContainer> GetWarzone;
        public readonly Action<TextField, bool> BlurWarzoneOnHover;

        public bool CanFire = true;

        public readonly Shape Aim;

        public readonly SoundChannel music;

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


            var ScoreBoard = new TextField
            {
                x = 24,
                y = 24,
                
                defaultTextFormat = new TextFormat
                {
                    size = 24
                },
                autoSize = TextFieldAutoSize.LEFT,
                filters = new[] { new BlurFilter() },
                text = "Defend yourself by shooting those mad sheep.",
                selectable = false,
            };

            ScoreBoard.AttachTo(this);


            BlurWarzoneOnHover(ScoreBoard, false);



            Action<double, Action> Times =
                (m, h) => (Width * Height * m).Times(h);

            Action<double, Func<BitmapAsset>> AddDoodads =
                (m, GetImage) => Times(m, () => GetImage().AttachTo(bg).SetCenteredPosition(Width.Random(), Height.Random()));

            AddDoodads(0.0001, () => Assets.grass1.ToBitmapAsset());
            AddDoodads(0.00005, () => Assets.bump2.ToBitmapAsset());

            this.music = Assets.snd_world.ToSoundAsset().play(0, 999, new SoundTransform(0.5));

            Func<Animation> AddCactus = () =>
                new Animation(null, Assets.img_cactus)
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

            turret = new Animation(Assets.img_turret1_gunfire_180, Assets.img_turret1_gunfire_180_frames);

            turret.x = (Width - turret.width) * 0.9;
            turret.y = (Height - turret.height) / 2;

            turret.AttachTo(GetWarzone());

            var channel1 = default(SoundChannel);

            var f = new[] { new GlowFilter() };




            var list = new List<Actor>();
            var bullets = 0;
            var runaways = 0;
            var score = 0;

            Action UpdateScoreBoard =
                delegate
                {
                    if (!CanFire) return;

                    ScoreBoard.text =
                        new
                        {
                            bullets,
                            runaways,
                            gore = (100 * (double)list.Count(i => !i.IsAlive) / (double)list.Count()).Round() + "%",
                            score,
                        }.ToString();
                };

            Action<MouseEvent> DoGunFire =
                e =>
                {
                    bullets--;

                    UpdateScoreBoard();


                    foreach (var s in
                   from ss in list
                   where ss.IsAlive
                   where new Point { x = ss.x - e.stageX, y = ss.y - e.stageY}.length < 32
                   select ss)
                        s.AddDamage(8 + 12.Random());
                };

            var CurrentTarget = default(MouseEvent);
            var CurrentTargetTimer = default(Timer);


            this.mouseDown +=
                e =>
                {
                    if (!CanFire) return;

                    if (channel1 != null)
                        channel1.stop();

                    channel1 = Assets.gunfire.ToSoundAsset().play(0, 999);

                    turret.filters = f;

                    turret.AnimationEnabled = true;



                    CurrentTarget = e;
                    CurrentTargetTimer =
                        (1000 / 10).AtInterval(
                            delegate
                            {
                                if (!turret.AnimationEnabled)
                                {
                                    CurrentTargetTimer.stop();
                                    channel1.stop();
                                    turret.filters = null;
                                    return;
                                }

                                DoGunFire(CurrentTarget);
                            }
                        );

                };
            HoldFireOnMouseUp();



            this.mouseMove +=
                e =>
                {
                    Aim.x = e.stageX;
                    Aim.y = e.stageY;
                    CurrentTarget = e;
                };


            if (stage == null)
            {

                this.addedToStage +=
                    delegate
                    {
                        stage.scaleMode = StageScaleMode.NO_BORDER;
                    };
            }
            else
                stage.scaleMode = StageScaleMode.NO_BORDER;

            Mouse.hide();


            Func<double> GetEntryPointY = () => (Height * 0.8).Random() + Height * 0.1;



            (1500).AtInterval(
                delegate
                {

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
                                    score += a.ScoreValue;
                                    UpdateScoreBoard();
                                };
                            a.AttachTo(GetWarzone()).AddTo(list);
                            a.PlayHelloSound();

                            return a;
                        };

                    // new actors if we got less 10 
                    if (list.Where(i => i.IsAlive).Count() < 8)
                    {
                        if (0.2.ByChance())
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


                            var Minions = new List<Actor>();

                            boss.Die +=
                                delegate
                                {
                                    // make the minions slower when boss dies
                                    Minions.ForEach(i => i.speed /= 2);

                                };

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
                                AttachRules(CreateMinionByArc((Math.PI * 2) * 0.15)).AddTo(Minions);
                                AttachRules(CreateMinionByArc((Math.PI * 2) * 0.85)).AddTo(Minions);
                            }
                            else if (0.3.ByChance())
                            {
                                // boss with 3 minions
                                AttachRules(CreateMinionByArc((Math.PI * 2) * 0.20)).AddTo(Minions);
                                AttachRules(CreateMinionByArc((Math.PI * 2) * 0)).AddTo(Minions);
                                AttachRules(CreateMinionByArc((Math.PI * 2) * 0.80)).AddTo(Minions);
                            }
                            else if (0.3.ByChance())
                            {
                                AttachRules(CreateMinionByArc((Math.PI * 2) * 0.15)).AddTo(Minions);
                                AttachRules(CreateMinionByArc((Math.PI * 2) * 0.25)).AddTo(Minions);
                                AttachRules(CreateMinionByArc((Math.PI * 2) * 0.85)).AddTo(Minions);
                                AttachRules(CreateMinionByArc((Math.PI * 2) * 0.75)).AddTo(Minions);
                            }
                            else
                            {
                                AttachRules(CreateMinionWarriorByArc((Math.PI * 2) * 0.3)).AddTo(Minions);
                                AttachRules(CreateMinionByArc((Math.PI * 2) * 0.15)).AddTo(Minions);
                                AttachRules(CreateMinionWarriorByArc((Math.PI * 2) * 0.0)).AddTo(Minions);
                                AttachRules(CreateMinionByArc((Math.PI * 2) * 0.85)).AddTo(Minions);
                                AttachRules(CreateMinionWarriorByArc((Math.PI * 2) * 0.7)).AddTo(Minions);
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
                            if (0.3.ByChance())
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
                }
            );


            (1000 / 24).AtInterval(
                delegate
                {
                    Aim.rotation += 1;

                    foreach (var s in
                           from ss in list
                           where ss.IsAlive
                           select ss)
                        s.x += s.speed;
                }
            );

            // lets create a hyperlink
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

            ScoreBoard.AttachTo(this);

            BlurWarzoneOnHover(powered_by_jsc, true);

            40000.AtInterval(
                delegate
                {
                    30.Random().ToInt32().AtDelay(
                        delegate
                        {
                            Assets.snd_bird2.ToSoundAsset().play();
                        }
                    );
                }
            );
            

        }

        //[Script(IsDebugCode = true)]
        private void HoldFireOnMouseUp()
        {

            this.mouseUp +=
                e =>
                {
                    turret.AnimationEnabled = false;
                };
        }

        public Actor[] KnownActors =
            new Actor []
            {
                new Sheep(),
                new Warrior(),
                new BossWarrior()
            };
    }

    [Script]
    class Sheep : Actor
    {

        static Bitmap[] frames
        {
            get
            {
                return new BitmapAsset[]
                {
                    Assets.sheep1,
                    Assets.sheep2,
                    Assets.sheep3,
                    Assets.sheep4
                };
            }
        }

        public Sheep()
            : base(frames, Assets.sheep_corpse, Assets.sheep_blood, Assets.snd_sheep)
        {
            ActorName = "Sheep";
        }
    }

    [Script]
    class BossWarrior : Warrior
    {

        public BossWarrior()
        {
            ActorName = "BossWarrior";
            ScoreValue = 4;
            Description = "Respawns once";

            filters = new[] { new GlowFilter((uint)new Random().Next()) };

            PlayHelloSound = () => Assets.snd_ghoullaugh.ToSoundAsset().play();
        }

    }

    [Script]
    class Warrior : Actor
    {
        static Bitmap[] frames
        {
            get
            {
                return new Bitmap[]
                {
                    Assets.man2_horns1,
                    Assets.man2_horns2,
                    Assets.man2_horns3,
                    Assets.man2_horns4,
                    Assets.man2_horns5,
                    Assets.man2_horns6,
                    Assets.man2_horns7,
                    Assets.man2_horns8,
                    Assets.man2_horns9,
                };
            }
        }
        public Warrior()
            : base(frames, Assets.man2_horns_dead1, Assets.man2_horns_dead2, Assets.snd_man2)
        {
            ActorName = "Warrior";
        }
    }



 
}
