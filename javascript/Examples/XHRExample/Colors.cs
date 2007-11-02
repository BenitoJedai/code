using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;

namespace jsXMLhttpRequest
{
    [Script]
    class ColorBeta
    {
        const byte RANGE = 240;
        const byte HLSMAX = RANGE; /* H,L, and S vary over 0-HLSMAX */
        const byte RGBMAX = 255;   /* R,G, and B vary over 0-RGBMAX */
        /* HLSMAX BEST IF DIVISIBLE BY 6 */
        /* RGBMAX, HLSMAX must each fit in a byte. */

        /* Hue is undefined if Saturation is 0 (grey-scale) */
        /* This value determines where the Hue scrollbar is */
        /* initially set for achromatic colors */
        const byte UNDEFINED = HLSMAX * 2 / 3;

        public byte H;
        public byte L;
        public byte S;

        public static ColorBeta RGBtoHLS(Color c)
        {
            return RGBtoHLS((byte)c.R, (byte) c.G, (byte) c.B);
        }

        public static ColorBeta RGBtoHLS(byte R, byte G, byte B)
        {
            ColorBeta c = new ColorBeta();

            int cMax = System.Math.Max(System.Math.Max(R, G), B);
            int cMin = System.Math.Min(System.Math.Min(R, G), B);

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

            c.H = (byte)System.Math.Floor((double)H);
            c.L = (byte)System.Math.Floor((double)L);
            c.S = (byte)System.Math.Floor((double)S);

            return c;
        }



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

        public Color HLStoRGB()
        {
            return HLStoRGB(this.H, this.L, this.S);
        }

        public static Color HLStoRGB(byte hue, byte lum, byte sat)
        {
            Color c = new Color();

            if (sat == 0)
            {            /* achromatic case */
                c.R = c.G = c.B = (byte)((lum * RGBMAX) / HLSMAX);
                if (hue != UNDEFINED)
                {
                    /* ERROR */
                }
            }
            else
            {                    /* chromatic case */

                int Magic1, Magic2;

                /* set up magic numbers */
                if (lum <= (HLSMAX / 2))
                    Magic2 = (lum * (HLSMAX + sat) + (HLSMAX / 2)) / HLSMAX;
                else
                    Magic2 = lum + sat - ((lum * sat) + (HLSMAX / 2)) / HLSMAX;

                Magic1 = 2 * lum - Magic2;

                /* get RGB, change units from HLSMAX to RGBMAX */
                c.R = (byte)System.Math.Round((double)(HueToRGB(Magic1, Magic2, hue + (HLSMAX / 3)) * RGBMAX + (HLSMAX / 2)) / HLSMAX);
                c.G = (byte)System.Math.Round((double)(HueToRGB(Magic1, Magic2, hue) * RGBMAX + (HLSMAX / 2)) / HLSMAX);
                c.B = (byte)System.Math.Round((double)(HueToRGB(Magic1, Magic2, hue - (HLSMAX / 3)) * RGBMAX + (HLSMAX / 2)) / HLSMAX);
            }


            return c;


        }
    }

    [Script]
    class ColorMap
    {
        public readonly IHTMLDiv View = new IHTMLDiv();

        ColorBeta cb = new ColorBeta();


        public ColorMap()
        {
            View.style.SetSize(260, 240);



            View.onmousemove += new EventHandler<IEvent>(View_onmousemove);
        }

        void View_onmousemove(IEvent e)
        {
            View.innerHTML = e.OffsetX + "; " + e.OffsetY;

            if (e.OffsetX < 240)
            {
                cb.H = (byte)e.OffsetX;
                cb.S = (byte)e.OffsetY;
                

            }
            else
            {
                cb.L = (byte)e.OffsetY;
            }

            View.style.background = cb.HLStoRGB();
        }

        
    }

    [Script]
    class ColorDialogBeta
    {
        IHTMLElement _view = new IHTMLDiv();
        IHTMLElement _viewclose = new IHTMLButton("Close");
        IHTMLElement _viewtitle = new IHTMLDiv("ColorDialog");

        public event EventHandler DialogClosed;

        // http://html.mcwebber.net/customize.html

        IHTMLInput _text_r = new IHTMLInput(HTMLInputTypeEnum.text, "0");
        IHTMLInput _text_g = new IHTMLInput(HTMLInputTypeEnum.text, "0");
        IHTMLInput _text_b = new IHTMLInput(HTMLInputTypeEnum.text, "0");
        IHTMLElement _rgb = new IHTMLDiv();
        IHTMLElement _RGBtoHLS = new IHTMLDiv();

        ColorMap _cmap = new ColorMap();

