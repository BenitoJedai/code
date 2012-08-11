using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using java.lang.reflect;
using ScriptCoreLib;
using ScriptCoreLib.Android.BCLImplementation.System;
using ScriptCoreLibJava.BCLImplementation.System;

namespace java.lang
{
    [Script(IsNative = true)]
    public class Class
    {
        public Method getDeclaredMethod(string @name, Class[] @parameterTypes)
        {
            return default(Method);
        }
    }

    public static class ClassExtensions
    {
        public static Type ToType(this Class c)
        {
            return (__Type)c;
        }

        public static Class ToClass(this System.Type t)
        {
            var tt = (__Type)(object)t;

            return tt.InternalTypeDescription;
        }
    }
}
