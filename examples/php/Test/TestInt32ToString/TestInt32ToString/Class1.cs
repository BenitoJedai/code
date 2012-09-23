using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]
namespace TestInt32ToString
{
    public static class Class1
    {
        static string Invoke(int i)
        {
            return i.ToString();
        }
    }
}
