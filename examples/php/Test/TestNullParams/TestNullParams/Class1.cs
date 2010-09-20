using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
[assembly: Obfuscation(Feature = "script")]
namespace TestNullParams
{
    public class Class1
    {
        void Foo(params object[] e)
        {
        }

        void FooEmptyArray()
        {
            Foo();
        }

        void FooNullArray()
        {
            Foo(null);
        }
    }
}
