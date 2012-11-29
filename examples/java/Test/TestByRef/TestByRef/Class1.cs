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
            var r = 0;
            var c = new Class1();
            bar(ref c, out r);
        }

        static void bar(ref Class1 c, out int r)
        {
            r = 2;

            var x = c;
            var y = c;
            var z = new[] { c };

            c.invoke();

            c = null;

            bar(ref y, out r);
        }
    }
}
