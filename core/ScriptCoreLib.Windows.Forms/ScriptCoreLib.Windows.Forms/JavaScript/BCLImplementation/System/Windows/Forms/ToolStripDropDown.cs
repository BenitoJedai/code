using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.ToolStripDropDown))]
    internal class __ToolStripDropDown : __ToolStrip
    {
        public event CancelEventHandler Opening;
    }
}
