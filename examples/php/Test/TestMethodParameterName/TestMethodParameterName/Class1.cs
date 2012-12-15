using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

[assembly: Obfuscation(Feature = "script")]

namespace TestMethodParameterName
{
    public class Class1
    {
        public void foo(string e)
        {
            var a = e;
        }

        public void foo<T>(ref T e, T x)
        {
            var u = e;

            e = x;

        }

        //[Script(DefineAsStatic = true)]
        public void Split(params char[] a0)
        {
            var c = (int)a0[0];

        }
    }
}
