using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
[assembly: Obfuscation(Feature = "script")]
namespace TestElementOfFunction
{
    class Program
    {
        static void Main(string[] args)
        {
            // Send it to the caller.
            var e = GetArray()[0];


        }

        private static string[] GetArray()
        {
            var s = new[] { "jsc" };
            return s;
        }
    }
}
