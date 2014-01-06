using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data
{
    [Script(Implements = typeof(global::System.Data.DataRowView))]
    public class __DataRowView
    {
        public DataRow Row { get; set; }
        public static implicit operator DataRowView(__DataRowView x)
        {
            return (DataRowView)(object)x;
        }
    }
}
