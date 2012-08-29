using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
[assembly: Obfuscation(Feature = "script")]
namespace TestGenericToArrayCast
{
    public class List<T>
    {
        public T[] ToArray()
        {
            return null;
        }
    }

    public class Class1
    {
        static void Invoke(List<float> a, List<Class1> b)
        {
            var x = a.ToArray();
            var y = b.ToArray();
        }
    }
}
