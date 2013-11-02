using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data
{
    [Script(Implements = typeof(global::System.Data.DataRowChangeEventArgs))]
    public class __DataRowChangeEventArgs
    {
        public __DataRowChangeEventArgs(DataRow row, DataRowAction action)
        {
            this.Row = row;
            this.Action = Action;
        }


        public DataRowAction Action { get; set; }
        public DataRow Row { get; set; }
    }
}
