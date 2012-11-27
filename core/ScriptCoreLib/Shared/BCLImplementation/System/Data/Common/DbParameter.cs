using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data.Common
{
    [Script(Implements = typeof(global::System.Data.Common.DbParameter))]
    internal abstract class __DbParameter
    {
        public abstract string ParameterName { get; set; }
        public abstract object Value { get; set; }
    }
}
