﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android.BCLImplementation.System.Drawing
{
    [Script(Implements = typeof(global::System.Drawing.Size))]
    internal class __Size
    {
        public __Size()
            : this(0, 0)
        {

        }

        public __Size(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public int Width { get; set; }
        public int Height { get; set; }
    }
}
