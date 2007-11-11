using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;

using ScriptCoreLib.Shared;

namespace ScriptCoreLib.JavaScript.Runtime
{
    /// <summary>
    /// http://www.utoronto.ca/ian/books/xhtml1/appd/update-23feb2000.html
    /// http://www.codehouse.com/webmaster_tools/x11_color_palette/
    /// </summary>
    [Script]
    public class JSColor
    {
        /// <summary>
        /// http://www.w3.org/TR/CSS21/ui.html#system-colors
        /// </summary>
        [Script]
        public static class System
        {
            /// <summary>
            /// Active window border.
            /// </summary>
            public static JSColor ActiveBorder { get { return JSColor.FromValue("ActiveBorder"); } }
            /// <summary>
            /// Active window caption.
            /// </summary>
            public static JSColor ActiveCaption { get { return JSColor.FromValue("ActiveCaption"); } }
            /// <summary>
            /// Background color of multiple document interface.
            /// </summary>
            public static JSColor AppWorkspace { get { return JSColor.FromValue("AppWorkspace"); } }
            /// <summary>
            /// Desktop background.
            /// </summary>
            public static JSColor Background { get { return JSColor.FromValue("Background"); } }
            /// <summary>
            /// Face color for three-dimensional display elements.
            /// </summary>
            public static JSColor ButtonFace { get { return JSColor.FromValue("ButtonFace"); } }
            /// <summary>
            /// Highlight color for three-dimensional display elements (for edges facing away from the light source).
            /// </summary>
            public static JSColor ButtonHighlight { get { return JSColor.FromValue("ButtonHighlight"); } }
            /// <summary>
            /// Shadow color for three-dimensional display elements.
            /// </summary>
            public static JSColor ButtonShadow { get { return JSColor.FromValue("ButtonShadow"); } }
            /// <summary>
            /// Text on push buttons.
            /// </summary>
            public static JSColor ButtonText { get { return JSColor.FromValue("ButtonText"); } }
            /// <summary>
            /// Text in caption, size box, and scrollbar arrow box.
            /// </summary>
            public static JSColor CaptionText { get { return JSColor.FromValue("CaptionText"); } }
            /// <summary>
            /// Grayed (disabled) text. This color is set to #000 if the current display driver does not support a solid gray color.
            /// </summary>
            public static JSColor GrayText { get { return JSColor.FromValue("GrayText"); } }
            /// <summary>
            /// Item(s) selected in a control.
            /// </summary>
            public static JSColor Highlight { get { return JSColor.FromValue("Highlight"); } }
            /// <summary>
            /// Text of item(s) selected in a control.
            /// </summary>
            public static JSColor HighlightText { get { return JSColor.FromValue("HighlightText"); } }
            /// <summary>
            /// Inactive window border.
            /// </summary>
            public static JSColor InactiveBorder { get { return JSColor.FromValue("InactiveBorder"); } }
            /// <summary>
            /// Inactive window caption.
            /// </summary>
            public static JSColor InactiveCaption { get { return JSColor.FromValue("InactiveCaption"); } }
            /// <summary>
            /// Color of text in an inactive caption.
            /// </summary>
            public static JSColor InactiveCaptionText { get { return JSColor.FromValue("InactiveCaptionText"); } }
            /// <summary>
            /// Background color for tooltip controls.
            /// </summary>
            public static JSColor InfoBackground { get { return JSColor.FromValue("InfoBackground"); } }
            /// <summary>
            /// Text color for tooltip controls.
            /// </summary>
            public static JSColor InfoText { get { return JSColor.FromValue("InfoText"); } }
            /// <summary>
            /// Menu background.
            /// </summary>
            public static JSColor Menu { get { return JSColor.FromValue("Menu"); } }
            /// <summary>
            /// Text in menus.
            /// </summary>
            public static JSColor MenuText { get { return JSColor.FromValue("MenuText"); } }
            /// <summary>
            /// Scroll bar gray area.
            /// </summary>
            public static JSColor Scrollbar { get { return JSColor.FromValue("Scrollbar"); } }
            /// <summary>
            /// Dark shadow for three-dimensional display elements.
            /// </summary>
            public static JSColor ThreeDDarkShadow { get { return JSColor.FromValue("ThreeDDarkShadow"); } }
            /// <summary>
            /// Face color for three-dimensional display elements.
            /// </summary>
            public static JSColor ThreeDFace { get { return JSColor.FromValue("ThreeDFace"); } }
            /// <summary>
            /// Highlight color for three-dimensional display elements.
            /// </summary>
            public static JSColor ThreeDHighlight { get { return JSColor.FromValue("ThreeDHighlight"); } }
            /// <summary>
            /// Light color for three-dimensional display elements (for edges facing the light source).
            /// </summary>
            public static JSColor ThreeDLightShadow { get { return JSColor.FromValue("ThreeDLightShadow"); } }
            /// <summary>
            /// Dark shadow for three-dimensional display elements.
            /// </summary>
            public static JSColor ThreeDShadow { get { return JSColor.FromValue("ThreeDShadow"); } }
            /// <summary>
            /// Window background.
            /// </summary>
            public static JSColor Window { get { return JSColor.FromValue("Window"); } }
            /// <summary>
            /// Window frame.
            /// </summary>
            public static JSColor WindowFrame { get { return JSColor.FromValue("WindowFrame"); } }
            /// <summary>
            /// Text in windows.
            /// </summary>
            public static JSColor WindowText { get { return JSColor.FromValue("WindowText"); } }

        }

