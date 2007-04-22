
namespace ScriptCoreLib.Shared.Drawing
{


    [Script]
    public class Color
    {
        public int R;
        public int G;
        public int B;

        public static implicit operator string(Color e)
        {
            return e.ToString();
        }

        public static implicit operator int(Color e)
        {
            return e.B + (e.G << 8) + (e.R << 16);
        }

        public static implicit operator Color([Hex] int e)
        {
            var b = e & 0xFF;
            var g = (e >> 8) & 0xFF;
            var r = (e >> 16) & 0xFF;

            return Color.FromRGB(r, g, b);
        }

        public static Color FromRGB(int r, int g, int b)
        {
            Color n = new Color();

            n.R = r;
            n.G = g;
            n.B = b;

            return n;
        }

         public static Color FromGray(int g)
        {
            return FromRGB(g, g, g);
        }

        /// <summary>
        /// returns empty string
        /// </summary>
        public static Color None { get { return FromKnownName(""); } }
        public static Color Transparent { get { return FromKnownName("transparent"); } }

        public static Color Black { get { return FromGray(0x00); } }
         public static Color Gray { get { return FromGray(0x80); } }
        public static Color White { get { return FromGray(0xFF); } }
        public static Color Red { get { return FromRGB(0xFF, 0x00, 0x00); } }
         public static Color Green { get { return FromRGB(0x00, 0xff, 0x00); }}
         public static Color Blue { get { return 0xff; } }
         public static Color Yellow { get { return 0xffff00; } }


        public string KnownName;

        public static Color FromKnownName(string p)
        {
            Color c = new Color();

            c.KnownName = p;

            return c;
        }

        public override string ToString()
        {
            Color z = this;

            if (z.KnownName != null)
                return z.KnownName;

            //if (z.isHLS)
            //    z = z.ToRGB();

            return "RGB(" + z.R + ", " + z.G + ", " + z.B + ")";
        }


        /// <summary>
        /// http://www.w3.org/TR/CSS21/ui.html#system-colors
        /// </summary>
        [Script]
        public static class System
        {
            /// <summary>
            /// Active window border.
            /// </summary>
            public static Color ActiveBorder { get { return Color.FromKnownName("ActiveBorder"); } }
            /// <summary>
            /// Active window caption.
            /// </summary>
            public static Color ActiveCaption { get { return Color.FromKnownName("ActiveCaption"); } }
            /// <summary>
            /// Background color of multiple document interface.
            /// </summary>
            public static Color AppWorkspace { get { return Color.FromKnownName("AppWorkspace"); } }
            /// <summary>
            /// Desktop background.
            /// </summary>
            public static Color Background { get { return Color.FromKnownName("Background"); } }
            /// <summary>
            /// Face color for three-dimensional display elements.
            /// </summary>
            public static Color ButtonFace { get { return Color.FromKnownName("ButtonFace"); } }
            /// <summary>
            /// Highlight color for three-dimensional display elements (for edges facing away from the light source).
            /// </summary>
            public static Color ButtonHighlight { get { return Color.FromKnownName("ButtonHighlight"); } }
            /// <summary>
            /// Shadow color for three-dimensional display elements.
            /// </summary>
            public static Color ButtonShadow { get { return Color.FromKnownName("ButtonShadow"); } }
            /// <summary>
            /// Text on push buttons.
            /// </summary>
            public static Color ButtonText { get { return Color.FromKnownName("ButtonText"); } }
            /// <summary>
            /// Text in caption, size box, and scrollbar arrow box.
            /// </summary>
            public static Color CaptionText { get { return Color.FromKnownName("CaptionText"); } }
            /// <summary>
            /// Grayed (disabled) text. This color is set to #000 if the current display driver does not support a solid gray color.
            /// </summary>
            public static Color GrayText { get { return Color.FromKnownName("GrayText"); } }
            /// <summary>
            /// Item(s) selected in a control.
            /// </summary>
            public static Color Highlight { get { return Color.FromKnownName("Highlight"); } }
            /// <summary>
            /// Text of item(s) selected in a control.
            /// </summary>
            public static Color HighlightText { get { return Color.FromKnownName("HighlightText"); } }
            /// <summary>
            /// Inactive window border.
            /// </summary>
            public static Color InactiveBorder { get { return Color.FromKnownName("InactiveBorder"); } }
            /// <summary>
            /// Inactive window caption.
            /// </summary>
            public static Color InactiveCaption { get { return Color.FromKnownName("InactiveCaption"); } }
            /// <summary>
            /// Color of text in an inactive caption.
            /// </summary>
            public static Color InactiveCaptionText { get { return Color.FromKnownName("InactiveCaptionText"); } }
            /// <summary>
            /// Background color for tooltip controls.
            /// </summary>
            public static Color InfoBackground { get { return Color.FromKnownName("InfoBackground"); } }
            /// <summary>
            /// Text color for tooltip controls.
            /// </summary>
            public static Color InfoText { get { return Color.FromKnownName("InfoText"); } }
            /// <summary>
            /// Menu background.
            /// </summary>
            public static Color Menu { get { return Color.FromKnownName("Menu"); } }
            /// <summary>
            /// Text in menus.
            /// </summary>
            public static Color MenuText { get { return Color.FromKnownName("MenuText"); } }
            /// <summary>
            /// Scroll bar gray area.
            /// </summary>
            public static Color Scrollbar { get { return Color.FromKnownName("Scrollbar"); } }
            /// <summary>
            /// Dark shadow for three-dimensional display elements.
            /// </summary>
            public static Color ThreeDDarkShadow { get { return Color.FromKnownName("ThreeDDarkShadow"); } }
            /// <summary>
            /// Face color for three-dimensional display elements.
            /// </summary>
            public static Color ThreeDFace { get { return Color.FromKnownName("ThreeDFace"); } }
            /// <summary>
            /// Highlight color for three-dimensional display elements.
            /// </summary>
            public static Color ThreeDHighlight { get { return Color.FromKnownName("ThreeDHighlight"); } }
            /// <summary>
            /// Light color for three-dimensional display elements (for edges facing the light source).
            /// </summary>
            public static Color ThreeDLightShadow { get { return Color.FromKnownName("ThreeDLightShadow"); } }
            /// <summary>
            /// Dark shadow for three-dimensional display elements.
            /// </summary>
            public static Color ThreeDShadow { get { return Color.FromKnownName("ThreeDShadow"); } }
            /// <summary>
            /// Window background.
            /// </summary>
            public static Color Window { get { return Color.FromKnownName("Window"); } }
            /// <summary>
            /// Window frame.
            /// </summary>
            public static Color WindowFrame { get { return Color.FromKnownName("WindowFrame"); } }
            /// <summary>
            /// Text in windows.
            /// </summary>
            public static Color WindowText { get { return Color.FromKnownName("WindowText"); } }

        }

    }

   
}
