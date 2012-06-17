using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]
namespace TestOrOperator
{
    public class Class1
    {
        public static void Foo()
        { }

        public static bool Foo(bool a, bool b)
        {
            Foo();

            var x = a || b;

            Foo();

            return x;
        }

      
    }
}
