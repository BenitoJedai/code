﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Drawing
{
    [Script(Implements = typeof(global::System.Drawing.Color))]
    internal class __Color
    {
        public Shared.Drawing.Color Value;

        public static Color FromArgb(int red, int green, int blue)
        {
            return new __Color { Value = Shared.Drawing.Color.FromRGB(red, green, blue) };
        }

        static __Color()
        {
            Green = new __Color { Value = Shared.Drawing.Color.Green };
            Red = new __Color { Value = Shared.Drawing.Color.Red };
            Yellow = new __Color { Value = Shared.Drawing.Color.Yellow };
        }

        static public Color Green { get; set; }
        static public Color Red { get; set; }
        static public Color Yellow { get; set; }
        static public Color Blue { get { return new __Color { Value = Shared.Drawing.Color.Blue }; } }
        static public Color Transparent { get { return new __Color { Value = Shared.Drawing.Color.Transparent }; } }


        #region
        static public implicit operator Color(__Color e)
        {
            return (Color)(object)e;
        }

        static public implicit operator __Color(Color e)
        {
            return (__Color)(object)e;
        }
        #endregion

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}
