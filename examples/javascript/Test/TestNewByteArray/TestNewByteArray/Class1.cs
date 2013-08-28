using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

[assembly: Obfuscation(Feature = "script")]

namespace TestNewByteArray
{
    public class Class1
    {
        public Class1()
        {
            //    b = new Array(16);
            var bytes = new byte[0x10];

        }
    }
}
