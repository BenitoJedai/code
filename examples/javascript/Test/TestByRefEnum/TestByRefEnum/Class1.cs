using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace TestByRefEnum
{
    enum Foo
    {

    }

    public class Class1
    {
        public Class1()
        {
            var x = default(Foo);
            var y = 0;

            foo(ref x, ref y);
        }

        static void foo(ref Foo foo, ref int zoo)
        {
        }
    }
}
