using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]

namespace TestByRefUInt32Add
{
    public class Class1
    {
        public static void foo(ref uint a, uint b)
        {
            a += b;

        }
    }
}
