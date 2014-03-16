using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]
namespace TestAddFloat
{
    public class Class1
    {
        public static double add(double x, double y)
        {
            //     d = (((b + c)) >>> 0);
            return x + y;
        }

        public static double add(uint x, uint y)
        {
            //     d = (((b + c)) >>> 0);
            //     d = (((b + c) &  0xffffffff)) >>> 0);

            return x + y;
        }
    }
}
