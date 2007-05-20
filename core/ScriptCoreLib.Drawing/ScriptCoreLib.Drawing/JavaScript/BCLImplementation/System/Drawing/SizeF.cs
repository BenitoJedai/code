using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Drawing
{
    [Script(Implements = typeof(global::System.Drawing.SizeF))]
    internal class __SizeF
    {
        public float Height { get; set; }
        public float Width { get; set; }

        public __SizeF(float w, float h)
        {
            this.Height = h;
            this.Width = w;
        }

    }
}
