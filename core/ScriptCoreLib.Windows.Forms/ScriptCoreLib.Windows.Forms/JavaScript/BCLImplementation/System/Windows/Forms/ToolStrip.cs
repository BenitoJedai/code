﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.ToolStrip))]
    internal class __ToolStrip : __ScrollableControl
    {
        public __ToolStripItemCollection Items { get; set; }
    }
}
