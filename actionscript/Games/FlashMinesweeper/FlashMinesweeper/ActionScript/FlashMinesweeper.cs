using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.mx.core;
using System;
using System.Linq;
using System.Collections.Generic;
using ScriptCoreLib.ActionScript.flash.utils;



namespace FlashMinesweeper.ActionScript
{
    /// <summary>
    /// testing...
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    [SWF(backgroundColor = 0xc0c0c0,
        width = FlashMinesweeper.FieldXCount * FlashMinesweeper.MineButton.Width,
        height = FlashMinesweeper.FieldYCount * FlashMinesweeper.MineButton.Height
        )]
    public class FlashMinesweeper : Sprite
    {
        // todo:
        // http://www.kirupa.com/forum/showthread.php?t=261577
        // http://groups.google.com/group/youtube-api-basics/browse_thread/thread/89ff378fc44985f0/6b63c2e46159640f?lnk=gst&q=flash+loadClip&rnum=1
        // snd:
        // http://www.a1sounddownload.com/freesoundsamples.htm
        // http://simplythebest.net/sounds/index.html
        // http://www.pacdv.com/sounds/interface_sounds.html


        public const int FieldXCount = 10;
        public const int FieldYCount = 10;

        [Script]
        static public class Assets
        {
            public const string Path = "/assets/FlashMinesweeper";

            [Embed(source = Path + "/flag.png")]
            static public readonly Class flag;

            [Embed(source = Path + "/button.png")]
            static public readonly Class button;

            [Embed(source = Path + "/1.png")]
            static public readonly Class _1;

            [Embed(source = Path + "/2.png")]
            static public readonly Class _2;

            [Embed(source = Path + "/3.png")]
            static public readonly Class _3;

            [Embed(source = Path + "/4.png")]
            static public readonly Class _4;

            [Embed(source = Path + "/5.png")]
            static public readonly Class _5;

            [Embed(source = Path + "/6.png")]
            static public readonly Class _6;

            [Embed(source = Path + "/7.png")]
            static public readonly Class _7;

            [Embed(source = Path + "/8.png")]
            static public readonly Class _8;

            [Embed(source = Path + "/empty.png")]
            static public readonly Class empty;

            [Embed(source = Path + "/mine.png")]
            static public readonly Class mine;

            [Embed(source = Path + "/notmine.png")]
            static public readonly Class notmine;


            [Embed(source = Path + "/mine_found.png")]
            static public readonly Class mine_found;


            [Embed(source = Path + "/click.mp3")]
            static public readonly Class click;

            [Embed(source = Path + "/explosion.mp3")]
            static public readonly Class explosion;

            [Embed(source = Path + "/flag.mp3")]
            static public readonly Class snd_flag;


            [Embed(source = Path + "/reveal.mp3")]
            static public readonly Class snd_reveal;

            [Embed(source = Path + "/tick.mp3")]
            static public readonly Class snd_tick;

            [Embed(source = Path + "/applause.mp3")]
            static public readonly Class snd_applause;

            [Embed(source = Path + "/buzzer.mp3")]
            static public readonly Class snd_buzzer;
        }

        public readonly SoundAsset snd_applause = Assets.snd_applause.ToSoundAsset();
        public readonly SoundAsset snd_reveal = Assets.snd_reveal.ToSoundAsset();
        public readonly SoundAsset snd_tick = Assets.snd_tick.ToSoundAsset();

        [Script]
        public class MineButton : Sprite
        {
            public const int Width = 16;
            public const int Height = 16;

            public readonly SoundAsset snd_buzzer = Assets.snd_buzzer.ToSoundAsset();
            public readonly SoundAsset snd_reveal = Assets.snd_reveal.ToSoundAsset();
            private readonly SoundAsset snd_flag = Assets.snd_flag.ToSoundAsset();
            private readonly SoundAsset snd_click = Assets.click.ToSoundAsset();
            private readonly SoundAsset snd_explosion = Assets.explosion.ToSoundAsset();

            private readonly BitmapAsset img_flag = Assets.flag.ToBitmapAsset();
            private readonly BitmapAsset img_button = Assets.button.ToBitmapAsset();
            private readonly BitmapAsset img_empty = Assets.empty.ToBitmapAsset();
            public readonly BitmapAsset img_mine = Assets.mine.ToBitmapAsset();
            private readonly BitmapAsset img_notmine = Assets.notmine.ToBitmapAsset();
            public readonly BitmapAsset img_mine_found = Assets.mine_found.ToBitmapAsset();

            private readonly BitmapAsset[] img_numbers =
                new[]
                {
                    Assets.empty.ToBitmapAsset(),
                    Assets._1.ToBitmapAsset(),
                    Assets._2.ToBitmapAsset(),
                    Assets._3.ToBitmapAsset(),
                    Assets._4.ToBitmapAsset(),
                    Assets._5.ToBitmapAsset(),
                    Assets._6.ToBitmapAsset(),
                    Assets._7.ToBitmapAsset(),
                    Assets._8.ToBitmapAsset(),
                };

            public bool Enabled { get; set; }
            public bool IsMined { get; set; }
            public bool IsFlag { get; set; }

            public event Action OnBang;
            public event Action OnComplete;

            public List<MineButton> Others;

