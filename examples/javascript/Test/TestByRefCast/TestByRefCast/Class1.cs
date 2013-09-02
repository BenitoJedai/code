using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

[assembly: Obfuscation(Feature = "script")]
namespace TestByRefCast
{
    public class Class1
    {
        void f()
        {

        }

        void f<T>(ref T t)
        {
            // box opcode!

            var x = t;
            var y = (Class1)(object)x;

            var g = (Class1)(object)t;
        }

    }
}
