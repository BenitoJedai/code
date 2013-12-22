using ScriptCoreLib.Shared.BCLImplementation.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.DataGridViewColumnEventArgs))]
    internal class __DataGridViewColumnEventArgs : __EventArgs
    {
        public __DataGridViewColumnEventArgs(DataGridViewColumn dataGridViewColumn)
        {
            Column = dataGridViewColumn;
        }

        public DataGridViewColumn Column { get; set; }
    }
}