            public MineButton()
            {
                this.Enabled = true;


                this.mouseDown +=
                    delegate
                    {
                        if (!Enabled)
                            return;

                        this.img = img_empty;
                    };

                this.mouseOut +=
                    delegate
                    {
                        if (!Enabled)
                            return;

                        Update();
                    };

                this.mouseUp +=
                    e =>
                    {
                        if (!Enabled)
                            return;


                        if (e.altKey || e.ctrlKey || e.shiftKey)
                        {
                            snd_flag.play();

                            this.IsFlag = !this.IsFlag;

                            Update();
                        }
                        else
                        {
                            if (IsFlag)
                            {
                                snd_buzzer.play();
                                return;
                            }

                            if (this.IsMined)
                            {
                                this.img = img_mine_found;

                                snd_explosion.play();

                                RevealHiddenMines();

                                if (OnBang != null)
                                    OnBang();
                            }
                            else
                            {
                                if (Reveal())
                                    snd_reveal.play();
                                else
                                    snd_click.play();
                            }

                            this.Enabled = false;


                        }

                        CheckComplete();
                    };

                Update();
            }

            public bool HasInvalidStateForCompletion
            {
                get
                {
                    if (IsMined && !IsFlag)
                        return true;

                    if (!IsMined && IsFlag)
                        return true;


                    return false;
                }
            }

            private void CheckComplete()
            {
                if (Others.Any(p => p.HasInvalidStateForCompletion))
                    return;

               

                if (OnComplete != null)
                    OnComplete();
            }

            public void Update()
            {
                if (IsFlag)
                    this.img = img_flag;
                else
                    this.img = img_button;
            }

            private void RevealHiddenMines()
            {
                foreach (var b in Others)
                {
                    if (b == this)
                        continue;

                    b.Enabled = false;

                    if (b.IsMined)
                    {
                        if (b.IsFlag)
                        {
                            // ok
                        }
                        else
                        {
                            b.img = b.img_mine;
                        }
                    }
                    else
                    {
                        if (b.IsFlag)
                        {
                            b.img = b.img_notmine;
                        }
                    }
                }
            }

            private bool Reveal()
            {
                if (!Enabled)
                    return false;

                var NearbyMines = from z in Others
                                  where IsNearTo(z)
                                  where z.IsMined
                                  select z;

                var NearbyMinesCount = NearbyMines.Count();

                this.img = img_numbers[NearbyMinesCount];
                this.Enabled = false;

                if (NearbyMinesCount == 0)
                {
                    foreach (var v in from z in Others where IsNearTo(z) select z)
                    {
                        v.Reveal();
                    }

                    return true;
                }

                return false;
            }


            public int FieldX;
            public int FieldY;

            public bool IsAtOffset(MineButton btn, int x, int y)
            {
                if (btn.FieldX + x != FieldX)
                    return false;

                return btn.FieldY + y == FieldY;
            }

            public bool IsNearTo(MineButton btn)
            {
                /*
            
            as3 compiler chokes on this:
                 
            if (!((((btn.FieldX - 1) != this.FieldX)) ? true : !((btn.FieldY - 1) == this.FieldY)))
            {
                return true;
            }
                 
                 */

                if (IsAtOffset(btn, -1, -1)) return true;
                if (IsAtOffset(btn, 0, -1)) return true;
                if (IsAtOffset(btn, 1, -1)) return true;
                if (IsAtOffset(btn, 1, 0)) return true;

                if (IsAtOffset(btn, 1, 1)) return true;
                if (IsAtOffset(btn, 0, 1)) return true;
                if (IsAtOffset(btn, -1, 1)) return true;
                if (IsAtOffset(btn, -1, 0)) return true;

                return false;
            }



            private Bitmap _img;

            public Bitmap img
            {
                set
                {
                    if (_img != null)
                    {
                        this.removeChild(_img);
                    }

                    _img = value;

                    if (_img != null)
                    {
                        this.addChild(_img);
                    }
                }
            }
        }

        public FlashMinesweeper()
        {
            var a = new List<MineButton>();

            for (int x = 0; x < FieldXCount; x++)
                for (int y = 0; y < FieldYCount; y++)
                {
                    a.Add(
                        new MineButton
                        {
                            x = x * MineButton.Width,
                            y = y * MineButton.Height,

                            FieldX = x,
                            FieldY = y,
                            Others = a
                        }
                    );
                }

            Action<int, Action> Delay =
                (time, h) =>
                {
                    var t = new Timer(time, 1);

                    t.timer +=
                        delegate
                        {
                            h();
                        };

                    t.start();
                };

            Action<int, Action[]> DelayArray =
                (time, h) =>
                {
                    var i = 0;

                    var Next = default(Action);


                    Next = delegate
                    {
                        if (i < h.Length)
                            Delay(time,
                                delegate
                                {
                                    h[i]();
                                    i++;
                                    Next();
                                }
                            );
                    };

                    Next();
                };

            Action Reset =
                delegate
                {
                    foreach (var v in a)
                    {
                        v.IsFlag = false;
                        v.Enabled = true;
                        v.IsMined = new Random().NextDouble() < (10 / a.Count);
                        v.Update();
                    }
                    snd_reveal.play();

                };

            foreach (var v in a)
            {
                var z = v;

                v.IsMined = new Random().NextDouble() < (10 / a.Count);
                v.OnComplete +=
                    delegate
                    {
                        foreach (var i in a)
                            i.Enabled = false;

                        snd_applause.play();

                        Delay(4000, Reset);
                    };

                v.OnBang +=
                    delegate
                    {
                        Delay(
                            3000,
                            delegate
                            {
                                DelayArray(1000,
                                    new Action[] {
                                    delegate
                                    {
                                        snd_tick.play();
                                        z.img = z.img_mine;
                                    },
                                    delegate
                                    {
                                        snd_tick.play();
                                        z.img = z.img_mine_found;

                                    },
                                    delegate
                                    {

                                        Reset();
                                    }
                                });
                            }
                        );


                    };

                addChild(v);
            }

            Reset();
        }
    }

}
