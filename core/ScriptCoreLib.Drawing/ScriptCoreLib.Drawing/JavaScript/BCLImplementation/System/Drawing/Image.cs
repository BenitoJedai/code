﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Drawing
{
    [Script(Implements = typeof(global::System.Drawing.Image))]
    internal class __Image : __MarshalByRefObject
    {
        public int Width { get; set;  }
        public int Height { get; set; }
    }
}
