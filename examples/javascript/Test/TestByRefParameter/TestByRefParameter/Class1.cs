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
        public static void foo()
        {
            var b = 0;
            var c = 0;
            var s = "";

            b = 7;

            bar_ref(ref b);
            bar_ref(ref s);

            c = b;
            b = c;

            bar_value(b);
            bar_value(c);
            bar_value(s);
        }


        static void bar_ref(ref int t)
        {
            var tt2 = new int[1];
            var tt = new[] { 7 };

            bar_value(tt[0]);

            t = t + 2;

            bar_ref(ref t);
        }

        static void bar_value(int t)
        {
            t = t + 2;
        }

        static void bar_ref(ref string t)
        {
            var x = t;

            t = "foo";

            bar_ref(ref t);
        }

        static void bar_value(string t)
        {
            t = "foo";
        }

        public static void foo(ref string e)
        {
            //e = "ref " + e;
        }
    }
}
