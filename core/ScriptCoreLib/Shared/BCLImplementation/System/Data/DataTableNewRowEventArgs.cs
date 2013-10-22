using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data
{
    [Script(Implements = typeof(global::System.Data.DataTableNewRowEventArgs))]
    public class __DataTableNewRowEventArgs
    {
        public __DataTableNewRowEventArgs(DataRow dataRow)
        {
            this.Row = dataRow;
        }

        public DataRow Row { get; set; }




    }
}
