using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]

namespace TestInterfaceProperty.Foo
{
    public interface ISupportsContainer
    {

        object Container { get; }
    }
}
