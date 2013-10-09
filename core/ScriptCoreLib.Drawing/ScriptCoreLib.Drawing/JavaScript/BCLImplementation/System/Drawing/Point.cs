using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Drawing
{
    [Script(Implements = typeof(global::System.Drawing.Point))]
    internal class __Point
    {

        public int X { get; set; }
        public int Y { get; set; }

        public __Point()
        {

        }

        public __Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public static __Point operator +(__Point pt, Size sz)
        {
            var p = new __Point(pt.X + sz.Width, pt.Y + sz.Height);


            return p;
        }

        public override string ToString()
        {
            return new { X, Y }.ToString();
        }
    }
}
