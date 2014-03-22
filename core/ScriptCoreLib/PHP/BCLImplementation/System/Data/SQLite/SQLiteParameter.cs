using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Data.SQLite
{
    //[Script(Implements = typeof(global::System.Data.SQLite.SQLiteParameter))]
    internal class __SQLiteParameter : __DbParameter
    {

        public override string ParameterName { get; set; }
        public override object Value { get; set; }
    }
}
