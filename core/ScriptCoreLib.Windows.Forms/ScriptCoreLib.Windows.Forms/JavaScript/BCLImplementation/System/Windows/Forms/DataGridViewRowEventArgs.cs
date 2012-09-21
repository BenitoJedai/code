using ScriptCoreLib.Shared.BCLImplementation.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.DataGridViewRowEventArgs))]
    internal class __DataGridViewRowEventArgs : __EventArgs
    {
        public __DataGridViewRowEventArgs(DataGridViewRow r)
        {
            this.Row = r;
        }

        public DataGridViewRow Row { get; set; }
    }
}
