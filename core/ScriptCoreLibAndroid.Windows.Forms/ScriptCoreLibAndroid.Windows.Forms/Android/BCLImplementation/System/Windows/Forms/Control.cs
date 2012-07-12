using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.Android.BCLImplementation.System.ComponentModel;

namespace ScriptCoreLib.Android.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.Control))]
    internal partial class __Control : __Component
    {
        public Control.ControlCollection Controls { get; internal set;  }

        public global::System.Drawing.Point Location { get; set; }
        public global::System.Drawing.Size Size { get; set; }

        public string Name { get; set; }

        public bool AutoSize { get; set; }
        public virtual string Text { get; set; }
        public int TabIndex { get; set; }
        public virtual DockStyle Dock { get; set; }

        bool InternalLayoutSuspended;

        public void SuspendLayout()
        {
            InternalLayoutSuspended = true;

        }

        public void ResumeLayout(bool b)
        {
            InternalLayoutSuspended = false;
        }

        public void PerformLayout()
        { }
    }
}
