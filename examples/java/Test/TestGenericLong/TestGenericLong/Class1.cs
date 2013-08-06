using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

[assembly: Obfuscation(Feature = "script")]

namespace TestGenericLong
{
    public class Class0<T, T2>
    {
        public T t;
        public T2 t2;
    }

    enum Long : long { };

    public class Class1
    {
        public void foo<T, T2>(T t, T2 t2)
        {

        }


        public void foo()
        {

            foo<Long, long>(0, 0);

            var x = new Class0<Long, long>();

        }
    }
}
