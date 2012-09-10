using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]
namespace TestTypeOrdering
{
    public class Class1 : Class4
    {
    }

    public class Class2
    {
    }

    public class Class3 : Class1
    {
    }

    public class Class4
    {
    }
}
