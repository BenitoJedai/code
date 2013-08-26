using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection
{
    [Script(Implements = typeof(global::System.Reflection.MethodBase))]
    public abstract class __MethodBase : __MemberInfo
    {
        public abstract object InternalInvoke(object obj, object[] parameters);

        public object Invoke(object obj, object[] parameters)
        {
            return InternalInvoke(obj, parameters);
        }
    }
}
