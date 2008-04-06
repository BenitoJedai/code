using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.mx.graphics;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.utils;


namespace FlashRadialGradient.ActionScript
{
    /// <summary>
    /// testing...
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    [SWF(width = FlashRadialGradient.Width, height = FlashRadialGradient.Height)]
    public class FlashRadialGradient : Sprite
    {
        public const int Width = 200;
        public const int Height = 200;

        public static int[] colors = new[] { 0xFFCC66, 0x00FF00, 0x0000FF, 0xFF0000 };

        public int color1;
        public int colors_index = 0;

        Sprite s;
        Graphics g;

        public FlashRadialGradient()
        {
            s = new Sprite().AttachTo(this);

            g = s.graphics;

            color1 = colors[0];


            addChild(
                    new TextField
                    {
                        text = "powered by jsc",
                        x = 20,
                        y = 40,
                        selectable = false,
                        sharpness = -400,
                        textColor = 0xffffff,
                        mouseEnabled = false
                    }
                );

            
            this.mouseMove +=
                ev =>
                {
                    localX = (int)ev.stageX;
                    localY = (int)ev.stageY;

                    redraw();
                };

            this.click +=
                delegate
                {
                    color1 = colors[++colors_index % (colors.Length - 1)];
                    redraw();
                };

		    var timer = new Timer(1000 / 24, 0);

            timer.timer +=
                delegate
                {
                    counter++;

                    redraw();
                };
				
			timer.start();

            redraw();
        }

        private void redraw()
        {
            g.clear();

            draw(counter % 360);
            draw((counter + this.localX) % 360);
            draw((counter + this.localY) % 360);
            draw((counter + this.localY + this.localX) % 360);
            draw((counter + this.localY - this.localX) % 360);

        }


        private int counter;
        private int localX;
        private int localY;

        private void draw(int angle)
        {
            var fill = new RadialGradient();


            fill.entries = new[] { 
                new GradientEntry((uint)color1, 0.00, 0.5),
                new GradientEntry(0x000000, 0.33, 0.5),
                new GradientEntry(0x99FF33, 0.66, 0.0)
            };

            // Set focal point to upper left corner.
            fill.angle = angle;
            fill.focalPointRatio = -0.8;



            // Draw a box and fill it with the RadialGradient.
            g.moveTo(0, 0);

            var w = Width;
            var h = Height;

            using (fill.Fill(g, new Rectangle(0, 0, w, h)))
            {
                g.lineTo(w, 0);
                g.lineTo(w, h);
                g.lineTo(0, h);
                g.lineTo(0, 0);
            }



        }
    }

}
