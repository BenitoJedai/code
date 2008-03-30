using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.mx.core;
using System;
using System.Linq;
using System.Collections.Generic;



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
        }

        [Script]
        public class MineButton : Sprite
        {
            public const int Width = 16;
            public const int Height = 16;

            private readonly SoundAsset snd_flag = Assets.snd_flag.ToSoundAsset();
            private readonly SoundAsset snd_click = Assets.click.ToSoundAsset();
            private readonly SoundAsset snd_explosion = Assets.explosion.ToSoundAsset();

            private readonly BitmapAsset img_flag = Assets.flag.ToBitmapAsset();
            private readonly BitmapAsset img_button = Assets.button.ToBitmapAsset();
            private readonly BitmapAsset img_empty = Assets.empty.ToBitmapAsset();
            private readonly BitmapAsset img_mine = Assets.mine.ToBitmapAsset();
            private readonly BitmapAsset img_notmine = Assets.notmine.ToBitmapAsset();
            private readonly BitmapAsset img_mine_found = Assets.mine_found.ToBitmapAsset();

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


            public List<MineButton> Others;

            public MineButton()
            {
                this.Enabled = true;

                // http://simplythebest.net/sounds/index.html
                // http://www.pacdv.com/sounds/interface_sounds.html

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
                            snd_click.play();

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
                                Reveal();
                            }

                            this.Enabled = false;
                        }
                    };

                Update();
            }

            private void Update()
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

            private void Reveal()
            {
                if (!Enabled)
                    return;

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
                }
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

        public const int FieldXCount = 12;
        public const int FieldYCount = 12;

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

            foreach (var v in a)
            {
                v.IsMined = new Random().NextDouble() < (10 / a.Count);

                addChild(v);
            }

        }
    }

}
