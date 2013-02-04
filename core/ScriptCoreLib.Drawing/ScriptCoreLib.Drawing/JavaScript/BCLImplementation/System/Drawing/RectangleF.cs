using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Drawing
{
    [Script(Implements = typeof(global::System.Drawing.RectangleF))]
    internal class __RectangleF
    {

        public __RectangleF()
        {

        }

        public __RectangleF(float x, float y, float width, float height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        public float Width { get; set; }
        public float Height { get; set; }
        public float X { get; set; }
        public float Y { get; set; }

        public static implicit operator __RectangleF(Rectangle r)
        {
            var x = new __RectangleF(); ;
            x.X = r.X;
            x.Y = r.Y;
            x.Width = r.Width;
            x.Height = r.Height;

            return x;
        }

    }
}
