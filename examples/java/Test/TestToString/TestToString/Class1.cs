using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

[assembly: Obfuscation(Feature = "script")]

namespace TestToString
{
    public class Class1
    {
        public Class1(object e)
        {
            var s = e.ToString();
            var x = s + "hi";
        }
    }
}
