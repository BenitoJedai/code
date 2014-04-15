using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.ToolStripStatusLabel))]
    public class __ToolStripStatusLabel : __ToolStripLabel
    {
        // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.ToolStripStatusLabel.set_Spring(System.Boolean)]

        public ToolStripItemAlignment Alignment { get; set; }

        public bool Spring { get; set; }

    }
}
