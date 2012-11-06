using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]

namespace TestStruct
{
    public struct Class2
    {
        public void Foo()
        {
            var c = new Class1();
        }
    }
    public struct Class1
    {
        public int i;
    }
}
