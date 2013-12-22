using ScriptCoreLib.Shared.BCLImplementation.System;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.DataGridViewCellValidatingEventArgs))]
    internal class __DataGridViewCellValidatingEventArgs : __CancelEventArgs
    {
        public __DataGridViewCellValidatingEventArgs(int c, int r)
        {
            this.ColumnIndex = c;
            this.RowIndex = r;
        }

        public int ColumnIndex { get; set; }
        public int RowIndex { get; set; }

        public object FormattedValue { get; set;  }
    }
}
