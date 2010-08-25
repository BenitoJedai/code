using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows
{
    [Script(Implements = typeof(global::System.Windows.Size))]
    internal class __Size  : __IFormattable
    {
        public __Size()
            : this(0, 0)
        {

        }

        public __Size(double width, double height)
        {
            this.Height = height;
            this.Width = width;
        }

        public double Height { get; set; }
        public double Width { get; set; }



        public string ToString(string format, IFormatProvider formatProvider)
        {
            throw new NotImplementedException();
        }
    }
}
