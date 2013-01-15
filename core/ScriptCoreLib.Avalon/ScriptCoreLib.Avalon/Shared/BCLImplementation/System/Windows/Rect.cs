using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Windows
{
    [Script(Implements = typeof(global::System.Windows.Rect))]
    internal class __Rect
    {
        public double Height { get; set; }
        public double Width { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public double Right { get { return this.X + this.Width; } }
        public double Bottom { get { return this.Y + this.Height; } }

        public __Rect()
            : this(0, 0, 0, 0)
        {

        }

        public __Rect(double x, double y, double width, double height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }
    }
}
