using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript.Runtime;

namespace Toolbar.JavaScript
{
    [Script]
    static class Extensions
    {
        
        public static byte ToByte(this int e)
        {
            return (byte)(e % 0x100);
        }

        public static Color AddLum(this Color e, int v)
        {
            var c = JSColor.FromRGB(e.R.ToByte(), e.G.ToByte(), e.B.ToByte()).ToHLS();

            c.L = (c.L + v).Min(240).Max(0).ToByte();

            var x = c.ToRGB();

            return Color.FromRGB(x.R, x.G, x.B);
        }

        public static void SetDialogColor(this IHTMLDiv toolbar, Color toolbar_color)
        {
            SetDialogColor(toolbar, toolbar_color, true);
        }

        public static void SetDialogColor(this IHTMLDiv toolbar, Color toolbar_color, bool up)
        {


            if (up)
            {
                toolbar.style.backgroundColor = toolbar_color;

                var toolbar_color_light = toolbar_color.AddLum(+20);
                var toolbar_color_shadow = toolbar_color.AddLum(-20);

                toolbar.style.borderLeft = "1px solid " + toolbar_color_light;
                toolbar.style.borderTop = "1px solid " + toolbar_color_light;
                toolbar.style.borderRight = "1px solid " + toolbar_color_shadow;
                toolbar.style.borderBottom = "1px solid " + toolbar_color_shadow;
                toolbar.style.backgroundPosition = "0px 0px";
            }
            else
            {
                toolbar.style.backgroundColor = toolbar_color.AddLum(+15);

                var toolbar_color_light = toolbar_color.AddLum(+20 + 15);
                var toolbar_color_shadow = toolbar_color.AddLum(-20 + 15);

                toolbar.style.borderLeft = "1px solid " + toolbar_color_shadow;
                toolbar.style.borderTop = "1px solid " + toolbar_color_shadow;
                toolbar.style.borderRight = "1px solid " + toolbar_color_light;
                toolbar.style.borderBottom = "1px solid " + toolbar_color_light;
                toolbar.style.backgroundPosition = "1px 1px";
            }

        }


        public static int Random(this int i)
        {
            return new Random().Next(i);
        }
    }
}
