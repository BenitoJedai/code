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
            bar(ref c);
        }

        static void bar(ref Class1 c)
        {
            var x = c;
            var y = c;
            var z = new[] { c };

            c.invoke();

            c = null;

            bar(ref y);
        }
    }
}
