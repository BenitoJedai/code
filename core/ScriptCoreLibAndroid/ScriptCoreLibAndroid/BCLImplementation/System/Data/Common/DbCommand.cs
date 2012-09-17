using ScriptCoreLibJava.BCLImplementation.System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android.BCLImplementation.System.Data.Common
{
    [Script(Implements = typeof(global::System.Data.Common.DbCommand))]
    internal abstract class __DbCommand : __Component
    {
        public abstract int ExecuteNonQuery();

    }
}
