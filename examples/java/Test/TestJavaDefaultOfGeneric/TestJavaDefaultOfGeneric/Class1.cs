using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]
namespace TestJavaDefaultOfGeneric
{
    public class Class1
    {
        public static T FirstOrDefault<T>(T[] source)
        {
            var r = default(T);

            foreach (var item in source)
            {
                r = item;
                break;
            }

            return r;
        }
    }
}
