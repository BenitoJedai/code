using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]
namespace TestDoubleArray
{
    public class Class1
    {
        // b = [0, 2.4375, 0, 1.875, 0, 2, 0, 2.125, 0, 2.25, 0, 2.3125, 0, 2.375, -3.089003E-27, 2.50025];

        static double[] Invoke() => new[] { 7.0, 1, 2, 3, 4, 5, 6, 8.008 };

    }
}
