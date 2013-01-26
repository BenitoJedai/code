using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]
namespace TestEmptyDoubleArray
{
    public class Class1
    {
        static void u()
        {
            var x = new double[] { 0, 0 };
        }

        static void u2()
        {
            var x = new double[2] ;
        }

        static void i()
        {
            var x = new int[] { 0, 0 };
        }

        static void i2()
        {
            var x = new int[2];
        }
    }
}
