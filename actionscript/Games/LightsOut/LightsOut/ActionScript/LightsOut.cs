using System;
using System.Linq;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.Shared.Lambda;


namespace LightsOut.ActionScript
{
    /// <summary>
    /// testing...
    /// </summary>
    [Script, ScriptApplicationEntryPoint(Width = ControlWidth, Height = ControlHeight)]
    [SWF(width = ControlWidth, height = ControlHeight)]
    public class LightsOut : Sprite
    {
        public const int ControlWidth = FieldX * FieldSize + FieldSize / 2;
        public const int ControlHeight = FieldY * FieldSize + FieldSize / 2;

        // background is not big enough
        const int FieldX = 5;
        const int FieldY = 5;

        const int FieldSize = 64;

        public LightsOut()
        {
            // http://en.wikipedia.org/wiki/Lights_Out_(game)

            AnimateBackground();

            // http://www.flashkit.com/soundfx/Interfaces/

            CreateField(FieldX, FieldY);


            // randomize



            Reset();
        }

        public void Reset()
        {
            Values.ForEach(i => i.Value = false);

            4.Times(() => UserClicks.Random()());
        }

        Array2D<Action> UserClicks;

        Array2D<Property<bool>> Values;

        public event Action<int, int> NetworkClick;

        public void CheckForCompleteness(bool LocalPlayer)
        {
            if (Values.Any(i => i.Value))
                return;

            Reset();
        }

        private void CreateField(int w, int h)
        {
            UserClicks = new Array2D<Action>(w, h);
            Values = new Array2D<Property<bool>>(w, h);

            var a = new Array2D<Action>(w, h);


            var r = new Random();

            for (int x = 0; x < w; x++)
                for (int y = 0; y < h; y++)
                    new
                    {
                        x,
                        y,
                        off = Assets.vistaLogoOff.ToBitmapAsset(),
                        @on = Assets.vistaLogoOn.ToBitmapAsset(),
                        sprite = new Sprite
                        {
                            x = x * FieldSize + FieldSize / 4,
                            y = y * FieldSize + FieldSize / 4
                        }.AttachTo(this),
                        click = Assets.click.ToSoundAsset()
                    }.Aggregate(
                        btn =>
                        {

                            btn.sprite.alpha = 0.6;
                            btn.@on.visible = false;

                            btn.sprite.addChild(btn.off);
                            btn.sprite.addChild(btn.@on);

                            a[btn.x, btn.y] =
                                delegate
                                {
                                    btn.@on.visible = !btn.@on.visible;
                                    btn.@off.visible = !btn.@off.visible;
                                };

                            Values[btn.x, btn.y] =
                                new Property<bool>
                                {
                                    GetValue = () => btn.@on.visible,
                                    SetValue = 
                                        value =>
                                        {
                                            btn.@on.visible = value;
                                            btn.@off.visible = !value;
                                        }
                                };

                            Func<int, int, bool> ToggleDirect =
                                (ix, iy) =>
                                {
                                    var n = a[ix, iy];

                                    if (n == null)
                                        return false;

                                    n();

                                    return true;
                                };

                            var f = ToggleDirect.WithOffset(btn.x, btn.y);

                            Action UserClick =
                                delegate
                                {
                                    f(0, 0);
                                    f(1, 0);
                                    f(-1, 0);
                                    f(0, 1);
                                    f(0, -1);
                                };

                            UserClicks[btn.x, btn.y] = UserClick;


                            btn.sprite.click +=
                                delegate
                                {
                                    UserClick();

                                    
                                    btn.click.play();

                                    if (NetworkClick != null)
                                        NetworkClick(btn.x, btn.y);

                                    CheckForCompleteness(true);
                                };



                            btn.sprite.mouseOver += e => btn.sprite.alpha = 1;
                            btn.sprite.mouseOut += e => btn.sprite.alpha = 0.6;
                        }
                    );

        }

        private void AnimateBackground()
        {
            new
            {
                a = Assets.background.ToBitmapAsset().AttachTo(this),
                b = Assets.background.ToBitmapAsset().AttachTo(this)
            }.TimerLoop(1000 / 24,
                v =>
                {
                    v.a.x -= 1;

                    if (v.a.x < -v.a.width)
                        v.a.x = 0;

                    v.b.x = v.a.x + v.a.width;
                }
            ); //.Aggregate(stop => this.click += e => stop());
        }
    }

    [Script]
    public class Property<T>
    {
        public Func<T> GetValue;
        public Action<T> SetValue;

        public T Value
        {
            get
            {
                return GetValue();
            }
            set
            {
                SetValue(value);
            }
        }
    }
}
