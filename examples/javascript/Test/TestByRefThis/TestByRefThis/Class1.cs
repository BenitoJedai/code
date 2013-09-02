using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]
namespace TestByRefThis
{
    public struct Class1
    {
        public void foo()
        {
            X.foo(ref this);


            var that = this;

            X.foo(ref that);
        }
    }

    public static class X
    {
        public static void foo(ref Class1 c)
        {

        }
    }
}
