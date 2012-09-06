using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

[assembly: Obfuscation(Feature = "script")]

namespace TestFieldInitializer
{
    public class Class1
    {
        public object foo;

        public static void h(Class1 c)
        {
            h(
                new Class1
                {
                    foo = c
                }
            );
        }
    }
}
