using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Drawing;
using ScriptCoreLib.JavaScript.Windows.Forms;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Drawing;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    public partial class __Control
    {
        public event LayoutEventHandler Layout;

        public void PerformLayout()
        {
            if (Layout != null)
                Layout(this, new LayoutEventArgs(this, ""));
        }

        bool InternalLayoutSuspended;

        public void SuspendLayout()
        {
            InternalLayoutSuspended = true;

        }

        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.Control.ResumeLayout()]



        public void ResumeLayout()
        {
            InternalLayoutSuspended = false;
        }

        public void ResumeLayout(bool performLayout)
        {
            InternalLayoutSuspended = false;

            if (performLayout)
                PerformLayout();
        }
    }
}
