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

        public FlashTowerDefense()
        {

            var bg = new Sprite { x = 0, y = 0 };

            //bg.graphics.beginFill(0xffffff);
            //bg.graphics.beginFill(0x808080);
            //bg.graphics.drawRect(0, 0, Width / 2, Height);



            var warzone = new Sprite { x = 0, y = 0 };

            warzone.graphics.beginFill(ColorWhite);
            warzone.graphics.drawRect(-OffscreenMargin, -OffscreenMargin, Width + 2 * OffscreenMargin, Height + 2 * OffscreenMargin);


            Func<DisplayObjectContainer> GetWarzone = () => warzone;

            bg.AttachTo(GetWarzone());
            warzone.AttachTo(this);

            #region create aim
            var aim = new Shape();

            aim.graphics.lineStyle(4, 0, 1);

            aim.graphics.moveTo(-8, 0);
            aim.graphics.lineTo(8, 0);

            aim.graphics.moveTo(0, -8);
            aim.graphics.lineTo(0, 8);

            aim.filters = new[] { new DropShadowFilter() };
            aim.AttachTo(GetWarzone());
            #endregion


            #region BlurWarzoneOnHover
            Action<TextField, bool> BlurWarzoneOnHover =
                (txt, HideAim) =>
                {

                    txt.mouseOver +=

                        delegate
                        {
                            //warzone.alpha = 0.8;
                            warzone.filters = new[] { new BlurFilter() };
                            //powered_by_jsc.htmlText = "<u><a href='http://jsc.sf.net'>powered by <b>jsc</b></a></u>";
                            txt.textColor = ColorBlue;
                            txt.filters = null;

                            if (HideAim)
                                aim.visible = false;
                        };

                    txt.mouseOut +=
                        delegate
                        {
                            //powered_by_jsc.htmlText = "<a href='http://jsc.sf.net'>powered by <b>jsc</b></a>";
                            txt.filters = new[] { new BlurFilter() };
                            txt.textColor = ColorBlack;
                            //warzone.alpha = 1;
                            warzone.filters = null;

                            if (HideAim)
                                aim.visible = true;
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

            var music = Assets.world.ToSoundAsset().play(0, 999);

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
                   where ss.hitTestPoint(e.stageX, e.stageY)
                   select ss)
                        s.AddDamage(8 + 12.Random());
                };

            var CurrentTarget = default(MouseEvent);
            var CurrentTargetTimer = default(Timer);


            this.mouseDown +=
                e =>
                {

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
                    aim.x = e.stageX;
                    aim.y = e.stageY;
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

                                        runaways++;

                                        ScoreBoard.textColor = ColorRed;
                                        UpdateScoreBoard();

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
                    aim.rotation += 1;

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

            ScoreBoard.AttachTo(this);

            powered_by_jsc.y = Height - powered_by_jsc.height - 32;

            BlurWarzoneOnHover(powered_by_jsc, true);
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

        }
    }

    [Script]
    class BossWarrior : Warrior
    {

        public BossWarrior()
        {
            ScoreValue = 4;

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

        }
    }

    [Script]
    class Animation : Sprite
    {
        readonly BitmapAsset StillFrame;
        readonly BitmapAsset[] AnimatedFrames;

        Timer _Timer;

        public int FrameRate = (1000 / 15);

        public bool AnimationEnabled
        {
            get
            {
                return _Timer != null;
            }

            set
            {
                if (_Timer != null)
                {
                    _Timer.stop();
                    _Timer = null;
                }

                Clear();

                if (value)
                {
                    _Timer = FrameRate.AtInterval(
                        delegate
                        {
                            Clear();

                            ShowCurrentFrame();
                        }
                    );

                }

                ShowCurrentFrame();

            }
        }

        private void ShowCurrentFrame()
        {
            if (AnimationEnabled)
                AnimatedFrames[_Timer.currentCount % AnimatedFrames.Length].AttachTo(this).MoveToCenter();
            else
                if (this.StillFrame != null)
                    this.StillFrame.AttachTo(this).MoveToCenter();
        }

        void Clear()
        {
            if (this.StillFrame != null)
                this.StillFrame.Orphanize();

            foreach (var v in AnimatedFrames)
            {
                v.Orphanize();
            }
        }


        public Animation(Class StillFrame, params Class[] AnimatedFrames)
        {
            this.StillFrame = StillFrame;

            this.AnimatedFrames = AnimatedFrames.Select(i => (BitmapAsset)i).ToArray();

            ShowCurrentFrame();
        }
    }


    [Script]
    class Actor : Sprite
    {
        public int ScoreValue = 1;

        public bool IsAlive = true;

        public event Action MakeSound;
        public event Action Die;

        public event Action CorpseGone;
        public event Action CorpseAndBloodGone;

        public event Action Moved;

        public double health = 100;
        public double speed = 0.5;

        public bool IsBleeding;

        public Action PlayHelloSound = delegate { };


        public void AddDamage(double e)
        {
            health -= e;

            if (health < 0)
                Die();
        }

        readonly List<Bitmap> Footsteps = new List<Bitmap>();

        public Actor(Bitmap[] frames, Bitmap corpse, Bitmap blood, Sound death)
        {
            MakeSound = death.ToAction();




            Die = delegate
            {
                IsAlive = false;
                MakeSound();

                foreach (var v in frames)
                    v.Orphanize();

                corpse.x = -corpse.width / 2;
                corpse.y = -corpse.height / 2;
                corpse.AttachTo(this);

                (10000 + 10000.Random().ToInt32()).AtDelay(
                    delegate
                    {
                        corpse.Orphanize();


                        blood.x = -blood.width / 2;
                        blood.y = -blood.height / 2;
                        blood.AttachTo(this);


                        (20000 + 10000.Random().ToInt32()).AtDelay(
                           delegate
                           {
                               blood.Orphanize();

                               if (CorpseAndBloodGone != null)
                                   CorpseAndBloodGone();

                           }
                       );

                        if (CorpseGone != null)
                            CorpseGone();
                    }
                );
            };

            //this.Moved +=
            //    delegate
            //    {



            (1000 / 15).AtInterval(
                 t =>
                 {
                     if (!IsAlive)
                     {
                         t.stop();
                         return;
                     }

                     for (int i = 0; i < frames.Length; i++)
                     {
                         var v = frames[i];

                         if (t.currentCount % frames.Length == i)
                         {
                             v.x = -v.width / 2;
                             v.y = -v.height / 2;
                             v.AttachTo(this);

                             if (this.Moved != null)
                                 this.Moved();

                             UpdateFootsteps();
                         }
                         else
                             v.Orphanize();
                     }
                 }
             );
        }

        private void UpdateFootsteps()
        {
            if (Footsteps.Count == 0)
            {
                CreateFootsteps();
            }
            else
            {
                var o = Footsteps.Last();

                if ((o.x + o.width) < x)
                {
                    CreateFootsteps();
                }
            }
        }

        private void CreateFootsteps()
        {
            if (this.parent == null)
                return;

            var n = Assets.footsteps.ToBitmapAsset();

            n.x = x - n.width / 2;
            n.y = y - n.height / 2;

            if (IsBleeding)
                n.filters = new[] { new GlowFilter(0xff0000) };

            n.AttachTo(this.parent).AddTo(Footsteps);

            (500).AtInterval(
                t =>
                {
                    n.alpha -= 0.1;

                    if (n.alpha < 0.1)
                    {
                        n.RemoveFrom(Footsteps).Orphanize();
                        t.stop();
                    }
                }
            );
        }


    }

}
