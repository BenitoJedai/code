using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.mx.graphics;
using ScriptCoreLib.Extensions;

namespace FlashLinearGradient
{
    public sealed class ApplicationSprite : Sprite
    {
        public const int DefaultWidth = 320;
        public const int DefaultHeight = 200;

        public const uint Color1 = 0xff0000;
        public const uint Color2 = 0x800000;

        public ApplicationSprite()
        {
            var s = new Sprite().AttachTo(this);
            var g = s.graphics;

            var fill = new LinearGradient();

            var g1 = new GradientEntry { color = Color1, ratio = 0.00, alpha = 0 };
            var g2 = new GradientEntry { color = Color2, ratio = 0.90, alpha = 1 };

            fill.entries = new[] { g1, g2 };
            // rotation?
            fill.angle = 45;

            // Draw a box and fill it with the LinearGradient.
            g.moveTo(0, 0);

            fill.begin(g, new Rectangle { width = DefaultWidth, height = DefaultHeight }, new Point());

            g.lineTo(DefaultWidth, 0);
            g.lineTo(DefaultWidth, DefaultHeight);
            g.lineTo(0, DefaultHeight);
            g.lineTo(0, 0);

            fill.end(g);

        }


    }
}
