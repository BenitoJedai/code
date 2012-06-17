using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestFSharpCSharpIntelliSense
{
    public class Class1
    {
        public static void Foo()
        {
            __fun.Module1.Foo();

            TestFSharpCSharpIntelliSense.core.Class1.Foo();

        }
    }
}
