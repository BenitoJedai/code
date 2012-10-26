using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]

namespace TestGenericTypeStaticPropertyCall
{
    public static class X<T>
    {
        static public T value
        {
            get
            {
                throw null;
            }
        }
    }

    public class Class1
    {
        public void foo()
        {
            var x = X<Class1>.value;

        }
    }
}
