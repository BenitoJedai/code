using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.Android.BCLImplementation.System.Reflection
{
    [Script(Implements = typeof(MethodInfo))]
    internal class __MethodInfo : __MethodBase
    {
        public global::java.lang.reflect.Method InternalMethod;


        public override object InternalInvoke(object obj, object[] parameters)
        {
            var n = default(object);

            try
            {
                n = this.InternalMethod.invoke(obj, parameters);
            }
            catch //(csharp.ThrowableException e)
            {
                //((Throwable)(object)e).printStackTrace();

                //throw new csharp.RuntimeException(e.Message);
            }

            return n;
        }
    }
}
