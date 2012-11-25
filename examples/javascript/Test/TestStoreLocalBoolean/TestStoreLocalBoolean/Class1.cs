using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]
namespace TestStoreLocalBoolean
{
    public class Class1
    {
        class x
        {
            public bool fld1 = true;
            public static bool fld2 = true;
        }

        public static void foo()
        {
            var loc1 = true;
            var loc2 = false;
        }
    }
}
