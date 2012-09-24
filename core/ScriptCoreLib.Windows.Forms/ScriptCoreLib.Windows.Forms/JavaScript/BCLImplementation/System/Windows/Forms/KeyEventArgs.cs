using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.KeyEventArgs))]
    internal class __KeyEventArgs : EventArgs
    {
        public Keys KeyCode { get; set;  }

        public __KeyEventArgs(Keys k)
        {
            this.KeyCode = k;
        }
    }
}
