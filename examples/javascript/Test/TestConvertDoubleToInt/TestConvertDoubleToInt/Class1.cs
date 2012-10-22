using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]
namespace TestConvertDoubleToInt
{
    public class Class1
    {
        public static int __i4(double e)
        {
            return (int)e;
        }

        public static ushort __u2(double e)
        {
            return (ushort)e;
        }

        public static byte __u1(double e)
        {
            return (byte)e;
        }
    }
}
