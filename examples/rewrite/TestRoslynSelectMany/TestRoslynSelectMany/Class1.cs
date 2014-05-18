using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRoslynSelectMany
{
    public class Class1
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140518
        // if jsc stack rewrites this roslyn compiland it breaks it?

        public static void Main(string[] e)
        {
            // roslyn likes dup keyword. alot.

            var x =
                from p in Enumerable.Range(0, 1)
                from i in Enumerable.Range(0, 1)
                select i;
        }
    }
}
