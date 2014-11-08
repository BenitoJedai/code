using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Reflection
{
    [Script(Implements = typeof(global::System.Reflection.MethodBase))]
    public abstract class __MethodBase : __MemberInfo
    {
        public object Invoke(object obj, object[] parameters)
        {
            return InternalInvoke(obj, parameters);
        }

        public virtual object InternalInvoke(object obj, object[] parameters)
        {
            return null;
        }

    }
}
