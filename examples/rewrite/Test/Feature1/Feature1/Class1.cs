using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "merge")]
namespace Feature2
{
    public class Class1
    {
        public static string Foo()
        {
            return "hi - test";
        }
    }
}
