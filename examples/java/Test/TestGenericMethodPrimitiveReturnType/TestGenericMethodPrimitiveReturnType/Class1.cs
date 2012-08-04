using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
[assembly: Script, ScriptTypeFilter(ScriptType.Java)]

namespace TestGenericMethodPrimitiveReturnType
{
    [Script]
    public class Class1
    {
        public static bool Test(Class1<bool> x, bool y = true)
        {
            Class1<bool> z = x;

            var value = z.Invoke(y);

            return value;
        }
    }

    [Script]
    public class Class1<T>
    {
        public  T Invoke(T e)
        {
            return e;
        }
    }
}
