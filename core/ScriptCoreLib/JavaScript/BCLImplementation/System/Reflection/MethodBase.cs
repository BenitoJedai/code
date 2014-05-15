using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection
{
    [Script(Implements = typeof(global::System.Reflection.MethodBase))]
    public abstract class __MethodBase : __MemberInfo
    {
        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Reflection\MethodBase.cs

        public abstract ParameterInfo[] GetParameters();


        public abstract object InternalInvoke(object obj, object[] parameters);

        public object Invoke(object obj, object[] parameters)
        {
            return InternalInvoke(obj, parameters);
        }
    }
}
