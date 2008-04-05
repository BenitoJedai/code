using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.mx.graphics;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.Extensions;


namespace FlashRadialGradient.ActionScript
{
    /// <summary>
    /// testing...
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    public class FlashRadialGradient : Sprite
    {


        public FlashRadialGradient()
        {

            var fill = new RadialGradient();

            var g1 = new GradientEntry(0xFFCC66u, 0.00, 0.5);
            var g2 = new GradientEntry(0x000000, 0.33, 0.5);
            var g3 = new GradientEntry(0x99FF33, 0.66, 0.0);

            fill.entries = new[] { g1, g2, g3 };

            // Set focal point to upper left corner.
            fill.angle = 45;
            fill.focalPointRatio = -0.8;

            var g = this.graphics;

            // Draw a box and fill it with the RadialGradient.
            g.moveTo(0, 0);

            var w = 200;
            var h = 200;

            using (fill.Fill(g, new Rectangle(0, 0, w, h)))
            {
                g.lineTo(w, 0);
                g.lineTo(w, h);
                g.lineTo(0, h);
                g.lineTo(0, 0);
            }



            addChild(
                    new TextField
                    {
                        text = "powered by jsc",
                        x = 20,
                        y = 40,
                        selectable = false,
                        sharpness = -400,
                        textColor = 0xffffff
                    }
                );


        }
    }

}
