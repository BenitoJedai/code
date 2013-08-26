using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection
{
    [Script(Implements = typeof(global::System.Reflection.MemberInfo))]
    public abstract class __MemberInfo
    {
        public abstract string Name
        {
            get;
        }

        //script: error JSC1000: No implementation found for this native method, please implement [static System.Reflection.MethodInfo.op_Equality(System.Reflection.MethodInfo, System.Reflection.MethodInfo)]

        public abstract object[] GetCustomAttributes(Type x, bool inherit);
        public abstract object[] GetCustomAttributes(bool inherit);
    }
}
