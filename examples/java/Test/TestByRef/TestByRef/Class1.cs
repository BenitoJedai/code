using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

[assembly: Obfuscation(Feature = "script")]

namespace TestByRef
{
    public class Class1
    {
        void invoke()
        {

        }

        static void foo()
        {
            var c = new Class1();

        }

        static void bar(ref Class1 c)
        {
            c.invoke();

            c = null;
        }
    }
}
