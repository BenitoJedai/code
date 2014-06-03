using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Reflection
{
    [Script(Implements = typeof(global::System.Reflection.MethodBase))]
    internal class __MethodBase : __MemberInfo
    {
        public override string Name
        {
            get { return ""; }

        }

        public override Type DeclaringType
        {
            get { throw new NotImplementedException(); }
        }
    }
}
