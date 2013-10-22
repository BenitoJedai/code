using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data
{
    [Script(Implements = typeof(global::System.Data.DataColumnChangeEventHandler))]
    public delegate void __DataColumnChangeEventHandler(object sender, DataColumnChangeEventArgs e);
}
