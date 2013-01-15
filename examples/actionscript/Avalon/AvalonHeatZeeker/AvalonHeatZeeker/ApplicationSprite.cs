using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Extensions.Avalon;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
using System.Diagnostics;
using System.Windows.Media;

namespace AvalonHeatZeeker
{
    [SWF(frameRate = 100)]
    public sealed class ApplicationSprite : Sprite
    {
        public readonly ApplicationCanvas content = new ApplicationCanvas();

        public ApplicationSprite()
        {
            this.InvokeWhenStageIsReady(
                () =>
                {
                    content.AttachToContainer(this);
                    content.AutoSizeTo(this.stage);
                }
            );


            //var music = "assets/AvalonHeatZeeker/helicopter1.mp3".ToSound();

            //music.Start();

            //music.PlaybackComplete +=
            //    delegate
            //    {
            //        music.Start();
            //    };

            var sw = new Stopwatch();

            sw.Start();

            var i = 0;

            this.enterFrame +=
                delegate
                {


                    if (sw.ElapsedMilliseconds < 1000)
                    {
                        i++;
                        return;
                    }

                    this.content.label.Text = new { fps = i }.ToString();

                    if (fps != null)
                        fps("" + i);

                    i = 0;

                    sw.Restart();

                };
        }

        public event Action<string> fps;

        public void foo()
        {

        }
    }
}
