using ScriptCoreLib.Shared.BCLImplementation.System;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using ScriptCoreLib.Shared.BCLImplementation.System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.DataGridViewCellFormattingEventArgs))]
    internal class __DataGridViewCellFormattingEventArgs : __ConvertEventArgs
    {
        public __DataGridViewCellFormattingEventArgs(int columnIndex, int rowIndex, object value, Type desiredType, DataGridViewCellStyle cellStyle)
        {
            this.ColumnIndex = columnIndex;
            this.RowIndex = rowIndex;

            this.Value = value;

            //this.des

            this.CellStyle = cellStyle;
        }

        public int ColumnIndex { get; set; }
        public int RowIndex { get; set; }

        public object FormattedValue { get; set; }

        public DataGridViewCellStyle CellStyle { get; set; }
    }
}
