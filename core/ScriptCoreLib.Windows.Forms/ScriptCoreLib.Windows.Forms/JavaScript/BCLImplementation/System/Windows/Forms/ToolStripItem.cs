using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.ToolStripItem))]
    internal abstract class __ToolStripItem : __Component
    {
        public virtual string Text { get; set; }

        public event EventHandler Click;

        public string Name { get; set; }

        public virtual Size Size { get; set; }
    }
}
