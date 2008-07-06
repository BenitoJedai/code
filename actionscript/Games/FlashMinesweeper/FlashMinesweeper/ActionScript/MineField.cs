using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.mx.core;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.utils;

namespace FlashMinesweeper.ActionScript
{
    [Script]
    public class MineField : Sprite
    {
        //public readonly SoundAsset snd_applause = Assets.snd_applause.ToSoundAsset();
        public readonly SoundAsset snd_reveal = Assets.snd_reveal.ToSoundAsset();
        public readonly SoundAsset snd_tick = Assets.snd_tick.ToSoundAsset();

        [Script]
        public class MineButton : Sprite
        {
            public const int Width = 16;
            public const int Height = 16;

            public readonly SoundAsset snd_buzzer = Assets.snd_buzzer.ToSoundAsset();
            public readonly SoundAsset snd_reveal = Assets.snd_reveal.ToSoundAsset();
            public readonly SoundAsset snd_flag = Assets.snd_flag.ToSoundAsset();
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

            public event Action<bool> OnComplete;

            public List<MineButton> Others;

            public event Action IsFlagChanged;
            public event Action OnReveal;

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

                            if (IsFlagChanged != null)
                                IsFlagChanged();

                            Update();
                        }
                        else
                        {
                            if (IsFlag)
                            {
                                snd_buzzer.play();
                                return;
                            }

                            if (OnReveal != null)
                                OnReveal();


                            RevealOrExplode(true);


                        }

                        CheckComplete(true);
                    };

                Update();
            }

            public void RevealOrExplode()
            {
                RevealOrExplode(false);
            }

            public event Action<bool> OnBang;


            public void RevealOrExplode(bool LocalPlayer)
            {
                if (this.IsMined)
                {
                    this.img = img_mine_found;

                    snd_explosion.play();

                    RevealHiddenMines();

                    if (OnBang != null)
                        OnBang(LocalPlayer);

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

            public void CheckComplete()
            {
                CheckComplete(false);
            }

            private void CheckComplete(bool LocalPlayer)
            {
                if (Others.Any(p => p.HasInvalidStateForCompletion))
                    return;



                if (OnComplete != null)
                    OnComplete(LocalPlayer);
            }

            public void Update()
            {
                if (!Enabled)
                    return;

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

            public void RevealLocal()
            {
                var NearbyMines = from z in Others
                                  where IsNearTo(z)
                                  where z.IsMined
                                  select z;

                var NearbyMinesCount = NearbyMines.Count();

                this.img = img_numbers[NearbyMinesCount];
                this.Enabled = false;
            }

            public bool Reveal()
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
                get
                {
                    return _img;
                }
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

        //public MineField(int FieldXCount, int FieldYCount) : this(FieldXCount, FieldYCount, 0.3)
        //{
        //}

        public readonly MineButton[] Buttons;

        public event Action<int, bool> IsFlagChanged;
        public event Action<int> OnReveal;

        public MineField(int FieldXCount, int FieldYCount, double percentage)
        {


            var a = new List<MineButton>();

            var k = 0;

            for (int x = 0; x < FieldXCount; x++)
                for (int y = 0; y < FieldYCount; y++)
                {
                    var n =
                        new MineButton
                        {
                            x = x * MineButton.Width,
                            y = y * MineButton.Height,

                            FieldX = x,
                            FieldY = y,
                            Others = a

                        };

                    var j = k;

                    n.IsFlagChanged +=
                        delegate
                        {
                            if (IsFlagChanged != null)
                                IsFlagChanged(j, n.IsFlag);
                        };

                    n.OnReveal +=
                     delegate
                     {
                         if (OnReveal != null)
                             OnReveal(j);
                     };


                    a.Add(
                        n
                    );

                    k++;
                }

            Buttons = a.ToArray();

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

            Func<bool> RandomIsMined = () => new Random().NextDouble() < percentage;

            Action Reset =
                delegate
                {
                    foreach (var v in a)
                    {
                        v.IsFlag = false;
                        v.Enabled = true;
                        v.IsMined = RandomIsMined();
                        v.Update();
                    }
                    snd_reveal.play();

                };

            foreach (var v in a)
            {
                var z = v;

                v.OnComplete +=
                    LocalPlayer =>
                    {
                        foreach (var i in a)
                            i.Enabled = false;

                        if (OnComplete != null)
                            OnComplete(LocalPlayer);

                        DelayArray(1000,
                                  new Action[] {
                                    delegate
                                    {
                                        snd_tick.play();
                                        
                                    },
                                    delegate
                                    {
                                        snd_tick.play();
                                    },
                                    delegate
                                    {

                                         if (LocalPlayer)
                                        {
                                            Reset();

                                            if (GameResetByLocalPlayer != null)
                                                GameResetByLocalPlayer();
                                        }
                                    }
                                });
                    };

                v.OnBang +=
                    LocalPlayer =>
                    {
                        if (OnBang != null)
                            OnBang(LocalPlayer);

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

                                        if (z.img == z.img_mine)
                                            z.img = z.img_mine_found;

                                    },
                                    delegate
                                    {
                                        if (LocalPlayer)
                                        {
                                            Reset();

                                            if (GameResetByLocalPlayer != null)
                                                GameResetByLocalPlayer();
                                        }
                                    }
                                });
                            }
                        );


                    };

                addChild(v);
            }

            Reset();
        }

        public event Action<bool> OnBang;
        public event Action<bool> OnComplete;

        public event Action GameResetByLocalPlayer;
    }
}
