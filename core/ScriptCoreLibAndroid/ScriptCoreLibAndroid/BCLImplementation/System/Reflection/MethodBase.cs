using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.Android.BCLImplementation.System.Reflection
{
    [Script(Implements = typeof(MethodBase))]
    internal abstract class __MethodBase : __MemberInfo
    {
        public object Invoke(object obj, object[] parameters)
        {
            return null;
        }
    }
}
