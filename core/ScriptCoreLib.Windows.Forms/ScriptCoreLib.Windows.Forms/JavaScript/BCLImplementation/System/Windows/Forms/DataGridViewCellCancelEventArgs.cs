using ScriptCoreLib.Shared.BCLImplementation.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.DataGridViewCellCancelEventArgs))]
    internal class __DataGridViewCellCancelEventArgs : __EventArgs
    {
        public __DataGridViewCellCancelEventArgs(int c, int r)
        {
            this.ColumnIndex = c;
            this.RowIndex = r;
        }

        public int ColumnIndex { get; set; }
        public int RowIndex { get; set; }
    }
}
