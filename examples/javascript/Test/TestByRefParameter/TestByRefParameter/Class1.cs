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
             var b =0;
             var c = 0;
             var d = new[] { 0 };

            bar(
                0, ref b, out c, ref d, 0, ref b, 0
                );
        }

        public static void bar<Ta, Tb, Tc, Td>(
            Ta a,
            ref Tb b,
            out Tc c,
            ref Td[] d,

            Tc cc,
            ref Tb bb,
            Td dd
            )
        {
            // http://geekswithblogs.net/ftom/archive/2008/09/10/c-and-the-difference-between-out-and-ref.aspx

            b = bb;

            c = cc;

            // later, we shall test this for java!
            d = new[] { dd };
        }
    }
}
