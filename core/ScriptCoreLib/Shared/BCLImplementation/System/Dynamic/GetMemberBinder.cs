using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Dynamic
{
    [Script(Implements = typeof(global::System.Dynamic.GetMemberBinder))]
    internal class __GetMemberBinder : __DynamicMetaObjectBinder
    {
        public string Name { get; set; }
    }
}
