using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]
namespace TestUInt32NameClash
{
    public class Class1
    {
        public static void GetBytes(int value)
        { }
        public static void GetBytes(uint value)
        { }

    }
}
