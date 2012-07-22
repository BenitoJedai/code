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
        public virtual object InternalInvoke(object obj, object[] parameters)
        {
            //throw new NotImplementedException();
            return null;
        }


        public object Invoke(object obj, object[] parameters)
        {
            return InternalInvoke(obj, parameters);
        }

    }
}
