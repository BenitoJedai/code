using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data.Common
{
    [Script(Implements = typeof(global::System.Data.Common.DbParameterCollection))]
    public abstract class __DbParameterCollection 
    {
        public abstract int Count { get; }
    }

}
