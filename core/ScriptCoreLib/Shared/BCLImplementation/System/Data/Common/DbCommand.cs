using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data.Common
{
    [Script(Implements = typeof(global::System.Data.Common.DbCommand))]
    public abstract class __DbCommand : __Component
    {
        public abstract int ExecuteNonQuery();

    }
}
