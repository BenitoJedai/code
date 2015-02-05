using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]
namespace TestReturnBoolean
{
    public class Class1
    {
        static bool foo1() => true;
        static bool foo0() => false;
    }
}
