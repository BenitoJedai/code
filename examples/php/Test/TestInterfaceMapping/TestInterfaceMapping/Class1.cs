using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]

namespace TestInterfaceMapping
{
    public interface IClass1
    {
        void foo(ref string foo, int bar);
    }

    public class Class1 : IClass1
    {

        void IClass1.foo(ref string foo, int bar)
        {
        }
    }
}
