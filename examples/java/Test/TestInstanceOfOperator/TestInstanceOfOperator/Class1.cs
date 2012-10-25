using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]

namespace TestInstanceOfOperator
{
    public static class Class1
    {
        public static bool IsInteger<T>(this T a)
        {
            return a is int;
        }

    }
}
