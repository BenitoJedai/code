using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]
namespace TestAsString
{
    public class Class1
    {
        public static void foo(object e)
        {
            var x = e as string;

            var y = e as Class1;
        }
    }
}
