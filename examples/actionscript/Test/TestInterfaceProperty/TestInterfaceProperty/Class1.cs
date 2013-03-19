using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TestInterfaceProperty.Foo;

[assembly: Obfuscation(Feature = "script")]

namespace TestInterfaceProperty
{


    public class Class1 : ISupportsContainer
    {
        // what about metadata name clash?
        public object Container { get { return null; } }
    }
}
