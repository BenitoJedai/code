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
        { 
        
        }

        public static bool Foo(bool a, bool b)
        {
            Foo();

            var x = a || b;

            Foo();

            var y = a && b;

            Foo();

            if (a || b)
                Foo();
            else
                Foo();

            Foo();

            if (a && b)
                Foo();
            else
                Foo();

            return x;
        }

      
    }
}
