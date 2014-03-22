using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data.Common
{
    [Script(Implements = typeof(global::System.Data.Common.DbParameter))]
    public abstract class __DbParameter : __IDbDataParameter
    {
        public abstract string ParameterName { get; set; }
        public abstract object Value { get; set; }

        public void Dispose()
        {
        }
    }
}
