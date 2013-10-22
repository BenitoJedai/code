using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data
{
    [Script(Implements = typeof(global::System.Data.DataColumnChangeEventArgs))]
    public class __DataColumnChangeEventArgs
    {
        public __DataColumnChangeEventArgs(DataRow row, DataColumn column, object value)
        {
            this.Row = row;
            this.Column = column;
            this.ProposedValue = value;
        }


        public DataColumn Column { get; set; }
        public object ProposedValue { get; set; }
        public DataRow Row { get; set; }

    }
}
