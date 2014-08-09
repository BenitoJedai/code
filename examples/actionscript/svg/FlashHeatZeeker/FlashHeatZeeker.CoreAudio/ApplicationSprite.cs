using FlashHeatZeeker.CoreAudio.Library;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using System;
using System.Windows.Forms;

namespace FlashHeatZeeker.CoreAudio
{
    public sealed class ApplicationSprite : Sprite
    {

        public ApplicationSprite()
        {
        }

        // cannot define the interface inline can we..
        public void Visualize(Func<IVisualizer> new_Visualizer)
        {
            // how many sounds we have?
            Soundboard sb = new Soundboard();

            new[] {
                sb.loopmachinegun,
                sb.loophelicopter1,
                sb.loopdiesel2,
                sb.loopsand_run,
                sb.loopjeepengine,
                sb.loopcrickets,
                sb.loopstrange1,
                sb.loop_GallinagoDelicata,
            }.WithEachIndex(
                (s, index) =>
                {
                    var v = new_Visualizer();

                    v.SetMasterVolume = value => s.MasterVolume = double.Parse(value);

                    // we did it for imp.
                    v.SetLeftVolume = value => s.LeftVolume = double.Parse(value);
                    v.SetRightVolume = value => s.RightVolume = double.Parse(value);

                    s.MasterVolume = 0;
                    s.Sound.play();


                    v.Initialize(new { index }.ToString());
                }
            );
        }
    }


    public interface IVisualizer
    {
        Action<string> SetMasterVolume { get; set; }
        Action<string> SetLeftVolume { get; set; }
        Action<string> SetRightVolume { get; set; }

        Action<string> Initialize { get; set; }
    }

    class Visualizer : IVisualizer
    {
        public Action<string> SetMasterVolume { get; set; }
        public Action<string> SetLeftVolume { get; set; }
        public Action<string> SetRightVolume { get; set; }

        public Action<string> Initialize { get; set; }

        // sent to flash

        public Visualizer()
        {
            // i was just created. yay. properties not ready yet!

            this.Initialize = Text =>
            {
                var f = new Form { Text = Text };

                f.LocationChanged += delegate
                {
                    var max = (Native.window.Width - f.Width) / 2.0;

                    var right = f.Left / max;
                    var left = (Native.window.Width - f.Right) / max;

                    var top = f.Top / (double)(Native.window.Height - f.Height);

                    this.SetMasterVolume("" + top);
                    this.SetLeftVolume("" + left);
                    this.SetRightVolume("" + right);

                };





                f.Show();
                f.Top = 0;

            };

        }
    }

    public static class ApplicationSpriteConsumer
    {

        // called by js
        public static void Visualize(this ApplicationSprite s)
        {
            s.Visualize(() => new Visualizer());
        }
    }


}
