using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

[assembly: Obfuscation(Feature = "script")]
namespace TestByRefParameter
{
    public class Class1
    {
        //static int x;

        public static void foo()
        {
            var b = 0;
            var c = 0;

            bar_ref(ref b);
            bar_value(b);
            bar_value(c);
        }

       
        static void bar_ref(ref int t)
        {
            t = t + 2;
        }

        static void bar_value(int t)
        {
            t = t + 2;
        }
    }
}
