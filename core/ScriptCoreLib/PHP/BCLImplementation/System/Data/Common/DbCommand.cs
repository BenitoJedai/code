﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.PHP.BCLImplementation.System.ComponentModel;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Data.Common
{
    [Script(Implements = typeof(global::System.Data.Common.DbCommand))]
    internal abstract class __DbCommand : __Component
    {
        public abstract int ExecuteNonQuery();

    }
}
