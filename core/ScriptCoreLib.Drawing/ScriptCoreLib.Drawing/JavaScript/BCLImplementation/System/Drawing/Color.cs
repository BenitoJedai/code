using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Drawing
{
    [Script(Implements = typeof(global::System.Drawing.Color))]
    internal class __Color
    {
        public static Color FromArgb(int red, int green, int blue)
        {
            return default(Color);
        }

        static public Color Red { get; set; }
    }
}
