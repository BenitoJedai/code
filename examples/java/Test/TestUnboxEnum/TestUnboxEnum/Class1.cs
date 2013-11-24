using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace TestUnboxEnum
{
    public enum XKey : long { }

    public class Class1
    {
        public Class1()
        {
            var x = default(XKey);
            object o = x;
            var y = (XKey)o;
        }

    }
}
