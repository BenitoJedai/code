using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.ActionScript.flash.utils;
using System;
using ScriptCoreLib.ActionScript.flash.events;
using System.Linq;


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

        [Script]
        public static class Assets
        {
            const string Path = "/assets/LightsOut";

            [Embed(source = Path + "/background.png")]
            public static Class background;

            [Embed(source = Path + "/vistaLogoOn.png")]
            public static Class vistaLogoOn;

            [Embed(source = Path + "/vistaLogoOff.png")]
            public static Class vistaLogoOff;

            [Embed(source = Path + "/click.mp3")]
            public static Class click;
        }

        const int FieldX = 5;
        const int FieldY = 5;

        const int FieldSize = 64;

        public LightsOut()
        {
            AnimateBackground();

            // http://www.flashkit.com/soundfx/Interfaces/

            CreateField(FieldX, FieldY);




        }

        private Array2D<Action> CreateField(int w, int h)
        {
            var a = new Array2D<Action>(w, h);

            var r =  new Random();

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

                            if (r.NextDouble() < 0.5)
                                a[btn.x, btn.y]();

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

                            
                            btn.sprite.click +=
                                delegate
                                {
                                    f(0, 0);
                                    f(1, 0);
                                    f(-1, 0);
                                    f(0, 1);
                                    f(0, -1);

                                    

                                    btn.click.play();
                                };



                            btn.sprite.mouseOver += e => btn.sprite.alpha = 1;
                            btn.sprite.mouseOut += e => btn.sprite.alpha = 0.6;
                        }
                    );

            return a;
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

}
