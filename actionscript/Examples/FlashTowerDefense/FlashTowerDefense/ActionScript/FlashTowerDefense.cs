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
    public sealed class FlashTowerDefense : Sprite
    {
        public const int Width = 640;
        public const int Height = 480;

        public const uint ColorBlack = 0x000000;
        public const uint ColorWhite = 0xffffff;
        public const uint ColorBlue = 0x0000ff;
        public const uint ColorBlueDark = 0x000080;
        public const uint ColorBlueLight = 0x9090ff;

        const int OffscreenMargin = 32;

        public bool Gunfire = false;

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

            var t = new TextField
            {
                x = 4,
                y = 4,
                width = 300,
                height = 20,
                mouseEnabled = false,

                filters = new[] { new DropShadowFilter() }
            };

            t.AttachTo(this);

            Action<double, Action> Times =
                (m, h) => (Width * Height * m).Times(h);

            Action<double, Func<BitmapAsset>> AddDoodads =
                (m, GetImage) => Times(m, () => GetImage().AttachTo(bg).SetCenteredPosition(Width.Random(), Height.Random()));

            AddDoodads(0.0001, () => Assets.grass1.ToBitmapAsset());
            AddDoodads(0.00005, () => Assets.bump2.ToBitmapAsset());

            var music = Assets.world.ToSoundAsset().play(0, 999);


            var turret = Assets.turret1_default.ToBitmapAsset();

            turret.x = (Width - turret.width) * 0.7;
            turret.y = (Height - turret.height) / 2;

            turret.AttachTo(GetWarzone());

            var channel1 = default(SoundChannel);

            var f = new[] { new GlowFilter() };




            var list = new List<Actor>();
            var bullets = 0;

            Action<MouseEvent> DoGunFire =
                e =>
                {
                    bullets--;

                    t.text =
                        new
                        {
                            x = e.stageX.Round(),
                            y = e.stageY.Round(),
                            bullets = bullets,
                            score = list.Where(i => !i.IsAlive).Count(),
                            actors = list.Count
                        }.ToString();

                    foreach (var s in
                   from ss in list
                   where ss.IsAlive
                   where ss.hitTestPoint(e.stageX, e.stageY)
                   select ss)
                        s.AddDamage(8 + 12.Random());
                };

            var CurrentTarget = default(MouseEvent);
            var CurrentTargetTimer = default(Timer);

            var aim = new Shape();

            aim.graphics.lineStyle(4, 0, 1);

            aim.graphics.moveTo(-8, 0);
            aim.graphics.lineTo(8, 0);

            aim.graphics.moveTo(0, -8);
            aim.graphics.lineTo(0, 8);

            aim.filters = new[] { new DropShadowFilter() };
            aim.AttachTo(GetWarzone());

            this.mouseDown +=
                e =>
                {

                    if (channel1 != null)
                        channel1.stop();

                    channel1 = Assets.gunfire.ToSoundAsset().play(0, 999);

                    turret.filters = f;
                    Gunfire = true;

                    CurrentTarget = e;
                    CurrentTargetTimer =
                        (1000 / 10).AtInterval(
                            delegate
                            {
                                if (!Gunfire)
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


            (1500).AtInterval(
                delegate
                {
               
                    Action<Actor> AttachRules =
                        a =>
                        {
                            a.CorpseAndBloodGone += () => list.Remove(a);
                            a.Moved +=
                                delegate
                                {
                                    if (a.x > (Width + OffscreenMargin))
                                        a.RemoveFrom(list).Dipsose();
                                };

                            a.AttachTo(GetWarzone()).AddTo(list);
                        };

                    // new actors if we got less 10 
                    if (list.Where(i => i.IsAlive).Count() < 10)
                    {
                        AttachRules(
                            new Sheep
                            {
                                x = -OffscreenMargin,
                                y = Height.Random(),
                                speed = 0.5 + 2.Random()
                            }
                        );

                        AttachRules(
                            new Warrior
                            {
                                x = -OffscreenMargin,
                                y = Height.Random(),
                                speed = 1 + 2.Random()
                            }
                        );
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

            powered_by_jsc.y = Height - powered_by_jsc.height - 32;
            
            powered_by_jsc.mouseOver +=

                delegate
                {
                    //warzone.alpha = 0.8;
                    warzone.filters = new[] { new BlurFilter() };
                    //powered_by_jsc.htmlText = "<u><a href='http://jsc.sf.net'>powered by <b>jsc</b></a></u>";
                    powered_by_jsc.textColor = ColorBlue;
                    powered_by_jsc.filters = null;

                    aim.visible = false;
                };

            powered_by_jsc.mouseOut +=
                delegate
                {
                    //powered_by_jsc.htmlText = "<a href='http://jsc.sf.net'>powered by <b>jsc</b></a>";
                    powered_by_jsc.filters = new[] { new BlurFilter() };
                    powered_by_jsc.textColor = ColorBlack;
                    //warzone.alpha = 1;
                    warzone.filters = null;
                    
                    aim.visible = true;
                };
        }

        //[Script(IsDebugCode = true)]
        private void HoldFireOnMouseUp()
        {

            this.mouseUp +=
                e =>
                {
                    Gunfire = false;
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
                return new[]
                {
                    Assets.sheep1.ToBitmapAsset(),
                    Assets.sheep2.ToBitmapAsset(),
                    Assets.sheep3.ToBitmapAsset(),
                    Assets.sheep4.ToBitmapAsset()
                };
            }
        }
        public Sheep()
            : base(frames, Assets.sheep_corpse.ToBitmapAsset(), Assets.sheep_blood.ToBitmapAsset(), Assets.snd_sheep.ToSoundAsset())
        {

        }
    }

    [Script]
    class Warrior : Actor
    {
        static Bitmap[] frames
        {
            get
            {
                return new[]
                {
                    Assets.man2_horns1.ToBitmapAsset(),
                    Assets.man2_horns2.ToBitmapAsset(),
                    Assets.man2_horns3.ToBitmapAsset(),
                    Assets.man2_horns4.ToBitmapAsset(),
                    Assets.man2_horns5.ToBitmapAsset(),
                    Assets.man2_horns6.ToBitmapAsset(),
                    Assets.man2_horns7.ToBitmapAsset(),
                    Assets.man2_horns8.ToBitmapAsset(),
                    Assets.man2_horns9.ToBitmapAsset(),
                };
            }
        }
        public Warrior()
            : base(frames, Assets.man2_horns_dead1.ToBitmapAsset(), Assets.man2_horns_dead2.ToBitmapAsset(), Assets.snd_man2.ToSoundAsset())
        {

        }
    }


    [Script]
    class Actor : Sprite
    {
        public bool IsAlive = true;

        public readonly Action MakeSound;
        public readonly Action Die;

        public event Action CorpseAndBloodGone;

        public event Action Moved;

        public double health = 100;
        public double speed = 0.5;

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
                    v.Dipsose();

                corpse.x = -corpse.width / 2;
                corpse.y = -corpse.height / 2;
                corpse.AttachTo(this);

                (10000 + 10000.Random().ToInt32()).AtDelay(
                    delegate
                    {
                        corpse.Dipsose();

                        blood.x = -blood.width / 2;
                        blood.y = -blood.height / 2;
                        blood.AttachTo(this);


                        (20000 + 10000.Random().ToInt32()).AtDelay(
                           delegate
                           {
                               blood.Dipsose();

                               if (CorpseAndBloodGone != null)
                                   CorpseAndBloodGone();

                           }
                       );
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
                             v.Dipsose();
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

            n.AttachTo(this.parent).AddTo(Footsteps);

            (500).AtInterval(
                t =>
                {
                    n.alpha -= 0.1;

                    if (n.alpha < 0.1)
                    {
                        n.RemoveFrom(Footsteps).Dipsose();
                        t.stop();
                    }
                }
            );
        }


    }

}
