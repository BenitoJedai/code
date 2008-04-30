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


namespace FlashTowerDefense.ActionScript
{

    /// <summary>
    /// testing...
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    public class FlashTowerDefense : Sprite
    {
        public FlashTowerDefense()
        {

            var bg = new Sprite { x = 0, y = 0 };

            bg.graphics.beginFill(0xffffff);
            bg.graphics.drawRect(0, 0, stage.stageWidth, stage.stageHeight);

            bg.AttachTo(this);

            var t = new TextField
            {
                x = 4,
                y = 4,
                width = 300,
                height = 20,
                mouseEnabled = false,

                filters = new[] { new GlowFilter(0xffffff) }
            };

            t.AttachTo(this);

            Action<double, Action> Times =
                (m, h) => (stage.stageWidth * stage.stageHeight * m).Times(h);

            Action<double, Func<BitmapAsset>> AddDoodads =
                (m, GetImage) => Times(m, () => GetImage().AttachTo(bg).SetCenteredPosition(stage.stageWidth.Random(), stage.stageHeight.Random()));

            AddDoodads(0.0001, () => Assets.grass1.ToBitmapAsset());
            AddDoodads(0.00005, () => Assets.bump2.ToBitmapAsset());

            var music = Assets.world.ToSoundAsset().play(0, 999);


            var turret = Assets.turret1_default.ToBitmapAsset();

            turret.x = (stage.stageWidth - turret.width) * 0.7;
            turret.y = (stage.stageHeight - turret.height) / 2;

            turret.AttachTo(this);

            var channel1 = default(SoundChannel);

            var f = new[] { new GlowFilter() };

            var Gunfire = false;


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
                            score = list.Where(i => !i.IsAlive).Count()
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
            aim.AttachTo(this);

            this.mouseDown +=
                e =>
                {

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

            this.mouseUp +=
                e =>
                {
                    Gunfire = false;
                };

            stage.mouseLeave +=
                  e =>
                  {
                      Gunfire = false;
                  };

            this.mouseMove +=
                e =>
                {
                    aim.x = e.stageX;
                    aim.y = e.stageY;
                    CurrentTarget = e;
                };

            stage.scaleMode = StageScaleMode.NO_BORDER;
            Mouse.hide();


            (1500).AtInterval(
                delegate
                {
                    new Sheep
                    {
                        x = -100,
                        y = stage.stageHeight.Random(),
                        speed = 0.5 + 2.Random()
                    }.AttachTo(this).AddTo(list);

                    new Warrior
                    {
                        x = -100,
                        y = stage.stageHeight.Random(),
                        speed = 1 + 2.Random()
                    }.AttachTo(this).AddTo(list);
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
            : base(frames)
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
            : base(frames)
        {

        }
    }


    [Script]
    class Actor : Sprite
    {
        public bool IsAlive = true;

        public readonly Action MakeSound;
        public readonly Action Die;

        public double health = 100;
        public double speed = 0.5;

        public void AddDamage(double e)
        {
            health -= e;

            if (health < 0)
                Die();
        }

    

        public Actor(Bitmap[] frames)
        {
            MakeSound = Assets.snd_sheep.ToSoundAsset().ToAction();

       
            var corpse = Assets.sheep_corpse.ToBitmapAsset();
            var blood = Assets.sheep_blood.ToBitmapAsset();

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
                           }
                       );
                    }
                );
            };

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
                         }
                         else
                             v.Dipsose();
                     }
                 }
             );
        }


    }

}
