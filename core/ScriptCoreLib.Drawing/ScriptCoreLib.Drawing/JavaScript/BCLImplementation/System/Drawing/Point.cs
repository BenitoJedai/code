using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Drawing
{
    [Script(Implements = typeof(global::System.Drawing.Point))]
    internal class __Point
    {

        public int X { get; set; }
        public int Y { get; set; }

        public __Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

    }
}
