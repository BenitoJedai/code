using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Drawing
{
    [Script(Implements = typeof(global::System.Drawing.Size))]
    internal class __Size
    {
        public int Height { get; set; }
        public int Width { get; set; }

        public __Size(int w, int h)
        {
            this.Height = h;
            this.Width = w;
        }

    }
}