        public void Show()
        {
            Native.Document.body.appendChild(_view);

            _view.style.ToCenter(Native.Document.body, 400, 400);
            _view.style.overflow = IStyle.OverflowEnum.hidden;

            _view.style.border = "2px solid #808080";
            _view.style.background = "#C0C0C0";
            _view.oncontextmenu += Native.DisabledEventHandler;



            //_viewtitle.style.position = IStyle.PositionEnum.absolute;
            _viewtitle.style.padding = "3px";

            _view.appendChild(_viewtitle);

            //_viewtitle.style.top = "0px";
            //_viewtitle.style.left = "0px";
            //_viewtitle.style.width = (_view.clientWidth - 4) + "px";
            //_viewtitle.style.height = "24px";

            _viewtitle.style.color = Color.White;
            _viewtitle.style.background = Color.Black;
            _viewtitle.style.cursor = IStyle.CursorEnum.@default;
            _viewtitle.onmousedown += _viewtitle_onmousedown;

            Native.Document.onmousemove +=_viewtitle_onmousemove;
            Native.Document.onmouseup += _viewtitle_onmouseup;


            _rgb.style.Float = IStyle.FloatEnum.right;
            _rgb.style.margin = "4px";
            _rgb.style.SetSize(32, 32);
            _rgb.style.border = "2px solid #808080";
            _view.appendChild(_rgb);

            EventHandler<IEvent> _change = delegate(IEvent e)
            {
                Color c = Color.FromRGB(
                        (byte)int.Parse(_text_r.value),
                        (byte)int.Parse(_text_g.value),
                        (byte)int.Parse(_text_b.value)
                        );

                _rgb.style.background = c;

                ColorBeta cb = ColorBeta.RGBtoHLS(c);
                Color cc = cb.HLStoRGB();

                _RGBtoHLS.innerHTML = "H: " + cb.H + ", L: " + cb.L + ", S: " + cb.S + ", R: " + cc.R + ", G: " + cc.G + ", B: " + cc.B;

            };

            _view.appendChild(new ITextNode("red:"));
            _text_r.style.margin = "4px";
            _text_r.onchange += _change;
            _view.appendChild(_text_r);
            _view.appendChild(Native.Document.createElement("br"));

            _view.appendChild(new ITextNode("green:"));
            _text_g.style.margin = "4px";
            _text_g.onchange += _change;
            _view.appendChild(_text_g);
            _view.appendChild(Native.Document.createElement("br"));

            _view.appendChild(new ITextNode("blue:"));
            _text_b.style.margin = "4px";
            _text_b.onchange += _change;
            _view.appendChild(_text_b);
            _view.appendChild(Native.Document.createElement("br"));

            _view.appendChild(_RGBtoHLS);

            _view.appendChild(Native.Document.createElement("hr"));

            _view.appendChild(_cmap.View);
            _cmap.View.style.background = Color.Black;
            _cmap.View.style.color = Color.White;
            _cmap.View.style.position = IStyle.PositionEnum.absolute;


            _viewclose.style.position = IStyle.PositionEnum.absolute;
            _viewclose.style.top = "2px";
            _viewclose.style.right = "2px";


            _view.appendChild(_viewclose);

            _viewclose.onclick += _viewclose_onclick;
        }

        //bool _ismove = false;

        int _move_x;
        int _move_y;

        IStyle.OverflowEnum _move_ov;
        int _move_state = 0;

        void _viewtitle_onmouseup(IEvent e)
        {
            if (_move_state > 0)
            {
                //_ismove = false;



                e.PreventDefault();
                e.StopPropagation();

                if (_move_state == 2)
                {
                    Native.Document.body.style.overflow = _move_ov;
                    _view.style.Opacity = 1;
                }

                _move_state = 0;
            }
        }

        void _viewtitle_onmousemove(IEvent e)
        {
            if (_move_state > 0)
            {
                if (_move_state == 1)
                {
                    _move_ov = Native.Document.body.style.overflow;
                    Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;
                    _view.style.Opacity = 0.7;

                    _move_state = 2;
                }

                int _x, _y;

                _x = System.Math.Max(0, e.CursorX - _move_x);
                _x = System.Math.Min(_x, Native.Document.body.clientWidth - _view.clientWidth);

                _y = System.Math.Max(0, e.CursorY - _move_y);
                _y = System.Math.Min(_y, Native.Document.body.clientHeight - _view.clientHeight);


                _view.style.SetLocation(_x, _y);
            }
        }

        void _viewtitle_onmousedown(IEvent e)
        {

            //_ismove = true;

            _move_x = e.OffsetX;
            _move_y = e.OffsetY;

            e.PreventDefault();
            e.StopPropagation();



            _move_state = 1;
        }

        public void Close()
        {
            Fader.FadeAndRemove(_view, 0, 100);

            if (DialogClosed != null)
                DialogClosed();
        }

        void _viewclose_onclick(IEvent e)
        {
            Close();
        }
    }
}