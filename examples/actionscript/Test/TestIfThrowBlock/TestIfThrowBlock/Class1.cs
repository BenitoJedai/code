using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
[assembly: Obfuscation(Feature = "script")]
namespace TestIfThrowBlock
{
    public static class Class1
    {
        public static T CheckThis<T>(T x) where T : class
        {
            if (x == null)
            {
                throw null;
            }
            return x;
        }
    }
}