        /// <summary>
        /// red
        /// </summary>
        public byte R;
        /// <summary>
        /// green
        /// </summary>
        public byte G;
        /// <summary>
        /// blue
        /// </summary>
        public byte B;

        public string Value;

        #region hls to rgb
        const byte RANGE = 240;
        const byte HLSMAX = RANGE; /* H,L, and S vary over 0-HLSMAX */
        const byte RGBMAX = 255;   /* R,G, and B vary over 0-RGBMAX */
        /* HLSMAX BEST IF DIVISIBLE BY 6 */
        /* RGBMAX, HLSMAX must each fit in a byte. */

        /* Hue is undefined if Saturation is 0 (grey-scale) */
        /* This value determines where the Hue scrollbar is */
        /* initially set for achromatic colors */
        const byte UNDEFINED = HLSMAX * 2 / 3;

        /// <summary>
        /// hue
        /// </summary>
        public byte H;
        /// <summary>
        /// lum
        /// </summary>
        public byte L;

        /// <summary>
        /// sat
        /// </summary>
        public byte S;

        bool isHLS;

        #region known colors

        public static JSColor Red
        {
            get
            {
                return JSColor.FromRGB(0xff, 0, 0);
            }
        }

        public static JSColor Green
        {
            get
            {
                return JSColor.FromRGB(0, 0xff, 0);
            }
        }

        public static JSColor Yellow = JSColor.FromRGB(0xff, 0xff, 0); 
        public static JSColor Gray = JSColor.FromGray(0x80);

        public static JSColor Blue { get { return JSColor.FromRGB(0, 0, 0xff); } }
        public static JSColor Cyan { get { return JSColor.FromRGB(0, 0xff, 0xff); } }
        public static JSColor Black = JSColor.FromGray(0); 
        public static JSColor Transparent = JSColor.FromValue("transparent"); 
        public static JSColor None = JSColor.FromValue("");

        private static JSColor FromValue(string p)
        {
            JSColor c = new JSColor();

            c.Value = p;

            return c;
        }

        public static JSColor White= JSColor.FromGray(0xff);
            
        

        #endregion


        static int HueToRGB(int n1, int n2, int hue)
        {
            /* range check: note values passed add/subtract thirds of range */
            if (hue < 0)
                hue += HLSMAX;

            if (hue > HLSMAX)
                hue -= HLSMAX;

            /* return r,g, or b value from this tridrant */
            if (hue < (HLSMAX / 6))
                return (n1 + (((n2 - n1) * hue + (HLSMAX / 12)) / (HLSMAX / 6)));
            if (hue < (HLSMAX / 2))
                return (n2);
            if (hue < ((HLSMAX * 2) / 3))
                return (n1 + (((n2 - n1) * (((HLSMAX * 2) / 3) - hue) + (HLSMAX / 12)) / (HLSMAX / 6)));
            else
                return (n1);
        }

