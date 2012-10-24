using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

[assembly: Obfuscation(Feature = "script")]

namespace TestGenericTypeOperators
{
    internal class __SZArrayEnumerator<T>
    {
        public static X foo<X>(X x)
        {
            return x;
        }

        public __SZArrayEnumerator(T[] e = null)
        {
            __SZArrayEnumerator<T>.foo<int>(0);
            __SZArrayEnumerator<T> x = e;
        }

        public static implicit operator __SZArrayEnumerator<T>(T[] e)
        {
            return new __SZArrayEnumerator<T>();
        }
    }
}
