using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ScriptCoreLib.JavaScript.Drawing
{
    using BCLImplementation.System.Drawing;

    [Script]
    public static class Extensions
    {


        public static string ToCssString(this GraphicsUnit e)
        {
            if (e == GraphicsUnit.Pixel) return "px";
            if (e == GraphicsUnit.Point) return "pt";

            return "";
        }

        public static string ToCssString(this Font e)
        {
            // see http://www.w3schools.com/css/pr_font_font.asp

            __Font f = e;

            var w = f.KnownName;

            if (w == null)
            {
                w = "";

                if ((f._style & FontStyle.Italic) == FontStyle.Italic)
                    w += " italic";
                else
                    w += " normal";

                // font-variant
                w += " normal";

                // font-weight
                if ((f._style & FontStyle.Bold) == FontStyle.Bold)
                    w += " bold";
                else
                    w += " normal";

                // font-size/line-height
                w += " " + f._emSize + f._unit.ToCssString();

                // font-family
                w += " " + f._familyName;

            }

            /*
            font-style
            font-variant
            font-weight
            font-size/line-height
            font-family
             *
            caption
            icon
            menu
            message-box
            small-caption
            status-bar
             */

            Console.WriteLine("font: " + w);

            return w;
        }
    }
}
