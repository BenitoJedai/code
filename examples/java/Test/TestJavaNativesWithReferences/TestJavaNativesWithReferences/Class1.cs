using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

[assembly: ObfuscationAttribute(Feature = "script")]

namespace TestJavaNativesWithReferences
{
    public class Class1
    {
        public Class2 field2;
    }



    public class Class2
    {
        public Class1 field1;
    }
}
