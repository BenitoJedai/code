using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace TestProtectedIfElse
{
    public class Class1
    {
        public static MethodInfo Foo(MethodBase x)
        {
            try
            {
                if (x is MethodInfo)
                    return (MethodInfo)x;
                else
                    return null;
            }
            catch 
            {
            }

            return null;
        }
    }
}