        public JSColor ToRGB()
        {
            JSColor c = new JSColor();

            if (this.S == 0)
            {   
                /* achromatic case */
                // compiler bug: multiple assignments not supported
                var v = (byte)((this.L * RGBMAX) / HLSMAX);

                c.R = v;
                c.G = v;
                c.B = v;

                if (this.H != UNDEFINED)
                {
                    /* ERROR */
                }
            }
            else
            {                    /* chromatic case */

                int Magic1, Magic2;

                /* set up magic numbers */
                if (this.L <= (HLSMAX / 2))
                    Magic2 = (this.L * (HLSMAX + this.S) + (HLSMAX / 2)) / HLSMAX;
                else
                    Magic2 = this.L + this.S - ((this.L * this.S) + (HLSMAX / 2)) / HLSMAX;

                Magic1 = 2 * this.L - Magic2;

                /* get RGB, change units from HLSMAX to RGBMAX */
                c.R = Convert.ToByte((HueToRGB(Magic1, Magic2, this.H  + (HLSMAX / 3)) * RGBMAX + (HLSMAX / 2)) / HLSMAX);
                c.G = Convert.ToByte((HueToRGB(Magic1, Magic2, this.H) * RGBMAX + (HLSMAX / 2)) / HLSMAX);
                c.B = Convert.ToByte((HueToRGB(Magic1, Magic2, this.H - (HLSMAX / 3)) * RGBMAX + (HLSMAX / 2)) / HLSMAX);
            }


            return c;


        }



        public JSColor ToHLS()
        {
            JSColor c = new JSColor();
            
            c.isHLS = true;

            int cMax = Native.Math.max(Native.Math.max(R, G), B);
            int cMin = Native.Math.min(Native.Math.min(R, G), B);

            int H, L, S;

            L = (((cMax + cMin) * HLSMAX) + RGBMAX) / (2 * RGBMAX);

            if (cMax == cMin)
            {           /* r=g=b --> achromatic case */
                S = 0;                     /* saturation */
                H = UNDEFINED;             /* hue */
            }
            else
            {
                /* saturation */
                if (L <= (HLSMAX / 2))
                    S = (((cMax - cMin) * HLSMAX) + ((cMax + cMin) / 2)) / (cMax + cMin);
                else
                    S = (((cMax - cMin) * HLSMAX) + ((2 * RGBMAX - cMax - cMin) / 2))
                       / (2 * RGBMAX - cMax - cMin);

                /* hue */
                int Rdelta = (((cMax - R) * (HLSMAX / 6)) + ((cMax - cMin) / 2)) / (cMax - cMin);
                int Gdelta = (((cMax - G) * (HLSMAX / 6)) + ((cMax - cMin) / 2)) / (cMax - cMin);
                int Bdelta = (((cMax - B) * (HLSMAX / 6)) + ((cMax - cMin) / 2)) / (cMax - cMin);

                if (R == cMax)
                    H = Bdelta - Gdelta;
                else if (G == cMax)
                    H = (HLSMAX / 3) + Rdelta - Bdelta;
                else /* B == cMax */
                    H = ((2 * HLSMAX) / 3) + Gdelta - Rdelta;

                if (H < 0)
                    H += HLSMAX;
                if (H > HLSMAX)
                    H -= HLSMAX;
            }

            c.H = Convert.ToByte(H);
            c.L = Convert.ToByte(L);
            c.S = Convert.ToByte(S);

            return c;
        }


        /// <summary>
        /// returns color by HLS
        /// </summary>
        /// <param name="h">up to 240</param>
        /// <param name="l">up to 120</param>
        /// <param name="s">up to 240</param>
        /// <returns></returns>
        public static JSColor FromHLS(byte h, byte l, byte s)
        {
            JSColor n = new JSColor();
            n.H = h;
            n.L = l;
            n.S = s;
            n.isHLS = true;

            return n;
        }

        public static JSColor FromRGB(byte r, byte g, byte b)
        {
            JSColor n = new JSColor();
            n.R = r;
            n.G = g;
            n.B = b;

            return n;
        }

        #endregion

        public static JSColor FromGray(byte g)
        {
            return FromRGB(g, g, g);
        }

      

        public static implicit operator string (JSColor e)
        {
            return e.ToString();
        }

        public override string ToString()
        {
            JSColor z = this;

            if (z.Value != null)
                return z.Value;

            if (z.isHLS)
                z = z.ToRGB();

            return "RGB(" + z.R + ", " + z.G + ", " + z.B + ")";
        }
    }
}