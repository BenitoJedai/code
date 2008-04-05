using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.mx.graphics;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;


namespace FlashLinearGradient.ActionScript
{
    /// <summary>
    /// testing...
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    [SWF(
        backgroundColor = FlashLinearGradient.Color1,
        width = FlashLinearGradient.DefaultWidth,
        height = FlashLinearGradient.DefaultHeight)
    ]
    public class FlashLinearGradient : Sprite
    {
        public const int DefaultWidth = 320;
        public const int DefaultHeight = 200;

        public const uint Color1 = 0xff0000;
        public const uint Color2 = 0x800000;

        public FlashLinearGradient()
        {
            var s = new Sprite().AttachTo(this);
            var g = s.graphics;

            var fill = new LinearGradient();

            var g1 = new GradientEntry { color = Color1, ratio = 0.00, alpha = 0 };
            var g2 = new GradientEntry { color = Color2, ratio = 0.90, alpha = 1 };
            
            fill.entries = new[] { g1, g2};
            fill.angle = 90;

            // Draw a box and fill it with the LinearGradient.
            g.moveTo(0, 0);

            using (fill.Fill(g, new Rectangle { width = DefaultWidth, height = DefaultHeight }))
            {
                g.lineTo(DefaultWidth, 0);
                g.lineTo(DefaultWidth, DefaultHeight);
                g.lineTo(0, DefaultHeight);
                g.lineTo(0, 0);
            }

            
        }
    }

}
