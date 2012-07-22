using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using java.lang.reflect;
using ScriptCoreLib;

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
}
